using Fleck;

namespace ToDoListBack
{
    public class User
    {
        private string id;
        private string name;
        private string password;
        private IDictionary<string, IWebSocketConnection> dic_Sockets = new Dictionary<string, IWebSocketConnection>();
        private List<string> ClipBoardMessage = new List<string>();
        public string Password { get => password; }
        public string Name { get => name; }
        public string Id { get => id; }
        public IDictionary<string, IWebSocketConnection> Dic_Sockets { get => dic_Sockets; }

        public User(string id, string name, string password)
        {
            this.id = id;
            this.name = name;

            this.password = password;
        }

        public bool AddDev(string devId, IWebSocketConnection socket)
        {
            try
            {
                if (!dic_Sockets.ContainsKey(devId))
                {
                    dic_Sockets.Add(devId, socket);
                }
                else return false;
            }
            catch { return false; }
            return true;
        }

        public void SendMessage(string message)
        {
            foreach (var socket in dic_Sockets.Values)
            {
                socket.Send(message);
            }
        }

        public void SyncClipBoard(string url)
        {
            foreach (var socketKey in dic_Sockets)
            {
                if (socketKey.Key == url)
                {
                    continue;
                }
                foreach (var messages in ClipBoardMessage)
                {
                    socketKey.Value.Send(messages + MessageType.Add);
                    Console.WriteLine($"向{socketKey}发送{messages + MessageType.Add}");
                }
            }
        }

        public void Update(string messages, string url)
        {
            if (ClipBoardMessage.Contains(messages)) return;
            ClipBoardMessage.Add(messages);
            SyncClipBoard(url);
        }
    }
}