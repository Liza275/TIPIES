using System;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class Divisions : Form
    {
        public Divisions()
        {
            InitializeComponent();
            dataGridViewDivisions.DataSource = DivisionsStorage.GetAll();

            var list = CharOfAccountsStorage.GetAll();
            list.Add(new DataBase.Models.ChartOfAccounts { Id = -1, Name = "Все" });
            comboBoxExpenseAccount.DataSource = list;
            comboBoxExpenseAccount.DisplayMember = "Name";
            comboBoxExpenseAccount.ValueMember = "Id";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Division newForm = new Division(null, dataGridViewDivisions);
            newForm.Show();
        }
        
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var selected = (int)dataGridViewDivisions.SelectedRows[0].Cells["Id"].Value;
            Division newForm = new Division(DivisionsStorage.Get(selected), dataGridViewDivisions);
            newForm.Show();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            var selected = (int)dataGridViewDivisions.SelectedRows[0].Cells["Id"].Value;
            DivisionsStorage.Delete(selected);
            dataGridViewDivisions.DataSource = DivisionsStorage.GetAll();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            dataGridViewDivisions.DataSource = DivisionsStorage.GetFiltered((int)comboBoxExpenseAccount.SelectedValue);
        }
    }
}
