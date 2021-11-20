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
                comboBoxDivision.SelectedValue = log.DivisionId;
                comboBoxProduct.SelectedValue = log.ProductId;
            }

            isLoaded = true;
        }

        private string ValidateOperation()
        {
            if (comboBoxOperationType.SelectedIndex < 0)
                return "Выберите тип операции";

            if (numericCount.Enabled && numericCount.Value < 0)
                return "Количество не может быть меньше 0";

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
                    var rasp = OperationLogStorage.GetAll().FirstOrDefault(rec =>
                          rec.Type.Equals("Распределение фактической себестоимости по выпущенной продукции") && rec.Data.Month == dateTimePicker.Value.Month && rec.Data.Year == dateTimePicker.Value.Year);
                    if (rasp != null)
                    {
                        return "Уже существует распределение за указанный месяц";
                    }
                    break;

                case "Реализация готовой продукции":
                    var operations = OperationLogStorage.GetAll().Where(rec => rec.ProductId == (int)comboBoxProduct.SelectedValue);
                    var addedBefore = operations
                                      .Where(rec => rec.Type.Equals("Поступления готовой продукции") && rec.Data.Date <= dateTimePicker.Value.Date)
                                      .Sum(rec => rec.Count);

                    var deletedBefore = operations
                                        .Where(rec => rec.Type.Equals("Реализация готовой продукции") && rec.Data.Date <= dateTimePicker.Value.Date)
                                        .Sum(rec => rec.Count);

                    if ((addedBefore - deletedBefore) < numericCount.Value)
                        return "Недостаточно продукции";

                    break;

                case "Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи":

                    break;
            }

            return string.Empty;
        }

        private void buttonSave_Click(object sender, EventArgs e)
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

            if (operationLog == null)
                OperationLogStorage.Add(log);
            else
            {
                log.Id = operationLog.Id;
                OperationLogStorage.Update(log);
            }

            CreateTransactions();

            grid.DataSource = OperationLogStorage.GetAll();
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
                    break;

                case "Распределение фактической себестоимости по выпущенной продукции":
                    comboBoxDivision.Enabled = false;
                    comboBoxProduct.Enabled = false;
                    textBoxSum.Enabled = false;
                    numericCount.Enabled = false;
                    break;

                case "Реализация готовой продукции":
                    comboBoxDivision.Enabled = false;
                    comboBoxProduct.Enabled = true;
                    textBoxSum.Enabled = true;
                    numericCount.Enabled = true;
                    break;

                case "Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи":
                    comboBoxDivision.Enabled = false;
                    comboBoxProduct.Enabled = false;
                    textBoxSum.Enabled = false;
                    numericCount.Enabled = false;
                    break;
            }
        }

        private void CreateTransactions()
        {

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
                var product = ProductStorage.Get((int)comboBoxProduct.SelectedValue);
                if (product != null)
                {
                    textBoxSum.Text = Math.Round((numericCount.Value * product.PlannedCostPrice), 4).ToString();
                }
            }
        }
    }
}
