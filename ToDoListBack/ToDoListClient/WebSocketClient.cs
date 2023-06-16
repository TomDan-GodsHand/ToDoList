using System.Net.WebSockets;
using System.Text;

namespace ToDoListClient
{
    public class WebSocketClient
    {
        private ClientWebSocket client;
        private Uri uri;
        private bool isUserClose;
        private string id;
        private string name;
        private string password;

        /// <summary>
        /// WebSocket状态
        /// </summary>
        public WebSocketState? State { get => client?.State; }

        public delegate void MessageEventHandler(object sender, string data);

        public delegate void ErrorEventHandler(object sender, Exception ex);

        /// <summary>
        /// 连接时触发
        /// </summary>
        public event EventHandler OnOpen;

        /// <summary>
        /// 连接关闭时触发
        /// </summary>
        public event EventHandler OnClose;

        /// <summary>
        /// 通讯错误时触发
        /// </summary>
        public event ErrorEventHandler OnError;

        /// <summary>
        /// 接受消息时触发
        /// </summary>
        public event MessageEventHandler OnMessage;

        public WebSocketClient(string uri, string id, string name, string password)
        {
            this.uri = new Uri(uri);
            client = new ClientWebSocket();
            this.id = id;
            this.name = name;
            this.password = password;
        }

        public void Open()
        {
            Task.Run(async () =>
            {
                if (client.State == WebSocketState.Connecting || client.State == WebSocketState.Open)
                    return;
                string netErr = string.Empty;
                try
                {
                    isUserClose = false;
                    client = new ClientWebSocket();
                    client.Options.SetRequestHeader("id", id);
                    client.Options.SetRequestHeader("name", name);
                    client.Options.SetRequestHeader("password", password);
                    await client.ConnectAsync(uri, CancellationToken.None);

                    if (OnOpen is not null)
                        OnOpen(client, new EventArgs());
                    //全部消息容器
                    List<byte> bs = new List<byte>();
                    //缓冲区
                    var buffer = new byte[1024 * 4];
                    //监听Socket信息
                    WebSocketReceiveResult result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    //是否关闭
                    while (!result.CloseStatus.HasValue)
                    {
                        //文本消息
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            bs.AddRange(buffer.Take(result.Count));

                            //消息是否已接收完全
                            if (result.EndOfMessage)
                            {
                                //发送过来的消息
                                string userMsg = Encoding.UTF8.GetString(bs.ToArray(), 0, bs.Count);

                                if (OnMessage != null)
                                    OnMessage(client, userMsg);

                                //清空消息容器
                                bs = new List<byte>();
                            }
                        }
                        //继续监听Socket信息
                        result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    }
                    ////关闭WebSocket（服务端发起）
                    await client.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    netErr = " .Net发生错误" + ex.Message;

                    if (OnError != null)
                        OnError(client, ex);

                    //if (ws != null && ws.State == WebSocketState.Open)
                    //    //关闭WebSocket（客户端发起）
                    //    await ws.CloseAsync(WebSocketCloseStatus.Empty, ex.Message, CancellationToken.None);
                }
                finally
                {
                    if (!isUserClose)
                        Close(client.CloseStatus.Value, client.CloseStatusDescription + netErr);
                }
            });
        }

        /// <summary>
        /// 使用连接发送文本消息
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="mess"></param>
        /// <returns>是否尝试了发送</returns>
        public bool Send(string mess)
        {
            if (client.State != WebSocketState.Open)
                return false;

            Task.Run(async () =>
            {
                var replyMess = Encoding.UTF8.GetBytes(mess);
                //发送消息
                await client.SendAsync(new ArraySegment<byte>(replyMess), WebSocketMessageType.Text, true, CancellationToken.None);
            });

            return true;
        }

        /// <summary>
        /// 使用连接发送字节消息
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="mess"></param>
        /// <returns>是否尝试了发送</returns>
        public bool Send(byte[] bytes)
        {
            if (client.State != WebSocketState.Open)
                return false;

            Task.Run(async () =>
            {
                //发送消息
                await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Binary, true, CancellationToken.None);
            });

            return true;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            isUserClose = true;
            Close(WebSocketCloseStatus.NormalClosure, "用户手动关闭");
        }

        public void Close(WebSocketCloseStatus closeStatus, string statusDescription)
        {
            Task.Run(async () =>
            {
                try
                {
                    //关闭WebSocket（客户端发起）
                    await client.CloseAsync(closeStatus, statusDescription, CancellationToken.None);
                }
                catch (Exception ex)
                {
                }

                client.Abort();
                client.Dispose();

                if (OnClose != null)
                    OnClose(client, new EventArgs());
            });
        }
    }
}