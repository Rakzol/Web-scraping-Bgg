using System;
using System.Collections.Generic;

namespace BGG_Finder
{
    class Enlace
    {
        public String id;
        public String valor;
        public Dictionary<String, Juego> juegos = new Dictionary<string, Juego>();
        public Enlace(String id, String valor)
        {
            this.id = id;
            this.valor = valor;
        }
    }
}
