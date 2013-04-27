using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Foogle_WPF
{
    //http://brice-lambson.blogspot.com/2012/10/entity-framework-on-postgresql.html
    class FoogleContext : DbContext
    {
        //public DbSet<Artist> Artists { get; set; }
        //public DbSet<Album> Albums { get; set; }

        public DbSet<FoogleUser> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map to the correct Chinook Database tables

            modelBuilder.Entity<FoogleUser>().ToTable("foogle_user", "public");
            modelBuilder.Entity<Category>().ToTable("category", "public");

            // Chinook Database for PostgreSQL doesn't auto-increment Ids
            //modelBuilder.Conventions
            //    .Remove<StoreGeneratedIdentityKeyConvention>();
        }
    }
}
