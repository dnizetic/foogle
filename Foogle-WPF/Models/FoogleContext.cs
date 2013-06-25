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
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map to the correct Chinook Database tables

            modelBuilder.Entity<FoogleUser>().ToTable("foogle_user", "public");
            modelBuilder.Entity<Category>().ToTable("category", "public");
            modelBuilder.Entity<Skill>().ToTable("skill", "public");
            modelBuilder.Entity<Recommendation>().ToTable("recommendation", "public");
            modelBuilder.Entity<UserSkills>().ToTable("userSkillsMenuItem", "public");
            
            // Chinook Database for PostgreSQL doesn't auto-increment Ids
            //modelBuilder.Conventions
            //    .Remove<StoreGeneratedIdentityKeyConvention>();
        }
    }
}
