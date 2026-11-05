using HaulRoad.Core;
using HaulRoad.Data;
using Microsoft.Data.Sqlite;
using Xunit;

namespace HaulRoad.Data.IntegrationTests;

public class SqliteSmokeTests
{
    [Fact]
    public void InMemoryDatabase_HasTheReadingsTable()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        ReadingSchema.Create(connection);

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = 'readings'";
        var tableCount = Convert.ToInt64(command.ExecuteScalar());

        Assert.Equal(1L, tableCount);
    }
}
