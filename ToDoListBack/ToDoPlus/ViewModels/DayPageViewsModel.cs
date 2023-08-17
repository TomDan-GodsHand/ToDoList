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
                    var item = new ToDoItem()
                    {
                        Name = EntryText,
                        Completed = false,
                    };
                    ToDoGroup.Items.Add(item);
                    ToDoGroup.UnCompletedItems.Add(item);
                    IsRefreshing = false;
                    OnPropertyChanged();
                    ToDoGroup = ToDoGroup;
                    EntryText = string.Empty;
                });
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                ToDoGroup.UnCompletedItems.Add(new ToDoItem { Name = " asdf", Type = ToDoItemType.Task });
                ToDoGroup.UnCompletedItems.Add(new ToDoItem { Name = " asddsdf", Type = ToDoItemType.Task });
                ToDoGroup.UnCompletedItems.Add(new ToDoItem { Name = " asdaaaaaaf", Type = ToDoItemType.Task, Important = true });

                foreach (var item in ToDoGroup.UnCompletedItems)
                {
                    ToDoGroup.Items.Add(item);
                }
                RefreshItemsAsync();
            });
        }

        public ICommand EntryReturnCommand { private set; get; }
        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());
        public ICommand CompletedCommand => new Command<string>((string id) => CompletedTask(id));

        private async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(2));
            IsRefreshing = false;
        }

        public void ChangeSort(SortEnum sortEnum)
        {
            IsRefreshing = true;
            ToDoGroup.ChangeSort(sortEnum);
            OnPropertyChanged();
            IsRefreshing = false;
        }

        private void CompletedTask(string Id)
        {
            ToDoGroup.SetCompelet(Id);
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