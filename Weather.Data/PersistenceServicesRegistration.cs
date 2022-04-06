using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Data.Services;

namespace Weather.Data;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WeatherDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("WeatherConnection"));
        });
        services.AddScoped<IWeatherService, WeatherService>();
        return services;
    }
}