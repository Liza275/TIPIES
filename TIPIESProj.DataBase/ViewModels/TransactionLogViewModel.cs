using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TIPIESProj.DataBase.ViewModels
{
    public class TransactionLogViewModel
    {
        public int Id { get; set; }

        [DisplayName("Дата")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("Дебет")]
        public float Debet { get; set; }

        [DisplayName("Субконто по дебету")]
        public string SubkontoDebet { get; set; }

        [DisplayName("Кредит")]
        public float Credit { get; set; }

        [DisplayName("Субконто по кредету")]
        public string SubkontoCredit { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        public int? ProductId { get; set; }

        [DisplayName("Продукция")]
        public string Product { get; set; }

        public int? OperationId { get; set; }
    }
}
