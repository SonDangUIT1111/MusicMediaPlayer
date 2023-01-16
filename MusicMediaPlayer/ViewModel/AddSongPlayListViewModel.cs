using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MusicMediaPlayer.ViewModel
{
    public class AddSongPlayListViewModel : BaseViewModel
    {
        #region commands
        public ICommand Loaded { get; set; }
        public ICommand Add { get; set; }
        public ICommand Cancel { get; set; }

        #endregion
        public CurrentUserAccountModel CurrentUser { get; set; }
        private ObservableCollection<MusicMediaPlayer.Model.Song> _List;
        public ObservableCollection<MusicMediaPlayer.Model.Song> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public MusicMediaPlayer.Model.PlayList pl;

        AddSongPlayList thiswindow;

        public AddSongPlayListViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();    
            Loaded = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                thiswindow = p as AddSongPlayList;
                LoadDanhSach(CurrentUser.Id);
            });

            Add = new RelayCommand<object>((p) =>
            {
                if (thiswindow != null && thiswindow.listview.SelectedItems.Count != 0)
                    return true;
                return false;
            }, (p) =>
            {
                foreach (Song item in thiswindow.listview.SelectedItems)
                {
                    item.PlayList.Add(pl);
                    pl.Song.Add(item);
                    pl.SongCount = pl.SongCount + 1;
                }

                DataProvider.Ins.DB.SaveChanges();
                thiswindow.Close();
            });

            Cancel = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                thiswindow.Close();
            });
        }

        void LoadDanhSach(int identity)
        {
            ObservableCollection<MusicMediaPlayer.Model.Song> list = new ObservableCollection<MusicMediaPlayer.Model.Song>(DataProvider.Ins.DB.Song.Where(x=>x.UserId == identity));
            foreach (Song item in pl.Song)
            {
                list.Remove(item);
            }
            List = list;
        }
    }
}
