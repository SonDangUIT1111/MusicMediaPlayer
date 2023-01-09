using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedTurnOnLogin { get; set; }  
        public ICommand SwitchMySong { get; set; }
        public ICommand SwitchMyPlayList { get; set; }
        //view model
        MySong MySongPage { get; set; }
        View.PlayList PlayListPage { get; set; }

        //information
        public CurrentUserAccountModel CurrentUser 
        { 
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }
        private CurrentUserAccountModel _currentUser;
        public MainViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            MySongPage = new MySong();
            PlayListPage = new View.PlayList();
            //
            var MySongData = MySongPage.DataContext as SongViewModel;
            var PlayListData = PlayListPage.DataContext as PlayListViewModel;
            //
            LoadedTurnOnLogin = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                Login login = new Login();
                login.ShowDialog();
                //sau khi dang nhap
                if (login.DataContext == null)
                    return;
                var LoginVM = login.DataContext as LoginViewModel;
                var window = p as MainWindow;
                if (LoginVM.IsLoggedIn == true)
                {
                    //truyen du lieu qua cac view
                    CurrentUser.UserName = LoginVM.Username;
                    ObservableCollection<int> IDuser = new ObservableCollection<int>(DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == LoginVM.Username).Select(x=>x.UserId));
                    MySongData.CurrentUser.Id = IDuser[0];
                    MySongData.MySongPlayerBar = window.PlayMusicBar;
                    MySongData.MainWindow = window;
                    PlayListData.CurrentUser.Id = IDuser[0];
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            }
            );
            SwitchMySong = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {  
                p.Content = MySongPage;
            });
            SwitchMyPlayList = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = PlayListPage;
            });

        }
    }
}
