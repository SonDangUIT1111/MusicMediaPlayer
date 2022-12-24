using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography.Xml;
using MusicMediaPlayer.View;

namespace MusicMediaPlayer.ViewModel
{
    public class UCProfileViewModel : BaseViewModel
    {
        public ICommand CPCommand { get; set; }
        public UCProfileViewModel()
        {
            CPCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                ChangePassword change = new ChangePassword();
                change.ShowDialog();
            });
        }
        
    }
}
