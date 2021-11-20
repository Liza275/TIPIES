using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Models.Filter;

namespace TIPIESProj.DataBase.Services
{
    public class OperationLogStorage
    {
        public static void Add(OperationLog model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.OperationLogs.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem == null)
                {
                    db.OperationLogs.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public static void Update(OperationLog model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.OperationLogs.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
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
        }

        public static string Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.OperationLogs.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    var all = GetAll();
                    if (all.FirstOrDefault(rec =>
                            rec.Data.Month == elem.Data.Month && rec.Data.Year == elem.Data.Year && rec.Equals("Распределение фактической себестоимости по выпущенной продукции")) != null)
                    {
                        return "Невозможно удалить так как существует распределение за указанный месяц";
                    }
                    if (elem.Type.Equals("Поступления готовой продукции"))
                    {
                        foreach (var el in all.Where(rec => rec.Data >= elem.Data && !rec.Type.Equals(elem.Type)))
                        {
                            Delete(el.Id);
                        }
                    }
                    db.OperationLogs.Remove(elem);
                    db.SaveChanges();
                }
                return string.Empty;
            }
        }

        public static OperationLog Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.OperationLogs.FirstOrDefault(rec => rec.Id == id);
            }
        }

        public static List<OperationLog> GetFiltered(OperationLogFilterModel filter)
        {
            using (var db = new ChartDB())
            {
                var query = db.OperationLogs.AsQueryable();

                if (filter.DateFrom.HasValue)
                    query = query.Where(rec => rec.Data >= filter.DateFrom.Value);

                if (filter.DateTo.HasValue)
                    query = query.Where(rec => rec.Data <= filter.DateTo.Value);

                if (!string.IsNullOrEmpty(filter.OperationType))
                    query = query.Where(rec => rec.Type.Equals(filter.OperationType));

                return query.ToList();
            }
        }

        public static List<OperationLog> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.OperationLogs.ToList();
            }
        }
    }
}
