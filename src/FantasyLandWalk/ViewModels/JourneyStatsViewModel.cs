using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyLandWalk.Models;
using FantasyLandWalk.Services;

namespace FantasyLandWalk.ViewModels;

public partial class JourneyStatsViewModel : ObservableObject
#if !NETSTANDARD && !NET10_0
    , IQueryAttributable
#endif
{
    private readonly IJourneyService _journeyService;
    private readonly ISettingsService _settingsService;

    private string _mapId = string.Empty;

    [ObservableProperty]
    private string _mapName = string.Empty;

    [ObservableProperty]
    private string _distanceWalked = string.Empty;

    [ObservableProperty]
    private string _distanceRemaining = string.Empty;

    [ObservableProperty]
    private string _totalDistance = string.Empty;

    [ObservableProperty]
    private int _totalSteps;

    [ObservableProperty]
    private int _waypointsReached;

    [ObservableProperty]
    private int _totalWaypoints;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProgressFraction))]
    private double _progressPercentage;

    [ObservableProperty]
    private string _journeyStarted = string.Empty;

    [ObservableProperty]
    private string _daysOnJourney = string.Empty;

    [ObservableProperty]
    private string _currentWaypointName = string.Empty;

    [ObservableProperty]
    private string _nextWaypointName = string.Empty;

    public double ProgressFraction => ProgressPercentage / 100.0;

    public JourneyStatsViewModel(IJourneyService journeyService, ISettingsService settingsService)
    {
        _journeyService = journeyService;
        _settingsService = settingsService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("mapId", out var mapIdObj) && mapIdObj is string mapId)
        {
            _mapId = mapId;
            LoadStats(mapId);
        }
    }

    public void LoadStats(string mapId)
    {
        var map = _journeyService.GetMap(mapId);
        if (map is null)
            return;

        var progress = _journeyService.GetProgress(mapId);
        var useMetric = _settingsService.UseMetricUnits;

        MapName = map.Name;
        TotalWaypoints = map.Waypoints.Count;
        TotalSteps = progress.TotalSteps;
        ProgressPercentage = _journeyService.GetProgressPercentage(mapId);

        if (useMetric)
        {
            DistanceWalked = $"{progress.DistanceWalkedKm:F1} km";
            TotalDistance = $"{map.TotalDistanceKm:N0} km";
            DistanceRemaining = $"{Math.Max(0, map.TotalDistanceKm - progress.DistanceWalkedKm):F1} km to go";
        }
        else
        {
            const double kmToMiles = 0.621371;
            DistanceWalked = $"{progress.DistanceWalkedKm * kmToMiles:F1} mi";
            TotalDistance = $"{map.TotalDistanceKm * kmToMiles:N0} mi";
            DistanceRemaining = $"{Math.Max(0, (map.TotalDistanceKm - progress.DistanceWalkedKm) * kmToMiles):F1} mi to go";
        }

        WaypointsReached = map.Waypoints.Count(w => w.CumulativeDistanceKm <= progress.DistanceWalkedKm);
        CurrentWaypointName = _journeyService.GetCurrentWaypoint(mapId)?.Name ?? string.Empty;
        NextWaypointName = _journeyService.GetNextWaypoint(mapId)?.Name ?? "—";

        if (progress.StartedAt != default)
        {
            JourneyStarted = progress.StartedAt.ToString("MMMM d, yyyy");
            var days = (int)(DateTime.UtcNow - progress.StartedAt).TotalDays;
            DaysOnJourney = days == 1 ? "1 day" : $"{days} days";
        }
        else
        {
            JourneyStarted = "Not yet started";
            DaysOnJourney = "—";
        }
    }
}
