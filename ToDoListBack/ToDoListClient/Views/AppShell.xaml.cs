using ToDoListClient.Services;
using ToDoListClient.Views;

namespace ToDoListClient
{
    public partial class AppShell : Shell
    {
        private static bool isSetup = false;

        public AppShell()
        {
            InitializeComponent();
            if (!isSetup)
            {
                isSetup = true;

                SetupTrayIcon();
                Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        this.FlyoutBackgroundColor = Application.Current.RequestedTheme == AppTheme.Light ? Colors.White : Color.FromArgb("222222");
                    });
                });
            }
            Global.ToDoGroup.ForEach(it =>
            {
                Items.Add(new FlyoutItem
                {
                    Title = it.Name,
                    Route = nameof(MainPage),
                    Items =
                    {
                        new ShellContent
                        {
                            Route = it.Id,
                            BindingContext = it,
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        }
                    }
                });
            });

            Routing.RegisterRoute("/options", typeof(Options));
        }

        private void SetupTrayIcon()
        {
            var trayService = ServiceExtensions.GetService<ITrayService>();

            if (trayService != null)
            {
                trayService.Initialize();
            }
        }
    }
}