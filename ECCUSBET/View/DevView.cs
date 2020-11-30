using System.Windows.Forms;
using System.Diagnostics;

namespace ECCUSBET.View
{
    public partial class DevView : Form
    {
        public DevView()
        {
            InitializeComponent();
        }
        private void LinkedinIgor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://linkedin.com/in/igor-quaresma-94b308133");
        }

        private void LinkInstagranECCUS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.instagram.com/institutoeccus/");
        }

        private void LinkedinAustregiselo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/austregiselojr/");
        }

        private void LinkedinVanini_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/vanine-farias-501a7b104/");
        }

        private void LinkedinSayonara_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/sayonara-elizi%C3%A1rio-2bb25a116/");
        }

        private void LindedinRafael_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/ssraf/");
        }

        private void LinkedinGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Instituto-ECCUS/ECCUS/tree/main");
        }

        private void LinkedinFabio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/fabiobpmaia");
        }

        private void LinkedinIcaro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/%C3%ADcaro-albuquerque-065a5a159/");
        }
    }
}
