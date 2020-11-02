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
        public double VolUtio, AreadaBet, VolTotal;
        double ContrDiaruiaTotal, Pd;
        readonly int CLodoFresco = 1;
        readonly int MetroQuadradoporHab = 2;
        int Sp, Ta;

        public int SelecaoPadrao(string padrao)
        {
            if (padrao == "Residência de baixo padrão")
            {
                Sp = 100;
            }
            else if (padrao == "Residência de médio padrão")
            {
                Sp = 130;
            }
            else if (padrao == "Residência de alto padrão")
            {
                Sp = 160;
            }
            else if (padrao == "Hotel (exceto lavanderia e cozinha)")
            {
                Sp = 100;
            }
            else if (padrao == "Alojamento provisório")
            {
                Sp = 80;
            }
            else if (padrao == "Edfícios públicos ou comerciais")
            {
                Sp = 50;
            }
            else if (padrao == "Escolas")
            {
                Sp = 50;
            }
            else if (padrao == "Bares")
            {
                Sp = 6;
            }
            else if (padrao == "Restaurantes e similares")
            {
                Sp = 25;
            }
            return Sp;
        }


        public double PeriododeDetencao(int pessoas)
        {

            ContrDiaruiaTotal = (Sp * pessoas);
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

        public int TaxadeAcumulacao(int intervalo, double temperatura) //Por padrão é passavem por valor 
        {
            SizingView sizingView = new SizingView();
            if (sizingView.txtIntervalodeLimpeza.Text == null)
            {
                Ta = 0;
            }


            if (intervalo == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Ta = 94;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Ta = 65;
                    }
                    else
                    {
                        Ta = 57;
                    }
                }
            }
            else if (intervalo == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Ta = 134;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Ta = 105;
                    }
                    else
                    {
                        Ta = 97;
                    }
                }
            }
            else if (intervalo == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Ta = 174;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Ta = 145;
                    }
                    else
                    {
                        Ta = 137;
                    }
                }
            }
            else if (intervalo == 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Ta = 214;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Ta = 185;
                    }
                    else
                    {
                        Ta = 177;
                    }
                }
            }
            else if (intervalo == 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Ta = 254;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
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
            double Pmini, Pmax, Pmedio;
            Pmedio = 0;
            if (VolUtio == 0)
            {
                sizingView.TxtProfundidadeMedia.Text = "0";
            }

            if (VolUtio <= 6)
            {
                Pmini = 1.2;
                Pmax = 2.2;
                Pmedio = ((Pmini + Pmax) / 2);
            }
            else if (VolUtio > 6 && VolUtio <= 10)
            {
                Pmini = 1.5;
                Pmax = 2.5;
                Pmedio = ((Pmini + Pmax) / 2);
            }
            else if (VolUtio > 10)
            {
                Pmini = 1.8;
                Pmax = 2.8;
                Pmedio = ((Pmini + Pmax) / 2);
            }
            return Pmedio;
        }

        public double Dimensionamento(int npessoas)
        {

            VolUtio = (1000 + npessoas * ((Sp * Pd) + (Ta * CLodoFresco))) / 1000;
            AreadaBet = (npessoas * MetroQuadradoporHab);
            VolTotal = AreadaBet * ProfundidadeMedia();
            return VolUtio + AreadaBet + VolTotal;
        }

    }

}








