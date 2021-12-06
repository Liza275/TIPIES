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

            var operationLog = OperationLogStorage.GetAll().FirstOrDefault(rec => rec.Data == dateTimePicker.Value
                && rec.Type.Equals("Накопление фактических коммерческих расходов за месяц") && rec.Count == 1 && rec.Sum == numericUpDownSum.Value);

            var transaction = new TransactionLog
            {
                DebetId = CharOfAccountsStorage.GetAll().FirstOrDefault(rec => rec.AccountNumber == 20).Id,
                SudKontoD1 = comboBoxExpense.Text,
                CreditId = CharOfAccountsStorage.GetAll().FirstOrDefault(rec => rec.AccountNumber == 10).Id,
                Data = dateTimePicker.Value,
                Count = 1,
                Sum = numericUpDownSum.Value,
                OperationLogId = operationLog.Id
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
