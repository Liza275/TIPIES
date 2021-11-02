using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services.Mappers;
using TIPIESProj.DataBase.ViewModels;

namespace TIPIESProj.DataBase.Services
{
    public class DivisionsStorage
    {
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
                    new MapperConfig().GetMapper<Division>().Map<Division, Division>(model, elem);
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

        public static List<DivisionViewModel> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.Divisions.Select(CreateModel).ToList();
            }
        }

        public static List<DivisionViewModel> GetFiltered(int charOfAccountsId)
        {
            using (var db = new ChartDB())
            {
                if (charOfAccountsId == -1)
                    return GetAll();

                return db.Divisions.Where(rec => rec.ChartOfAccountsId == charOfAccountsId).Select(CreateModel).ToList();
            }
        }

        private static DivisionViewModel CreateModel(Division div)
        {
            return new DivisionViewModel
            {
                Id = div.Id,
                Name = div.Name,
                ChartOfAccountsId = div.ChartOfAccountsId,
                CharOfAccountsName = CharOfAccountsStorage.Get(div.ChartOfAccountsId)?.Name
            };
        }
    }
}
