using MusicMediaPlayer.Model;
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
using MusicMediaPlayer.View;
using System.Windows.Data;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;

namespace MusicMediaPlayer.ViewModel
{
    public class Discover_AlbumViewModel : BaseViewModel
    {
        public MediaPlayer mediaPlayer = new MediaPlayer();
        private ObservableCollection<Album> _List;
        public ObservableCollection<Album> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private bool _IsPickAllAlbums = true;
        public bool IsPickAllAlbums { get => _IsPickAllAlbums; set { _IsPickAllAlbums = value; OnPropertyChanged(); } }
        private bool _IsPickPopularAlbum = false;
        public bool IsPickPopularAlbum { get => _IsPickPopularAlbum; set => _IsPickPopularAlbum = value; }
        private string _ImagePathToChange;
        public string ImagePathToChange { get => _ImagePathToChange; set => _ImagePathToChange = value; }
        private Album _AlbumChanging;
        public Album AlbumChanging { get => _AlbumChanging; set => _AlbumChanging = value; }

        public Discover_Album AlbumWindow { get; set; }

        public CurrentUserAccountModel CurrentUser { get; set; }
        public ICommand LoadData { get; set; }
        public ICommand FilterChangeValue { get; set; }
        public ICommand AllAlbums { get; set; }
        public ICommand PopularAlbum { get; set; }
        public ICommand ChangePopular { get; set; }
        public ICommand ChangeImageAlbum { get; set; }
        public ICommand ChangeImage { get; set; }
        public ICommand Changing { get; set; }
        public ICommand CancelChanging { get; set; }
        public ICommand Explore { get; set; }

        public Button SkipPreviousbtn { get; set; }
        public Button SkipNextbtn { get; set; }
        public ToggleButton Playbtn { get; set; }
        public ToggleButton Pausebtn { get; set; }
        public Label InTime { get; set; }
        public Label TotalTime { get; set; }
        public Slider sliProgress { get; set; }
        public Grid MainViewProgram { get; set; }
        public Grid PlayerBar { get; set; }
        public Grid PlayerBarAlbum { get; set; }

        public Discover_AlbumViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            List = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id));


            LoadData = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                AlbumWindow = p as Discover_Album;
                LoadAll();
                if (List.Count == 0)
                {
                    AlbumWindow.IsThereSong.Visibility = Visibility.Visible;
                }
                else
                {
                    AlbumWindow.IsThereSong.Visibility = Visibility.Hidden;
                }
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AlbumWindow.ListAlbum.ItemsSource);
                view.Filter = FiltersSong;

                bool FiltersSong(object item)
                {
                    if (String.IsNullOrEmpty(AlbumWindow.SongFilter.Text))
                        return true;
                    else
                        return ((item as Album).AlbumName.IndexOf(AlbumWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            });
            FilterChangeValue = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionViewSource.GetDefaultView(AlbumWindow.ListAlbum.ItemsSource).Refresh();
            });
            AllAlbums = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPickAllAlbums = true;
                IsPickPopularAlbum = false;
                LoadAll();
            });
            //PopularAlbum = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    IsPickAllAlbums = false;
            //    IsPickPopularAlbum = true;
            //    LoadPopular();
            //});
            ChangePopular = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataProvider.Ins.DB.SaveChanges();
            });
            ChangeImageAlbum = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AlbumChanging = p as Album;
                ChangeAlbumPictureWindow changewindow = new ChangeAlbumPictureWindow();
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                var imagestream = new MemoryStream(AlbumChanging.ImageAlbumBinary);
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
                var wd = p as ChangeAlbumPictureWindow;
                if (ImagePathToChange != null)
                {
                    Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                    byte[] BinaryImage = converter.ImageToBinary(ImagePathToChange);
                    AlbumChanging.ImageAlbumBinary = BinaryImage;
                }
                DataProvider.Ins.DB.SaveChanges();
                LoadAll();
                ImagePathToChange = null;
                MessageBox.Show("Succesfully changed");
                wd.Close();
            });
            CancelChanging = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var wd = p as ChangeAlbumPictureWindow;
                wd.Close();
                ImagePathToChange = null;
            });
            Explore = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Album item = p as Album;
                Discover_AlbumSong window = new Discover_AlbumSong();
                var windowData = window.DataContext as Discover_AlbumSongViewModel;
                windowData.CurrentUser = CurrentUser;
                windowData.CurrentAlbum = item;

                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                MemoryStream stream = new MemoryStream(item.ImageAlbumBinary);
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                imageBrush.Stretch = Stretch.UniformToFill;
                window.AlbumFrame.Background = imageBrush;
                window.NameAlbum.Text = item.AlbumName;
                window.Composer.Text = item.Composer.ToString();
                AlbumWindow.NavigationService.Navigate(window);

                
            });
        }
        public void LoadAll()
        {
            List = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id).ToList());
            if (List.Count == 0)
            {
                AlbumWindow.IsThereSong.Visibility = Visibility.Visible;
            }
            else
            {
                AlbumWindow.IsThereSong.Visibility = Visibility.Hidden;
            }
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AlbumWindow.ListAlbum.ItemsSource);
            view.Filter = FiltersSong;
            bool FiltersSong(object item)
            {
                if (String.IsNullOrEmpty(AlbumWindow.SongFilter.Text))
                    return true;
                else
                    return ((item as Album).AlbumName.IndexOf(AlbumWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        //public void LoadPopular()
        //{
        //    List = new ObservableCollection<Album>(DataProvider.Ins.DB.Albums.Where(x => x.UserId == CurrentUser.Id ).ToList());
        //    if (List.Count == 0)
        //    {
        //        AlbumWindow.IsThereSong.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        AlbumWindow.IsThereSong.Visibility = Visibility.Hidden;
        //    }
        //    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AlbumWindow.ListAlbum.ItemsSource);
        //    view.Filter = FiltersSong;
        //    bool FiltersSong(object item)
        //    {
        //        if (String.IsNullOrEmpty(AlbumWindow.SongFilter.Text))
        //            return true;
        //        else
        //            return ((item as Artist).ArtistName.IndexOf(AlbumWindow.SongFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //    }
        //}
        //public void LoadCommon()
        //{
        //    if (IsPickAllAlbums)
        //    {
        //        LoadAll();
        //    }
        //    else if (IsPickPopularAlbum)
        //    {
        //        LoadPopular();
        //    }
        //}

    }
    }
