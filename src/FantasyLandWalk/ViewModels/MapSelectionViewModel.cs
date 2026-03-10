using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyLandWalk.Models;
using FantasyLandWalk.Services;

namespace FantasyLandWalk.ViewModels;

public partial class MapSelectionViewModel : ObservableObject
{
    private readonly IJourneyService _journeyService;

    public ObservableCollection<JourneyMap> Maps { get; } = [];

    [ObservableProperty]
    private JourneyMap? _selectedMap;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public MapSelectionViewModel(IJourneyService journeyService)
    {
        _journeyService = journeyService;
        LoadMaps();
    }

    private void LoadMaps()
    {
        var maps = _journeyService.GetAvailableMaps();

        Maps.Clear();
        foreach (var map in maps)
        {
            Maps.Add(map);
        }
    }

    [RelayCommand]
    private async Task SelectMap(JourneyMap map)
    {
        if (!map.IsAvailable)
        {
            StatusMessage = map.ComingSoonMessage;
            return;
        }

        SelectedMap = map;
#if !NET10_0
        await Shell.Current.GoToAsync($"JourneyMapPage?mapId={map.Id}");
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
    private async Task OpenAINarrator()
    {
#if !NET10_0
        await Shell.Current.DisplayAlertAsync("Coming soon!", "AI Narrator is not yet available.", "OK");
#else
        await Task.CompletedTask;
#endif
    }

    [RelayCommand]
    private async Task OpenSettings()
    {
#if !NET10_0
        await Shell.Current.DisplayAlertAsync("Coming soon!", "Settings are not yet available.", "OK");
#else
        await Task.CompletedTask;
#endif
    }

    [RelayCommand]
    private async Task OpenStats()
    {
#if !NET10_0
        await Shell.Current.DisplayAlertAsync("Coming soon!", "Stats are not yet available.", "OK");
#else
        await Task.CompletedTask;
#endif
    }
}
