namespace ToDoListClient
{
    public class ClipBoardClient
    {
        private string id;
        private string name;
        private string password;
        private string serverIp;
        private string serverPort;
        private Uri connectUri;
        private WebSocketClient client;
        private string SaveMesaage;
        private string GetMessage;

        public ClipBoardClient(string id, string name, string password, string serverIp, string serverPort)
        {
            Id = id;
            Name = name;
            Password = password;
            ServerIp = serverIp;
            ServerPort = serverPort;
            Clipboard.Default.ClipboardContentChanged += Clipboard_ClipboardContentChanged;
            Connect();
        }

        private void Connect()
        {
            client = new($"ws://{serverIp}:{serverPort}", id, name, password);
            client.OnOpen -= Client_OnOpen;
            client.OnMessage -= Client_OnMessage;
            client.OnClose -= Client_OnClose;
            client.OnError -= Client_OnError;

            client.OnOpen += Client_OnOpen;
            client.OnMessage += Client_OnMessage;
            client.OnClose += Client_OnClose;
            client.OnError += Client_OnError;
            client.Open();
        }

        private string copyString = "";

        private void Clipboard_ClipboardContentChanged(object sender, EventArgs e)
        {
            if (Clipboard.Default.HasText)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    copyString = await Clipboard.Default.GetTextAsync();
                    if (GetMessage == copyString) return;
                    if (SaveMesaage != copyString)
                    {
                        SaveMesaage = copyString;
                        client.Send(SaveMesaage + 2);
                    }
                });
            }
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string ServerIp { get => serverIp; set => serverIp = value; }
        public string ServerPort { get => serverPort; set => serverPort = value; }

        private void Client_OnError(object sender, Exception ex)
        {
        }

        private void Client_OnClose(object sender, EventArgs e)
        {
        }

        private void Client_OnMessage(object sender, string data)
        {
            //处理的消息错误将会忽略
            try
            {
                data = data.Remove(data.Length - 1);
                if (GetMessage != data)
                {
                    GetMessage = data;
                    MainThread.BeginInvokeOnMainThread(() => { Clipboard.Default.SetTextAsync(GetMessage); });
                }
            }
            catch (Exception ex)
            {
            }
        }

        ~ClipBoardClient()
        {
            client?.Close();
        }

        private void Client_OnOpen(object sender, EventArgs e)
        {
        }

        public void ReConnect()
        {
            if (client.State == System.Net.WebSockets.WebSocketState.Open)
            {
                return;
            }
            else
            {
                client.Open();
            }
        }

        internal void Close()
        {
            client?.Close();
        }
    }
}