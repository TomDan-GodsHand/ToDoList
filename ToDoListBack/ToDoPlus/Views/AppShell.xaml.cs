namespace ToDoPlus
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

                Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        this.FlyoutBackgroundColor = Application.Current.RequestedTheme == AppTheme.Light ? Global.LightWindowBackGroundColor : Global.DarkWindowBackGroundColor;
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
        }
    }
}