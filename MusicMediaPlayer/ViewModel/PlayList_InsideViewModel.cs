using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicMediaPlayer.ViewModel
{
    public class PlayList_InsideViewModel : BaseViewModel
    {
        #region commands
        public ICommand Loaded { get; set; }

        public ICommand Rename { get; set; }

        public ICommand DeletePlayList { get; set; }

        public ICommand Delete_One_Song { get; set; }

        public ICommand AddSongs { get; set; }

        #endregion

        private string _PLName;
        public string PLName { get => _PLName; set { _PLName = value; OnPropertyChanged(); } }

        private string _SongCount;
        public string SongCount { get => _SongCount; set { _SongCount = value; OnPropertyChanged(); } }

        public View.PlayList page_PlayList;

        MusicMediaPlayer.Model.PlayList pl;

        PlayList_Inside thispage;

        public bool IsGoBack = false;

        private ObservableCollection<MusicMediaPlayer.Model.Song> _List;
        public ObservableCollection<MusicMediaPlayer.Model.Song> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public PlayList_InsideViewModel()
        {
            Loaded = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
                thispage = p as PlayList_Inside;
                pl = page_PlayList.listview.SelectedItem as MusicMediaPlayer.Model.PlayList;
                PLName = pl.PlayListName;
                SongCount = pl.SongCount.ToString() + " Bài hát";
                LoadDanhSach();
            }
            );

            Rename = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                RenamePlayList wd = new RenamePlayList();

                wd.ShowDialog();

                var rename = wd.DataContext as RenamePlayListViewModel;

                if (rename.IsLuu)
                {
                    pl.PlayListName = rename.Title;

                    DataProvider.Ins.DB.SaveChanges();

                    PLName = rename.Title;

                    wd.NamePL.Text = null;
                    rename.IsLuu = false;
                }
            }
            );

            DeletePlayList = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
                MessageBoxResult dr = System.Windows.MessageBox.Show("Do you want to delete it?", "Delete!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dr == MessageBoxResult.Yes)
                {
                    var song_in_pl = pl.Songs1;

                    foreach (Song item in song_in_pl.ToList())
                    {
                        item.PlayLists.Remove(pl);

                        pl.Songs1.Remove(item);
                    }

                    DataProvider.Ins.DB.PlayLists.Remove(pl);
                    DataProvider.Ins.DB.SaveChanges();

                    var trang = page_PlayList.DataContext as PlayListViewModel;

                    trang.List = new ObservableCollection<MusicMediaPlayer.Model.PlayList>(DataProvider.Ins.DB.PlayLists);

                    p.NavigationService.GoBack();
                }
            }
            );

            Delete_One_Song = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                MessageBoxResult dr = System.Windows.MessageBox.Show("Do you want to delete it?", "Delete!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dr == MessageBoxResult.Yes)
                {
                    pl.Songs1.Remove(p as Song);
                    pl.SongCount = pl.SongCount - 1;
                    DataProvider.Ins.DB.SaveChanges();
                    SongCount = pl.SongCount.ToString() + " Bài hát";
                    LoadDanhSach();
                }

            }
            );

            AddSongs = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddSongPlayList wd = new AddSongPlayList();

                var trang = wd.DataContext as AddSongPlayListViewModel;

                trang.pl = pl;

                wd.ShowDialog();

                SongCount = pl.SongCount.ToString() + " Bài hát";
                LoadDanhSach();
            }
            );

        }

        void LoadDanhSach()
        {
            List = new ObservableCollection<Song>(pl.Songs1);
        }
    }
}
