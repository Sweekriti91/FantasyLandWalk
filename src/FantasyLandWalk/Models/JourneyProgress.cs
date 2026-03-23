namespace FantasyLandWalk.Models;

using SQLite;

public class JourneyProgress
{
    [PrimaryKey]
    public string MapId { get; set; } = string.Empty;
    public double DistanceWalkedKm { get; set; }
    public int TotalSteps { get; set; }
    public int CurrentWaypointIndex { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime LastUpdated { get; set; }
}
