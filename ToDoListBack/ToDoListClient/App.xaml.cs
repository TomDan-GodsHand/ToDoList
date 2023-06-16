using ToDoListClient.Platforms.Windows;

namespace ToDoListClient
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
                    WindowsTitleBar.SetColorForWindows(Colors.White, Colors.Black);
                }
                else if (appTheme == AppTheme.Dark)
                {
                    WindowsTitleBar.SetColorForWindows(Color.FromArgb("222222"), Colors.White);
                }
            };
            var appTheme = Application.Current.RequestedTheme;
            if (appTheme == AppTheme.Light)
            {
                WindowsTitleBar.SetColorForWindows(Colors.White, Colors.Black);
            }
            else if (appTheme == AppTheme.Dark)
            {
                WindowsTitleBar.SetColorForWindows(Color.FromArgb("222222"), Colors.White);
            }
#endif
        }
    }
}