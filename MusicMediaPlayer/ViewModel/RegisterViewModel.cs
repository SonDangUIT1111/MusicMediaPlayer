using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicMediaPlayer.ViewModel
{
    public class RegisterViewModel:BaseViewModel,IDataErrorInfo
    {
        public bool IsSend { get; set; }
        public bool IsVerified { get; set; }
        public bool IsSignedUp { get; set; }
        public int RandomCode { get; set; }
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private string _ConfirmPassword;
        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _EmailProtected;
        public string EmailProtected { get => _EmailProtected; set { _EmailProtected = value; OnPropertyChanged(); } }
        private string _NewPassword;
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; OnPropertyChanged(); } }
        private string _ConfirmNewPassword;

        private string _Code;
        public string Code { get => _Code; set { _Code = value; OnPropertyChanged(); } }
        public string ConfirmNewPassword { get => _ConfirmNewPassword; set { _ConfirmNewPassword = value; OnPropertyChanged(); } }
        public ICommand ToLogin { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand PasswordEyeChangedCommand { get; set; }
        public ICommand ConfirmPasswordChangedCommand { get; set; }

        public ICommand ConfirmPasswordEyeChangedCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SendCodeCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand NewPassEyeChangedCommand { get; set; }
        public ICommand ConfirmNewPasswordChangedCommand { get; set; }
        public ICommand ConfirmEyeChangedCommand { get; set; }
        public ICommand VerifiedCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand CheckCode { get; set; }
        public ICommand ShowNewPassword { get; set; }
        public ICommand UnshowNewPassword { get; set; }
        public ICommand ShowConfirmPassword { get; set; }
        public ICommand UnshowConfirmPassword { get; set; }
        public ICommand ShowPassword_Register { get; set; }
        public ICommand UnshowPassword_Register { get; set; }
        public ICommand ShowConfirmPassword_Register { get; set; }
        public ICommand UnshowConfirmPassword_Register { get; set; }
        public string Error { get { return null; } }

        public string this[string columnName]
        {
            get
            {
                string ErrorMess = null;
                switch (columnName)
                {
                    case "Username":
                        if (String.IsNullOrEmpty(Username))
                            ErrorMess = "Username can not be empty";
                        if (Username.Length < 4)
                            ErrorMess = "Username lenght has to be greater or equal to 4";
                        break;
                    case "Email":
                        if (String.IsNullOrEmpty(Email))
                            ErrorMess = "Email can not be empty";
                        break;

                }
                return ErrorMess;
            }
        }

        public RegisterViewModel()
        {
            //khởi tạo
            RandomCode = 0;
            //cờ đánh dấu trạng thái
            IsSend = false;
            IsVerified = false;
            IsSignedUp = false;
            //lệnh nhấn nút login
            ToLogin = new RelayCommand<Window>((p) => { return true; }, (p) =>
             {
                 if (p == null)
                     return;
                 p.Close();
                 Login login = new Login();
                 login.ShowDialog();
                 
             });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            PasswordEyeChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { Password = p.Text; });
            ConfirmPasswordEyeChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { ConfirmPassword = p.Text; });
            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { ConfirmPassword = p.Password; });
            NewPassEyeChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { NewPassword = p.Text; });
            ConfirmEyeChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { ConfirmNewPassword = p.Text; });
            //lệnh nhấn nút sign up
            SignUpCommand = new RelayCommand<Window>((p) => { return true; },(p) =>
            {
                Sign(p);
            } );
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPassword = p.Password; });
            ConfirmNewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { ConfirmNewPassword = p.Password; });
            SendCodeCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Send(p);
            });
            CheckCode = new RelayCommand<Window>((p) =>
            {
                var window = p as ForgotPassword;
                var code = window.CodeVerified.Text;
                if (!String.IsNullOrEmpty(code))
                {
                    if (code.Length == 6)
                        return true;
                    else
                    {
                        window.VerifiedButton.IsEnabled = false;
                        return false;
                    }
                }
                else
                {
                    return false;
                }    
            }
            , (p) =>
             {
                 var window = p as ForgotPassword;
                 window.VerifiedButton.IsEnabled = true;
             });
            VerifiedCommand = new RelayCommand<Window>((p) =>
            {
                if (IsSend == true)

                    return true;
                else return false;
            }, 
            (p) =>
            {
                Verified(p);
            });
            ChangePasswordCommand = new RelayCommand<Window>((p) => 
            { 
                if (IsVerified == true)
                {
                    return true;
                }
                else return false; 
            }, (p) =>
             {
                 Change(p);
             });
            ShowNewPassword = new RelayCommand<ForgotPassword>((p) => { return true; }, (p) =>
            {
                p.ShowNewPass.Visibility = Visibility.Hidden;
                p.UnshowNewPass.Visibility = Visibility.Visible;
                p.NewPassEye.Text = p.NewPassword.Password;
                p.NewPassEye.Visibility = Visibility.Visible;
                p.NewPassword.Visibility = Visibility.Hidden;
            });
            UnshowNewPassword = new RelayCommand<ForgotPassword>((p) => { return true; }, (p) =>
            {
                p.ShowNewPass.Visibility = Visibility.Visible;
                p.UnshowNewPass.Visibility = Visibility.Hidden;
                p.NewPassword.Visibility = Visibility.Visible;
                p.NewPassword.Password = p.NewPassEye.Text;
                p.NewPassEye.Visibility = Visibility.Hidden;
            });
            ShowConfirmPassword = new RelayCommand<ForgotPassword>((p) => { return true; }, (p) =>
            {
                p.ShowConfirmPass.Visibility = Visibility.Hidden;
                p.UnshowConfirmPass.Visibility = Visibility.Visible;
                p.ConfirmPassEye.Text = p.ConfirmNewPassword.Password;
                p.ConfirmPassEye.Visibility = Visibility.Visible;
                p.ConfirmNewPassword.Visibility = Visibility.Hidden;
            });
            UnshowConfirmPassword = new RelayCommand<ForgotPassword>((p) => { return true; }, (p) =>
            {
                p.ShowConfirmPass.Visibility = Visibility.Visible;
                p.UnshowConfirmPass.Visibility = Visibility.Hidden;
                p.ConfirmNewPassword.Visibility = Visibility.Visible;
                p.ConfirmNewPassword.Password = p.ConfirmPassEye.Text;
                p.ConfirmPassEye.Visibility = Visibility.Hidden;
            });
            ShowPassword_Register = new RelayCommand<Register>((p) => { return true; }, (p) =>
            {
                p.ShowPass.Visibility = Visibility.Hidden;
                p.UnshowPass.Visibility = Visibility.Visible;
                p.PasswordEye.Text = p.Password.Password;
                p.PasswordEye.Visibility = Visibility.Visible;
                p.Password.Visibility = Visibility.Hidden;
            });
            UnshowPassword_Register = new RelayCommand<Register>((p) => { return true; }, (p) =>
            {
                p.ShowPass.Visibility = Visibility.Visible;
                p.UnshowPass.Visibility = Visibility.Hidden;
                p.Password.Visibility = Visibility.Visible;
                p.Password.Password = p.PasswordEye.Text;
                p.PasswordEye.Visibility = Visibility.Hidden;
            });
            ShowConfirmPassword_Register = new RelayCommand<Register>((p) => { return true; }, (p) =>
            {
                p.ShowConfirmPass.Visibility = Visibility.Hidden;
                p.UnshowConfirmPass.Visibility = Visibility.Visible;
                p.ConfirmPasswordEye.Text = p.ConfirmPassword.Password;
                p.ConfirmPasswordEye.Visibility = Visibility.Visible;
                p.ConfirmPassword.Visibility = Visibility.Hidden;
            });
            UnshowConfirmPassword_Register = new RelayCommand<Register>((p) => { return true; }, (p) =>
            {
                p.ShowConfirmPass.Visibility = Visibility.Visible;
                p.UnshowConfirmPass.Visibility = Visibility.Hidden;
                p.ConfirmPassword.Visibility = Visibility.Visible;
                p.ConfirmPassword.Password = p.ConfirmPasswordEye.Text;
                p.ConfirmPasswordEye.Visibility = Visibility.Hidden;
            });
        }
        void Sign(Window p)
        {
            if (p == null)
                return;
            //Kiểm tra đã nhập đủ thông tin
            if (String.IsNullOrEmpty(Username))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Please enter the user name";
                MB.ShowDialog();
                return;
            }
            else if (String.IsNullOrEmpty(Password))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Please enter the password";
                MB.ShowDialog();
                return;
            }
            else if (Password.Length < 4)
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Password length has to be greater or euqal to 4";
                MB.ShowDialog();
                return;
            }
            else if (String.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Please confirm the password";
                MB.ShowDialog();
                return;
            }
            else if (String.IsNullOrEmpty(Email))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Please enter the email to protect the account";
                MB.ShowDialog();
                return;
            }
            //Kiểm tra validation của password và email
            int countUpcase = 0, countNum = 0;
            foreach (char c in Password)
            {
                if (c >= 'A' && c <= 'Z')
                    countUpcase++;
                if (c >= '0' && c <= '9')
                    countNum++;
            }
            if (countNum == 0 || countUpcase == 0)
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Password must contain at least 1 Upcase and 1 number";
                MB.ShowDialog();
                return;
            }
            else if (Password != ConfirmPassword)
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Password confirmes wrong";
                MB.ShowDialog();
                return;
            }
            else if (!Regex.IsMatch(Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Email format is invalid";
                MB.ShowDialog();
                return;
            }
            else
            {
                //Kiểm tra user và email đăng ký có tồn tại không 
                var UserCountm = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserName == Username).Count();
                var EmailCountm = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserEmail == Email).Count();
                if (UserCountm > 0 )
                {
                    IsSignedUp = false;
                    MessageBoxOK MB = new MessageBoxOK();
                    var data = MB.DataContext as MessageBoxOKViewModel;
                    data.Content = "Username has been used, please try another username";
                    MB.ShowDialog();
                }
                if (EmailCountm > 0)
                {
                    IsSignedUp = false;
                    MessageBoxOK MB = new MessageBoxOK();
                    var data = MB.DataContext as MessageBoxOKViewModel;
                    data.Content = "Email has been used, please try another email";
                    MB.ShowDialog();
                }
                else
                {
                    string passEncode = CreateMD5(Base64Encode(Password));
                    //Thêm user mới vào database
                    try
                    {
                        var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                        var filePath = Path.Combine(projectPath, "Image", "music_note.jpg");
                        Converter.ByteArrayToBitmapImageConverter converter = new MusicMediaPlayer.Converter.ByteArrayToBitmapImageConverter();
                        string uriIamge = filePath;
                        byte[] newUserAvatar = converter.ImageToBinary(uriIamge);
                        var newuser = new UserAccount() { UserName = Username, NickName = Username ,UserEmail = Email, UserPassword = passEncode, UserImage = newUserAvatar };
                        DataProvider.Ins.DB.UserAccounts.Add(newuser);
                        DataProvider.Ins.DB.SaveChanges();
                        IsSignedUp = true;
                        p.Close();
                        Login login = new Login();
                        login.ShowDialog();
                    }
                    catch (Exception)
                    {
                       
                    }
                
                }
            }
            
        }
        void Send(Window p)
        {
            if (p == null)
                return;
            //Kiểm tra đã nhập đủ thông tin
            if (String.IsNullOrEmpty(EmailProtected))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Please enter the email has assigned";
                MB.ShowDialog();
                return;
            }
            else
            {
                //Kiểm tra email bảo vệ có tồn tại không 
                var EmailCountm = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserEmail == EmailProtected).Count();
                //email đúng
                if (EmailCountm > 0)
                {
                    IsSend = true;
                    Random rd = new Random();
                    RandomCode = rd.Next(100000,999999);
                    string RandomCodeString = RandomCode.ToString();
                    MessageBox.Show(RandomCodeString);
                    SendCodeByEmail(RandomCodeString, "thienthanvsacquy1234@gmail.com");
                    return;
                }
                else
                {
                    IsSend = false;
                    MessageBoxOK MB = new MessageBoxOK();
                    var data = MB.DataContext as MessageBoxOKViewModel;
                    data.Content = "This email has not been assigned";
                    MB.ShowDialog();
                    return;
                }
            }
        }
        void Verified(Window p)
        {
            if (p == null)
                return;
            var window = p as ForgotPassword;
            try
            {
                if (Int32.Parse(window.CodeVerified.Text) == RandomCode)
                {
                    IsVerified = true;
                    return;
                }
                else
                {
                    MessageBoxOK MB = new MessageBoxOK();
                    var data = MB.DataContext as MessageBoxOKViewModel;
                    data.Content = "The code is not right";
                    MB.ShowDialog();
                    IsVerified = false;
                }
            }
            catch (Exception)
            {
                IsVerified = false;
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Code format is not correct";
                MB.ShowDialog();
            }
        }
        void Change(Window p)
        {
            if (p == null)
                return;
            if (String.IsNullOrEmpty(NewPassword))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Please enter new password";
                MB.ShowDialog();
                return;
            }
            if (String.IsNullOrEmpty(ConfirmNewPassword))
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Please confirm new password";
                MB.ShowDialog();
                return;
            }
            //Kiểm tra validation của password 
            int countUpcase = 0, countNum = 0;
            foreach (char c in NewPassword)
            {
                if (c >= 'A' && c <= 'Z')
                    countUpcase++;
                if (c >= '0' && c <= '9')
                    countNum++;
            }
            if (countNum == 0 || countUpcase == 0)
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Password must contain at least 1 Upcase and 1 number";
                MB.ShowDialog();
                return;
            }
            if (ConfirmNewPassword != NewPassword)
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Confirm wrong";
                MB.ShowDialog();
                return;
            }
            else
            {
                string encodenewPass = CreateMD5(Base64Encode(NewPassword));
                var acc = DataProvider.Ins.DB.UserAccounts.Where(x => x.UserEmail == EmailProtected).SingleOrDefault();
                acc.UserPassword = encodenewPass;
                DataProvider.Ins.DB.SaveChanges();
                MessageBoxSuccessful MB = new MessageBoxSuccessful();
                MB.ShowDialog();
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
        public static void SendCodeByEmail(string codesend,string to)
        {
            string from, subject,messageBody;
            messageBody = "Your verified code is " + codesend;
            from = "spksk1111@gmail.com";
            subject = "Music Media Player - Changing Password";
            MailMessage message = new MailMessage(from,to,subject,messageBody);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(from, "aonfbkjdjndadyso");
            try
            {
                client.Send(message);
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "The code verified has been sent to your email protect";
                MB.ShowDialog();
            }
            catch (Exception)
            {
                MessageBoxOK MB = new MessageBoxOK();
                var data = MB.DataContext as MessageBoxOKViewModel;
                data.Content = "Failed to sent, please wait a minute and try again";
                MB.ShowDialog();
            }
        }

    }
}
