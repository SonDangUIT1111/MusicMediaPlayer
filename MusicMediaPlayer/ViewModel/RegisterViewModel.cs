using MusicMediaPlayer.Model;
using MusicMediaPlayer.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ICommand ConfirmPasswordChangedCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SendCodeCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand ConfirmNewPasswordChangedCommand { get; set; }
        public ICommand VerifiedCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand CheckCode { get; set; }

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
            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { ConfirmPassword = p.Password; });
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
        }
        void Sign(Window p)
        {
            if (p == null)
                return;
            //Kiểm tra đã nhập đủ thông tin
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
                
                    MessageBox.Show("Password must contain at least 1 Upcase and 1 number");
                    return;
            }
            else if (Password != ConfirmPassword)
            {
                MessageBox.Show("Password confirmes wrong");
                return;
            }
            else if (!Regex.IsMatch(Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                    MessageBox.Show("Email format is invalid");
                    return;
            }
            else
            {
                //Kiểm tra user và email đăng ký có tồn tại không 
                var UserCountm = DataProvider.Ins.DB.UserAccount.Where(x => x.UserName == Username).Count();
                var EmailCountm = DataProvider.Ins.DB.UserAccount.Where(x => x.UserEmail == Email).Count();
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
                else
                {
                    string passEncode = CreateMD5(Base64Encode(Password));
                    //Thêm user mới vào database
                    try
                    {
                        var newuser = new UserAccount() { UserName = Username, UserEmail = Email, UserPassword = passEncode };
                        DataProvider.Ins.DB.UserAccount.Add(newuser);
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
                MessageBox.Show("Please enter the email has assigned");
                return;
            }
            else
            {
                //Kiểm tra email bảo vệ có tồn tại không 
                var EmailCountm = DataProvider.Ins.DB.UserAccount.Where(x => x.UserEmail == EmailProtected).Count();
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
                    MessageBox.Show("This email has not been assigned");
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
                    MessageBox.Show("The code is not right");
                    IsVerified = false;
                }
            }
            catch (Exception)
            {
                IsVerified = false;
                MessageBox.Show("Code format is not correct");
            }
        }
        void Change(Window p)
        {
            if (p == null)
                return;
            if (String.IsNullOrEmpty(NewPassword))
            {
                MessageBox.Show("Please enter new password");
                return;
            }
            if (String.IsNullOrEmpty(ConfirmNewPassword))
            {

                MessageBox.Show("Please confirm new password");
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

                MessageBox.Show("Password must contain at least 1 Upcase and 1 number");
                return;
            }
            if (ConfirmNewPassword != NewPassword)
            {
                MessageBox.Show("Confirm wrong");
                return;
            }
            else
            {
                string encodenewPass = CreateMD5(Base64Encode(NewPassword));
                var acc = DataProvider.Ins.DB.UserAccount.Where(x => x.UserEmail == EmailProtected).SingleOrDefault();
                acc.UserPassword = encodenewPass;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Successfully changes password");
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
                MessageBox.Show("The code verified has been sent to your email protect");
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to sent, please wait a minute and try again");
            }
        }

    }
}
