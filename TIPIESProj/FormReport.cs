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
using TIPIESProj.DataBase.Services.DocGenerator;
using TIPIESProj.DataBase.ViewModels;
using TIPIESProj.DataBase.ViewModels.Report;

namespace TIPIESProj
{
    public partial class FormReport : Form
    {
        private readonly List<string> reports = new List<string> { "Ведомость распределения фактических затрат",
            "Ведомость продаж продукции", "Ведомость расчета отклонений фактической себестоимости от плановой","Отчет по проводкам" };

        private readonly List<string> docTypes = new List<string> { "doc",
            "xls", "pdf" };

        List<TransactionLogViewModel> tl = new List<TransactionLogViewModel>();
        FactRasprFullReportModel fr = new FactRasprFullReportModel();
        SoldFullReportModel sf = new SoldFullReportModel();
        FactRasprFullReportModel or = new FactRasprFullReportModel();

        bool selectedTl = false;
        bool selectedFr = false;
        bool selectedSf = false;
        bool selectedOr = false;

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
                    fr = ReportDataService.GetFactRasprReport(dateTimePicker1.Value, dateTimePicker2.Value);
                    if (fr == null)
                        return;
                    dataGridView1.DataSource = fr.DataList;
                    labelTotal1.Text = fr.TotalCount.ToString();
                    labelTotal2.Text = fr.TotalPlannedPrice.ToString();
                    labelTotal3.Text = fr.TotalOtkloneniePlannedCostPrice.ToString();
                    selectedTl = false;
                    selectedFr = true;
                    selectedSf = false;
                    selectedOr = false;
                    break;
                case "Ведомость продаж продукции":
                    sf = ReportDataService.GetSoldReport(dateTimePicker1.Value, dateTimePicker2.Value);
                    if (sf == null)
                        return;
                    dataGridView1.DataSource = sf.DataList;
                    labelTotal1.Text = sf.TotalSoldSum.ToString();
                    labelTotal2.Text = sf.TotalSoldInPlannedCostPrice.ToString();
                    labelTotal3.Text = sf.TotalSold.ToString();
                    selectedTl = false;
                    selectedFr = false;
                    selectedSf = true;
                    selectedOr = false;
                    break;
                case "Ведомость расчета отклонений фактической себестоимости от плановой":
                    or = ReportDataService.GetOtrlReport(dateTimePicker1.Value, dateTimePicker2.Value);
                    if (or == null)
                        return;
                    dataGridView1.DataSource = or.DataList;
                    labelTotal1.Text = or.TotalCount.ToString();
                    labelTotal2.Text = or.TotalPlannedPrice.ToString();
                    labelTotal3.Text = or.TotalOtkloneniePlannedCostPrice.ToString();
                    selectedTl = false;
                    selectedFr = false;
                    selectedSf = false;
                    selectedOr = true;
                    break;

