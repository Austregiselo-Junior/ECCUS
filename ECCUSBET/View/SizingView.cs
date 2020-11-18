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
Austregíselo Junior     11/11/2020 Implementando o cálculo de custo total, segundoo a tabela de custo;
Austregíselo Junior     16/11/2020 Implementando o dimensionamento completo com os pneus e mecânica da tabela de orçamento;
Austregíselo Junior     17/11/2020 Implementando edição de arquivo da tabela de orçamento (OK);
Austregíselo Junior     17/11/2020 Retirei análise gráfica porque achei sem sentido;
Austregíselo Junior     18/11/2020 Alterei o leyout e a mecânica de abrir telas no StartView.cs e Program.cs;
Austregíselo Junior     18/11/2020 Adicionei programação defensiva para que o usuário não calcule nada com dado = 0;

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
            else if (txtNPessoas.Text == "0" || txtIntervalodeLimpeza.Text == "0")
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtTemperatura.Text == "0" || TxtLarguraPneu.Text == "0")
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtPerfil.Text == "0" || TxtAro.Text == "0")
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                    Bet_Entities bet = new Bet_Entities(ocupacao, npessoas, intervalo, temperatura);
                    Pneu_Entities pneu = new Pneu_Entities(larguraPneu, perfil, aro);

                    // Chamada do métododo 
                    bet.Dimensionamento();
                    bet.ProfundidadeMedia();
                    pneu.Dimensi_Pneu();
                    bet.Largura_Bet(larguraPneu);
                    pneu.QTE_Pneu(bet.VolUtio);
                    bet.ComprimentodaBaciat(pneu.QTEPneus, pneu.LarguraPneu);

                    // Saída de dados
                    TxtVolUtio.Text = bet.VolUtio.ToString("F2", CultureInfo.InvariantCulture);
                    TxtProfundidadeMedia.Text = bet.ProfundidadeM.ToString("F2", CultureInfo.InvariantCulture);
                    TxtVolPneu.Text = pneu.VolPneu.ToString("F2", CultureInfo.InvariantCulture);
                    TxtLarguradaBet.Text = bet.LarguradaBet.ToString("F2", CultureInfo.InvariantCulture);
                    TxtQtePeneus.Text = pneu.QTEPneus.ToString("F2", CultureInfo.InvariantCulture);
                    TxtComprimento.Text = bet.ComprimentoBet.ToString("F2", CultureInfo.InvariantCulture);

                    // Add os resultados ao grid de dimensionamento
                    GridDimens.Rows.Add(TxtVolUtio.Text, TxtProfundidadeMedia.Text, TxtQtePeneus.Text, TxtComprimento.Text, TxtLarguradaBet.Text);
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

        private void BtnExcluirLinha_TbOrcamento_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir a linha selecionada?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (GrigOrcamento.CurrentRow == null)
                {
                    MessageBox.Show("Selecione uma linha da tabela!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    GrigOrcamento.Rows.RemoveAt(GrigOrcamento.CurrentRow.Index);
                }
            }
        }

        private void BtnAddGridOrcamento_Click(object sender, EventArgs e)
        {
            double precoUnidade, unidade, precoTotal;

            if (TxtServico_Equi.Text == "")
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtPreco_Uni.Text == "0" || TxtUnidade.Text == "0")
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _ = TxtServico_Equi.Text;

                try
                {
                    precoUnidade = double.Parse(TxtPreco_Uni.Text);
                    unidade = double.Parse(TxtUnidade.Text);

                    precoTotal = unidade * precoUnidade;
                    TxtPreco_total.Text = precoTotal.ToString("F2", CultureInfo.InvariantCulture);

                    // add os dados ao grid de orçamento
                    GrigOrcamento.Rows.Add(TxtServico_Equi.Text, TxtUnidade.Text, TxtPreco_Uni.Text, TxtPreco_total.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Adicione o ítem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void BtnCustoTotal_Click_1(object sender, EventArgs e)
        {
            double quantia;
            double custoTotal = 0;
            int nlinhas = GrigOrcamento.Rows.Count;

            for (int i = 0; i < nlinhas; i++) // É dessa forma porque com 0 são 4 iterações pra 3 lnhas, assim i tem que ser < que nlinhas e não <=
            {
                quantia = Convert.ToDouble(GrigOrcamento.Rows[i].Cells["TabPrecoTotal"].Value);
                custoTotal += quantia;
            }

            TxtCustoTotal.Text = custoTotal.ToString("F2", CultureInfo.InvariantCulture);
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
            txtNPessoas.Clear();
            TxtVolUtio.Clear();
            TxtProfundidadeMedia.Clear();
            GridDimens.Rows.Clear();
            TxtServico_Equi.Clear();
            TxtUnidade.Clear();
            TxtPreco_Uni.Clear();
            TxtPreco_total.Clear();
            GrigOrcamento.Rows.Clear();
            TxtLarguradaBet.Clear();
            TxtComprimento.Clear();
            TxtProfundidadeMedia.Clear();
            TxtQtePeneus.Clear();
            TxtLarguraPneu.Clear();
            TxtPerfil.Clear();
            TxtAro.Clear();
            TxtVolPneu.Clear();
            TxtCustoTotal.Clear();
        }


        // -------------------------- Serviços de edição de arquivos apartir do grid -------------------//
        // Só está aqui porque não consigo editar o grid e nem salvar arquivo com dados do grid a partir de uma classe, mesmo herdando a classe que contem o grid

        readonly string pathPasta = @"C:\";
        readonly string pathArquivoTBDimensionamento = @"C:\ECCUS_Dados\DadosTB_dimensionamento.txt";
        readonly string pathArquivoTBOrcamento = @"C:\ECCUS_Dados\DadosTB_Orcamento.txt";
        StreamWriter sw;
        StreamReader sr;

        private void SalvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TB dimensionamento
            try
            {
                Directory.CreateDirectory(pathPasta + "\\ECCUS_Dados");
                using (sw = new StreamWriter(pathArquivoTBDimensionamento))
                {
                    for (int i = 0; i < GridDimens.RowCount; i++)
                    {

                        string linha = $"{ GridDimens.Rows[i].Cells["TabVolutio"].Value };" +
                              $"{ GridDimens.Rows[i].Cells["TabProfundidade"].Value};" +
                              $"{ GridDimens.Rows[i].Cells["TabTipodePneu"].Value };" +
                              $"{ GridDimens.Rows[i].Cells["TabComprimento"].Value};" +
                              $"{ GridDimens.Rows[i].Cells["TabLargura"].Value};";
                        sw.WriteLine(linha);
                    }
                }
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo salvo!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TB orçamento
            try
            {
                Directory.CreateDirectory(pathPasta + "\\ECCUS_Dados");
                using (sw = new StreamWriter(pathArquivoTBOrcamento))
                {
                    for (int i = 0; i < GrigOrcamento.RowCount; i++)
                    {

                        string linha = $"{ GrigOrcamento.Rows[i].Cells["TabSercicoeEquipamento"].Value };" +
                              $"{ GrigOrcamento.Rows[i].Cells["TabUnidade"].Value};" +
                              $"{ GrigOrcamento.Rows[i].Cells["TabPrecoUnitario"].Value };" +
                              $"{ GrigOrcamento.Rows[i].Cells["TabPrecoTotal"].Value};";
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
            // TB dimensionamento
            try
            {
                File.Delete(pathArquivoTBDimensionamento);
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo excluido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TB orçamento
            try
            {
                File.Delete(pathArquivoTBOrcamento);
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo excluido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        // TB dimensionamento
        private void CarregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                sr = File.OpenText(pathArquivoTBDimensionamento);
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

            // TB orçamento
            try
            {
                sr = File.OpenText(pathArquivoTBOrcamento);
                while (!sr.EndOfStream)
                {
                    string linha = sr.ReadLine();
                    string[] vect = linha.Split(';');
                    GrigOrcamento.Rows.Add(vect);
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
