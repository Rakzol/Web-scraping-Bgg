using System;
using System.Collections.Generic;
using System.Xml;

namespace BGG_Finder
{
    class Juego
    {
        public String id;
        public String nombre;
        public String añoPublicación;
        public String rating;
        public String cadenaAutores;
        public String cadenaArtistas;
        public int minimoJuegadores;
        public int maximoJugadores;
        public int jugadas;
        public int victorias;
        public int derrotas;
        public int empates;
        public int recomendados;
        public DateTime ultimaVezJugado;
        public Dictionary<int, NumeroJugadores> numerosJuegadores = new Dictionary<int, NumeroJugadores>();
        public Dictionary<String, Rival> rivales = new Dictionary<String, Rival>();
        public Dictionary<String, Enlace> autores = new Dictionary<String, Enlace>();
        public Dictionary<String, Enlace> categorias = new Dictionary<String, Enlace>();
        public Dictionary<String, Enlace> mecanicas = new Dictionary<String, Enlace>();
        public Dictionary<String, Enlace> familias = new Dictionary<String, Enlace>();
        public Juego(XmlNode xmlNodoJuego)
        {
            id = xmlNodoJuego.SelectSingleNode("@id").Value;
            nombre = xmlNodoJuego.SelectSingleNode("name[@type='primary']/@value").Value;
            añoPublicación = xmlNodoJuego.SelectSingleNode("yearpublished/@value").Value;
            rating = xmlNodoJuego.SelectSingleNode("statistics/ratings/average/@value").Value;
            minimoJuegadores = int.Parse(xmlNodoJuego.SelectSingleNode("minplayers/@value").Value);
            maximoJugadores = int.Parse(xmlNodoJuego.SelectSingleNode("maxplayers/@value").Value);

            foreach (XmlNode nodoAutor in xmlNodoJuego.SelectNodes("link[@type='boardgamedesigner']/@value"))
            {
                cadenaAutores += nodoAutor.Value + ";\n";
            }

            foreach (XmlNode nodoArtista in xmlNodoJuego.SelectNodes("link[@type='boardgameartist']/@value"))
            {
                cadenaArtistas += nodoArtista.Value + ";\n";
            }

            String idUsuario = xmlNodoJuego.SelectSingleNode("plays/@userid").Value;
            bool esCooperativo = xmlNodoJuego.SelectSingleNode("link[@type='boardgamemechanic'][@id='2023']") != null;
            String rutaNodo = "plays/play[players/player[@userid='" + idUsuario + "']";

            jugadas = xmlNodoJuego.SelectNodes(rutaNodo + "]").Count;
            victorias = xmlNodoJuego.SelectNodes(rutaNodo + "[@win='1']]" + (!esCooperativo ? "[count(players/player[@win='1'])='1']" : "")).Count;
            derrotas = xmlNodoJuego.SelectNodes(rutaNodo + "[@win='0']]" + (!esCooperativo ? "[count(players/player[@win='1'])>'0']" : "")).Count;//|count(players/player[@win='0'])='1'
            empates = (!esCooperativo ? jugadas - (victorias + derrotas) : 0);

            XmlNode nodoFecha = xmlNodoJuego.SelectSingleNode(rutaNodo + "]/@date");
            //XmlNode nodoFecha = xmlNodoJuego.SelectSingleNode("plays/play/@date");
            ultimaVezJugado = nodoFecha != null ? DateTime.Parse(nodoFecha.Value) : ultimaVezJugado;

            String cadenaRecomendados = xmlNodoJuego.SelectSingleNode("poll[@name='suggested_numplayers']/results[ not(../results/result[@value='Best']/@numvotes > result[@value='Best']/@numvotes) ]/@numplayers").Value;
            recomendados = int.Parse(cadenaRecomendados.IndexOf('+') != -1 ? cadenaRecomendados.Remove(cadenaRecomendados.IndexOf('+')) : cadenaRecomendados);
            recomendados = recomendados >= minimoJuegadores && recomendados <= maximoJugadores ? recomendados : -1;

            for (int cantidadJugadores = minimoJuegadores; cantidadJugadores <= maximoJugadores; cantidadJugadores++)
            {
                NumeroJugadores numeroJugadores = new NumeroJugadores();
                rutaNodo = "plays/play[count(players/player)='" + cantidadJugadores + "'][players/player[@userid='" + idUsuario + "']";
                numeroJugadores.cantidadJugadores = cantidadJugadores;
                numeroJugadores.jugadas = xmlNodoJuego.SelectNodes(rutaNodo + "]").Count;
                numeroJugadores.victorias = xmlNodoJuego.SelectNodes(rutaNodo + "[@win='1']]" + (!esCooperativo ? "[count(players/player[@win='1'])='1']" : "")).Count;
                numeroJugadores.derrotas = xmlNodoJuego.SelectNodes(rutaNodo + "[@win='0']]" + (!esCooperativo ? "[count(players/player[@win='1'])>'0']" : "")).Count;//|count(players/player[@win='0'])='1'
                numeroJugadores.empates = (!esCooperativo ? numeroJugadores.jugadas - (numeroJugadores.victorias + numeroJugadores.derrotas) : 0);
                numerosJuegadores.Add(cantidadJugadores, numeroJugadores);
            }

        }
    }
}