                case "Отчет по проводкам":
                    tl = TransactionLogStorage.GetAllViewModels().Where(rec => rec.TransactionDate.Date >= dateTimePicker1.Value && rec.TransactionDate.Date <= dateTimePicker2.Value).ToList();
                    if (tl == null)
                        return;
                    dataGridView1.DataSource = tl;
                    labelTotal1.Text = "";
                    labelTotal2.Text = "";
                    labelTotal3.Text = "";
                    selectedTl = true;
                    selectedFr = false;
                    selectedSf = false;
                    selectedOr = false;
                    break;
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            switch (comboBoxDocType.SelectedValue)
            {
                case "doc":
                    if (selectedTl)
                        CreateWord.CreateWordTable($"D:/otchetTl{Guid.NewGuid()}.docx", "Проводки", GetHelperWordsTl(), tl);
                    if (selectedFr)
                        CreateWord.CreateWordTable($"D:/otchetFr{Guid.NewGuid()}.docx", "Фактическое распределение", GetHelperWordFr(), fr.DataList, fr);
                    if (selectedOr)
                        CreateWord.CreateWordTable($"D:/otchetOr{Guid.NewGuid()}.docx", "Ведомость расчета отклонений фактической себестоимости от плановой", GetHelperWordFr(), or.DataList, or);
                    if (selectedSf)
                        CreateWord.CreateWordTable($"D:/otchetSf{Guid.NewGuid()}.docx", "Ведомость продаж продукции", GetHelperWordSf(), sf.DataList, sf);
                    break;
                case "pdf":
                    if (selectedTl)
                        CreatePdfTl.SaveTable($"D:/otchetTl{Guid.NewGuid()}.pdf", "Проводки", GetColumnsTl(), GetRowsTl(), tl);
                    if (selectedFr)
                        CreatePdfFr.SaveTable($"D:/otchetFr{Guid.NewGuid()}.pdf", "Фактическое распределение", GetColumnsFr(), GetRowsTl(), fr.DataList, fr);
                    if (selectedOr)
                        CreatePdfFr.SaveTable($"D:/otchetOr{Guid.NewGuid()}.pdf", "Ведомость расчета отклонений фактической себестоимости от плановой", GetColumnsFr(), GetRowsTl(), or.DataList, or);
                    if (selectedSf)
                        CreatePdfSf.SaveTable($"D:/otchetFr{Guid.NewGuid()}.pdf", "Фактическое распределение", GetColumnsSf(), GetRowsTl(), sf.DataList, sf);
                    break;
                case "xls":

                    break;
            }
        }

        int widthSf = 100;

        private List<string> GetHelperWordsSfProps()
        {
            var result = new List<string>
            {
                "ProductId",
                "ProductName",
                "SoldSum",
                "SoldInPlannedCostPrice",
                "TotalSoldByProduct"
            };
            return result;
        }

        private List<HelperWordTable> GetHelperWordSf()
        {
            var result = new List<HelperWordTable>
            {
                new HelperWordTable
                {
                    MainHeader = "Номер продукта",
                    Width = widthSf,
                    Properties=GetHelperWordsSfProps()
                },
                new HelperWordTable
                {
                    MainHeader = "Название продукта",
                    Width = widthSf
                },
                new HelperWordTable
                {
                    MainHeader = "Цена продажи",
                    Width = widthSf
                },
                new HelperWordTable
                {
                    MainHeader = "Планируемая цена продажи",
                    Width = widthSf
                },
                new HelperWordTable
                {
                    MainHeader = "Всего продано по продукту",
                    Width = widthSf
                }
            };
            return result;
        }


        int widthFr = 100;
        private List<HelperWordTable> GetHelperWordFr()
        {
            var result = new List<HelperWordTable>
            {
                new HelperWordTable
                {
                    MainHeader = "Номер продукта",
                    Width = widthFr,
                    Properties=GetHelperWordsFrProps()
                },
                new HelperWordTable
                {
                    MainHeader = "Название продукта",
                    Width = widthFr
                },
                new HelperWordTable
                {
                    MainHeader = "Продано кол-во",
                    Width = widthFr
                },
                new HelperWordTable
                {
                    MainHeader = "Планируемая стоимоть",
                    Width = widthFr
                },
                new HelperWordTable
                {
                    MainHeader = "Отклонение от факт себестоимости",
                    Width = widthFr
                },
                new HelperWordTable
                {
                    MainHeader = "Факт цена",
                    Width = widthFr
                }
            };
            return result;
        }

        private List<string> GetHelperWordsFrProps()
        {
            var result = new List<string>
            {
                "ProductId",
                "ProductName",
                "SoldCount",
                "PlannedCostPrice",
                "OtkloneniePlannedCostPrice",
                "FactPrice"
            };
            return result;
        }

