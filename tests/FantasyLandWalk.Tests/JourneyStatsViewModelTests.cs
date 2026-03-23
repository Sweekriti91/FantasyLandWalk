using FantasyLandWalk.Models;
using FantasyLandWalk.Services;
using FantasyLandWalk.ViewModels;

namespace FantasyLandWalk.Tests;

public class JourneyStatsViewModelTests
{
    private static JourneyService CreateService() => new(new NoOpStorageService());

    private static FakeSettingsService MetricSettings() => new() { UseMetricUnits = true };
    private static FakeSettingsService ImperialSettings() => new() { UseMetricUnits = false };

    [Fact]
    public void LoadStats_WhenMapExists_SetsMapName()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Equal("The Realm Walk", vm.MapName);
    }

    [Fact]
    public void LoadStats_SetsWaypointsReachedToZero_ForNewJourney()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");

        // At start the first waypoint (0 km, The Burrows) counts as reached
        Assert.Equal(1, vm.WaypointsReached);
    }

    [Fact]
    public void LoadStats_SetsTotalWaypoints()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Equal(9, vm.TotalWaypoints);
    }

    [Fact]
    public void LoadStats_SetsProgressPercentageToZero_ForNewJourney()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Equal(0, vm.ProgressPercentage);
    }

    [Fact]
    public void LoadStats_ProgressFraction_IsPercentageDividedBy100()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Equal(vm.ProgressPercentage / 100.0, vm.ProgressFraction);
    }

    [Fact]
    public void LoadStats_FormatsDistanceInKm_WhenMetricUnits()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Contains("km", vm.DistanceWalked);
        Assert.Contains("km", vm.TotalDistance);
    }

    [Fact]
    public void LoadStats_FormatsDistanceInMiles_WhenImperialUnits()
    {
        var vm = new JourneyStatsViewModel(CreateService(), ImperialSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Contains("mi", vm.DistanceWalked);
        Assert.Contains("mi", vm.TotalDistance);
    }

    [Fact]
    public void LoadStats_SetsCurrentWaypointToTheBurrows_ForNewJourney()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Equal("The Burrows", vm.CurrentWaypointName);
    }

    [Fact]
    public void LoadStats_SetsNextWaypointToCrossroadsInn_ForNewJourney()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Equal("Crossroads Inn", vm.NextWaypointName);
    }

    [Fact]
    public void LoadStats_SetsJourneyStartedToNotYetStarted_ForNewJourney()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("the-realm-walk");
        Assert.Equal("Not yet started", vm.JourneyStarted);
    }

    [Fact]
    public void LoadStats_DoesNothing_ForUnknownMapId()
    {
        var vm = new JourneyStatsViewModel(CreateService(), MetricSettings());
        vm.LoadStats("nonexistent-map");
        Assert.Equal(string.Empty, vm.MapName);
    }

    // --- Fakes ---

    private class FakeSettingsService : ISettingsService
    {
        public bool UseMetricUnits { get; set; } = true;
        public string AppVersion => "1.0-test";
    }
}
