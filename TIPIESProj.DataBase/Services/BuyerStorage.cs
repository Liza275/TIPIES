using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services.Mappers;

namespace TIPIESProj.DataBase.Services
{
    public class BuyerStorage
    {        
        public static void Add(Buyer model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Buyers.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem == null)
                {
                    db.Buyers.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public static void Update(Buyer model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Buyers.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
                    var id = elem.Id;
                    new MapperConfig().GetMapper<Buyer>().Map<Buyer, Buyer>(model, elem);
                    elem.Id = id;

                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Buyers.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    db.Buyers.Remove(elem);
                    db.SaveChanges();
                }
            }
        }

        public static Buyer Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.Buyers.FirstOrDefault(rec => rec.Id == id);
            }
        }

        public static List<Buyer> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.Buyers.ToList();
            }
        }
    }
}
