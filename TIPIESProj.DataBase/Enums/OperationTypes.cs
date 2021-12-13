using System;
using System.Collections.Generic;
using System.Text;

namespace TIPIESProj.DataBase.Enums
{
    public enum OperationTypes
    {
        Postyp,
        Raspr,
        Realization,
        SpisanieOtlonenii,
        Fact
    }

    public class OperationsHelper
    {
        public static List<string> GetTypeList()
        {
            return new List<string>
            {
                "Поступления готовой продукции",
                "Распределение фактической себестоимости по выпущенной продукции",
                "Реализация готовой продукции",
                "Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи",
                "Накопление фактических коммерческих расходов за месяц"
            };
        }
    }
}
