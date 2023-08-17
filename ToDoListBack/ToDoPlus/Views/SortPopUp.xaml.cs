using CommunityToolkit.Maui.Views;
using ToDoEntity;

namespace ToDoPlus.Views;

public partial class SortPopUp : Popup
{
    public SortPopUp()
    {
        InitializeComponent();
    }

    private void OnPickerSelectionChanged(object sender, EventArgs e)
    {
        Picker picker = sender as Picker;
        SortEnum sortEnum = (SortEnum)picker.SelectedIndex;
    }
}