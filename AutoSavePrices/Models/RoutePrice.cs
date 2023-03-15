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

        public string FullPath { get; set; }

        public string PathDirectory { get; set; }

        public string NameFile { get; set; }
    }
}
