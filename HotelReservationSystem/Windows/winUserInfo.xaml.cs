using BAL.Classes;
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
using System.Windows.Shapes;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace HotelReservationSystem.Windows
{
    /// <summary>
    /// Interaction logic for winProductInfo.xaml
    /// </summary>
    public partial class winUserInfo : Window
    {
        public clsUserBMSBAL _clsUserBMSBAL = new clsUserBMSBAL();
        public winUserInfo()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clearRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _clsUserBMSBAL.password = txtPassword.Password;
                    _clsUserBMSBAL._ConfirmPassword = txtconfirmPassword.Password;
                    bool vbol = clsUserBMSBAL.SaveLogic(_clsUserBMSBAL);
                    if (vbol)
                    {
                        MessageBox.Show("Record Save successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearRecord();
                    }
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
                CbUserType.ItemsSource = UserTypeBAL.getUserType();
                _clsUserBMSBAL.UserTypeId = 2;
                this.DataContext = _clsUserBMSBAL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearRecord()
        {
            _clsUserBMSBAL = new clsUserBMSBAL();
            txtPassword.Password = "";
            txtconfirmPassword.Password = "";
            DataContext = _clsUserBMSBAL;
        }

        private void btnNew_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
