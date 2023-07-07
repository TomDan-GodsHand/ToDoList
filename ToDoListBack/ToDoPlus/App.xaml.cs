#if WINDOWS
using ToDoPlus.Platforms.Windows;
#endif

namespace ToDoPlus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

#if WINDOWS

            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                var appTheme = Application.Current.RequestedTheme;

                if (appTheme == AppTheme.Light)
                {
                    WindowsHelper.SetColorForWindows(Global.LightWindowBackGroundColor, Colors.Black);
                }
                else if (appTheme == AppTheme.Dark)
                {
                    WindowsHelper.SetColorForWindows(Global.DarkWindowBackGroundColor, Colors.White);
                }
            };

            var appTheme = Application.Current.RequestedTheme;
            if (appTheme == AppTheme.Light)
            {
                WindowsHelper.SetColorForWindows(Global.LightWindowBackGroundColor, Colors.Black);
            }
            else if (appTheme == AppTheme.Dark)
            {
                WindowsHelper.SetColorForWindows(Global.DarkWindowBackGroundColor, Colors.White);
            }

#endif
        }
    }
}