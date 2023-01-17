using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicMediaPlayer.View
{
    /// <summary>
    /// Interaction logic for SleepTimerForArtist.xaml
    /// </summary>
    public partial class SleepTimerForArtist : Window
    {
        public SleepTimerForArtist()
        {
            InitializeComponent();
        }
        private bool _isPressed = false;
        private Canvas _templateCanvas = null;

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Enable moving mouse to change the value.
            _isPressed = true;
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Disable moving mouse to change the value.
            _isPressed = false;
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isPressed)
            {
                //Find the parent canvas.
                if (_templateCanvas == null)
                {
                    _templateCanvas = MyHelper.FindParent<Canvas>(e.Source as Ellipse);
                    if (_templateCanvas == null) return;
                }
                //Canculate the current rotation angle and set the value.
                const double RADIUS = 150;
                Point newPos = e.GetPosition(_templateCanvas);
                double angle = MyHelper.GetAngleR(newPos, RADIUS);
                knob.Value = (knob.Maximum - knob.Minimum) * angle / (2 * Math.PI);
            }
        }

    }
}
