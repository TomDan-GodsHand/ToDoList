using ToDoEntity;
using ToDoListClient.Models;

namespace ToDoListClient
{
    public class Global
    {
        public static User User { get; set; }
        public static ClipBoardClient client;
        public static HttpClient HttpClient { get; set; }
        public static List<ToDoGroup> ToDoGroup { get; set; }

        public static async void UpdateToDoList()
        {
        }

        public static void Connect()
        {
            User = new User();
            User.Id = "TomDan";
            User.Name = "PC";
            User.Password = "123123";
            if (User is null) { return; }
            client = new ClipBoardClient(User.Id, User.Name, User.Password, "43.136.132.25", "30001");
        }

        internal static void Ini()
        {
            ToDoGroup = new List<ToDoGroup>
            {
                new ToDoEntity.ToDoGroup(){
                    Name="fasdf",Items = new List<ToDoTaskItem>(),
                    Id = "asdfasdf"
                },
                new ToDoEntity.ToDoGroup(){
                    Name="fasdaf",Items = new List<ToDoTaskItem>(),
                    Id = "asdaafasdf"
                }
            };
        }
    }
}