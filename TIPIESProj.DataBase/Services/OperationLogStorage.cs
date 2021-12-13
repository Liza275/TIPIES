using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Enums;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Models.Filter;

namespace TIPIESProj.DataBase.Services
{
    public class OperationLogStorage
    {
        public static int LastAddedId;

        public static string Add(OperationLog model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.OperationLogs.FirstOrDefault(rec => rec.Id == model.Id);

                var result = CanAdd(model);
                if (!string.IsNullOrEmpty(result))
                    return result;

                if (elem == null)
                {
                    db.OperationLogs.Add(model);
                    db.SaveChanges();
                    LastAddedId = model.Id;
                }
            }
            return string.Empty;
        }

        //Если не было поступления, то не можем сделать остальные операции
        private static string CanAdd(OperationLog ol)
        {
            if (ol.Type.Equals("Накопление фактических коммерческих расходов за месяц"))
            {
                return string.Empty;
            }

            var all = GetAll();
            if (!ol.Type.Equals(GetTypeString(OperationTypes.Postyp))
                && all.FirstOrDefault(rec => IsDate(ol.Data, rec.Data) && rec.Type.Equals(GetTypeString(OperationTypes.Postyp))) == null)
            {
                return "Если не было поступления, то не можем сделать остальные операции";
            }

            if (ol.Type.Equals(GetTypeString(OperationTypes.SpisanieOtlonenii)) &&
                all.FirstOrDefault(rec => rec.Data.Month == ol.Data.Month && rec.Data.Year == ol.Data.Year && rec.Type.Equals(GetTypeString(OperationTypes.SpisanieOtlonenii))) != null)
            {
                return "Списание уже было";
            }

            //if (ol.Type.Equals(GetTypeString(OperationTypes.Raspr)) &&
            //    all.FirstOrDefault(rec => rec.Data.Month == ol.Data.Month && rec.Data.Year == ol.Data.Year && rec.Type.Equals(GetTypeString(OperationTypes.Raspr))) != null)
            //{
            //    return "Распределние уже было";
            //}

            return string.Empty;
        }

        private static bool IsDate(DateTime ol, DateTime rec)
        {
            if (ol.Month != rec.Month)
                return false;
            if (ol.Year != rec.Year)
                return false;
            if (ol.Day < rec.Day)
                return false;
            if (ol.Day == rec.Day)
            {
                if (ol.Hour < rec.Hour)
                    return false;
                if (ol.Hour == rec.Hour)
                {
                    if (ol.Minute < rec.Minute)
                        return false;
                    if (ol.Minute == rec.Minute)
                    {
                        if (ol.Second < rec.Second)
                            return false;
                        if (ol.Second == rec.Second)
                        {
                            if (ol.Millisecond < rec.Millisecond)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public static string Update(OperationLog model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.OperationLogs.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
                    var result = CanUpdate(model);
                    if (!string.IsNullOrEmpty(result))
                        return result;

                    TransactionLogStorage.DeleteTransactions(model.Id);
                    elem.Type = model.Type;
                    elem.Data = model.Data;
                    elem.ProductId = model.ProductId;
                    elem.Division = model.Division;
                    elem.Sum = model.Sum;
                    elem.Count = model.Count;
                    elem.BuyerId = model.BuyerId;

                    db.SaveChanges();
                }
            }
            return string.Empty;
        }

        private static string CanUpdate(OperationLog ol)
        {
            var all = GetAll();
            //Если была реализация, то мы не можем изменить поступление и распределение
            if (ol.Type.Equals(GetTypeString(OperationTypes.Postyp)) || ol.Type.Equals(GetTypeString(OperationTypes.Raspr))
                || ol.Type.Equals(GetTypeString(OperationTypes.Fact)))
            {
                if (all.FirstOrDefault(rec => DataCheck(ol.Data, rec.Data) && rec.Type.Equals(GetTypeString(OperationTypes.Realization))) != null)
                {
                    return "Если была реализация, то мы не можем изменить поступление, распределение и факт.";
                }
            }

            //Если было распределение, то изменить поступление нельзя
            if ((ol.Type.Equals(GetTypeString(OperationTypes.Postyp))|| ol.Type.Equals(GetTypeString(OperationTypes.Fact))) &&
                all.FirstOrDefault(rec => DataCheck(ol.Data, rec.Data) && rec.Type.Equals(GetTypeString(OperationTypes.Raspr))) != null)
            {
                return "Если было распределение, то изменить поступление или факт. нельзя";
            }

            //Если было списание, то поступление, реализация и распределение редактировать нельзя
            if (!ol.Type.Equals(GetTypeString(OperationTypes.SpisanieOtlonenii)) &&
               all.FirstOrDefault(rec => DataCheck(ol.Data, rec.Data) && rec.Type.Equals(GetTypeString(OperationTypes.SpisanieOtlonenii))) != null)
            {
                return "Если было списание, то поступление, факт., реализация и распределение редактировать нельзя";
            }

            return string.Empty;
        }

        public static string Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.OperationLogs.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    var result = CanDelete(elem);
                    if (!string.IsNullOrEmpty(result))
                        return result;

                    if (elem.Type.Equals("Поступления готовой продукции"))
                    {
                        foreach (var el in GetAll().Where(rec => rec.Data >= elem.Data && !rec.Type.Equals(elem.Type)))
                        {
                            Delete(el.Id);
                        }
                    }

                    TransactionLogStorage.DeleteTransactions(id);
                    db.OperationLogs.Remove(elem);
                    db.SaveChanges();
                }
                return string.Empty;
            }
        }

