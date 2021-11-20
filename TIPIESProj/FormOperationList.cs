using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIPIESProj.DataBase.Models.Filter;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class FormOperationList : Form
    {
        private readonly List<string> operationTypes = new List<string> { "Поступления готовой продукции", "Распределение коммерческих расходов за месяц",
        "Реализация готовой продукции", "Списание коммерческих расходов за месяц","Все"};

        public FormOperationList()
        {
            InitializeComponent();
            comboBoxOperationType.DataSource = operationTypes;
            gridOperations.DataSource = OperationLogStorage.GetAll();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var form = new FormAddEditOperation(null, gridOperations);
            form.Show();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var selected = (int)gridOperations.SelectedRows[0].Cells["Id"].Value;
            var form = new FormAddEditOperation(OperationLogStorage.Get(selected), gridOperations);
            form.Show();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var selected = (int)gridOperations.SelectedRows[0].Cells["Id"].Value;
            var delResult = OperationLogStorage.Delete(selected);
            if (!string.IsNullOrEmpty(delResult))
            {
                MessageBox.Show(delResult, "Message");
            }
            else
            {
                gridOperations.DataSource = OperationLogStorage.GetAll();
            }
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (comboBoxOperationType.SelectedValue.Equals("Все"))
            {
                gridOperations.DataSource = OperationLogStorage.GetAll();
                return;
            }

            var filter = new OperationLogFilterModel
            {
                DateFrom = datePickerFrom.Value,
                DateTo = datePickerTo.Value,
                OperationType = (string)comboBoxOperationType.SelectedValue
            };

            gridOperations.DataSource = OperationLogStorage.GetFiltered(filter);
        }
    }
}
