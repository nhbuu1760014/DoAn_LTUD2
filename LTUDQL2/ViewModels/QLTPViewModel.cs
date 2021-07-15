using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LTUDQL2.Models;

namespace LTUDQL2.ViewModels
{
    public class QLTPViewModel : DependencyObject
    {
        ObservableCollection<Movy> movies;

        public static readonly DependencyProperty DSPhimProperty;

        static QLTPViewModel()
        {
            DSPhimProperty = DependencyProperty.Register("DSPhim", typeof(ObservableCollection<Movy>), typeof(QLTPViewModel));
        }

        public ObservableCollection<Movy> DSPhim
        {
            get => (ObservableCollection<Movy>)GetValue(DSPhimProperty);
            set => SetValue(DSPhimProperty, value);
        }

        public QLTPViewModel()
        {
            using (var qlp = new QuanLyTrangPhimEntities1())
            {
                DSPhim = new ObservableCollection<Movy>(qlp.Movies.Include("MovieName").ToList());
            }
        }
    }
}
