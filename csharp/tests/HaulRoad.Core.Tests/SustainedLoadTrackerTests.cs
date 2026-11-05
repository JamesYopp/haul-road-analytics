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

    [Fact]
    public void Current_StaysEmpty_WhenLoadedIsInterruptedBeforeTheWindowEnds()
    {
        var clock = new FakeTimeProvider(Start);
        var tracker = new SustainedLoadTracker(Window, clock);

        tracker.Observe(LoadState.Loaded);
        clock.Advance(TimeSpan.FromSeconds(5));
        tracker.Observe(LoadState.Empty);
        clock.Advance(TimeSpan.FromSeconds(8));
        tracker.Observe(LoadState.Loaded);

        Assert.Equal(LoadState.Empty, tracker.Current);
    }
}
