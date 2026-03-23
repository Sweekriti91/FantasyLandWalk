using Microsoft.Extensions.Logging;
using FantasyLandWalk.Services;
using FantasyLandWalk.ViewModels;
using FantasyLandWalk.Views;

namespace FantasyLandWalk;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services
		var dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "fantasy_journey.db3");
		builder.Services.AddSingleton<IStorageService>(new StorageService(dbPath));
		builder.Services.AddSingleton<IJourneyService, JourneyService>();
		builder.Services.AddSingleton<ISettingsService, SettingsService>();

		// ViewModels
		builder.Services.AddTransient<MapSelectionViewModel>();
		builder.Services.AddTransient<JourneyMapViewModel>();
		builder.Services.AddTransient<JourneyStatsViewModel>();
		builder.Services.AddTransient<SettingsViewModel>();

		// Pages
		builder.Services.AddTransient<MapSelectionPage>();
		builder.Services.AddTransient<JourneyMapPage>();
		builder.Services.AddTransient<JourneyStatsPage>();
		builder.Services.AddTransient<SettingsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
