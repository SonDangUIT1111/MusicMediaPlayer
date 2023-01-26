using MaterialDesignThemes.Wpf;
using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace MusicMediaPlayer.ViewModel
{
    public class PlayListViewModel : BaseViewModel
    {
        #region commands
        public ICommand AddPL { get; set; }

        public ICommand Rename { get; set; }

        public ICommand Delete { get; set; }

        public ICommand TextChanged { get; set; }

        public ICommand Load { get; set; }

        public ICommand MouseDoubleClick { get; set; }

        public ICommand AZ { get; set; }

        public ICommand ZA { get; set; }

        #endregion

        private ObservableCollection<MusicMediaPlayer.Model.PlayList> _List;
        public ObservableCollection<MusicMediaPlayer.Model.PlayList> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private CurrentUserAccountModel _CurrentUser;
        public CurrentUserAccountModel CurrentUser { get => _CurrentUser; set { _CurrentUser = value; } }

        private string _Search;
        public string Search { get => _Search; set { _Search = value; OnPropertyChanged(); } }

        private MusicMediaPlayer.Model.PlayList _ItemDoubleClick;
        public MusicMediaPlayer.Model.PlayList ItemDoubleClick { get => _ItemDoubleClick; set { _ItemDoubleClick = value; OnPropertyChanged(); } }

        public View.PlayList page;

        public bool IsSort = false;

        //passing parameter
        public MediaPlayer mediaPlayer = new MediaPlayer();
        public MediaPlayer mediaPlayer1 = new MediaPlayer();
        public MediaPlayer mediaPlayer2 = new MediaPlayer();
        public MediaPlayer mediaPlayer3 = new MediaPlayer();
        public MediaPlayer mediaPlayer4 = new MediaPlayer();
        private bool _mediaPlayerIsPlaying = false;
        public bool MediaPlayerIsPlaying { get => _mediaPlayerIsPlaying; set => _mediaPlayerIsPlaying = value; }
        private bool _mediaPlayerIsPlaying1 = false;
        public bool MediaPlayerIsPlaying1 { get => _mediaPlayerIsPlaying1; set => _mediaPlayerIsPlaying1 = value; }

        private bool _mediaPlayerIsPlaying2 = false;
        public bool MediaPlayerIsPlaying2 { get => _mediaPlayerIsPlaying2; set => _mediaPlayerIsPlaying2 = value; }
        private bool _mediaPlayerIsPlaying3 = false;
        public bool MediaPlayerIsPlaying3 { get => _mediaPlayerIsPlaying3; set => _mediaPlayerIsPlaying3 = value; }
        private bool _mediaPlayerIsPlaying4 = false;
        public bool MediaPlayerIsPlaying4 { get => _mediaPlayerIsPlaying4; set => _mediaPlayerIsPlaying4 = value; }
        public Button SkipPreviousbtn { get; set; }
        public Button SkipNextbtn { get; set; }
        public ToggleButton Playbtn { get; set; }
        public ToggleButton Pausebtn { get; set; }
        public ToggleButton PlayInvisible { get; set; }
        public ToggleButton PauseInvisible { get; set; }
        public ToggleButton Playbtn1 { get; set; }
        public ToggleButton Pausebtn1 { get; set; }
        public ToggleButton PlayInvisible1 { get; set; }
        public ToggleButton PauseInvisible1 { get; set; }
        public ToggleButton Playbtn2 { get; set; }
        public ToggleButton Pausebtn2 { get; set; }
        public ToggleButton Playbtn3 { get; set; }
        public ToggleButton Pausebtn3 { get; set; }
        public ToggleButton Playbtn4 { get; set; }
        public ToggleButton Pausebtn4 { get; set; }
        public Label InTime { get; set; }
        public Label TotalTime { get; set; }
        public Slider sliProgress { get; set; }
        public Grid MainViewProgram { get; set; }
        public Grid PlayerBar { get; set; }
        public Grid PlayerBarPlaylist { get; set; }
        public Grid PlayerBarArtist { get; set; }
        public Grid PlayerBarAlbum { get; set; }
        public Grid PlayerBarGenre { get; set; }

        public PlayListViewModel()
        {
            PlayInvisible1 = new ToggleButton();
            PauseInvisible1 = new ToggleButton();
            CurrentUser = new CurrentUserAccountModel();
            bool PlaylistFilter(object item)
            {
                if (String.IsNullOrEmpty(Search))
                    return true;
                else
                    return ((item as MusicMediaPlayer.Model.PlayList).PlayListName.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            Load = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                page = p as View.PlayList;
                LoadDanhSach(CurrentUser.Id);
            }
            );

            AddPL = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddPlayList wd = new AddPlayList();
                var data = wd.DataContext as AddPlayListViewModel;
                data.CurrentUser = CurrentUser;
                wd.ShowDialog();
                LoadDanhSach(CurrentUser.Id);
            }
            );

            Rename = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                RenamePlayList wd = new RenamePlayList();
                wd.ShowDialog();

                var RenameWD = wd.DataContext as RenamePlayListViewModel;

                if (RenameWD.IsLuu)
                {
                    var pl = p as MusicMediaPlayer.Model.PlayList;
                    pl.PlayListName = RenameWD.Title;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadDanhSach(CurrentUser.Id);
                    RenameWD.IsLuu = false;
                    wd.NamePL.Text = null;
                }
            }
            );

            Delete = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBoxResult dr = System.Windows.MessageBox.Show("Do you want to delete it?", "Delete!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dr == MessageBoxResult.Yes)
                {
                    var pl = p as MusicMediaPlayer.Model.PlayList;

                    var song_in_pl = pl.Song;

                    foreach (Song item in song_in_pl.ToList())
                    {
                        item.PlayList.Remove(pl);

                        pl.Song.Remove(item);
                    }

                    DataProvider.Ins.DB.PlayList.Remove(pl);
                    DataProvider.Ins.DB.SaveChanges();
                    LoadDanhSach(CurrentUser.Id);
                }

            }
           );


            TextChanged = new RelayCommand<System.Windows.Controls.TextBox>((p) => { return true; }, (p) =>
            {
                Search = p.Text;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(page.listview.ItemsSource);
                view.Filter = PlaylistFilter;
            }
            );

            MouseDoubleClick = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                PlayList_Inside wd = new PlayList_Inside();

                var trang = wd.DataContext as PlayList_InsideViewModel;

                trang.page_PlayList = page;
                trang.CurrentUser = CurrentUser;
                trang.SkipNextbtn = SkipNextbtn;
                trang.SkipPreviousbtn = SkipPreviousbtn;
                trang.Playbtn = Playbtn;
                trang.Pausebtn = Pausebtn;
                trang.PlayInvisible = PlayInvisible;
                trang.PauseInvisible = PauseInvisible;
                trang.Playbtn1 = Playbtn1;
                trang.Pausebtn1 = Pausebtn1;
                trang.Playbtn2 = Playbtn2;
                trang.Pausebtn2 = Pausebtn2;
                trang.Playbtn3 = Playbtn3;
                trang.Pausebtn3 = Pausebtn3;
                trang.Playbtn4 = Playbtn4;
                trang.Pausebtn4 = Pausebtn4;
                trang.InTime = InTime;
                trang.TotalTime = TotalTime;
                trang.sliProgress = sliProgress;
                trang.MainViewProgram = MainViewProgram;
                trang.PlayerBarArtist = PlayerBarArtist;
                trang.PlayerBar = PlayerBar;
                trang.PlayerBarAlbum = PlayerBarAlbum;
                trang.PlayerBarGenre = PlayerBarGenre;
                trang.PlayerBarPlaylist = PlayerBarPlaylist;
                trang.mediaPlayer = mediaPlayer;
                trang.mediaPlayer1 = mediaPlayer1;
                trang.mediaPlayer2 = mediaPlayer2;
                trang.mediaPlayer3 = mediaPlayer3;
                trang.mediaPlayer4 = mediaPlayer4;
                trang.MediaPlayerIsPlaying = MediaPlayerIsPlaying;
                trang.MediaPlayerIsPlaying1 = MediaPlayerIsPlaying1;
                trang.MediaPlayerIsPlaying2 = MediaPlayerIsPlaying2;
                trang.MediaPlayerIsPlaying3 = MediaPlayerIsPlaying3;
                trang.MediaPlayerIsPlaying4 = MediaPlayerIsPlaying4;
                PlayInvisible1 = wd.Play;
                PauseInvisible1 = wd.Pause;
                trang.PlayInvisible1 = PlayInvisible1;
                trang.PauseInvisible1 = PauseInvisible1;
                page.NavigationService.Navigate(wd);
            }
            );

            AZ = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(page.listview.ItemsSource);

                if (IsSort)
                {
                    view.SortDescriptions.Clear();
                }

                view.SortDescriptions.Add(new SortDescription("PlayListName", ListSortDirection.Ascending));
                IsSort = true;
            }
            );

            ZA = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(page.listview.ItemsSource);

                if (IsSort)
                {
                    view.SortDescriptions.Clear();
                }

                view.SortDescriptions.Add(new SortDescription("PlayListName", ListSortDirection.Descending));
                IsSort = true;
            }
            );


        }
        public void LoadDanhSach(int identity)
        {
            List = new ObservableCollection<MusicMediaPlayer.Model.PlayList>(DataProvider.Ins.DB.PlayList.Where(x => x.OwnerId == identity));
        }
    }
}