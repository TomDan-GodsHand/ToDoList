using CommunityToolkit.Maui.Views;
using ToDoEntity;
using ToDoPlus.ViewModels;

namespace ToDoPlus.Views;

public partial class DayPage : ContentPage
{
    public DayPage()
    {
        InitializeComponent();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var popUp = new SortPopUp();
        var result = await this.ShowPopupAsync(popUp);
    }

    private void OnPickerSelectionChanged(object sender, EventArgs e)
    {
        Picker picker = sender as Picker;
        SortEnum sortEnum = (SortEnum)picker.SelectedIndex;
        (BindingContext as DayPageViewsModel).ChangeSort(sortEnum);
    }
}