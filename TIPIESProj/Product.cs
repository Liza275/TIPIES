using System;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class Product : Form
    {
        private bool UpdateMode;
        private DataGridView parentGrid;
        private int objId = -1;

        public Product(DataBase.Models.Product product, DataGridView parentGrid)
        {
            InitializeComponent();

            this.parentGrid = parentGrid;
            
            if (product != null)
            {
                objId = product.Id;
                UpdateMode = true;
                textBoxName.Text = product.Name;
                textBoxCost.Text = product.PlannedCostPrice.ToString();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                var newData = new DataBase.Models.Product
                {
                    Name = textBoxName.Text,
                    PlannedCostPrice = decimal.Parse(textBoxCost.Text)
                };

                if (UpdateMode)
                {
                    newData.Id = objId;
                    ProductStorage.Update(newData);
                }
                else
                {
                    ProductStorage.Add(newData);
                }

                parentGrid.DataSource = ProductStorage.GetAll();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
