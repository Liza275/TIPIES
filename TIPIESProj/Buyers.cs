using System;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class Buyers : Form
    {
        public Buyers()
        {
            InitializeComponent();
            dataGridViewBuyers.DataSource = BuyerStorage.GetAll();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Buyer newForm = new Buyer(null,dataGridViewBuyers);
            newForm.Show();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var selected = (int)dataGridViewBuyers.SelectedRows[0].Cells["Id"].Value;
            Buyer newForm = new Buyer(BuyerStorage.Get(selected), dataGridViewBuyers);
            newForm.Show();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            var selected = (int)dataGridViewBuyers.SelectedRows[0].Cells["Id"].Value;
            BuyerStorage.Delete(selected);
            dataGridViewBuyers.DataSource = BuyerStorage.GetAll();
        }
    }
}
