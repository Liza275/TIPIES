using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TIPIESProj.DataBase.Models;
using TIPIESProj.DataBase.Services.Mappers;
using TIPIESProj.DataBase.ViewModels;

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
                return db.TransactionLogs.Include(rec => rec.Product).Include(rec => rec.Division).Include(rec => rec.Debet)
                    .Include(rec => rec.Credit).Include(rec => rec.OperationLog).FirstOrDefault(rec => rec.Id == id);
            }
        }

        public static List<TransactionLog> GetAll()
        {
            using (var db = new ChartDB())
            {
                return db.TransactionLogs.Include(rec => rec.Product).Include(rec => rec.Division).Include(rec => rec.Debet)
                    .Include(rec => rec.Credit).Include(rec => rec.OperationLog).ToList();
            }
        }

        public static List<TransactionLogViewModel> GetAllViewModels()
        {
            using (var db = new ChartDB())
            {
                return db.TransactionLogs.Include(rec => rec.Product).Include(rec => rec.Division).Include(rec => rec.Debet)
                    .Include(rec => rec.Credit).Include(rec => rec.OperationLog).Select(CreateModel).ToList();
            }
        }

        public static void DeleteTransactions(int logId)
        {
            foreach (var el in GetAll().Where(rec => rec.OperationLogId == logId).Select(rec => rec.Id))
            {
                Delete(el);
            }
        }

        private static TransactionLogViewModel CreateModel(TransactionLog transactionLog)
        {
            return new TransactionLogViewModel
            {
                Id = transactionLog.Id,
                TransactionDate = transactionLog.Data,
                SubkontoDebet = transactionLog.SudKontoD1,
                SubkontoCredit = transactionLog.SubKontoK1,
                Count = transactionLog.Count == null || transactionLog.Count == 0 ? 1 : transactionLog.Count,
                Sum = transactionLog.Sum,
                Debet = transactionLog.Debet.AccountNumber,
                Credit = transactionLog.Credit.AccountNumber,
                ProductId = transactionLog?.Id,
                Product = transactionLog.Product?.Name,
                OperationId = transactionLog.OperationLogId
            };
        }
    }
}
