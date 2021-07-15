using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace LTUDQL2.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        DispatcherTimer timer;
        int controlButton = 0;
        public Register()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(1);
            timer.Tick += Timer_Tick;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(controlButton == 0)
            {
                form_1.Width -= 10;
                if (form_1.Width <= 0)
                {
                    controlButton = 1;
                    timer.Stop();
                }
            }
            else if(controlButton == 1)
            {
                form_1.Width += 10;
                if (form_1.Width >= 1500)
                {
                    controlButton = 0;
                    timer.Stop();
                }
            }
            else if(controlButton == 2)
            {
                form_2.Width -= 10;
                if (form_2.Width <= 0)
                {
                    controlButton = 3;
                    timer.Stop();
                }
            }
            else if (controlButton == 3)
            {
                form_2.Width += 10;
                if (form_2.Width >= 1500)
                {
                    controlButton = 2;
                    timer.Stop();
                }
            }
            else if (controlButton == 4)
            {
                form_3.Width -= 10;
                if (form_3.Width <= 0)
                {
                    controlButton = 3;
                    timer.Stop();
                }
            }
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

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if(emailText.Text == "")
            {
                messageErrorText.Text = "Bạn cần nhập địa chỉ Email!";
            }
            else if(isValidEmail(emailText.Text) == false)
            {
                messageErrorText.Text = "Đây phải là Email!";
            }
            else
            {
                messageErrorText.Text = "";
                timer.Start();
            }
        }

        private void BackForm1_Click(object sender, RoutedEventArgs e)
        {
            controlButton = 1;
            timer.Start();
        }

        private void NextForm3_Click(object sender, RoutedEventArgs e)
        {
            controlButton = 2;
            timer.Start();
        }

        private void BackForm2_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void NextForm4_Click(object sender, RoutedEventArgs e)
        {
            if(passwordText.Password == "")
            {
                messageErrorPass.Text = "Mật khẩu không được trống!";
            }
            else if(passwordText.Password.Length < 8)
            {
                messageErrorPass.Text = "Mật khẩu phải chứa ít nhất 8 kí tự!";
            }
            else
            {
                controlButton = 4;
                timer.Start();
            }
        }
    }
}
