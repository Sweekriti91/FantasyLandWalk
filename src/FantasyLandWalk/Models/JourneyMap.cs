namespace FantasyLandWalk.Models;

public class JourneyMap
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MapImageResource { get; set; } = string.Empty;
    public double TotalDistanceKm { get; set; }
    public bool IsAvailable { get; set; }
    public string ComingSoonMessage { get; set; } = string.Empty;
    public List<Waypoint> Waypoints { get; set; } = [];

    public bool IsComingSoon => !IsAvailable;
    public int WaypointCount => Waypoints.Count;
    public string DistanceInfo => $"~{TotalDistanceKm:N0} km • {WaypointCount} waypoints";
}
