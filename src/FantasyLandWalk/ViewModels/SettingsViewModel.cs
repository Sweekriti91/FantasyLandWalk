using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyLandWalk.Services;

namespace FantasyLandWalk.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private readonly IJourneyService _journeyService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(UnitsLabel))]
    private bool _useMetricUnits;

    [ObservableProperty]
    private string _appVersion = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasStatusMessage))]
    private string _statusMessage = string.Empty;

    public bool HasStatusMessage => !string.IsNullOrEmpty(StatusMessage);

    public string UnitsLabel => UseMetricUnits ? "Kilometers (km)" : "Miles (mi)";

    public SettingsViewModel(ISettingsService settingsService, IJourneyService journeyService)
    {
        _settingsService = settingsService;
        _journeyService = journeyService;

        // Set backing fields directly to avoid persisting during construction
        _useMetricUnits = settingsService.UseMetricUnits;
        _appVersion = $"Version {settingsService.AppVersion}";
    }

    partial void OnUseMetricUnitsChanged(bool value)
    {
        _settingsService.UseMetricUnits = value;
    }

    [RelayCommand]
    private async Task ResetAllProgress()
    {
#if !NET10_0
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Reset All Progress",
            "This will reset all your journey progress. This cannot be undone.",
            "Reset",
            "Cancel");

        if (!confirmed)
            return;

        var maps = _journeyService.GetAvailableMaps();
        foreach (var map in maps.Where(m => m.IsAvailable))
            await _journeyService.ResetProgressAsync(map.Id);

        StatusMessage = "All journey progress has been reset.";
#else
        await Task.CompletedTask;
#endif
    }
}
