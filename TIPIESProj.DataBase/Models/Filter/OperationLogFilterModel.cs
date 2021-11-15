using System;
using System.Collections.Generic;
using System.Text;

namespace TIPIESProj.DataBase.Models.Filter
{
    public class OperationLogFilterModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string OperationType { get; set; }
    }
}
