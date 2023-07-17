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
    public partial class ucProductCatagory : UserControl
    {
        public ucProductCatagory()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winProductCatagory win = new winProductCatagory();
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
                winProductCatagory win = new winProductCatagory();
                clsCategoryInfoBAL _clsCategoryInfoBAL = gridviewProduct.SelectedItem as clsCategoryInfoBAL;
                win._clsCategoryInfoBAL = _clsCategoryInfoBAL;
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
                    clsCategoryInfoBAL _clsProductsInfoBAL = gridviewProduct.SelectedItem as clsCategoryInfoBAL;
                    bool vob = clsCategoryInfoBAL.DeleteCategory(_clsProductsInfoBAL.CategoryId);
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
            var list = clsCategoryInfoBAL.getRecords();
            gridviewProduct.ItemsSource = list;
        }
    }
}
