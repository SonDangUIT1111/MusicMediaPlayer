using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MusicMediaPlayer.UserControlMusic
{
    /// <summary>
    /// Interaction logic for PianoAdventure.xaml
    /// </summary>
    public partial class PianoAdventure : UserControl
    {
        public string projectPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        public string filePathC;
        public string filePathC1;
        public string filePathCs;
        public string filePathCs1;
        public string filePathD;
        public string filePathD1;
        public string filePathDs;
        public string filePathDs1;
        public string filePathE;
        public string filePathE1;
        public string filePathF;
        public string filePathF1;
        public string filePathFs;
        public string filePathG;
        public string filePathGs;
        public string filePathA;
        public string filePathB;
        public string filePathBb;
        public PianoAdventure()
        {
            InitializeComponent();
            filePathC = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note","C.wav");
            filePathC1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "C1.wav");
            filePathCs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "C_s.wav");
            filePathCs1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "C_s1.wav");
            filePathD = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "D.wav");
            filePathD1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "D1.wav");
            filePathDs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "D_s.wav");
            filePathDs1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "D_s1.wav");
            filePathE = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "E.wav");
            filePathE1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "E1.wav");
            filePathF = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "F.wav");
            filePathF1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "F1.wav");
            filePathFs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "F_s.wav");
            filePathG = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "G.wav");
            filePathGs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "G_S.wav");
            filePathA = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "A.wav");
            filePathB = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "B.wav");
            filePathBb = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Bb.wav");
        }
        public void C()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathC));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void D()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathD));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void E()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathE));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void F()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathF));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void G()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathG));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void A()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathA));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void B()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathB));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void C1()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathC1));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void D1()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathD1));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void E1()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathE1));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void F1()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathF1));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void Cs()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathCs));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void Cs1()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathCs1));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void Ds()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathDs));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void Ds1()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathDs1));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void Fs()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathFs));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void Gs()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathGs));
                player.Play();
            }
            catch (Exception)
            {

            }
        }
        public void Bb()
        {
            var player = new System.Windows.Media.MediaPlayer();
            try
            {
                player.Open(new Uri(filePathBb));
                player.Play();
            }
            catch (Exception)
            {

            }
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            C();
        }

        private void D_Click(object sender, RoutedEventArgs e)
        {
            D();
        }

        private void E_Click(object sender, RoutedEventArgs e)
        {
            E();
        }

        private void F_Click(object sender, RoutedEventArgs e)
        {
            F();
        }

        private void G_Click(object sender, RoutedEventArgs e)
        {
            G();
        }

        private void A_Click(object sender, RoutedEventArgs e)
        {
            A();
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            B();
        }

        private void C1_Click(object sender, RoutedEventArgs e)
        {
            C1();
        }

        private void D1_Click(object sender, RoutedEventArgs e)
        {
            D1();
        }

        private void E1_Click(object sender, RoutedEventArgs e)
        {
            E1();
        }

        private void F1_Click(object sender, RoutedEventArgs e)
        {
            F1();
        }

        private void CSharp_Click(object sender, RoutedEventArgs e)
        {
            Cs();
        }

        private void DSharp_Click(object sender, RoutedEventArgs e)
        {
            Ds();
        }

        private void GSharp_Click(object sender, RoutedEventArgs e)
        {
            Gs();
        }

        private void FSharp_Click(object sender, RoutedEventArgs e)
        {
            Fs();
        }

        private void ASharp_Click(object sender, RoutedEventArgs e)
        {
            Bb();
        }

        private void CSharp1_Click(object sender, RoutedEventArgs e)
        {
            Cs1();
        }

        private void DSharp1_Click(object sender, RoutedEventArgs e)
        {
            Ds1();
        }
        private async void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    Ckey.Background = Brushes.Aqua;
                    C();
                    await Task.Delay(500);
                    Ckey.Background = Brushes.White;
                    break;
                case Key.S:
                    Dkey.Background = Brushes.Aqua;
                    D();
                    await Task.Delay(500);
                    Dkey.Background = Brushes.White;
                    break;
                case Key.D:
                    Ekey.Background = Brushes.Aqua;
                    E();
                    await Task.Delay(500);
                    Ekey.Background = Brushes.White;
                    break;
                case Key.F:
                    Fkey.Background = Brushes.Aqua;
                    F();
                    await Task.Delay(500);
                    Fkey.Background = Brushes.White;
                    break;
                case Key.G:
                    Gkey.Background = Brushes.Aqua;
                    G();
                    await Task.Delay(500);
                    Gkey.Background = Brushes.White;
                    break;
                case Key.H:
                    Akey.Background = Brushes.Aqua;
                    A();
                    await Task.Delay(500);
                    Akey.Background = Brushes.White;
                    break;
                case Key.J:
                    Bkey.Background = Brushes.Aqua;
                    B();
                    await Task.Delay(500);
                    Bkey.Background = Brushes.White;
                    break;
                case Key.K:
                    C1key.Background = Brushes.Aqua;
                    C1();
                    await Task.Delay(500);
                    C1key.Background = Brushes.White;
                    break;
                case Key.L:
                    D1key.Background = Brushes.Aqua;
                    D1();
                    await Task.Delay(500);
                    D1key.Background = Brushes.White;
                    break;
                case Key.OemSemicolon:
                    E1key.Background = Brushes.Aqua;
                    E1();
                    await Task.Delay(500);
                    E1key.Background = Brushes.White;
                    break;
                case Key.OemQuotes:
                    F1key.Background = Brushes.Aqua;
                    F1();
                    await Task.Delay(500);
                    F1key.Background = Brushes.White;
                    break;
                case Key.W:
                    CSharp.Background = Brushes.Aqua;
                    Cs();
                    await Task.Delay(500);
                    CSharp.Background = Brushes.Black;
                    break;
                case Key.E:
                    DSharp.Background = Brushes.Aqua;
                    Ds();
                    await Task.Delay(500);
                    DSharp.Background = Brushes.Black;
                    break;
                case Key.T:
                    FSharp.Background = Brushes.Aqua;
                    Fs();
                    await Task.Delay(500);
                    FSharp.Background = Brushes.Black;
                    break;
                case Key.Y:
                    GSharp.Background = Brushes.Aqua;
                    Gs();
                    await Task.Delay(500);
                    GSharp.Background = Brushes.Black;
                    break;
                case Key.U:
                    ASharp.Background = Brushes.Aqua;
                    Bb();
                    await Task.Delay(500);
                    ASharp.Background = Brushes.Black;
                    break;
                case Key.O:
                    CSharp1.Background = Brushes.Aqua;
                    Cs1();
                    await Task.Delay(500);
                    CSharp1.Background = Brushes.Black;
                    break;
                case Key.P:
                    DSharp1.Background = Brushes.Aqua;
                    Ds1();
                    await Task.Delay(500);
                    DSharp1.Background = Brushes.Black;
                    break;
            }
        }
    }
}
