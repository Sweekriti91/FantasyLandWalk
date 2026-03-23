namespace FantasyLandWalk.Services;

public class SettingsService : ISettingsService
{
    private const string UseMetricKey = "use_metric_units";

    public bool UseMetricUnits
    {
        get
        {
#if !NET10_0
            return Preferences.Default.Get(UseMetricKey, true);
#else
            return true;
#endif
        }
        set
        {
#if !NET10_0
            Preferences.Default.Set(UseMetricKey, value);
#endif
        }
    }

    public string AppVersion
    {
#if !NET10_0
        get => AppInfo.Current.VersionString;
#else
        get => "1.0";
#endif
    }
}
