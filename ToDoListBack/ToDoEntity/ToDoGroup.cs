namespace ToDoEntity
{
    public class ToDoGroup
    {
        private List<ToDoTaskItem> items;
        private string name;
        private string id;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<ToDoTaskItem> Items { get => items; set => items = value; }
    }
}