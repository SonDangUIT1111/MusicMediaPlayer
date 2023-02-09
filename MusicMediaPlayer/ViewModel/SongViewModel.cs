using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Controls.Primitives;

namespace MusicMediaPlayer.ViewModel
{

    public class SongViewModel : BaseViewModel
    {
        public MediaPlayer mediaPlayer = new MediaPlayer();
        public MediaPlayer mediaPlayer1 = new MediaPlayer();
        public MediaPlayer mediaPlayer2 = new MediaPlayer();
        public MediaPlayer mediaPlayer3 = new MediaPlayer();
        public MediaPlayer mediaPlayer4 = new MediaPlayer();
        private ObservableCollection<Song> _List;
        public ObservableCollection<Song> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<Song> _ListEdit;
        public ObservableCollection<Song> ListEdit { get => _ListEdit; set { _ListEdit = value; OnPropertyChanged(); } }
        private ObservableCollection<Song> _TopTrending;
        public ObservableCollection<Song> TopTrending { get => _TopTrending; set { _TopTrending = value; OnPropertyChanged(); } }
        public MySong MySongWindow { get; set; }
        public EditMySong EditSongWindow { get; set; }

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
        public CurrentUserAccountModel CurrentUser { get; set; }
        public DispatcherTimer SleepTimer { get; set; }
        private Song _SelectedItem;
        public Song SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    try
                    {
                        //sync parameter main window
                        MainViewProgram.Height = 650;
                        PlayerBar.Visibility = Visibility.Visible;
                        PlayerBarPlaylist.Visibility = Visibility.Hidden;
                        PlayerBarArtist.Visibility = Visibility.Hidden;
                        PlayerBarAlbum.Visibility = Visibility.Hidden;
                        PlayerBarGenre.Visibility = Visibility.Hidden;
                        SkipPreviousbtn.IsEnabled = true;
                        SkipNextbtn.IsEnabled = true;
                        if (mediaPlayer1 != null)
                        {
                            mediaPlayer1.Stop();
                        }
                        MediaPlayerIsPlaying1 = false;
                        if (mediaPlayer2 != null)
                        {
                            mediaPlayer2.Stop();
                        }
                        MediaPlayerIsPlaying2 = false;
                        if (mediaPlayer3 != null)
                        {
                            mediaPlayer3.Stop();
                        }
                        MediaPlayerIsPlaying3 = false;
                        if (mediaPlayer4 != null)
                        {
                            mediaPlayer4.Stop();
                        }
                        MediaPlayerIsPlaying4 = false;
                        //sync with playlist
                        Playbtn1.IsChecked = false;
                        Pausebtn1.IsChecked = true;
                        Playbtn1.Visibility = Visibility.Visible;
                        Pausebtn1.Visibility = Visibility.Hidden;
                        PlayInvisible1.IsChecked = false;
                        PauseInvisible1.IsChecked = true;
                        //sync with artist
                        Playbtn2.IsChecked = false;
                        Pausebtn2.IsChecked = true;
                        Playbtn2.Visibility = Visibility.Visible;
                        Pausebtn2.Visibility = Visibility.Hidden;
                        //sync with album
                        Playbtn3.IsChecked = false;
                        Pausebtn3.IsChecked = true;
                        Playbtn3.Visibility = Visibility.Visible;
                        Pausebtn3.Visibility = Visibility.Hidden;
                        //sync with genre
                        Playbtn4.IsChecked = false;
                        Pausebtn4.IsChecked = true;
                        Playbtn4.Visibility = Visibility.Visible;
                        Pausebtn4.Visibility = Visibility.Hidden;
                        //check file exists
                        var stringUri = SelectedItem.FilePath;
                        Uri uri = new Uri(stringUri);
                        SelectedItem.Times++;
                        DataProvider.Ins.DB.SaveChanges();
                        if (File.Exists(stringUri) == false)
                        {
                            mediaPlayer.Stop();
                            InTime.Content = "00:00";
                            sliProgress.Minimum = 0;
                            sliProgress.Maximum = 1;
                            sliProgress.Value = 0;
                            Playbtn.IsEnabled = false;
                            Playbtn.IsChecked = false;
                            Playbtn.Visibility = Visibility.Hidden;
                            Pausebtn.Visibility = Visibility.Visible;
                            Pausebtn.IsEnabled = false;
                            MySongWindow.Play.IsChecked = false;
                            MessageBoxFail ms = new MessageBoxFail();
                            ms.ShowDialog();
                            return;
                        }
                        else
                        {
                            //open some function when song is picked
                            sliProgress.IsEnabled = true;
                            Playbtn.IsEnabled = true;
                            Playbtn.IsChecked = true;
                            MySongWindow.Play.IsChecked = true;
                            Pausebtn.IsChecked = false;
                            MySongWindow.Pause.IsChecked = false;
                            if (CanChangeTOP_CD == true)
                            {
                                MySongWindow.TopTrendExpander.IsExpanded = false;
                                MySongWindow.CDCircle.IsExpanded = false;
                                MySongWindow.CDCircle.IsExpanded = true;
                            }
                            Pausebtn.IsEnabled = true;
                            mediaPlayer.Open(uri);
                            MediaPlayerIsPlaying = true;
                            Playbtn.Visibility = Visibility.Hidden;
                            Pausebtn.Visibility = Visibility.Visible;
                            mediaPlayer.Play();
                            DispatcherTimer timer = new DispatcherTimer();
                            timer.Interval = TimeSpan.FromSeconds(1);
                            timer.Tick += timer_Tick;
                            timer.Start();
                            void timer_Tick(object sender, EventArgs e)
                            {
                                if (mediaPlayer.Source != null)
                                {

                                    if (mediaPlayer.NaturalDuration.HasTimeSpan == true)
                                    {
                                        InTime.Content = String.Format("{0}", mediaPlayer.Position.ToString(@"mm\:ss"));
                                        TotalTime.Content = String.Format("{0}", mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                                        sliProgress.Minimum = 0;
                                        sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                                        sliProgress.Value = mediaPlayer.Position.TotalSeconds;
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBoxOK wd = new MessageBoxOK();
                        var data = wd.DataContext as MessageBoxOKViewModel;
                        data.Content = "File not found";
                        wd.ShowDialog();
                    }
                }
            }
        }
        private Song _SongChanging;
        public Song SongChanging { get { return _SongChanging; } set { _SongChanging = value; } }
        // menu
        private bool _IsPickRecent = false;
        public bool IsPickRecent { get => _IsPickRecent; set => _IsPickRecent = value; }
        private bool _IsPickMySong = true;
        public bool IsPickMySong { get => _IsPickMySong; set => _IsPickMySong = value; }
        private bool _IsPickFavourite;
        public bool IsPickFavourite { get => _IsPickFavourite; set => _IsPickFavourite = value;}
        private bool _CanChangeTOP_CD = true;
        public bool CanChangeTOP_CD { get => _CanChangeTOP_CD; set => _CanChangeTOP_CD = value; }
        //flag
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
        // add,delete
        private string _TitleToAdd;
        public string TitleToAdd { get { return _TitleToAdd; } set { _TitleToAdd = value; } }
        private string _ArtistToAdd;
        public string ArtistToAdd { get { return _ArtistToAdd; } set { _ArtistToAdd = value; } }
        private string _AlbumToAdd;
        public string AlbumToAdd { get { return _AlbumToAdd; } set { _AlbumToAdd = value; } }
        private string _GenreToAdd;
        public string GenreToAdd { get { return _GenreToAdd; } set { _GenreToAdd = value; } }
        private string _FilePathToAdd;
        public string FilePathToAdd { get { return _FilePathToAdd; } set { _FilePathToAdd = value; OnPropertyChanged(); } }
        private string _ImagePathToAdd;
        public string ImagePathToAdd { get { return _ImagePathToAdd; } set { _ImagePathToAdd = value; OnPropertyChanged(); } }

        private string _TitleToChange;
        public string TitleToChange { get { return _TitleToChange; } set { _TitleToChange = value; } }
        private string _ArtistToChange;
        public string ArtistToChange { get { return _ArtistToChange; } set { _ArtistToChange = value; } }
        private string _AlbumToChange;
        public string AlbumToChange { get { return _AlbumToChange; } set { _AlbumToChange = value; } }
        private string _GenreToChange;
        public string GenreToChange { get { return _GenreToChange; } set { _GenreToChange = value; } }
        private string _ImagePathToChange;
        public string ImagePathToChange { get { return _ImagePathToChange; } set { _ImagePathToChange = value; OnPropertyChanged(); } }
        private byte[] imageBinaryAdd;
        public byte[] ImageBinaryAdd { get { return imageBinaryAdd; } set { imageBinaryAdd = value; OnPropertyChanged(); } }
        // volumn
        private double _VolumePrevious;
        public double VolumePrevious { get => _VolumePrevious; set => _VolumePrevious = value; }
        //sleep timer
        private int _countTimer;
        public int CountTimer { get => _countTimer; set => _countTimer = value; }

        private bool _isPressed = false;
        public bool IsPressed { get => _isPressed; set => _isPressed = value; }
        private Canvas _templateCanvas = null;
        public Canvas TemplateCanvas { get => _templateCanvas; set => _templateCanvas = value; }
        //
        public ICommand AddFile { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand Complete { get; set; }
        public ICommand Changing { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand CancelChanging { get; set; }
        public ICommand ChangeImage { get; set; }
        public ICommand LoadData { get; set; }
        public ICommand Play { get; set; }
        public ICommand PlayShortcut { get; set; }
        public ICommand PauseShortcut { get; set; }
        public ICommand Pause { get; set; }
        public ICommand FilterChangeValue { get; set; }
        public ICommand EditFilterChangeValue { get; set; }
        public ICommand AddSong { get; set; }
        public ICommand EditSong { get; set; }
        public ICommand BackToMySong { get; set; }
        public ICommand LoadDataEditPage { get; set; }
        public ICommand ChangeInfoSong { get; set; }
        public ICommand DeleteSong { get; set; }
        public ICommand ChangeFavourite { get; set; }
        public ICommand ChangeTime { get; set; }
        public ICommand ChangeVolumn { get; set; }
        public ICommand SkipNext { get; set; }
        public ICommand SkipPrevious { get; set; }
        public ICommand ShuffleVariant { get; set; }
        public ICommand ShuffleDisabled { get; set; }
        public ICommand Loop { get; set; }
        public ICommand NonMute { get; set; }
        public ICommand Mute { get; set; }

        //sleep timer
        public ICommand Sleeper { get; set; }
        public ICommand CloseSleepTimer { get; set; }
        public ICommand OpenSleepTimer { get; set; }

        //menu song
        public ICommand RecentSong { get; set; }
        public ICommand AllSong { get; set; }
        public ICommand FavouriteSong { get; set; }

        //
        public ICommand ShowTopOrCD { get; set; }

        public SongViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id));
            TopTrending = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id).OrderByDescending(x => x.Times).ToList());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            Play = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Play();
                MediaPlayerIsPlaying = true;
                p.Play.IsChecked = true;
                p.Pause.IsChecked = false;
                MySongWindow.Play.IsChecked = true;
                MySongWindow.Pause.IsChecked = false;
                p.Play.Visibility = Visibility.Hidden;
                p.Pause.Visibility = Visibility.Visible;
            });
            Pause = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer?.Pause();
                MediaPlayerIsPlaying = false;
                p.Play.IsChecked = false;
                p.Pause.IsChecked = true;
                MySongWindow.Play.IsChecked = false;
                MySongWindow.Pause.IsChecked = true;
                p.Play.Visibility = Visibility.Visible;
                p.Pause.Visibility = Visibility.Hidden;
            });
            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                MySongWindow = p as MySong;
                LoadCommon();
                if (List.Count == 0)
                {
                    MySongWindow.IsThereSong.Visibility = Visibility.Visible;
                    MySongWindow.CDCircle.IsExpanded = false;
                    MySongWindow.TopTrendExpander.IsExpanded = true;
                    sliProgress.Value = 0;
                    sliProgress.IsEnabled = false;
                    InTime.Visibility = Visibility.Hidden;
                    TotalTime.Visibility = Visibility.Hidden;
                    Playbtn.Visibility = Visibility.Visible;
                    Playbtn.IsEnabled = false;
                    Pausebtn.Visibility = Visibility.Hidden;
                    mediaPlayer.Stop();
                }
                else
                {
                    MySongWindow.IsThereSong.Visibility = Visibility.Hidden;
                    sliProgress.IsEnabled = true;
                    InTime.Visibility = Visibility.Visible;
                    TotalTime.Visibility = Visibility.Visible;
                }
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MySongWindow.ListSong.ItemsSource);
                view.Filter = FiltersSong;
                bool FiltersSong(object item)
                {
                    if (String.IsNullOrEmpty(MySongWindow.SongFilter.Text))
                        return true;
                    else
                        return ((item as Song).SongTitle.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                        || (item as Song).Artist.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            });
            FilterChangeValue = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                MySongWindow = p as MySong;
                MySongWindow.CDCircle.IsExpanded = false;
                MySongWindow.TopTrendExpander.IsExpanded = true;
                CollectionViewSource.GetDefaultView(MySongWindow.ListSong.ItemsSource).Refresh();
            });
            EditFilterChangeValue = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                EditSongWindow = p as EditMySong;
                CollectionViewSource.GetDefaultView(EditSongWindow.ListSongEdit.ItemsSource).Refresh();
            });
            AddSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddSongToApp addsongview = new AddSongToApp();
                addsongview.ShowDialog();
            });
            EditSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditSongWindow = new EditMySong();
                MySongWindow.NavigationService.Navigate(EditSongWindow);
            });
            BackToMySong = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 EditSongWindow.NavigationService.Navigate(MySongWindow);
             });
            LoadDataEditPage = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListEdit = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id));
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(EditSongWindow.ListSongEdit.ItemsSource);
                view.Filter = FiltersSong;
                bool FiltersSong(object item)
                {
                    if (String.IsNullOrEmpty(EditSongWindow.SongFilter.Text))
                        return true;
                    else
                        return ((item as Song).SongTitle.IndexOf(EditSongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                        || (item as Song).Artist.IndexOf(EditSongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                if (ListEdit.Count == 0)
                {
                    EditSongWindow.IsThereSong.Visibility = Visibility.Visible;
                }
                else
                {
                    EditSongWindow.IsThereSong.Visibility = Visibility.Hidden;
                }
            });
            DeleteSong = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p)
                 =>
            {
                bool IsDeletePlaying = false;
                MessageBoxYesNo wd = new MessageBoxYesNo();
                var data = wd.DataContext as MessageBoxYesNoViewModel;
                data.Title = "Delete!";
                data.Question = "Do you want to delete it?";
                wd.ShowDialog();
                var result = wd.DataContext as MessageBoxYesNoViewModel;
                if (result.IsYes == true)
                {
                    var item = p as Song;
                    //check if delete the song is playing
                    if (item == MySongWindow.ListSong.SelectedItem)
                    {
                        IsDeletePlaying = true;
                    }
                    int artistid = (int)item.ArtistId;
                    int albumid = (int)item.AlbumId;
                    int genreid = (int)item.GenreId;
                    if (item != null)
                    {
                        //Sync with playlist
                        var allplaylist = DataProvider.Ins.DB.PlayLists.Where(x=>x.OwnerId == CurrentUser.Id).ToList();
                        foreach (Model.PlayList playlist in allplaylist)
                        {
                            var list = playlist.Songs.ToList();
                            foreach (Song song in list)
                            {
                                if (song == item)
                                {
                                    playlist.Songs.Remove(item);
                                    playlist.SongCount--;
                                }
                            }
                        }
                        DataProvider.Ins.DB.Songs.Remove(item);
                        DataProvider.Ins.DB.SaveChanges();
                        //Sync with artist
                        if (DataProvider.Ins.DB.Songs.Where(x=>x.UserId == CurrentUser.Id && x.ArtistId == artistid).Count() == 0)
                        {
                            ObservableCollection<Artist> artistDelete = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x=>x.UserId == CurrentUser.Id && x.ArtistId == artistid));
                            DataProvider.Ins.DB.Artists.Remove(artistDelete[0]);
                        }
                        //Sync with album
                        if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == albumid).Count() == 0)
                        {
                            ObservableCollection<Album> albumDelete = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == albumid ));
                            DataProvider.Ins.DB.Albums.Remove(albumDelete[0]);
                        }
                        //Sync with genre
                        if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.GenreId == genreid).Count() == 0)
                        {
                            ObservableCollection<Genre> genreDelete = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.UserId == CurrentUser.Id && x.GenreId == genreid));
                            DataProvider.Ins.DB.Genres.Remove(genreDelete[0]);
                        }
                        DataProvider.Ins.DB.SaveChanges();
                        //Complete delete
                        MySongWindow.ListSong.Items.Refresh();
                        LoadCommon();
                        LoadEditPage();
                        if (List.Count != 0 && MediaPlayerIsPlaying == true && IsDeletePlaying == true)
                        {
                            MySongWindow.ListSong.SelectedIndex = -1;
                            MySongWindow.ListSong.SelectedIndex = 0;
                        }
                    }
                }
                else return;

            });

            Changing = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditSongInApp wd = p as EditSongInApp;
                TitleToChange = wd.ChangeTitleSong.Text;
                ArtistToChange = wd.ChangeArtistSong.Text;
                GenreToChange = wd.ChangeGenreSong.Text;
                AlbumToChange = wd.ChangeAlbumtSong.Text;
                int artistid = (int)SongChanging.ArtistId;
                int albumid = (int)SongChanging.AlbumId;
                int genreid = (int)SongChanging.GenreId;
                byte[] imagechanging = SongChanging.ImageSongBinary;
                //Case rename both artist and album
                //case change to artist exists amd album not exitst
                if (DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange).Count() > 0 &&
                    DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange).Count() == 0)
                {
                    Album newAlbum = new Album();
                    newAlbum.AlbumName = AlbumToChange;
                    newAlbum.UserId = CurrentUser.Id;
                    newAlbum.Composer = ArtistToChange;
                    newAlbum.ImageAlbumBinary = imagechanging;
                    DataProvider.Ins.DB.Albums.Add(newAlbum);
                    DataProvider.Ins.DB.SaveChanges();

                    ObservableCollection<Artist> artistlist = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange));
                    ObservableCollection<Album> albumlist = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange && x.Composer == ArtistToChange));
                    SongChanging.ArtistId = artistlist[0].ArtistId;
                    SongChanging.AlbumId = albumlist[0].AlbumId;
                    DataProvider.Ins.DB.SaveChanges();
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.ArtistId == artistid).Count() == 0)
                    {
                        ObservableCollection<Artist> artistDelete = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.ArtistId == artistid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Artists.Remove(artistDelete[0]);
                    }
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == albumid ).Count() == 0)
                    {
                        ObservableCollection<Album> albumDelete = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.AlbumId == albumid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Albums.Remove(albumDelete[0]);
                    }
                }
                //case change to album exists and artist not exists
                if (DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange).Count() == 0 &&
                    DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange).Count() > 0)
                {
                    Artist newArtist = new Artist();
                    newArtist.ArtistName = ArtistToChange;
                    newArtist.UserId = CurrentUser.Id;
                    newArtist.Streams = 0;
                    newArtist.ImageArtistBinary = imagechanging;
                    DataProvider.Ins.DB.Artists.Add(newArtist);

                    Album newAlbum = new Album();
                    newAlbum.AlbumName = AlbumToChange;
                    newAlbum.UserId = CurrentUser.Id;
                    newAlbum.Composer = ArtistToChange;
                    newAlbum.ImageAlbumBinary = imagechanging;
                    DataProvider.Ins.DB.Albums.Add(newAlbum);
                    DataProvider.Ins.DB.SaveChanges();

                    ObservableCollection<Artist> artistlist = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange));
                    ObservableCollection<Album> albumlist = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange && x.Composer == ArtistToChange));
                    SongChanging.ArtistId = artistlist[0].ArtistId;
                    SongChanging.AlbumId = albumlist[0].AlbumId;
                    DataProvider.Ins.DB.SaveChanges();
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.ArtistId == artistid).Count() == 0)
                    {
                        ObservableCollection<Artist> artistDelete = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.ArtistId == artistid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Artists.Remove(artistDelete[0]);
                    }
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == albumid).Count() == 0)
                    {
                        ObservableCollection<Album> albumDelete = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.AlbumId == albumid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Albums.Remove(albumDelete[0]);
                    }
                }
                //Case change to artist exists and album exists is of artist
                if (DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange).Count() > 0 &&
                    DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange && x.Composer ==ArtistToChange).Count() > 0)
                {
                    ObservableCollection<Artist> artistlist = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange));
                    ObservableCollection<Album> albumlist = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange && x.Composer == ArtistToChange));
                    SongChanging.ArtistId = artistlist[0].ArtistId;
                    SongChanging.AlbumId = albumlist[0].AlbumId;
                    DataProvider.Ins.DB.SaveChanges();
                    try
                    {
                        if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.ArtistId == artistid).Count() == 0)
                        {
                            ObservableCollection<Artist> artistDelete = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.ArtistId == artistid && x.UserId == CurrentUser.Id));
                            DataProvider.Ins.DB.Artists.Remove(artistDelete[0]);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == albumid).Count() == 0)
                        {
                            ObservableCollection<Album> albumDelete = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.AlbumId == albumid && x.UserId == CurrentUser.Id));
                            DataProvider.Ins.DB.Albums.Remove(albumDelete[0]);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                //case change to artist exists and album exists is not of artist
                if (DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange).Count() > 0 &&
                    DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange && x.Composer == ArtistToChange).Count() == 0)
                {
                    ObservableCollection<Artist> artistlist = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange));
                    Album newAlbum = new Album();
                    newAlbum.AlbumName = AlbumToChange;
                    newAlbum.UserId = CurrentUser.Id;
                    newAlbum.Composer = ArtistToChange;
                    newAlbum.ImageAlbumBinary = imagechanging;
                    DataProvider.Ins.DB.Albums.Add(newAlbum);
                    DataProvider.Ins.DB.SaveChanges();
                    ObservableCollection<Album> albumlist = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange && x.Composer == ArtistToChange));
                    SongChanging.ArtistId = artistlist[0].ArtistId;
                    SongChanging.AlbumId = albumlist[0].AlbumId;
                    DataProvider.Ins.DB.SaveChanges();
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.ArtistId == artistid).Count() == 0)
                    {
                        ObservableCollection<Artist> artistDelete = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.ArtistId == artistid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Artists.Remove(artistDelete[0]);
                    }
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == albumid ).Count() == 0)
                    {
                        ObservableCollection<Album> albumDelete = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.AlbumId == albumid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Albums.Remove(albumDelete[0]);
                    }
                }
                //case both is not exists
                if (DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange).Count() == 0 &&
                    DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange).Count() == 0)
                {
                    Artist newArtist = new Artist();
                    newArtist.ArtistName = ArtistToChange;
                    newArtist.UserId = CurrentUser.Id;
                    newArtist.Streams = 0;
                    newArtist.ImageArtistBinary = imagechanging;
                    DataProvider.Ins.DB.Artists.Add(newArtist);

                    Album newAlbum = new Album();
                    newAlbum.AlbumName = AlbumToChange;
                    newAlbum.UserId = CurrentUser.Id;
                    newAlbum.Composer = ArtistToChange;
                    newAlbum.ImageAlbumBinary = imagechanging;
                    DataProvider.Ins.DB.Albums.Add(newAlbum);
                    DataProvider.Ins.DB.SaveChanges();

                    ObservableCollection<Artist> artistlist = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == CurrentUser.Id && x.ArtistName == ArtistToChange));
                    ObservableCollection<Album> albumlist = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id && x.AlbumName == AlbumToChange && x.Composer == ArtistToChange));
                    SongChanging.ArtistId = artistlist[0].ArtistId;
                    SongChanging.AlbumId = albumlist[0].AlbumId;
                    DataProvider.Ins.DB.SaveChanges();
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.ArtistId == artistid).Count() == 0)
                    {
                        ObservableCollection<Artist> artistDelete = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.ArtistId == artistid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Artists.Remove(artistDelete[0]);
                    }
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == albumid).Count() == 0)
                    {
                        ObservableCollection<Album> albumDelete = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.AlbumId == albumid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Albums.Remove(albumDelete[0]);
                    }
                }
                if (GenreToChange != SongChanging.Genre)
                {
                    ObservableCollection<Genre> genrelist = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.UserId == CurrentUser.Id && x.GenreName == GenreToChange));
                    //case rename to the name that exists
                    if (genrelist.Count > 0)
                    {
                        SongChanging.GenreId = genrelist[0].GenreId;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    //case rename to the name that does not exist
                    else
                    {
                        Genre newGenre = new Genre();
                        newGenre.GenreName = GenreToChange;
                        newGenre.UserId = CurrentUser.Id;
                        newGenre.ImageGenreBinary = imagechanging;
                        DataProvider.Ins.DB.Genres.Add(newGenre);
                        DataProvider.Ins.DB.SaveChanges();
                        genrelist = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.GenreName == GenreToChange && x.UserId == CurrentUser.Id));
                        SongChanging.GenreId = genrelist[0].GenreId;
                    }
                    if (DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.GenreId == genreid).Count() == 0)
                    {
                        ObservableCollection<Genre> genreDelete = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.GenreId == genreid && x.UserId == CurrentUser.Id));
                        DataProvider.Ins.DB.Genres.Remove(genreDelete[0]);
                    }
                }
                //change
                SongChanging.SongTitle = TitleToChange;
                SongChanging.Artist = ArtistToChange;
                SongChanging.Album = AlbumToChange;
                SongChanging.Genre = GenreToChange;
                if (ImagePathToChange != null)
                {
                    Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                    byte[] BinaryImage = converter.ImageToBinary(ImagePathToChange);
                    SongChanging.ImageSongBinary = BinaryImage;
                }
                DataProvider.Ins.DB.SaveChanges();
                LoadCommon();
                LoadEditPage();
                TitleToChange = null;
                ArtistToChange = null;
                AlbumToChange = null;
                GenreToChange = null;
                ImagePathToChange = null;
                MySongWindow.CDCircle.IsExpanded = false;
                MySongWindow.CDCircle.IsExpanded = true;
                MessageBoxSuccessful ms = new MessageBoxSuccessful();
                ms.Show();
                wd.Close();
                
            });
            ChangeInfoSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SongChanging = p as Song;
                EditSongInApp editWD = new EditSongInApp();
                editWD.ChangeTitleSong.Text = SongChanging.SongTitle;
                editWD.ChangeArtistSong.Text = SongChanging.Artist;
                editWD.ChangeAlbumtSong.Text = SongChanging.Album;
                editWD.ChangeGenreSong.Text = SongChanging.Genre;
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                var imagestream = new MemoryStream(SongChanging.ImageSongBinary);
                bitmap.StreamSource = imagestream;
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                editWD.ChangegrdSelectImg.Background = imageBrush;
                editWD.ShowDialog();
            });

            ChangeFavourite = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Song item = p as Song;
                item.IsFavourite = !item.IsFavourite;
                DataProvider.Ins.DB.SaveChanges();
            });
            AddFile = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePathToAdd = openFileDialog.FileName;
                }
            });
            AddImage = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Insert Image";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    ImagePathToAdd = op.FileName;
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(ImagePathToAdd);
                    bitmap.EndInit();
                    imageBrush.ImageSource = bitmap;
                    p.Background = imageBrush;
                    if (p.Children.Count > 1)
                    {
                        p.Children.Remove(p.Children[0]);
                        p.Children.Remove(p.Children[1]);
                    }
                }
            });
            ChangeImage = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Insert Image";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    ImagePathToChange = op.FileName;
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(ImagePathToChange);
                    bitmap.EndInit();
                    imageBrush.ImageSource = bitmap;
                    p.Background = imageBrush;
                    if (p.Children.Count > 1)
                    {
                        p.Children.Remove(p.Children[0]);
                        p.Children.Remove(p.Children[1]);
                    }
                }
            });

            Complete = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                var filePath = Path.Combine(projectPath, "Image", "music_note.jpg");
                string titleNewSong = "Unknown";
                string artistNewSong = "Unknown";
                string albumNewSong = "Unknown";
                string genreNewSong = "Unknown";
                string uriImage = filePath;
                var MySongWindow = p as AddSongToApp;
                if (String.IsNullOrEmpty(FilePathToAdd))
                {
                    MessageBoxOK ms = new MessageBoxOK();
                    var dt = ms.DataContext as MessageBoxOKViewModel;
                    dt.Content = "Please add song file";
                    ms.ShowDialog();
                    return;
                }
                if (!String.IsNullOrEmpty(MySongWindow.TitleSong.Text))
                {
                    titleNewSong = MySongWindow.TitleSong.Text;
                }
                if (!String.IsNullOrEmpty(MySongWindow.ArtistSong.Text))
                {
                    artistNewSong = MySongWindow.ArtistSong.Text;
                }
                if (!String.IsNullOrEmpty(MySongWindow.GenreSong.Text))
                {
                    genreNewSong = MySongWindow.GenreSong.Text;
                }
                if (!String.IsNullOrEmpty(MySongWindow.AlbumSong.Text))
                {
                    albumNewSong = MySongWindow.AlbumSong.Text;
                }
                if (ImagePathToAdd != null)
                {
                    uriImage = ImagePathToAdd;
                }
                if (DataProvider.Ins.DB.Songs.Where(o => o.SongTitle == TitleToAdd && o.UserId == CurrentUser.Id).Count() > 0)
                {
                    MessageBoxOK mb = new MessageBoxOK();
                    var data = mb.DataContext as MessageBoxOKViewModel;
                    data.Content = "This title already exists, please try another title";
                    mb.ShowDialog();
                    return;
                }
                try
                {
                    //
                    string stradd = FilePathToAdd;
                    Uri uriadd = new Uri(stradd);
                    MediaPlayer med = new MediaPlayer();
                    med.Open(uriadd);
                    string timetoadd = "";
                    // this is a trick to waiting hastimespan change to true
                    MessageBoxLoading MBL = new MessageBoxLoading();
                    MBL.ShowDialog();
                    //
                    if (med.HasAudio == false)
                    {
                        MessageBoxFail ms = new MessageBoxFail();
                        ms.ShowDialog();
                        return;
                    }
                    if (med.NaturalDuration.HasTimeSpan)
                    {
                        timetoadd = String.Format("{0}", med.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                    }
                    Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                    ImageBinaryAdd = converter.ImageToBinary(uriImage);

                    //add song into database
                    Song newSongItem = new Song();
                    newSongItem.Artist = artistNewSong;
                    newSongItem.SongTitle = titleNewSong;
                    newSongItem.Genre = genreNewSong;
                    newSongItem.Album = albumNewSong;
                    newSongItem.FilePath = FilePathToAdd;
                    newSongItem.ImageSongBinary = ImageBinaryAdd;
                    newSongItem.Times = 0;
                    newSongItem.TimeAdd = DateTime.Now;
                    newSongItem.HowLong = timetoadd;
                    newSongItem.IsFavourite = false;
                    newSongItem.UserId = CurrentUser.Id;

                    //
                    //filter song to artist filter
                    ObservableCollection<Artist> artistlist = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.ArtistName == artistNewSong && x.UserId == CurrentUser.Id));
                    if (artistlist.Count == 0)
                    {
                        Artist newArtist = new Artist();
                        newArtist.ArtistName = artistNewSong;
                        newArtist.UserId = CurrentUser.Id;
                        newArtist.Streams = 0;
                        newArtist.ImageArtistBinary = ImageBinaryAdd;
                        DataProvider.Ins.DB.Artists.Add(newArtist);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    artistlist = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.ArtistName == artistNewSong && x.UserId == CurrentUser.Id));
                    newSongItem.ArtistId = artistlist[0].ArtistId;
                    //
                    //filter song to album filter
                    ObservableCollection<Album> albumlist = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.AlbumName == albumNewSong && x.Composer == artistNewSong && x.UserId == CurrentUser.Id));
                    if (albumlist.Count == 0)
                    {
                        Album newAlbum = new Album();
                        newAlbum.AlbumName = albumNewSong;
                        newAlbum.Composer = artistNewSong;
                        newAlbum.UserId = CurrentUser.Id;
                        newAlbum.ImageAlbumBinary = ImageBinaryAdd;
                        DataProvider.Ins.DB.Albums.Add(newAlbum);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    albumlist = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.AlbumName == albumNewSong && x.Composer == artistNewSong && x.UserId == CurrentUser.Id));
                    newSongItem.AlbumId = albumlist[0].AlbumId;
                    //filter song to genre filter
                    ObservableCollection<Genre> genrelist = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.GenreName == genreNewSong && x.UserId == CurrentUser.Id));
                    if (genrelist.Count == 0)
                    {
                        Genre newGenre = new Genre();
                        newGenre.GenreName = genreNewSong;
                        newGenre.UserId = CurrentUser.Id;
                        newGenre.ImageGenreBinary = ImageBinaryAdd;
                        DataProvider.Ins.DB.Genres.Add(newGenre);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    genrelist = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.GenreName == genreNewSong && x.UserId == CurrentUser.Id));
                    newSongItem.GenreId = genrelist[0].GenreId;
                    //
                    DataProvider.Ins.DB.Songs.Add(newSongItem);
                    DataProvider.Ins.DB.SaveChanges();
                    LoadCommon();

                    MySongWindow.Close();
                    MessageBoxSuccessful MB = new MessageBoxSuccessful();
                    MB.ShowDialog();

                    TitleToAdd = null;
                    ArtistToAdd = null;
                    AlbumToAdd = null;
                    GenreToAdd = null;
                    FilePathToAdd = null;
                    ImagePathToAdd = null;
                    ImageBinaryAdd = null;
                }
                catch (Exception)
                {
                    MessageBoxFail wd = new MessageBoxFail();
                    wd.ShowDialog();
                }

            });
            Cancel = new RelayCommand<Window>((p) => { return true; }, (p) => {
                var MySongWindow = p as AddSongToApp;
                MySongWindow.Close();
                TitleToAdd = null;
                ArtistToAdd = null;
                AlbumToAdd = null;
                GenreToAdd = null;
                FilePathToAdd = null;
                ImagePathToAdd = null;
                ImageBinaryAdd = null;
            });
            CancelChanging = new RelayCommand<Window>((p) => { return true; }, (p) => {
                var wd = p as EditSongInApp;
                wd.Close();
                TitleToChange = null;
                ArtistToChange = null;
                ImagePathToChange = null;
            });

            ChangeTime = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MySongWindow.ListSong.Items.Count == 0)
                {
                    return;
                }
                if (sliProgress.IsFocused == true)
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
                    mediaPlayer.Play();
                    MySongWindow.Focus();
                }
                if (sliProgress.Value == sliProgress.Maximum)

                {
                    sliProgress.Value = 0;
                    if (MySongWindow.RandomLoop.IsChecked == true)
                    {
                        Random random = new Random();
                        int nextIndex = -1;
                        do
                        {
                            nextIndex = random.Next(0, MySongWindow.ListSong.Items.Count);
                        } while (nextIndex < 0 || nextIndex == MySongWindow.ListSong.SelectedIndex);
                        MySongWindow.ListSong.SelectedIndex = -1;
                        MySongWindow.ListSong.SelectedIndex = nextIndex;
                    }
                    else if (MySongWindow.OneLoop.IsChecked == true)
                    {
                        int nextIndex = MySongWindow.ListSong.SelectedIndex;
                        MySongWindow.ListSong.SelectedIndex = -1;
                        MySongWindow.ListSong.SelectedIndex = nextIndex;
                    }
                    else if (MySongWindow.SequencecyLoop.IsChecked == true)
                    {
                        int nextIndex = (MySongWindow.ListSong.SelectedIndex + 1) % (MySongWindow.ListSong.Items.Count);
                        MySongWindow.ListSong.SelectedIndex = -1;
                        MySongWindow.ListSong.SelectedIndex = nextIndex;
                    }
                    else
                    {
                        int nextIndex = (MySongWindow.ListSong.SelectedIndex + 1) % (MySongWindow.ListSong.Items.Count);
                        MySongWindow.ListSong.SelectedIndex = -1;
                        MySongWindow.ListSong.SelectedIndex = nextIndex;
                    }

                }
            });
            ChangeVolumn = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                if (p.Volume.IsFocused == true)
                {
                    mediaPlayer.Volume = p.Volume.Value;
                }
                if (p.Volume.Value >= 0.8)
                {
                    p.SpeakerHigh.Visibility = Visibility.Visible;
                    p.SpeakerLow.Visibility = Visibility.Hidden;
                    p.SpeakerMedium.Visibility = Visibility.Hidden;
                    p.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else if (p.Volume.Value >= 0.4)
                {
                    p.SpeakerHigh.Visibility = Visibility.Hidden;
                    p.SpeakerLow.Visibility = Visibility.Hidden;
                    p.SpeakerMedium.Visibility = Visibility.Visible;
                    p.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else if (p.Volume.Value > 0)
                {
                    p.SpeakerHigh.Visibility = Visibility.Hidden;
                    p.SpeakerLow.Visibility = Visibility.Visible;
                    p.SpeakerMedium.Visibility = Visibility.Hidden;
                    p.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else
                {
                    p.SpeakerHigh.Visibility = Visibility.Hidden;
                    p.SpeakerLow.Visibility = Visibility.Hidden;
                    p.SpeakerMedium.Visibility = Visibility.Hidden;
                    p.SpeakerOff.Visibility = Visibility.Visible;
                }
            });
            SkipNext = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MySongWindow.ListSong.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (MySongWindow.ListSong.SelectedIndex + 1) % (MySongWindow.ListSong.Items.Count);
                MySongWindow.ListSong.SelectedIndex = nextIndex;
            });
            SkipPrevious = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MySongWindow.ListSong.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (MySongWindow.ListSong.SelectedIndex - 1) % (MySongWindow.ListSong.Items.Count);
                if (nextIndex < 0) nextIndex = MySongWindow.ListSong.Items.Count - 1;
                MySongWindow.ListSong.SelectedIndex = nextIndex;
            });
            ShuffleVariant = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop.IsChecked = false;
                p.OneLoop.IsChecked = false;
                MySongWindow.SequencecyLoop.IsChecked = false;
                MySongWindow.OneLoop.IsChecked = false;
                MySongWindow.RandomLoop.IsChecked = true;
            });
            ShuffleDisabled = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.RandomLoop.IsChecked = false;
                p.OneLoop.IsChecked = false;
                MySongWindow.RandomLoop.IsChecked = false;
                MySongWindow.OneLoop.IsChecked = false;
                MySongWindow.SequencecyLoop.IsChecked = true;
            });
            Loop = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop.IsChecked = false;
                p.RandomLoop.IsChecked = false;
                MySongWindow.SequencecyLoop.IsChecked = false;
                MySongWindow.RandomLoop.IsChecked = false;
                MySongWindow.OneLoop.IsChecked = true;
            });
            NonMute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.Volume.Value = VolumePrevious;
                mediaPlayer.Volume = p.Volume.Value;
            });
            Mute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                VolumePrevious = p.Volume.Value;
                p.Volume.Value = 0;
                mediaPlayer.Volume = 0;
            });

            // sleep timer
            Sleeper = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window sleepwd = p as Window;
                CountTimer = 0;
                SleepTimerView wd = p as SleepTimerView;
                Slider slider = wd.knob as Slider;
                double convertsleep = slider.Value * 60;
                int sleepsecond = (int)convertsleep;
                DispatcherTimer countdownoff = new DispatcherTimer();
                countdownoff.Interval = TimeSpan.FromSeconds(1);
                countdownoff.Tick += countdown_Tick;
                countdownoff.Start();
                SleepTimer = new DispatcherTimer();
                SleepTimer.Interval = TimeSpan.FromSeconds(1);
                SleepTimer.Tick += timer_Tick;
                SleepTimer.Start();
                void countdown_Tick(object sender,EventArgs e)
                {
                    countdownoff.Stop();
                    sleepwd.Close();
                }
                void timer_Tick(object sender, EventArgs e)
                {
                    CountTimer++;
                    if (CountTimer == sleepsecond)
                    {
                        mediaPlayer.Stop();
                        SleepTimer.Stop();
                        Playbtn.IsChecked = false;
                        MySongWindow.Play.IsChecked = false;
                        Pausebtn.IsChecked = true;
                        MySongWindow.Pause.IsChecked = true;
                        Playbtn.Visibility = Visibility.Visible;
                        Pausebtn.Visibility = Visibility.Hidden;
                    }
                }
            });
            CloseSleepTimer = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Window sleepwd = p as Window;
                sleepwd.Close();
            });
            OpenSleepTimer = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SleepTimerView sleeptimerView = new SleepTimerView();
                sleeptimerView.ShowDialog();
            });

            // recent song
            RecentSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickMySong = false;
                IsPickFavourite = false;
                IsPickRecent = true;
                LoadRecent();
            });
            AllSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickMySong = true;
                IsPickRecent = false;
                IsPickFavourite = false;
                Load();
            });
            FavouriteSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickMySong = false;
                IsPickRecent = false;
                IsPickFavourite = true;
                LoadFavourite();
            });
            //
            ShowTopOrCD = new RelayCommand<MainWindow>((p) =>
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                CanChangeTOP_CD = !CanChangeTOP_CD;
                LoadCommon();
                if (MySongWindow.TopTrendExpander.IsExpanded == true)
                {
                    MySongWindow.TopTrendExpander.IsExpanded = false;
                    MySongWindow.CDCircle.IsExpanded = true;
                }
                else
                {
                    MySongWindow.TopTrendExpander.IsExpanded = true;
                    MySongWindow.CDCircle.IsExpanded = false;
                }
            });
           

        }
        public void Load()
        {
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id));
            if (List.Count == 0)
            {
                MySongWindow.IsThereSong.Visibility = Visibility.Visible;
                MySongWindow.CDCircle.IsExpanded = false;
                MySongWindow.TopTrendExpander.IsExpanded = true;
                sliProgress.Value = 0;
                sliProgress.IsEnabled = false;
                InTime.Visibility = Visibility.Hidden;
                TotalTime.Visibility = Visibility.Hidden;
                Playbtn.Visibility = Visibility.Visible;
                Playbtn.IsEnabled = false;
                Pausebtn.Visibility = Visibility.Hidden;
                mediaPlayer.Stop();
            }
            else
            {
                MySongWindow.IsThereSong.Visibility = Visibility.Hidden;
                sliProgress.IsEnabled = true;
                InTime.Visibility = Visibility.Visible;
                TotalTime.Visibility = Visibility.Visible;
            }
            TopTrending = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id).OrderByDescending(x => x.Times).ToList());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MySongWindow.ListSong.ItemsSource);
            view.Filter = FiltersSong;

            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(MySongWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Song).SongTitle.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                    || (item as Song).Artist.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        public void LoadRecent()
        {
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id).OrderByDescending(x => x.TimeAdd).ToList());
            if (List.Count == 0)
            {
                MySongWindow.IsThereSong.Visibility = Visibility.Visible;
                MySongWindow.CDCircle.IsExpanded = false;
                MySongWindow.TopTrendExpander.IsExpanded = true;
                sliProgress.Value = 0;
                sliProgress.IsEnabled = false;
                InTime.Visibility = Visibility.Hidden;
                TotalTime.Visibility = Visibility.Hidden;
                Playbtn.Visibility = Visibility.Visible;
                Playbtn.IsEnabled = false;
                Pausebtn.Visibility = Visibility.Hidden;
                mediaPlayer.Stop();
            }
            else
            {
                MySongWindow.IsThereSong.Visibility = Visibility.Hidden;
                sliProgress.IsEnabled = true;
                InTime.Visibility = Visibility.Visible;
                TotalTime.Visibility = Visibility.Visible;
            }
            TopTrending = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id).OrderByDescending(x => x.Times).ToList());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MySongWindow.ListSong.ItemsSource);
            view.Filter = FiltersSong;

            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(MySongWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Song).SongTitle.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                    || (item as Song).Artist.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        void LoadFavourite()
        {
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.IsFavourite == true).ToList());
            if (List.Count == 0)
            {
                MySongWindow.IsThereSong.Visibility = Visibility.Visible;
                MySongWindow.CDCircle.IsExpanded = false;
                MySongWindow.TopTrendExpander.IsExpanded = true;
                sliProgress.Value = 0;
                sliProgress.IsEnabled = false;
                InTime.Visibility = Visibility.Hidden;
                TotalTime.Visibility = Visibility.Hidden;
                Playbtn.Visibility = Visibility.Visible;
                Playbtn.IsEnabled = false;
                Pausebtn.Visibility = Visibility.Hidden;
                mediaPlayer.Stop();
            }
            else
            {
                MySongWindow.IsThereSong.Visibility = Visibility.Hidden;
                sliProgress.IsEnabled = true;
                InTime.Visibility = Visibility.Visible;
                TotalTime.Visibility = Visibility.Visible;
            }
            TopTrending = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id).OrderByDescending(x => x.Times).ToList());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            TopTrending.Add(new Song());
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MySongWindow.ListSong.ItemsSource);
            view.Filter = FiltersSong;

            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(MySongWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Song).SongTitle.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                    || (item as Song).Artist.IndexOf(MySongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        public void LoadCommon()
        {
            if (IsPickMySong)
            {
                Load();
            }
            else if (IsPickRecent)
            {
                LoadRecent();
            }
            else if (IsPickFavourite)
            {
                LoadFavourite();
            }
        }
        public void LoadEditPage()
        {
            ListEdit = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id));
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(EditSongWindow.ListSongEdit.ItemsSource);
            view.Filter = FiltersSong;

            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(EditSongWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Song).SongTitle.IndexOf(EditSongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                    || (item as Song).Artist.IndexOf(EditSongWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (ListEdit.Count == 0)
            {
                EditSongWindow.IsThereSong.Visibility = Visibility.Visible;
            }
            else
            {
                EditSongWindow.IsThereSong.Visibility = Visibility.Hidden;
            }
        }


    }
}