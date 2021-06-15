using ECCUSBET.View;
using System.IO;
using System.Windows.Forms;

namespace ECCUSBET.Model.Services
{
    class Arquivos_Service
    {
        readonly string pathArquivoTBDimensionamento = "DadosTB_dimensionamento.csv";
        readonly string pathArquivoTBOrcamento = "DadosTB_Orcamento.csv";
        StreamWriter sw;
        StreamReader sr;

        /// <summary>
        /// Método para salvar arquivo, onde os dois arquivos são salvos em caminho relativo
        /// </summary>
        /// <param name="sizingView">Recebe o formulário</param>
        public void SalvarArquivo(SizingView sizingView)
        {
            try
            {
                // Grid de dimensionamento
                using (sw = new StreamWriter(pathArquivoTBDimensionamento, append: true))
                {
                    for (var i = 0; i < sizingView.GridDimens.RowCount; i++)
                    {
                        var linha = $"{ sizingView.GridDimens.Rows[i].Cells["TabVolutil"].Value };" +
      $"{ sizingView.GridDimens.Rows[i].Cells["TabProfundidade"].Value};" +
      $"{ sizingView.GridDimens.Rows[i].Cells["TabTipodePneu"].Value };" +
      $"{ sizingView.GridDimens.Rows[i].Cells["TabComprimento"].Value};" +
      $"{ sizingView.GridDimens.Rows[i].Cells["TabLargura"].Value};";
                        sw.WriteLine(linha);
                    }
                }

                // Grid de orçamento
                using (sw = new StreamWriter(pathArquivoTBOrcamento, append: true))
                {
                    for (int i = 0; i < sizingView.GrigOrcamento.RowCount; i++)
                    {
                        var linha = $"{ sizingView.GrigOrcamento.Rows[i].Cells["TabSercicoeEquipamento"].Value };" +
      $"{ sizingView.GrigOrcamento.Rows[i].Cells["TabUnidade"].Value};" +
      $"{ sizingView.GrigOrcamento.Rows[i].Cells["TabPrecoUnitario"].Value };" +
      $"{ sizingView.GrigOrcamento.Rows[i].Cells["TabPrecoTotal"].Value};";
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

        /// <summary>
        /// Método para excluir os arquivos.
        /// </summary>
        public void ExcluirArquivo()
        {
            try
            {
                File.Delete(pathArquivoTBDimensionamento);
                File.Delete(pathArquivoTBOrcamento);
            }
            catch (FileNotFoundException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo excluido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Método para carregar arquivos.
        /// </summary>
        /// <param name="sizingView">Recebe o formulário</param>
        public void CarregarArquivo(SizingView sizingView)
        {
            try
            {
                // Carregar gid de simensionamento
                using (sr = File.OpenText(pathArquivoTBDimensionamento))
                {
                    while (!sr.EndOfStream)
                    {
                        var linha = sr.ReadLine();
                        string[] vect = linha.Split(';');
                        sizingView.GridDimens.Rows.Add(vect);
                    }
                }

                // Carregar grid de orçamento
                using (sr = File.OpenText(pathArquivoTBOrcamento))
                {
                    while (!sr.EndOfStream)
                    {
                        var linha = sr.ReadLine();
                        string[] vect = linha.Split(';');
                        sizingView.GrigOrcamento.Rows.Add(vect);
                    }
                }
            }
            catch (FileNotFoundException msg)
            {
                MessageBox.Show(msg.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
