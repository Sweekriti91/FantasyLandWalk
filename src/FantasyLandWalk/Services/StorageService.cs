namespace FantasyLandWalk.Services;

using SQLite;
using FantasyLandWalk.Models;

public class StorageService : IStorageService
{
    private readonly SQLiteAsyncConnection _db;

    public StorageService(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
    }

    public Task InitializeAsync() =>
        _db.CreateTableAsync<JourneyProgress>();

    public async Task<JourneyProgress?> LoadProgressAsync(string mapId) =>
        await _db.FindAsync<JourneyProgress>(mapId);

    public Task<List<JourneyProgress>> LoadAllProgressAsync() =>
        _db.Table<JourneyProgress>().ToListAsync();

    public Task SaveProgressAsync(JourneyProgress progress) =>
        _db.InsertOrReplaceAsync(progress);

    public Task DeleteProgressAsync(string mapId) =>
        _db.DeleteAsync<JourneyProgress>(mapId);
}
