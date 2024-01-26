using CommunityToolkit.Maui;
using Memory.DataAccess.SQLServer;
using Memory.Maui_App.Services;
using Memory.Maui_App.ViewModels;
using Memory.Maui_App.Views;
using Microsoft.Extensions.Logging;

namespace Memory.Maui_App
{

	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
					fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
					fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				}).UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif

            builder.Services.AddSingleton<IToastService, ToastService>();
            builder.Services.AddSingleton<IMemoryScoreRepository, MemoryScoreRepository>();

            builder.Services.AddScoped<PageViewModel>();
			builder.Services.AddScoped<GamePageViewModel>();
			builder.Services.AddScoped<UserOptionsPageViewModel>();
			builder.Services.AddScoped<BaseViewModel>();
            builder.Services.AddScoped<ImageUploadPageViewModel>();
            builder.Services.AddScoped<HighscoresPageViewModel>();


            builder.Services.AddScoped<UserOptionsPage>();
            builder.Services.AddScoped<ImageUploadPage>();
            builder.Services.AddScoped<ConfirmPage>();
            builder.Services.AddScoped<GamePage>();
            builder.Services.AddScoped<HighscoresPage>();

			return builder.Build();
		}
	}
}
