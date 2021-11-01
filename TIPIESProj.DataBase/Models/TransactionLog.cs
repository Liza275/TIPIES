using System;
using System.Collections.Generic;
using System.Text;

namespace TIPIESProj.DataBase.Models
{
    public class TransactionLog
    {
        public int Id { get; set; }

        public int OperationLogId { get; set; }

        public DateTime Data { get; set; }

        public string SudKontoD1 { get; set; }

        public string SubKontoK1 { get; set; }

        public int DebetId { get; set; }

        public virtual ChartOfAccounts Debet { get; set; }

        public int CreditId { get; set; }

        public virtual ChartOfAccounts Credit { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public virtual OperationLog OperationLog { get; set; }
    }
}
