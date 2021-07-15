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
    /// Interaction logic for NewRegister.xaml
    /// </summary>
    public partial class NewRegister : Page
    {
        public NewRegister()
        {
            InitializeComponent();
            LoadedWrapper();
            GetDataVM();
        }
        public static string emailuserAc = "";
        public string emailuser;
        public NewRegister(string emailUser) : this()
        {
            emailuser = emailUser;
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
    
        void GetDataVM()
        {
            ViewModels.ListMovieViewModel vm = new ViewModels.ListMovieViewModel();
            var list = vm.List;
            var listCate = vm.ListCate;
            if (list.Count() > 0)
            {
                Random rnd = new Random();
                var result = list.OrderBy(item => rnd.Next());
                foreach (var item in result)
                {
                    var category = listCate.Where(c => c.IDCategory == item.IDCategory).SingleOrDefault();
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
                    gb.MouseEnter += Gb_MouseEnter;
                    gb.MouseLeave += Gb_MouseLeave;

                    Button btn = new Button();
                    btn.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Play };
                    btn.Width = 60;
                    btn.Background = Brushes.Red;
                    btn.BorderBrush = Brushes.White;
                    btn.Name = "btn" + item.IDMovie.ToString();
                    btn.ToolTip = "Play Video";

                    Button btnLater = new Button();
                    btnLater.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Clock };
                    btnLater.Width = 60;
                    btnLater.Background = Brushes.Red;
                    btnLater.BorderBrush = Brushes.White;
                    btnLater.Name = "btn" + item.IDMovie.ToString();
                    btnLater.ToolTip = "Xem Sau";
                    btnLater.Click += BtnLater_Click; 

                    StackPanel st = new StackPanel();
                    Thickness marginSt = st.Margin;
                    marginSt.Top = 40;
                    st.Margin = marginSt;

                    st.Children.Add(btn);
                    st.Children.Add(btnLater);

                    gb.Content = st;

                    btn.Click += Btn_Click;
                    popularNetflix.Children.Add(gb);
                }
            }
            else
            {
                TextBlock txt = new TextBlock();
                txt.FontSize = 40;
                txt.FontWeight = FontWeights.Medium;
                txt.Foreground = Brushes.White;
                txt.Text = "Không có phim nào hiện tại lọt Top trending trên NETFLIX";
                Thickness margin = txt.Margin;
                margin.Left = 20;
                margin.Top = 20;
                margin.Right = 20;
                margin.Bottom = 20;

                txt.Margin = margin;
                popularNetflix.Children.Add(txt);
            }
        }

        private void BtnLater_Click(object sender, RoutedEventArgs e)
        {
            var user = DataProvider.Ins.DB.Users.Where(u => u.Email == emailuserAc).SingleOrDefault();

            Button btn = (sender) as Button;
            string idM = btn.Name.Replace("btn", "");
            int idU = user.IDUser;
            try
            {
                var userMovie = DataProvider.Ins.DB.UserMovies.Where(i => i.IDMovie.ToString() == idM && i.IDUser == idU);
                if(userMovie.Count() > 0)
                {
                    MessageBox.Show("Video đã được thêm vào DS xem sau!");
                }
                else
                {
                    DataProvider.Ins.DB.UserMovies.Add(new UserMovy()
                    {
                        IDMovie = Int32.Parse(idM),
                        IDUser = idU,
                    });
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đã Thêm!");
                }
               
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

        void LoadedWrapper()
        {
            ViewModels.ListMovieViewModel vm = new ViewModels.ListMovieViewModel();
            var list = vm.List;

            var listCate = vm.ListCate;
            if(list.Count() > 0)
            {
                Random rnd = new Random();
                var result = list.OrderBy(item => rnd.Next());
                foreach (var item in result)
                {
                    var category = listCate.Where(c => c.IDCategory == item.IDCategory).SingleOrDefault();
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
                    gb.MouseEnter += Gb_MouseEnter;
                    gb.MouseLeave += Gb_MouseLeave;

                    Button btn = new Button();
                    btn.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Play };
                    btn.Width = 60;
                    btn.Background = Brushes.Red;
                    btn.BorderBrush = Brushes.White;
                    btn.ToolTip = "Play Video";
                    btn.Name = "btn" + item.IDMovie.ToString();
                    btn.Click += Btn_Click;

                    Button btnLater = new Button();
                    btnLater.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Clock };
                    btnLater.Width = 60;
                    btnLater.Background = Brushes.Red;
                    btnLater.BorderBrush = Brushes.White;
                    btnLater.Name = "btn" + item.IDMovie.ToString();
                    btnLater.ToolTip = "Xem Sau";

                    StackPanel st = new StackPanel();
                    Thickness marginSt = st.Margin;
                    marginSt.Top = 40;
                    st.Margin = marginSt;

                    st.Children.Add(btn);
                    st.Children.Add(btnLater);

                    gb.Content = st;
                    btnLater.Click += BtnLater_Click;
                    trendingNetflix.Children.Add(gb);
                }
            }
            else
            {
                TextBlock txt = new TextBlock();
                txt.FontSize = 40;
                txt.FontWeight = FontWeights.Medium;
                txt.Foreground = Brushes.White;
                txt.Text = "Không có phim nào hiện tại lọt Top trending trên NETFLIX";
                Thickness margin = txt.Margin;
                margin.Left = 20;
                margin.Top = 20;
                margin.Right = 20;
                margin.Bottom = 20;

                txt.Margin = margin;
                trendingNetflix.Children.Add(txt);
            }
            
        }
        

        private void Gb_MouseLeave(object sender, MouseEventArgs e)
        {
            GroupBox gb = (sender) as GroupBox;
            MaterialDesignThemes.Wpf.ColorZoneAssist.SetMode(gb, ColorZoneMode.PrimaryDark);
            gb.Background.Opacity = 1;
        }

        private void Gb_MouseEnter(object sender, MouseEventArgs e)
        {
            GroupBox gb = (sender) as GroupBox;
            MaterialDesignThemes.Wpf.ColorZoneAssist.SetMode(gb, ColorZoneMode.PrimaryLight);
            gb.Background.Opacity = 0.75;
        }
    }
}
