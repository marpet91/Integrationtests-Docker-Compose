using Microsoft.EntityFrameworkCore;
using Weather.Data.Entities;

namespace Weather.Data;

public class WeatherDbContext : DbContext
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}