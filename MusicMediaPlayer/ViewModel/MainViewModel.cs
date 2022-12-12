using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class MainViewModel:BaseViewModel,INotifyPropertyChanged
    {
        public bool IsLoaded = false;
        public ICommand LoadedTurnOnLogin { get; set; }
        public CurrentUserAccountModel CurrentUser 
        { 
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(CurrentUser.UserName);
            }
        }

        private CurrentUserAccountModel _currentUser;
        public MainViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            LoadedTurnOnLogin = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                Login login = new Login();
                login.ShowDialog();

                if (login.DataContext == null)
                    return;
                var LoginVM = login.DataContext as LoginViewModel;
                var window = p as MainWindow;
                if (LoginVM.IsLoggedIn == true)
                {
                    //
                    CurrentUser.UserName = LoginVM.Username;
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            }
            );
           
            //

        }
    }
}
