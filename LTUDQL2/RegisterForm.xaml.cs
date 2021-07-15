using LTUDQL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LTUDQL2
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void BtnDangKy_Click(object sender, RoutedEventArgs e)
        {
            if (txttendangnhap.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tài khoản ", "Thông báo ", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                txttendangnhap.Focus();
            }
            else if (txtmatkhau.Password.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu ", "Thông báo ", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                txtmatkhau.Focus();
            }
            else if (txtxacnhanmatkhau.Password.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập lại mật khẩu không đúng ", "Thông báo ", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                txtmatkhau.Focus();
            }
            else if (txtemail.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập email ", "Thông báo ", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                txtmatkhau.Focus();
            }
            else if (txtsdt.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại ", "Thông báo ", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                txtmatkhau.Focus();
            }
            else
            {
                using (var data = new QuanLyTrangPhimEntities1())
                {
                    try
                    {
                        var user = new User()
                        {
                            Email = txtemail.Text,
                            Name = txthovaten.Text,
                            Password = txtxacnhanmatkhau.Password,
                            Role = 1,
                            PurchaseInfo = "",
                        };

                        data.Users.Add(user);
                        data.SaveChanges();
                        MessageBox.Show("Create account successfully!");
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("error !!!");
                    }

                }
            }
        }
    }
}
