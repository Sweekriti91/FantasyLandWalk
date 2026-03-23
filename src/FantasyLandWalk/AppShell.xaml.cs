using FantasyLandWalk.Views;

namespace FantasyLandWalk;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("JourneyMapPage", typeof(JourneyMapPage));
		Routing.RegisterRoute("JourneyStatsPage", typeof(JourneyStatsPage));
		Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
	}
}
