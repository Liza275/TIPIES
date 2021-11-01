using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIPIESProj.DataBase.Models;

namespace TIPIESProj.DataBase.Services
{
    public class BuyerStorage
    {
        private static readonly Mapper _mapper = MapperConfig.GetMapper<Buyer>();

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
                    elem = _mapper.Map<Buyer>(model);
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
    }
}
