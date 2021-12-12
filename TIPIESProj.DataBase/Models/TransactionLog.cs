using System;

namespace TIPIESProj.DataBase.Models
{
    public class TransactionLog
    {
        public int Id { get; set; }

        public int OperationLogId { get; set; }

        public DateTime Data { get; set; }

        public string SudKontoD1 { get; set; }

        public string SubKontoK1 { get; set; }

        public int? DebetId { get; set; }

        public ChartOfAccounts Debet { get; set; }

        public int? CreditId { get; set; }

        public ChartOfAccounts Credit { get; set; }

        public int? ProductId { get; set; }

        public Product Product { get; set; }

        public int? DivisionId { get; set; }

        public Division Division { get; set; }

        public int? Count { get; set; }

        public decimal Sum { get; set; }

        public virtual OperationLog OperationLog { get; set; }
    }
}
