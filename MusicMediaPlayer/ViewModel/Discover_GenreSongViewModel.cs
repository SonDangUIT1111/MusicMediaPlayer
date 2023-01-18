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

namespace MusicMediaPlayer.ViewModel
{
    public class Discover_GenreSongViewModel : BaseViewModel
    {
        public MediaPlayer mediaPlayer = new MediaPlayer();
        private ObservableCollection<Song> _ListSong;
        public ObservableCollection<Song> ListSong { get { return _ListSong; } set { _ListSong = value; OnPropertyChanged(); } }
        private ObservableCollection<Song> _ListPopular;
        public ObservableCollection<Song> ListPopular { get { return _ListPopular; } set { _ListPopular = value; OnPropertyChanged(); } }


        //
        public CurrentUserAccountModel CurrentUser { get; set; }
        public Genre CurrentGenre { get; set; }
        

        //player bar
        private bool _mediaPlayerIsPlaying = false;
        public bool MediaPlayerIsPlaying { get => _mediaPlayerIsPlaying; set => _mediaPlayerIsPlaying = value; }
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
                        PlayerBarGenre.Visibility = Visibility.Visible;
                        SkipPreviousbtn.IsEnabled = true;
                        SkipNextbtn.IsEnabled = true;

                        sliProgress.IsEnabled = true;
                        Playbtn.IsEnabled = true;
                        Playbtn.IsChecked = true;
                        Pausebtn.IsChecked = false;

