using System.Net.WebSockets;

namespace ToDoListPC
{
    public class ClipBoardClient
    {
        private string id;
        private string name;
        private string password;
        private string serverIp;
        private string serverPort;
        private ClientWebSocket client;
        private CancellationToken cancellationToken = new();
        private Uri connectUri;
        private bool connected = false;

        public ClipBoardClient(string id, string name, string password, string serverIp, string serverPort)
        {
            Id = id;
            Name = name;
            Password = password;
            ServerIp = serverIp;
            ServerPort = serverPort;
            client = new ClientWebSocket();
            var task = Task.Run(async () => { connected = await ConnectWebSocket(); });
            task.ContinueWith(t => { });
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string ServerIp { get => serverIp; set => serverIp = value; }
        public string ServerPort { get => serverPort; set => serverPort = value; }
        public bool Connected { get => connected; }

        private async Task<bool> ConnectWebSocket()
        {
            try
            {
                cancellationToken = CancellationToken.None;
                connectUri = new($"ws://{serverIp}:{serverPort}");
                client.Options.SetRequestHeader("id", id);
                client.Options.SetRequestHeader("name", name);
                client.Options.SetRequestHeader("password", password);
                await client.ConnectAsync(connectUri, cancellationToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}