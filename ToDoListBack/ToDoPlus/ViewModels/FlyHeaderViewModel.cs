using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoPlus.Models;

namespace ToDoPlus.ViewModels
{
    public class FlyHeaderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private User user;

        public User User
        {
            get
            {
                if (user is null)
                {
                    user = new User();
                    user = Global.User;
                }
                return user;
            }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }
    }
}