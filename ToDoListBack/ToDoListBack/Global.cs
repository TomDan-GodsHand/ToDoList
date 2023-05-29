using SqlSugar;
using ToDoListBack.Properties;

namespace ToDoListBack
{
    public class Global
    {
        public static List<User> LoggedUsers = new List<User>();
        public static SqlSugarScope db;

        public static bool StartUp(ref string message)
        {
            try
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = DbType.MySql,
#if DEBUG
                    ConnectionString = $"server={Resources.SqlUrl_};Database={Resources.SqlDateBase_};Uid={Resources.SqlUser_};Pwd={Resources.SqlPassword_}",
#else
                    ConnectionString = $"server={Resources.SqlUrl};Datebase={Resources.SqlDateBase};Uid={Resources.SqlUser};Pwd={Resources.SqlPassword}",
#endif
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
            }
            catch (Exception ex)
            {
                message = ex.Message + "\r\n" + ex.StackTrace;
                return false;
            }
            return true;
        }

        public bool Login()
        {
            var a = db.Queryable<User>().ToList();
            return true;
        }
    }
}