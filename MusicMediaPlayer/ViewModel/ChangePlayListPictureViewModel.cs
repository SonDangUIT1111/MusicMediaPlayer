using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using MusicMediaPlayer.View;
using System.IO;
using MusicMediaPlayer.Model;
using MaterialDesignThemes.Wpf;

namespace MusicMediaPlayer.ViewModel
{
    public class ChangePlayListPictureViewModel : BaseViewModel
    {
        #region commands
        public ICommand AddImage { get; set; }
        public ICommand Close { get; set; }
        public ICommand Change { get; set; }
        #endregion

        private string _ImagePathToAdd;
        public string ImagePathToAdd { get { return _ImagePathToAdd; } set { _ImagePathToAdd = value; OnPropertyChanged(); } }

        private byte[] imageBinaryAdd;
        public byte[] ImageBinaryAdd { get { return imageBinaryAdd; } set { imageBinaryAdd = value; OnPropertyChanged(); } }

        public MusicMediaPlayer.Model.PlayList pl;

        public ChangePlayListPictureViewModel()
        {
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
                        p.Children.Clear();
                    }
                }
            });

            Close = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            Change = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (ImagePathToAdd != null)
                {
                    string uriImage;
                    uriImage = ImagePathToAdd;
                    Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                    ImageBinaryAdd = converter.ImageToBinary(uriImage);
                    pl.ImagePlaylistBinary = ImageBinaryAdd;
                    DataProvider.Ins.DB.SaveChanges();
                    ImagePathToAdd = null;
                    ImageBinaryAdd = null;
                    p.Close();
                }
                else
                {
                    MessageBoxOK wd = new MessageBoxOK();
                    var data = wd.DataContext as MessageBoxOKViewModel;
                    data.Content = "Picture wasn't seleted";
                    wd.ShowDialog();
                }
            });
        }
    }
}
