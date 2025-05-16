using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ParcheggioThread
{
    public partial class Form1 : Form
    {
        public FlowLayoutPanel PanelIngressi { get; private set; }
        public FlowLayoutPanel PanelUscite { get; private set; }
        public ListBox BoxParcheggio { get; private set; }

        public Form1()
        {
            InitializeComponent();
            InizializzaUI();
            Parcheggio parcheggio = new Parcheggio(this, new Random().Next(10, 15));
            CheckForIllegalCrossThreadCalls = false;
        }

        private void InizializzaUI()
        {
            Width = 1500;
            Height = 1000;
            Text = "Parcheggio";

            PanelIngressi = new FlowLayoutPanel { Dock = DockStyle.Left, Width = 250, AutoScroll = true };
            Controls.Add(PanelIngressi);

            BoxParcheggio = new ListBox { Location = new Point(350, 0), Size = new Size(300, 300) };
            Controls.Add(BoxParcheggio);

            PanelUscite = new FlowLayoutPanel { Location = new Point(820, 0), Size = new Size(250, Height), AutoScroll = true };
            Controls.Add(PanelUscite);
        }
    }
}
