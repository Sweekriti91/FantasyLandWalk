namespace FantasyLandWalk.ViewModels;

public class WaypointDisplayModel
{
    public string Name { get; set; } = string.Empty;
    public double DistanceKm { get; set; }
    public bool IsReached { get; set; }
    public string TerrainType { get; set; } = string.Empty;

    public string StatusIcon => IsReached ? "✅" : "⬜";
    public string DistanceDisplay => $"{DistanceKm:N0} km";
}
