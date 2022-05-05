using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace BGG_Finder
{
    static class Utilerias
    {
        static int tiempoEsperaExcepcion = 1000;
        static int tiempoEsperaConsulta = 0;
        static String consultarWeb(String URL)
        {
            WebRequest solicitud = WebRequest.Create(URL);
            Thread.Sleep(tiempoEsperaConsulta);
            WebResponse respuesta = solicitud.GetResponse();
            Stream flujo = respuesta.GetResponseStream();
            StreamReader lector = new StreamReader(flujo);
            String cadena = lector.ReadToEnd();
            return cadena;
        }
        static XmlDocument consultarXml(String URL)
        {
            String xmlCadena = consultarWeb(URL);
            XmlDocument xmlDocumento = new XmlDocument();
            xmlDocumento.LoadXml(xmlCadena);
            return xmlDocumento;
        }
        public static XmlDocument consultarXmlUsuario(String nombreUsuario, String directorioXmlUsuarios)
        {
            XmlDocument xmlUsuario = new XmlDocument();
            if (File.Exists(directorioXmlUsuarios + "/" + nombreUsuario + ".xml"))
            {
                xmlUsuario.Load(directorioXmlUsuarios + "/" + nombreUsuario + ".xml");
                return xmlUsuario;
            }
            String urlConsultaXmlUsuario = "https://www.boardgamegeek.com/xmlapi2/user?name=" + nombreUsuario;
            while (true)
            {
                try
                {
                    xmlUsuario = consultarXml(urlConsultaXmlUsuario);
                }
                catch (WebException ex)
                {
                    if (!ex.Status.Equals(WebExceptionStatus.ProtocolError))
                    {
                        throw new Exception("No se puede acceder al usuario de manera local ni en internet");
                    }
                }
                if (xmlUsuario.SelectSingleNode("user") != null)
                {
                    if (!xmlUsuario.SelectSingleNode("user/@id").Value.Equals(""))
                    {
                        xmlUsuario.Save(directorioXmlUsuarios + "/" + nombreUsuario + ".xml");
                        return xmlUsuario;
                    }
                    else
                    {
                        throw new Exception("El usuario no esta registrado en BGG");
                    }
                }
                Thread.Sleep(tiempoEsperaExcepcion);
            }
        }
        public static XmlDocument consultarXmlJuego(String idJuego, String directorioXmlJuegos, String directorioImagenesJuegos, String directorioImagenesAutores)
        {
            XmlDocument xmlJuego = new XmlDocument();
            if (File.Exists(directorioXmlJuegos + "/" + idJuego + ".xml"))
            {
                xmlJuego.Load(directorioXmlJuegos + "/" + idJuego + ".xml");
                return xmlJuego;
            }
            String urlConsultaXmlJuego = "https://www.boardgamegeek.com/xmlapi2/thing?stats=1&id=" + idJuego;
            while (true)
            {
                try
                {
                    xmlJuego = consultarXml(urlConsultaXmlJuego);
                }
                catch (WebException ex)
                {
                    if (!ex.Status.Equals(WebExceptionStatus.ProtocolError))
                    {
                        throw new Exception("No se puede acceder al juego de mesa de manera local ni en internet");
                    }
                }
                if (xmlJuego.SelectSingleNode("items") != null)
                {
                    if (xmlJuego.SelectSingleNode("items/item") != null)
                    {
                        XmlNode nodoImagen = xmlJuego.SelectSingleNode("items/item/thumbnail");
                        WebClient clienteWeb = new WebClient();
                        if (nodoImagen != null)
                        {
                            try
                            {
                                clienteWeb.DownloadFile(nodoImagen.InnerText, directorioImagenesJuegos + "/" + idJuego + ".jpg");
                            }
                            catch (Exception ex) { }
                        }
                        foreach (XmlNode xmlNodoAutor in xmlJuego.SelectNodes("items/item/link[@type='boardgamedesigner']/@id"))
                        {
                            if (!File.Exists(directorioImagenesAutores + "/" + xmlNodoAutor.Value + ".jpg"))
                            {
                                try
                                {
                                    String htmlCadenaAutor = consultarWeb("https://boardgamegeek.com/boardgamedesigner/" + xmlNodoAutor.Value);
                                    HtmlAgilityPack.HtmlDocument htmlAutor = new HtmlAgilityPack.HtmlDocument();
                                    htmlAutor.LoadHtml(htmlCadenaAutor);
                                    String urlImagenAutor = htmlAutor.DocumentNode.SelectSingleNode("//div[@id='module_2']/table/tr/td/div/a/img").GetAttributeValue("src", "");
                                    clienteWeb.DownloadFile(urlImagenAutor, directorioImagenesAutores + "/" + xmlNodoAutor.Value + ".jpg");
                                }
                                catch (Exception ex) { }
                            }
                        }
                        xmlJuego.Save(directorioXmlJuegos + "/" + idJuego + ".xml");
                        return xmlJuego;
                    }
                    else
                    {
                        throw new Exception("El juego de mesa no esta registrado en BGG");
                    }
                }
                Thread.Sleep(tiempoEsperaExcepcion);
            }
        }
        public static XmlDocument consultarXmlColeccion(String nombreUsuario, String directorioXmlColecciones, bool actualizar)
        {
            XmlDocument xmlColeccion = new XmlDocument();
            if (File.Exists(directorioXmlColecciones + "/" + nombreUsuario + ".xml") && !actualizar)
            {
                xmlColeccion.Load(directorioXmlColecciones + "/" + nombreUsuario + ".xml");
                return xmlColeccion;
            }
            String urlConsultaXmlColeccion = "https://www.boardgamegeek.com/xmlapi2/collection?subtype=boardgame&own=1&brief=1&username=" + nombreUsuario;
            while (true)
            {
                try
                {
                    xmlColeccion = consultarXml(urlConsultaXmlColeccion);
                }
                catch (WebException ex)
                {
                    if (!ex.Status.Equals(WebExceptionStatus.ProtocolError))
                    {
                        if (File.Exists(directorioXmlColecciones + "/" + nombreUsuario + ".xml"))
                        {
                            xmlColeccion.Load(directorioXmlColecciones + "/" + nombreUsuario + ".xml");
                            return xmlColeccion;
                        }
                        throw new Exception("No se puede acceder a la coleccion del usuario de manera local ni en internet");
                    }
                }
                if (xmlColeccion.SelectSingleNode("items") != null)
                {
                    xmlColeccion.Save(directorioXmlColecciones + "/" + nombreUsuario + ".xml");
                    return xmlColeccion;
                }
                else if (xmlColeccion.SelectSingleNode("errors") != null)
                {
                    throw new Exception("El usuario de la coleccion no esta registrado en BGG");
                }
                Thread.Sleep(tiempoEsperaExcepcion);
            }
        }
        public static XmlDocument consultarXmlRepertorio(String nombreUsuario, String directorioXmlColecciones, String directorioXmlJuegos, String directorioImagenesJuegos, String directorioImagenesAutores, String directorioXmlJugadas, bool actualizarColeccion, bool actualizarJugadas, ProgressBar progresoCarga)
        {
            XmlDocument xmlColeccion = consultarXmlColeccion(nombreUsuario, directorioXmlColecciones, actualizarColeccion);
            XmlDocument xmlRepertorio = new XmlDocument();
            xmlRepertorio.LoadXml("<items></items>");
            XmlNodeList xmlJuegos = xmlColeccion.SelectNodes("items/item/@objectid");
            progresoCarga.Maximum = xmlJuegos.Count;
            foreach (XmlNode xmlNodoJuego in xmlJuegos)
            {
                try
                {
                    /* Es if es para no meter juegos con id's duplicadas al xmlRepertorio porque las colecciones pueden tener id's de juegos duplicadas */
                    if (xmlRepertorio.SelectSingleNode("items/item[@id='" + xmlNodoJuego.Value + "']") == null)
                    {
                        XmlDocument xmlJuego = consultarXmlJuego(xmlNodoJuego.Value, directorioXmlJuegos, directorioImagenesJuegos, directorioImagenesAutores);
                        XmlDocument xmlJugadas = consultarXmlJugadas(nombreUsuario, xmlNodoJuego.Value, directorioXmlJugadas, actualizarJugadas);
                        XmlNode xmlNodoJugadasImportable = xmlJuego.ImportNode(xmlJugadas.SelectSingleNode("plays"), true);
                        xmlJuego.SelectSingleNode("items/item").AppendChild(xmlNodoJugadasImportable);
                        XmlNode xmlNodoJuegoImportable = xmlRepertorio.ImportNode(xmlJuego.SelectSingleNode("items/item"), true);
                        xmlRepertorio.DocumentElement.AppendChild(xmlNodoJuegoImportable);
                        progresoCarga.Value++;
                    }
                }
                catch (Exception ex)
                {
                    progresoCarga.Value++;
                }
            }
            return xmlRepertorio;
        }
        public static XmlDocument consultarXmlJugadas(String nombreUsuario, String idJuego, String directorioXmlJugadas, Boolean actualizar)
        {
            String directorio = directorioXmlJugadas + "/" + nombreUsuario + "/" + idJuego + ".xml";
            XmlDocument xmlJugadasLocales = new XmlDocument();
            String fechaMinima = "";
            bool existenJugadasLocales = File.Exists(directorio);
            if (existenJugadasLocales)
            {
                xmlJugadasLocales.Load(directorio);
                //double horasPasadas = (DateTime.Now - DateTime.Parse(xmlJugadasLocales.DocumentElement.Attributes["lastupdate"].Value)).TotalHours;
                //Console.WriteLine(horasPasadas.ToString());
                if ((DateTime.Now - DateTime.Parse(xmlJugadasLocales.DocumentElement.Attributes["lastupdate"].Value)).TotalHours < 24d && !actualizar)
                {
                    return xmlJugadasLocales;
                }
                XmlNode nodoUltimaJugada = xmlJugadasLocales.SelectSingleNode("plays/play");
                fechaMinima = nodoUltimaJugada != null ? nodoUltimaJugada.Attributes["date"].Value : "";
            }
            XmlDocument xmlJugadasNuevas = new XmlDocument();
            String cadenaFechaActual = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            String urlConsultaXmlJugadas = "https://www.boardgamegeek.com/xmlapi2/plays?username=" + nombreUsuario + "&id=" + idJuego + "&mindate=" + fechaMinima + "&maxdate=" + cadenaFechaActual;
            while (true)
            {
                try
                {
                    xmlJugadasNuevas = consultarXml(urlConsultaXmlJugadas);
                }
                catch (WebException ex)
                {
                    if (!ex.Status.Equals(WebExceptionStatus.ProtocolError))
                    {
                        break;
                    }
                }
                if (xmlJugadasNuevas.SelectSingleNode("plays") != null)
                {
                    if (!Directory.Exists(directorioXmlJugadas + "/" + nombreUsuario))
                    {
                        Directory.CreateDirectory(directorioXmlJugadas + "/" + nombreUsuario);
                    }
                    if (existenJugadasLocales)
                    {
                        //Agregamos hasta arriba las jugadas que no tenga id's repetidas y comenzando desde abajo para al final clocar hasta arriba la ultim de las ultimas jugadas
                        XmlNodeList jugadasNuevas = xmlJugadasNuevas.SelectNodes("plays/play");
                        for (int indiceJugadaNueva = jugadasNuevas.Count - 1; indiceJugadaNueva >= 0; indiceJugadaNueva--)
                        {
                            if (xmlJugadasLocales.SelectSingleNode("plays/play[@id=" + jugadasNuevas[indiceJugadaNueva].Attributes["id"].Value + "]") == null)
                            {
                                xmlJugadasLocales.SelectSingleNode("plays").PrependChild(xmlJugadasLocales.ImportNode(jugadasNuevas[indiceJugadaNueva], true));
                            }
                        }
                        xmlJugadasLocales.DocumentElement.Attributes["lastupdate"].Value = cadenaFechaActual;
                        xmlJugadasLocales.DocumentElement.Attributes["total"].Value = xmlJugadasLocales.SelectNodes("plays/play").Count.ToString();
                        xmlJugadasLocales.Save(directorio);
                        return xmlJugadasLocales;
                    }
                    xmlJugadasNuevas.DocumentElement.SetAttribute("lastupdate", cadenaFechaActual);
                    xmlJugadasNuevas.Save(directorio);
                    return xmlJugadasNuevas;
                }
                Thread.Sleep(tiempoEsperaExcepcion);
            }
            if (existenJugadasLocales)
            {
                return xmlJugadasLocales;
            }
            throw new Exception("No se puede acceder a las jugadas del usuario de manera local ni en internet");
        }
    }
}
