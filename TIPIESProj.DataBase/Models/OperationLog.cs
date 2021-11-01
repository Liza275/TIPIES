using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TIPIESProj.DataBase.Models
{
    public class OperationLog
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [ForeignKey("TransactionLogId")]
        public virtual List<TransactionLog> TransactionLog { get; set; }

        [ForeignKey("DivisionId")]
        public virtual List<Division> Division { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<Product> Product { get; set; }
    }
}
