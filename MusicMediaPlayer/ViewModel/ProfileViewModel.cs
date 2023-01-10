using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class ProfileViewModel:BaseViewModel
    {
        private string _UserName;
        public string UserName { get { return _UserName; } set { _UserName = value; OnPropertyChanged(); } }
        public CurrentUserAccountModel CurrentUser { get; set; }
        public ICommand Click { get; set; }
        public ProfileViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
        }
    }
}
