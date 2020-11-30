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
Austregíselo Junior     19/11/2020 Fazendo correção de portugûes e melhorando o código com implementação de serviços de edição de arquivos;
Austregíselo Junior     20/11/2020 Ajuste na largura da bet = diâmetro do pneu + 1.0;
Austregíselo Junior     20/11/2020 Verificar a questão dos limites e pesquisar sobre as configurações, certificados e assinaturas;
Austregíselo Junior     23/11/2020 Substituindo as igualdades por equals(A diferença é que == compara a refeência e o equals compara o conteúdo do objeto);

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
using ECCUSBET.Model.Services;
using System;
using System.Globalization;
using System.Windows.Forms;


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
            else if (txtNPessoas.Text.Equals("0") || txtIntervalodeLimpeza.Text.Equals("0"))
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtTemperatura.Text.Equals("0") || TxtLarguraPneu.Text.Equals("0"))
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtPerfil.Text.Equals("0") || TxtAro.Text.Equals("0"))
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
                    bet.Largura_Bet(pneu.DiametroPneu);
                    pneu.QTE_Pneu(bet.VolUtio);
                    bet.ComprimentodaBaciat(pneu.QTEPneus, pneu.LarguraPneu);


                    // Saída de dados (Esse tipo de saída só funciona porque passei o próprio form como parâmetro através do "this")
                   
                    bet.SaidadeDados(this);
                    pneu.SaidadeDados(this);


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

            if (TxtServico_Equi.Text.Equals(""))
            {
                MessageBox.Show("Adicione o ítem corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtPreco_Uni.Text.Equals("0") || TxtUnidade.Text.Equals("0"))
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

        readonly Arquivos_Service arquivos = new Arquivos_Service();

        private void SalvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arquivos.SalvarArquivo(this);
        }

        private void ExcluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arquivos.ExcluirArquivo();
        }

        private void CarregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arquivos.CarregarArquivo(this);
        }

        private void NosAjudeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NosAjude nosAjude = new NosAjude();
            nosAjude.Show();
        }

        private void FeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();
        }
    }
}
