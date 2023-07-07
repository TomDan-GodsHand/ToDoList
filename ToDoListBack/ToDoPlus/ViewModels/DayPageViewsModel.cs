using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDoEntity;

namespace ToDoPlus.ViewModels
{
    public class DayPageViewsModel : INotifyPropertyChanged
    {
        private DateTime datetime;
        private ToDoGroup toDoGroup;
        private string entryText;
        private bool isRefreshing;

        public DayPageViewsModel()
        {
            EntryReturnCommand = new Command(
                execute: () =>
                {
                    IsRefreshing = true;
                    ToDoGroup.Items.Add(new ToDoTaskItem()
                    {
                        Id = 1231321321,
                        Name = EntryText,
                    });
                    IsRefreshing = false;
                    OnPropertyChanged();
                    ToDoGroup = ToDoGroup;
                    EntryText = string.Empty;
                });
        }

        public ICommand EntryReturnCommand { private set; get; }
        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());

        private async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(2));
            IsRefreshing = false;
        }

        public DateTime Datetime
        {
            get => DateTime.Today;
            set
            {
                if (datetime != value)
                {
                    datetime = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }

        public ToDoGroup ToDoGroup
        {
            get
            {
                if (toDoGroup is null)
                {
                    toDoGroup = new ToDoGroup();
                    toDoGroup = Global.ToDoGroup.Find(it => it.Name == "Day");
                }
                return toDoGroup;
            }
            set
            {
                if (toDoGroup != value)
                {
                    toDoGroup = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }

        public string EntryText
        {
            get => entryText;
            set
            {
                if (entryText != value)
                { entryText = value; OnPropertyChanged(); }
            }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}