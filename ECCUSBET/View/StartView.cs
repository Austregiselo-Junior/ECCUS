using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            StartView Start = new StartView();
            SizingView sizingView = new SizingView();
            sizingView.Show();
            Start.Close();
        }

        private void BntFechar_Click(object sender, EventArgs e)
        {
            StartView Start = new StartView();
            Start.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
