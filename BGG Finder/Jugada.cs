using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGG_Finder
{
    class Jugada
    {
        public Juego juego;
        public int jugadas;
        public int victorias;
        public int derrotas;
        public int empates;
        public Jugada( Juego juego, int jugadas, int victorias, int derrotas/*, int empates */)
        {
            this.juego = juego;
            this.jugadas = jugadas;
            this.victorias = victorias;
            this.derrotas = derrotas;
            empates = jugadas - ( victorias + derrotas );
        }
    }
}
