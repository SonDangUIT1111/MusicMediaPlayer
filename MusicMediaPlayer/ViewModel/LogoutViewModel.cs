using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class LogoutViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; set; }
        public LogoutViewModel()
        {
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Logout logout = p as Logout;
                logout.Close();
            });
        }
    }
}
