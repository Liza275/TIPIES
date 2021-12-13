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

namespace TIPIESProj
{
    public partial class FormReport : Form
    {
        private readonly List<string> reports = new List<string> { "Ведомость распределения фактических затрат",
            "Ведомость продаж продукции", "Ведомость расчета отклонений фактической себестоимости от плановой" };

        private readonly List<string> docTypes = new List<string> { "doc",
            "xls", "pdf" };

        public FormReport()
        {
            InitializeComponent();
            comboBoxReport.DataSource = reports;
            comboBoxDocType.DataSource = docTypes;
        }

        //create
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBoxReport.SelectedValue)
            {
                case "Ведомость распределения фактических затрат":
                    var result1 = ReportDataService.GetFactRasprReport(dateTimePicker1.Value, dateTimePicker2.Value);
                    if (result1 == null)
                        return;
                    dataGridView1.DataSource = result1.DataList;
                    labelTotal1.Text = result1.TotalCount.ToString();
                    labelTotal2.Text = result1.TotalPlannedPrice.ToString();
                    labelTotal3.Text = result1.TotalOtkloneniePlannedCostPrice.ToString();
                    break;
                case "Ведомость продаж продукции":
                    var result2 = ReportDataService.GetSoldReport(dateTimePicker1.Value, dateTimePicker2.Value);
                    if (result2 == null)
                        return;
                    dataGridView1.DataSource = result2.DataList;
                    labelTotal1.Text = result2.TotalSoldSum.ToString();
                    labelTotal2.Text = result2.TotalSoldInPlannedCostPrice.ToString();
                    labelTotal3.Text = result2.TotalSold.ToString();
                    break;
                case "Ведомость расчета отклонений фактической себестоимости от плановой":
                    var result3 = ReportDataService.GetOtrlReport(dateTimePicker1.Value, dateTimePicker2.Value);
                    if (result3 == null)
                        return;
                    dataGridView1.DataSource = result3.DataList;
                    labelTotal1.Text = result3.TotalCount.ToString();
                    labelTotal2.Text = result3.TotalPlannedPrice.ToString();
                    labelTotal3.Text = result3.TotalOtkloneniePlannedCostPrice.ToString();
                    break;
            }

        }
    }
}
