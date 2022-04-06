using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Data.Entities;

namespace Weather.Data.Configurations;

public class WeatherForecastEntityConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Summary).HasMaxLength(255);
    }
}