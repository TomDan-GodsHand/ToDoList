namespace ToDoListClient.ViewModels
{
    public class MyEntry : ContentView
    {
        public static readonly BindableProperty FirstButtonWidthP = BindableProperty.Create(nameof(FirstButtonWidth), typeof(int), typeof(int), 20);
        public int FirstButtonWidth { set; get; }
    }
}