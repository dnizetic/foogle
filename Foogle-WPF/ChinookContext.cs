using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Foogle_WPF
{
    class ChinookContext : DbContext
    {
        //public DbSet<Artist> Artists { get; set; }
        //public DbSet<Album> Albums { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map to the correct Chinook Database tables
            //modelBuilder.Entity<Artist>().ToTable("Artist", "public");
            //modelBuilder.Entity<Album>().ToTable("Album", "public");
            modelBuilder.Entity<Skill>().ToTable("skill", "public");

            // Chinook Database for PostgreSQL doesn't auto-increment Ids
            modelBuilder.Conventions
                .Remove<StoreGeneratedIdentityKeyConvention>();
        }
    }
}
