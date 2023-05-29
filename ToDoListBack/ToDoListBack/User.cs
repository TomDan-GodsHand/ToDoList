namespace ToDoListBack
{
    public class User
    {
        private int id;
        private string name;
        private string password;
        private List<string> urls;

        public List<string> Urls { get => urls; }
        public string Password { get => password; }
        public string Name { get => name; }
        public int Id { get => id; }

        public User(int id, string name, string password)
        {
            this.id = id;
            this.name = name;
            urls = new List<string>();
            this.password = password;
        }

        public bool AddUrl(string url)
        {
            try
            {
                if (!urls.Contains(url))
                {
                    urls.Add(url);
                }
                else return false;
            }
            catch { return false; }
            return true;
        }

        public void Update(List<string> messages)
        {
            throw new NotImplementedException();
        }
    }
}