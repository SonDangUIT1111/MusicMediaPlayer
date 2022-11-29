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

namespace MusicMediaPlayer.ViewModel
{

   public class SongViewModel : BaseViewModel
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private ObservableCollection<Song> _List;
        public ObservableCollection<Song> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        public MySong Window { get; set; }
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
                        var stringUri = SelectedItem.FilePath;
                        Uri uri = new Uri(stringUri);
                        mediaPlayer.Open(uri);
                        MediaPlayerIsPlaying = true;
                        Window.Play.Visibility = Visibility.Hidden;
                        Window.Pause.Visibility = Visibility.Visible;
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
                                    Window.InTime.Content = String.Format("{0}", mediaPlayer.Position.ToString(@"mm\:ss"));
                                    Window.TotalTime.Content = String.Format("{0}",  mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                                    Window.sliProgress.Minimum = 0;
                                    Window.sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                                    Window.sliProgress.Value = mediaPlayer.Position.TotalSeconds;
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
        private bool _mediaPlayerIsPlaying = false;
        private bool _userIsDraggingSlider = false;
        public bool UserIsDraggingSlider { get => _userIsDraggingSlider; set => _userIsDraggingSlider = value; }
        public bool MediaPlayerIsPlaying { get => _mediaPlayerIsPlaying; set => _mediaPlayerIsPlaying = value; }
        private string _TitleToAdd;
        public string TitleToAdd { get { return _TitleToAdd; } set { _TitleToAdd = value; } }
        private string _ArtistToAdd;
        public string ArtistToAdd { get { return _ArtistToAdd; } set { _ArtistToAdd = value; } }
        private string _FilePathToAdd;
        public string FilePathToAdd { get { return _FilePathToAdd; } set { _FilePathToAdd = value; } }
        private double _VolumePrevious;
        public double VolumePrevious { get => _VolumePrevious; set => _VolumePrevious = value; }
        public ICommand AddFile { get; set; }
        public ICommand Complete { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand LoadData { get; set; }
        public ICommand Play { get; set; }
        public ICommand Stop { get; set; }
        public ICommand Pause { get; set; }
        public ICommand FilterChangeValue { get; set; }
        public ICommand AddSong { get; set; }
        public ICommand ChangeInfoSong { get; set; }
        public ICommand DeleteSong { get; set; }
        public ICommand Refresh { get; set; }
        public ICommand ChangeFavourite { get; set; }
        public ICommand DragStarted { get; set; }
        public ICommand DragCompleted { get; set; }
        public ICommand ChangeTime { get; set; }
        public ICommand ChangeVolumn { get; set; }
        public ICommand SkipNext { get; set; }
        public ICommand SkipPrevious { get; set; }
        public ICommand ShuffleVariant { get; set; }
        public ICommand ShuffleDisabled { get; set; }
        public ICommand Loop { get; set; }
        public ICommand NonMute { get; set; }
        public ICommand Mute { get; set; }
        public SongViewModel()
        {
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs);
            Play = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Play();
                MediaPlayerIsPlaying = true;
                Window.Play.Visibility = Visibility.Hidden;
                Window.Pause.Visibility = Visibility.Visible;
            });
            Pause = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                mediaPlayer?.Pause();
                MediaPlayerIsPlaying = false;
                Window.Play.Visibility = Visibility.Visible;
                Window.Pause.Visibility = Visibility.Hidden;
            });
            Stop = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Stop();
            });
            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
             {
                 Window = p as MySong;

                 var data = DataProvider.Ins.DB.Songs;
                 CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Window.ListSong.ItemsSource);
                 view.Filter = FiltersSong;

                 bool FiltersSong(object item)
                 {
                     if (String.IsNullOrEmpty(Window.SongFilter.Text))
                         return true;
                     else
                         return ((item as Song).SongTitle.IndexOf(Window.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0

                         || (item as Song).Artist.IndexOf(Window.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                 }
             });
            FilterChangeValue = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                Window = p as MySong;
                CollectionViewSource.GetDefaultView(Window.ListSong.ItemsSource).Refresh();
            });
            AddSong = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                AddSongToApp addsongview = new AddSongToApp();
                addsongview.ShowDialog();


             });
            DeleteSong = new RelayCommand<object>((p) => 
            {
                return true;
            }, (p)
                 =>
             {

                 MessageBoxResult result = MessageBox.Show("Do you want to delete it", "Delete song", MessageBoxButton.YesNo);
                 if (result == MessageBoxResult.Yes)
                 {
                     var item = p as Song;
                     if (item != null)
                     {
                         DataProvider.Ins.DB.Songs.Remove(item);
                         DataProvider.Ins.DB.SaveChanges();
                         Window.ListSong.Items.Refresh();
                         Load();
                     }
                 }
                 else return;

             });
            Refresh = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                Window.ListSong.Items.Refresh();
                Load();
            });
            ChangeFavourite = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 var item = p as Song;
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
            Complete = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                var Window = p as AddSongToApp;
                if (String.IsNullOrEmpty(FilePathToAdd))
                {
                    MessageBox.Show("Please add song file");
                    return;
                }
                else if (String.IsNullOrEmpty(Window.TitleSong.Text))
                {
                    MessageBox.Show("Please enter song name");
                    return;
                }
                else if (String.IsNullOrEmpty(Window.ArtistSong.Text))
                {
                    MessageBox.Show("Please enter song artist");
                    return;
                }
                try
                {
                    DataProvider.Ins.DB.Songs.Add(new Song() { Artist = ArtistToAdd, SongTitle = TitleToAdd, FilePath = FilePathToAdd });
                    DataProvider.Ins.DB.SaveChanges();
                    Load();
                    MessageBox.Show("Add successfully");
                    Window.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Add process failed due to some invalid infomation");
                }

            });
            Cancel = new RelayCommand<Window>((p) => { return true; }, (p) => {
                var Window = p as AddSongToApp;
                Window.Close();
            });
            ChangeTime = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
               if (Window.sliProgress.IsFocused == true)
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Position = TimeSpan.FromSeconds(Window.sliProgress.Value);
                    mediaPlayer.Play();
                    Window.Focus();
                }
               if (Window.sliProgress.Value == Window.sliProgress.Maximum)
                
                {
                    Window.sliProgress.Value = 0;
                     if (Window.RandomLoop.IsChecked == true)
                    {
                        Random random = new Random();
                        int nextIndex = -1;
                        while (nextIndex < 0 || nextIndex == Window.ListSong.SelectedIndex)
                        {
                            nextIndex = random.Next(0, Window.ListSong.Items.Count + 1);
                        }
                        Window.ListSong.SelectedIndex = nextIndex;
                    }
                    else if (Window.OneLoop.IsChecked == true)
                    {
                        int nextIndex = Window.ListSong.SelectedIndex;
                        Window.ListSong.SelectedIndex = -1;
                        Window.ListSong.SelectedIndex = nextIndex;
                    }
                    else if (Window.SequencecyLoop.IsChecked == true)
                    {
                        int nextIndex = (Window.ListSong.SelectedIndex + 1) % (Window.ListSong.Items.Count);
                        Window.ListSong.SelectedIndex = nextIndex;
                    }
                    else
                    {
                        int nextIndex = (Window.ListSong.SelectedIndex + 1) % (Window.ListSong.Items.Count);
                        Window.ListSong.SelectedIndex = nextIndex;
                    }

                }
            });
            ChangeVolumn = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Volume = Window.Volume.Value;
                if (Window.Volume.Value >= 0.8)
                {
                    Window.SpeakerHigh.Visibility = Visibility.Visible;
                    Window.SpeakerLow.Visibility = Visibility.Hidden;
                    Window.SpeakerMedium.Visibility = Visibility.Hidden;
                    Window.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else if (Window.Volume.Value >= 0.4)
                {
                    Window.SpeakerHigh.Visibility = Visibility.Hidden;
                    Window.SpeakerLow.Visibility = Visibility.Hidden;
                    Window.SpeakerMedium.Visibility = Visibility.Visible;
                    Window.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else if (Window.Volume.Value > 0)
                {
                    Window.SpeakerHigh.Visibility = Visibility.Hidden;
                    Window.SpeakerLow.Visibility = Visibility.Visible;
                    Window.SpeakerMedium.Visibility = Visibility.Hidden;
                    Window.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else
                {
                    Window.SpeakerHigh.Visibility = Visibility.Hidden;
                    Window.SpeakerLow.Visibility = Visibility.Hidden;
                    Window.SpeakerMedium.Visibility = Visibility.Hidden;
                    Window.SpeakerOff.Visibility = Visibility.Visible;
                }
            });
            SkipNext = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                int nextIndex = (Window.ListSong.SelectedIndex + 1) % (Window.ListSong.Items.Count);
                Window.ListSong.SelectedIndex = nextIndex;
            });
            SkipPrevious = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                int nextIndex = (Window.ListSong.SelectedIndex - 1) % (Window.ListSong.Items.Count);
                Window.ListSong.SelectedIndex = nextIndex;
            });
            ShuffleVariant = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window.SequencecyLoop.IsChecked = false;
                Window.OneLoop.IsChecked = false;
            });
            ShuffleDisabled = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window.RandomLoop.IsChecked = false;
                Window.OneLoop.IsChecked = false;
            });
            Loop = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window.SequencecyLoop.IsChecked = false;
                Window.RandomLoop.IsChecked = false;
            });
            NonMute = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window.Volume.Value = VolumePrevious;
            });
            Mute = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                VolumePrevious = Window.Volume.Value;
                Window.Volume.Value = 0;
            });
        }
        void Load()
        {
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs);
        }

       
    }
}
