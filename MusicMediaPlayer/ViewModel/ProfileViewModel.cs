using Microsoft.Win32;
using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicMediaPlayer.ViewModel
{
    public class ProfileViewModel:BaseViewModel
    {
  
        private bool allow = false;
        private string currentpass;
        private string newpass;
        private string confirmnewpass;
        private string _UserName;
        private string _Password;
        private string _NickName;
        private string allowchangebutton = "Visible";
        private string accept = "Hidden";
        private byte[] image;
        private string _Email;
        private string newemail;
        private string confirmemail;
        public string PassWord { get { return _Password; } set { _Password = value; OnPropertyChanged(); } }
        public string UserName { get { return _UserName; } set { _UserName = value; OnPropertyChanged(); } }
        public string NickName { get { return _NickName; } set { _NickName = value; OnPropertyChanged(); } }
        public bool AllowChanging { get { return allow; } set { allow = value; OnPropertyChanged(); } }
        public string AllowChangeButton { get { return allowchangebutton; } set { allowchangebutton = value; OnPropertyChanged(); } }
        public string Accept { get { return accept; } set { accept = value; OnPropertyChanged(); } }
        public byte[] Image { get { return image; } set { image = value; OnPropertyChanged(); } }
        public string Email { get { return _Email; } set { _Email = value; OnPropertyChanged(); } }
        public CurrentUserAccountModel CurrentUser { get; set; }


        //

        public Profile ProfileWindow { get; set; }

        //
        public ICommand Click { get; set; }
        public ICommand CPCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ChangeNickname { get; set; }
        public ICommand Confirm { get; set; }
        public ICommand AcceptChanging { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand PasswordEyeChangedCommand { get; set; }
        public ICommand NewPassChangedCommand { get; set; }
        public ICommand NewPassEyeChangedCommand { get; set; }
        public ICommand ConfirmChangedCommand { get; set; }
        public ICommand ConfirmEyeChangedCommand { get; set; }
        public ICommand OpenExpander { get; set; }
        public ICommand AcceptChangingEmail { get; set; }
        public ICommand CancelChangingEmail { get; set; }
        public ICommand ChangeAvatarCommand { get; set; }
        public ICommand ChangeImage { get; set; }
        public ICommand Changing { get; set; }
        public ICommand CancelChanging { get; set; }
        public ICommand ShowCurrentPassword { get; set; }
        public ICommand UnShowCurrentPassword { get; set; }
        public ICommand ShowNewPassword { get; set; }
        public ICommand UnShowNewPassword { get; set; }
        public ICommand ShowConfirmPassword { get; set; }
        public ICommand UnShowConfirmPassword { get; set; }
        public ICommand ShowPassword_Email { get; set; }
        public ICommand UnshowPassword_Email { get; set; }
        public string CurrentPass { get { return currentpass; } set { currentpass = value; } }
        public string NewPass { get { return newpass; } set { newpass = value; } }
        public string ConfirmNewPass { get { return confirmnewpass; } set { confirmnewpass = value; } }
        public string NewEmail { get { return newemail; } set { newemail = value; } }
        public string ConfirmEmail { get { return confirmemail; } set { confirmemail = value; } }
        private string _ImagePathToChange;
        public string ImagePathToChange { get => _ImagePathToChange; set => _ImagePathToChange = value; }
        private UserAccount _AccountChanging;
        public UserAccount AccountChanging { get => _AccountChanging; set => _AccountChanging = value; }
        public ProfileViewModel()
        {
            CurrentUser = new CurrentUserAccountModel();
            Click = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                MessageBox.Show(CurrentUser.Id.ToString());
                p.Text = CurrentUser.Id.ToString();
            });
            CPCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                ChangePassword change = new ChangePassword();
                change.ShowDialog();
            });
            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ChangePassword change = p as ChangePassword;
                change.Close();
            });
            ChangeNickname = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AllowChanging = true;
                AllowChangeButton = "Hidden";
                Accept = "Visible";
            });
            AcceptChanging = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var acc = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == UserName).SingleOrDefault();
                if (acc != null)
                {
                    acc.NickName = NickName;
                }
                AllowChanging = false;
                AllowChangeButton = "Visible";
                Accept = "Hidden";
                DataProvider.Ins.DB.SaveChanges();
                MessageBoxSuccessful MB = new MessageBoxSuccessful(); 
                MB.ShowDialog();
            });
            Confirm = new RelayCommand<ChangePassword>((p) => { return true; }, (p) =>
            {
                if (currentpass == null || newpass == null || confirmnewpass == null)
                {
                    MessageBoxOK wd = new MessageBoxOK();
                    var data = wd.DataContext as MessageBoxOKViewModel;
                    data.Content = "Please fill all needed information";
                    wd.ShowDialog();
                }
                else
                {
                    if (currentpass == PassWord)
                    {
                        if (newpass.Length < 4)
                        {
                            MessageBoxOK wd = new MessageBoxOK();
                            var data = wd.DataContext as MessageBoxOKViewModel;
                            data.Content = "Password requires length greater or equal to 4";
                            wd.ShowDialog();
                            return;
                        }
                        //Kiểm tra validation của password 
                        int countUpcase = 0, countNum = 0;
                        foreach (char c in newpass)
                        {
                            if (c >= 'A' && c <= 'Z')
                                countUpcase++;
                            if (c >= '0' && c <= '9')
                                countNum++;
                        }
                        if (countNum == 0 || countUpcase == 0)
                        {
                            MessageBoxOK wd = new MessageBoxOK();
                            var data = wd.DataContext as MessageBoxOKViewModel;
                            data.Content = "Password must contain at least 1 Upcase and 1 number";
                            wd.ShowDialog();
                            return;
                        }
                        if (confirmnewpass != newpass)
                        {
                            MessageBoxOK wd = new MessageBoxOK();
                            var data = wd.DataContext as MessageBoxOKViewModel;
                            data.Content = "Password did not match";
                            wd.ShowDialog();
                            return;
                        }
                        else
                        {
                            string encodenewPass = CreateMD5(Base64Encode(newpass));
                            var acc = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == UserName).SingleOrDefault();
                            acc.UserPassword = encodenewPass;
                            DataProvider.Ins.DB.SaveChanges();
                            CurrentPass = null;
                            NewPass = null;
                            ConfirmNewPass = null;
                            MessageBoxSuccessful MB = new MessageBoxSuccessful();
                            MB.ShowDialog();
                            p.Close();
                        }
                    }
                    else
                    {
                        MessageBoxOK wd = new MessageBoxOK();
                        var data = wd.DataContext as MessageBoxOKViewModel;
                        data.Content = "Current password is wrong";
                        wd.ShowDialog();
                    }
                }
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { CurrentPass = p.Password; });
            PasswordEyeChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { CurrentPass = p.Text; });
            NewPassChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPass = p.Password; });
            NewPassEyeChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { NewPass = p.Text; });
            ConfirmChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { ConfirmNewPass = p.Password; });
            ConfirmEyeChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { ConfirmNewPass = p.Text; });
            OpenExpander = new RelayCommand<Expander>((p) => { return true; }, (p) => {
                p.IsEnabled = true;
                p.IsExpanded = true;
            });
            AcceptChangingEmail = new RelayCommand<Profile>((p) => { return true; }, (p) =>
            {
                if (CurrentPass == null || NewEmail == null || ConfirmEmail == null)
                {
                    MessageBoxOK wd = new MessageBoxOK();
                    var data = wd.DataContext as MessageBoxOKViewModel;
                    data.Content = "Please fill all needed information";
                    wd.ShowDialog();
                }
                else
                {
                    if (currentpass == PassWord)
                    {
                        if (!Regex.IsMatch(NewEmail, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                        {
                            MessageBoxOK wd = new MessageBoxOK();
                            var data = wd.DataContext as MessageBoxOKViewModel;
                            data.Content = "New Email format is invalid";
                            wd.ShowDialog();
                            return;
                        }
                        if (NewEmail != ConfirmEmail)
                        {
                            MessageBoxOK wd = new MessageBoxOK();
                            var data = wd.DataContext as MessageBoxOKViewModel;
                            data.Content = "Email did not match";
                            wd.ShowDialog();
                            return;
                        }
                        else
                        {
                            var acc = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == UserName).SingleOrDefault();
                            acc.UserEmail = NewEmail;
                            DataProvider.Ins.DB.SaveChanges();
                            Email = NewEmail;
                            p.EmailChange.IsEnabled = false;
                            p.EmailChange.IsExpanded = false;
                            p.NewEmailtb.Text = null;
                            p.ConfirmEmailtb.Text = null;
                            p.Password.Password = null;
                            p.PasswordEye.Text = null;
                            MessageBoxSuccessful MB = new MessageBoxSuccessful();
                            MB.ShowDialog();
                        }    
                    }   
                    else
                    {
                        MessageBoxOK wd = new MessageBoxOK();
                        var data = wd.DataContext as MessageBoxOKViewModel;
                        data.Content = "Current password is wrong";
                        wd.ShowDialog();
                    }    
                }    
            }
            );
            CancelChangingEmail = new RelayCommand<Profile>((p) => { return true; }, (p) =>
            {
                p.EmailChange.IsEnabled = false;
                p.EmailChange.IsExpanded = false;
                p.NewEmailtb.Text = null;
                p.ConfirmEmailtb.Text = null;
                p.Password.Password = null;
            }
            );
            ChangeAvatarCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ProfileWindow = p as Profile;
                var users = DataProvider.Ins.DB.UserAccounts.Where(o => o.UserId == CurrentUser.Id).SingleOrDefault();
                ChangeAvatar changewindow = new ChangeAvatar();
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                var imagestream = new MemoryStream(users.UserImage);
                bitmap.StreamSource = imagestream;
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                changewindow.ChangegrdSelectImg.Background = imageBrush;
                changewindow.ShowDialog();
            });
            ChangeImage = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Insert Image";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    ImagePathToChange = op.FileName;
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(ImagePathToChange);
                    bitmap.EndInit();
                    imageBrush.ImageSource = bitmap;
                    p.Background = imageBrush;
                    if (p.Children.Count > 1)
                    {
                        p.Children.Remove(p.Children[0]);
                        p.Children.Remove(p.Children[1]);
                    }
                }
            });
            Changing = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var users = DataProvider.Ins.DB.UserAccounts.Where(o => o.UserId == CurrentUser.Id).SingleOrDefault();
                var wd = p as ChangeAvatar;
                if (ImagePathToChange != null)
                {
                    Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                    byte[] BinaryImage = converter.ImageToBinary(ImagePathToChange);
                    users.UserImage = BinaryImage;
                }
                DataProvider.Ins.DB.SaveChanges();
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(ImagePathToChange);
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                ProfileWindow.AvatarFrame.Background = imageBrush;
                ImagePathToChange = null;
                MessageBoxSuccessful MB = new MessageBoxSuccessful();
                MB.ShowDialog();
                wd.Close();
            });
            CancelChanging = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var wd = p as ChangeAvatar;
                wd.Close();
                ImagePathToChange = null;
            });
            ShowCurrentPassword = new RelayCommand<ChangePassword>((p) => { return true; }, (p) =>
            {
                p.ShowCurrent.Visibility = Visibility.Hidden;
                p.UnshowCurrent.Visibility = Visibility.Visible;
                p.PasswordEye.Text = p.Password.Password;
                p.PasswordEye.Visibility = Visibility.Visible;
                p.Password.Visibility = Visibility.Hidden;
            });
            UnShowCurrentPassword = new RelayCommand<ChangePassword>((p) => { return true; }, (p) =>
            {
                p.ShowCurrent.Visibility = Visibility.Visible;
                p.UnshowCurrent.Visibility= Visibility.Hidden;
                p.Password.Visibility = Visibility.Visible;
                p.Password.Password = p.PasswordEye.Text;
                p.PasswordEye.Visibility = Visibility.Hidden;
            });
            ShowNewPassword = new RelayCommand<ChangePassword>((p) => { return true; }, (p) =>
            {
                p.ShowNewPass.Visibility = Visibility.Hidden;
                p.UnshowNewPass.Visibility = Visibility.Visible;
                p.NewPassEye.Text = p.NewPass.Password;
                p.NewPassEye.Visibility = Visibility.Visible;
                p.NewPass.Visibility = Visibility.Hidden;
            });
            UnShowNewPassword = new RelayCommand<ChangePassword>((p) => { return true; }, (p) =>
            {
                p.ShowNewPass.Visibility = Visibility.Visible;
                p.UnshowNewPass.Visibility = Visibility.Hidden;
                p.NewPass.Visibility = Visibility.Visible;
                p.NewPass.Password = p.NewPassEye.Text;
                p.NewPassEye.Visibility = Visibility.Hidden;
            });
            ShowConfirmPassword = new RelayCommand<ChangePassword>((p) => { return true; }, (p) =>
            {
                p.ShowConfirmPass.Visibility = Visibility.Hidden;
                p.UnshowConfirmPass.Visibility = Visibility.Visible;
                p.ConfirmPassEye.Text = p.ConfirmPass.Password;
                p.ConfirmPassEye.Visibility = Visibility.Visible;
                p.ConfirmPass.Visibility = Visibility.Hidden;
            });
            UnShowConfirmPassword = new RelayCommand<ChangePassword>((p) => { return true; }, (p) =>
            {
                p.ShowConfirmPass.Visibility = Visibility.Visible;
                p.UnshowConfirmPass.Visibility = Visibility.Hidden;
                p.ConfirmPass.Visibility = Visibility.Visible;
                p.ConfirmPass.Password = p.ConfirmPassEye.Text;
                p.ConfirmPassEye.Visibility = Visibility.Hidden;
            });
            ShowPassword_Email = new RelayCommand<Profile>((p) => { return true; }, (p) =>
            {
                p.ShowPass.Visibility = Visibility.Hidden;
                p.UnshowPass.Visibility = Visibility.Visible;
                p.PasswordEye.Text = p.Password.Password;
                p.PasswordEye.Visibility = Visibility.Visible;
                p.Password.Visibility = Visibility.Hidden;
            });
            UnshowPassword_Email = new RelayCommand<Profile>((p) => { return true; }, (p) =>
            {
                p.ShowPass.Visibility = Visibility.Visible;
                p.UnshowPass.Visibility = Visibility.Hidden;
                p.Password.Visibility = Visibility.Visible;
                p.Password.Password = p.PasswordEye.Text;
                p.PasswordEye.Visibility = Visibility.Hidden;
            });

        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
