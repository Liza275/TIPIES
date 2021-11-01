using System.ComponentModel.DataAnnotations;

namespace TIPIESProj.DataBase.Models
{
    public class Division
    {
        public int Id { get; set; }

        public int ChartOfAccountsId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ChartOfAccounts ChartOfAccounts { get; set; }
    }
}
