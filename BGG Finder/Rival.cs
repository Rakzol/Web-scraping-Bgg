using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGG_Finder
{
    class Rival
    {
        public String nombre;
        public int vecesJugadas;
        public int victorias;
        public int derrotas;
        public int empates;
        public bool esIndividual;
        public Dictionary<String, Jugada> jugadas = new Dictionary<String, Jugada>();
        public Rival( bool esIndividual, String nombre, Jugada jugada)
        {
            this.nombre = nombre;
            this.esIndividual = esIndividual;
            agregarJugada(jugada);
        }
        public Rival(bool esIndividual, String nombre)
        {
            this.nombre = nombre;
            this.esIndividual = esIndividual;
        }
        public void agregarJugada(Jugada jugada)
        {
            jugadas.Add(jugada.juego.id, jugada);
            vecesJugadas += jugada.jugadas;
            victorias += jugada.victorias;
            derrotas += jugada.derrotas;
            empates += vecesJugadas - (victorias + derrotas);
        }
    }
}
