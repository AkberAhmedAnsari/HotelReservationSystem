using HotelReservationSystem.MainWindows;
using BAL;
using BAL.Classes;
using DAL;
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

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtlogin.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter login Id", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (txtpassword.Password.Trim() == "")
                {
                    MessageBox.Show("Please enter Password", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var user = clsUserBMSBAL.GetUser(txtlogin.Text, txtpassword.Password);
                if (user == null || user.userid == 0)
                {
                    MessageBox.Show("Invalid Login Id and Password", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (user.loginId != txtlogin.Text || user.password != txtpassword.Password)
                {
                    MessageBox.Show("Invalid Login Id and Password", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                clsAppObject.LoginUser = user;
                ShowWindow();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowWindow()
        {
            MainWindows.winMainManu main = new winMainManu();
            this.Hide();
            main.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtlogin.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
