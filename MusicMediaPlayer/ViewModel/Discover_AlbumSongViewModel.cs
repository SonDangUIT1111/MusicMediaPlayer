using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using MusicMediaPlayer.ViewModel;
using MusicMediaPlayer;

namespace MusicMediaPlayer.ViewModel
{
    public class Discover_AlbumSongViewModel : BaseViewModel
    {
        public MediaPlayer mediaPlayer = new MediaPlayer();
        public MediaPlayer mediaPlayer1 = new MediaPlayer();
        public MediaPlayer mediaPlayer2 = new MediaPlayer();
        public MediaPlayer mediaPlayer3 = new MediaPlayer();
        public MediaPlayer mediaPlayer4 = new MediaPlayer();
        private ObservableCollection<Song> _ListSong;
        public ObservableCollection<Song> ListSong { get { return _ListSong; } set { _ListSong = value; OnPropertyChanged(); } }
        private ObservableCollection<Song> _ListPopular;
        public ObservableCollection<Song> ListPopular { get { return _ListPopular; } set { _ListPopular = value; OnPropertyChanged(); } }


        //
        public CurrentUserAccountModel CurrentUser { get; set; }
        public Album CurrentAlbum { get; set; }


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
                        MainViewProgram.Height = 650;
                        PlayerBar.Visibility = Visibility.Hidden;
                        PlayerBarPlaylist.Visibility = Visibility.Hidden;
                        PlayerBarArtist.Visibility = Visibility.Hidden;
                        PlayerBarAlbum.Visibility = Visibility.Visible;
                        PlayerBarGenre.Visibility = Visibility.Hidden;
                        SkipPreviousbtn.IsEnabled = true;
                        SkipNextbtn.IsEnabled = true;
                        if (mediaPlayer != null)
                        {
                            mediaPlayer.Stop();
                        }
                        MediaPlayerIsPlaying = false;
                        if (mediaPlayer1 != null)
                        {
                            mediaPlayer1.Stop();
                        }
                        MediaPlayerIsPlaying1 = false;
                        if (mediaPlayer3 != null)
                        {
                            mediaPlayer3.Stop();
                        }
                        MediaPlayerIsPlaying2 = false;
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
                        PlayInvisible1.IsChecked = false;
                        PauseInvisible1.IsChecked = true;

                        Playbtn2.IsChecked = false;
                        Pausebtn2.IsChecked = true;
                        Playbtn2.Visibility = Visibility.Visible;
                        Pausebtn2.Visibility = Visibility.Hidden;

                        Playbtn1.IsChecked = false;
                        Pausebtn1.IsChecked = true;
                        Playbtn1.Visibility = Visibility.Visible;
                        Pausebtn1.Visibility = Visibility.Hidden;

                        Playbtn4.IsChecked = false;
                        Pausebtn4.IsChecked = true;
                        Playbtn4.Visibility = Visibility.Visible;
                        Pausebtn4.Visibility = Visibility.Hidden;

                        //

                        sliProgress.IsEnabled = true;
                        Playbtn3.IsEnabled = true;
                        Playbtn3.IsChecked = true;
                        Pausebtn3.IsChecked = false;

                        Pausebtn3.IsEnabled = true;
                        var stringUri = SelectedItem.FilePath;
                        Uri uri = new Uri(stringUri);
                        SelectedItem.Times++;
                        DataProvider.Ins.DB.SaveChanges();
                        mediaPlayer3.Open(uri);
                        MediaPlayerIsPlaying3 = true;
                        Playbtn3.Visibility = Visibility.Hidden;
                        Pausebtn3.Visibility = Visibility.Visible;

                        mediaPlayer3.Play();
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += timer_Tick;
                        timer.Start();
                        void timer_Tick(object sender, EventArgs e)
                        {
                            if (mediaPlayer3.Source != null)
                            {
                                if (mediaPlayer3.NaturalDuration.HasTimeSpan == true)
                                {
                                    InTime.Content = String.Format("{0}", mediaPlayer3.Position.ToString(@"mm\:ss"));
                                    TotalTime.Content = String.Format("{0}", mediaPlayer3.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                                    sliProgress.Minimum = 0;
                                    sliProgress.Maximum = mediaPlayer3.NaturalDuration.TimeSpan.TotalSeconds;
                                    sliProgress.Value = mediaPlayer3.Position.TotalSeconds;
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("File not found");
                    }
                }
            }
        }
        public Discover_AlbumSong AlbumSongWindow { get; set; }
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
        //


