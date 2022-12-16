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

namespace MusicMediaPlayer.ViewModel
{
    public class PlayList_InsideViewModel : BaseViewModel
    {
        #region commands
        public ICommand Loaded { get; set; }

        public ICommand Rename { get; set; }

        public ICommand DeletePlayList { get; set; }

        #endregion

        private string _PLName;
        public string PLName { get => _PLName; set { _PLName = value; OnPropertyChanged(); } }

        public PlayList page_PlayList;

        MusicMediaPlayer.Model.PlayList pl;

        PlayList_Inside thispage;

        public bool IsGoBack = false;

        public PlayList_InsideViewModel()
        {
            Loaded = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                thispage = p as PlayList_Inside;
                pl = page_PlayList.listview.SelectedItem as MusicMediaPlayer.Model.PlayList;
                PLName = pl.PlayListName;
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

                    DataProvider.Ints.DB.SaveChanges();

                    PLName = rename.Title;

                    wd.NamePL.Text = null;
                    rename.IsLuu = false;
                }    
            }
            );

            DeletePlayList = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                MessageBoxResult dr = System.Windows.MessageBox.Show("Do you want to delete it?", "Delete!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dr == MessageBoxResult.Yes)
                {
                    var song_in_pl = pl.Song;

                    foreach (Song item in song_in_pl.ToList())
                    {
                        item.PlayList.Remove(pl);

                        pl.Song.Remove(item);
                    }

                    DataProvider.Ints.DB.PlayList.Remove(pl);
                    DataProvider.Ints.DB.SaveChanges();

                    var trang = page_PlayList.DataContext as PlayListViewModel;

                    trang.List = new ObservableCollection<MusicMediaPlayer.Model.PlayList>(DataProvider.Ints.DB.PlayList);
                
                    p.NavigationService.GoBack();
                }
            }
            );

        }
    }
}
