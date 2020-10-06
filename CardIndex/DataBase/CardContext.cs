using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardIndex.DataBase
{
    class CardContext : DbContext
    {
        public CardContext()
            : base("DbConnection")
        {

        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
