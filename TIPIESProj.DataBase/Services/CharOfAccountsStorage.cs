using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIPIESProj.DataBase.Models;

namespace TIPIESProj.DataBase.Services
{
    public class CharOfAccountsStorage
    {
        private static readonly Mapper _mapper = MapperConfig.GetMapper<ChartOfAccounts>();

        public static void Add(ChartOfAccounts model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.ChartOfAccounts.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem == null)
                {
                    db.ChartOfAccounts.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public static void Update(ChartOfAccounts model)
        {
            using (var db = new ChartDB())
            {
                var elem = db.ChartOfAccounts.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
                    var id = elem.Id;
                    elem = _mapper.Map<ChartOfAccounts>(model);
                    elem.Id = id;

                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ChartDB())
            {
                var elem = db.ChartOfAccounts.FirstOrDefault(rec => rec.Id == id);

                if (elem != null)
                {
                    db.ChartOfAccounts.Remove(elem);
                    db.SaveChanges();
                }
            }
        }

        public static ChartOfAccounts Get(int id)
        {
            using (var db = new ChartDB())
            {
                return db.ChartOfAccounts.FirstOrDefault(rec => rec.Id == id);
            }
        }
    }
}