        private static string CanDelete(OperationLog ol)
        {
            var all = GetAll();
            //Если было распределение, то поступление и факт удалять нельзя
            if ((ol.Type.Equals(GetTypeString(OperationTypes.Postyp))|| ol.Type.Equals(GetTypeString(OperationTypes.Fact))) &&
                all.FirstOrDefault(rec => DataCheck(ol.Data, rec.Data) && rec.Type.Equals(GetTypeString(OperationTypes.Raspr))) != null)
            {
                return "Если было распределение, то поступление и факт. удалять нельзя";
            }
            //Если было списание, то реализацию, поступление и распределение удалять нельзя
            if (!ol.Type.Equals(GetTypeString(OperationTypes.SpisanieOtlonenii)) &&
                all.FirstOrDefault(rec => DataCheck(ol.Data, rec.Data) && rec.Type.Equals(GetTypeString(OperationTypes.SpisanieOtlonenii))) != null)
            {
                return "Если было списание, то реализацию, факт., поступление и распределение удалять нельзя";
            }

            return string.Empty;
        }

        private static bool DataCheck(DateTime candidate, DateTime rec)
        {
            if (rec.Month == candidate.Month && rec.Year == candidate.Year)
            {
                return true;
            }
            return false;
        }


        public static OperationLog Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.OperationLogs.Include(rec => rec.Division)
                    .Include(rec => rec.Product)
                    .Include(rec => rec.Buyer)
                    .Include(rec => rec.TransactionLog)
                    .FirstOrDefault(rec => rec.Id == id);
            }
        }

        public static List<OperationLog> GetFiltered(OperationLogFilterModel filter)
        {
            using (var db = new ChartDB())
            {
                var query = db.OperationLogs.Include(rec => rec.Division)
                    .Include(rec => rec.Product)
                    .Include(rec => rec.TransactionLog)
                    .Include(rec => rec.Buyer).AsQueryable();

                if (filter.DateFrom.HasValue)
                    query = query.Where(rec => rec.Data >= filter.DateFrom.Value);

                if (filter.DateTo.HasValue)
                    query = query.Where(rec => rec.Data <= filter.DateTo.Value);

                if (!string.IsNullOrEmpty(filter.OperationType))
                    query = query.Where(rec => rec.Type.Equals(filter.OperationType));

                return query.ToList();
            }
        }

        public static List<OperationLogViewModel> GetFilteredView(OperationLogFilterModel filter)
        {
            using (var db = new ChartDB())
            {
                var query = db.OperationLogs.Include(rec => rec.Division)
                    .Include(rec => rec.Product)
                    .Include(rec => rec.TransactionLog)
                    .Include(rec => rec.Buyer).AsQueryable();

                if (filter.DateFrom.HasValue)
                    query = query.Where(rec => rec.Data >= filter.DateFrom.Value);

                if (filter.DateTo.HasValue)
                    query = query.Where(rec => rec.Data <= filter.DateTo.Value);

                if (!string.IsNullOrEmpty(filter.OperationType))
                    query = query.Where(rec => rec.Type.Equals(filter.OperationType));

                return query.Select(CreateModel).ToList();
            }
        }

        public static List<OperationLog> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.OperationLogs.Include(rec => rec.Division)
                    .Include(rec => rec.Product)
                    .Include(rec => rec.TransactionLog)
                    .Include(rec => rec.Buyer).ToList();
            }
        }

        public static List<OperationLogViewModel> GetAllView()
        {
            using (var db = new ChartDB())
            {
                return db.OperationLogs.Include(rec => rec.Division)
                    .Include(rec => rec.Product)
                    .Include(rec => rec.TransactionLog)
                    .Include(rec => rec.Buyer).Select(CreateModel).ToList();
            }
        }

        private static string GetTypeString(OperationTypes ot)
        {
            switch (ot)
            {
                case OperationTypes.Postyp:
                    return "Поступления готовой продукции";
                case OperationTypes.Raspr:
                    return "Распределение фактической себестоимости по выпущенной продукции";
                case OperationTypes.Realization:
                    return "Реализация готовой продукции";
                case OperationTypes.Fact:
                    return "Накопление фактических коммерческих расходов за месяц";
                default:
                    return "Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи";
            }
        }

        private static OperationLogViewModel CreateModel(OperationLog ol)
        {
            var product = ProductStorage.Get(ol.ProductId ?? -1)?.Name;
            var buyer = BuyerStorage.Get(ol.BuyerId ?? -1)?.Fio;
            var division = DivisionsStorage.Get(ol.DivisionId ?? -1)?.Name;

            return new OperationLogViewModel
            {
                Id = ol.Id,
                Type = ol.Type,
                Data = ol.Data,
                Count = ol.Count,
                Sum = ol.Sum,
                Division = division,
                Product = product,
                Buyer = buyer
            };
        }
    }


    public class OperationLogViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime Data { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Division { get; set; }

        public string Product { get; set; }

        public string Buyer { get; set; }
    }
}
