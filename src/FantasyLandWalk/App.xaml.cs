using Microsoft.Extensions.DependencyInjection;
using FantasyLandWalk.Services;

namespace FantasyLandWalk;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		_ = InitializeServicesAsync();
		return new Window(new AppShell());
	}

	private static async Task InitializeServicesAsync()
	{
		var journeyService = IPlatformApplication.Current?.Services.GetService<IJourneyService>();
		if (journeyService is not null)
			await journeyService.InitializeAsync();
	}
}