using MusicMediaPlayer.Model;
using MusicMediaPlayer.UserControlMusic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicMediaPlayer.ViewModel
{
    public class HomeViewModel:BaseViewModel
    {
        private bool EverLoaded = false;
        public CurrentUserAccountModel CurrentUser { get; set; }
        public ICommand LoadTheme { get; set; }
        public HomeViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            LoadTheme = new RelayCommand<Welcome>(p => { return true; }, (p) =>
            {
                var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                string filePath;
                ImageBrush imageBrushBack = new ImageBrush();
                BitmapImage bitmapBack = new BitmapImage();
                bitmapBack.BeginInit();
                bitmapBack.CacheOption = BitmapCacheOption.OnLoad;
                int Hour = DateTime.Now.Hour;
                if (Hour >= 0 && Hour < 6)
                {
                    p.WelcomeText.Text = "New Nice Day";
                    filePath = Path.Combine(projectPath, "Image", "Sunset.jpg");
                }
                else if (Hour >= 6 && Hour < 12)
                {
                    p.WelcomeText.Text = "Good Morning";
                    filePath = Path.Combine(projectPath, "Image", "Morning.jpg");
                }
                else if (Hour >= 12 && Hour < 18)
                {
                    p.WelcomeText.Text = "Good Evening";
                    filePath = Path.Combine(projectPath, "Image", "Evening.jpg");
                }
                else
                {
                    p.WelcomeText.Text = "Good Night";
                    filePath = Path.Combine(projectPath, "Image", "Night.jpg");
                }
                bitmapBack.UriSource = new Uri(filePath);
                bitmapBack.EndInit();
                imageBrushBack.ImageSource = bitmapBack;
                imageBrushBack.Stretch = Stretch.UniformToFill;
                p.MainBackground.Background = imageBrushBack;
                if (EverLoaded == false)
                {
                    var acc = DataProvider.Ins.DB.UserAccounts.Where((x) => x.UserName == CurrentUser.UserName).SingleOrDefault();
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    MemoryStream stream = new MemoryStream(acc.UserImage);
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    imageBrush.ImageSource = bitmap;
                    imageBrush.Stretch = Stretch.UniformToFill;
                    p.UserPic.Background = imageBrush;
                    p.User.Text = CurrentUser.UserName;
                    EverLoaded = true;
                }
               
            });
        }
    }
}
