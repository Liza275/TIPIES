using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIPIESProj.DataBase.Models;

namespace TIPIESProj.DataBase.Services
{
    public class DivisionsStorage
    {
        private static readonly Mapper _mapper = MapperConfig.GetMapper<Division>();

        public static void Add(Division model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Divisions.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem == null)
                {
                    db.Divisions.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public static void Update(Division model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Divisions.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
                    var id = elem.Id;
                    elem = _mapper.Map<Division>(model);
                    elem.Id = id;

                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.Divisions.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    db.Divisions.Remove(elem);
                    db.SaveChanges();
                }
            }
        }

        public static Division Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.Divisions.FirstOrDefault(rec => rec.Id == id);
            }
        }
    }
}
