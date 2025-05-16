using System.Collections.Generic;

namespace ParcheggioThread
{
    class Ingresso
    {
        public int TempoIngresso { get; }
        public List<Automobile> Coda { get; }

        public Ingresso()
        {
            TempoIngresso = new System.Random().Next(1, 4);
            Coda = new List<Automobile>();
        }
    }
}

