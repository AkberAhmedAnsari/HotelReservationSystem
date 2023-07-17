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
    public partial class winProductCatagory : Window
    {
        public clsCategoryInfoBAL _clsCategoryInfoBAL = new clsCategoryInfoBAL();
        public winProductCatagory()
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
                    bool vbol = clsCategoryInfoBAL.SaveLogic(_clsCategoryInfoBAL);
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
                this.DataContext = _clsCategoryInfoBAL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearRecord()
        {
            _clsCategoryInfoBAL = new clsCategoryInfoBAL();
            DataContext = _clsCategoryInfoBAL;
        }
    }
}
