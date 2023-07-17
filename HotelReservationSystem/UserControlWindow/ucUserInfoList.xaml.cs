using HotelReservationSystem.Windows;
using BAL.Classes;
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

namespace HotelReservationSystem.UserControlWindow
{
    /// <summary>
    /// Interaction logic for ucProductInfo.xaml
    /// </summary>
    public partial class ucUserInfoList : UserControl
    {
        public ucUserInfoList()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winUserInfo win = new winUserInfo();
                win.ShowDialog();
                GetRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridviewUser.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                winUserInfo win = new winUserInfo();
                clsUserBMSBAL _clsUserBMSBAL = gridviewUser.SelectedItem as clsUserBMSBAL;
                win._clsUserBMSBAL = _clsUserBMSBAL;
                win.ShowDialog();
                GetRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridviewUser.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to delete record?", "Confirmation", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsUserBMSBAL _clsUserBMSBAL = gridviewUser.SelectedItem as clsUserBMSBAL;
                    bool vob = clsUserBMSBAL.DeleteCategory(_clsUserBMSBAL.userid);
                    if (vob)
                    {
                        MessageBox.Show("Record deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btngetRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetRecord()
        {
            var list = clsUserBMSBAL.getRecords();
            gridviewUser.ItemsSource = list;
        }

        private void btnRoleAssign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridviewUser.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                clsUserBMSBAL _clsUserBMSBAL = gridviewUser.SelectedItem as clsUserBMSBAL;
                winUserRole _winUserRole = new winUserRole();
                _winUserRole.UserId = _clsUserBMSBAL.userid;
                _winUserRole.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
