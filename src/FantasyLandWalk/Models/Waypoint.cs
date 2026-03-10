namespace FantasyLandWalk.Models;

public class Waypoint
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TerrainType { get; set; } = string.Empty;
    public double CumulativeDistanceKm { get; set; }
    public float MapX { get; set; }
    public float MapY { get; set; }
}
