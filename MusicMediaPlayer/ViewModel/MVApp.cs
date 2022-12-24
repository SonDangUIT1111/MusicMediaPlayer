using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
 
        public class MVApp : BaseViewModel
        {
            private object _currentView;
            public object CurrentView
            {
                get { return _currentView; }
                set { _currentView = value; OnPropertyChanged(); }
            }
            public ICommand ProfileCommand { get; set; }
            public ICommand HomeCommand { get; set; }
            private void Profile(object obj) => CurrentView = new UCProfileViewModel();
            private void Home(object obj) => CurrentView = new UCHomeViewModel();
            public MVApp()
            {
                HomeCommand = new RelayCommand(Home);
                ProfileCommand = new RelayCommand(Profile);

                CurrentView = new UCHomeViewModel();
            }
        }
}

