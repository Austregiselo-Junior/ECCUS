using ECCUSBET.View;
using System;
using System.Globalization;

namespace ECCUSBET.Model.Entities
{

    class Pneu_Entities
    {
        private double RaioPeneu;
        public double DiametroPneu { get; set; }
        public double LarguraPneu { get; set; }
        public double Perfil { get; set; }
        public double N_Roda { get; set; }
        public double VolPneu { get; set; }
        public int QTEPneus { get; set; }


        public Pneu_Entities()
        {
        }

        public Pneu_Entities(double larguraPneu, double perfil, double n_Roda)
        {
            LarguraPneu = larguraPneu * 0.001; // fator de conversão;
            Perfil = (perfil / 100) * LarguraPneu; // Percentagem da larguraPneu;
            N_Roda = n_Roda * 0.0254; // fator de conversão;
        }

        public double Dimensi_Pneu()
        {
            DiametroPneu = (2 * Perfil + N_Roda);
            RaioPeneu = DiametroPneu / 2;
            VolPneu = Math.PI * Math.Pow(RaioPeneu, 2) * LarguraPneu;
            return VolPneu;
        }

        public int QTE_Pneu(double volutio)
        {
            QTEPneus = (int)(volutio / VolPneu);
            return QTEPneus;
        }
             
        public void SaidadeDados(SizingView sizingView)
        {
            sizingView.TxtVolPneu.Text = VolPneu.ToString("F2", CultureInfo.InvariantCulture);
            sizingView.TxtQtePeneus.Text = QTEPneus.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
