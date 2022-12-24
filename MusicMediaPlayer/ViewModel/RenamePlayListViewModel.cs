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
    public class RenamePlayListViewModel : BaseViewModel
    {
        #region commands
        public ICommand Cancel { get; set; }
        public ICommand Luu { get; set; }
        public ICommand TextChanged { get; set; }
        public ICommand Load { get; set; }
        #endregion

        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        public bool IsLuu = false;

        public RenamePlayListViewModel()
        {
            Cancel = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var wd = p as RenamePlayList;

                wd.NamePL.Text = null;

                p.Close();
            }
            );

            Luu = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Title))
                    return false;
                return true;
            }, (p) =>
            {
                IsLuu = true;
                p.Close();
            }
            );

            TextChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Title = p.Text;
            }
            );
        }
    }
}
