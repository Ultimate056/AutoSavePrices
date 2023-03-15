using AutoSavePrices.Models;
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


        private static readonly string main_path = @"C:\FalconTemp";

        private static readonly string extFormat = ".xlsx";

        public static string GetFullPath(RoutePrice p)
        {
            p.PathDirectory = main_path + @"\" + p.Category + @"\" + p.IdClient;
            Directory.CreateDirectory(p.PathDirectory);
            return main_path + @"\" + p.Category + @"\" + p.IdClient + @"\" + p.NameFile + extFormat;
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
