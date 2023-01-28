using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    internal class MessageBoxOK : BaseViewModel
    {
        #region commands
        public ICommand OK { get; set; }
        #endregion

        public MessageBoxOK() 
        {
            OK = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            }
            );
        }
    }
}