        //define command
        public ICommand LoadData { get; set; }
        //
        public Discover_AlbumSongViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            CurrentAlbum = new Album();
            ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentAlbum.AlbumId));
            ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentAlbum.AlbumId).OrderBy(x => x.Times));
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            

            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                AlbumSongWindow = p as Discover_AlbumSong;
                ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentAlbum.AlbumId));
                ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentAlbum.AlbumId).OrderBy(x => x.Times));
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
            });
            Play = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer3.Play();
                MediaPlayerIsPlaying3 = true;
                p.Play3.IsChecked = true;
                p.Pause3.IsChecked = false;
                p.Play3.Visibility = Visibility.Hidden;
                p.Pause3.Visibility = Visibility.Visible;
            });
            Pause = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer3.Pause();
                MediaPlayerIsPlaying3 = false;
                p.Play3.IsChecked = false;
                p.Pause3.IsChecked = true;
                p.Play3.Visibility = Visibility.Visible;
                p.Pause3.Visibility = Visibility.Hidden;
            });
            ChangeTime = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (AlbumSongWindow.CompositionList.Items.Count == 0)
                {
                    return;
                }
                if (sliProgress.IsFocused == true)
                {
                    mediaPlayer3.Stop();
                    mediaPlayer3.Position = TimeSpan.FromSeconds(sliProgress.Value);
                    mediaPlayer3.Play();
                    AlbumSongWindow.Focus();
                }
                if (sliProgress.Value == sliProgress.Maximum)

                {
                    sliProgress.Value = 0;
                    if (AlbumSongWindow.RandomLoop.IsChecked == true)
                    {
                        Random random = new Random();
                        int nextIndex = -1;
                        while (nextIndex < 0 || nextIndex == AlbumSongWindow.CompositionList.SelectedIndex)
                        {
                            nextIndex = random.Next(0, AlbumSongWindow.CompositionList.Items.Count + 1);
                        }
                        AlbumSongWindow.CompositionList.SelectedIndex = -1;
                        AlbumSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }
                    else if (AlbumSongWindow.OneLoop.IsChecked == true)
                    {
                        int nextIndex = AlbumSongWindow.CompositionList.SelectedIndex;
                        AlbumSongWindow.CompositionList.SelectedIndex = -1;
                        AlbumSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }
                    else if (AlbumSongWindow.SequencecyLoop.IsChecked == true)
                    {
                        int nextIndex = (AlbumSongWindow.CompositionList.SelectedIndex + 1) % (AlbumSongWindow.CompositionList.Items.Count);
                        AlbumSongWindow.CompositionList.SelectedIndex = -1;
                        AlbumSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }
                    else
                    {
                        int nextIndex = (AlbumSongWindow.CompositionList.SelectedIndex + 1) % (AlbumSongWindow.CompositionList.Items.Count);
                        AlbumSongWindow.CompositionList.SelectedIndex = -1;
                        AlbumSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }

                }
            });
            ChangeVolumn = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                if (p.Volume3.IsFocused == true)
                {
                    mediaPlayer3.Volume = p.Volume3.Value;
                }
                if (p.Volume3.Value >= 0.8)
                {
                    p.SpeakerHigh3.Visibility = Visibility.Visible;
                    p.SpeakerLow3.Visibility = Visibility.Hidden;
                    p.SpeakerMedium3.Visibility = Visibility.Hidden;
                    p.SpeakerOff3.Visibility = Visibility.Hidden;
                }
                else if (p.Volume3.Value >= 0.4)
                {
                    p.SpeakerHigh3.Visibility = Visibility.Hidden;
                    p.SpeakerLow3.Visibility = Visibility.Hidden;
                    p.SpeakerMedium3.Visibility = Visibility.Visible;
                    p.SpeakerOff3.Visibility = Visibility.Hidden;
                }
                else if (p.Volume3.Value > 0)
                {
                    p.SpeakerHigh3.Visibility = Visibility.Hidden;
                    p.SpeakerLow3.Visibility = Visibility.Visible;
                    p.SpeakerMedium3.Visibility = Visibility.Hidden;
                    p.SpeakerOff3.Visibility = Visibility.Hidden;
                }
                else
                {
                    p.SpeakerHigh3.Visibility = Visibility.Hidden;
                    p.SpeakerLow3.Visibility = Visibility.Hidden;
                    p.SpeakerMedium3.Visibility = Visibility.Hidden;
                    p.SpeakerOff3.Visibility = Visibility.Visible;
                }
            });
            SkipNext = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (AlbumSongWindow.CompositionList.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (AlbumSongWindow.CompositionList.SelectedIndex + 1) % (AlbumSongWindow.CompositionList.Items.Count);
                AlbumSongWindow.CompositionList.SelectedIndex = nextIndex;
            });
            SkipPrevious = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (AlbumSongWindow.CompositionList.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (AlbumSongWindow.CompositionList.SelectedIndex - 1) % (AlbumSongWindow.CompositionList.Items.Count);
                if (nextIndex < 0) nextIndex = AlbumSongWindow.CompositionList.Items.Count - 1;
                AlbumSongWindow.CompositionList.SelectedIndex = nextIndex;
            });
            ShuffleVariant = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop3.IsChecked = false;
                p.OneLoop3.IsChecked = false;
                AlbumSongWindow.SequencecyLoop.IsChecked = false;
                AlbumSongWindow.OneLoop.IsChecked = false;
                AlbumSongWindow.RandomLoop.IsChecked = true;
            });
            ShuffleDisabled = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.RandomLoop3.IsChecked = false;
                p.OneLoop3.IsChecked = false;
                AlbumSongWindow.RandomLoop.IsChecked = false;
                AlbumSongWindow.OneLoop.IsChecked = false;
                AlbumSongWindow.SequencecyLoop.IsChecked = true;
            });
            Loop = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop3.IsChecked = false;
                p.RandomLoop3.IsChecked = false;
                AlbumSongWindow.SequencecyLoop.IsChecked = false;
                AlbumSongWindow.RandomLoop.IsChecked = false;
                AlbumSongWindow.OneLoop.IsChecked = true;
            });
            NonMute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.Volume3.Value = VolumePrevious;
            });
            Mute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                VolumePrevious = p.Volume3.Value;
                p.Volume3.Value = 0;
            });

            // sleep timer
            Sleeper = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window sleepwd = p as Window;
                CountTimer = 0;
                SleepTimerForAlbum wd = p as SleepTimerForAlbum;
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
                        mediaPlayer3.Stop();
                        SleepTimer.Stop();
                        Playbtn3.IsChecked = false;
                        Pausebtn3.IsChecked = true;
                        Playbtn3.Visibility = Visibility.Visible;
                        Pausebtn3.Visibility = Visibility.Hidden;
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
                SleepTimerForArtist sleeptimerView = new SleepTimerForArtist();
                sleeptimerView.ShowDialog();
            });
        }
    }
}

