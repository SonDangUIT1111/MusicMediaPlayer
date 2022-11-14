using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LogintoRegister;
        public MainViewModel()
        {
           //
                IsLoaded = true;
                Login login = new Login();
                login.ShowDialog();
           //
           LogintoRegister = new RelayCommand<object>((p) => { return true;  }, (p) => {
                Register register =  new Register();
                register.ShowDialog();
           });
           
        }
    }
}
