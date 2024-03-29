﻿using MusicMediaPlayer.Model;
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
        public ICommand BackPiano { get; set; }
        public ICommand EnterProgress { get; set; }
        public ICommand LeaveProgress { get; set; }
        public ICommand BackBegin { get; set; }

        public ICommand Logoutcommand { get; set; }
        //view model
        MainWindow mainWindow { get; set; }
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
            //Get Datacontext to passing parameter
            var MySongData = MySongPage.DataContext as SongViewModel;
            var PlayListData = PlayListPage.DataContext as PlayListViewModel;
            var HomeData = HomePage.DataContext as HomeViewModel;
            var ProfileData = ProfilePage.DataContext as ProfileViewModel;
            var ArtistData = ArtistPage.DataContext as Discover_ArtistViewModel;
            var AlbumData = AlbumPage.DataContext as Discover_AlbumViewModel;
            var GenreData = GenrePage.DataContext as Discover_GenreViewModel;
            //Login
            LoadedTurnOnLogin = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mainWindow = p;
                p.HomeButton.IsChecked = true;
                IsLoaded = true;
                if (p == null)
                    return;
                ////////////////////make layout back begining
                if (MySongData.MySongWindow != null)
                {
                    MySongData.MySongWindow.TopTrendExpander.IsExpanded = true;
                    MySongData.MySongWindow.CDCircle.IsExpanded = false;
                    MySongData.CanChangeTOP_CD = !MySongData.CanChangeTOP_CD;
                }
                if (MySongData.mediaPlayer != null)
                {
                    MySongData.mediaPlayer.Stop();
                }
                MySongData.MediaPlayerIsPlaying = false;
                if (PlayListData.mediaPlayer1 != null)
                {
                    PlayListData.mediaPlayer1.Stop();
                }
                PlayListData.MediaPlayerIsPlaying1 = false;
                if (ArtistData.mediaPlayer2 != null)
                {
                    ArtistData.mediaPlayer2.Stop();
                }
                ArtistData.MediaPlayerIsPlaying2 = false;
                if (AlbumData.mediaPlayer3 != null)
                {
                    AlbumData.mediaPlayer3.Stop();
                }
                AlbumData.MediaPlayerIsPlaying3 = false;
                if (GenreData.mediaPlayer4 != null)
                {
                    GenreData.mediaPlayer4.Stop();
                }
                GenreData.MediaPlayerIsPlaying4 = false;
                //sync with playlist
                if (PlayListData.PlayListInsideWindow != null && PlayListData.PlayListInsideWindow.listview != null)
                {
                    PlayListData.PlayListInsideWindow.listview.SelectedIndex = -1;
                    PlayListData.PlayListInsideWindow.Play.IsChecked = false;
                    PlayListData.PlayListInsideWindow.Pause.IsChecked = true;
                }
                if (PlayListData.Playbtn1 != null)
                {
                    PlayListData.Playbtn1.IsChecked = false;
                    PlayListData.Playbtn1.Visibility = Visibility.Visible;
                }
                if (PlayListData.Pausebtn1 != null)
                {
                    PlayListData.Pausebtn1.IsChecked = true;
                    PlayListData.Pausebtn1.Visibility = Visibility.Hidden;
                }
                //sync with artist
                if (ArtistData.ArtistSongWindow != null && ArtistData.ArtistSongWindow.CompositionList != null)
                {
                    ArtistData.ArtistSongWindow.CompositionList.SelectedIndex = -1;
                }
                if (ArtistData.Playbtn2 != null)
                {
                    ArtistData.Playbtn2.IsChecked = false;
                    ArtistData.Playbtn2.Visibility = Visibility.Visible;
                }
                if (ArtistData.Pausebtn2 != null)
                {
                    ArtistData.Pausebtn2.IsChecked = true;
                    ArtistData.Pausebtn2.Visibility = Visibility.Hidden;
                }
                //sync with album
                if (AlbumData.AlbumSongWindow != null && AlbumData.AlbumSongWindow.CompositionList != null)
                {
                    AlbumData.AlbumSongWindow.CompositionList.SelectedIndex = -1;
                }
                if (AlbumData.Playbtn3 != null)
                {
                    AlbumData.Playbtn3.IsChecked = false;
                    AlbumData.Playbtn3.Visibility = Visibility.Visible;
                }
                if (AlbumData.Pausebtn3 != null)
                {
                    AlbumData.Pausebtn3.IsChecked = true;
                    AlbumData.Pausebtn3.Visibility = Visibility.Hidden;
                }
                //sync with genre
                if (GenreData.GenreSongWindow != null && GenreData.GenreSongWindow.CompositionList != null)
                {
                    GenreData.GenreSongWindow.CompositionList.SelectedIndex = -1;
                }
                if (GenreData.Playbtn4 != null)
                {
                    GenreData.Playbtn4.IsChecked = false;
                    GenreData.Playbtn4.Visibility = Visibility.Visible;
                }
                if (GenreData.Pausebtn4 != null)
                {
                    GenreData.Pausebtn4.IsChecked = true;
                    GenreData.Pausebtn4.Visibility = Visibility.Hidden;
                }
                //sync with my song
                if (MySongPage.ListSong != null)
                {
                    MySongPage.ListSong.SelectedIndex = -1;
                }
                if (MySongData.Playbtn != null)
                {
                    MySongData.Playbtn.IsChecked = false;
                    MySongData.Playbtn.Visibility = Visibility.Visible;
                }
                if (MySongData.Pausebtn != null)
                {
                    MySongData.Pausebtn.IsChecked = true;
                    MySongData.Pausebtn.Visibility = Visibility.Hidden;
                }
                if (ArtistData.PlayInvisible != null)
                {
                    ArtistData.PlayInvisible.IsChecked = false;
                }
                if (ArtistData.PauseInvisible != null)
                {
                    ArtistData.PauseInvisible.IsChecked = true;
                }
                //////////////////
                p.MainViewProgram.Height = 760;
                p.PlayerBar.Visibility = Visibility.Hidden;
                p.PlayerBarPlaylist.Visibility = Visibility.Hidden;
                p.PlayerBarArtist.Visibility = Visibility.Hidden;
                p.PlayerBarAlbum.Visibility = Visibility.Hidden;
                p.PlayerBarGenre.Visibility = Visibility.Hidden;
                /////////////////
                p.Hide();
                Login login = new Login();
                login.Account.Text = null;
                login.Password.Password = null;
                var LoginVM = login.DataContext as LoginViewModel;
                LoginVM.IsLoggedIn = false;
                LoginVM.Username = "";
                LoginVM.Password = "";
                login.ShowDialog();
                //Login successfully
                if (login.DataContext == null)
                    return;
                var window = p as MainWindow;
                if (LoginVM.IsLoggedIn == true)
                {
                    //passing information through all page
                    CurrentUser.UserName = LoginVM.Username;
                    ObservableCollection<int> IDuser = new ObservableCollection<int>(DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == LoginVM.Username).Select(x => x.UserId));
                    PlayListData.CurrentUser.Id = IDuser[0];
                    HomeData.CurrentUser.Id = IDuser[0];
                    HomeData.CurrentUser.UserName = LoginVM.Username;
                    ProfileData.CurrentUser.Id = IDuser[0];
                    MySongData.CurrentUser.Id = IDuser[0];
                    ArtistData.CurrentUser.Id = IDuser[0];
                    AlbumData.CurrentUser.Id = IDuser[0];
                    GenreData.CurrentUser.Id = IDuser[0];
                    //passing profile
                    ProfileData.UserName = LoginVM.Username;
                    ProfileData.PassWord = LoginVM.Password;
                    var acc = DataProvider.Ins.DB.UserAccounts.Where((x) => x.UserName == LoginVM.Username).SingleOrDefault();
                    ProfileData.NickName = acc.NickName;
                    ProfileData.Email = acc.UserEmail;
                    //sync avatar
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    MemoryStream stream = new MemoryStream(acc.UserImage);
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    imageBrush.ImageSource = bitmap;
                    imageBrush.Stretch = Stretch.UniformToFill;
                    ProfilePage.AvatarFrame.Background = imageBrush;
                    //passing my song
                    MySongData.SkipNextbtn = window.SkipNextbtn;
                    MySongData.SkipPreviousbtn = window.SkipPreviousbtn;
                    MySongData.Playbtn = window.Play;
                    MySongData.Pausebtn = window.Pause;
                    MySongData.Playbtn1 = window.Play1;
                    MySongData.Pausebtn1 = window.Pause1;
                    MySongData.Playbtn2 = window.Play2;
                    MySongData.Pausebtn2 = window.Pause2;
                    MySongData.Playbtn3 = window.Play3;
                    MySongData.Pausebtn3 = window.Pause3;
                    MySongData.Playbtn4 = window.Play4;
                    MySongData.Pausebtn4 = window.Pause4;
                    MySongData.InTime = window.InTime;
                    MySongData.TotalTime = window.TotalTime;
                    MySongData.sliProgress = window.sliProgress;
                    MySongData.MainViewProgram = window.MainViewProgram;
                    MySongData.PlayerBar = window.PlayerBar;
                    MySongData.PlayerBarPlaylist = window.PlayerBarPlaylist;
                    MySongData.PlayerBarArtist = window.PlayerBarArtist;
                    MySongData.PlayerBarAlbum = window.PlayerBarAlbum;
                    MySongData.PlayerBarGenre = window.PlayerBarGenre;
                    MySongData.mediaPlayer1 = PlayListData.mediaPlayer1;
                    MySongData.mediaPlayer2 = ArtistData.mediaPlayer2;
                    MySongData.mediaPlayer3 = AlbumData.mediaPlayer3;
                    MySongData.mediaPlayer4 = GenreData.mediaPlayer4;
                    MySongData.MediaPlayerIsPlaying1 = PlayListData.MediaPlayerIsPlaying1;
                    MySongData.MediaPlayerIsPlaying2 = ArtistData.MediaPlayerIsPlaying2;
                    MySongData.MediaPlayerIsPlaying3 = AlbumData.MediaPlayerIsPlaying3;
                    MySongData.MediaPlayerIsPlaying4 = GenreData.MediaPlayerIsPlaying4;
                    MySongData.PlayInvisible1 = PlayListData.PlayInvisible1;
                    MySongData.PauseInvisible1 = PlayListData.PauseInvisible1;
                    //playlist window
                    PlayListData.SkipNextbtn = window.SkipNextbtn1;
                    PlayListData.SkipPreviousbtn = window.SkipPreviousbtn1;
                    PlayListData.Playbtn = window.Play;
                    PlayListData.Pausebtn = window.Pause;
                    PlayListData.Playbtn1 = window.Play1;
                    PlayListData.Pausebtn1 = window.Pause1;
                    PlayListData.Playbtn2 = window.Play2;
                    PlayListData.Pausebtn2 = window.Pause2;
                    PlayListData.Playbtn3 = window.Play3;
                    PlayListData.Pausebtn3 = window.Pause3;
                    PlayListData.Playbtn4 = window.Play4;
                    PlayListData.Pausebtn4 = window.Pause4;
                    PlayListData.InTime = window.InTime1;
                    PlayListData.TotalTime = window.TotalTime1;
                    PlayListData.sliProgress = window.sliProgress1;
                    PlayListData.MainViewProgram = window.MainViewProgram;
                    PlayListData.PlayerBar = window.PlayerBar;
                    PlayListData.PlayerBarPlaylist = window.PlayerBarPlaylist;
                    PlayListData.PlayerBarArtist = window.PlayerBarArtist;
                    PlayListData.PlayerBarAlbum = window.PlayerBarAlbum;
                    PlayListData.PlayerBarGenre = window.PlayerBarGenre;
                    PlayListData.mediaPlayer = MySongData.mediaPlayer;
                    PlayListData.mediaPlayer2 = ArtistData.mediaPlayer2;
                    PlayListData.mediaPlayer3 = AlbumData.mediaPlayer3;
                    PlayListData.mediaPlayer4 = GenreData.mediaPlayer4;
                    PlayListData.MediaPlayerIsPlaying = MySongData.MediaPlayerIsPlaying;
                    PlayListData.MediaPlayerIsPlaying2 = ArtistData.MediaPlayerIsPlaying2;
                    PlayListData.MediaPlayerIsPlaying3 = AlbumData.MediaPlayerIsPlaying3;
                    PlayListData.MediaPlayerIsPlaying4 = GenreData.MediaPlayerIsPlaying4;
                    PlayListData.PlayInvisible = MySongPage.Play;
                    PlayListData.PauseInvisible = MySongPage.Pause;
                    //artist window
                    ArtistData.SkipNextbtn = window.SkipNextbtn2;
                    ArtistData.SkipPreviousbtn = window.SkipPreviousbtn2;
                    ArtistData.Playbtn = window.Play;
                    ArtistData.Pausebtn = window.Pause;
                    ArtistData.Playbtn1 = window.Play1;
                    ArtistData.Pausebtn1 = window.Pause1;
                    ArtistData.Playbtn2 = window.Play2;
                    ArtistData.Pausebtn2 = window.Pause2;
                    ArtistData.Playbtn3 = window.Play3;
                    ArtistData.Pausebtn3 = window.Pause3;
                    ArtistData.Playbtn4 = window.Play4;
                    ArtistData.Pausebtn4 = window.Pause4;
                    ArtistData.InTime = window.InTime2;
                    ArtistData.TotalTime = window.TotalTime2;
                    ArtistData.sliProgress = window.sliProgress2;
                    ArtistData.MainViewProgram = window.MainViewProgram;
                    ArtistData.PlayerBar = window.PlayerBar;
                    ArtistData.PlayerBarPlaylist = window.PlayerBarPlaylist;
                    ArtistData.PlayerBarArtist = window.PlayerBarArtist;
                    ArtistData.PlayerBarAlbum = window.PlayerBarAlbum;
                    ArtistData.PlayerBarGenre = window.PlayerBarGenre;
                    ArtistData.mediaPlayer1 = PlayListData.mediaPlayer1;
                    ArtistData.mediaPlayer = MySongData.mediaPlayer;
                    ArtistData.mediaPlayer3 = AlbumData.mediaPlayer3;
                    ArtistData.mediaPlayer4 = GenreData.mediaPlayer4;
                    ArtistData.MediaPlayerIsPlaying1 = PlayListData.MediaPlayerIsPlaying1;
                    ArtistData.MediaPlayerIsPlaying = MySongData.MediaPlayerIsPlaying;
                    ArtistData.MediaPlayerIsPlaying3 = AlbumData.MediaPlayerIsPlaying3;
                    ArtistData.MediaPlayerIsPlaying4 = GenreData.MediaPlayerIsPlaying4;
                    ArtistData.PlayInvisible = MySongPage.Play;
                    ArtistData.PauseInvisible = MySongPage.Pause;
                    ArtistData.PlayInvisible1 = PlayListData.PlayInvisible1;
                    ArtistData.PauseInvisible1 = PlayListData.PauseInvisible1;
                    //album window
                    AlbumData.SkipNextbtn = window.SkipNextbtn3;
                    AlbumData.SkipPreviousbtn = window.SkipPreviousbtn3;
                    AlbumData.Playbtn = window.Play;
                    AlbumData.Pausebtn = window.Pause;
                    AlbumData.Playbtn1 = window.Play1;
                    AlbumData.Pausebtn1 = window.Pause1;
                    AlbumData.Playbtn2 = window.Play2;
                    AlbumData.Pausebtn2 = window.Pause2;
                    AlbumData.Playbtn3 = window.Play3;
                    AlbumData.Pausebtn3 = window.Pause3;
                    AlbumData.Playbtn4 = window.Play4;
                    AlbumData.Pausebtn4 = window.Pause4;
                    AlbumData.InTime = window.InTime3;
                    AlbumData.TotalTime = window.TotalTime3;
                    AlbumData.sliProgress = window.sliProgress3;
                    AlbumData.MainViewProgram = window.MainViewProgram;
                    AlbumData.PlayerBar = window.PlayerBar;
                    AlbumData.PlayerBarPlaylist = window.PlayerBarPlaylist;
                    AlbumData.PlayerBarArtist = window.PlayerBarArtist;
                    AlbumData.PlayerBarAlbum = window.PlayerBarAlbum;
                    AlbumData.PlayerBarGenre = window.PlayerBarGenre;
                    AlbumData.mediaPlayer1 = PlayListData.mediaPlayer1;
                    AlbumData.mediaPlayer = MySongData.mediaPlayer;
                    AlbumData.mediaPlayer2 = ArtistData.mediaPlayer2;
                    AlbumData.mediaPlayer4 = GenreData.mediaPlayer4;
                    AlbumData.MediaPlayerIsPlaying1 = PlayListData.MediaPlayerIsPlaying1;
                    AlbumData.MediaPlayerIsPlaying = MySongData.MediaPlayerIsPlaying;
                    AlbumData.MediaPlayerIsPlaying2 = ArtistData.MediaPlayerIsPlaying2;
                    AlbumData.MediaPlayerIsPlaying4 = GenreData.MediaPlayerIsPlaying4;
                    AlbumData.PlayInvisible = MySongPage.Play;
                    AlbumData.PauseInvisible = MySongPage.Pause;
                    AlbumData.PlayInvisible1 = PlayListData.PlayInvisible1;
                    AlbumData.PauseInvisible1 = PlayListData.PauseInvisible1;
                    //Genre window
                    GenreData.SkipNextbtn = window.SkipNextbtn4;
                    GenreData.SkipPreviousbtn = window.SkipPreviousbtn4;
                    GenreData.Playbtn = window.Play;
                    GenreData.Pausebtn = window.Pause;
                    GenreData.Playbtn1 = window.Play1;
                    GenreData.Pausebtn1 = window.Pause1;
                    GenreData.Playbtn2 = window.Play2;
                    GenreData.Pausebtn2 = window.Pause2;
                    GenreData.Playbtn3 = window.Play3;
                    GenreData.Pausebtn3 = window.Pause3;
                    GenreData.Playbtn4 = window.Play4;
                    GenreData.Pausebtn4 = window.Pause4;
                    GenreData.InTime = window.InTime4;
                    GenreData.TotalTime = window.TotalTime4;
                    GenreData.sliProgress = window.sliProgress4;
                    GenreData.MainViewProgram = window.MainViewProgram;
                    GenreData.PlayerBar = window.PlayerBar;
                    GenreData.PlayerBarPlaylist = window.PlayerBarPlaylist;
                    GenreData.PlayerBarArtist = window.PlayerBarArtist;
                    GenreData.PlayerBarAlbum = window.PlayerBarAlbum;
                    GenreData.PlayerBarGenre = window.PlayerBarGenre;
                    GenreData.mediaPlayer1 = PlayListData.mediaPlayer1;
                    GenreData.mediaPlayer = MySongData.mediaPlayer;
                    GenreData.mediaPlayer2 = ArtistData.mediaPlayer2;
                    GenreData.mediaPlayer3 = AlbumData.mediaPlayer3;
                    GenreData.MediaPlayerIsPlaying1 = PlayListData.MediaPlayerIsPlaying1;
                    GenreData.MediaPlayerIsPlaying = MySongData.MediaPlayerIsPlaying;
                    GenreData.MediaPlayerIsPlaying2 = ArtistData.MediaPlayerIsPlaying2;
                    GenreData.MediaPlayerIsPlaying3 = AlbumData.MediaPlayerIsPlaying3;
                    GenreData.PlayInvisible = MySongPage.Play;
                    GenreData.PauseInvisible = MySongPage.Pause;
                    GenreData.PlayInvisible1 = PlayListData.PlayInvisible1;
                    GenreData.PauseInvisible1 = PlayListData.PauseInvisible1;

                    p.FrameView.Content = HomePage;
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
            EnterProgress = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            {
                p.Focus();
            });
            LeaveProgress = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                p.Focus();
            });
            BackBegin = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                if (MySongData.MySongWindow != null)
                {
                    MySongData.MySongWindow.TopTrendExpander.IsExpanded = true;
                    MySongData.MySongWindow.CDCircle.IsExpanded = false;
                    MySongData.CanChangeTOP_CD = !MySongData.CanChangeTOP_CD;
                }
                if (MySongData.mediaPlayer != null)
                {
                    MySongData.mediaPlayer.Stop();
                }
                MySongData.MediaPlayerIsPlaying = false;
                if (PlayListData.mediaPlayer1 != null)
                {
                    PlayListData.mediaPlayer1.Stop();
                }
                PlayListData.MediaPlayerIsPlaying1 = false;
                if (ArtistData.mediaPlayer2 != null)
                {
                    ArtistData.mediaPlayer2.Stop();
                }
                ArtistData.MediaPlayerIsPlaying2 = false;
                if (AlbumData.mediaPlayer3 != null)
                {
                    AlbumData.mediaPlayer3.Stop();
                }
                AlbumData.MediaPlayerIsPlaying3 = false;
                if (GenreData.mediaPlayer4 != null)
                {
                    GenreData.mediaPlayer4.Stop();
                }
                GenreData.MediaPlayerIsPlaying4 = false;
                //sync with playlist
                if (PlayListData.PlayListInsideWindow != null && PlayListData.PlayListInsideWindow.listview != null)
                {
                    PlayListData.PlayListInsideWindow.listview.SelectedIndex = -1;
                    PlayListData.PlayListInsideWindow.Play.IsChecked = false;
                    PlayListData.PlayListInsideWindow.Pause.IsChecked = true;
                }
                PlayListData.Playbtn1.IsChecked = false;
                PlayListData.Pausebtn1.IsChecked = true;
                PlayListData.Playbtn1.Visibility = Visibility.Visible;
                PlayListData.Pausebtn1.Visibility = Visibility.Hidden;
                //sync with artist
                if (ArtistData.ArtistSongWindow != null && ArtistData.ArtistSongWindow.CompositionList != null)
                {
                    ArtistData.ArtistSongWindow.CompositionList.SelectedIndex = -1;
                }
                ArtistData.Playbtn2.IsChecked = false;
                ArtistData.Pausebtn2.IsChecked = true;
                ArtistData.Playbtn2.Visibility = Visibility.Visible;
                ArtistData.Pausebtn2.Visibility = Visibility.Hidden;
                //sync with album
                if (AlbumData.AlbumSongWindow != null && AlbumData.AlbumSongWindow.CompositionList != null)
                {
                    AlbumData.AlbumSongWindow.CompositionList.SelectedIndex = -1;
                }
                AlbumData.Playbtn3.IsChecked = false;
                AlbumData.Pausebtn3.IsChecked = true;
                AlbumData.Playbtn3.Visibility = Visibility.Visible;
                AlbumData.Pausebtn3.Visibility = Visibility.Hidden;
                //sync with genre
                if (GenreData.GenreSongWindow != null && GenreData.GenreSongWindow.CompositionList != null)
                {
                    GenreData.GenreSongWindow.CompositionList.SelectedIndex = -1;
                }
                GenreData.Playbtn4.IsChecked = false;
                GenreData.Pausebtn4.IsChecked = true;
                GenreData.Playbtn4.Visibility = Visibility.Visible;
                GenreData.Pausebtn4.Visibility = Visibility.Hidden;
                //sync with my song
                if (MySongPage.ListSong != null)
                {
                    MySongPage.ListSong.SelectedIndex = -1;
                }
                MySongData.Playbtn.IsChecked = false;
                MySongData.Pausebtn.IsChecked = true;
                MySongData.Playbtn.Visibility = Visibility.Visible;
                MySongData.Pausebtn.Visibility = Visibility.Hidden;
                ArtistData.PlayInvisible.IsChecked = false;
                ArtistData.PauseInvisible.IsChecked = true;
                //
                p.MainViewProgram.Height = 760;
                p.PlayerBar.Visibility = Visibility.Hidden;
                p.PlayerBarPlaylist.Visibility = Visibility.Hidden;
                p.PlayerBarArtist.Visibility = Visibility.Hidden;
                p.PlayerBarAlbum.Visibility = Visibility.Hidden;
                p.PlayerBarGenre.Visibility = Visibility.Hidden;
            });
        }
    }
}