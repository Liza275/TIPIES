using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TIPIESProj.DataBase.Models
{
    public class Buyer
    {
        public int Id { get; set; }

        [Required]
        public string Fio { get; set; }

        [Required]
        public string Number { get; set; }

    }
}
