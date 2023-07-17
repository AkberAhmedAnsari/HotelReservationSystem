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
    /// Interaction logic for winPointOfSale.xaml
    /// </summary>
    public partial class winPointOfSale : Window
    {
        public ObservableCollection<clsSaleInvoiceItemBAL> CollectionSaleInvoiceItem = new ObservableCollection<clsSaleInvoiceItemBAL>();
        public clsSaleInvoiceBAL _clsSaleInvoiceBAL = new clsSaleInvoiceBAL();
        public bool IsEdit = false; 
        public winPointOfSale()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPointOfSAle.CollectionSaleInvoiceItem = CollectionSaleInvoiceItem;
                ucPointOfSAle._clsSaleInvoiceBAL = _clsSaleInvoiceBAL;
                ucPointOfSAle.IsEdit = IsEdit;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
