using ECCUSBET.View;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ECCUSBET.Model.Services
{
    class Edi_Arquivo_Service
    {
        public string Linha { get; set; }
        public string Linhas { get; set; }

        readonly SizingView view = new SizingView();
        readonly string pathPasta = @"C:\";
        readonly string pathArquivo = @"C:\ECCUS_Dados\Dados.txt";
        StreamWriter sw;
        StreamReader sr;
        FileStream fs;

        public void SalvarArquivo()
        {
            try
            {
                Directory.CreateDirectory(pathPasta + "\\ECCUS_Dados");
                using (sw = new StreamWriter(pathArquivo))
                {
                    for (int i = 0; i < view.GridDimens.RowCount; i++)
                    {

                        Linha = $"{ view.GridDimens.Rows[i].Cells["VolUtioGrid"].Value };" +
                             $"{ view.GridDimens.Rows[i].Cells["ProfundidadeGrid"].Value};";
                        sw.WriteLine(Linha);
                    }
                }
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
            MessageBox.Show("Arquivo salvo!");
        }

        public void ExcluirArquivo()
        {
            try
            {
                File.Delete(pathArquivo);
            }
            catch (IOException msg)
            {
                MessageBox.Show($"Erro, {msg.Message}", MessageBoxButtons.OK.ToString());
            }
        }

        public void CarregarArquivo()
        {
            try
            {
                fs = new FileStream(pathArquivo, FileMode.Open);
                sr = new StreamReader(fs);
                Linhas = sr.ReadToEnd();

                view.GridDimens.Rows.Add(Linhas);

            }
            catch (IOException)
            {
                MessageBox.Show("Erro, não há arquivo salvo!");
            }
        }
    }
}
