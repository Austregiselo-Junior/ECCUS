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
Austregíselo Junior     29/10/2020 Dimensionamento quase terminado, falta a correção de bugs;
Austregíselo Junior     30/10/2020 Dimensionamento básico OK; 
Austregíselo Junior     31/10/2020 Aplicando orientação a objeto, faltou verificar a saída de dados;
Austregíselo Junior     02/11/2020 Saída de dados OK;

-------------------------------------------------------------------------------------------------------------------------
Histórico de Bugs        
Autor                   Data       Descrição  
Austregíselo Junior     29/10/2020 Quando o textbox não tem nada ele estoura uma exerção. -> A correção foi por todo o cálculo num bloco try e gerar 
                                   uma análise de erro pra tudo, já que eram os mesmos erros.

----------------------  ---------- --------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------------
*/

using ECCUSBET.Model;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace ECCUSBET.View
{
    public partial class SizingView : Form
    {
        public SizingView()
        {
            InitializeComponent();
        }

        public void BtnCalcular_Click(object sender, EventArgs e)
        {

            if (((string)BoxSelecaoPadrao.SelectedItem == null))
            {
                MessageBox.Show("Erro, escolha o padrão de ocupação!", MessageBoxButtons.OK.ToString());
            }
            else
            {
                try
                {
                    // Entrada de dados + instanciação com construtor
                    string padrao = BoxSelecaoPadrao.Text;
                    int npessoas = int.Parse(txtNPessoas.Text);
                    int intervalo = int.Parse(txtIntervalodeLimpeza.Text, CultureInfo.InvariantCulture);
                    double temperatura = double.Parse(txtTemperatura.Text, CultureInfo.InvariantCulture);
                    BET_Entities bet_entities = new BET_Entities(padrao, npessoas, intervalo, temperatura);
                   
                    // Chamada do métododo 
                    bet_entities.Dimensionamento();

                    // Saída de dados
                    TxtVolUtio.Text = bet_entities.VolUtio.ToString("F2", CultureInfo.InvariantCulture);
                    TxtProfundidadeMedia.Text = bet_entities.ProfundidadeM.ToString("F2", CultureInfo.InvariantCulture);
                    TxtAreadaBet.Text = bet_entities.AreadaBet.ToString("F2", CultureInfo.InvariantCulture);
                    TxtVolTotal.Text = bet_entities.VolTotal.ToString("F2", CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Erro, adicione o ítem!", MessageBoxButtons.OK.ToString());
                }

            }

        }

        // --------------------------- Mecânica do layout --------------------------------//
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
