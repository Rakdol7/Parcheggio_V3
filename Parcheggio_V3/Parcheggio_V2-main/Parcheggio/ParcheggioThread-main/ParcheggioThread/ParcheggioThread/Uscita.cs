using System.Collections.Generic;

namespace ParcheggioThread
{
    class Uscita
    {
        public int TempoUscita { get; }
        public List<Automobile> Coda { get; }

        public Uscita()
        {
            TempoUscita = new System.Random().Next(1, 4);
            Coda = new List<Automobile>();
        }
    }
}
