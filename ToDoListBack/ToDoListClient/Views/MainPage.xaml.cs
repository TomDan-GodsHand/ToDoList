using ToDoEntity;

namespace ToDoListClient
{
    public enum SortEnum
    {
        Important = 0,
        DeadLine = 1,
        CreateTime = 3,
        Character = 4
    }

    public partial class MainPage : ContentPage
    {
        private int count = 0;
        private ToDoGroup ToDoGroup;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ToDoGroup = this.BindingContext as ToDoGroup;
        }

        private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
        {
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
    }
}