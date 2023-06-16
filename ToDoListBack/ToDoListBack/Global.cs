using Fleck;
using SqlSugar;
using ToDoListBack.Properties;

namespace ToDoListBack
{
    public class Global
    {
        public static Dictionary<string, User> LoggedUsers = new Dictionary<string, User>();
        public static SqlSugarScope db;
        private static WebSocketServer Server;
        private static IDictionary<string, IWebSocketConnection> dic_Sockets = new Dictionary<string, IWebSocketConnection>();
        private static Dictionary<string, User> UserHaveSocket = new Dictionary<string, User>();

        private static Action<IWebSocketConnection> config = socket =>
        {
            socket.OnOpen = () =>
            {
                string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;

                var headers = socket.ConnectionInfo.Headers;
                if (Login(headers, clientUrl, socket))
                {
                    dic_Sockets.Add(clientUrl, socket);
                    Console.WriteLine(DateTime.Now.ToString() + "|服务器:和客户端网页:" + clientUrl + " 建立WebSock连接！");
                }
                else
                {
                    socket.Send("密码错误");
                    socket.Close();
                }
            };
            socket.OnClose = () =>
            {
                string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                //如果存在这个客户端,那么对这个socket进行移除
                if (dic_Sockets.ContainsKey(clientUrl))
                {
                    //注:Fleck中有释放
                    //关闭对象连接
                    //if (dic_Sockets[clientUrl] != null)
                    //{
                    //dic_Sockets[clientUrl].Close();
                    //}
                    dic_Sockets.Remove(clientUrl);
                }
                Console.WriteLine(DateTime.Now.ToString() + "|服务器:和客户端网页:" + clientUrl + " 断开WebSock连接！");
            };
            socket.OnMessage = message =>  //接受客户端网页消息事件
            {
                GetMessage(message, socket);
            };
            socket.OnError = Exception =>
            {
                Console.WriteLine(Exception.Message);
            };
        };

        private static bool Login(IDictionary<string, string> headers, string clientUrl, IWebSocketConnection socket)
        {
            if (!LoggedUsers.ContainsKey(headers["id"]))
            {
                User user = new(headers["id"], headers["name"], headers["password"]);
                user.AddDev(clientUrl, socket);
                LoggedUsers.Add(headers["id"], user);
                UserHaveSocket.Add(clientUrl, user);
                Console.WriteLine($"User:{headers["name"]}注册并添加至{user.Id}");
            }
            else
            {
                User user = LoggedUsers[headers["id"]];
                if (user.Password != headers["password"]) return false;
                LoggedUsers[headers["id"]].AddDev(clientUrl, socket);
                UserHaveSocket.Add(clientUrl, user);
                Console.WriteLine($"User:{headers["name"]}注册并添加至{user.Id}");
            }
            return true;
        }

        private static void GetMessage(string message, IWebSocketConnection socket)
        {
            string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
            int flag = int.Parse(message.Last().ToString());
            MessageType messageType = (MessageType)flag;
            var headers = socket.ConnectionInfo.Headers;
            switch (messageType)
            {
                case MessageType.Delete:
                    //Login(message, clientUrl, socket);
                    //socket.Send("Success" + (int)messageType);
                    break;

                case MessageType.Add:
                    UpDataClipBoardToUser(message, clientUrl, headers["id"]);
                    break;

                case MessageType.UpdateToDoList:
                    UpdateToDoList(message, clientUrl, headers["id"]);
                    break;
            }

            Console.WriteLine(DateTime.Now.ToString() + "|服务器:【收到】来客户端网页:" + clientUrl + "的信息：\n" + message);
        }

        private static void UpdateToDoList(string message, string clientUrl, string id)
        {
            throw new NotImplementedException();
        }

        private static void UpDataClipBoardToUser(string message, string clientUrl, string id)
        {
            if (LoggedUsers.ContainsKey(id))
            {
                LoggedUsers[id].Update(message.Remove(message.Length - 1), clientUrl);
            }
        }

        public static bool StartUp(ref string message)
        {
            try
            {
                #region 数据库相关

                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = DbType.SqlServer,

                    ConnectionString = $"server={Resources.SqlUrl};Uid={Resources.SqlUser_};Pwd={Resources.SqlPassword};Database={Resources.SqlDateBase};",

                    IsAutoCloseConnection = true
                },
                db =>
                {
                    //单例参数配置，所有上下文生效
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        //Console.WriteLine(sql);//输出sql
                    };
                });
                db = sqlSugar;

                #endregion 数据库相关

#if DEBUG
                Server = new WebSocketServer("ws://0.0.0.0:30001");
#else
                Server = new WebSocketServer("ws://0.0.0.0:30001");
#endif

                Server.RestartAfterListenError = true;

                Server.Start(config);
            }
            catch (Exception ex)
            {
                message = ex.Message + "\r\n" + ex.StackTrace;
                return false;
            }
            return true;
        }
    }
}