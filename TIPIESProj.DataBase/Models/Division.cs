using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIPIESProj.DataBase.Models
{
    public class Division
    {
        public int Id { get; set; }

        public int ChartOfAccountsId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ChartOfAccounts ChartOfAccounts { get; set; }

        [ForeignKey("DivisionId")]
        public virtual List<OperationLog> OperationLogs { get; set; }
    }
}
