using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIPIESProj.DataBase.ViewModels.Report;

namespace TIPIESProj.DataBase.Services
{
    public class ReportDataService
    {
        public static FactRasprFullReportModel GetFactRasprReport(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                return null;
            }

            var result = new FactRasprFullReportModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                DataList = new List<FactRasprReportProductModel>()
            };

            var transactions = TransactionLogStorage.GetAll();
            result.TotalFactZatrat = transactions
                        .Where(rec => rec.Debet.AccountNumber == 20 &&
                            rec.Data >= dateFrom && rec.Data <= dateTo)
                        .Sum(rec => rec.Sum);

            foreach (var el in ProductStorage.GetAll())
            {
                var postyp = OperationLogStorage.GetAll().Where(rec => rec.Type.Equals("Поступления готовой продукции") &&
                    rec.Data >= dateFrom && rec.Data <= dateTo && rec.ProductId == el.Id);

                if (!postyp.Any())
                    continue;

                var productCount = postyp.Sum(rec => rec.Count);
                var productSum = postyp.Sum(rec => rec.Sum);

                var kolfi = (int)productCount;
                var sumfi = productSum;

                var realizTrans = transactions
                .Where(rec => rec.Debet.AccountNumber == 90 && rec.Credit.AccountNumber == 43 && rec.ProductId == el.Id &&
                    rec.Data >= dateFrom && rec.Data <= dateTo);

                if (!realizTrans.Any())
                    continue;

                var kolpi = (int)realizTrans.Sum(rec => rec.Count);
                var sumpi = realizTrans.Sum(rec => rec.Sum);

                var sum = ((sumfi / kolfi) - (sumpi / kolpi)) * kolfi;

                result.DataList.Add(new FactRasprReportProductModel
                {
                    ProductId = el.Id,
                    ProductName = el.Name,
                    SoldCount = productCount,
                    PlannedCostPrice = el.PlannedCostPrice,
                    //PlannedCostPrice = sumpi / kolpi,
                    //By formula
                    OtkloneniePlannedCostPrice = sum,
                    FactPrice = sumfi / kolfi
                });
            }

            result.TotalCount = result.DataList.Sum(rec => rec.SoldCount);
            result.TotalPlannedPrice = result.DataList.Sum(rec => rec.PlannedCostPrice);
            result.TotalOtkloneniePlannedCostPrice = result.DataList.Sum(rec => rec.OtkloneniePlannedCostPrice);
            result.TotalFactPrice = result.DataList.Sum(rec => rec.FactPrice);

            return result;
        }

        public static SoldFullReportModel GetSoldReport(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                return null;
            }

            var result = new SoldFullReportModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                DataList = new List<SoldReportProductModel>()
            };

            foreach (var el in ProductStorage.GetAll())
            {
                var realiz = OperationLogStorage.GetAll().Where(rec => rec.Type.Equals("Реализация готовой продукции") &&
                    rec.Data >= dateFrom && rec.Data <= dateTo && rec.ProductId == el.Id).Sum(rec => rec.Sum);

                var realizInPlanned = OperationLogStorage.GetAll().Where(rec => rec.Type.Equals("Реализация готовой продукции") &&
                    rec.Data >= dateFrom && rec.Data <= dateTo && rec.ProductId == el.Id).Sum(rec => rec.Count) * el.PlannedCostPrice;

                result.DataList.Add(new SoldReportProductModel
                {
                    ProductId = el.Id,
                    ProductName = el.Name,
                    SoldSum = realiz,
                    SoldInPlannedCostPrice = realizInPlanned
                });
            }

            result.TotalSoldSum = result.DataList.Sum(rec => rec.SoldSum);
            result.TotalSoldInPlannedCostPrice = result.DataList.Sum(rec => rec.SoldInPlannedCostPrice);

            return result;
        }

        public static FactRasprFullReportModel GetOtrlReport(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                return null;
            }

            var result = new FactRasprFullReportModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                DataList = new List<FactRasprReportProductModel>()
            };

            var transactions = TransactionLogStorage.GetAll();
            result.TotalFactZatrat = transactions
                        .Where(rec => rec.Debet.AccountNumber == 20 &&
                            rec.Data >= dateFrom && rec.Data <= dateTo)
                        .Sum(rec => rec.Sum);

            foreach (var el in ProductStorage.GetAll())
            {
                var postyp = OperationLogStorage.GetAll().Where(rec => rec.Type.Equals("Поступления готовой продукции") &&
                    rec.Data >= dateFrom && rec.Data <= dateTo && rec.ProductId == el.Id);

                var realiz = OperationLogStorage.GetAll().Where(rec => rec.Type.Equals("Реализация готовой продукции") &&
                    rec.Data >= dateFrom && rec.Data <= dateTo && rec.ProductId == el.Id);

                if (!realiz.Any())
                    continue;

                var productCount = postyp.Sum(rec => rec.Count);
                var productSum = postyp.Sum(rec => rec.Sum);

                var kolfi = (int)productCount;
                var sumfi = productSum;

                var realizTrans = transactions
                .Where(rec => rec.Debet.AccountNumber == 90 && rec.Credit.AccountNumber == 43 && rec.ProductId == el.Id &&
                    rec.Data >= dateFrom && rec.Data <= dateTo);

                if (!realizTrans.Any())
                    continue;

                var kolpi = (int)realizTrans.Sum(rec => rec.Count);
                var sumpi = realizTrans.Sum(rec => rec.Sum);

                var sum = ((sumfi / kolfi) - (sumpi / kolpi)) * kolfi;

                result.DataList.Add(new FactRasprReportProductModel
                {
                    ProductId = el.Id,
                    ProductName = el.Name,
                    SoldCount = realiz.Sum(rec => rec.Count),
                    //PlannedCostPrice = el.PlannedCostPrice,
                    PlannedCostPrice = sumpi / kolpi,
                    //By formula
                    OtkloneniePlannedCostPrice = sum,
                    FactPrice = sumfi / kolfi
                });
            }

            result.TotalCount = result.DataList.Sum(rec => rec.SoldCount);
            result.TotalPlannedPrice = result.DataList.Sum(rec => rec.PlannedCostPrice);
            result.TotalOtkloneniePlannedCostPrice = result.DataList.Sum(rec => rec.OtkloneniePlannedCostPrice);
            result.TotalFactPrice = result.DataList.Sum(rec => rec.FactPrice);

            return result;
        }
    }
}
