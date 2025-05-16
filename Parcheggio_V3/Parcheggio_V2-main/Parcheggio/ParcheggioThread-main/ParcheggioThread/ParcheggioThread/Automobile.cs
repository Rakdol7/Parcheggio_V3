using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ParcheggioThread
{
    class Automobile
    {
        private static Random random = new();
        private Parcheggio parcheggio;
        private int tempoSosta;
        private string descrizione;
        private static int i= 0;

        public Automobile(Parcheggio parcheggio)
        {
            this.parcheggio = parcheggio;
            tempoSosta = random.Next(5, 10);
            descrizione = "auto"+GeneraNum()+$"({tempoSosta}s)";
            new Thread(Esegui).Start();
        }

        private void Esegui()
        {
            int indexIngresso = random.Next(parcheggio.Ingressi.Count);
            Ingresso ingresso = parcheggio.Ingressi[indexIngresso];
            Semaphore semIngresso = parcheggio.SemIngressi[indexIngresso];
            ListBox boxIngresso = parcheggio.GetIngressoBox(indexIngresso);

            ingresso.Coda.Add(this);
            parcheggio.AggiornaListBox(boxIngresso, descrizione, true);
            semIngresso.WaitOne();
            Thread.Sleep(ingresso.TempoIngresso * 1000);
            semIngresso.Release();
            parcheggio.AggiornaListBox(boxIngresso, descrizione, false);

            parcheggio.SimulaSosta(descrizione, tempoSosta);

            int indexUscita = random.Next(parcheggio.Uscite.Count);
            Uscita uscita = parcheggio.Uscite[indexUscita];
            Semaphore semUscita = parcheggio.SemUscite[indexUscita];
            ListBox boxUscita = parcheggio.GetUscitaBox(indexUscita);

            uscita.Coda.Add(this);
            parcheggio.AggiornaListBox(boxUscita, descrizione, true);
            semUscita.WaitOne();
            Thread.Sleep(uscita.TempoUscita * 1000);
            semUscita.Release();
            parcheggio.AggiornaListBox(boxUscita, descrizione, false);
        }

        private static string GeneraNum()
        {
            i++;
            return i.ToString();
        }
    }
}
