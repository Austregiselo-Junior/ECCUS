using ECCUSBET.View;
using System;
using System.Windows.Forms;

namespace ECCUSBET
{
    static class Program
    {
        public static SizingView SizingView { get; private set; }

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartView());
            Application.Run(new SizingView());

        }
    }
}
