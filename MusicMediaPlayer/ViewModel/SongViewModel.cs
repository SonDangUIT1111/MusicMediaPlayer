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

namespace MusicMediaPlayer.ViewModel
{

   public class SongViewModel : BaseViewModel
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private ObservableCollection<Song> _List;
        public ObservableCollection<Song> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<Song> _TopTrending;
        public ObservableCollection<Song> TopTrending { get => _TopTrending; set { _TopTrending = value; OnPropertyChanged(); } }
        public MySong MySongWindow { get; set; }
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
                        MySongWindow.sliProgress.IsEnabled = true;
                        MySongWindow.Play.IsEnabled = true;
                        MySongWindow.Play.IsChecked = true;
                        MySongWindow.Pause.IsChecked = false;
                        if (CanChangeTOP_CD == true)
                        {
                            MySongWindow.TopTrendExpander.IsExpanded = false;
                            MySongWindow.CDCircle.IsExpanded = false;
                            MySongWindow.CDCircle.IsExpanded = true;
                        }
                        MySongWindow.Pause.IsEnabled = true;
                        var stringUri = SelectedItem.FilePath;
                        Uri uri = new Uri(stringUri);
                        SelectedItem.Times++;
                        DataProvider.Ins.DB.SaveChanges();
                        mediaPlayer.Open(uri);
                        MediaPlayerIsPlaying = true;
                        MySongWindow.Play.Visibility = Visibility.Hidden;
                        MySongWindow.Pause.Visibility = Visibility.Visible;
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
                                    MySongWindow.InTime.Content = String.Format("{0}", mediaPlayer.Position.ToString(@"mm\:ss"));
                                    MySongWindow.TotalTime.Content = String.Format("{0}",  mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                                    MySongWindow.sliProgress.Minimum = 0;
                                    MySongWindow.sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                                    MySongWindow.sliProgress.Value = mediaPlayer.Position.TotalSeconds;
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
        // menu
        private bool _IsPickRecent = false;
        public bool IsPickRecent { get => _IsPickRecent; set => _IsPickRecent = value; }
        private bool _IsPickMySong = true;
        public bool IsPickMySong { get => _IsPickMySong; set => _IsPickMySong = value; }
        private bool _CanChangeTOP_CD = true;
        public bool CanChangeTOP_CD { get => _CanChangeTOP_CD; set => _CanChangeTOP_CD = value;}
        //
        private bool _mediaPlayerIsPlaying = false;
        private bool _userIsDraggingSlider = false;
        public bool UserIsDraggingSlider { get => _userIsDraggingSlider; set => _userIsDraggingSlider = value; }
        public bool MediaPlayerIsPlaying { get => _mediaPlayerIsPlaying; set => _mediaPlayerIsPlaying = value; }
        // add,delete
        private string _TitleToAdd;
        public string TitleToAdd { get { return _TitleToAdd; } set { _TitleToAdd = value; } }
        private string _ArtistToAdd;
        public string ArtistToAdd { get { return _ArtistToAdd; } set { _ArtistToAdd = value; } }
        private string _FilePathToAdd;
        public string FilePathToAdd { get { return _FilePathToAdd; } set { _FilePathToAdd = value; OnPropertyChanged(); } }
        private string _ImagePathToAdd;
        public string ImagePathToAdd { get { return _ImagePathToAdd; } set { _ImagePathToAdd = value;OnPropertyChanged(); } }

        private string _TitleToChange;
        public string TitleToChange { get { return _TitleToChange; } set { _TitleToChange = value; } }
        private string _ArtistToChange;
        public string ArtistToChange { get { return _ArtistToChange; } set { _ArtistToChange = value; } }
        private string _ImagePathToChange;
        public string ImagePathToChange { get { return _ImagePathToChange; } set { _ImagePathToChange = value; OnPropertyChanged(); } }
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
        public ICommand Stop { get; set; }
        public ICommand Pause { get; set; }
        public ICommand FilterChangeValue { get; set; }
        public ICommand AddSong { get; set; }
        public ICommand OverOption { get; set; }
        public ICommand LeaveOption { get; set; }
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

        //sleep timer
        public ICommand Sleeper { get; set; }
        public ICommand CloseSleepTimer { get; set; }
        public ICommand OpenSleepTimer { get; set; }

        //menu song
        public ICommand RecentSong { get; set; }
        public ICommand AllSong { get; set; }

        //
        public ICommand ShowTopOrCD { get; set; }
        public ICommand PlayShortcut { get; set; }

        public SongViewModel()
        {
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs);
            TopTrending = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.OrderByDescending(x => x.Times).ToList());
            Play = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Play();
                MediaPlayerIsPlaying = true;
                MySongWindow.Play.IsChecked = true;
                MySongWindow.Pause.IsChecked = false;
                MySongWindow.Play.Visibility = Visibility.Hidden;
                MySongWindow.Pause.Visibility = Visibility.Visible;
            });
            Pause = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                mediaPlayer?.Pause();
                MediaPlayerIsPlaying = false;
                MySongWindow.Play.IsChecked = false;
                MySongWindow.Pause.IsChecked = true;
                MySongWindow.Play.Visibility = Visibility.Visible;
                MySongWindow.Pause.Visibility = Visibility.Hidden;
            });
            Stop = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Stop();
            });
            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
             {
                 MySongWindow = p as MySong;
                 var data = DataProvider.Ins.DB.Songs;
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
                CollectionViewSource.GetDefaultView(MySongWindow.ListSong.ItemsSource).Refresh();
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
                         MySongWindow.ListSong.Items.Refresh();
                         LoadCommon();
                     }
                 }
                 else return;

             });

            Changing = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditSongInApp wd = p as EditSongInApp;
                if (String.IsNullOrEmpty(wd.ChangeTitleSong.Text))
                {
                    MessageBox.Show("Please enter new title");
                    return;
                }
                if (String.IsNullOrEmpty(wd.ChangeArtistSong.Text))
                {
                    MessageBox.Show("Please enter new artist");
                    return;
                }
                if (String.IsNullOrEmpty(ImagePathToChange))
                {
                    MessageBox.Show("Please select an image");
                    return;
                }
                 if (DataProvider.Ins.DB.Songs.Where(o => o.SongTitle == TitleToChange).Count()>0)
                {
                    MessageBox.Show("This title already exists, please try another title");
                    return;
                }
                 else
                {
                    MessageBox.Show("Succesfully changed");
                    wd.Close();
                }
            });
            ChangeInfoSong = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 var item = p as Song;
                 EditSongInApp editWD = new EditSongInApp();
                 editWD.ShowDialog();
                 if (TitleToChange != null&& ArtistToChange != null && ImagePathToChange!= null)
                 {
                     item.SongTitle = TitleToChange;
                     item.Artist = ArtistToChange;
                     item.ImagePath = ImagePathToChange;
                     DataProvider.Ins.DB.SaveChanges();
                     Load();
                     TitleToChange = null;
                     ArtistToChange = null;
                     ImagePathToChange = null;
                 }
             });

            Refresh = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                MySongWindow.ListSong.Items.Refresh();
                LoadCommon();
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
                var filePath = Path.Combine(projectPath, "Image","logomusicapp.png");
                string titleNewSong = "Unknown";
                string artistNewSong = "Unknown";
                string uriIamge = filePath;
                var MySongWindow = p as AddSongToApp;
                if (String.IsNullOrEmpty(FilePathToAdd))
                {
                    MessageBox.Show("Please add song file");
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
                if (ImagePathToAdd!=null)
                {
                    uriIamge = ImagePathToAdd;
                }
                try
                {
                    string stradd = FilePathToAdd;
                    Uri uriadd = new Uri(stradd);
                    MediaPlayer med = new MediaPlayer();
                    med.Open(uriadd);
                    string timetoadd ="";
                    // this is a trick to waiting hastimespan change to true
                    MessageBox.Show("Processing");
                    //
                    if (med.HasAudio == false)
                    {
                        MessageBox.Show("Audio file invalid");
                        return;
                    }
                    if (med.NaturalDuration.HasTimeSpan)
                    {
                        timetoadd = String.Format("{0}", med.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                    }
                    DataProvider.Ins.DB.Songs.Add(new Song() { Artist = artistNewSong, 
                                                               SongTitle = titleNewSong, 
                                                               FilePath = FilePathToAdd,
                                                               ImagePath = uriIamge,
                                                               Times = 0,
                                                               TimeAdd = DateTime.Now,
                                                               HowLong = timetoadd,
                                                               IsFavourite = false});
                    DataProvider.Ins.DB.SaveChanges();
                    LoadCommon();
                    MessageBox.Show("Add successfully");
                    MySongWindow.Close();
                    TitleToAdd = null;
                    ArtistToAdd = null;
                    ImagePathToAdd = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("Add process failed due to some invalid infomation");
                }

            });
            Cancel = new RelayCommand<Window>((p) => { return true; }, (p) => {
                var MySongWindow = p as AddSongToApp;
                MySongWindow.Close();
                TitleToAdd = null;
                ArtistToAdd = null;
                ImagePathToAdd = null;
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
               if (MySongWindow.sliProgress.IsFocused == true)
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Position = TimeSpan.FromSeconds(MySongWindow.sliProgress.Value);
                    mediaPlayer.Play();
                    MySongWindow.Focus();
                }
               if (MySongWindow.sliProgress.Value == MySongWindow.sliProgress.Maximum)
                
                {
                    MySongWindow.sliProgress.Value = 0;
                     if (MySongWindow.RandomLoop.IsChecked == true)
                    {
                        Random random = new Random();
                        int nextIndex = -1;
                        while (nextIndex < 0 || nextIndex == MySongWindow.ListSong.SelectedIndex)
                        {
                            nextIndex = random.Next(0, MySongWindow.ListSong.Items.Count + 1);
                        }
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
                        MySongWindow.ListSong.SelectedIndex = nextIndex;
                    }
                    else
                    {
                        int nextIndex = (MySongWindow.ListSong.SelectedIndex + 1) % (MySongWindow.ListSong.Items.Count);
                        MySongWindow.ListSong.SelectedIndex = nextIndex;
                    }

                }
            });
            ChangeVolumn = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                mediaPlayer.Volume = MySongWindow.Volume.Value;
                if (MySongWindow.Volume.Value >= 0.8)
                {
                    MySongWindow.SpeakerHigh.Visibility = Visibility.Visible;
                    MySongWindow.SpeakerLow.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerMedium.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else if (MySongWindow.Volume.Value >= 0.4)
                {
                    MySongWindow.SpeakerHigh.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerLow.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerMedium.Visibility = Visibility.Visible;
                    MySongWindow.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else if (MySongWindow.Volume.Value > 0)
                {
                    MySongWindow.SpeakerHigh.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerLow.Visibility = Visibility.Visible;
                    MySongWindow.SpeakerMedium.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerOff.Visibility = Visibility.Hidden;
                }
                else
                {
                    MySongWindow.SpeakerHigh.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerLow.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerMedium.Visibility = Visibility.Hidden;
                    MySongWindow.SpeakerOff.Visibility = Visibility.Visible;
                }
            });
            SkipNext = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                int nextIndex = (MySongWindow.ListSong.SelectedIndex + 1) % (MySongWindow.ListSong.Items.Count);
                MySongWindow.ListSong.SelectedIndex = nextIndex;
            });
            SkipPrevious = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                int nextIndex = (MySongWindow.ListSong.SelectedIndex - 1) % (MySongWindow.ListSong.Items.Count);
                if (nextIndex < 0) nextIndex = MySongWindow.ListSong.Items.Count - 1;
                MySongWindow.ListSong.SelectedIndex = nextIndex;
            });
            ShuffleVariant = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MySongWindow.SequencecyLoop.IsChecked = false;
                MySongWindow.OneLoop.IsChecked = false;
            });
            ShuffleDisabled = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MySongWindow.RandomLoop.IsChecked = false;
                MySongWindow.OneLoop.IsChecked = false;
            });
            Loop = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MySongWindow.SequencecyLoop.IsChecked = false;
                MySongWindow.RandomLoop.IsChecked = false;
            });
            NonMute = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MySongWindow.Volume.Value = VolumePrevious;
            });
            Mute = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                VolumePrevious = MySongWindow.Volume.Value;
                MySongWindow.Volume.Value = 0;
            });

            // sleep timer
            Sleeper = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 Window sleepwd = p as Window;
                 CountTimer = 0;
                 SleepTimerView wd = p as SleepTimerView;
                 Slider slider = wd.knob as Slider;
                 double convertsleep = slider.Value*60;
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
                 SleepTimerView sleeptimerView = new SleepTimerView();
                 sleeptimerView.ShowDialog();
             });

            // recent song
            RecentSong = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 IsPickMySong = false;
                 IsPickRecent = true;
                 List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.OrderByDescending(x=>x.TimeAdd).ToList());
                 LoadRecent();
             });
            AllSong = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickMySong = true;
                IsPickRecent = false;
                List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs);
                Load();
            });
            OverOption = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Expander expander = p as Expander;
                expander.IsExpanded = true;
            });
            LeaveOption = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Expander expander = p as Expander;
                expander.IsExpanded = false;
            });

            //
            ShowTopOrCD = new RelayCommand<object>((p) => 
            { 
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                CanChangeTOP_CD = !CanChangeTOP_CD;
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

            PlayShortcut = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 MessageBox.Show("SS");
             });



        }
        public void Load()
        {
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs);
            TopTrending = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.OrderByDescending(x => x.Times).ToList());
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
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.OrderByDescending(x => x.TimeAdd).ToList());
            TopTrending = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.OrderByDescending(x => x.Times).ToList());
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
        }


    }
}
