using LTUDQL2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LTUDQL2.ViewModels
{
    public class ListMovieViewModel : BaseViewModel
    {
        private ObservableCollection<Movy> _List;
        public ObservableCollection<Movy> List { get => _List; set { _List = value; OnPropertyChanged(); }}

        private ObservableCollection<Category> _ListCate;
        public ObservableCollection<Category> ListCate { get => _ListCate; set { _ListCate = value; OnPropertyChanged(); } }
        public ICommand RenderInfoCommand { get; set; }

        public ListMovieViewModel()
        {
            List = new ObservableCollection<Movy>(DataProvider.Ins.DB.Movies);
            ListCate = new ObservableCollection<Category>(DataProvider.Ins.DB.Categories);
        }
       

    }
}
