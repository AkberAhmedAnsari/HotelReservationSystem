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
using System.Windows.Shapes;

namespace HotelReservationSystem.Windows
{
    /// <summary>
    /// Interaction logic for winCustomerList.xaml
    /// </summary>
    public partial class winCustomerList : Window
    {
        public clsCustomerBAL Customer = null;
        public winCustomerList()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SetCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridviewCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SetCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetCustomer()
        {
            if (gridviewCustomer.SelectedItem == null)
            {
                MessageBox.Show("Please Select Customer", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Customer = (clsCustomerBAL)gridviewCustomer.SelectedItem;
            this.Close();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                getrecord();
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
                if (gridviewCustomer.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                winCustomerInformation win = new winCustomerInformation();
                clsCustomerBAL Customer = gridviewCustomer.SelectedItem as clsCustomerBAL;
                win.objCustomerBAL = Customer;
                win.ShowDialog();
                getrecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winCustomerInformation win = new winCustomerInformation();
                win.ShowDialog();
                getrecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void getrecord()
        {
            var collection = clsCustomerBAL.getRecords();
            gridviewCustomer.ItemsSource = collection;
        }
    }
}
