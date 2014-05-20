using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NSEmbroidery.Data.Models;
using System.Configuration;

namespace NSEmbroidery.Data.EF
{
    class ModelContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Embroidery> Embroideries { get; set; }

    }
}
