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
Austregíselo Junior     31/10/2020 Adicionando construtores e propriedades, faltou verificar a saída de dados;
Austregíselo Junior     02/11/2020 Saída de dados OK e adição de encapsulamento e enumeração;
Austregíselo Junior     03/11/2020 Adicionando composição através das classes de entidades;
Austregíselo Junior     04/11/2020 Desistir de add um DB, vou add os resultados direto no grid;
Austregíselo Junior     05/11/2020 Tentando add os resultados direto no grid, mas ta dando bug;
Austregíselo Junior     09/11/2020 Fazendo limpeza dos dados, salvamento, exclusão e carregamento de arquivo ok;
Austregíselo Junior     10/11/2020 Alterando os cálculos segundo a consultoria de Icaro do ECCUS (esperar os dados do eccusoftware);
Austregíselo Junior     10/11/2020 Criando e implementando os serviços de edição de arquivo; 
Austregíselo Junior     11/11/2020 Não consegui realizar a atividade acima como queria (inplementar o serviçonuma classe específica);
Austregíselo Junior     11/11/2020 Implementando o cálculo de custo total, segundpo a tabela de custo;


-------------------------------------------------------------------------------------------------------------------------
Histórico de Bugs        
Autor                   Data       Descrição  
Austregíselo Junior     29/10/2020 Quando o textbox não tem nada ele estoura uma exerção. -> A correção foi por todo o cálculo num bloco try e gerar 
                                   uma análise de erro pra tudo, já que eram os mesmos erros.
Austregíselo Junior     06/11/2020 Ao passar os valores ao grid o grid não mostra nada. -> consegui contornar o problema implementando o grid 
                                   diretamente no formulário em que ele está.
Austregíselo Junior     10/11/2020 Salvar e carregar não então funcionando, ao salvar o arquuivo fica vazio o arquivo não está sendo carregado ->
                                   o jeiro foi adicionar os serviço no form em que está o grid, pois mesmo fazendo de um form para outra deu errado

----------------------  ---------- --------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------------
*/

using ECCUSBET.Model.Entities;
using ECCUSBET.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization;

namespace ECCUSBET.View
{
    public partial class SizingView : Form
    {
        public SizingView()
        {
            InitializeComponent();
        }


        //---------------------------------- Dimensionamento -----------------------------------------//
        public void BtnCalcular_Click(object sender, EventArgs e)
        {

            if (((string)BoxSelecaoPadrao.SelectedItem == null))
            {
                MessageBox.Show("Escolha o padrão de ocupação!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Entrada de dados + instanciação com construtor
                    Enum.TryParse(BoxSelecaoPadrao.Text, out Ocupacao_Enums ocupacao);
                    int npessoas = int.Parse(txtNPessoas.Text);
                    int intervalo = int.Parse(txtIntervalodeLimpeza.Text, CultureInfo.InvariantCulture);
                    double temperatura = double.Parse(txtTemperatura.Text, CultureInfo.InvariantCulture);
                    double larguraPneu = double.Parse(TxtLarguraPneu.Text, CultureInfo.InvariantCulture);
                    double perfil = double.Parse(TxtPerfil.Text, CultureInfo.InvariantCulture);
                    int aro = int.Parse(TxtAro.Text, CultureInfo.InvariantCulture);


                    Dimensi_Entities bet_entities = new Dimensi_Entities(ocupacao, npessoas, intervalo, temperatura);
                    new Dimensi_Entities(larguraPneu, perfil, aro);

                    // Chamada do métododo 
                    bet_entities.Dimensionamento();
                    bet_entities.ProfundidadeMedia();
                    bet_entities.Dimensi_Pneu();
                    bet_entities.Largura_Bet();

                    // Saída de dados
                    TxtVolUtio.Text = bet_entities.VolUtio.ToString("F2", CultureInfo.InvariantCulture);
                    TxtProfundidadeMedia.Text = bet_entities.ProfundidadeM.ToString("F2", CultureInfo.InvariantCulture);
                    TxtVolPneu.Text = bet_entities.VolPneu.ToString("P2", CultureInfo.InvariantCulture);
                    TxtLarguradaBet.Text = bet_entities.LarguradaBet.ToString("F2", CultureInfo.InvariantCulture);

                    // Add os resultados ao grid
                    GridDimens.Rows.Add(TxtVolUtio.Text, TxtProfundidadeMedia.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Adicione o ítem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //--------------------------------- Mecânica das tabelas ---------------------------------------//

        private void BtnExcluirLinha_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir a linha selecionada?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (GridDimens.CurrentRow == null)
                {
                    MessageBox.Show("Selecione uma linha da tabela!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    GridDimens.Rows.RemoveAt(GridDimens.CurrentRow.Index);
                }
            }
        }

        private void BtnAddGridOrcamento_Click(object sender, EventArgs e)
        {
            double precoUnidade, unidade, precoTotal;
            precoUnidade = double.Parse(TxtPreco_Uni.Text);
            unidade = double.Parse(TxtUnidade.Text);

            precoTotal = unidade * precoUnidade;
            TxtPreco_total.Text = precoTotal.ToString("F2", CultureInfo.InvariantCulture);

            GrigOrcamento.Rows.Add(TxtServico_Equi.Text, TxtUnidade.Text, TxtPreco_Uni.Text, TxtPreco_total.Text);
        }


        private void BtnCustoTotal_Click_1(object sender, EventArgs e)
        {
            double quantia = 0;
            double custo = 0;
            int nlinhas = GrigOrcamento.Rows.Count;
            for (int i = 0; i <= nlinhas; i++)
            {
                try
                {
                    quantia = (double)GrigOrcamento.Rows[i].Cells["TabPrecoTotal"].FormattedValue;
                    custo += quantia;
                }
                catch (Exception)
                {
                    custo += quantia;

                }


            }
            TxtCustoTotal.Text = custo.ToString("F2", CultureInfo.InvariantCulture);
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
            Application.Exit();
        }

        private void LimparToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoxSelecaoPadrao.SelectedItem = null;
            txtTemperatura.Clear();
            txtIntervalodeLimpeza.Clear();
            txtNPessoas.ClearUndo();
            TxtVolUtio.Clear();
            TxtProfundidadeMedia.Clear();
            GridDimens.Rows.Clear();
        }


        // -------------------------- Serviços de edição de arquivos apartir do grid -------------------//
        // Só está aqui porque não consigo editar o grid e nem salvar arquivo com dados do grid a partir de uma classe, mesmo herdando a classe que contem o grid

        readonly string pathPasta = @"C:\";
        readonly string pathArquivo = @"C:\ECCUS_Dados\Dados.txt";
        StreamWriter sw;
        StreamReader sr;

        private void SalvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(pathPasta + "\\ECCUS_Dados");
                using (sw = new StreamWriter(pathArquivo))
                {
                    for (int i = 0; i < GridDimens.RowCount; i++)
                    {

                        string linha = $"{ GridDimens.Rows[i].Cells["VolUtioGrid"].Value };" +
                              $"{ GridDimens.Rows[i].Cells["ProfundidadeGrid"].Value};";
                        sw.WriteLine(linha);
                    }
                }
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo salvo!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ExcluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(pathArquivo);
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo excluido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CarregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                sr = File.OpenText(pathArquivo);
                while (!sr.EndOfStream)
                {
                    string linha = sr.ReadLine();
                    string[] vect = linha.Split(';');
                    GridDimens.Rows.Add(vect);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Não há arquivo salvo!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
              
    }
}
