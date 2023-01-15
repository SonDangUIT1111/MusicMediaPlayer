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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicMediaPlayer.ViewModel
{
    public class Discover_ArtistViewModel:BaseViewModel
    {
        private ObservableCollection<Artist> _List;
        public ObservableCollection<Artist> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private bool _IsPickAllArtist = true;
        public bool IsPickAllArtist { get => _IsPickAllArtist; set => _IsPickAllArtist = value; }
        private bool _IsPickPopularArtist = false;
        public bool IsPickPopularArtist { get => _IsPickPopularArtist; set => _IsPickPopularArtist = value; }
        private string _ImagePathToChange;
        public string ImagePathToChange { get => _ImagePathToChange; set => _ImagePathToChange = value; }
        private Artist _ArtistChanging;
        public Artist ArtistChanging { get => _ArtistChanging; set => _ArtistChanging = value; }
        //
        public Discover_Artist ArtistWindow { get; set; }
        //
        public CurrentUserAccountModel CurrentUser { get; set; }


        //define command
        public ICommand LoadData { get; set; }
        public ICommand FilterChangeValue { get; set; }
        public ICommand AllArtist { get; set; }
        public ICommand PopularArtist { get; set; }
        public ICommand ChangePopular { get; set; }
        public ICommand ChangeImageArtist { get; set; }
        public ICommand ChangeImage { get; set; }
        public ICommand Changing { get; set; }
        public ICommand CancelChanging { get; set; }
        public ICommand Explore { get; set; }

        //

        public Discover_ArtistViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            List = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == 12));

            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                ArtistWindow = p as Discover_Artist;
                LoadCommon();
                if (List.Count == 0)
                {
                    ArtistWindow.IsThereSong.Visibility = Visibility.Visible;
                }
                else
                {
                    ArtistWindow.IsThereSong.Visibility = Visibility.Hidden;
                }
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ArtistWindow.ListArtist.ItemsSource);
                view.Filter = FiltersSong;

                bool FiltersSong(object item)
                {
                    if (String.IsNullOrEmpty(ArtistWindow.SongFilter.Text))
                        return true;
                    else
                        return ((item as Artist).ArtistName.IndexOf(ArtistWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            });
            FilterChangeValue = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionViewSource.GetDefaultView(ArtistWindow.ListArtist.ItemsSource).Refresh();
            });
            AllArtist = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickAllArtist = true;
                IsPickPopularArtist = false;
                LoadAll();
            });
            PopularArtist = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickAllArtist = false;
                IsPickPopularArtist = true;
                LoadPopular();
            });
            ChangePopular = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataProvider.Ins.DB.SaveChanges();
            });
            ChangeImageArtist = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ArtistChanging = p as Artist;
                ChangeArtistPictureWindow changewindow = new ChangeArtistPictureWindow();
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                var imagestream = new MemoryStream(ArtistChanging.ImageArtistBinary);
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
                var wd = p as ChangeArtistPictureWindow;
                if (ImagePathToChange != null)
                {
                    Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                    byte[] BinaryImage = converter.ImageToBinary(ImagePathToChange);
                    ArtistChanging.ImageArtistBinary = BinaryImage;
                }
                DataProvider.Ins.DB.SaveChanges();
                LoadCommon();
                ImagePathToChange = null;
                MessageBox.Show("Succesfully changed");
                wd.Close();
            });
            CancelChanging = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var wd = p as ChangeArtistPictureWindow;
                wd.Close();
                ImagePathToChange = null;
            });
            Explore = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Artist item = p as Artist;
                Discover_ArtistSong window = new Discover_ArtistSong();
                var windowData = window.DataContext as Discover_ArtistSongViewModel;
                windowData.CurrentArtist = item;

                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                MemoryStream stream = new MemoryStream(item.ImageArtistBinary);
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                imageBrush.Stretch = Stretch.UniformToFill;
                window.ArtistFrame.Background = imageBrush;
                window.Name.Text = item.ArtistName;
                window.Stream.Text = item.Streams.ToString();
                ArtistWindow.NavigationService.Navigate(window);
            });
        }
        public void LoadAll()
        {
            List = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == 12).ToList());
            if (List.Count == 0)
            {
                ArtistWindow.IsThereSong.Visibility = Visibility.Visible;
            }
            else
            {
                ArtistWindow.IsThereSong.Visibility = Visibility.Hidden;
            }
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ArtistWindow.ListArtist.ItemsSource);
            view.Filter = FiltersSong;
            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(ArtistWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Artist).ArtistName.IndexOf(ArtistWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        public void LoadPopular()
        {
            List = new ObservableCollection<Artist>(DataProvider.Ins.DB.Artists.Where(x => x.UserId == 12 && x.IsPopular == true).ToList());
            if (List.Count == 0)
            {
                ArtistWindow.IsThereSong.Visibility = Visibility.Visible;
            }
            else
            {
                ArtistWindow.IsThereSong.Visibility = Visibility.Hidden;
            }
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ArtistWindow.ListArtist.ItemsSource);
            view.Filter = FiltersSong;
            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(ArtistWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Artist).ArtistName.IndexOf(ArtistWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        public void LoadCommon()
        {
            if (IsPickAllArtist)
            {
                LoadAll();
            }
            else if (IsPickPopularArtist)
            {
                LoadPopular();
            }
        }
    }
}
