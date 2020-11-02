using ECCUSBET.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECCUSBET.Model
{
    class BET_Entities
    { //------------------------------  Variáveis de escopo global  ----------------------------//


        private readonly string Padrao;
        private readonly int Npessoas, Intervalo;
        private readonly double Temperatura;
        private double ContrDiaruiaTotal, Pd;
        private readonly int CLodoFresco = 1;
        private readonly int MetroQuadradoporHab = 2;
        private int Sp, Ta;

        //-----  Construtor padrão (Irrelevando ao código, já que a instanciação é feita por métodos e parâmetros)  ----------------------------//
        public BET_Entities()
        {
        }

        public BET_Entities(string padrao, int npessoas, int intervalo, double temperatura)
        {
            Padrao = padrao;
            Npessoas = npessoas;
            Intervalo = intervalo;
            Temperatura = temperatura;
        }

        public double VolUtio { get; private set; }
        public double AreadaBet { get; private set; }
        public double VolTotal { get; private set; }
        public double ProfundidadeM { get; private set; }


        //------------------  Métodos que recebem os parametros a partir do View  -----------------//
        private int SelecaoPadrao()
        {
            if (Padrao == "Residência de baixo padrão")
            {
                Sp = 100;
            }
            else if (Padrao == "Residência de médio padrão")
            {
                Sp = 130;
            }
            else if (Padrao == "Residência de alto padrão")
            {
                Sp = 160;
            }
            else if (Padrao == "Hotel (exceto lavanderia e cozinha)")
            {
                Sp = 100;
            }
            else if (Padrao == "Alojamento provisório")
            {
                Sp = 80;
            }
            else if (Padrao == "Edfícios públicos ou comerciais")
            {
                Sp = 50;
            }
            else if (Padrao == "Escolas")
            {
                Sp = 50;
            }
            else if (Padrao == "Bares")
            {
                Sp = 6;
            }
            else if (Padrao == "Restaurantes e similares")
            {
                Sp = 25;
            }
            return Sp;
        }

        private double PeriododeDetencao()
        {

            ContrDiaruiaTotal = (Sp * Npessoas);
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

        private double ProfundidadeMedia()
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
           
            VolUtio = (1000 + Npessoas * ((SelecaoPadrao() * PeriododeDetencao()) + (TaxadeAcumulacao() * CLodoFresco))) / 1000;
            AreadaBet = (Npessoas * MetroQuadradoporHab);
            VolTotal = AreadaBet * ProfundidadeMedia();
            return VolUtio + AreadaBet + VolTotal;
        }

    }

}








