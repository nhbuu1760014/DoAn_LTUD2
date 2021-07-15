using LTUDQL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LTUDQL2.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand RegisterCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
      

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand<Page>((prams) => { return true; }, (prams) => { Register(prams); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((prams) => { return true; }, (prams) => { Password = prams.Password; });
        }

        void Register(Page parms)
        {
            if (parms == null)
                return;
            else
            {
                var email = DataProvider.Ins.DB.Users.Where(u => u.Email == Email);

                if (email.Count() > 0)
                {
                    MessageBox.Show("Email đã tồn tại xin mời Đăng ký lại", "Thông báo!");
                    parms.NavigationService.Navigate(new Uri("Views/Register.xaml", UriKind.Relative));
                }
                else
                {
                    DataProvider.Ins.DB.Users.Add(new User()
                    {
                        Email = Email,
                        Name = "NO NAME",
                        Password = Password,
                        Role = 0,
                        PurchaseInfo = "",
                    });

                    DataProvider.Ins.DB.SaveChanges();
                    var wnd = Window.GetWindow(parms);
                    Views.HomePage hp = new Views.HomePage(Email);
                    hp.Show();
                    wnd.Close();
                }
            }
        }
    }
}