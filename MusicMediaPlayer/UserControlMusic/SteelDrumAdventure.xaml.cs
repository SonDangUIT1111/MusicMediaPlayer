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
using MusicMediaPlayer.View;
using MusicMediaPlayer.ViewModel;

namespace MusicMediaPlayer.UserControlMusic
{
    /// <summary>
    /// Interaction logic for PianoAdventure.xaml
    /// </summary>
    public partial class SteelDrumAdventure : UserControl
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
        public static int[] LittleStar = new int[] { 1, 1, 5, 5, 6, 6, 5, 5, 4, 4, 3, 3, 2, 3, 2, 3, 1, 5, 5, 4, 4, 3, 3, 2, 2, 5, 5, 4, 4, 3, 4, 3, 4, 3, 2, 1, 1, 5, 5, 6, 6, 5, 5, 4, 4, 3, 3, 2, 3, 2, 3, 1 };
        public int[] DemoScript = LittleStar;
        public int indexofnote = 0;
        public bool CustomMode = false;
        public bool IsWriteScript = false;
        public SteelDrumAdventure()
        {
            InitializeComponent();
            filePathC = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "C_Drum.wav");
            filePathC1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "C1_Drum.wav");
            filePathCs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Cq_Drum.wav");
            filePathCs1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Cq1_Drum.wav");
            filePathD = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "D_Drum.wav");
            filePathD1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "D1_Drum.wav");
            filePathDs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Dq_Drum.wav");
            filePathDs1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Dq1_Drum.wav");
            filePathE = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "E_Drum.wav");
            filePathE1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "E1_Drum.wav");
            filePathF = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "F_Drum.wav");
            filePathF1 = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "F1_Drum.wav");
            filePathFs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Fq_Drum.wav");
            filePathG = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "G_Drum.wav");
            filePathGs = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Gq_Drum.wav");
            filePathA = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "A_Drum.wav");
            filePathB = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "B_Drum.wav");
            filePathBb = System.IO.Path.Combine(projectPath, "ResourceProject", "Instrument", "Music_Note", "Bb_Drum.wav");
        }
        public void C()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 1)
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
                Ckey.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }

        }
        public void D()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 2)
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
                Dkey.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void E()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 3)
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
                Ekey.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void F()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 4)
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
                Fkey.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void G()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 5)
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
                Gkey.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void A()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 6)
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
                Akey.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void B()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 7)
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
                Bkey.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void C1()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 8)
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
                C1key.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void D1()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 9)
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
                D1key.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void E1()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 10)
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
                E1key.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void F1()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 11)
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
                F1key.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void Cs()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 12)
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
                CSharp.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void Cs1()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 17)
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
                CSharp1.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void Ds()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 13)
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
                DSharp.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void Ds1()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 18)
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
                DSharp1.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void Fs()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 14)
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
                FSharp.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void Gs()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 15)
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
                GSharp.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }
        public void Bb()
        {
            if (Practice.IsChecked == false && CustomMode == false)
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
            else if ((Practice.IsChecked == true || CustomMode == true) && DemoScript[indexofnote] == 16)
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
                ASharp.Background = Brushes.Transparent;
                indexofnote++;
                if (indexofnote == DemoScript.Count())
                {
                    indexofnote = 0;
                    Adventure.IsChecked = true;
                    return;
                }
                LightOut(DemoScript[indexofnote]);
            }
        }

        private async void C_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                Ckey.Background = Brushes.Aqua;
                C();
                await Task.Delay(500);
                Ckey.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                C();
            }

        }

        private async void D_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                Dkey.Background = Brushes.Aqua;
                D();
                await Task.Delay(500);
                Dkey.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                D();
            }

        }

        private async void E_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                Ekey.Background = Brushes.Aqua;
                E();
                await Task.Delay(500);
                Ekey.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                E();
            }

        }

        private async void F_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                Fkey.Background = Brushes.Aqua;
                F();
                await Task.Delay(500);
                Fkey.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                F();
            }
        }

        private async void G_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                Gkey.Background = Brushes.Aqua;
                G();
                await Task.Delay(500);
                Gkey.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                G();
            }
        }

        private async void A_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                Akey.Background = Brushes.Aqua;
                A();
                await Task.Delay(500);
                Akey.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                A();
            }
        }

        private async void B_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                Bkey.Background = Brushes.Aqua;
                B();
                await Task.Delay(500);
                Bkey.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                B();
            }
        }

        private async void C1_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                C1key.Background = Brushes.Aqua;
                C1();
                await Task.Delay(500);
                C1key.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                C1();
            }
        }

        private async void D1_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                D1key.Background = Brushes.Aqua;
                D1();
                await Task.Delay(500);
                D1key.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                D1();
            }
        }

        private async void E1_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                E1key.Background = Brushes.Aqua;
                E1();
                await Task.Delay(500);
                E1key.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                E1();
            }
        }

        private async void F1_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                F1key.Background = Brushes.Aqua;
                F1();
                await Task.Delay(500);
                F1key.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                F1();
            }
        }

        private async void CSharp_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                CSharp.Background = Brushes.Aqua;
                Cs();
                await Task.Delay(500);
                CSharp.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                Cs();
            }
        }

        private async void DSharp_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                DSharp.Background = Brushes.Aqua;
                Ds();
                await Task.Delay(500);
                DSharp.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                Ds();
            }
        }

        private async void GSharp_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                GSharp.Background = Brushes.Aqua;
                Gs();
                await Task.Delay(500);
                GSharp.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                Gs();
            }
        }

        private async void FSharp_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                FSharp.Background = Brushes.Aqua;
                Fs();
                await Task.Delay(500);
                FSharp.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                Fs();
            }
        }

        private async void ASharp_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                ASharp.Background = Brushes.Aqua;
                Bb();
                await Task.Delay(500);
                ASharp.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                Bb();
            }
        }

        private async void CSharp1_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                CSharp1.Background = Brushes.Aqua;
                Cs1();
                await Task.Delay(500);
                CSharp1.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                Cs1();
            }
        }

        private async void DSharp1_Click(object sender, RoutedEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                DSharp1.Background = Brushes.Aqua;
                Ds1();
                await Task.Delay(500);
                DSharp1.Background = Brushes.Transparent;
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && (IsWriteScript == false))
            {
                Ds1();
            }
        }
        private async void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Practice.IsChecked == false && IsWriteScript == false && CustomMode == false)
            {
                switch (e.Key)
                {
                    case Key.A:
                        Ckey.Background = Brushes.Aqua;
                        C();
                        await Task.Delay(500);
                        Ckey.Background = Brushes.Transparent;
                        break;
                    case Key.S:
                        Dkey.Background = Brushes.Aqua;
                        D();
                        await Task.Delay(500);
                        Dkey.Background = Brushes.Transparent;
                        break;
                    case Key.D:
                        Ekey.Background = Brushes.Aqua;
                        E();
                        await Task.Delay(500);
                        Ekey.Background = Brushes.Transparent;
                        break;
                    case Key.F:
                        Fkey.Background = Brushes.Aqua;
                        F();
                        await Task.Delay(500);
                        Fkey.Background = Brushes.Transparent;
                        break;
                    case Key.G:
                        Gkey.Background = Brushes.Aqua;
                        G();
                        await Task.Delay(500);
                        Gkey.Background = Brushes.Transparent;
                        break;
                    case Key.H:
                        Akey.Background = Brushes.Aqua;
                        A();
                        await Task.Delay(500);
                        Akey.Background = Brushes.Transparent;
                        break;
                    case Key.J:
                        Bkey.Background = Brushes.Aqua;
                        B();
                        await Task.Delay(500);
                        Bkey.Background = Brushes.Transparent;
                        break;
                    case Key.K:
                        C1key.Background = Brushes.Aqua;
                        C1();
                        await Task.Delay(500);
                        C1key.Background = Brushes.Transparent;
                        break;
                    case Key.L:
                        D1key.Background = Brushes.Aqua;
                        D1();
                        await Task.Delay(500);
                        D1key.Background = Brushes.Transparent;
                        break;
                    case Key.OemSemicolon:
                        E1key.Background = Brushes.Aqua;
                        E1();
                        await Task.Delay(500);
                        E1key.Background = Brushes.Transparent;
                        break;
                    case Key.OemQuotes:
                        F1key.Background = Brushes.Aqua;
                        F1();
                        await Task.Delay(500);
                        F1key.Background = Brushes.Transparent;
                        break;
                    case Key.W:
                        CSharp.Background = Brushes.Aqua;
                        Cs();
                        await Task.Delay(500);
                        CSharp.Background = Brushes.Transparent;
                        break;
                    case Key.E:
                        DSharp.Background = Brushes.Aqua;
                        Ds();
                        await Task.Delay(500);
                        DSharp.Background = Brushes.Transparent;
                        break;
                    case Key.T:
                        FSharp.Background = Brushes.Aqua;
                        Fs();
                        await Task.Delay(500);
                        FSharp.Background = Brushes.Transparent;
                        break;
                    case Key.Y:
                        GSharp.Background = Brushes.Aqua;
                        Gs();
                        await Task.Delay(500);
                        GSharp.Background = Brushes.Transparent;
                        break;
                    case Key.U:
                        ASharp.Background = Brushes.Aqua;
                        Bb();
                        await Task.Delay(500);
                        ASharp.Background = Brushes.Transparent;
                        break;
                    case Key.O:
                        CSharp1.Background = Brushes.Aqua;
                        Cs1();
                        await Task.Delay(500);
                        CSharp1.Background = Brushes.Transparent;
                        break;
                    case Key.P:
                        DSharp1.Background = Brushes.Aqua;
                        Ds1();
                        await Task.Delay(500);
                        DSharp1.Background = Brushes.Transparent;
                        break;
                }
            }
            else if ((Practice.IsChecked == true || CustomMode == true) && IsWriteScript == false)
            {
                switch (e.Key)
                {
                    case Key.A:
                        C();
                        break;
                    case Key.S:
                        D();
                        break;
                    case Key.D:
                        E();
                        break;
                    case Key.F:
                        F();
                        break;
                    case Key.G:
                        G();
                        break;
                    case Key.H:
                        A();
                        break;
                    case Key.J:
                        B();
                        break;
                    case Key.K:
                        C1();
                        break;
                    case Key.L:
                        D1();
                        break;
                    case Key.OemSemicolon:
                        E1();
                        break;
                    case Key.OemQuotes:
                        F1();
                        break;
                    case Key.W:
                        Cs();
                        break;
                    case Key.E:
                        Ds();
                        break;
                    case Key.T:
                        Fs();
                        break;
                    case Key.Y:
                        Gs();
                        break;
                    case Key.U:
                        Bb();
                        break;
                    case Key.O:
                        Cs1();
                        break;
                    case Key.P:
                        Ds1();
                        break;
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CustomMode = false;
            IsWriteScript = false;
            indexofnote = 0;
            try
            {
                ComposePaper.IsExpanded = false;
                Ckey.Background = Brushes.Transparent;
                Dkey.Background = Brushes.Transparent;
                Ekey.Background = Brushes.Transparent;
                Fkey.Background = Brushes.Transparent;
                Gkey.Background = Brushes.Transparent;
                Akey.Background = Brushes.Transparent;
                Bkey.Background = Brushes.Transparent;
                C1key.Background = Brushes.Transparent;
                D1key.Background = Brushes.Transparent;
                E1key.Background = Brushes.Transparent;
                F1key.Background = Brushes.Transparent;
                CSharp.Background = Brushes.Transparent;
                DSharp.Background = Brushes.Transparent;
                FSharp.Background = Brushes.Transparent;
                GSharp.Background = Brushes.Transparent;
                ASharp.Background = Brushes.Transparent;
                CSharp1.Background = Brushes.Transparent;
                DSharp1.Background = Brushes.Transparent;
            }
            catch (Exception)
            {

            }
        }

        private void Practice_Checked(object sender, RoutedEventArgs e)
        {
            ComposePaper.IsExpanded = false;
            CustomMode = false;
            IsWriteScript = false;
            try
            {
                Ckey.Background = Brushes.Transparent;
                Dkey.Background = Brushes.Transparent;
                Ekey.Background = Brushes.Transparent;
                Fkey.Background = Brushes.Transparent;
                Gkey.Background = Brushes.Transparent;
                Akey.Background = Brushes.Transparent;
                Bkey.Background = Brushes.Transparent;
                C1key.Background = Brushes.Transparent;
                D1key.Background = Brushes.Transparent;
                E1key.Background = Brushes.Transparent;
                F1key.Background = Brushes.Transparent;
                CSharp.Background = Brushes.Transparent;
                DSharp.Background = Brushes.Transparent;
                FSharp.Background = Brushes.Transparent;
                GSharp.Background = Brushes.Transparent;
                ASharp.Background = Brushes.Transparent;
                CSharp1.Background = Brushes.Transparent;
                DSharp1.Background = Brushes.Transparent;
            }
            catch (Exception)
            {

            }
            indexofnote = 0;
            DemoScript = LittleStar;
            LightOut(DemoScript[indexofnote]);
        }
        public void LightOut(int index)
        {
            switch (index)
            {
                case 1:
                    Ckey.Background = Brushes.Yellow;
                    break;
                case 2:
                    Dkey.Background = Brushes.Yellow;
                    break;
                case 3:
                    Ekey.Background = Brushes.Yellow;
                    break;
                case 4:
                    Fkey.Background = Brushes.Yellow;
                    break;
                case 5:
                    Gkey.Background = Brushes.Yellow;
                    break;
                case 6:
                    Akey.Background = Brushes.Yellow;
                    break;
                case 7:
                    Bkey.Background = Brushes.Yellow;
                    break;
                case 8:
                    C1key.Background = Brushes.Yellow;
                    break;
                case 9:
                    D1key.Background = Brushes.Yellow;
                    break;
                case 10:
                    E1key.Background = Brushes.Yellow;
                    break;
                case 11:
                    F1key.Background = Brushes.Yellow;
                    break;
                case 12:
                    CSharp.Background = Brushes.Yellow;
                    break;
                case 13:
                    DSharp.Background = Brushes.Yellow;
                    break;
                case 14:
                    FSharp.Background = Brushes.Yellow;
                    break;
                case 15:
                    GSharp.Background = Brushes.Yellow;
                    break;
                case 16:
                    ASharp.Background = Brushes.Yellow;
                    break;
                case 17:
                    CSharp1.Background = Brushes.Yellow;
                    break;
                case 18:
                    DSharp1.Background = Brushes.Yellow;
                    break;

            }
        }

        private void Custom_Checked(object sender, RoutedEventArgs e)
        {
            ComposePaper.IsExpanded = true;
            IsWriteScript = true;
            try
            {
                Ckey.Background = Brushes.Transparent;
                Dkey.Background = Brushes.Transparent;
                Ekey.Background = Brushes.Transparent;
                Fkey.Background = Brushes.Transparent;
                Gkey.Background = Brushes.Transparent;
                Akey.Background = Brushes.Transparent;
                Bkey.Background = Brushes.Transparent;
                C1key.Background = Brushes.Transparent;
                D1key.Background = Brushes.Transparent;
                E1key.Background = Brushes.Transparent;
                F1key.Background = Brushes.Transparent;
                CSharp.Background = Brushes.Transparent;
                DSharp.Background = Brushes.Transparent;
                FSharp.Background = Brushes.Transparent;
                GSharp.Background = Brushes.Transparent;
                ASharp.Background = Brushes.Transparent;
                CSharp1.Background = Brushes.Transparent;
                DSharp1.Background = Brushes.Transparent;
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(MusicScript.Document.ContentStart, MusicScript.Document.ContentEnd);
            ConvertScriptToMusicComposition(textRange.Text);
        }
        public void ConvertScriptToMusicComposition(string script)
        {
            try
            {
                string[] split = script.ToLower().Trim().Split(' ');
                int[] newscript = new int[split.Count()];
                int count = 0;
                foreach (string str in split)
                {
                    switch (str)
                    {
                        case "c":
                            newscript[count++] = 1;
                            break;
                        case "d":
                            newscript[count++] = 2;
                            break;
                        case "e":
                            newscript[count++] = 3;
                            break;
                        case "f":
                            newscript[count++] = 4;
                            break;
                        case "g":
                            newscript[count++] = 5;
                            break;
                        case "a":
                            newscript[count++] = 6;
                            break;
                        case "b":
                            newscript[count++] = 7;
                            break;
                        case "c1":
                            newscript[count++] = 8;
                            break;
                        case "d1":
                            newscript[count++] = 9;
                            break;
                        case "e1":
                            newscript[count++] = 10;
                            break;
                        case "f1":
                            newscript[count++] = 11;
                            break;
                        case "cs":
                            newscript[count++] = 12;
                            break;
                        case "ds":
                            newscript[count++] = 13;
                            break;
                        case "fs":
                            newscript[count++] = 14;
                            break;
                        case "gs":
                            newscript[count++] = 15;
                            break;
                        case "bb":
                            newscript[count++] = 16;
                            break;
                        case "cs1":
                            newscript[count++] = 17;
                            break;
                        case "ds1":
                            newscript[count++] = 18;
                            break;
                        default:
                            MessageBoxOK ms = new MessageBoxOK();
                            var dt = ms.DataContext as MessageBoxOKViewModel;
                            dt.Content = "Script format is invalid";
                            ms.ShowDialog();
                            ComposePaper.IsExpanded = false;
                            IsWriteScript = false;
                            Adventure.IsChecked = true;
                            return;
                            
                    }
                }
                DemoScript = newscript;
                IsWriteScript = false;
                CustomMode = true;
                indexofnote = 0;
                ComposePaper.IsExpanded = false;
                LightOut(DemoScript[indexofnote]);
            }
            catch (Exception)
            {
                MessageBoxOK ms = new MessageBoxOK();
                var dt = ms.DataContext as MessageBoxOKViewModel;
                dt.Content = "Script format is invalid";
                ms.ShowDialog();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var projectPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string filePath;
            ImageBrush imageBrushBack = new ImageBrush();
            BitmapImage bitmapBack = new BitmapImage();
            bitmapBack.BeginInit();
            bitmapBack.CacheOption = BitmapCacheOption.OnLoad;
            int Hour = DateTime.Now.Hour;
            if (Hour >= 6 && Hour < 12)
            {
                filePath = System.IO.Path.Combine(projectPath, "Image", "Morning.jpg");
            }
            else if (Hour >= 12 && Hour < 18)
            {
                filePath = System.IO.Path.Combine(projectPath, "Image", "Afternoon.jpg");
            }
            else
            {
                filePath = System.IO.Path.Combine(projectPath, "Image", "Night.jpg");
            }
            bitmapBack.UriSource = new Uri(filePath);
            bitmapBack.EndInit();
            imageBrushBack.ImageSource = bitmapBack;
            imageBrushBack.Stretch = Stretch.UniformToFill;
            MainBackground.Background = imageBrushBack;
        }
    }
}
