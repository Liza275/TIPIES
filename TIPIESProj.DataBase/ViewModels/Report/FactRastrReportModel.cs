using System;
using System.Collections.Generic;
using System.Text;

namespace TIPIESProj.DataBase.ViewModels.Report
{
    public class FactRasprReportProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int SoldCount { get; set; }

        public decimal PlannedCostPrice { get; set; }

        public decimal OtkloneniePlannedCostPrice { get; set; }

        public decimal FactPrice { get; set; }
    }

    public class FactRasprFullReportModel
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public decimal TotalFactZatrat { get; set; }

        public List<FactRasprReportProductModel> DataList { get; set; }

        public int TotalCount { get; set; }

        public decimal TotalPlannedPrice { get; set; }

        public decimal TotalOtkloneniePlannedCostPrice { get; set; }

        public decimal TotalFactPrice { get; set; }
    }
}
