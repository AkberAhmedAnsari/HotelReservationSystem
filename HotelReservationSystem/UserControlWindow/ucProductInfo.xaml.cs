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
    public partial class ucProductInfo : UserControl
    {
        public ucProductInfo()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winProductInfo win = new winProductInfo();
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
                if (gridviewProduct.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                winProductInfo win = new winProductInfo();
                clsProductsInfoBAL _clsProductsInfoBAL = gridviewProduct.SelectedItem as clsProductsInfoBAL;
                win._clsProductsInfoBAL = _clsProductsInfoBAL;
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
                if (gridviewProduct.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to delete record?", "Confirmation", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsProductsInfoBAL _clsProductsInfoBAL = gridviewProduct.SelectedItem as clsProductsInfoBAL;
                    bool vob = clsProductsInfoBAL.DeleteProduct(_clsProductsInfoBAL.productItemId);
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
            var list = clsProductsInfoBAL.getRecords();
            gridviewProduct.ItemsSource = list;
        }
    }
}
