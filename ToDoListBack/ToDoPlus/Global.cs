using ToDoEntity;
using ToDoPlus.Models;

namespace ToDoPlus
{
    public class Global
    {
        public static Color LightWindowBackGroundColor = Color.FromArgb("f3f3f3");
        public static Color DarkWindowBackGroundColor = Color.FromArgb("202020");
        public static Color PrimaryColor = new Color(0, 0, 0);
        private static string userName;
        public static List<ToDoGroup> ToDoGroup { get; set; }
#if WINDOWS
        public static Microsoft.UI.Xaml.Window window { get; set; }
#endif
        public static User User;

        // See "https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetcolorizationcolor"
        [System.Runtime.InteropServices.DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(out int pcrColorization, out bool pfOpaqueBlend);

        public static System.Drawing.Color GetColor(int argb) => System.Drawing.Color.FromArgb(argb);

        public static void Ini()
        {
            User = new User();
            User.Name = "TomDan";
            User.Header = "flag.png";
            ToDoGroup = new List<ToDoGroup>
            {
                new ToDoEntity.ToDoGroup(){
                    Name="Day",
                    Id = "asdfasdf"
                },
                new ToDoEntity.ToDoGroup(){
                    Name="fasdaf",Items = new List<ToDoItem>(),
                    Id = "asdaafasdf"
                }
            };
        }
    }
}