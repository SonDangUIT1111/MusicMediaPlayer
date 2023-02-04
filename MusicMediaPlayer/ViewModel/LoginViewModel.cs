using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        public bool IsLoggedIn { get; set; }
        private string _Username;
        public string Username { get => _Username;  set { _Username = value;OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand LogintoRegister { get; set; }
        public ICommand ToForgotPassword { get; set; }
        public ICommand LoginSuccess { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel()
        {
            IsLoggedIn = false;
            Username = "";
            Password = "";
            LogintoRegister = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                p.Resources.Clear();
                Register register = new Register();
                register.ShowDialog();
            
            });
            LoginSuccess = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Log(p);
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; },(p)=> { Password = p.Password; });
            ToForgotPassword = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                ForgotPassword forgotPassword = new ForgotPassword();
                forgotPassword.ShowDialog();

            });


        }
        void Log(Window p)
        {
            if (p == null)
                return;
            if (Username == "")
            {
                MessageBoxOK wd = new MessageBoxOK();

                var data = wd.DataContext as MessageBoxOKViewModel;

                data.Content = "Please enter username";

                wd.ShowDialog();
            }
            else if (Password == "")
            {
                MessageBoxOK wd = new MessageBoxOK();
                var data = wd.DataContext as MessageBoxOKViewModel;
                data.Content = "Please enter password";
                wd.ShowDialog();
            }
            else
            {
                string passEncode = CreateMD5(Base64Encode(Password));
                var AccCount = DataProvider.Ins.DB.UserAccount.Where(x => x.UserName == Username).Count();
                if (AccCount > 0)
                {
                    var CheckPass = DataProvider.Ins.DB.UserAccount.Where(x => x.UserName == Username && x.UserPassword == passEncode).Count();
                    if (CheckPass > 0)
                    {
                        IsLoggedIn = true;
                        p.Close();
                    }
                    else
                    {
                        IsLoggedIn = false;
                        MessageBoxOK wd = new MessageBoxOK();
                        var data = wd.DataContext as MessageBoxOKViewModel;
                        data.Content = "Wrong password";
                        wd.ShowDialog();
                        return;
                    }
                }
                else
                {
                    IsLoggedIn = false;
                    MessageBoxOK wd = new MessageBoxOK();
                    var data = wd.DataContext as MessageBoxOKViewModel;
                    data.Content = "User Account does not exists";
                    wd.ShowDialog();
                }
            }
            
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
