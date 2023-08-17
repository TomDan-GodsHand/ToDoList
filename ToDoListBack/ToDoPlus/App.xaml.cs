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
        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            window.SizeChanged += UpdateFlyoutBehaviorIfNeeded;
            return window;
        }
        private const double minPageWidth = 800;
        private void UpdateFlyoutBehaviorIfNeeded(object sender, EventArgs e)
        {
            double currentWidth = ((Window)sender).Width;
            if (currentWidth <= minPageWidth)
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
            else if (currentWidth > minPageWidth + Shell.Current.FlyoutWidth)
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
            }
        }
    }
}