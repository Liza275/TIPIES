using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services.Mappers;

namespace TIPIESProj.DataBase.Services
{
    public class ProductStorage
    {
        public static void Add(Product model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Products.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem == null)
                {
                    db.Products.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public static void Update(Product model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Products.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
                    var id = elem.Id;
                    new MapperConfig().GetMapper<Product>().Map<Product, Product>(model, elem);
                    elem.Id = id;

                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Products.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    db.Products.Remove(elem);
                    db.SaveChanges();
                }
            }
        }

        public static Product Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.Products.FirstOrDefault(rec => rec.Id == id);
            }
        }

        public static List<Product> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.Products.ToList();
            }
        }
    }
}
