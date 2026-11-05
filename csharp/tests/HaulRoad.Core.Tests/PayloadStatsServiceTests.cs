using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class PayloadStatsServiceTests
{
    private static readonly DateTimeOffset ShiftStart = new(2026, 11, 2, 6, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset ShiftEnd = ShiftStart.AddHours(12);

    private static TelemetryReading ReadingAtMinute(int minute, ushort rawPayload) =>
        new("T-12", ShiftStart.AddMinutes(minute), rawPayload);

    [Fact]
    public void Summarize_AveragesThePayload_OfTheReadingsInTheRange()
    {
        var repository = new FakeReadingRepository();
        repository.Add(ReadingAtMinute(1, 100));
        repository.Add(ReadingAtMinute(2, 200));
        var service = new PayloadStatsService(repository);

        var summary = service.Summarize("T-12", ShiftStart, ShiftEnd);

        Assert.Equal((double?)150.0, summary.AverageTonnes);
        Assert.Equal(2, summary.UsedCount);
    }

    [Fact]
    public void Summarize_LeavesOutUnavailableReadings_AndCountsThem()
    {
        var repository = new FakeReadingRepository();
        repository.Add(ReadingAtMinute(1, 100));
        repository.Add(ReadingAtMinute(2, 0xFFFF));
        repository.Add(ReadingAtMinute(3, 200));
        var service = new PayloadStatsService(repository);

        var summary = service.Summarize("T-12", ShiftStart, ShiftEnd);

        Assert.Equal((double?)150.0, summary.AverageTonnes);
        Assert.Equal(2, summary.UsedCount);
        Assert.Equal(1, summary.UnavailableCount);
    }

    [Fact]
    public void Summarize_ReportsNoAverage_WhenEveryReadingIsUnavailable()
    {
        var repository = new FakeReadingRepository();
        repository.Add(ReadingAtMinute(1, 0xFFFF));
        repository.Add(ReadingAtMinute(2, 0xFFFF));
        var service = new PayloadStatsService(repository);

        var summary = service.Summarize("T-12", ShiftStart, ShiftEnd);

        Assert.Null(summary.AverageTonnes);
        Assert.Equal(0, summary.UsedCount);
        Assert.Equal(2, summary.UnavailableCount);
    }

    [Fact]
    public void Summarize_ReportsNoAverage_WhenThereAreNoReadings()
    {
        var service = new PayloadStatsService(new FakeReadingRepository());

        var summary = service.Summarize("T-12", ShiftStart, ShiftEnd);

        Assert.Null(summary.AverageTonnes);
        Assert.Equal(0, summary.UsedCount);
        Assert.Equal(0, summary.UnavailableCount);
    }
}
