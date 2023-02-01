using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

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
        public Border AvatarFrame { get; set; }

        //
        public ICommand Click { get; set; }
        public ICommand CPCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ChangeNickname { get; set; }
        public ICommand Confirm { get; set; }
        public ICommand AcceptChanging { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand NewPassChangedCommand { get; set; }
        public ICommand ConfirmChangedCommand { get; set; }
        public ICommand OpenExpander { get; set; }
        public ICommand AcceptChangingEmail { get; set; }
        public string CurrentPass { get { return currentpass; } set { currentpass = value; } }
        public string NewPass { get { return newpass; } set { newpass = value; } }
        public string ConfirmNewPass { get { return confirmnewpass; } set { confirmnewpass = value; } }
        public string NewEmail { get { return newemail; } set { newemail = value; } }
        public string ConfirmEmail { get { return confirmemail; } set { confirmemail = value; } }
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
                var acc = DataProvider.Ins.DB.UserAccount.Where(x => x.UserName == UserName).SingleOrDefault();
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
            Confirm = new RelayCommand<Window>((p) => { return true; }, (p) =>
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
                            var acc = DataProvider.Ins.DB.UserAccount.Where(x => x.UserName == UserName).SingleOrDefault();
                            acc.UserPassword = encodenewPass;
                            DataProvider.Ins.DB.SaveChanges();
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
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { CurrentPass = p.Password; });
            NewPassChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPass = p.Password; });
            ConfirmChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { ConfirmNewPass = p.Password; });
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
                            var acc = DataProvider.Ins.DB.UserAccount.Where(x => x.UserName == UserName).SingleOrDefault();
                            acc.UserEmail = NewEmail;
                            DataProvider.Ins.DB.SaveChanges();
                            Email = NewEmail;
                            p.EmailChange.IsEnabled = false;
                            p.EmailChange.IsExpanded = false;
                            p.NewEmailtb.Text = null;
                            p.ConfirmEmailtb.Text = null;
                            p.Password.Password = null;
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
