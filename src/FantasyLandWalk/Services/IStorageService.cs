namespace FantasyLandWalk.Services;

using FantasyLandWalk.Models;

public interface IStorageService
{
    Task InitializeAsync();
    Task<JourneyProgress?> LoadProgressAsync(string mapId);
    Task<List<JourneyProgress>> LoadAllProgressAsync();
    Task SaveProgressAsync(JourneyProgress progress);
    Task DeleteProgressAsync(string mapId);
}
