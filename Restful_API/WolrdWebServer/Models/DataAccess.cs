using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace WolrdWebServer.Models
{
    public class WorldDbContext : DbContext
    {
        public WorldDbContext(DbContextOptions<WorldDbContext> options):base(options)
        {}

        public DbSet<Country> Country{get;set;}
        public DbSet<City> City{get;set;}
    }

    public static class WorldDbContextFactory{
        public static WorldDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorldDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            var dbContext = new WorldDbContext(optionsBuilder.Options);
            return dbContext;
        }
    }
}