using System.Diagnostics;
using System.Windows.Forms;

namespace ECCUSBET.View
{
    public partial class NosAjude : Form
    {
        public NosAjude()
        {
            InitializeComponent();
        }

        private void Link_Site_ECCUS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://instituto-eccus.github.io/index.html");
        }
    }
}
