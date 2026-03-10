using FantasyLandWalk.Models;
using FantasyLandWalk.Services;

namespace FantasyLandWalk.Tests;

public class JourneyServiceTests
{
    private readonly JourneyService _service = new();

    [Fact]
    public void GetAvailableMaps_ReturnsAtLeastOneMap()
    {
        var maps = _service.GetAvailableMaps();
        Assert.NotEmpty(maps);
    }

    [Fact]
    public void GetAvailableMaps_ContainsRealmWalk()
    {
        var maps = _service.GetAvailableMaps();
        Assert.Contains(maps, m => m.Id == "the-realm-walk" && m.IsAvailable);
    }

    [Fact]
    public void GetAvailableMaps_ContainsBattleStationAsComingSoon()
    {
        var maps = _service.GetAvailableMaps();
        var battleStation = maps.FirstOrDefault(m => m.Id == "the-battle-station");
        Assert.NotNull(battleStation);
        Assert.False(battleStation.IsAvailable);
    }

    [Fact]
    public void GetMap_ReturnsCorrectMap()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);
        Assert.Equal("The Realm Walk", map.Name);
        Assert.Equal(2860, map.TotalDistanceKm);
    }

    [Fact]
    public void GetMap_ReturnsNullForUnknownId()
    {
        var map = _service.GetMap("nonexistent");
        Assert.Null(map);
    }

    [Fact]
    public void RealmWalk_HasNineWaypoints()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);
        Assert.Equal(9, map.Waypoints.Count);
    }

    [Fact]
    public void RealmWalk_FirstWaypointIsBurrows()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);
        Assert.Equal("The Burrows", map.Waypoints[0].Name);
        Assert.Equal(0, map.Waypoints[0].CumulativeDistanceKm);
    }

    [Fact]
    public void RealmWalk_LastWaypointIsFlamePeak()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);
        var last = map.Waypoints[^1];
        Assert.Equal("The Flame Peak", last.Name);
        Assert.Equal(2860, last.CumulativeDistanceKm);
    }

    [Fact]
    public void RealmWalk_WaypointsInAscendingDistanceOrder()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);

        for (int i = 1; i < map.Waypoints.Count; i++)
        {
            Assert.True(
                map.Waypoints[i].CumulativeDistanceKm > map.Waypoints[i - 1].CumulativeDistanceKm,
                $"Waypoint {map.Waypoints[i].Name} should be farther than {map.Waypoints[i - 1].Name}");
        }
    }

    [Fact]
    public void RealmWalk_NoTrademarkedNames()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);

        var forbiddenNames = new[] { "Shire", "Mordor", "Rivendell", "Moria", "Lothlorien",
            "Mount Doom", "Hobbiton", "Bree", "Weathertop", "Amon Hen", "Bag End" };

        foreach (var waypoint in map.Waypoints)
        {
            foreach (var forbidden in forbiddenNames)
            {
                Assert.DoesNotContain(forbidden, waypoint.Name, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    [Fact]
    public void GetProgress_ReturnsZeroForNewJourney()
    {
        var progress = _service.GetProgress("the-realm-walk");
        Assert.Equal(0, progress.DistanceWalkedKm);
        Assert.Equal(0, progress.TotalSteps);
    }

    [Fact]
    public void GetProgressPercentage_ReturnsZeroForNewJourney()
    {
        var percentage = _service.GetProgressPercentage("the-realm-walk");
        Assert.Equal(0, percentage);
    }

    [Fact]
    public void GetCurrentWaypoint_ReturnsFirstWaypointForNewJourney()
    {
        var waypoint = _service.GetCurrentWaypoint("the-realm-walk");
        Assert.NotNull(waypoint);
        Assert.Equal("The Burrows", waypoint.Name);
    }

    [Fact]
    public void GetNextWaypoint_ReturnsSecondWaypointForNewJourney()
    {
        var next = _service.GetNextWaypoint("the-realm-walk");
        Assert.NotNull(next);
        Assert.Equal("Crossroads Inn", next.Name);
    }

    [Fact]
    public void GetDistanceToNextWaypoint_ReturnsCorrectDistanceForNewJourney()
    {
        var distance = _service.GetDistanceToNextWaypoint("the-realm-walk");
        Assert.Equal(193, distance);
    }

    [Fact]
    public void AllWaypoints_HaveValidMapCoordinates()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);

        foreach (var wp in map.Waypoints)
        {
            Assert.InRange(wp.MapX, 0f, 1f);
            Assert.InRange(wp.MapY, 0f, 1f);
        }
    }

    [Fact]
    public void AllWaypoints_HaveTerrainType()
    {
        var map = _service.GetMap("the-realm-walk");
        Assert.NotNull(map);

        foreach (var wp in map.Waypoints)
        {
            Assert.False(string.IsNullOrWhiteSpace(wp.TerrainType),
                $"Waypoint {wp.Name} is missing terrain type");
        }
    }
}
