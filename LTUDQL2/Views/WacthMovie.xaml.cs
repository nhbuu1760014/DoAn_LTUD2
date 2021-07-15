using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
    /// Interaction logic for WacthMovie.xaml
    /// </summary>
    public partial class WacthMovie : Window
    {
        private bool userIsDraggingSlider = false;
        DispatcherTimer timer = new DispatcherTimer();
        public WacthMovie()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            movie.MediaOpened += Movie_MediaOpened;
        }

        private string _idmovie = "";
        public WacthMovie(string idMovies) : this()
        {
            _idmovie = idMovies;
        }

        void RenderVideo(string idmovie)
        {
            ViewModels.ListMovieViewModel vm = new ViewModels.ListMovieViewModel();
            var list = vm.List;

            var item = list.Where(u => u.IDMovie.ToString() == idmovie).SingleOrDefault();
            movie.Source = new Uri(@"" + item.MoviePath, UriKind.Relative);
        }
      

        string strTime = "";
        private void Movie_MediaOpened(object sender, RoutedEventArgs e)
        {
            var totalDurationTime = movie.NaturalDuration.TimeSpan.TotalSeconds;
            TimeSpan time = TimeSpan.FromSeconds(totalDurationTime);
            strTime = time.ToString(@"hh\:mm\:ss");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((movie.Source != null) && (movie.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = movie.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = movie.Position.TotalSeconds;
            }
        }
        int count = 10;
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            RenderVideo(_idmovie);
            movie.Play();
            btnPlay.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Visible;
            sliderVol.Visibility = Visibility.Visible;
            sliProgress.Visibility = Visibility.Visible;
            btnStop.Opacity = 0;
        }

        private void BtnStop_MouseEnter(object sender, MouseEventArgs e)
        {
            btnStop.Opacity = 1;
        }

        private void BtnStop_MouseLeave(object sender, MouseEventArgs e)
        {
            btnStop.Opacity = 0;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            movie.Pause();
            btnPlay.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Hidden;
        }

        private void SliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
            if (userIsDraggingSlider == false)
            {
                movie.Position = TimeSpan.FromSeconds(sliProgress.Value);
            }
            if(strTime == lblProgressStatus.Text)
            {
                btnReset.Visibility = Visibility.Visible;
                btnStop.Visibility = Visibility.Hidden;
            }
        }

        private void SliProgress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {

            userIsDraggingSlider = true;
        }

        private void SliProgress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            movie.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            sliderVol.Visibility = Visibility.Visible;
            sliProgress.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            sliderVol.Visibility = Visibility.Hidden;
            sliProgress.Visibility = Visibility.Hidden;
        }
        bool checkSpace = false;

        public object Urikind { get; private set; }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (checkSpace == false)
                {
                    checkSpace = true;
                    movie.Pause();
                    btnPlay.Visibility = Visibility.Visible;
                    btnStop.Visibility = Visibility.Hidden;
                }
                else
                {
                    checkSpace = false;
                    movie.Play();
                    btnPlay.Visibility = Visibility.Hidden;
                    btnStop.Visibility = Visibility.Visible;
                    sliderVol.Visibility = Visibility.Visible;
                    sliProgress.Visibility = Visibility.Visible;
                    btnStop.Opacity = 0;
                }
            }
            if (e.Key == Key.Right)
            {
                if (userIsDraggingSlider == false)
                {
                    movie.Position = TimeSpan.FromSeconds(sliProgress.Value + 10);
                }
            }
            if (e.Key == Key.Left)
            {
                if (userIsDraggingSlider == false)
                {
                    movie.Position = TimeSpan.FromSeconds(sliProgress.Value - 10);
                }
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            movie.Play();
            sliProgress.Value = 0;
            movie.Position = TimeSpan.FromSeconds(sliProgress.Value);
            btnReset.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Visible;
            btnStop.Opacity = 0;
        }

        private void BtnFullscreen_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            btnFullscreen.Visibility = Visibility.Hidden;
            btnFullscreenExit.Visibility = Visibility.Visible;
            Thickness marginScreen = btnFullscreenExit.Margin;
            marginScreen.Top = 790;
            btnFullscreenExit.Margin = marginScreen;
            movie.Height = 840;
        }

        private void BtnFullscreenExit_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            btnFullscreen.Visibility = Visibility.Visible;
            btnFullscreenExit.Visibility = Visibility.Hidden;
            movie.Stretch = Stretch.Fill;
            movie.Height = 670;
            Thickness marginScreen = btnFullscreenExit.Margin;
            marginScreen.Top = 620;
            btnFullscreenExit.Margin = marginScreen;
        }
    }
}
