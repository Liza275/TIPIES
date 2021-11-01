using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services.Mappers;

namespace TIPIESProj.DataBase.Services
{
    public class CharOfAccountsStorage
    {
        //private static readonly Mapper _mapper = new MapperConfig().GetMapper<ChartOfAccounts>();

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
                    elem = new MapperConfig().GetMapper<ChartOfAccounts>().Map<ChartOfAccounts>(model);
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

        public static List<ChartOfAccounts> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.ChartOfAccounts.ToList();
            }
        }
    }
}
