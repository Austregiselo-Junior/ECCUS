using ECCUSBET.View;
using ECCUSBET.Model.Enums;
using System;
using System.Text.RegularExpressions;

namespace ECCUSBET.Model.Entities
{
    class Dimensi_Entities
    {
        //------------------------------  Variáveis de escopo global  ----------------------------//
        private readonly int Npessoas, Intervalo;
        private readonly double Temperatura;
        private double ContrDiaruiaTotal, Pd, AlturaPneu, RaioPeneu, LarguraPneu;
        private readonly int CLodoFresco = 1;
        private int Ta;



        //------------------------  Propriedades usadas no dimensionamento  ------------------------//
        public double VolUtio { get; set; }
        public double ProfundidadeM { get; set; }
        public Ocupacao_Enums SelecaoPadrao { get; set; }
        public double VolPneu { get; set; }
        public double Perfil { get; set; }
        public int N_Roda { get; set; }
        public double LarguradaBet { get; set; }


        //--------------------------------  Construtores   ----------------------------//
        public Dimensi_Entities()
        {
        }

        public Dimensi_Entities(Ocupacao_Enums selecaoPadrao, int npessoas, int intervalo, double temperatura)
        {
            SelecaoPadrao = selecaoPadrao;
            Npessoas = npessoas;
            Intervalo = intervalo;
            Temperatura = temperatura;
        }

        public Dimensi_Entities(double larguraPneu, double perfil, int n_Roda)
        {
            LarguraPneu = larguraPneu;
            Perfil = perfil;
            N_Roda = n_Roda;
        }



        //------------------  Métodos persolanizados  -----------------//
        private double PeriododeDetencao()
        {
            ContrDiaruiaTotal = ((double)SelecaoPadrao * Npessoas);
            if (ContrDiaruiaTotal <= 1500)
            {
                Pd = 1;
            }
            else if (ContrDiaruiaTotal <= 1500)
            {
                Pd = 1;
            }
            else if (ContrDiaruiaTotal >= 1501 && ContrDiaruiaTotal <= 3000)
            {
                Pd = 0.92;
            }
            else if (ContrDiaruiaTotal >= 3001 && ContrDiaruiaTotal <= 4500)
            {
                Pd = 0.83;
            }
            else if (ContrDiaruiaTotal >= 4501 && ContrDiaruiaTotal <= 6000)
            {
                Pd = 0.75;
            }
            else if (ContrDiaruiaTotal >= 6001 && ContrDiaruiaTotal <= 7500)
            {
                Pd = 0.67;
            }
            else if (ContrDiaruiaTotal >= 7501 && ContrDiaruiaTotal <= 9000)
            {
                Pd = 0.58;
            }
            else
                Pd = 0.5;
            return Pd;
        }

        private int TaxadeAcumulacao()
        {
            SizingView sizingView = new SizingView();
            if (sizingView.txtIntervalodeLimpeza.Text == null)
            {
                Ta = 0;
            }


            if (Intervalo == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Temperatura <= 10)
                    {
                        Ta = 94;
                    }
                    else if (Temperatura > 10 && Temperatura <= 20)
                    {
                        Ta = 65;
                    }
                    else
                    {
                        Ta = 57;
                    }
                }
            }
            else if (Intervalo == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Temperatura <= 10)
                    {
                        Ta = 134;
                    }
                    else if (Temperatura > 10 && Temperatura <= 20)
                    {
                        Ta = 105;
                    }
                    else
                    {
                        Ta = 97;
                    }
                }
            }
            else if (Intervalo == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Temperatura <= 10)
                    {
                        Ta = 174;
                    }
                    else if (Temperatura > 10 && Temperatura <= 20)
                    {
                        Ta = 145;
                    }
                    else
                    {
                        Ta = 137;
                    }
                }
            }
            else if (Intervalo == 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Temperatura <= 10)
                    {
                        Ta = 214;
                    }
                    else if (Temperatura > 10 && Temperatura <= 20)
                    {
                        Ta = 185;
                    }
                    else
                    {
                        Ta = 177;
                    }
                }
            }
            else if (Intervalo == 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Temperatura <= 10)
                    {
                        Ta = 254;
                    }
                    else if (Temperatura > 10 && Temperatura <= 20)
                    {
                        Ta = 255;
                    }
                    else
                    {
                        Ta = 217;
                    }
                }
            }
            return Ta;
        }

        public double ProfundidadeMedia()
        {
            SizingView sizingView = new SizingView();
            double Pmini, Pmax;

            if (VolUtio == 0)
            {
                sizingView.TxtProfundidadeMedia.Text = "0";
            }

            if (VolUtio <= 6)
            {
                Pmini = 1.2;
                Pmax = 2.2;
                ProfundidadeM = ((Pmini + Pmax) / 2);
            }
            else if (VolUtio > 6 && VolUtio <= 10)
            {
                Pmini = 1.5;
                Pmax = 2.5;
                ProfundidadeM = ((Pmini + Pmax) / 2);
            }
            else if (VolUtio > 10)
            {
                Pmini = 1.8;
                Pmax = 2.8;
                ProfundidadeM = ((Pmini + Pmax) / 2);
            }
            return ProfundidadeM;
        }

        public double Dimensionamento()
        {
            VolUtio = (1000 + Npessoas * (((double)SelecaoPadrao * PeriododeDetencao()) + (TaxadeAcumulacao() * CLodoFresco))) / 1000;
            return VolUtio;
        }

        public double Dimensi_Pneu()
        {
            AlturaPneu = (Perfil / 100) * LarguraPneu;
            RaioPeneu = AlturaPneu / 2;

            VolPneu = Math.PI * Math.Pow(RaioPeneu, 2) * LarguraPneu;
           // VolPneu /= 1000000000;
            return VolPneu;
        }

        public double Largura_Bet()
        {
            LarguraPneu /= 1000;
            LarguradaBet = LarguraPneu + 1.2;
            return LarguradaBet;
        }

    }
}








