using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TIPIESProj.DataBase.Models
{
    public class ChartOfAccounts
    {
        public int Id { get; set; }

        [Required]
        public float AccountNumber { get; set; }

        [Required]
        public string Name { get; set; }

        public string SudKonto1 { get; set; }

        [InverseProperty("Debet")]
        public virtual List<TransactionLog> DebetsTransactionLogs { get; set; }

        [InverseProperty("Credit")]
        public virtual List<TransactionLog> CreditTransactionLogs { get; set; }


        [ForeignKey("DivisionId")]
        public virtual List<Division> Divisions { get; set; }
    }
}
