using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIPIESProj
{
    public partial class Buyers : Form
    {
        public Buyers()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Buyer newForm = new Buyer();
            newForm.Show();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Buyer newForm = new Buyer();
            newForm.Show();
        }
    }
}
