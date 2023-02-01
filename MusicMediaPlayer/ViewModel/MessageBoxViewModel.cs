using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MusicMediaPlayer.ViewModel
{
    public class MessageBoxViewModel : BaseViewModel
    {
        #region commands
        public ICommand Loaded { get; set; }

        #endregion

        DispatcherTimer timer;
        
        int second;

        
        public MessageBoxViewModel() 
        {
            Loaded = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                second = 3;
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += Timer_Tick;
                timer.Start();

                void Timer_Tick(object sender, EventArgs e)
                {
                    second--;
                    if (second == 0)
                    {
                        p.Close();
                    }
                }
            }
            );
        }
    }
}
