using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;

namespace MusicMediaPlayer.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; set; }
        
        public ChangePasswordViewModel()
        {
            CancelCommand = new RelayCommand<Window>((p) => { return  true; }, (p) =>
            {
               ChangePassword change = p as ChangePassword;
                change.Close();
            });
        }        
    }
}
