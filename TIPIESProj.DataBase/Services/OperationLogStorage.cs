using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;

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
                    var id = elem.Id;
                    //elem = _mapper.Map<OperationLog>(model);
                    elem.Id = id;

                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.OperationLogs.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    db.OperationLogs.Remove(elem);
                    db.SaveChanges();
                }
            }
        }

        public static OperationLog Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.OperationLogs.FirstOrDefault(rec => rec.Id == id);
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
