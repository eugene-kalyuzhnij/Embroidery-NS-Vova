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
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public ModelContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            base.Configuration.LazyLoadingEnabled = false;

            modelBuilder.Entity<User>().HasMany(c => c.Comments).WithRequired().HasForeignKey(c => c.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(l => l.Likes).WithRequired().HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);

        }
    }
}
