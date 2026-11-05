using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class SustainedLoadTrackerTests
{
    private static readonly TimeSpan Window = TimeSpan.FromSeconds(12);

    [Fact]
    public void Current_BecomesLoaded_AfterLoadedHoldsForTheWindow()
    {
        var tracker = new SustainedLoadTracker(Window);

        tracker.Observe(LoadState.Loaded);
        Thread.Sleep(Window + TimeSpan.FromMilliseconds(500)); // real time: slow on purpose
        tracker.Observe(LoadState.Loaded);

        Assert.Equal(LoadState.Loaded, tracker.Current);
    }
}