        private List<TableColumn> GetColumnsTl()
        {
            var columns = new List<TableColumn>
            {
                new TableColumn{Width=widthTl, Name="Дебет",PropertyName="Debet"},
                new TableColumn{Width=widthTl, Name="Субконто дебет",PropertyName="SubkontoDebet"},
                new TableColumn{Width=widthTl, Name="Кредит",PropertyName="Credit"},
                new TableColumn{Width=widthTl, Name="Субконто кредит",PropertyName="SubkontoCredit"},
                new TableColumn{Width=widthTl, Name="Количество",PropertyName="Count"},
                new TableColumn{ Width=widthTl,Name="Сумма",PropertyName="Sum"},
                new TableColumn{ Width=widthTl,Name="Продукт",PropertyName="Product"}
            };
            return columns;
        }

        int widthTl = 60;
        private List<HelperWordTable> GetHelperWordsTl()
        {
            var result = new List<HelperWordTable>
            {
                new HelperWordTable
                {
                    MainHeader = "Дата",
                    Width = widthTl,
                    Properties=GetHelperWordsTLProps()
                },
                new HelperWordTable
                {
                    MainHeader = "Дебет",
                    Width = widthTl
                },
                new HelperWordTable
                {
                    MainHeader = "Субконто по дебету",
                    Width = widthTl
                },
                new HelperWordTable
                {
                    MainHeader = "Кредит",
                    Width = widthTl
                },
                new HelperWordTable
                {
                    MainHeader = "Субконто по кредету",
                    Width = widthTl
                },
                new HelperWordTable
                {
                    MainHeader="Сумма",
                    Width=widthTl
                },
                new HelperWordTable
                {
                    MainHeader = "Количество",
                    Width = widthTl
                },
                new HelperWordTable
                {
                    MainHeader = "Продукция",
                    Width = widthTl
                }
            };
            return result;
        }

        private List<string> GetHelperWordsTLProps()
        {
            var result = new List<string>
            {
                "TransactionDate",
                "Debet",
                "SubkontoDebet",
                "Credit",
                "SubkontoCredit",
                "Count",
                "Sum",
                "Product"
            };
            return result;
        }

        private List<TableColumn> GetColumnsFr()
        {
            var columns = new List<TableColumn>
            {
                new TableColumn{Width=widthFr, Name="Название продукта",PropertyName="ProductName"},
                new TableColumn{Width=widthFr, Name="Продано кол-во",PropertyName="SoldCount"},
                new TableColumn{Width=widthFr, Name="Плановая цена продажи",PropertyName="PlannedCostPrice"},
                new TableColumn{Width=widthFr, Name="Отклонение от планируемой стоимости",PropertyName="OtkloneniePlannedCostPrice"},
                new TableColumn{Width=widthFr, Name="Фактическая цена",PropertyName="FactPrice"}
            };
            return columns;
        }

        private List<TableColumn> GetColumnsSf()
        {
            var columns = new List<TableColumn>
            {
                new TableColumn{Width=widthSf, Name="Имя продукта",PropertyName="ProductName"},
                new TableColumn{Width=widthSf, Name="Сумма продажи",PropertyName="SoldSum"},
                new TableColumn{Width=widthSf, Name="Сумма продажи по плановой стоимости",PropertyName="SoldInPlannedCostPrice"},
                new TableColumn{Width=widthSf, Name="Всего продано по продукту",PropertyName="TotalSoldByProduct"}
            };
            return columns;
        }

        private TableRow[] GetRowsTl()
        {
            var rows = new TableRow[] { new TableRow { Height = 2 }, new TableRow { Height = 80 } };
            return rows;
        }

        private void buttonAnalyze_Click(object sender, EventArgs e)
        {
            var gistogramDocument = new PdfGisto();
            var legend = LocationLegend.BottomCenter;

            var data = OperationLogStorage.GetAll().Where(rec => rec.Type.Equals("Реализация готовой продукции") && rec.ProductId != null)
                .GroupBy(rec => rec.Product.Name).ToDictionary(recK => TranslitService.Tr2(recK.Key), recV => new float[] { (float)recV.Sum(recS => recS.Sum) });
            var gistInfo = new GistInfo
            {
                Data = data
            };

            gistogramDocument.CreateGist($"D:/gisto{Guid.NewGuid()}.pdf", "Отчет по продажам", "Gist", legend, gistInfo);
        }
    }
}
