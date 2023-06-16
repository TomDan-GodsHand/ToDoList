using ToDoListClient.Services;

namespace ToDoListClient.Platforms.Windows
{
    public class TrayService : ITrayService
    {
        WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }

        public void Initialize()
        {
            tray = new WindowsTrayIcon("Platforms/Windows/trayicon.ico");
            tray.LeftClick = () =>
            {
                Services.WindowExtensions.BringToFront();
                ClickHandler?.Invoke();
            };
        }
    }
}