using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("TESTEntities")
        {
            
        }

        public DbSet<spr_kontr> kontrs { get; set; }

        public DbSet<spr_agent_kontr> a_kontrs { get; set; }
}
}
