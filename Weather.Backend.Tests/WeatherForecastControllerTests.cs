using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Weather.Backend.Tests.WebApplicationFactories;
using Weather.Data.Entities;
using Xunit;

namespace Weather.Backend.Tests;

[Collection("Database")]
public class WeatherForecastControllerTests : IClassFixture<WeatherForecastWebApplicationFactory>
{
    private readonly WeatherForecastWebApplicationFactory _factory;
    
    public WeatherForecastControllerTests(WeatherForecastWebApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Theory]
    [InlineAutoData]
    public async Task WeatherForecastReturnsOkay(WeatherForecast forecast)
    {
        var waf = new WebApplicationFactory<Program>();
        var client = waf.CreateDefaultClient();

        // insert into db what you want to assert
        await client.PostAsJsonAsync("WeatherForecast", forecast);
        
        // read from db
        var forecasts = await client.GetFromJsonAsync<List<WeatherForecast>>("WeatherForecast");

        // do asserts or whatever..
        forecasts.Should().NotBeEmpty();
    }
}