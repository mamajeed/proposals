using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Matrimonial.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Matrimonial.context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole, string>
    {
        
        public string ConnectionString { get; set; }
        // IdentityDbContext<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole, string>
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }

       

        //public DatabaseContext(IOptions<AppSettings> appSettings) 
        //{

        //    ConnectionString = appSettings.Value.ConnectionString;

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConnectionString);

        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ProductMap();
          base.OnModelCreating(modelBuilder);
            new ModelMap(modelBuilder.Entity<RegisterUser>());

            modelBuilder.Entity<RegisterUser>().ToTable("RegisterUser");
            


        }


        public DbSet<RegisterUser> RegisterUser { get; set; }
        
    }
}
