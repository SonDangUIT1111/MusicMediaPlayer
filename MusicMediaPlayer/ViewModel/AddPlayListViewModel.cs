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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MusicMediaPlayer.View;
using System.IO;

namespace MusicMediaPlayer.ViewModel
{
    public class AddPlayListViewModel : BaseViewModel
    {
        #region commands
        public ICommand Load { get; set; }
        public ICommand Add { get; set; }
        public ICommand TextChanged { get; set; }
        public ICommand SelectedItems { get; set; }
        public ICommand AddImage { get; set; }
        #endregion
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



        //start test
        private string _ImagePathToAdd;
        public string ImagePathToAdd { get { return _ImagePathToAdd; } set { _ImagePathToAdd = value; OnPropertyChanged(); } }

        private byte[] imageBinaryAdd;
        public byte[] ImageBinaryAdd { get { return imageBinaryAdd; } set { imageBinaryAdd = value; OnPropertyChanged(); } }
        //end test

        public AddPlayListViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            List = new ObservableCollection<Song>(DataProvider.Ins.DB.Song.Where(x => x.UserId == CurrentUser.Id));
            Load = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                 List = new ObservableCollection<Song>(DataProvider.Ins.DB.Song.Where(x => x.UserId == CurrentUser.Id));
            });
            Add = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Title))
                    return false;
                return true;
            }, (p) =>
            {
                var pl = new MusicMediaPlayer.Model.PlayList();
                pl.PlayListName = Title;
                pl.OwnerId = CurrentUser.Id;
                if (SelectedItemss != null)
                {
                    pl.SongCount = SelectedItemss.SelectedItems.Count;
                }
                else
                    pl.SongCount = 0;

                // start test
                var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                var filePath = Path.Combine(projectPath, "Image", "logomusicapp.png");
                string uriIamge = filePath;

                if (ImagePathToAdd != null)
                {
                    uriIamge = ImagePathToAdd;
                }

                Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                ImageBinaryAdd = converter.ImageToBinary(uriIamge);

                pl.ImagePlaylistBinary = ImageBinaryAdd;

                // end test

                DataProvider.Ins.DB.PlayList.Add(pl);
                ImagePathToAdd = null;

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


            AddImage = new RelayCommand<Border>((p) => { return true; }, (p) =>
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
                }
            });

        }
    }
}
