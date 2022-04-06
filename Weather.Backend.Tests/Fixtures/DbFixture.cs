using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weather.Data;
using Xunit;

namespace Weather.Backend.Tests.Fixtures;

public class DbFixture : IAsyncDisposable
{
    private readonly WeatherDbContext _dbContext;
    public readonly string ConnectionString;
    public readonly string WeatherDbName = $"WeatherApp-{Guid.NewGuid()}";

    private bool _disposed;

    public DbFixture()
    {
        // "Server=localhost,1433;Database=WeatherApp;User=sa;Password=2@LaiNw)PDvs^t>L!Ybt]6H^%h3U>M"
        // ConnectionString = $"Server=localhost,1433;Database={WeatherDbName};User=sa;Password=2@LaiNw)PDvs^t>L!Ybt]6H^%h3U>M";
        ConnectionString = $"Server=localhost,1433;Database={WeatherDbName};User=sa;Password=2@LaiNw)PDvs^t>L!Ybt]6H^%h3U>M";

        var builder = new DbContextOptionsBuilder<WeatherDbContext>();

        builder.UseSqlServer(ConnectionString);
        _dbContext = new WeatherDbContext(builder.Options);

        _dbContext.Database.Migrate();
    }

    protected virtual async Task DisposeAsync(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            // remove the temp db from the server once all tests are done
            await _dbContext.Database.EnsureDeletedAsync();
        }

        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

[CollectionDefinition("Database")]
public class DatabaseCollection : ICollectionFixture<DbFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}