using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class ProfileViewModel:BaseViewModel
    {
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
        private bool allow = false;
        private string currentpass;
        private string newpass;
        private string confirmnewpass;
        private string _UserName;
        private string _Password;
        private string _NickName;
        private string allowchangebutton = "Visible";
        private string accept = "Hidden";
        public string PassWord { get { return _Password; } set { _Password = value; OnPropertyChanged(); } }
        public string UserName { get { return _UserName; } set { _UserName = value; OnPropertyChanged(); } }
        public string NickName { get { return _NickName; } set { _NickName = value; OnPropertyChanged(); } }
        public bool AllowChanging { get { return allow; } set { allow = value; OnPropertyChanged(); } }
        public string AllowChangeButton { get { return allowchangebutton; } set { allowchangebutton = value; OnPropertyChanged(); } }
        public string Accept { get { return accept; } set { accept = value; OnPropertyChanged(); } }
        public CurrentUserAccountModel CurrentUser { get; set; }
        public ICommand Click { get; set; }
        public ICommand CPCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ChangeNickname { get; set; }
        public ICommand Confirm { get; set; }
        public ICommand AcceptChanging { get; set; }
        public string CurrentPass { get { return currentpass; } set { currentpass = value; } }
        public string NewPass { get { return newpass; } set { newpass = value; } }
        public string ConfirmNewPass { get { return confirmnewpass; } set { confirmnewpass = value; } }
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
                MessageBox.Show("Successfully changing the nickname");
            });
            Confirm = new RelayCommand<Window>((p)=> { return true; }, (p) =>
            {
                if (currentpass == null|| newpass == null||confirmnewpass == null)
                {
                    MessageBox.Show("Please fill all needed information");
                }
                else
                {
                    if (currentpass == PassWord)
                    {
                        if (newpass.Length < 8)
                        {
                            MessageBox.Show("Password requires length greater or equal to 8");
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

                            MessageBox.Show("Password must contain at least 1 Upcase and 1 number");
                            return;
                        }
                        if (confirmnewpass != newpass)
                        {
                            MessageBox.Show("Password did not match");
                            return;
                        }
                        else
                        {
                            string encodenewPass = CreateMD5(Base64Encode(newpass));
                            var acc = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == UserName).SingleOrDefault();
                            acc.UserPassword = encodenewPass;
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Successfully changes the password");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Current password is wrong");
                    }    
                }                 
            });
        }
    }
}
