using BAL.Classes;
using HotelReservationSystem.Windows;
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
    /// Interaction logic for ucRooms.xaml
    /// </summary>
    public partial class ucCountry : UserControl
    {
        public ucCountry()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowCountry _WindowCountry = new WindowCountry();
                _WindowCountry.ShowDialog();
                getrecord();
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
                if (gridviewCountry.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                WindowCountry _WindowCountry = new WindowCountry();
                _WindowCountry._clsCountryBAL = gridviewCountry.SelectedItem as clsCountryBAL;
                _WindowCountry.ShowDialog();
                getrecord();
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
                if (gridviewCountry.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to delete record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsCountryBAL _clsCountryBAL = gridviewCountry.SelectedItem as clsCountryBAL;
                    bool vbol = clsCountryBAL.DeleteCountry(_clsCountryBAL.CountryId);
                    if (vbol)
                    {
                        MessageBox.Show("Record deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        getrecord();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getrecord()
        {
            var collection = clsCountryBAL.GetAllCountry();
            gridviewCountry.ItemsSource = collection;
        }
    }
}
