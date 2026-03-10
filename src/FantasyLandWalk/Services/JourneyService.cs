namespace FantasyLandWalk.Services;

using FantasyLandWalk.Models;

public class JourneyService : IJourneyService
{
    private readonly List<JourneyMap> _maps;
    private readonly Dictionary<string, JourneyProgress> _progressByMap = [];

    public JourneyService()
    {
        _maps = BuildMaps();
    }

    public List<JourneyMap> GetAvailableMaps() => _maps;

    public JourneyMap? GetMap(string mapId) =>
        _maps.FirstOrDefault(m => m.Id == mapId);

    public JourneyProgress GetProgress(string mapId)
    {
        if (!_progressByMap.TryGetValue(mapId, out var progress))
        {
            progress = new JourneyProgress
            {
                MapId = mapId,
                DistanceWalkedKm = 0,
                TotalSteps = 0,
                CurrentWaypointIndex = 0,
                StartedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            _progressByMap[mapId] = progress;
        }

        return progress;
    }

    public Waypoint? GetCurrentWaypoint(string mapId)
    {
        var map = GetMap(mapId);
        var progress = GetProgress(mapId);

        if (map is null || map.Waypoints.Count == 0)
            return null;

        return map.Waypoints[Math.Min(progress.CurrentWaypointIndex, map.Waypoints.Count - 1)];
    }

    public Waypoint? GetNextWaypoint(string mapId)
    {
        var map = GetMap(mapId);
        var progress = GetProgress(mapId);

        if (map is null || map.Waypoints.Count == 0)
            return null;

        var nextIndex = progress.CurrentWaypointIndex + 1;
        return nextIndex < map.Waypoints.Count ? map.Waypoints[nextIndex] : null;
    }

    public double GetDistanceToNextWaypoint(string mapId)
    {
        var progress = GetProgress(mapId);
        var next = GetNextWaypoint(mapId);

        if (next is null)
            return 0;

        return Math.Max(0, next.CumulativeDistanceKm - progress.DistanceWalkedKm);
    }

    public double GetProgressPercentage(string mapId)
    {
        var map = GetMap(mapId);
        var progress = GetProgress(mapId);

        if (map is null || map.TotalDistanceKm <= 0)
            return 0;

        return Math.Clamp(progress.DistanceWalkedKm / map.TotalDistanceKm * 100, 0, 100);
    }

    private static List<JourneyMap> BuildMaps() =>
    [
        new JourneyMap
        {
            Id = "the-realm-walk",
            Name = "The Realm Walk",
            Description = "A legendary journey across the realm — from the rolling hills of The Burrows to the fiery summit of The Flame Peak.",
            MapImageResource = "realm_walk_map.png",
            TotalDistanceKm = 2860,
            IsAvailable = true,
            Waypoints =
            [
                new Waypoint
                {
                    Id = "the-burrows",
                    Name = "The Burrows",
                    Description = "A peaceful hamlet nestled among green, rolling hills. Your journey begins here.",
                    TerrainType = "hills",
                    CumulativeDistanceKm = 0,
                    MapX = 0.15f,
                    MapY = 0.35f
                },
                new Waypoint
                {
                    Id = "crossroads-inn",
                    Name = "Crossroads Inn",
                    Description = "A bustling waypoint where traders and travelers gather before heading into the wilds.",
                    TerrainType = "farmland",
                    CumulativeDistanceKm = 193,
                    MapX = 0.25f,
                    MapY = 0.38f
                },
                new Waypoint
                {
                    Id = "storm-ridge",
                    Name = "Storm Ridge",
                    Description = "Windswept highlands where the sky churns with endless storms.",
                    TerrainType = "highlands",
                    CumulativeDistanceKm = 322,
                    MapX = 0.35f,
                    MapY = 0.30f
                },
                new Waypoint
                {
                    Id = "the-hidden-valley",
                    Name = "The Hidden Valley",
                    Description = "A secret mountain refuge, sheltered from the dangers of the wider world.",
                    TerrainType = "mountain_refuge",
                    CumulativeDistanceKm = 483,
                    MapX = 0.42f,
                    MapY = 0.28f
                },
                new Waypoint
                {
                    Id = "the-deep-mines",
                    Name = "The Deep Mines",
                    Description = "Ancient underground passages carved deep into the mountains.",
                    TerrainType = "mines",
                    CumulativeDistanceKm = 744,
                    MapX = 0.45f,
                    MapY = 0.40f
                },
                new Waypoint
                {
                    Id = "the-golden-wood",
                    Name = "The Golden Wood",
                    Description = "An enchanted forest where golden light filters through ancient canopies.",
                    TerrainType = "forest",
                    CumulativeDistanceKm = 933,
                    MapX = 0.52f,
                    MapY = 0.45f
                },
                new Waypoint
                {
                    Id = "the-great-statues",
                    Name = "The Great Statues",
                    Description = "Towering stone sentinels that guard the river passage to the east.",
                    TerrainType = "river",
                    CumulativeDistanceKm = 1094,
                    MapX = 0.58f,
                    MapY = 0.42f
                },
                new Waypoint
                {
                    Id = "the-shadow-lands",
                    Name = "The Shadow Lands",
                    Description = "A desolate volcanic wasteland shrouded in ash and darkness.",
                    TerrainType = "volcanic",
                    CumulativeDistanceKm = 2173,
                    MapX = 0.75f,
                    MapY = 0.55f
                },
                new Waypoint
                {
                    Id = "the-flame-peak",
                    Name = "The Flame Peak",
                    Description = "The fiery summit at the end of the world. Your final destination.",
                    TerrainType = "volcanic",
                    CumulativeDistanceKm = 2860,
                    MapX = 0.82f,
                    MapY = 0.58f
                }
            ]
        },
        new JourneyMap
        {
            Id = "the-battle-station",
            Name = "The Battle Station",
            Description = "A perilous trek through fortified wastelands and star-lit corridors.",
            MapImageResource = "battle_station_map.png",
            TotalDistanceKm = 0,
            IsAvailable = false,
            ComingSoonMessage = "Coming in a future update!"
        }
    ];
}
