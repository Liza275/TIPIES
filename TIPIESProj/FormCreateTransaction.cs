using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class FormCreateTransaction : Form
    {
        public FormCreateTransaction()
        {
            InitializeComponent();
            comboBoxExpense.DataSource = CharOfAccountsStorage.GetAll().Where(rec => rec.Name.Equals("Учет затрат на выпуск продукции")).ToList();
            comboBoxExpense.ValueMember = "Id";
            comboBoxExpense.DisplayMember = "Name";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            OperationLogStorage.Add(new OperationLog
            {
                Data = dateTimePicker.Value,
                Type = "Накопление фактических коммерческих расходов за месяц",
                Count = 1,
                Sum = numericUpDownSum.Value
            });

            var coa20 = CharOfAccountsStorage.GetAll().FirstOrDefault(rec => rec.AccountNumber == 20);
            var coa10 = CharOfAccountsStorage.GetAll().FirstOrDefault(rec => rec.AccountNumber == 10);
            var transaction = new TransactionLog
            {
                DebetId = coa20.Id,
                SudKontoD1 = coa20.Name,
                CreditId = coa10.Id,
                SubKontoK1 = coa10.Name,
                Data = dateTimePicker.Value,
                Count = 1,
                Sum = numericUpDownSum.Value,
                OperationLogId= OperationLogStorage.LastAddedId
            };

            TransactionLogStorage.Add(transaction);
            Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
