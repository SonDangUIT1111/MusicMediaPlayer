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
        public ICommand SwitchArtist { get; set; }
        public ICommand SwitchAlbum { get; set; }
        public ICommand SwitchGenre { get; set; }

        MainWindow mainWindow;

        public ICommand Logoutcommand { get; set; }
        //view model
        MySong MySongPage { get; set; }
        View.PlayList PlayListPage { get; set; }
        Home HomePage { get; set; }
        Profile ProfilePage { get; set; } 
        Discover_Artist ArtistPage { get; set; }

        Discover_Album AlbumPage { get; set; }
        
        Discover_Genre GenrePage { get; set; }
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
            ArtistPage = new Discover_Artist();
            AlbumPage = new Discover_Album();
            GenrePage = new Discover_Genre();

            //
            var MySongData = MySongPage.DataContext as SongViewModel;
            var PlayListData = PlayListPage.DataContext as PlayListViewModel;
            var HomeData = HomePage.DataContext as HomeViewModel;
            var ProfileData = ProfilePage.DataContext as ProfileViewModel;
            var ArtistData = ArtistPage.DataContext as Discover_ArtistViewModel;
            var AlbumData = AlbumPage.DataContext as Discover_AlbumViewModel;
            var GenreData = GenrePage.DataContext as Discover_GenreViewModel;
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

                    //my song window
                    MySongData.CurrentUser.Id = IDuser[0];
                    MySongData.SkipNextbtn = window.SkipNextbtn;
                    MySongData.SkipPreviousbtn = window.SkipPreviousbtn;
                    MySongData.Playbtn = window.Play;
                    MySongData.Pausebtn = window.Pause;
                    MySongData.InTime = window.InTime;
                    MySongData.TotalTime = window.TotalTime;
                    MySongData.sliProgress = window.sliProgress;
                    MySongData.MainViewProgram = window.MainViewProgram;
                    MySongData.PlayerBar = window.PlayerBar;
                    MySongData.PlayerBarArtist = window.PlayerBarArtist;
                    //artist window
                    ArtistData.CurrentUser.Id = IDuser[0];
                    ArtistData.SkipNextbtn = window.SkipNextbtn2;
                    ArtistData.SkipPreviousbtn = window.SkipPreviousbtn2;
                    ArtistData.Playbtn = window.Play2;
                    ArtistData.Pausebtn = window.Pause2;
                    ArtistData.InTime = window.InTime2;
                    ArtistData.TotalTime = window.TotalTime2;
                    ArtistData.sliProgress = window.sliProgress2;
                    ArtistData.MainViewProgram = window.MainViewProgram;
                    ArtistData.PlayerBarArtist = window.PlayerBarArtist;
                    ArtistData.PlayerBar = window.PlayerBar;

                    //album window
                    AlbumData.CurrentUser.Id = IDuser[0];

                    //Genre window
                    GenreData.CurrentUser.Id = IDuser[0];
                    
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
            SwitchArtist = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = ArtistPage;
            });
            SwitchAlbum = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = AlbumPage;
            });
            SwitchGenre = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = GenrePage;
            });
        }
    }
}