using Microsoft.Win32;
using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace MusicMediaPlayer.ViewModel
{
    public class Discover_GenreViewModel : BaseViewModel
    {
        public MediaPlayer mediaPlayer = new MediaPlayer();
        public MediaPlayer mediaPlayer2 = new MediaPlayer();
        public MediaPlayer mediaPlayer3 = new MediaPlayer();
        public MediaPlayer mediaPlayer4 = new MediaPlayer();
        private ObservableCollection<Genre> _List;
        public ObservableCollection<Genre> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private bool _mediaPlayerIsPlaying = false;
        public bool MediaPlayerIsPlaying { get => _mediaPlayerIsPlaying; set => _mediaPlayerIsPlaying = value; }
        private bool _mediaPlayerIsPlaying2 = false;
        public bool MediaPlayerIsPlaying2 { get => _mediaPlayerIsPlaying2; set => _mediaPlayerIsPlaying2 = value; }
        private bool _mediaPlayerIsPlaying3 = false;
        public bool MediaPlayerIsPlaying3 { get => _mediaPlayerIsPlaying3; set => _mediaPlayerIsPlaying3 = value; }
        private bool _mediaPlayerIsPlaying4 = false;
        public bool MediaPlayerIsPlaying4 { get => _mediaPlayerIsPlaying4; set => _mediaPlayerIsPlaying4 = value; }
        private bool _IsPickAllGenres = true;
        public bool IsPickAllGenres { get => _IsPickAllGenres; set { _IsPickAllGenres = value; OnPropertyChanged(); } }
        private bool _IsPickPopularGenre = false;
        public bool IsPickPopularGenre { get => _IsPickPopularGenre; set => _IsPickPopularGenre = value; }
        private string _ImagePathToChange;
        public string ImagePathToChange { get => _ImagePathToChange; set => _ImagePathToChange = value; }
        private Genre _GenreChanging;
        public Genre GenreChanging { get => _GenreChanging; set => _GenreChanging = value; }

        public Discover_Genre GenreWindow { get; set; }

        public CurrentUserAccountModel CurrentUser { get; set; }
        public ICommand LoadData { get; set; }
        public ICommand FilterChangeValue { get; set; }
        public ICommand AllGenres { get; set; }
        public ICommand ChangePopular { get; set; }
        public ICommand ChangeImageGenre { get; set; }
        public ICommand ChangeImage { get; set; }
        public ICommand Changing { get; set; }
        public ICommand CancelChanging { get; set; }
        public ICommand Explore { get; set; }

        public Button SkipPreviousbtn { get; set; }
        public Button SkipNextbtn { get; set; }
        public ToggleButton Playbtn { get; set; }
        public ToggleButton Pausebtn { get; set; }
        public ToggleButton PlayInvisible { get; set; }
        public ToggleButton PauseInvisible { get; set; }
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
        public Grid PlayerBarArtist { get; set; }
        public Grid PlayerBarAlbum { get; set; }
        public Grid PlayerBarGenre { get; set; }

        public Discover_GenreViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            List = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.UserId == CurrentUser.Id));


            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                GenreWindow = p as Discover_Genre;
                LoadAll();
                if (List.Count == 0)
                {
                    GenreWindow.IsThereSong.Visibility = Visibility.Visible;
                }
                else
                {
                    GenreWindow.IsThereSong.Visibility = Visibility.Hidden;
                }
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GenreWindow.ListGenre.ItemsSource);
                view.Filter = FiltersSong;

                bool FiltersSong(object item)
                {
                    if (String.IsNullOrEmpty(GenreWindow.SongFilter.Text))
                        return true;
                    else
                        return ((item as Genre).GenreName.IndexOf(GenreWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            });
            FilterChangeValue = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionViewSource.GetDefaultView(GenreWindow.ListGenre.ItemsSource).Refresh();
            });
            AllGenres = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickAllGenres = true;
                IsPickPopularGenre = false;
                LoadAll();
            });

            ChangePopular = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataProvider.Ins.DB.SaveChanges();
            });
            ChangeImageGenre = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GenreChanging = p as Genre;
                ChangeGenrePictureWindow changewindow = new ChangeGenrePictureWindow();
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                var imagestream = new MemoryStream(GenreChanging.ImageGenreBinary);
                bitmap.StreamSource = imagestream;
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                changewindow.ChangegrdSelectImg.Background = imageBrush;
                changewindow.ShowDialog();
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
            Changing = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var wd = p as ChangeGenrePictureWindow;
                if (ImagePathToChange != null)
                {
                    Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                    byte[] BinaryImage = converter.ImageToBinary(ImagePathToChange);
                    GenreChanging.ImageGenreBinary = BinaryImage;
                }
                DataProvider.Ins.DB.SaveChanges();
                LoadAll();
                ImagePathToChange = null;
                MessageBox.Show("Succesfully changed");
                wd.Close();
            });
            CancelChanging = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var wd = p as ChangeGenrePictureWindow;
                wd.Close();
                ImagePathToChange = null;
            });
            Explore = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Genre item = p as Genre;
                Discover_GenreSong window = new Discover_GenreSong();
                var windowData = window.DataContext as Discover_GenreSongViewModel;
                windowData.CurrentUser = CurrentUser;
                windowData.CurrentGenre = item;

                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                MemoryStream stream = new MemoryStream(item.ImageGenreBinary);
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                imageBrush.Stretch = Stretch.UniformToFill;
                window.GenreFrame.Background = imageBrush;
                window.NameGenre.Text = item.GenreName;            
                GenreWindow.NavigationService.Navigate(window);
            });
        }
        public void LoadAll()
        {
            List = new ObservableCollection<Genre>(DataProvider.Ins.DB.Genres.Where(x => x.UserId == CurrentUser.Id).ToList());
            if (List.Count == 0)
            {
                GenreWindow.IsThereSong.Visibility = Visibility.Visible;
            }
            else
            {
                GenreWindow.IsThereSong.Visibility = Visibility.Hidden;
            }
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GenreWindow.ListGenre.ItemsSource);
            view.Filter = FiltersSong;
            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(GenreWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Genre).GenreName.IndexOf(GenreWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
    }
}
