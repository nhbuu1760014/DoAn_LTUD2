using LTUDQL2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LTUDQL2.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        private ObservableCollection<User> _List;
        public ObservableCollection<User> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private int _IDUser { get; set; }
        public int IDUser { get => _IDUser; set { _IDUser = value; OnPropertyChanged(); } }
        private string _Email { get; set; }
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _Password { get; set; }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private int _Role { get; set; }
        public int Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }
        private string _PurchaseInfo { get; set; }
        public string PurchaseInfo { get => _PurchaseInfo; set { _PurchaseInfo = value; OnPropertyChanged(); } }

        private User _SelectedItem;
        public User SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name = SelectedItem.Name;
                    Email = SelectedItem.Email;
                    Role = SelectedItem.Role;
                    PurchaseInfo = SelectedItem.PurchaseInfo;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public UserViewModel()
        {
            List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var User = new User() { Name = Name, Email = Email, Role = Role, PurchaseInfo = PurchaseInfo};

                DataProvider.Ins.DB.Users.Add(User);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(User);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Users.Where(x => x.IDUser == SelectedItem.IDUser);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var User = DataProvider.Ins.DB.Users.Where(x => x.IDUser == SelectedItem.IDUser).SingleOrDefault();
                User.Name = Name;
                User.Email = Email;
                User.Role = Role;
                User.PurchaseInfo = PurchaseInfo;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Name = Name;
            });
        }
    }
}
