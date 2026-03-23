using FantasyLandWalk.Models;
using FantasyLandWalk.Services;

namespace FantasyLandWalk.Tests;

public class StorageServiceTests : IAsyncLifetime
{
    private readonly string _dbPath = System.IO.Path.Combine(
        System.IO.Path.GetTempPath(),
        $"flw_test_{Guid.NewGuid():N}.db3");

    private StorageService _service = null!;

    public async Task InitializeAsync()
    {
        _service = new StorageService(_dbPath);
        await _service.InitializeAsync();
    }

    public Task DisposeAsync()
    {
        if (System.IO.File.Exists(_dbPath))
            System.IO.File.Delete(_dbPath);
        return Task.CompletedTask;
    }

    [Fact]
    public async Task SaveAndLoad_JourneyProgress_RoundTrips()
    {
        var progress = new JourneyProgress
        {
            MapId = "the-realm-walk",
            DistanceWalkedKm = 193,
            TotalSteps = 250000,
            CurrentWaypointIndex = 1,
            StartedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            LastUpdated = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        await _service.SaveProgressAsync(progress);
        var loaded = await _service.LoadProgressAsync("the-realm-walk");

        Assert.NotNull(loaded);
        Assert.Equal(193, loaded.DistanceWalkedKm);
        Assert.Equal(250000, loaded.TotalSteps);
        Assert.Equal(1, loaded.CurrentWaypointIndex);
    }

    [Fact]
    public async Task LoadProgress_ReturnsNull_ForNonExistentMap()
    {
        var loaded = await _service.LoadProgressAsync("nonexistent-map");
        Assert.Null(loaded);
    }

    [Fact]
    public async Task SaveProgressAsync_UpdatesExistingRecord()
    {
        var progress = new JourneyProgress { MapId = "the-realm-walk", DistanceWalkedKm = 100 };
        await _service.SaveProgressAsync(progress);

        progress.DistanceWalkedKm = 500;
        await _service.SaveProgressAsync(progress);

        var loaded = await _service.LoadProgressAsync("the-realm-walk");
        Assert.NotNull(loaded);
        Assert.Equal(500, loaded.DistanceWalkedKm);
    }

    [Fact]
    public async Task DeleteProgressAsync_RemovesRecord()
    {
        var progress = new JourneyProgress { MapId = "the-realm-walk", DistanceWalkedKm = 100 };
        await _service.SaveProgressAsync(progress);

        await _service.DeleteProgressAsync("the-realm-walk");

        var loaded = await _service.LoadProgressAsync("the-realm-walk");
        Assert.Null(loaded);
    }

    [Fact]
    public async Task LoadAllProgressAsync_ReturnsAllSavedRecords()
    {
        await _service.SaveProgressAsync(new JourneyProgress { MapId = "map-a", DistanceWalkedKm = 10 });
        await _service.SaveProgressAsync(new JourneyProgress { MapId = "map-b", DistanceWalkedKm = 20 });

        var all = await _service.LoadAllProgressAsync();

        Assert.Equal(2, all.Count);
        Assert.Contains(all, p => p.MapId == "map-a");
        Assert.Contains(all, p => p.MapId == "map-b");
    }

    [Fact]
    public async Task LoadAllProgressAsync_ReturnsEmpty_WhenNothingSaved()
    {
        var all = await _service.LoadAllProgressAsync();
        Assert.Empty(all);
    }
}
