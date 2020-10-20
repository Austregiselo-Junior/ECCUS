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
Austregíselo Junior     15/09/2020 Implementação das regras de negócio e desenvolvimento de layout;
Austregíselo Junior     16/09/2020 Implementação dos cálculos completa e funcionando;
Austregíselo Junior     17/09/2020 Adicionando a programaçõpa defensiva em caso de erro na adição de dado de entrada;
Austregíselo Junior     18/09/2020 Adicionando a programaçõpa defensiva em caso de erro na adição de dado de entrada;
Austregíselo Junior     21/09/2020 Exportando dados do Grid para excel e fim do APP
-------------------------------------------------------------------------------------------------------------------------
Histórico de Bugs        
Autor                   Data Descrição  
----------------------  ---------- --------------------------------------------------------------------------------
Austregíselo Junior     15/09/2020 O Volume útil está dando valor errado, os valores das funçõa estão dando igual a 0;
                                   Resolução -> A contribuição deve retornar apenas Rc;
Austregíselo Junior     16/09/2020 As funções PeriododeDetencao() e TaxadeAcumulacao() não estão reconhecendo os parâmetros;
                                   Resolução -> No PeriodoDetencao() coloquei o CDiarioTotal dentro da função PeriodoDetencao(). 
                                   Já a função TaxadeAcumulacao estava faltando o ".tex" ao final da expressão.
-------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECCUSBET.View
{
    public partial class SizingView : Form
    {
        public SizingView()
        {
            InitializeComponent();
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {

        }

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
            this.Close();
            StartView startView = new StartView();
            startView.Close();
        }
     
    }
}
