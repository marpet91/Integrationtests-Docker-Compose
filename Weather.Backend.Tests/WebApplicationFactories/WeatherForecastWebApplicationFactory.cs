using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Weather.Backend.Tests.Fixtures;
using Xunit;

namespace Weather.Backend.Tests.WebApplicationFactories;

[Collection("Database")]
public class WeatherForecastWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbFixture _dbFixture;

    public WeatherForecastWebApplicationFactory(DbFixture dbFixture)
    {
        _dbFixture = dbFixture;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>(
                    "ConnectionStrings:WeatherConnection", _dbFixture.ConnectionString)
            });
        });
    }
}