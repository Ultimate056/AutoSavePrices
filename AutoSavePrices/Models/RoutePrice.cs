using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices.Models
{
    public class RoutePrice
    {
        public string Category { get; set; }

        public string IdClient { get; set; }

        /// <summary>
        /// Полный путь до файла прайсов
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Путь до папки, в которой лежат прайсы
        /// </summary>
        public string PathDirectory { get; set; }

        /// <summary>
        /// Имя файла для отправки на email
        /// </summary>
        public string NameFile { get; set; }
    }
}
