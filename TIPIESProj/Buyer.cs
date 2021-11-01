using System;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class Buyer : Form
    {
        private bool UpdateMode;
        private DataGridView parentGrid;
        private int objId = -1;

        public Buyer(DataBase.Models.Buyer buyer, DataGridView parentGrid)
        {
            InitializeComponent();

            this.parentGrid = parentGrid;

            if (buyer != null)
            {
                objId = buyer.Id;
                UpdateMode = true;

                textBoxFIO.Text = buyer.Fio;
                textBoxNumber.Text = buyer.Number;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNumber.Text.Trim().Length > 11)
                {
                    throw new Exception("Число знаков в номере не больше 11");
                }

                var newData = new DataBase.Models.Buyer
                {
                    Fio = textBoxFIO.Text,
                    Number = Convert.ToInt64(textBoxNumber.Text).ToString()
                };

                if (UpdateMode)
                {
                    newData.Id = objId;
                    BuyerStorage.Update(newData);
                }
                else
                {
                    BuyerStorage.Add(newData);
                }

                parentGrid.DataSource = BuyerStorage.GetAll();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
