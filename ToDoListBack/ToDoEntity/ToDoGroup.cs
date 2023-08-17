using System.Collections.ObjectModel;

namespace ToDoEntity
{
    public class ToDoGroup
    {
        private List<ToDoItem> items;
        private ObservableCollection<ToDoItem> completedItems;
        private ObservableCollection<ToDoItem> unCompletedItems;
        private string name;
        private string id;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public List<ToDoItem> Items
        {
            get
            {
                if (items is null)
                {
                    items = new List<ToDoItem>();
                }
                return items;
            }
            set => items = value;
        }

        public ObservableCollection<ToDoItem> CompletedItems
        {
            get
            {
                if (completedItems == null)
                {
                    completedItems = new ObservableCollection<ToDoItem>();
                    foreach (var item in Items)
                    {
                        if (item.Completed)
                            completedItems.Add(item);
                    }
                }
                return completedItems;
            }

            set
            {
                completedItems = value;
            }
        }

        public ObservableCollection<ToDoItem> UnCompletedItems

        {
            get
            {
                if (unCompletedItems == null)
                {
                    unCompletedItems = new ObservableCollection<ToDoItem>();
                    foreach (var item in Items)
                    {
                        if (!item.Completed)
                            unCompletedItems.Add(item);
                    }
                }
                return unCompletedItems;
            }
            set
            {
                unCompletedItems = value;
            }
        }

        public void ChangeSort(SortEnum sortEnum)
        {
            ObservableCollection<ToDoItem> it = new();
            switch (sortEnum)
            {
                case SortEnum.CreateDate:
                    it = new(unCompletedItems.OrderBy(it => it.CreateDate));
                    break;

                case SortEnum.Important:
                    it = new(unCompletedItems.OrderByDescending(it => it.Important));
                    break;

                case SortEnum.DeadLine:
                    it = new(unCompletedItems.OrderBy(it => it.DeadLine));
                    break;

                case SortEnum.FristCode:
                    it = new(unCompletedItems.OrderBy(it => it.Name));
                    break;

                default:
                    it = new(unCompletedItems.OrderBy(it => it.CreateDate));
                    break;
            }
            unCompletedItems.Clear();
            foreach (var item in it)
                unCompletedItems.Add(item);
        }

        public void SetCompelet(string id)
        {
            var selectedItem = items.Find(it => it.Id == id);

            var q = unCompletedItems.Remove(selectedItem);
            CompletedItems.Add(selectedItem);
            selectedItem.SetCompleted();
        }
    }
}