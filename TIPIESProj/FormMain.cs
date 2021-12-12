using System;
using System.Windows.Forms;

namespace TIPIESProj
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void продукцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Products();
            form.Show();
        }

        private void подразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Divisions();
            form.Show();
        }

        private void покупательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Buyers();
            form.Show();
        }

        private void планСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.Show();
        }

        private void журналОперациToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormOperationList();
            form.Show();
        }

        private void журналПроводокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormTransactions();
            form.Show();
        }

        private void отчетРаспределенияФактическихЗатратToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void отчетРасчетаОтклоненийToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void отчетПродажПродукцииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
