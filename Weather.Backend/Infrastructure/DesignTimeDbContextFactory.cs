using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Weather.Data;

namespace Weather.Backend.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeatherDbContext>
{
    public WeatherDbContext CreateDbContext(string[] args)
    {
        // Load	the	settings from the project which	contains the connection	string
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var builder = new DbContextOptionsBuilder<WeatherDbContext>();

        builder.UseSqlServer(configuration.GetConnectionString("WeatherConnection"));
        return new WeatherDbContext(builder.Options);
    }
}