using LTUDQL2.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Page
    {
        public UserProfile()
        {
            InitializeComponent();
        }

        string _email = "";
        public UserProfile(string email) : this()
        {
            _email = email;
            emailText.Text = _email;
            Renderinfo(email);
            RenderPlayList(email);
        }

        void Renderinfo(string email)
        {
            var objData = DataProvider.Ins.DB.Users.Where(u => u.Email == email).SingleOrDefault();

            if (objData != null)
            {
                idText.Text = objData.IDUser.ToString();
                emailText.Text = objData.Email;
                nameText.Text = objData.Name;
                bankText.Text = objData.PurchaseInfo;
            }
            else
                return;
        }

        public BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {
            using (var stream = new MemoryStream(byteArrayIn))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();

                return image;
            }
        }

        void RenderPlayList(string email)
        {
            var user = DataProvider.Ins.DB.Users.Where(u => u.Email == email).SingleOrDefault();
            if (user != null)
            {
                int idUser = user.IDUser;
                List<int> idMovie = new List<int>();

                var objPlaylist = DataProvider.Ins.DB.UserMovies;
                foreach (var item in objPlaylist)
                {
                    if (item.IDUser == idUser)
                    {
                        idMovie.Add(item.IDMovie);
                    }
                }

                if (idMovie.Count() > 0)
                {
                    var objMovie = DataProvider.Ins.DB.Movies;
                    var listCate = DataProvider.Ins.DB.Categories;
                    Random rnd = new Random();
                    foreach (var item in objMovie)
                    {
                        var category = listCate.Where(c => c.IDCategory == item.IDCategory).SingleOrDefault();
                        var boolIndexof = idMovie.Where(l => l == item.IDMovie);
                        if (boolIndexof.Count() > 0)
                        {
                            bool randStatus = rnd.Next(100) <= 50 ? true : false;
                            int viewFake = rnd.Next(1000, 100000);
                            GroupBox gb = new GroupBox();
                            gb.Width = 340;
                            gb.Height = 200;
                            gb.Header = item.MovieName.Trim() + " - " + viewFake.ToString() + " view" + (randStatus == true ? " - (Đã Xem)" : "") + " == Thể loại: " + category.CategoryName;
                            Thickness margin = gb.Margin;
                            margin.Left = 16;
                            margin.Top = 16;
                            margin.Right = 16;
                            margin.Bottom = 16;
                            gb.Margin = margin;
                            gb.Cursor = Cursors.Hand;
                            MaterialDesignThemes.Wpf.ColorZoneAssist.SetMode(gb, ColorZoneMode.PrimaryDark);

                            //Image in GrBox 
                            ImageBrush img = new ImageBrush();
                            img.ImageSource = byteArrayToImage(item.PosterPath);
                            gb.Background = img;

                            Button btn = new Button();
                            btn.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Play };
                            btn.Width = 60;
                            btn.Background = Brushes.Red;
                            btn.BorderBrush = Brushes.White;
                            btn.Name = "btn" + item.IDMovie.ToString();
                            btn.ToolTip = "Play Video";

                            Button btnLater = new Button();
                            btnLater.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Delete };
                            btnLater.Width = 60;
                            btnLater.Background = Brushes.Red;
                            btnLater.BorderBrush = Brushes.White;
                            btnLater.Name = "btn" + item.IDMovie.ToString();
                            btnLater.ToolTip = "Xóa Khỏi DS";
                            btnLater.Click += BtnLater_Click;

                            StackPanel st = new StackPanel();
                            Thickness marginSt = st.Margin;
                            marginSt.Top = 40;
                            st.Margin = marginSt;

                            st.Children.Add(btn);
                            st.Children.Add(btnLater);

                            gb.Content = st;

                            btn.Click += Btn_Click; ;
                            listPlayList.Children.Add(gb);
                        }
                    }
                }
                else
                {
                    TextBlock txt = new TextBlock();
                    txt.FontSize = 40;
                    txt.FontWeight = FontWeights.Medium;
                    txt.Foreground = Brushes.White;
                    txt.Text = "Danh Sách PlayList Rỗng";
                    txt.TextAlignment = TextAlignment.Center;
                    Thickness margin = txt.Margin;
                    margin.Left = 20;
                    margin.Top = 20;
                    margin.Right = 20;
                    margin.Bottom = 20;

                    txt.Margin = margin;
                    listPlayList.Children.Add(txt);
                }
            }
            else
                return;
        }

        private void BtnLater_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (sender) as Button;
            string idM = btn.Name.Replace("btn", "");
            var user = DataProvider.Ins.DB.Users.Where(u => u.Email == _email).SingleOrDefault();

            int idU = user.IDUser;
            try
            {
                var userMovie = DataProvider.Ins.DB.UserMovies.Where(i => i.IDMovie.ToString() == idM && i.IDUser == idU).SingleOrDefault();

                DataProvider.Ins.DB.UserMovies.Remove(userMovie);
                DataProvider.Ins.DB.SaveChanges();
                listPlayList.Children.Clear();
                RenderPlayList(_email);
            }
            catch
            {
                MessageBox.Show("Error Database!", "Thông Báo!");
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (sender) as Button;
            string idM = btn.Name.Replace("btn", "");
            WacthMovie wtm = new WacthMovie(idM);
            wtm.ShowDialog();
        }

        void OpenText()
        {
            emailText.IsEnabled = true;
            nameText.IsEnabled = true;
            bankText.IsEnabled = true;
        }

        void CloseText()
        {
            emailText.IsEnabled = false;
            nameText.IsEnabled = false;
            bankText.IsEnabled = false;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            OpenText();
            updateBtn.IsEnabled = false;
            submitBtn.IsEnabled = true;
        }

        private void BackHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            Views.NewRegister.emailuserAc = emailText.Text;
            this.NavigationService.Navigate(new Uri("Views/NewRegister.xaml", UriKind.Relative));
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(idText.Text);
            var objData = Models.DataProvider.Ins.DB.Users.Where(u => u.IDUser == id).SingleOrDefault();

            if (objData != null)
            {
                objData.Email = emailText.Text;
                objData.Name = nameText.Text;
                objData.PurchaseInfo = bankText.Text;

                Models.DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Cập Nhật Thành Công!", "Thông Báo!");
                ViewModels.LoginViewModel vm = new ViewModels.LoginViewModel();
                vm.Email = emailText.Text;
                var parentForm = Window.GetWindow(this);
                Views.HomePage hpv = new Views.HomePage();
                hpv.emailUser.Text = emailText.Text;
                hpv.Show();
                parentForm.Close();
                CloseText();
                submitBtn.IsEnabled = false;
                updateBtn.IsEnabled = true;
            }
            else
            {
                CloseText();
                submitBtn.IsEnabled = false;
                updateBtn.IsEnabled = true;
                return;
            }
        }
    }
}
