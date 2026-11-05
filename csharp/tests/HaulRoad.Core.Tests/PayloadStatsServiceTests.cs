using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class PayloadStatsServiceTests
{
    private static readonly DateTimeOffset ShiftStart = new(2026, 11, 2, 6, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset ShiftEnd = ShiftStart.AddHours(12);

    [Fact]
    public void Summarize_AveragesThePayload_OfTheReadingsInTheRange()
    {
        var repository = new FakeReadingRepository();
        repository.Add(new TelemetryReading("T-12", ShiftStart.AddMinutes(1), 100));
        repository.Add(new TelemetryReading("T-12", ShiftStart.AddMinutes(2), 200));
        var service = new PayloadStatsService(repository);

        var summary = service.Summarize("T-12", ShiftStart, ShiftEnd);

        Assert.Equal((double?)150.0, summary.AverageTonnes);
        Assert.Equal(2, summary.UsedCount);
    }

    [Fact]
    public void Summarize_LeavesOutUnavailableReadings_AndCountsThem()
    {
        var repository = new FakeReadingRepository();
        repository.Add(new TelemetryReading("T-12", ShiftStart.AddMinutes(1), 100));
        repository.Add(new TelemetryReading("T-12", ShiftStart.AddMinutes(2), 0xFFFF));
        repository.Add(new TelemetryReading("T-12", ShiftStart.AddMinutes(3), 200));
        var service = new PayloadStatsService(repository);

        var summary = service.Summarize("T-12", ShiftStart, ShiftEnd);

        Assert.Equal((double?)150.0, summary.AverageTonnes);
        Assert.Equal(2, summary.UsedCount);
        Assert.Equal(1, summary.UnavailableCount);
    }
}
