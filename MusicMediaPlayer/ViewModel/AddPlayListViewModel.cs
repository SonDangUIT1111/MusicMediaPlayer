using Microsoft.Win32;
using MusicMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MusicMediaPlayer.ViewModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using MusicMediaPlayer.View;

namespace MusicMediaPlayer.ViewModel
{
    public class AddPlayListViewModel : BaseViewModel
    {
        #region commands
        public ICommand Load { get; set; }
        public ICommand Add { get; set; }
        public ICommand TextChanged { get; set; }
        public ICommand SelectedItems { get; set; }
        public ICommand Close { get; set; }
        public ICommand AddImage { get; set; }
        #endregion
        private string _ImagePathToAdd;
        public string ImagePathToAdd { get { return _ImagePathToAdd; } set { _ImagePathToAdd = value; OnPropertyChanged(); } }

        private byte[] imageBinaryAdd;
        public byte[] ImageBinaryAdd { get { return imageBinaryAdd; } set { imageBinaryAdd = value; OnPropertyChanged(); } }
        public CurrentUserAccountModel CurrentUser { get; set; }
        private ObservableCollection<Song> _List;
        public ObservableCollection<Song> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        private ListView _SelectedItems;

        public ListView SelectedItemss
        {
            get => _SelectedItems;
            set
            {
                _SelectedItems = value; OnPropertyChanged();
            }
        }

        public AddPlayListViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id));
            Load = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                List = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == CurrentUser.Id));
            });
            Add = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Title))
                    return false;
                return true;
            }, (p) =>
            {
                var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                var filePath = Path.Combine(projectPath, "Image", "music_note.jpg");
                string uriImage = filePath;
                if (ImagePathToAdd != null)
                {
                    uriImage = ImagePathToAdd;
                }
                Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                ImageBinaryAdd = converter.ImageToBinary(uriImage);
                var pl = new MusicMediaPlayer.Model.PlayList();
                pl.PlayListName = Title;
                pl.OwnerId = CurrentUser.Id;
                pl.ImagePlaylistBinary = ImageBinaryAdd;
                if (SelectedItemss != null)
                {
                    pl.SongCount = SelectedItemss.SelectedItems.Count;
                }
                else
                    pl.SongCount = 0;

                DataProvider.Ins.DB.PlayLists.Add(pl);

                if (SelectedItemss != null && SelectedItemss.SelectedItems.Count > 0)
                {
                    foreach (Song item in SelectedItemss.SelectedItems)
                    {
                        item.PlayLists.Add(pl);
                        pl.Songs.Add(item);
                    }

                    SelectedItemss.SelectedItems.Clear();
                }

                DataProvider.Ins.DB.SaveChanges();

                Title = null;
                ImagePathToAdd = null;
                ImageBinaryAdd = null;

                p.Close();
            }
            );

            TextChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Title = p.Text;
            }
            );

            SelectedItems = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                SelectedItemss = p;
            }
            );

            Close = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            }
            );

            AddImage = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Insert Image";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    ImagePathToAdd = op.FileName;
                    try
                    {
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
                            p.Children.Clear();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBoxOK ms = new MessageBoxOK();
                        var data = ms.DataContext as MessageBoxOKViewModel;
                        data.Content = "Image file is invalid";
                        ms.ShowDialog();
                    }
                }
            });
        }
    }
}