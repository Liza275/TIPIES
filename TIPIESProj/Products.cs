using System;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            dataGridViewProducts.DataSource = ProductStorage.GetAll();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Product newForm = new Product(null, dataGridViewProducts);
            newForm.Show();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var selected = (int)dataGridViewProducts.SelectedRows[0].Cells["Id"].Value;
            Product newForm = new Product(ProductStorage.Get(selected), dataGridViewProducts);
            newForm.Show();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            var selected = (int)dataGridViewProducts.SelectedRows[0].Cells["Id"].Value;
            ProductStorage.Delete(selected);
            dataGridViewProducts.DataSource = ProductStorage.GetAll();
        }
    }
}
