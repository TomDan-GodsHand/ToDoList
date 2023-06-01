using ToDoListClient.Views;

namespace ToDoListClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("/options", typeof(Options));
        }
    }
}