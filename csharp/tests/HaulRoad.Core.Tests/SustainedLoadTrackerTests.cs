using HaulRoad.Core;
using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace HaulRoad.Core.Tests;

public class SustainedLoadTrackerTests
{
    private static readonly TimeSpan Window = TimeSpan.FromSeconds(12);
    private static readonly DateTimeOffset Start = new(2026, 11, 2, 6, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Current_BecomesLoaded_AfterLoadedHoldsForTheWindow()
    {
        var clock = new FakeTimeProvider(Start);
        var tracker = new SustainedLoadTracker(Window, clock);

        tracker.Observe(LoadState.Loaded);
        clock.Advance(Window);
        tracker.Observe(LoadState.Loaded);

        Assert.Equal(LoadState.Loaded, tracker.Current);
    }
}
