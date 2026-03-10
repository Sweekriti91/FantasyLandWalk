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
		builder.Services.AddSingleton<IJourneyService, JourneyService>();

		// ViewModels
		builder.Services.AddTransient<MapSelectionViewModel>();
		builder.Services.AddTransient<JourneyMapViewModel>();

		// Pages
		builder.Services.AddTransient<MapSelectionPage>();
		builder.Services.AddTransient<JourneyMapPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
