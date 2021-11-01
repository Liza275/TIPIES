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
    public partial class Buyer : Form
    {
        public Buyer()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Buyers newForm = new Buyers();
            newForm.Show();
        }
    }
}
