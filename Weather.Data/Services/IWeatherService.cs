using Weather.Data.Entities;

namespace Weather.Data.Services;

public interface IWeatherService
{
    Task<IReadOnlyCollection<WeatherForecast>> GetForecastsAsync(CancellationToken token = default);
    Task StoreForecastAsync(WeatherForecast forecast);
}