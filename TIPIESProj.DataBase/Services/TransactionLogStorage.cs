using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services.Mappers;

namespace TIPIESProj.DataBase.Services
{
    public class TransactionLogStorage
    {
        private static readonly Mapper _mapper = new MapperConfig().GetMapper<TransactionLog>();

        public static void Add(TransactionLog model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.TransactionLogs.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem == null)
                {
                    db.TransactionLogs.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public static void Update(TransactionLog model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.TransactionLogs.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
                    var id = elem.Id;
                    elem = _mapper.Map<TransactionLog>(model);
                    elem.Id = id;

                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.TransactionLogs.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    db.TransactionLogs.Remove(elem);
                    db.SaveChanges();
                }
            }
        }

        public static TransactionLog Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.TransactionLogs.FirstOrDefault(rec => rec.Id == id);
            }
        }

        public static List<TransactionLog> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.TransactionLogs.ToList();
            }
        }
    }
}
