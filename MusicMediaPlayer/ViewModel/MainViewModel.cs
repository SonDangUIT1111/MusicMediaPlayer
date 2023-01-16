using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using Prism.Commands;
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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace MusicMediaPlayer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedTurnOnLogin { get; set; }
        public ICommand SwitchMySong { get; set; }
        public ICommand SwitchMyPlayList { get; set; }
        public ICommand SwitchHome { get; set; }
        public ICommand SwitchProfile { get; set; }

        MainWindow mainWindow;

        public ICommand Logoutcommand { get; set; }
        //view model
        MySong MySongPage { get; set; }
        View.PlayList PlayListPage { get; set; }
        Home HomePage { get; set; }
        Profile ProfilePage { get; set; }   
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
            HomePage = new Home();
            ProfilePage = new Profile();

            //
            var MySongData = MySongPage.DataContext as SongViewModel;
            var PlayListData = PlayListPage.DataContext as PlayListViewModel;
            var HomeData = HomePage.DataContext as HomeViewModel;
            var ProfileData = ProfilePage.DataContext as ProfileViewModel;  
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
                    ObservableCollection<int> IDuser = new ObservableCollection<int>(DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == LoginVM.Username).Select(x => x.UserId));
                    MySongData.CurrentUser.Id = IDuser[0];
                    PlayListData.CurrentUser.Id = IDuser[0];
                    HomeData.CurrentUser.Id=IDuser[0];

                    //xu ly profile
                    ProfileData.CurrentUser.Id=IDuser[0];
                    ProfileData.UserName = LoginVM.Username;
                    ProfileData.PassWord = LoginVM.Password;
                    var acc = DataProvider.Ins.DB.UserAccounts.Where((x) => x.UserName == LoginVM.Username).SingleOrDefault();
                    ProfileData.NickName = acc.NickName;
                    ProfileData.Email = acc.UserEmail;
                    //
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
            SwitchHome = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = HomePage;
            });
            SwitchProfile = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = ProfilePage;
            });
        }
    }
}