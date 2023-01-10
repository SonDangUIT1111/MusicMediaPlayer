using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMediaPlayer.ViewModel
{
    public class HomeViewModel:BaseViewModel
    {
        public CurrentUserAccountModel CurrentUser { get; set; }
        public HomeViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
        }
    }
}
