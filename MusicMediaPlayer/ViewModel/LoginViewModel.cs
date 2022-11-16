using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        public ICommand LogintoRegister { get; set; }
        public LoginViewModel()
        {
            LogintoRegister = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Register register = new Register();
                register.ShowDialog();
            });

        }
    }
}
