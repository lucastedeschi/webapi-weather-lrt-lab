using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Persistence;

namespace WeatlherLrt.Application.Test.Persistence
{
    internal sealed class WeatherLrtTestContextFactory
    {
        public static IWeatherLrtContext CreateContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<WeatherLrtContext>()
                .UseSqlite(connection)
                .ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning));

            var context = new WeatherLrtContext(builder.Options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
