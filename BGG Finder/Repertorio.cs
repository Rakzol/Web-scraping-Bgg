using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace BGG_Finder
{
    class Repertorio
    {
        public Dictionary<String, Juego> juegos = new Dictionary<String, Juego>();
        public Dictionary<String, Rival> rivales = new Dictionary<String, Rival>();
        public Dictionary<String, Enlace> autores = new Dictionary<String, Enlace>();
        public Dictionary<String, Enlace> categorias = new Dictionary<String, Enlace>();
        public Dictionary<String, Enlace> mecanicas = new Dictionary<String, Enlace>();
        public Dictionary<String, Enlace> familias = new Dictionary<String, Enlace>();
        public int minimoJuegadores = -1;
        public int maximoJugadores = -1;
        public Enlace autorPreferido;
        public Juego juegoMasJugado;
        public Juego juegoMenosJugado;
        public Juego juegoUltimoJugado;
        public Juego juegoMuchoSinJugar;
        public Rival rivalMasGanado;
        public Rival rivalMasPerdido;
        public Repertorio(XmlDocument xmlRepertorio)
        {
            foreach (XmlNode xmlJuego in xmlRepertorio.SelectNodes("items/item"))
            {
                Juego juego = new Juego(xmlJuego);
                juegos.Add(juego.id, juego);

                agregarEnlaces(juego, juego.autores, autores, xmlJuego.SelectNodes("link[@type='boardgamedesigner']"));
                agregarEnlaces(juego, juego.categorias, categorias, xmlJuego.SelectNodes("link[@type='boardgamecategory']"));
                agregarEnlaces(juego, juego.mecanicas, mecanicas, xmlJuego.SelectNodes("link[@type='boardgamemechanic']"));
                agregarEnlaces(juego, juego.familias, familias, xmlJuego.SelectNodes("link[@type='boardgamefamily']"));

                minimoJuegadores = juego.minimoJuegadores < minimoJuegadores || minimoJuegadores == -1 ? juego.minimoJuegadores : minimoJuegadores;
                maximoJugadores = juego.maximoJugadores > maximoJugadores || maximoJugadores == -1 ? juego.maximoJugadores : maximoJugadores;

                juegoMasJugado = juegoMasJugado == null ? juego : juego.jugadas > juegoMasJugado.jugadas ? juego : juegoMasJugado;
                juegoMenosJugado = juegoMenosJugado == null ? juego : juego.jugadas < juegoMenosJugado.jugadas ? juego : juegoMenosJugado;

                if (juego.ultimaVezJugado.Year != 1)
                {
                    juegoUltimoJugado = juegoUltimoJugado == null ? juego : DateTime.Compare(juego.ultimaVezJugado, juegoUltimoJugado.ultimaVezJugado) > 0 ? juego : juegoUltimoJugado;
                    juegoMuchoSinJugar = juegoMuchoSinJugar == null ? juego : DateTime.Compare(juego.ultimaVezJugado, juegoMuchoSinJugar.ultimaVezJugado) < 0 ? juego : juegoMuchoSinJugar;
                }

                String idUsuario = xmlJuego.SelectSingleNode("plays/@userid").Value;
                bool esCooperativo = xmlJuego.SelectSingleNode("link[@type='boardgamemechanic'][@id='2023']") != null;

                if (!esCooperativo)
                {
                    foreach (XmlNode nodoJugada in xmlJuego.SelectNodes("plays/play"))
                    {
                        foreach (XmlNode nodoRival in nodoJugada.SelectNodes("players/player"))
                        {
                            if (!nodoRival.Attributes["userid"].Value.Equals(idUsuario))
                            {
                                String llaveRival = (!nodoRival.Attributes["username"].Value.Equals("") ? "user" : "") + "name";
                                String nombreRival = nodoRival.Attributes[llaveRival].Value;

                                Rival rival = rivales.ContainsKey(nombreRival.ToLower()) ? rivales[nombreRival.ToLower()] : null;
                                if (rival == null)
                                {
                                    rival = new Rival(true, nombreRival);
                                    rivales.Add(nombreRival.ToLower(), rival);
                                }

                                if (!rival.jugadas.ContainsKey(juego.id))
                                {
                                    String rutaRival = "players/player[@" + llaveRival + "='" + nombreRival + "']";
                                    String rutaUsuario = "players/player[@userid='" + idUsuario + "']";
                                    Jugada jugada;
                                    if (xmlJuego.SelectNodes("plays/play[count(" + rutaRival + ")>'1']").Count == 0)
                                    {
                                        jugada = new Jugada(juego,
                                            xmlJuego.SelectNodes("plays/play[" + rutaRival + "][" + rutaUsuario + "]").Count,
                                            xmlJuego.SelectNodes("plays/play[" + rutaRival + "[@win='1']][" + rutaUsuario + "[@win='0']]").Count,
                                            xmlJuego.SelectNodes("plays/play[" + rutaRival + "[@win='0']][" + rutaUsuario + "[@win='1']]").Count
                                        );
                                    }
                                    else
                                    {
                                        jugada = new Jugada(juego,
                                            xmlJuego.SelectNodes("plays/play/" + rutaRival + "[../../" + rutaUsuario + "]").Count,
                                            xmlJuego.SelectNodes("plays/play/" + rutaRival + "[@win='1'][../../" + rutaUsuario + "[@win='0']]").Count,
                                            xmlJuego.SelectNodes("plays/play/" + rutaRival + "[@win='0'][../../" + rutaUsuario + "[@win='1']]").Count
                                        );
                                        rival.esIndividual = false;
                                    }
                                    if (jugada.jugadas > 0)
                                    {
                                        rival.agregarJugada(jugada);
                                        juego.rivales.Add(nombreRival.ToLower(), rival);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Rival rivalJuego = new Rival(false, juego.nombre, new Jugada(juego, juego.jugadas, juego.derrotas, juego.victorias));
                    rivales.Add(juego.nombre.ToLower(), rivalJuego);
                    juego.rivales.Add(juego.nombre.ToLower(), rivalJuego);
                }
            }

            foreach (Enlace autor in autores.Values)
            {
                autorPreferido = autorPreferido == null ? autor : autor.juegos.Count > autorPreferido.juegos.Count ? autor : autorPreferido;
            }

            foreach (Rival rival in rivales.Values)
            {
                rivalMasGanado = rivalMasGanado == null ? rival : rival.derrotas > rivalMasGanado.derrotas && rival.esIndividual ? rival : rivalMasGanado;
                rivalMasPerdido = rivalMasPerdido == null ? rival : rival.victorias > rivalMasPerdido.victorias && rival.esIndividual ? rival : rivalMasPerdido;
            }
        }
        private void agregarEnlaces(Juego juego, Dictionary<String, Enlace> diccionarioEnlaceJuego, Dictionary<String, Enlace> diccionarioEnlace, XmlNodeList xmlNodosEnlace)
        {
            if (xmlNodosEnlace.Count == 0)
            {
                agregarEnlace(juego, diccionarioEnlaceJuego, diccionarioEnlace, "0", "( Desconocido )");
            }
            foreach (XmlNode xmlNodoEnlace in xmlNodosEnlace)
            {
                agregarEnlace(juego, diccionarioEnlaceJuego, diccionarioEnlace, xmlNodoEnlace.Attributes["id"].Value, xmlNodoEnlace.Attributes["value"].Value);
            }
        }
        private void agregarEnlace(Juego juego, Dictionary<String, Enlace> diccionarioEnlaceJuego, Dictionary<String, Enlace> diccionarioEnlace, String idEnlace, String valorEnlace)
        {
            Enlace enlace = diccionarioEnlace.ContainsKey(idEnlace) ? diccionarioEnlace[idEnlace] : null;
            if (enlace == null)
            {
                enlace = new Enlace(idEnlace, valorEnlace);
                diccionarioEnlace.Add(enlace.id, enlace);
            }
            enlace.juegos.Add(juego.id, juego);
            diccionarioEnlaceJuego.Add(enlace.id, enlace);
        }
    }
}
