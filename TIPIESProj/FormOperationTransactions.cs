using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIPIESProj.DataBase.Services;

namespace TIPIESProj
{
    public partial class FormOperationTransactions : Form
    {
        public int? Id { get; set; }
        public FormOperationTransactions()
        {
            InitializeComponent();
        }

        private void FormOperationTransactions_Load(object sender, EventArgs e)
        {
            if (!Id.HasValue) return;

            var operation = OperationLogStorage.Get(Id.Value);

            labelName.Text = operation.Type;
            labelDivision.Text = operation.Division == null ? "" : operation.Division.Name;
            labelDate.Text = operation.Data.ToShortDateString();
            labelCode.Text = operation.Id.ToString();
            labelCount.Text = operation.Count.ToString();
            labelSum.Text = operation.Sum.ToString(CultureInfo.InvariantCulture);

            dataGridView.DataSource = TransactionLogStorage.GetAllViewModels().Where(rec => rec.OperationId == Id.Value).ToList();
        }
    }
}
