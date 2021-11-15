using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIPIESProj.DataBase.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal PlannedCostPrice { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<OperationLog> OperationLogs { get; set; }
    }
}
