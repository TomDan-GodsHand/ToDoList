using Microsoft.Extensions.Logging;
using ToDoListClient.Platforms.Windows;
using ToDoListClient.Services;

namespace ToDoListClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Global.Ini();
            Global.Connect();
            Global.HttpClient = new HttpClient();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var services = builder.Services;
#if WINDOWS

            services.AddSingleton<ITrayService, TrayService>();
#endif
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}