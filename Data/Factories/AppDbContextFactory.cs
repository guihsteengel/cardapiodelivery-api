using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FoodDelivery.Api.Data.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=fooddeliverydb;Username=postgres;Password=postgres"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}