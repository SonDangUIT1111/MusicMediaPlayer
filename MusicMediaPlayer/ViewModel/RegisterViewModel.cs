using MusicMediaPlayer.Model;
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
    public class RegisterViewModel:BaseViewModel
    {
        public bool IsSignedUp { get; set; }
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private string _ConfirmPassword;
        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _ConfirmPassword; set { _Email = value; OnPropertyChanged(); } }
        public ICommand ToLogin { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ConfirmPasswordChangedCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public RegisterViewModel()
        {
            IsSignedUp = false;
            ToLogin = new RelayCommand<Window>((p) => { return true; }, (p) =>
             {
                 if (p == null)
                     return;
                 p.Close();
                 Login login = new Login();
                 login.ShowDialog();
                 
             });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { ConfirmPassword = p.Password; });
            SignUpCommand = new RelayCommand<Window>((p) => { return true; },(p) =>
            {
                Sign(p);
            } );
        }
        void Sign(Window p)
        {
            if (p == null)
                return;
            if (String.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Please enter the user name");
                return;
            }
            else if (String.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please enter the password");
                return;
            }
            else if (String.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Please confirm the password");
                return;
            }
            else if (String.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Please enter the email to protect the account");
                return;
            }
            else
            {
                var UserCountm = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == Username).Count();
                var EmailCountm = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserEmail == Username).Count();
                if (UserCountm > 0 )
                {
                    IsSignedUp = false;
                    MessageBox.Show("Username has been used, please try another username");
                }
                if (EmailCountm > 0)
                {
                    IsSignedUp = false;
                    MessageBox.Show("Email has been used, please try another email");
                }
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Password confirms wrong");
                }
                else
                {
                    string passEncode = CreateMD5(Base64Encode(Password));
                    //them du lieu vao database

                    var newuser = new UserAccount() { UserName = Username,UserEmail = Email, UserPassword = passEncode };
                    DataProvider.Ins.DB.UserAccounts.Add(newuser);
                    DataProvider.Ins.DB.SaveChanges();
                    IsSignedUp = true;
                    p.Close();
                    Login login = new Login();  
                    login.ShowDialog();
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
