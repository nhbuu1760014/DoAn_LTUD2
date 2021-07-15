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

namespace LTUDQL2.Views
{
    /// <summary>
    /// Interaction logic for Banking.xaml
    /// </summary>
    public partial class Banking : Page
    {
        public Banking()
        {
            InitializeComponent();
        }

        string _email = "";
        public Banking(string email) : this()
        {
            _email = email;
            Renderinfo(email);
        }

        void Renderinfo(string email)
        {
            var objData = Models.DataProvider.Ins.DB.Users.Where(u => u.Email == email).SingleOrDefault();

            if (objData != null)
            {
                nameText.Text = objData.Name;
                bankText.Text = objData.PurchaseInfo;
                costText.Text = "70000";
            }
            else
                return;
        }

        private void Purchasebtn_Click(object sender, RoutedEventArgs e)
        {
            if(bankText.Text == "")
            {
                MessageBox.Show("Bạn Chưa Nhập Mã Tài Khoản!");
            }
            else
            {
                var user = DataProvider.Ins.DB.Users.Where(u => u.Email == _email).SingleOrDefault();
                int idUser = user.IDUser;
                DataProvider.Ins.DB.Purchases.Add(new Purchase()
                {
                    IDUser = idUser,
                    Cost = Int32.Parse(costText.Text),
                    DatePurchase = DateTime.Now,
                    DateExpire = DateTime.UtcNow.AddDays(30),
                });

                user.PurchaseInfo = bankText.Text;

                DataProvider.Ins.DB.SaveChanges();

                Models.DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thanh toán thành công! Xin mời bạn đăng nhập lại!", "Thông Báo!");
                Views.HomePage vm = new Views.HomePage();
                MainWindow ms = new MainWindow();
                ms.Show();
                vm.Close();
            }
          
        }
    }
}
