namespace ToDoListClient
{
    public partial class MainPage : ContentPage
    {
        private int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OptionBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("options");
        }

        private void ReConnect_Clicked(object sender, EventArgs e)
        {
        }
    }
}