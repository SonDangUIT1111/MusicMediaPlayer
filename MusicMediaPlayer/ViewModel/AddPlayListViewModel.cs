﻿using Microsoft.Win32;
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


namespace MusicMediaPlayer.ViewModel
{
    public class AddPlayListViewModel : BaseViewModel
    {
        #region commands
        public ICommand Add { get; set; }
        public ICommand TextChanged { get; set; }
        public ICommand SelectedItems { get; set; }
        #endregion

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
            List = new ObservableCollection<Song>(DataProvider.Ints.DB.Song);

            Add = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Title))
                    return false;
                return true;
            }, (p) =>
            {
                var pl = new MusicMediaPlayer.Model.PlayList();
                pl.PlayListName = Title;
                pl.OwnerId = 1;
                if (SelectedItemss != null)
                {
                    pl.SongCount = SelectedItemss.SelectedItems.Count;
                }
                else
                    pl.SongCount = 0;

                DataProvider.Ints.DB.PlayList.Add(pl);

                if (SelectedItemss != null && SelectedItemss.SelectedItems.Count > 0)
                {
                    foreach (Song item in SelectedItemss.SelectedItems)
                    {
                        item.PlayList.Add(pl);
                        pl.Song.Add(item);
                    }

                    SelectedItemss.SelectedItems.Clear();
                }

                DataProvider.Ints.DB.SaveChanges();

               

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
        }
    }
}