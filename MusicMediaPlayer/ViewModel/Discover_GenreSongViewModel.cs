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
using System.IO;

namespace MusicMediaPlayer.ViewModel
{
    public class Discover_GenreSongViewModel : BaseViewModel
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
        public Genre CurrentGenre { get; set; }


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
                        //Sync with main window
                        MainViewProgram.Height = 650;
                        PlayerBar.Visibility = Visibility.Hidden;
                        PlayerBarPlaylist.Visibility = Visibility.Hidden;
                        PlayerBarArtist.Visibility = Visibility.Hidden;
                        PlayerBarAlbum.Visibility = Visibility.Hidden;
                        PlayerBarGenre.Visibility = Visibility.Visible;
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
                        MediaPlayerIsPlaying3 = false;
                        if (mediaPlayer2 != null)
                        {
                            mediaPlayer2.Stop();
                        }
                        MediaPlayerIsPlaying2 = false;
                        //Sync with my song
                        Playbtn.IsChecked = false;
                        Pausebtn.IsChecked = true;
                        Playbtn.Visibility = Visibility.Visible;
                        Pausebtn.Visibility = Visibility.Hidden;
                        PlayInvisible.IsChecked = false;
                        PauseInvisible.IsChecked = true;
                        PlayInvisible1.IsChecked = false;
                        PauseInvisible1.IsChecked = true;
                        //Sync with artist
                        Playbtn2.IsChecked = false;
                        Pausebtn2.IsChecked = true;
                        Playbtn2.Visibility = Visibility.Visible;
                        Pausebtn2.Visibility = Visibility.Hidden;
                        //Sync with my playlist
                        Playbtn1.IsChecked = false;
                        Pausebtn1.IsChecked = true;
                        Playbtn1.Visibility = Visibility.Visible;
                        Pausebtn1.Visibility = Visibility.Hidden;
                        //Sync with album
                        Playbtn3.IsChecked = false;
                        Pausebtn3.IsChecked = true;
                        Playbtn3.Visibility = Visibility.Visible;
                        Pausebtn3.Visibility = Visibility.Hidden;
                        //check if the file exists
                        var stringUri = SelectedItem.FilePath;
                        Uri uri = new Uri(stringUri);
                        SelectedItem.Times++;
                        DataProvider.Ins.DB.SaveChanges();
                        if (File.Exists(stringUri) == false)
                        {
                            mediaPlayer4.Stop();
                            InTime.Content = "00:00";
                            sliProgress.Minimum = 0;
                            sliProgress.Maximum = 1;
                            sliProgress.Value = 0;
                            Playbtn4.IsEnabled = false;
                            Playbtn4.IsChecked = false;
                            Playbtn4.Visibility = Visibility.Hidden;
                            Pausebtn4.Visibility = Visibility.Visible;
                            Pausebtn4.IsEnabled = false;
                            MessageBoxFail ms = new MessageBoxFail();
                            ms.ShowDialog();
                            return;
                        }
                        else
                        {
                            //open some function when song is picked
                            sliProgress.IsEnabled = true;
                            Playbtn4.IsEnabled = true;
                            Playbtn4.IsChecked = true;
                            Pausebtn4.IsChecked = false;
                            Pausebtn4.IsEnabled = true;
                            mediaPlayer4.Open(uri);
                            MediaPlayerIsPlaying4 = true;
                            Playbtn4.Visibility = Visibility.Hidden;
                            Pausebtn4.Visibility = Visibility.Visible;
                            mediaPlayer4.Play();
                            DispatcherTimer timer = new DispatcherTimer();
                            timer.Interval = TimeSpan.FromSeconds(1);
                            timer.Tick += timer_Tick;
                            timer.Start();
                            void timer_Tick(object sender, EventArgs e)
                            {
                                if (mediaPlayer4.Source != null)
                                {
                                    if (mediaPlayer4.NaturalDuration.HasTimeSpan == true)
                                    {
                                        InTime.Content = String.Format("{0}", mediaPlayer4.Position.ToString(@"mm\:ss"));
                                        TotalTime.Content = String.Format("{0}", mediaPlayer4.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                                        sliProgress.Minimum = 0;
                                        sliProgress.Maximum = mediaPlayer4.NaturalDuration.TimeSpan.TotalSeconds;
                                        sliProgress.Value = mediaPlayer4.Position.TotalSeconds;
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBoxOK MB = new MessageBoxOK();
                        var data = MB.DataContext as MessageBoxOKViewModel;
                        data.Content = "File not found";
                        MB.ShowDialog();
                    }
                }
            }
        }
        public Discover_GenreSong GenreSongWindow { get; set; }
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
        public Discover_GenreSongViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            CurrentGenre = new Genre();
            ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.GenreId == CurrentGenre.GenreId));
            ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.GenreId == CurrentGenre.GenreId).OrderBy(x => x.Times));
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());

            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                GenreSongWindow = p as Discover_GenreSong;
                ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.GenreId == CurrentGenre.GenreId));
                ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id && x.GenreId == CurrentGenre.GenreId).OrderBy(x => x.Times));
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
            });
            Play = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer4.Play();
                MediaPlayerIsPlaying4 = true;
                p.Play4.IsChecked = true;
                p.Pause4.IsChecked = false;
                p.Play4.Visibility = Visibility.Hidden;
                p.Pause4.Visibility = Visibility.Visible;
            });
            Pause = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                mediaPlayer4.Pause();
                MediaPlayerIsPlaying4 = false;
                p.Play4.IsChecked = false;
                p.Pause4.IsChecked = true;
                p.Play4.Visibility = Visibility.Visible;
                p.Pause4.Visibility = Visibility.Hidden;
            });
            ChangeTime = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (GenreSongWindow.CompositionList.Items.Count == 0)
                {
                    return;
                }
                if (sliProgress.IsFocused == true)
                {
                    mediaPlayer4.Stop();
                    mediaPlayer4.Position = TimeSpan.FromSeconds(sliProgress.Value);
                    mediaPlayer4.Play();
                    GenreSongWindow.Focus();
                }
                if (sliProgress.Value == sliProgress.Maximum)

                {
                    sliProgress.Value = 0;
                    if (GenreSongWindow.RandomLoop.IsChecked == true)
                    {
                        Random random = new Random();
                        int nextIndex = -1;
                        do
                        {
                            nextIndex = random.Next(0, GenreSongWindow.CompositionList.Items.Count);
                        } while (nextIndex < 0 || nextIndex == GenreSongWindow.CompositionList.SelectedIndex);
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
                if (p.Volume4.IsFocused == true)
                {
                    mediaPlayer4.Volume = p.Volume4.Value;
                }
                if (p.Volume4.Value >= 0.8)
                {
                    p.SpeakerHigh4.Visibility = Visibility.Visible;
                    p.SpeakerLow4.Visibility = Visibility.Hidden;
                    p.SpeakerMedium4.Visibility = Visibility.Hidden;
                    p.SpeakerOff4.Visibility = Visibility.Hidden;
                }
                else if (p.Volume4.Value >= 0.4)
                {
                    p.SpeakerHigh4.Visibility = Visibility.Hidden;
                    p.SpeakerLow4.Visibility = Visibility.Hidden;
                    p.SpeakerMedium4.Visibility = Visibility.Visible;
                    p.SpeakerOff4.Visibility = Visibility.Hidden;
                }
                else if (p.Volume4.Value > 0)
                {
                    p.SpeakerHigh4.Visibility = Visibility.Hidden;
                    p.SpeakerLow4.Visibility = Visibility.Visible;
                    p.SpeakerMedium4.Visibility = Visibility.Hidden;
                    p.SpeakerOff4.Visibility = Visibility.Hidden;
                }
                else
                {
                    p.SpeakerHigh4.Visibility = Visibility.Hidden;
                    p.SpeakerLow4.Visibility = Visibility.Hidden;
                    p.SpeakerMedium4.Visibility = Visibility.Hidden;
                    p.SpeakerOff4.Visibility = Visibility.Visible;
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
                p.SequencecyLoop4.IsChecked = false;
                p.OneLoop4.IsChecked = false;
                GenreSongWindow.SequencecyLoop.IsChecked = false;
                GenreSongWindow.OneLoop.IsChecked = false;
                GenreSongWindow.RandomLoop.IsChecked = true;
            });
            ShuffleDisabled = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.RandomLoop4.IsChecked = false;
                p.OneLoop4.IsChecked = false;
                GenreSongWindow.RandomLoop.IsChecked = false;
                GenreSongWindow.OneLoop.IsChecked = false;
                GenreSongWindow.SequencecyLoop.IsChecked = true;
            });
            Loop = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.SequencecyLoop4.IsChecked = false;
                p.RandomLoop4.IsChecked = false;
                GenreSongWindow.SequencecyLoop.IsChecked = false;
                GenreSongWindow.RandomLoop.IsChecked = false;
                GenreSongWindow.OneLoop.IsChecked = true;
            });
            NonMute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                p.Volume4.Value = VolumePrevious;
                mediaPlayer4.Volume = p.Volume4.Value;
            });
            Mute = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                VolumePrevious = p.Volume4.Value;
                p.Volume4.Value = 0;
                mediaPlayer4.Volume = 0;
            });

            // sleep timer
            Sleeper = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window sleepwd = p as Window;
                CountTimer = 0;
                SleepTimerForGenre wd = p as SleepTimerForGenre;
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
                        mediaPlayer4.Stop();
                        SleepTimer.Stop();
                        Playbtn4.IsChecked = false;
                        Pausebtn4.IsChecked = true;
                        Playbtn4.Visibility = Visibility.Visible;
                        Pausebtn4.Visibility = Visibility.Hidden;
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
                SleepTimerForGenre sleeptimerView = new SleepTimerForGenre();
                sleeptimerView.ShowDialog();
            });
        }
    }
}
