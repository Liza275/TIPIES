using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //[ForeignKey("DivisionId")]
        //public virtual List<Division> Division { get; set; }

        //[ForeignKey("ProductId")]
        //public virtual List<Product> Product { get; set; }

        public int DivisionId { get; set; }

        public Division Division { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int BuyerId { get; set; }

        public Buyer Buyer { get; set; }
    }
}
