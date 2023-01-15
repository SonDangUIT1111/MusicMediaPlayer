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
        public CurrentUserAccountModel CurrentUser { get; set; }
        public Artist CurrentArtist { get; set; }
        public Discover_ArtistSongViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            CurrentArtist = new Artist();
        }
    }
}
