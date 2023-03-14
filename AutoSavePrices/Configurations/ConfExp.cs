using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices.Configurations
{
    public static class ConfExp
    {


        private static readonly string path = Directory.GetCurrentDirectory() + @"\generated" + @"\";

        private static readonly string extFormat = ".xlsx";

        public static string GetFullPath(string NameFile)
        {
            return path + NameFile + extFormat;
        }


        public static readonly List<string> NameCols = new List<string>()
        {
            "Код товара",
            "Наименование",
            "Артикул",
            "Цена",
            "РРЦ",
            "Кол-во",
            "Бренд"
        };

        public static readonly List<string> NameColsDataTable = new List<string>()
        {
            "idtov",
            "n_tov",
            "id_tov_oem_short",
            "price",
            "pricerrc",
            "resttov",
            "tm_name"
        };

    }
}
