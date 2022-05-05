using System;
using System.Xml;

namespace BGG_Finder
{
    class Usuario
    {
        public String id;
        public String nombreUsuario;
        public String nombre;
        public String apellido;
        public String estadoProvincia;
        public String país;
        public String inscritoDesde;
        public String últimaConexión;

        public Usuario(XmlDocument xmlUsuario)
        {
            id = xmlUsuario.SelectSingleNode("user/@id").Value;
            nombreUsuario = xmlUsuario.SelectSingleNode("user/@name").Value;
            nombre = xmlUsuario.SelectSingleNode("user/firstname/@value").Value;
            apellido = xmlUsuario.SelectSingleNode("user/lastname/@value").Value;
            estadoProvincia = xmlUsuario.SelectSingleNode("user/stateorprovince/@value").Value;
            país = xmlUsuario.SelectSingleNode("user/country/@value").Value;
            inscritoDesde = xmlUsuario.SelectSingleNode("user/yearregistered/@value").Value;
            últimaConexión = xmlUsuario.SelectSingleNode("user/lastlogin/@value").Value;
        }
    }
}
