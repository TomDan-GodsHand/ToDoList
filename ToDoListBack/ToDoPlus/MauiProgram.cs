using Coldairarrow.Util;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace ToDoPlus
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Global.Ini();
            InitId();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(wndLifeCycleBuilder =>
                    {
                        wndLifeCycleBuilder.OnWindowCreated(window =>
                        {
                            //Global.window = window;
                            //  window.TryMicaOrAcrylic();
                        });
                    });
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        private static void InitId()
        {
            long workid = 1;
            new IdHelperBootstrapper()
                .SetWorkderId(workid)
                .Boot();
        }
    }
}