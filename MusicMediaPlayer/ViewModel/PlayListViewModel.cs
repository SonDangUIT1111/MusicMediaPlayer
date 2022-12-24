using MaterialDesignThemes.Wpf;
using MusicMediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;

namespace MusicMediaPlayer.ViewModel
{
    public class PlayListViewModel : BaseViewModel
    {
        #region commands
        public ICommand AddPL { get; set; }

        public ICommand Rename { get; set; }

        public ICommand Delete { get; set; }

        public ICommand TextChanged { get; set; }

        public ICommand Load { get; set; }

        public ICommand MouseDoubleClick { get; set; }

        public ICommand AZ { get; set; }

        public ICommand ZA { get; set; }

        #endregion

        private ObservableCollection<MusicMediaPlayer.Model.PlayList> _List;
        public ObservableCollection<MusicMediaPlayer.Model.PlayList> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private string _Search;
        public string Search { get => _Search; set { _Search = value; OnPropertyChanged(); } }

        private MusicMediaPlayer.Model.PlayList _ItemDoubleClick;
        public MusicMediaPlayer.Model.PlayList ItemDoubleClick { get => _ItemDoubleClick; set { _ItemDoubleClick = value; OnPropertyChanged(); } }

        public PlayList page;

        public bool IsSort = false;

        public PlayListViewModel()
        {
            LoadDanhSach();

            bool PlaylistFilter(object item)
            {
                if (String.IsNullOrEmpty(Search))
                    return true;
                else
                    return ((item as MusicMediaPlayer.Model.PlayList).PlayListName.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            Load = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                page = p as PlayList;
            }
            );

            AddPL = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddPlayList wd = new AddPlayList();
                wd.ShowDialog();

                LoadDanhSach();
            }
            );

            Rename = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                RenamePlayList wd = new RenamePlayList();
                wd.ShowDialog();

                var RenameWD = wd.DataContext as RenamePlayListViewModel;

                if (RenameWD.IsLuu)
                {
                    var pl = p as MusicMediaPlayer.Model.PlayList;

                    pl.PlayListName = RenameWD.Title;

                    DataProvider.Ins.DB.SaveChanges();

                    LoadDanhSach();

                    RenameWD.IsLuu = false;
                    wd.NamePL.Text = null;
                }
            }
            );

            Delete = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBoxResult dr = System.Windows.MessageBox.Show("Do you want to delete it?", "Delete!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dr == MessageBoxResult.Yes)
                {
                    var pl = p as MusicMediaPlayer.Model.PlayList;

                    var song_in_pl = pl.Song;

                    foreach (Song item in song_in_pl.ToList())
                    {
                        item.PlayList.Remove(pl);

                        pl.Song.Remove(item);
                    }

                    DataProvider.Ins.DB.PlayList.Remove(pl);
                    DataProvider.Ins.DB.SaveChanges();

                    LoadDanhSach();
                }

            }
           );


            TextChanged = new RelayCommand<System.Windows.Controls.TextBox>((p) => { return true; }, (p) =>
            {
                Search = p.Text;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(page.listview.ItemsSource);
                view.Filter = PlaylistFilter;
            }
            );

            MouseDoubleClick = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                PlayList_Inside wd = new PlayList_Inside();

                var trang = wd.DataContext as PlayList_InsideViewModel;

                trang.page_PlayList = page;

                page.NavigationService.Navigate(wd);
            }
            );

            AZ = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(page.listview.ItemsSource);

                if (IsSort)
                {
                    view.SortDescriptions.Clear();
                }

                view.SortDescriptions.Add(new SortDescription("PlayListName", ListSortDirection.Ascending));
                IsSort = true;
            }
            );

            ZA = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(page.listview.ItemsSource);

                if (IsSort)
                {
                    view.SortDescriptions.Clear();
                }

                view.SortDescriptions.Add(new SortDescription("PlayListName", ListSortDirection.Descending));
                IsSort = true;
            }
            );


        }
        void LoadDanhSach()
        {
            List = new ObservableCollection<MusicMediaPlayer.Model.PlayList>(DataProvider.Ins.DB.PlayList);
        }
    }
}
