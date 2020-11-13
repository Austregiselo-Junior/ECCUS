using System;
using System.Windows.Forms;

namespace ECCUSBET.View
{
    public partial class StartView : Form
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {

            SizingView sizingView = new SizingView();
            sizingView.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        private void BntFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

    }
}
