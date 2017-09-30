using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrimonial.context;
using System.Reflection;

namespace Matrimonial.Data
{
    //public class TemporaryDbContextFactory : IDbContextFactory<DatabaseContext>
    //{
    //    public DatabaseContext Create(DbContextFactoryOptions options)
    //    {
    //        var builder = new DbContextOptionsBuilder<DatabaseContext>();
    //        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=efmigrations2017;Trusted_Connection=True;MultipleActiveResultSets=true",
    //            optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DatabaseContext).GetTypeInfo().Assembly.GetName().Name));
    //        return new DatabaseContext(builder.Options);
    //    }
    //}
}
