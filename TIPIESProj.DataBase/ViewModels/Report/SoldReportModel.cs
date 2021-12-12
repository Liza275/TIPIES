using System;
using System.Collections.Generic;
using System.Text;

namespace TIPIESProj.DataBase.ViewModels.Report
{
    public class SoldReportProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal SoldSum { get; set; }

        public decimal SoldInPlannedCostPrice { get; set; }

        public decimal TotalSoldByProduct
        {
            get => SoldSum - SoldInPlannedCostPrice;
        }
    }

    public class SoldFullReportModel
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<SoldReportProductModel> DataList { get; set; }

        public decimal TotalSoldSum { get; set; }

        public decimal TotalSoldInPlannedCostPrice { get; set; }

        public decimal TotalSold
        {
            get => TotalSoldSum - TotalSoldInPlannedCostPrice;
        }
    }
}
