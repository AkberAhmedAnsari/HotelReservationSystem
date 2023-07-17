using BAL.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for winCustomerInformation.xaml
    /// </summary>
    public partial class winCustomerInformation : Window
    {
        public clsCustomerBAL objCustomerBAL = new clsCustomerBAL();
        ObservableCollection<clsCityBAL> CityList = null;
        public winCustomerInformation()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    bool vbol = clsCustomerBAL.SaveLogic(objCustomerBAL);
                    if (vbol)
                    {
                        MessageBox.Show("Record save successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// All data get In controll
        /// </summary>
        private void fillDataInClassObject()
        {
            try
            {
                fillComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillComboBox()
        {
            var List = clsCustomerTypeBAL.getCoustomerType();
            CbClientType.ItemsSource = List;

            var CountryList = clsCountryBAL.GetAllCountry();
            CbCountry.ItemsSource = CountryList;

            CityList = clsCityBAL.GetAllCity();
            CbCity.ItemsSource = CityList;

            var TitleList = clsTitleBAL.getTitle();
            Cbtitle.ItemsSource = TitleList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = objCustomerBAL;
                fillComboBox();
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
                clearRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearRecord()
        {
            objCustomerBAL = new clsCustomerBAL();
            this.DataContext = objCustomerBAL;
        }

        private void CbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(CbCountry.SelectedItem != null)
                {
                    clsCountryBAL _clsCountryBAL = CbCountry.SelectedItem as clsCountryBAL;
                    if (CityList != null && CityList.Count > 0)
                        CbCity.ItemsSource = CityList.Where(x => x.CountryId == _clsCountryBAL.CountryId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddCity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowCity _WindowCity = new WindowCity();
                _WindowCity.ShowDialog();
                CityList = clsCityBAL.GetAllCity();
                CbCity.ItemsSource = CityList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowCountry _WindowCountry = new WindowCountry();
                _WindowCountry.ShowDialog();
                var CountryList = clsCountryBAL.GetAllCountry();
                CbCountry.ItemsSource = CountryList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
