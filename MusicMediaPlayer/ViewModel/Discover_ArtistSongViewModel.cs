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
    public class Discover_ArtistSongViewModel:BaseViewModel
    {
        private ObservableCollection<Song> _ListSong;
        public ObservableCollection<Song> ListSong { get { return _ListSong; } set { _ListSong = value; OnPropertyChanged(); } }
        private ObservableCollection<Song> _ListPopular;
        public ObservableCollection<Song> ListPopular { get { return _ListPopular; } set { _ListPopular = value; OnPropertyChanged(); } }

        public CurrentUserAccountModel CurrentUser { get; set; }
        public Artist CurrentArtist { get; set; }


        //define command
        public ICommand LoadData { get; set; }
        //
        public Discover_ArtistSongViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            CurrentArtist = new Artist();
            ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x=>x.UserId == 12 && x.ArtistId == CurrentArtist.ArtistId));
            ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == 12 && x.ArtistId == CurrentArtist.ArtistId).OrderBy(x=>x.Times));
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());
            ListPopular.Add(new Song());


            LoadData = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListSong = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == 12 && x.ArtistId == CurrentArtist.ArtistId));
                ListPopular = new ObservableCollection<Song>(DataProvider.Ins.DB.Songs.Where(x => x.UserId == 12 && x.ArtistId == CurrentArtist.ArtistId).OrderBy(x => x.Times));
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
                ListPopular.Add(new Song());
            });
        }
    }
}
