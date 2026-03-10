using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyLandWalk.Models;
using FantasyLandWalk.Services;

namespace FantasyLandWalk.ViewModels;

public partial class JourneyMapViewModel : ObservableObject
#if !NETSTANDARD && !NET10_0
    , IQueryAttributable
#endif
{
    private readonly IJourneyService _journeyService;

    [ObservableProperty]
    private string _mapName = string.Empty;

    [ObservableProperty]
    private string _currentWaypointName = string.Empty;

    [ObservableProperty]
    private string _nextWaypointName = string.Empty;

    [ObservableProperty]
    private double _distanceWalkedKm;

    [ObservableProperty]
    private double _totalDistanceKm;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProgressFraction))]
    private double _progressPercentage;

    [ObservableProperty]
    private double _distanceToNextKm;

    [ObservableProperty]
    private JourneyMap? _currentMap;

    public double ProgressFraction => ProgressPercentage / 100.0;

    public ObservableCollection<WaypointDisplayModel> Waypoints { get; } = [];

    public JourneyMapViewModel(IJourneyService journeyService)
    {
        _journeyService = journeyService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("mapId", out var mapIdObj) && mapIdObj is string mapId)
        {
            LoadMap(mapId);
        }
    }

    public void LoadMap(string mapId)
    {
        var map = _journeyService.GetMap(mapId);
        if (map is null)
            return;

        CurrentMap = map;
        MapName = map.Name;
        TotalDistanceKm = map.TotalDistanceKm;

        var progress = _journeyService.GetProgress(mapId);
        DistanceWalkedKm = progress.DistanceWalkedKm;

        ProgressPercentage = _journeyService.GetProgressPercentage(mapId);
        DistanceToNextKm = _journeyService.GetDistanceToNextWaypoint(mapId);

        var currentWaypoint = _journeyService.GetCurrentWaypoint(mapId);
        CurrentWaypointName = currentWaypoint?.Name ?? string.Empty;

        var nextWaypoint = _journeyService.GetNextWaypoint(mapId);
        NextWaypointName = nextWaypoint?.Name ?? string.Empty;

        Waypoints.Clear();
        foreach (var waypoint in map.Waypoints)
        {
            Waypoints.Add(new WaypointDisplayModel
            {
                Name = waypoint.Name,
                DistanceKm = waypoint.CumulativeDistanceKm,
                IsReached = waypoint.CumulativeDistanceKm <= progress.DistanceWalkedKm,
                TerrainType = waypoint.TerrainType,
            });
        }
    }

    [RelayCommand]
    private async Task OpenPhotoJournal()
    {
#if !NET10_0
        await Shell.Current.DisplayAlertAsync("Coming soon!", "Photo Journal is not yet available.", "OK");
#else
        await Task.CompletedTask;
#endif
    }

    [RelayCommand]
    private async Task OpenStoryLog()
    {
#if !NET10_0
        await Shell.Current.DisplayAlertAsync("Coming soon!", "Story Log is not yet available.", "OK");
#else
        await Task.CompletedTask;
#endif
    }

    [RelayCommand]
    private async Task OpenSoundscape()
    {
#if !NET10_0
        await Shell.Current.DisplayAlertAsync("Coming soon!", "Soundscapes are not yet available.", "OK");
#else
        await Task.CompletedTask;
#endif
    }

    [RelayCommand]
    private async Task OpenJourneyStats()
    {
#if !NET10_0
        await Shell.Current.DisplayAlertAsync("Coming soon!", "Journey Stats are not yet available.", "OK");
#else
        await Task.CompletedTask;
#endif
    }
}
