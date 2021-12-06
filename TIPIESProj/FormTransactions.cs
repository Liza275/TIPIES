using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;
using TIPIESProj.DataBase.ViewModels;

namespace TIPIESProj
{
    public partial class FormTransactions : Form
    {
        public FormTransactions()
        {
            InitializeComponent();
        }

        private void LoadData(List<TransactionLogViewModel> data)
        {
            dataGridView.DataSource = data;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var form = new FormCreateTransaction();
            form.ShowDialog();

            var transactions = TransactionLogStorage.GetAllViewModels();
            LoadData(transactions);
        }

        private void buttonOperation_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView.SelectedRows[0].Cells[0].Value;

            //var model = new TransactionLogBindingModel { Id = id };
            var transaction = TransactionLogStorage.Get(id);

            var form = new FormOperationTransactions();
            form.Id = transaction.OperationLogId;
            form.ShowDialog();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value > dateTimePickerTo.Value)
            {
                MessageBox.Show("Выбран неккоректный промежуток", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var transactions = TransactionLogStorage.GetAllViewModels().Where(rec => rec.TransactionDate >= dateTimePickerFrom.Value &&
                rec.TransactionDate <= dateTimePickerTo.Value).ToList();
            LoadData(transactions);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
