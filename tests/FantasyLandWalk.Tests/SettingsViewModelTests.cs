using FantasyLandWalk.Services;
using FantasyLandWalk.ViewModels;

namespace FantasyLandWalk.Tests;

public class SettingsViewModelTests
{
    [Fact]
    public void Constructor_LoadsUseMetricUnitsFromService()
    {
        var settings = new FakeSettingsService { UseMetricUnits = false };
        var vm = new SettingsViewModel(settings, new JourneyService(new NoOpStorageService()));

        Assert.False(vm.UseMetricUnits);
    }

    [Fact]
    public void Constructor_LoadsAppVersionFromService()
    {
        var settings = new FakeSettingsService();
        var vm = new SettingsViewModel(settings, new JourneyService(new NoOpStorageService()));

        Assert.Equal("Version 1.0-test", vm.AppVersion);
    }

    [Fact]
    public void UnitsLabel_IsKilometers_WhenMetricEnabled()
    {
        var vm = new SettingsViewModel(
            new FakeSettingsService { UseMetricUnits = true },
            new JourneyService(new NoOpStorageService()));

        Assert.Equal("Kilometers (km)", vm.UnitsLabel);
    }

    [Fact]
    public void UnitsLabel_IsMiles_WhenMetricDisabled()
    {
        var vm = new SettingsViewModel(
            new FakeSettingsService { UseMetricUnits = false },
            new JourneyService(new NoOpStorageService()));

        Assert.Equal("Miles (mi)", vm.UnitsLabel);
    }

    [Fact]
    public void ToggleMetricUnits_UpdatesService()
    {
        var settings = new FakeSettingsService { UseMetricUnits = true };
        var vm = new SettingsViewModel(settings, new JourneyService(new NoOpStorageService()));

        vm.UseMetricUnits = false;

        Assert.False(settings.UseMetricUnits);
    }

    [Fact]
    public void ToggleMetricUnits_UpdatesUnitsLabel()
    {
        var vm = new SettingsViewModel(
            new FakeSettingsService { UseMetricUnits = true },
            new JourneyService(new NoOpStorageService()));

        vm.UseMetricUnits = false;

        Assert.Equal("Miles (mi)", vm.UnitsLabel);
    }

    [Fact]
    public void HasStatusMessage_IsFalse_Initially()
    {
        var vm = new SettingsViewModel(
            new FakeSettingsService(),
            new JourneyService(new NoOpStorageService()));

        Assert.False(vm.HasStatusMessage);
    }

    // --- Fakes ---

    private class FakeSettingsService : ISettingsService
    {
        public bool UseMetricUnits { get; set; } = true;
        public string AppVersion => "1.0-test";
    }
}
