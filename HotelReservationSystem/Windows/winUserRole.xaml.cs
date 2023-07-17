using BAL;
using BAL.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


namespace HotelReservationSystem.Windows
{
    /// <summary>
    /// Interaction logic for winUserRole.xaml
    /// </summary>
    /// 

    public partial class winUserRole : Window
    {

        ObservableCollection<clsBMSRoleBAL> RolesCollection = new ObservableCollection<clsBMSRoleBAL>();
        ObservableCollection<clsBMSRoleBAL> UserRolesCollection = new ObservableCollection<clsBMSRoleBAL>();
        public int UserId = 0;
        public winUserRole()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteCollection = new ObservableCollection<clsBMSRoleBAL>(RolesCollection.Where(x => x.UserRoleID > 0));
                var usercollection = new ObservableCollection<clsBMSRoleBAL>(UserRolesCollection.Where(X => X.UserRoleID == 0));
                try
                {
                    if (MessageBox.Show("Do you want to save record?", "Confirmation",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        bool vbol = clsUserBMSRoleBAL.SaveLogic(usercollection, deleteCollection, UserId);
                        if (vbol)
                        {
                            MessageBox.Show("Record Save successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RolesCollection = clsBMSRoleBAL.GetBMSRole(UserId);
                listboxRoles.ItemsSource = RolesCollection;
  
                UserRolesCollection = clsBMSRoleBAL.GetBMSUserRole(UserId);
                ListboxChildRoles.ItemsSource = UserRolesCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listboxRoles.SelectedItems == null && listboxRoles.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please Select Roles", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                for (int i = 0; i < listboxRoles.SelectedItems.Count; i++)
                {
                    var _clsBMSRoleBAL = (clsBMSRoleBAL)listboxRoles.SelectedItems[i];
                    UserRolesCollection.Add(_clsBMSRoleBAL);
                    RolesCollection.Remove(_clsBMSRoleBAL);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListboxChildRoles.SelectedItems == null && ListboxChildRoles.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please Select Roles", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                for (int i = 0; i < ListboxChildRoles.SelectedItems.Count; i++)
                {
                    var _clsBMSRoleBAL = (clsBMSRoleBAL)ListboxChildRoles.SelectedItems[i];
                    RolesCollection.Add(_clsBMSRoleBAL);
                    UserRolesCollection.Remove(_clsBMSRoleBAL);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
