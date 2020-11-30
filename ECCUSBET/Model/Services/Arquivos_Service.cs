using ECCUSBET.View;
using System.IO;
using System.Windows.Forms;

namespace ECCUSBET.Model.Services
{
    class Arquivos_Service
    {

        readonly string pathPasta = @"C:\";
        readonly string pathArquivoTBDimensionamento = @"C:\ECCUS_Dados\DadosTB_dimensionamento.txt";
        readonly string pathArquivoTBOrcamento = @"C:\ECCUS_Dados\DadosTB_Orcamento.txt";
        StreamWriter sw;
        StreamReader sr;

        public void SalvarArquivo(SizingView sizingView)
        {
            try
            {
                Directory.CreateDirectory(pathPasta + "\\ECCUS_Dados");
                // Grid de dimensionamento
                using (sw = new StreamWriter(pathArquivoTBDimensionamento))
                {
                    for (int i = 0; i < sizingView.GridDimens.RowCount; i++)
                    {

                        string linha = $"{ sizingView.GridDimens.Rows[i].Cells["TabVolutil"].Value };" +
                              $"{ sizingView.GridDimens.Rows[i].Cells["TabProfundidade"].Value};" +
                              $"{ sizingView.GridDimens.Rows[i].Cells["TabTipodePneu"].Value };" +
                              $"{ sizingView.GridDimens.Rows[i].Cells["TabComprimento"].Value};" +
                              $"{ sizingView.GridDimens.Rows[i].Cells["TabLargura"].Value};";
                        sw.WriteLine(linha);
                    }
                }

                // Grid de orçamento
                using (sw = new StreamWriter(pathArquivoTBOrcamento))
                {
                    for (int i = 0; i < sizingView.GrigOrcamento.RowCount; i++)
                    {

                        string linha = $"{ sizingView.GrigOrcamento.Rows[i].Cells["TabSercicoeEquipamento"].Value };" +
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

        public void ExcluirArquivo()
        {
            try
            {
                File.Delete(pathArquivoTBDimensionamento);
                File.Delete(pathArquivoTBOrcamento);
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo excluido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void CarregarArquivo(SizingView sizingView)
        {
            try
            {
                // Carregar gid de simensionamento
                sr = File.OpenText(pathArquivoTBDimensionamento);
                while (!sr.EndOfStream)
                {
                    string linha = sr.ReadLine();
                    string[] vect = linha.Split(';');
                    sizingView.GridDimens.Rows.Add(vect);
                }

                // Carregar grid de orçamento
                sr = File.OpenText(pathArquivoTBOrcamento);
                while (!sr.EndOfStream)
                {
                    string linha = sr.ReadLine();
                    string[] vect = linha.Split(';');
                    sizingView.GrigOrcamento.Rows.Add(vect);
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
