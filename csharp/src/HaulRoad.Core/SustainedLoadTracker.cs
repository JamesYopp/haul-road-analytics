namespace HaulRoad.Core;

public sealed class SustainedLoadTracker
{
    public SustainedLoadTracker(TimeSpan window)
    {
        _ = window;
    }

    public LoadState Current => LoadState.Empty;

    public void Observe(LoadState observed)
    {
    }
}
