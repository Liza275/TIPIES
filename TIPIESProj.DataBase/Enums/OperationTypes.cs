using System;
using System.Collections.Generic;
using System.Text;

namespace TIPIESProj.DataBase.Enums
{
    public class OperationsHelper
    {
        public static readonly string ReadyProducts = "Поступления готовой продукции";
        public static readonly string RasprFactSebestoimosti = "Распределение фактической себестоимости по выпущенной продукции";
        public static readonly string Realization = "Реализация готовой продукции";
        public static readonly string SpisanieOtlonenii = "Списание отлонений от фактической себестоимости реализованной продукции на расходы от продажи";

        public static List<string> GetTypeList()
        {
            return new List<string>
            {
                ReadyProducts,
                RasprFactSebestoimosti,
                Realization,
                SpisanieOtlonenii
            };
        }
    }
}
