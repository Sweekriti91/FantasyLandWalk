namespace FantasyLandWalk.Services;

public interface ISettingsService
{
    bool UseMetricUnits { get; set; }
    string AppVersion { get; }
}
