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
using System.Windows.Data;
using System.Windows.Input;

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
