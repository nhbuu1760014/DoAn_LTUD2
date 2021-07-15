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
    class UserProfileViewModel : BaseViewModel
    {
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _IDUser;
        public string IDUser { get => _IDUser; set { _IDUser = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _Bank;
        public string Bank { get => _Bank; set { _Bank = value; OnPropertyChanged(); } }

        public ICommand updateCommand { get; set; }

        public UserProfileViewModel()
        {
            updateCommand = new RelayCommand<Page>((prams) => { return true; }, (prams) => { updateInfo(prams); });
        }

        void updateInfo(Page prams)
        {
            if (prams == null)
                return;
            else
            {
                MessageBox.Show(IDUser + " " + Name + " " + Bank);
            }
        }
    }
}
