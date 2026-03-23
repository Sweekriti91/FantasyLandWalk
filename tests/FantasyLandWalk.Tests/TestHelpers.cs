using FantasyLandWalk.Models;
using FantasyLandWalk.Services;

namespace FantasyLandWalk.Tests;

// Shared no-op storage for tests that don't need persistence
internal class NoOpStorageService : IStorageService
{
    public Task InitializeAsync() => Task.CompletedTask;
    public Task<JourneyProgress?> LoadProgressAsync(string mapId) => Task.FromResult<JourneyProgress?>(null);
    public Task<List<JourneyProgress>> LoadAllProgressAsync() => Task.FromResult(new List<JourneyProgress>());
    public Task SaveProgressAsync(JourneyProgress progress) => Task.CompletedTask;
    public Task DeleteProgressAsync(string mapId) => Task.CompletedTask;
}
