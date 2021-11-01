using System;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class Division : Form
    {
        private bool UpdateMode;
        private DataGridView parentGrid;
        private int objId = -1;

        public Division(DataBase.Models.Division div, DataGridView parentGrid)
        {
            InitializeComponent();

            comboBoxExpenseAccount.DataSource = CharOfAccountsStorage.GetAll();
            comboBoxExpenseAccount.DisplayMember = "Name";
            comboBoxExpenseAccount.ValueMember = "Id";
            comboBoxExpenseAccount.SelectedIndex = 0;

            this.parentGrid = parentGrid;

            if (div != null)
            {
                objId = div.Id;
                UpdateMode = true;
                textBoxName.Text = div.Name;
                comboBoxExpenseAccount.SelectedValue = div.Id;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var newData = new DataBase.Models.Division
                {
                    ChartOfAccountsId = (int)comboBoxExpenseAccount.SelectedValue,
                    Name = textBoxName.Text
                };

                if (UpdateMode)
                {
                    newData.Id = objId;
                    DivisionsStorage.Update(newData);
                }
                else
                {
                    DivisionsStorage.Add(newData);
                }

                parentGrid.DataSource = DivisionsStorage.GetAll();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
