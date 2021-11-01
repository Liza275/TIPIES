using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIPIESProj.DataBase.Models;

namespace TIPIESProj.DataBase.Services
{
    public class ProductStorage
    {
        private static readonly Mapper _mapper = MapperConfig.GetMapper<Product>();

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
                    elem = _mapper.Map<Product>(model);
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
    }
}
