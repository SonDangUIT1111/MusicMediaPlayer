using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicMediaPlayer.ViewModel
{
    public class PlayList_InsideViewModel : BaseViewModel
    {
        #region commands
        public ICommand LoadData { get; set; }

        public ICommand Rename { get; set; }

        public ICommand DeletePlayList { get; set; }

        public ICommand Delete_One_Song { get; set; }

        public ICommand AddSong { get; set; }
        public ICommand RemoveSong { get; set; }
        public ICommand LoadDataEditPage { get; set; }
        public ICommand EditFilterChangeValue { get; set; }
        public ICommand BackToMyPlaylist { get; set; }
        public ICommand DeleteSong { get; set; }

        #endregion
        public MediaPlayer mediaPlayer { get; set; }
        public MediaPlayer mediaPlayer1 { get; set; }
        public MediaPlayer mediaPlayer2 { get; set; }
        public MediaPlayer mediaPlayer3 { get; set; }
        public MediaPlayer mediaPlayer4 { get; set; }
        public CurrentUserAccountModel CurrentUser { get; set; }
        private string _PLName;
        public string PLName { get => _PLName; set { _PLName = value; OnPropertyChanged(); } }

        private string _SongCount;
        public string SongCount { get => _SongCount; set { _SongCount = value; OnPropertyChanged(); } }

        public View.PlayList page_PlayList;

        MusicMediaPlayer.Model.PlayList pl;

        public PlayList_Inside thispage { get; set; }
        public EditSongInPlayList EditSongPlaylistWindow { get; set; }

        public bool IsGoBack = false;

        private ObservableCollection<MusicMediaPlayer.Model.Song> _List;
        public ObservableCollection<MusicMediaPlayer.Model.Song> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<MusicMediaPlayer.Model.Song> _ListEdit;
        public ObservableCollection<MusicMediaPlayer.Model.Song> ListEdit { get => _ListEdit; set { _ListEdit = value; OnPropertyChanged(); } }

        //player bar
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
        private double _VolumePrevious;
        public double VolumePrevious { get => _VolumePrevious; set => _VolumePrevious = value; }
        private int _countTimer;
        public int CountTimer { get => _countTimer; set => _countTimer = value; }
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
                        PlayerBar.Visibility = Visibility.Hidden;
                        PlayerBarPlaylist.Visibility = Visibility.Visible;
                        PlayerBarArtist.Visibility = Visibility.Hidden;
                        PlayerBarAlbum.Visibility = Visibility.Hidden;
                        PlayerBarGenre.Visibility = Visibility.Hidden;
                        SkipPreviousbtn.IsEnabled = true;
                        SkipNextbtn.IsEnabled = true;
                        if (mediaPlayer != null)
                        {
                            mediaPlayer.Stop();
                        }
                        MediaPlayerIsPlaying = false;
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
                        Playbtn.IsChecked = false;
                        Pausebtn.IsChecked = true;
                        Playbtn.Visibility = Visibility.Visible;
                        Pausebtn.Visibility = Visibility.Hidden;
                        PlayInvisible.IsChecked = false;
                        PauseInvisible.IsChecked = true;

                        Playbtn2.IsChecked = false;
                        Pausebtn2.IsChecked = true;
                        Playbtn2.Visibility = Visibility.Visible;
                        Pausebtn2.Visibility = Visibility.Hidden;

                        Playbtn3.IsChecked = false;
                        Pausebtn3.IsChecked = true;
                        Playbtn3.Visibility = Visibility.Visible;
                        Pausebtn3.Visibility = Visibility.Hidden;

                        Playbtn4.IsChecked = false;
                        Pausebtn4.IsChecked = true;
                        Playbtn4.Visibility = Visibility.Visible;
                        Pausebtn4.Visibility = Visibility.Hidden;

                        //
                        sliProgress.IsEnabled = true;
                        Playbtn1.IsEnabled = true;
                        Playbtn1.IsChecked = true;
                        Pausebtn1.IsChecked = false;
                        Pausebtn1.IsEnabled = true;
                        var stringUri = SelectedItem.FilePath;
                        Uri uri = new Uri(stringUri);
                        SelectedItem.Times++;
                        DataProvider.Ins.DB.SaveChanges();
                        mediaPlayer1.Open(uri);
                        MediaPlayerIsPlaying1 = true;
                        Playbtn1.Visibility = Visibility.Hidden;
                        Pausebtn1.Visibility = Visibility.Visible;
                        thispage.Play.IsChecked = true;
                        mediaPlayer1.Play();
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += timer_Tick;
                        timer.Start();
                        void timer_Tick(object sender, EventArgs e)
                        {
                            if (mediaPlayer1.Source != null)
                            {
                                if (mediaPlayer1.NaturalDuration.HasTimeSpan == true)
                                {
                                    InTime.Content = String.Format("{0}", mediaPlayer1.Position.ToString(@"mm\:ss"));
                                    TotalTime.Content = String.Format("{0}", mediaPlayer1.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                                    sliProgress.Minimum = 0;
                                    sliProgress.Maximum = mediaPlayer1.NaturalDuration.TimeSpan.TotalSeconds;
                                    sliProgress.Value = mediaPlayer1.Position.TotalSeconds;
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
        public DispatcherTimer SleepTimer { get; set; }
        public ICommand Play { get; set; }
        public ICommand Pause { get; set; }
        public ICommand ChangeTime { get; set; }
        public ICommand ChangeVolumn { get; set; }
        public ICommand SkipNext { get; set; }
        public ICommand SkipPrevious { get; set; }
        public ICommand ShuffleVariant { get; set; }
        public ICommand ShuffleDisabled { get; set; }
        public ICommand Loop { get; set; }
        public ICommand NonMute { get; set; }
        public ICommand Mute { get; set; }
        public ICommand Sleeper { get; set; }
        public ICommand CloseSleepTimer { get; set; }
        public ICommand OpenSleepTimer { get; set; }

        public PlayList_InsideViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            LoadData = new RelayCommand<PlayList_Inside>((p) => { return true; }, (p) =>
            {
                thispage = p;
                pl = page_PlayList.listview.SelectedItem as MusicMediaPlayer.Model.PlayList;
                PLName = pl.PlayListName;
                SongCount = "Song: " + pl.SongCount.ToString();
                LoadDanhSach();
            }
            );

            Rename = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                RenamePlayList wd = new RenamePlayList();

                wd.ShowDialog();

                var rename = wd.DataContext as RenamePlayListViewModel;

                if (rename.IsLuu)
                {
                    pl.PlayListName = rename.Title;

                    DataProvider.Ins.DB.SaveChanges();

                    PLName = rename.Title;

                    wd.NamePL.Text = null;
                    rename.IsLuu = false;
                }
            }
            );

            DeletePlayList = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
                MessageBoxYesNo wd = new MessageBoxYesNo();

                var data = wd.DataContext as MessageBoxYesNoViewModel;
                data.Title = "Delete!";
                data.Question = "Do you want to delete it?";
                wd.ShowDialog();

                var result = wd.DataContext as MessageBoxYesNoViewModel;

                if (result.IsYes == true)
                {
                    var song_in_pl = pl.Song;

                    foreach (Song item in song_in_pl.ToList())
                    {
                        item.PlayList.Remove(pl);

                        pl.Song.Remove(item);
                    }

                    DataProvider.Ins.DB.PlayList.Remove(pl);
                    DataProvider.Ins.DB.SaveChanges();

                    var trang = page_PlayList.DataContext as PlayListViewModel;

                    trang.List = new ObservableCollection<MusicMediaPlayer.Model.PlayList>(DataProvider.Ins.DB.PlayList);

                    p.NavigationService.GoBack();
                }
            }
            );
            RemoveSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditSongPlaylistWindow = new EditSongInPlayList();
                EditSongPlaylistWindow.PlaylistName.Text = PLName;
                thispage.NavigationService.Navigate(EditSongPlaylistWindow);
            });
            LoadDataEditPage = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListEdit = new ObservableCollection<Song>(pl.Song);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(EditSongPlaylistWindow.ListSongEdit.ItemsSource);
                view.Filter = FiltersSong;

                bool FiltersSong(object item)
                {
                    if (String.IsNullOrEmpty(EditSongPlaylistWindow.SongFilter.Text))
                        return true;
                    else
                        return ((item as Song).SongTitle.IndexOf(EditSongPlaylistWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                        || (item as Song).Artist.IndexOf(EditSongPlaylistWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                if (ListEdit.Count == 0)
                {
                    EditSongPlaylistWindow.IsThereSong.Visibility = Visibility.Visible;
                }
                else
                {
                    EditSongPlaylistWindow.IsThereSong.Visibility = Visibility.Hidden;
                }
            });
            EditFilterChangeValue = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                EditSongPlaylistWindow = p as EditSongInPlayList;
                CollectionViewSource.GetDefaultView(EditSongPlaylistWindow.ListSongEdit.ItemsSource).Refresh();
            });
            BackToMyPlaylist = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditSongPlaylistWindow.NavigationService.Navigate(thispage);
            });
            DeleteSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                MessageBoxYesNo wd = new MessageBoxYesNo();

                var data = wd.DataContext as MessageBoxYesNoViewModel;
                data.Title = "Delete!";
                data.Question = "Do you want to delete it?";
                wd.ShowDialog();

                var result = wd.DataContext as MessageBoxYesNoViewModel;

                if (result.IsYes == true)
                {
                    pl.Song.Remove(p as Song);
                    pl.SongCount = pl.SongCount - 1;
                    DataProvider.Ins.DB.SaveChanges();
                    SongCount = "Song: " + pl.SongCount.ToString();
                    LoadDanhSach();
                    LoadEditPage();
                }

            }
            );

            AddSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddSongPlayList wd = new AddSongPlayList();
                var trang = wd.DataContext as AddSongPlayListViewModel;
                trang.pl = pl;
                trang.CurrentUser = CurrentUser;
                wd.ShowDialog();
                SongCount = "Song: " + pl.SongCount.ToString();
                LoadDanhSach();
            }
            );
            Play = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer1.Play();
                MediaPlayerIsPlaying1 = true;
                thispage.Play.IsChecked = true;
                thispage.Pause.IsChecked = false;
                p.Play1.IsChecked = true;
                p.Pause1.IsChecked = false;
                p.Play1.Visibility = Visibility.Hidden;
                p.Pause1.Visibility = Visibility.Visible;
            });
            Pause = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer1.Pause();
                MediaPlayerIsPlaying1 = false;
                thispage.Play.IsChecked = false;
                thispage.Pause.IsChecked = true;
                p.Play1.IsChecked = false;
                p.Pause1.IsChecked = true;
                p.Play1.Visibility = Visibility.Visible;
                p.Pause1.Visibility = Visibility.Hidden;
            });
            ChangeTime = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (thispage.listview.Items.Count == 0)
                {
                    return;
                }
                if (sliProgress.IsFocused == true)
                {
                    mediaPlayer1.Stop();
                    mediaPlayer1.Position = TimeSpan.FromSeconds(sliProgress.Value);
                    mediaPlayer1.Play();
                    thispage.Focus();
                }
                if (sliProgress.Value == sliProgress.Maximum)

                {
                    sliProgress.Value = 0;
                    if (thispage.RandomLoop.IsChecked == true)
                    {
                        Random random = new Random();
                        int nextIndex = -1;
                        while (nextIndex < 0 || nextIndex == thispage.listview.SelectedIndex)
                        {
                            nextIndex = random.Next(0, thispage.listview.Items.Count + 1);
                        }
                        thispage.listview.SelectedIndex = -1;
                        thispage.listview.SelectedIndex = nextIndex;
                    }
                    else if (thispage.OneLoop.IsChecked == true)
                    {
                        int nextIndex = thispage.listview.SelectedIndex;
                        thispage.listview.SelectedIndex = -1;
                        thispage.listview.SelectedIndex = nextIndex;
                    }
                    else if (thispage.SequencecyLoop.IsChecked == true)
                    {
                        int nextIndex = (thispage.listview.SelectedIndex + 1) % (thispage.listview.Items.Count);
                        thispage.listview.SelectedIndex = -1;
                        thispage.listview.SelectedIndex = nextIndex;
                    }
                    else
                    {
                        int nextIndex = (thispage.listview.SelectedIndex + 1) % (thispage.listview.Items.Count);
                        thispage.listview.SelectedIndex = -1;
                        thispage.listview.SelectedIndex = nextIndex;
                    }

                }
            });
            ChangeVolumn = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                if (p.Volume1.IsFocused == true)
                {
                    mediaPlayer1.Volume = p.Volume1.Value;
                }
                if (p.Volume1.Value >= 0.8)
                {
                    p.SpeakerHigh1.Visibility = Visibility.Visible;
                    p.SpeakerLow1.Visibility = Visibility.Hidden;
                    p.SpeakerMedium1.Visibility = Visibility.Hidden;
                    p.SpeakerOff1.Visibility = Visibility.Hidden;
                }
                else if (p.Volume1.Value >= 0.4)
                {
                    p.SpeakerHigh1.Visibility = Visibility.Hidden;
                    p.SpeakerLow1.Visibility = Visibility.Hidden;
                    p.SpeakerMedium1.Visibility = Visibility.Visible;
                    p.SpeakerOff1.Visibility = Visibility.Hidden;
                }
                else if (p.Volume1.Value > 0)
                {
                    p.SpeakerHigh1.Visibility = Visibility.Hidden;
                    p.SpeakerLow1.Visibility = Visibility.Visible;
                    p.SpeakerMedium1.Visibility = Visibility.Hidden;
                    p.SpeakerOff1.Visibility = Visibility.Hidden;
                }
                else
                {
                    p.SpeakerHigh1.Visibility = Visibility.Hidden;
                    p.SpeakerLow1.Visibility = Visibility.Hidden;
                    p.SpeakerMedium1.Visibility = Visibility.Hidden;
                    p.SpeakerOff1.Visibility = Visibility.Visible;
                }
            });
            SkipNext = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (thispage.listview.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (thispage.listview.SelectedIndex + 1) % (thispage.listview.Items.Count);
                thispage.listview.SelectedIndex = nextIndex;
            });
            SkipPrevious = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (thispage.listview.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (thispage.listview.SelectedIndex - 1) % (thispage.listview.Items.Count);
                if (nextIndex < 0) nextIndex = thispage.listview.Items.Count - 1;
                thispage.listview.SelectedIndex = nextIndex;
            });
            ShuffleVariant = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop1.IsChecked = false;
                p.OneLoop1.IsChecked = false;
                thispage.SequencecyLoop.IsChecked = false;
                thispage.OneLoop.IsChecked = false;
                thispage.RandomLoop.IsChecked = true;
            });
            ShuffleDisabled = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.RandomLoop1.IsChecked = false;
                p.OneLoop1.IsChecked = false;
                thispage.RandomLoop.IsChecked = false;
                thispage.OneLoop.IsChecked = false;
                thispage.SequencecyLoop.IsChecked = true;
            });
            Loop = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop1.IsChecked = false;
                p.RandomLoop1.IsChecked = false;
                thispage.SequencecyLoop.IsChecked = false;
                thispage.RandomLoop.IsChecked = false;
                thispage.OneLoop.IsChecked = true;
            });
            NonMute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.Volume1.Value = VolumePrevious;
            });
            Mute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                VolumePrevious = p.Volume1.Value;
                p.Volume1.Value = 0;
            });

            // sleep timer
            Sleeper = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window sleepwd = p as Window;
                CountTimer = 0;
                SleepTimerForPlaylist wd = p as SleepTimerForPlaylist;
                Slider slider = wd.knob as Slider;
                double convertsleep = slider.Value * 60;
                int sleepsecond = (int)convertsleep;
                SleepTimer = new DispatcherTimer();
                SleepTimer.Interval = TimeSpan.FromSeconds(1);
                SleepTimer.Tick += timer_Tick;
                SleepTimer.Start();
                void timer_Tick(object sender, EventArgs e)
                {
                    CountTimer++;
                    if (CountTimer == sleepsecond)
                    {
                        mediaPlayer1.Stop();
                        SleepTimer.Stop();
                        Playbtn1.IsChecked = false;
                        Pausebtn1.IsChecked = true;
                        Playbtn1.Visibility = Visibility.Visible;
                        Pausebtn1.Visibility = Visibility.Hidden;
                    }
                    else if (CountTimer == 1)
                    {
                        sleepwd.Close();
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
                SleepTimerForPlaylist sleeptimerView = new SleepTimerForPlaylist();
                sleeptimerView.ShowDialog();
            });

        }

        void LoadDanhSach()
        {
            List = new ObservableCollection<Song>(pl.Song);
        }
        public void LoadEditPage()
        {
            ListEdit = new ObservableCollection<Song>(pl.Song); 
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(EditSongPlaylistWindow.ListSongEdit.ItemsSource);
            view.Filter = FiltersSong;

            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(EditSongPlaylistWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Song).SongTitle.IndexOf(EditSongPlaylistWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                    || (item as Song).Artist.IndexOf(EditSongPlaylistWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (ListEdit.Count == 0)
            {
                EditSongPlaylistWindow.IsThereSong.Visibility = Visibility.Visible;
            }
            else
            {
                EditSongPlaylistWindow.IsThereSong.Visibility = Visibility.Hidden;
            }
        }
    }
}