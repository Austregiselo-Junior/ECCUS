/*
----------------------------------------------------- CABEÇALHO -------------------------------------------------------------        
Nome do programa		   : ECCUSBet;
Versão                     : 2.0;
Objetivo                   : Software para dimensionar bacia de evapotranspiração;
Empresa Responsável		   : ONG Instituto ECCUS;
Criado em                  : 15/10/2020.
-------------------------------------------------------------------------------------------------------------------------   
Histórico de atividades        
Autor                   Data       Descrição        
----------------------  ---------- --------------------------------------------------------------------------------------        
Austregíselo Junior     15/10/2020 Criação do APP e Início do layout;
Austregíselo Junior     19/10/2020 desenvolvendo Layout;
Austregíselo Junior     20/10/2020 Layout básico OK;
Austregíselo Junior     29/10/2020 Dimensionamento quase terminado, falta a correção de bigs;

-------------------------------------------------------------------------------------------------------------------------
Histórico de Bugs        
Autor                   Data       Descrição  
Austregíselo Junior     29/10/2020 Quando o textbox não tem nada ele estoura uma exerção;
----------------------  ---------- --------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------------
*/

using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ECCUSBET.View
{
    public partial class SizingView : Form
    {
        public SizingView()
        {
            InitializeComponent();
        }

        //------------------------------  Variáveis de escopo global  ----------------------------//
        double VolUtio, ContrDiaruiaTotal, Rp, AreadaBet;
        int CLodoFresco = 1;
        int MetroQuadradoporHab = 2;
        int Rc, Rt, NPessoas;


        //----------------------- Funçôes com adição de programação defenciva  -------------------//
        private int ContribuicaoDiaria()
        {
            if ((string)BoxSelecaoPadrao.SelectedItem == "Residência de baixo padrão")  //Converção explicita (casting)
            {
                Rc = 100;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Residência de médio padrão")
            {
                Rc = 130;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Residência de alto padrão")
            {
                Rc = 160;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Hotel (exceto lavanderia e cozinha)")
            {
                Rc = 100;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Alojamento provisório")
            {
                Rc = 80;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Edfícios públicos ou comerciais")
            {
                Rc = 50;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Escolas")
            {
                Rc = 50;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Bares")
            {
                Rc = 6;
            }
            else if ((string)BoxSelecaoPadrao.SelectedItem == "Restaurantes e similares")
            {
                Rc = 25;
            }
            else
                MessageBox.Show("Erro, o padrão de ocupação!", MessageBoxButtons.OK.ToString());
            return Rc;
        }


        private double PeriododeDetencao()
        {
            if (txtNPessoas.Text == "0" || txtNPessoas.Text == "")
            {
                Rp = 0;
                            }

            ContrDiaruiaTotal = (Rc * NPessoas);
            if (ContrDiaruiaTotal <= 1500)
            {
                Rp = 1;
            }
            else if (ContrDiaruiaTotal <= 1500)
            {
                Rp = 1;
            }
            else if (ContrDiaruiaTotal >= 1501 && ContrDiaruiaTotal <= 3000)
            {
                Rp = 0.92;
            }
            else if (ContrDiaruiaTotal >= 3001 && ContrDiaruiaTotal <= 4500)
            {
                Rp = 0.83;
            }
            else if (ContrDiaruiaTotal >= 4501 && ContrDiaruiaTotal <= 6000)
            {
                Rp = 0.75;
            }
            else if (ContrDiaruiaTotal >= 6001 && ContrDiaruiaTotal <= 7500)
            {
                Rp = 0.67;
            }
            else if (ContrDiaruiaTotal >= 7501 && ContrDiaruiaTotal <= 9000)
            {
                Rp = 0.58;
            }
            else
                Rp = 0.5;
            return Rp;
        }

        public int TaxadeAcumulacao(int intervalo, int temperatura) //Por padrão é passavem por valor 
        {
            if (txtIntervalodeLimpeza.Text == "0" || txtTemperatura.Text == "")
            {
                Rt = 0;
            }


            if (intervalo == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Rt = 94;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Rt = 65;
                    }
                    else
                    {
                        Rt = 57;
                    }
                }
            }
            else if (intervalo == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Rt = 134;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Rt = 105;
                    }
                    else
                    {
                        Rt = 97;
                    }
                }
            }
            else if (intervalo == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Rt = 174;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Rt = 145;
                    }
                    else
                    {
                        Rt = 137;
                    }
                }
            }
            else if (intervalo == 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Rt = 214;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Rt = 185;
                    }
                    else
                    {
                        Rt = 177;
                    }
                }
            }
            else if (intervalo == 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (temperatura <= 10)
                    {
                        Rt = 254;
                    }
                    else if (temperatura > 10 && temperatura <= 20)
                    {
                        Rt = 255;
                    }
                    else
                    {
                        Rt = 217;
                    }
                }
            }
            return Rt;
        }

        private double ProfundidadeUtil()
        {
            double Pmini, Pmax, Pmedio;
            Pmedio = 0;
            if (VolUtio == 0)
            {
                TxtProfundidadeMedia.Text = "0";
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

        private void BtnCalcular_Click(object sender, EventArgs e)
        {

            try
            {
                double PD;
                int CD, TA;

                CD = ContribuicaoDiaria();
                TA = TaxadeAcumulacao(intervalo: int.Parse(txtIntervalodeLimpeza.Text),
                                      temperatura: int.Parse(txtTemperatura.Text));
                NPessoas = int.Parse(txtNPessoas.Text);
                PD = PeriododeDetencao();

                VolUtio = (1000 + NPessoas * ((CD * PD) + (TA * CLodoFresco))) / 1000;
                TxtVolUtio.Text = VolUtio.ToString("F2");

                if (CD == 0 || PD == 0 || TA == 0) // programação defenciva
                {
                    TxtVolUtio.Text = "0";
                }

                TxtProfundidadeMedia.Text = ProfundidadeUtil().ToString("F2");
                AreadaBet = (NPessoas * MetroQuadradoporHab);
                TxtAreadaBet.Text = AreadaBet.ToString("F2");
                TxtVolTotal.Text = (AreadaBet * ProfundidadeUtil()).ToString("F2");
            }
            catch (FormatException)
            {
                MessageBox.Show("Erro, adicione o ítem!", MessageBoxButtons.OK.ToString());
            }

        }

        private void ManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManualView manualView = new ManualView();
            manualView.Show();
        }

        private void DesenvolvimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevView devView = new DevView();
            devView.Show();
        }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            StartView startView = new StartView();
            startView.Close();
        }

    }
}
