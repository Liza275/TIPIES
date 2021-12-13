using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIPIESProj.DataBase.Enums;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class FormAddEditOperation : Form
    {
        private OperationLog operationLog;
        private DataGridView grid;
        private readonly bool isLoaded = false;

        private readonly List<string> operationTypes;

        public FormAddEditOperation(OperationLog log, DataGridView grid)
        {
            InitializeComponent();
            operationTypes = OperationsHelper.GetTypeList();

            operationLog = log;

            this.grid = grid;

            comboBoxOperationType.DataSource = operationTypes;

            comboBoxBuyer.DataSource = BuyerStorage.GetAll();
            comboBoxBuyer.DisplayMember = "Fio";
            comboBoxBuyer.ValueMember = "Id";

            comboBoxDivision.DataSource = DivisionsStorage.GetAll();
            comboBoxDivision.DisplayMember = "Name";
            comboBoxDivision.ValueMember = "Id";

            comboBoxProduct.DataSource = ProductStorage.GetAll();
            comboBoxProduct.DisplayMember = "Name";
            comboBoxProduct.ValueMember = "Id";

            if (operationLog == null)
            {
                comboBoxOperationType.SelectedIndex = 0;
                comboBoxBuyer.SelectedIndex = comboBoxProduct.SelectedIndex = comboBoxDivision.SelectedIndex = -1;
            }
            else
            {
                buttonSave.Text = "Сохранить";
                textBoxSum.Text = log.Sum.ToString();
                numericCount.Value = log.Count;
                dateTimePicker.Value = log.Data;
                comboBoxOperationType.SelectedIndex = operationTypes.IndexOf(log.Type);
                comboBoxBuyer.SelectedValue = log.BuyerId;
                if (log.DivisionId != null)
                    comboBoxDivision.SelectedValue = log.DivisionId;
                if (log.ProductId != null)
                    comboBoxProduct.SelectedValue = log.ProductId;
            }

            isLoaded = true;
        }

        private string ValidateOperation()
        {
            if (comboBoxOperationType.SelectedIndex < 0)
                return "Выберите тип операции";

            if (numericCount.Enabled && numericCount.Value < 1)
                return "Количество не может быть меньше 1";

            if (textBoxSum.Enabled && (!decimal.TryParse(textBoxSum.Text, out decimal result) || result < 0))
                return "Некорректное значение суммы";

            if (comboBoxDivision.Enabled && comboBoxDivision.SelectedIndex < 0)
                return "Выберите подразделение";

            if (comboBoxProduct.Enabled && comboBoxProduct.SelectedIndex < 0)
                return "Выберите продукт";

            if (comboBoxBuyer.SelectedIndex < 0)
                return "Выберите покупателя";

            switch (comboBoxOperationType.SelectedValue)
            {
                case "Поступления готовой продукции":

                    break;

                case "Распределение фактической себестоимости по выпущенной продукции":
                    var existPostyp = OperationLogStorage.GetAll().FirstOrDefault(rec =>
                        rec.Type.Equals("Поступления готовой продукции") && rec.Data.Date <= dateTimePicker.Value.Date);

                    if (existPostyp == null)
                        return "Не найдено поступление";

                    var rasp = OperationLogStorage.GetAll().FirstOrDefault(rec =>
                          rec.Type.Equals("Распределение фактической себестоимости по выпущенной продукции") &&
                              rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year);
                    if (rasp != null)
                    {
                        return "Уже существует распределение за указанный месяц";
                    }

                    var provodka = OperationLogStorage.GetAll().FirstOrDefault(rec => rec.Type.Equals("Накопление фактических коммерческих расходов за месяц") &&
                                rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year);
                    if (provodka == null)
                    {
                        return "Не найдены проводки 20-10";
                    }

                    break;

                case "Реализация готовой продукции":
                    var operations = OperationLogStorage.GetAll().Where(rec => rec.ProductId == (int)comboBoxProduct.SelectedValue);
                    var addedBefore = operations
                                      .Where(rec => rec.Type.Equals("Поступления готовой продукции") &&
                                          rec.Data.Date <= dateTimePicker.Value.Date)
                                      .Sum(rec => rec.Count);

                    var deletedBefore = operations
                                        .Where(rec => rec.Type.Equals("Реализация готовой продукции") && rec.Data.Date <= dateTimePicker.Value.Date)
                                        .Sum(rec => rec.Count);

                    if ((addedBefore - deletedBefore) < numericCount.Value)
                        return "Недостаточно продукции";

                    break;

                case "Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи":
                    var existPostyp2 = OperationLogStorage.GetAll().FirstOrDefault(rec =>
                        rec.Type.Equals("Поступления готовой продукции") && rec.Data.Date <= dateTimePicker.Value.Date);

                    if (existPostyp2 == null)
                        return "Не найдено поступление";
                    break;
            }

            return string.Empty;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                var valResult = ValidateOperation();
                if (!string.IsNullOrEmpty(valResult))
                {
                    MessageBox.Show(valResult, "Ошибка");
                    return;
                }

                var log = new OperationLog
                {
                    Type = (string)comboBoxOperationType.SelectedValue,
                    Data = dateTimePicker.Value,
                    Count = numericCount.Enabled ? (int)numericCount.Value : 0,
                    Sum = textBoxSum.Enabled ? decimal.Parse(textBoxSum.Text) : 0,
                    DivisionId = comboBoxDivision.Enabled ? (int?)comboBoxDivision.SelectedValue : null,
                    ProductId = comboBoxProduct.Enabled ? (int?)comboBoxProduct.SelectedValue : null,
                    BuyerId = (int?)comboBoxBuyer.SelectedValue
                };

                string result;
                if (operationLog == null)
                {
                    result = OperationLogStorage.Add(log);
                    log.Id = OperationLogStorage.LastAddedId;
                }
                else
                {
                    log.Id = operationLog.Id;
                    result = OperationLogStorage.Update(log);
                }

                if (!string.IsNullOrEmpty(result))
                {
                    MessageBox.Show(result, "Сообщение");
                }

                CreateTransactions(log);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }

            grid.DataSource = OperationLogStorage.GetAllView();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxOperationType_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBoxOperationType.SelectedValue)
            {
                case "Поступления готовой продукции":
                    comboBoxDivision.Enabled = true;
                    comboBoxProduct.Enabled = true;
                    textBoxSum.Enabled = true;
                    numericCount.Enabled = true;
                    textBoxCost.Enabled = false;
                    break;

                case "Распределение фактической себестоимости по выпущенной продукции":
                    comboBoxDivision.Enabled = false;
                    comboBoxProduct.Enabled = false;
                    textBoxSum.Enabled = false;
                    numericCount.Enabled = false;
                    textBoxCost.Enabled = false;
                    break;

                case "Реализация готовой продукции":
                    comboBoxDivision.Enabled = false;
                    comboBoxProduct.Enabled = true;
                    textBoxSum.Enabled = true;
                    numericCount.Enabled = true;
                    textBoxCost.Enabled = true;
                    break;

                case "Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи":
                    comboBoxDivision.Enabled = false;
                    comboBoxProduct.Enabled = false;
                    textBoxSum.Enabled = false;
                    numericCount.Enabled = false;
                    textBoxCost.Enabled = false;
                    break;
            }
        }

        private void CreateTransactions(OperationLog log)
        {
            var charOfAccounts = CharOfAccountsStorage.GetAll();
            var transactions = TransactionLogStorage.GetAll();

            var coa43 = charOfAccounts.FirstOrDefault(rec => rec.AccountNumber == 43);
            var coa20 = charOfAccounts.FirstOrDefault(rec => rec.AccountNumber == 20);
            var coa90 = charOfAccounts.FirstOrDefault(rec => rec.AccountNumber == 90);
            var coa62 = charOfAccounts.FirstOrDefault(rec => rec.AccountNumber == 62);

            var product = comboBoxProduct.SelectedItem as DataBase.Models.Product;
            if (log.Type.Equals("Поступления готовой продукции"))
            {
                TransactionLogStorage.Add(new TransactionLog
                {
                    DebetId = coa43.Id,
                    CreditId = coa20.Id,
                    SudKontoD1 = coa43.Name,
                    SubKontoK1 = coa20.Name,
                    OperationLogId = log.Id,
                    Data = dateTimePicker.Value,
                    Count = log.Count,
                    ProductId = product.Id,
                    Sum = log.Sum
                });
            }
            if (log.Type.Equals("Распределение фактической себестоимости по выпущенной продукции"))
            {
                var doSum = transactions
                        .Where(rec => rec.Debet.AccountNumber == 20 &&
                            rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year)
                        .Sum(rec => rec.Sum);
                var spiSum = transactions
                    .Where(rec => rec.OperationLog != null && rec.OperationLog.Type.Equals("Поступления готовой продукции") &&
                        rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year).Sum(rec => rec.Sum);

                foreach (var el in ProductStorage.GetAll())
                {
                    //if not presented product
                    var spiData = transactions
                    .Where(rec => rec.Debet.AccountNumber == 43 && rec.Credit.AccountNumber == 20 && rec.Product.Name.Equals(el.Name) &&
                        rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year);

                    if (!spiData.Any())
                        continue;
                    //if (!spiData.Any())
                    //    throw new Exception("Не все товары представлены");

                    var spi = spiData.Sum(rec => rec.Sum);
                    var sfi = (doSum / spiSum) * spi;

                    if (sfi - spi == 0)
                        continue;

                    TransactionLogStorage.Add(new TransactionLog
                    {
                        DebetId = coa43.Id,
                        CreditId = coa20.Id,
                        SudKontoD1 = coa43.Name,
                        SubKontoK1 = coa20.Name,
                        OperationLogId = log.Id,
                        Data = dateTimePicker.Value,
                        Count = null,
                        Sum = sfi - spi
                    });
                }
            }
            if (log.Type.Equals("Реализация готовой продукции"))
            {
                TransactionLogStorage.Add(new TransactionLog
                {
                    DebetId = coa90.Id,
                    CreditId = coa43.Id,
                    SudKontoD1 = coa90.Name,
                    SubKontoK1 = coa43.Name,
                    OperationLogId = log.Id,
                    Data = dateTimePicker.Value,
                    Count = log.Count,
                    ProductId = product.Id,
                    Sum = textBoxSum.Enabled ? decimal.Parse(textBoxSum.Text) : 0
                });

                TransactionLogStorage.Add(new TransactionLog
                {
                    DebetId = coa62.Id,
                    CreditId = coa90.Id,
                    SudKontoD1 = coa62.Name,
                    SubKontoK1 = coa90.Name,
                    OperationLogId = log.Id,
                    Data = dateTimePicker.Value,
                    Count = log.Count,
                    ProductId = product.Id,
                    Sum = log.Sum
                });
            }
            if (log.Type.Equals("Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи"))
            {
                foreach (var el in ProductStorage.GetAll())
                {
                    var postypTrans = transactions.Where(rec => rec.OperationLog != null && rec.OperationLog.Type.Equals("Поступления готовой продукции") &&
                        rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year &&
                        rec.Product.Name.Equals(el.Name));

                    if (!postypTrans.Any())
                        continue;
                    //if (!postypTrans.Any())
                    //    throw new Exception("Не все товары представлены");

                    var kolfi = (int)postypTrans.Sum(rec => rec.Count);
                    var sumfi = postypTrans.Sum(rec => rec.Sum);

                    var realizTrans = transactions
                    .Where(rec => rec.Debet.AccountNumber == 90 && rec.Credit.AccountNumber == 43 && rec.Product.Name.Equals(el.Name) &&
                        rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year);

                    if (!realizTrans.Any())
                        continue;
                    //if (!realizTrans.Any())
                    //    throw new Exception("Не все товары представлены");

                    var kolpi = (int)realizTrans.Sum(rec => rec.Count);
                    var sumpi = realizTrans.Sum(rec => rec.Sum);

                    var sum = ((sumfi / kolfi) - (sumpi / kolpi)) * kolfi;
                    if (sum == 0)
                        continue;

                    TransactionLogStorage.Add(new TransactionLog
                    {
                        DebetId = coa90.Id,
                        CreditId = coa43.Id,
                        SudKontoD1 = coa90.Name,
                        SubKontoK1 = coa43.Name,
                        OperationLogId = log.Id,
                        Data = dateTimePicker.Value,
                        Count = null,
                        Sum = sum
                    });
                }
            }
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoaded && comboBoxProduct.SelectedIndex != -1)
            {
                var product = ProductStorage.Get((int)comboBoxProduct.SelectedValue);
                if (product != null)
                {
                    textBoxSum.Text = Math.Round((numericCount.Value * product.PlannedCostPrice), 4).ToString();
                }
            }
        }

        private void numericCount_ValueChanged(object sender, EventArgs e)
        {
            if (isLoaded && comboBoxProduct.SelectedIndex != -1)
            {
                if (decimal.TryParse(textBoxCost.Text.Replace(',', '.'), out decimal newPrice))
                {
                    textBoxSum.Text = Math.Round((numericCount.Value * newPrice), 4).ToString();
                }
                else
                {
                    var product = ProductStorage.Get((int)comboBoxProduct.SelectedValue);
                    if (product != null)
                    {
                        textBoxSum.Text = Math.Round((numericCount.Value * product.PlannedCostPrice), 4).ToString();
                    }
                }
            }
        }

        private void textBoxCost_TextChanged(object sender, EventArgs e)
        {
            if (isLoaded && comboBoxProduct.SelectedIndex != -1)
            {
                if (decimal.TryParse(textBoxCost.Text.Replace(',', '.'), out decimal newPrice))
                {
                    textBoxSum.Text = Math.Round((numericCount.Value * newPrice), 4).ToString();
                }
                else
                {
                    var product = ProductStorage.Get((int)comboBoxProduct.SelectedValue);
                    if (product != null)
                    {
                        textBoxSum.Text = Math.Round((numericCount.Value * product.PlannedCostPrice), 4).ToString();
                    }
                }
            }
        }
    }
}
