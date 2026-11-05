using HaulRoad.Core;
using HaulRoad.Data;
using Microsoft.Data.Sqlite;
using Xunit;

namespace HaulRoad.Data.IntegrationTests;

// Integration tests: these use a real (in-memory) SQLite database on purpose.
public sealed class SqliteReadingRepositoryTests : IDisposable
{
    private static readonly DateTimeOffset Noon = new(2026, 11, 2, 12, 0, 0, TimeSpan.Zero);

    private readonly SqliteConnection _connection;
    private readonly SqliteReadingRepository _repository;

    public SqliteReadingRepositoryTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();
        ReadingSchema.Create(_connection);
        _repository = new SqliteReadingRepository(_connection);
    }

    public void Dispose() => _connection.Dispose();

    [Fact]
    public void GetBetween_ReturnsAStoredReading()
    {
        var reading = new TelemetryReading("T-12", Noon, 180);
        _repository.Add(reading);

        var result = _repository.GetBetween("T-12", Noon.AddHours(-1), Noon.AddHours(1));

        Assert.Equal(new[] { reading }, result);
    }

    [Fact]
    public void GetBetween_ReturnsOnlyTheRequestedTrucksReadings()
    {
        _repository.Add(new TelemetryReading("T-12", Noon, 180));
        _repository.Add(new TelemetryReading("T-14", Noon, 90));

        var result = _repository.GetBetween("T-12", Noon.AddHours(-1), Noon.AddHours(1));

        Assert.Equal(new[] { new TelemetryReading("T-12", Noon, 180) }, result);
    }

    [Fact]
    public void GetBetween_LeavesOutReadingsOutsideTheRange()
    {
        _repository.Add(new TelemetryReading("T-12", Noon.AddHours(-2), 50));
        _repository.Add(new TelemetryReading("T-12", Noon, 180));
        _repository.Add(new TelemetryReading("T-12", Noon.AddHours(2), 60));

        var result = _repository.GetBetween("T-12", Noon.AddHours(-1), Noon.AddHours(1));

        Assert.Equal(new[] { new TelemetryReading("T-12", Noon, 180) }, result);
    }

    [Fact]
    public void GetBetween_IncludesTheStart_AndExcludesTheEnd()
    {
        _repository.Add(new TelemetryReading("T-12", Noon, 180));
        _repository.Add(new TelemetryReading("T-12", Noon.AddHours(1), 190));

        var result = _repository.GetBetween("T-12", Noon, Noon.AddHours(1));

        Assert.Equal(new[] { new TelemetryReading("T-12", Noon, 180) }, result);
    }

    [Fact]
    public void GetBetween_ReturnsReadingsOldestFirst_WhateverTheInsertOrder()
    {
        var later = new TelemetryReading("T-12", Noon.AddMinutes(2), 190);
        var earlier = new TelemetryReading("T-12", Noon.AddMinutes(1), 180);
        _repository.Add(later);
        _repository.Add(earlier);

        var result = _repository.GetBetween("T-12", Noon.AddHours(-1), Noon.AddHours(1));

        Assert.Equal(new[] { earlier, later }, result);
    }
}
