using Microsoft.AspNetCore.Mvc;
using Weather.Data.Entities;
using Weather.Data.Services;

namespace Weather.Backend.Features.Weather;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
    {
        ArgumentNullException.ThrowIfNull(weatherService);
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken token)
    {
        return await _weatherService.GetForecastsAsync(token);
    }

    [HttpPost]
    public async Task Post([FromBody] WeatherForecast forecast)
    {
        await _weatherService.StoreForecastAsync(forecast);
    }
}