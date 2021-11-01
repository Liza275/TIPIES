using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public List<TransactionLog> Debets { get; set; }

        [InverseProperty("Credit")]
        public List<TransactionLog> Credits { get; set; }

        [ForeignKey("DivisionId")]
        public virtual List<Division> Divisions { get; set; }
    }
}