                        Pausebtn.IsEnabled = true;
                        var stringUri = SelectedItem.FilePath;
                        Uri uri = new Uri(stringUri);
                        SelectedItem.Times++;
                        DataProvider.Ins.DB.SaveChanges();
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
                    catch (Exception)
                    {
                        MessageBox.Show("File not found");
                    }
                }
            }
        }
        public Discover_GenreSong GenreSongWindow { get; set; }
        public Button SkipPreviousbtn { get; set; }
        public Button SkipNextbtn { get; set; }
        public ToggleButton Playbtn { get; set; }
        public ToggleButton Pausebtn { get; set; }
        public Label InTime { get; set; }
        public Label TotalTime { get; set; }
        public Slider sliProgress { get; set; }
        public Grid MainViewProgram { get; set; }
        public Grid PlayerBar { get; set; }
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
        public Discover_GenreSongViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            CurrentGenre = new Genre();
            ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentGenre.GenreId));
            ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentGenre.GenreId).OrderBy(x => x.Times));
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());


            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                GenreSongWindow = p as Discover_GenreSong;
                ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentGenre.GenreId));
                ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.AlbumId == CurrentGenre.GenreId).OrderBy(x => x.Times));
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
            });
            Play = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Play();
                MediaPlayerIsPlaying = true;
                p.Play2.IsChecked = true;
                p.Pause2.IsChecked = false;
                p.Play2.Visibility = Visibility.Hidden;
                p.Pause2.Visibility = Visibility.Visible;
            });
            Pause = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer?.Pause();
                MediaPlayerIsPlaying = false;
                p.Play2.IsChecked = false;
                p.Pause2.IsChecked = true;
                p.Play2.Visibility = Visibility.Visible;
                p.Pause2.Visibility = Visibility.Hidden;
            });
            ChangeTime = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (GenreSongWindow.CompositionList.Items.Count == 0)
                {
                    return;
                }
                if (sliProgress.IsFocused == true)
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
                    mediaPlayer.Play();
                    GenreSongWindow.Focus();
                }
                if (sliProgress.Value == sliProgress.Maximum)

                {
                    sliProgress.Value = 0;
                    if (GenreSongWindow.RandomLoop.IsChecked == true)
                    {
                        Random random = new Random();
                        int nextIndex = -1;
                        while (nextIndex < 0 || nextIndex == GenreSongWindow.CompositionList.SelectedIndex)
                        {
                            nextIndex = random.Next(0, GenreSongWindow.CompositionList.Items.Count + 1);
                        }
                        GenreSongWindow.CompositionList.SelectedIndex = -1;
                        GenreSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }
                    else if (GenreSongWindow.OneLoop.IsChecked == true)
                    {
                        int nextIndex = GenreSongWindow.CompositionList.SelectedIndex;
                        GenreSongWindow.CompositionList.SelectedIndex = -1;
                        GenreSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }
                    else if (GenreSongWindow.SequencecyLoop.IsChecked == true)
                    {
                        int nextIndex = (GenreSongWindow.CompositionList.SelectedIndex + 1) % (GenreSongWindow.CompositionList.Items.Count);
                        GenreSongWindow.CompositionList.SelectedIndex = -1;
                        GenreSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }
                    else
                    {
                        int nextIndex = (GenreSongWindow.CompositionList.SelectedIndex + 1) % (GenreSongWindow.CompositionList.Items.Count);
                        GenreSongWindow.CompositionList.SelectedIndex = -1;
                        GenreSongWindow.CompositionList.SelectedIndex = nextIndex;
                    }

                }
            });
            ChangeVolumn = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                if (p.Volume2.IsFocused == true)
                {
                    mediaPlayer.Volume = p.Volume2.Value;
                }
                if (p.Volume2.Value >= 0.8)
                {
                    p.SpeakerHigh2.Visibility = Visibility.Visible;
                    p.SpeakerLow2.Visibility = Visibility.Hidden;
                    p.SpeakerMedium2.Visibility = Visibility.Hidden;
                    p.SpeakerOff2.Visibility = Visibility.Hidden;
                }
                else if (p.Volume2.Value >= 0.4)
                {
                    p.SpeakerHigh2.Visibility = Visibility.Hidden;
                    p.SpeakerLow2.Visibility = Visibility.Hidden;
                    p.SpeakerMedium2.Visibility = Visibility.Visible;
                    p.SpeakerOff2.Visibility = Visibility.Hidden;
                }
                else if (p.Volume2.Value > 0)
                {
                    p.SpeakerHigh2.Visibility = Visibility.Hidden;
                    p.SpeakerLow2.Visibility = Visibility.Visible;
                    p.SpeakerMedium2.Visibility = Visibility.Hidden;
                    p.SpeakerOff2.Visibility = Visibility.Hidden;
                }
                else
                {
                    p.SpeakerHigh2.Visibility = Visibility.Hidden;
                    p.SpeakerLow2.Visibility = Visibility.Hidden;
                    p.SpeakerMedium2.Visibility = Visibility.Hidden;
                    p.SpeakerOff2.Visibility = Visibility.Visible;
                }
            });
            SkipNext = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (GenreSongWindow.CompositionList.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (GenreSongWindow.CompositionList.SelectedIndex + 1) % (GenreSongWindow.CompositionList.Items.Count);
                GenreSongWindow.CompositionList.SelectedIndex = nextIndex;
            });
            SkipPrevious = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (GenreSongWindow.CompositionList.Items.Count == 0)
                {
                    return;
                }
                int nextIndex = (GenreSongWindow.CompositionList.SelectedIndex - 1) % (GenreSongWindow.CompositionList.Items.Count);
                if (nextIndex < 0) nextIndex = GenreSongWindow.CompositionList.Items.Count - 1;
                GenreSongWindow.CompositionList.SelectedIndex = nextIndex;
            });
            ShuffleVariant = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop2.IsChecked = false;
                p.OneLoop2.IsChecked = false;
                GenreSongWindow.SequencecyLoop.IsChecked = false;
                GenreSongWindow.OneLoop.IsChecked = false;
                GenreSongWindow.RandomLoop.IsChecked = true;
            });
            ShuffleDisabled = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.RandomLoop2.IsChecked = false;
                p.OneLoop2.IsChecked = false;
                GenreSongWindow.RandomLoop.IsChecked = false;
                GenreSongWindow.OneLoop.IsChecked = false;
                GenreSongWindow.SequencecyLoop.IsChecked = true;
            });
            Loop = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop2.IsChecked = false;
                p.RandomLoop2.IsChecked = false;
                GenreSongWindow.SequencecyLoop.IsChecked = false;
                GenreSongWindow.RandomLoop.IsChecked = false;
                GenreSongWindow.OneLoop.IsChecked = true;
            });
            NonMute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.Volume2.Value = VolumePrevious;
            });
            Mute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                VolumePrevious = p.Volume2.Value;
                p.Volume2.Value = 0;
            });

            // sleep timer
            Sleeper = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window sleepwd = p as Window;
                CountTimer = 0;
                SleepTimerForArtist wd = p as SleepTimerForArtist;
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
                        mediaPlayer.Stop();
                        SleepTimer.Stop();
                        Playbtn.IsChecked = false;
                        Pausebtn.IsChecked = true;
                        Playbtn.Visibility = Visibility.Visible;
                        Pausebtn.Visibility = Visibility.Hidden;
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
