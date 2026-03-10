namespace FantasyLandWalk.Services;

using FantasyLandWalk.Models;

public interface IJourneyService
{
    List<JourneyMap> GetAvailableMaps();
    JourneyMap? GetMap(string mapId);
    JourneyProgress GetProgress(string mapId);
    Waypoint? GetCurrentWaypoint(string mapId);
    Waypoint? GetNextWaypoint(string mapId);
    double GetDistanceToNextWaypoint(string mapId);
    double GetProgressPercentage(string mapId);
}
