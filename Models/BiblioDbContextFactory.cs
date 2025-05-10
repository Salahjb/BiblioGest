using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BiblioGest.Models
{
    public class BiblioDbContextFactory : IDesignTimeDbContextFactory<BiblioDbContext>
    {
        public BiblioDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BiblioDbContext>();
            var connectionString = "server=localhost;port=3306;database=biblio;user=root";

            optionsBuilder.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString));

            return new BiblioDbContext(optionsBuilder.Options);
        }
    }
}