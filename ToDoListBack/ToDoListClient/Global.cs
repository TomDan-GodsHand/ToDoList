using ToDoListPC;

namespace ToDoListClient
{
    public class Global
    {
        private static string id = "TomDan";
        private static string name = "TomDan";
        private static string password = "123123";
        public static ClipBoardClient client;
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }

        public static void Connect()
        {
            client = new ClipBoardClient(id, name, password, "43.136.132.25", "30001");
        }
    }
}