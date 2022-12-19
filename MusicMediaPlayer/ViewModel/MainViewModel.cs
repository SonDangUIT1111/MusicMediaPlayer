﻿using MusicMediaPlayer.Commands;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        //public bool IsLoaded = false;
        //public ICommand LoadedTurnOnLogin { get; set; }
        private BaseViewModel _selectedViewmodel = new UCHomeViewModel();
        public BaseViewModel SelectedViewmodel
        {
            get { return _selectedViewmodel; }
            set {
                _selectedViewmodel = value;
                OnPropertyChanged(nameof(SelectedViewmodel));
            }
        }
        public ICommand UpdateViewCommand { get; set; } 


        public MainViewModel()
        {

            //LoadedTurnOnLogin = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    IsLoaded = true;
            //    if (p == null)
            //        return;
            //    p.Hide();
            //    Login login = new Login();
            //    login.ShowDialog();

            //    if (login.DataContext == null)
            //        return;
            //    var LoginVM = login.DataContext as LoginViewModel;
            //    if (LoginVM.IsLoggedIn == true)
            //    {
            //        p.Show();
            //    }
            //    else
            //    {
            //        p.Close();
            //    }
            //}
            //);
            ////
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
