using LTUDQL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LTUDQL2.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _ErrorMail;
        public string ErrMail { get => _ErrorMail; set { _ErrorMail = value; OnPropertyChanged(); } }
        private string _ErrorPass;
        public string ErrPass { get => _ErrorPass; set { _ErrorPass = value; OnPropertyChanged(); } }

        private string _NameUser;
        public string NameUser { get => _NameUser; set { _NameUser = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<Page>((prams) => { return true; }, (prams) => { Login(prams); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((prams) => { return true; }, (prams) => { Password = prams.Password; });
        }

        // is validate email
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        bool isEmail = false, isPass = false;
        void Login(Page parms)
        {
            if (parms == null)
                return;
            else
            {
                if (Email == null)
                {
                    ErrMail = "Email không được trống!";
                    isEmail = false;
                }
                else
                {
                    if (isValidEmail(Email) == false)
                    {
                        ErrMail = "Email này không hợp lệ!";
                        isEmail = false;
                    }
                    else
                    {
                        ErrMail = "";
                        isEmail = true;
                    }
                }
                if (Password == null)
                {
                    ErrPass = "Password không được trống!";
                    isPass = false;
                }
                else
                {
                    if (Password.Length < 8)
                    {
                        ErrPass = "Password ít nhất 8 ký tự!";
                        isPass = false;
                    }
                    else
                    {
                        ErrPass = "";
                        isPass = true;
                    }
                }

                if (isEmail == true && isPass == true)
                {
                    var account = DataProvider.Ins.DB.Users.Where(u => u.Email == Email).SingleOrDefault();

                    if (account != null)
                    {
                        ErrMail = "";
                        if (account.Password == Password)
                        {
                            if(account.Role == 1)
                            {
                                var wnd = Window.GetWindow(parms);
                                AdminForm ad = new AdminForm();
                                ad.Show();
                                wnd.Close();
                            }
                            else
                            {
                                var wnd = Window.GetWindow(parms);
                                Views.HomePage hp = new Views.HomePage();
                                NameUser = account.Name;
                                hp.Show();
                                wnd.Close();
                            }
                        }
                        else
                        {
                            ErrPass = "Mật khẩu không chính xác!";
                            return;
                        }
                    }
                    else
                    {
                        ErrMail = "Email này chưa đăng ký hoặc không tồn tại!";
                        return;
                    }
                }


            }
        }
    }
}
