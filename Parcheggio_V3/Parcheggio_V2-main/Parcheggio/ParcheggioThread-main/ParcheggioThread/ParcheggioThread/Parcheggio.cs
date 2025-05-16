using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ParcheggioThread
{
    class Parcheggio
    {
        public Form1 Form { get; }
        public int PostiDisponibili { get; }
        public List<Ingresso> Ingressi { get; } = new();
        public List<Uscita> Uscite { get; } = new();
        public List<Semaphore> SemIngressi { get; } = new();
        public List<Semaphore> SemUscite { get; } = new();

        private List<ListBox> ingressiBox = new();
        private List<ListBox> usciteBox = new();
        private Random rnd = new();

        public Parcheggio(Form1 form, int posti)
        {
            Form = form;
            PostiDisponibili = posti;

            int numIngressi = rnd.Next(1, 6);
            int numUscite = rnd.Next(1, 6);

            for (int i = 0; i < numIngressi; i++)
            {
                Ingresso ingresso = new Ingresso();
                Ingressi.Add(ingresso);
                SemIngressi.Add(new Semaphore(1, 1));
                ListBox box = new ListBox { Width = 150, Height = 150, Name = $"listaIngresso{i}" };
                ingressiBox.Add(box);
                form.PanelIngressi.Controls.Add(box);
            }

            for (int i = 0; i < numUscite; i++)
            {
                Uscita uscita = new Uscita();
                Uscite.Add(uscita);
                SemUscite.Add(new Semaphore(1, 1));
                ListBox box = new ListBox { Width = 150, Height = 150, Name = $"listaUscita{i}" };
                usciteBox.Add(box);
                form.PanelUscite.Controls.Add(box);
            }

            form.Shown += FormShown;
        }

        private void FormShown(object sender, EventArgs e)
        {
            new Thread(CreazioneAuto).Start();
        }

        private void CreazioneAuto()
        {
            for (int i = 0; i < 10; i++)
            {
                new Automobile(this);
                Thread.Sleep(1000);
            }
        }

        public ListBox GetIngressoBox(int index)
        {
            return ingressiBox[index];
        }

        public ListBox GetUscitaBox(int index)
        {
            return usciteBox[index];
        }

        public void AggiornaListBox(ListBox box, string testo, bool aggiungi)
        {
            box.Invoke(new MethodInvoker(() =>
            {
                if (aggiungi)
                    box.Items.Add(testo);
                else
                    box.Items.Remove(testo);
            }));
        }

        public void SimulaSosta(string descrizione, int secondi)
        {
            for (int i = 0; i < secondi; i++)
            {
                Form.BoxParcheggio.Invoke(() => Form.BoxParcheggio.Items.Add(descrizione));
                Thread.Sleep(1000);
                Form.BoxParcheggio.Invoke(() => Form.BoxParcheggio.Items.Remove(descrizione));
            }
        }
    }
}
