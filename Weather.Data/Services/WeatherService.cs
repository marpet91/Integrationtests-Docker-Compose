using Microsoft.EntityFrameworkCore;
using Weather.Data.Entities;

namespace Weather.Data.Services;

public class WeatherService : IWeatherService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly WeatherDbContext _dbContext;

    public WeatherService(WeatherDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<WeatherForecast>> GetForecastsAsync(CancellationToken token = default)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        
        var forecasts = await _dbContext.WeatherForecasts.AsNoTracking().ToListAsync(token).ConfigureAwait(false);
        return forecasts.AsReadOnly();
    }

    public async Task StoreForecastAsync(WeatherForecast forecast)
    {
        _dbContext.Add(forecast);
        await _dbContext.SaveChangesAsync();
    }
}