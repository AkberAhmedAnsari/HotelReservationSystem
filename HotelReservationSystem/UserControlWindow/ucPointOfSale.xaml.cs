using HotelReservationSystem.Windows;
using BAL;
using BAL.Classes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace HotelReservationSystem.UserControlWindow
{
    /// <summary>
    /// Interaction logic for ucPointOfSale.xaml
    /// </summary>
    public partial class ucPointOfSale : UserControl
    {
       public ObservableCollection<clsSaleInvoiceItemBAL> CollectionSaleInvoiceItem = new ObservableCollection<clsSaleInvoiceItemBAL>();
       ObservableCollection<clsSaleInvoiceItemBAL> DeleteSaleInvoiceItems = new ObservableCollection<clsSaleInvoiceItemBAL>();
       public clsSaleInvoiceBAL _clsSaleInvoiceBAL = new clsSaleInvoiceBAL();
       public bool IsEdit = false;
        public ucPointOfSale()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var List = clsCategoryInfoBAL.getCategoryInfo();
                ListProductCategory.ItemsSource = List;
                GridViewProduct.ItemsSource = CollectionSaleInvoiceItem;
                _clsSaleInvoiceBAL.IsOrder = true;
                _clsSaleInvoiceBAL.InvoiceDate = DateTime.Now;
                GridProductMaster.DataContext = _clsSaleInvoiceBAL;
                if (IsEdit)
                {
                    rbClientName.Content = _clsSaleInvoiceBAL.ClientName;
                    rbAddress.Content = _clsSaleInvoiceBAL.Address;
                    SetFoter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBackPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveLogic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveLogic()
        {
            _clsSaleInvoiceBAL.SaleTypeId = 1;
            if (SaveValadation() == false)
            {
                return;
            }
            winSaleInovicePayment _winSaleInovicePayment = new winSaleInovicePayment();
            _winSaleInovicePayment._clsSaleInvoiceBAL = _clsSaleInvoiceBAL;
            _winSaleInovicePayment.ShowDialog();
            if (_winSaleInovicePayment._clsSaleInvoicePaymentModeBAL != null)
            {
                bool isSave = false;
                if (rdbOrder.IsChecked == true)
                {
                    var _clsSaleInvoicePaymentModeBAL = _winSaleInovicePayment._clsSaleInvoicePaymentModeBAL;
                    if (_clsSaleInvoiceBAL.IsRefOrderInvoice)
                        isSave = clsSaleInvoiceBAL.SaveLogic(_clsSaleInvoiceBAL, CollectionSaleInvoiceItem, _clsSaleInvoicePaymentModeBAL,DeleteSaleInvoiceItems, IsEdit, 1);
                    else
                        isSave = clsSaleInvoiceBAL.SaveLogic(_clsSaleInvoiceBAL, CollectionSaleInvoiceItem, _clsSaleInvoicePaymentModeBAL, DeleteSaleInvoiceItems, IsEdit, 2);
                }
                else
                {
                    var _clsSaleInvoicePaymentModeBAL = _winSaleInovicePayment._clsSaleInvoicePaymentModeBAL;
                    isSave = clsSaleInvoiceBAL.SaveLogic(_clsSaleInvoiceBAL, CollectionSaleInvoiceItem,  _clsSaleInvoicePaymentModeBAL, DeleteSaleInvoiceItems, IsEdit, 1);
                }

                if (isSave == false)
                {
                    MessageBox.Show("Record Not Save.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (rdbOrder.IsChecked == true && _clsSaleInvoiceBAL.IsRefOrderInvoice == false)
                    {
                        PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Customer Slip");
                        PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Order Slip");
                        PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Original Slip");
                    }
                    else
                    {
                        PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Final Slip");
                    }
                    ClearLogic();
                }

            }
        }

        private bool SaveValadation()
        {
            if (_clsSaleInvoiceBAL.InvoiceDate == null)
            {
                MessageBox.Show("Please Select Invoice Date", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (_clsSaleInvoiceBAL.CustomerId == 0)
            {
                MessageBox.Show("Please Select Client.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (rdbOrder.IsChecked == true && _clsSaleInvoiceBAL.DeliveryDate == null)
            {
                MessageBox.Show("Please Select Delivery Date.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (CollectionSaleInvoiceItem == null && CollectionSaleInvoiceItem.Count > 0)
            {
                MessageBox.Show("Please enter product.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;

        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btncat = sender as RadButton;
                if (btncat != null)
                {
                    var _clsCategoryInfoBAL = btncat.DataContext as clsCategoryInfoBAL;
                    if (_clsCategoryInfoBAL != null)
                        ListProductItem.ItemsSource = clsProductsInfoBAL.GetProductItemByCategoryId(_clsCategoryInfoBAL.CategoryId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProductItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btnProduct = sender as RadButton;
                if (btnProduct != null)
                {
                    var _clsProductsInfoBAL = btnProduct.DataContext as clsProductsInfoBAL;
                    if (_clsProductsInfoBAL != null)
                    {
                        AddButtonLogic(_clsProductsInfoBAL);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddButtonLogic(clsProductsInfoBAL _clsProductsInfoBAL)
        {
            clsSaleInvoiceItemBAL _SaleInvoiceItemBAL = new clsSaleInvoiceItemBAL();
            _SaleInvoiceItemBAL.Barcode = _clsProductsInfoBAL.barcode;
            _SaleInvoiceItemBAL.ProductItemName = _clsProductsInfoBAL.productItemname;
            _SaleInvoiceItemBAL.ProductItemId = _clsProductsInfoBAL.productItemId;
            _SaleInvoiceItemBAL.Price = _clsProductsInfoBAL.saleprice;
            _SaleInvoiceItemBAL.Quantity = 1;
            CollectionSaleInvoiceItem.Add(_SaleInvoiceItemBAL);
            SetFoter();
        }

        private void SetFoter()
        {
            if (CollectionSaleInvoiceItem != null)
            {
                _clsSaleInvoiceBAL.NetAmount = CollectionSaleInvoiceItem.Sum(x => x.NetAmount);
                _clsSaleInvoiceBAL.TotalItemDiscount = CollectionSaleInvoiceItem.Sum(x => x.DiscountAmount);
                _clsSaleInvoiceBAL.ItemCount = CollectionSaleInvoiceItem.Count;
                GridForter.DataContext = _clsSaleInvoiceBAL;
            }
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            lblValue.Content = "";
        }

        private void btnBackprice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setValueInItem("Price");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBackdisc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setValueInItem("Disc.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBackqty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setValueInItem("QTY");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button _RadButton = sender as Button;
                if (_RadButton != null)
                {
                    if (_RadButton.Content.ToString() == "." && lblValue.Content.ToString().Contains(_RadButton.Content.ToString()))
                        return;

                    lblValue.Content += _RadButton.Content.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setValueInItem(string btnName)
        {
            if (GridViewProduct.SelectedItem == null)
            {
                MessageBox.Show("Please select Item.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_clsSaleInvoiceBAL.IsRefOrderInvoice)
            {
                MessageBox.Show("Not Allow to Change Order " + btnName, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (lblValue.Content.ToString() == "" || lblValue.Content.ToString() == "0." || lblValue.Content.ToString() == ".")
            {
                MessageBox.Show("In Valid Value.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                lblValue.Content = "";
                return;
            }

            var SelectedItem = (clsSaleInvoiceItemBAL)GridViewProduct.SelectedItem;
            if (btnName == "QTY")
            {
                SelectedItem.Quantity = Convert.ToDecimal(lblValue.Content.ToString());
                lblValue.Content = "";
            }
            else if (btnName == "Price")
            {
                SelectedItem.Price = Convert.ToDecimal(lblValue.Content.ToString());
                lblValue.Content = "";
            }
            else if (btnName == "Disc.")
            {
                SelectedItem.DiscountAmount = Convert.ToDecimal(lblValue.Content.ToString());
                lblValue.Content = "";
            }
            else if (btnName == "DiscPer")
            {
                SelectedItem.DiscountPer = Convert.ToDecimal(lblValue.Content.ToString());
                lblValue.Content = "";
            }
            SelectedItem.NetAmount = SelectedItem.NetAmount;
            SetFoter();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GridViewProduct.SelectedItem == null)
                {
                    MessageBox.Show("Please select Item.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to delete record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    var SelectedItem = (clsSaleInvoiceItemBAL)GridViewProduct.SelectedItem;
                    CollectionSaleInvoiceItem.Remove(SelectedItem);
                    if (IsEdit)
                        DeleteSaleInvoiceItems.Add(SelectedItem);
                    SetFoter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rbClientName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientLoginc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClientLoginc()
        {
            winCustomerList _WinCustomerList = new winCustomerList();
            _WinCustomerList.ShowDialog();
            if (_WinCustomerList.Customer != null)
            {
                rbClientName.Content = _WinCustomerList.Customer.FirstName + " " + _WinCustomerList.Customer.LastName;
                rbAddress.Content = _WinCustomerList.Customer.Address + " " + _WinCustomerList.Customer.CityName + " " + _WinCustomerList.Customer.CountryName;
                _clsSaleInvoiceBAL.CustomerId = _WinCustomerList.Customer.CustomerId;
            }
            else
            {
                rbClientName.Content = "Client Name";
                rbAddress.Content = "";
                _clsSaleInvoiceBAL.CustomerId = 0;
            }
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearLogic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearLogic()
        {
            if (CollectionSaleInvoiceItem != null)
                CollectionSaleInvoiceItem.Clear();
            if (DeleteSaleInvoiceItems != null)
                DeleteSaleInvoiceItems.Clear();
            _clsSaleInvoiceBAL = new clsSaleInvoiceBAL();
            _clsSaleInvoiceBAL.InvoiceDate = DateTime.Now;
            _clsSaleInvoiceBAL.DeliveryDate = null;
            _clsSaleInvoiceBAL.IsOrder = true;
            rbClientName.Content = "Select Client than Click";
            rbAddress.Content = "";
            _clsSaleInvoiceBAL.CustomerId = 0;
            GridProductMaster.DataContext = _clsSaleInvoiceBAL;
            GridForter.DataContext = _clsSaleInvoiceBAL;
            controlEnable(true);
        }

        private void controlEnable(bool _vbol)
        {
            GridProductMaster.IsEnabled = _vbol;
            GridCategory.IsEnabled = _vbol;
            GridProductItem.IsEnabled = _vbol;
            rwtBarcode.IsEnabled = _vbol;
        }


        private void rwtOrderNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (rwtOrderNumber.Text != "")
                    {
                        ClearLogic();
                        int InvoiceNumber = 0;
                        int.TryParse(rwtOrderNumber.Text, out InvoiceNumber);
                        ObservableCollection<clsSaleInvoiceItemBAL> colSaleInvoiceItem = null;
                        clsSaleInvoiceBAL _clsSaleInvBAL = null;
                        clsSaleInvoiceBAL.getOrder(InvoiceNumber, ref _clsSaleInvBAL, ref colSaleInvoiceItem);
                        if (_clsSaleInvBAL == null)
                        {

                            MessageBox.Show("Invoice not found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        _clsSaleInvoiceBAL.IsOrder = true;
                        CollectionSaleInvoiceItem = colSaleInvoiceItem;
                        _clsSaleInvoiceBAL = _clsSaleInvBAL;
                        _clsSaleInvoiceBAL.IsRefOrderInvoice = true;
                        GridViewProduct.ItemsSource = CollectionSaleInvoiceItem;
                        _clsSaleInvoiceBAL.IsOrder = true;
                        GridProductMaster.DataContext = _clsSaleInvoiceBAL;
                        rbClientName.Content = _clsSaleInvoiceBAL.ClientName;
                        rbAddress.Content = _clsSaleInvoiceBAL.Address;
                        controlEnable(false);
                        SetFoter();
                        SaveLogic();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void PrintLogic(int SaleInvoiceId,string Reportype)
        {

            ReportDocument report = new ReportDocument();
            ConnectionInfo CI = new ConnectionInfo();
            TableLogOnInfo TLI = new TableLogOnInfo();
            TableLogOnInfos TLIS = new TableLogOnInfos();
            Tables crTables;
            string con = "";
            clsAppObject.clsCore.GetConnection(ref con);
            SqlConnectionStringBuilder str = new SqlConnectionStringBuilder(con);

            try
            {
               
               
                CI.ServerName = str.DataSource;
                CI.DatabaseName = str.InitialCatalog;
                CI.UserID = str.UserID;
                CI.Password = str.Password;
                string ReportPath;
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                ReportPath = dir + "//Reports//OrderReprt" + ".rpt";
                if (File.Exists(ReportPath) == true)
                {
                    report.Load(ReportPath);
                }
                else
                {
                    throw new Exception("Report not Found");
                }
                crTables = report.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    TLI = crTable.LogOnInfo;
                    TLI.ConnectionInfo = CI;
                    crTable.ApplyLogOnInfo(TLI);
                }

              

                #region declare and pass parameter

                ParameterDiscreteValue Parameter = new ParameterDiscreteValue();
                Parameter.Value = 5;
                report.ParameterFields["@TypeId"].CurrentValues.Add(Parameter);

                ParameterDiscreteValue Parameter1 = new ParameterDiscreteValue();
                Parameter1.Value = null;
                report.ParameterFields["@RecordStatus"].CurrentValues.Add(Parameter1);

                ParameterDiscreteValue Parameter2 = new ParameterDiscreteValue();
                Parameter2.Value = SaleInvoiceId;
                report.ParameterFields["@SaleInvoiceId"].CurrentValues.Add(Parameter2);

                ParameterDiscreteValue Parameter3 = new ParameterDiscreteValue();
                Parameter3.Value = null;
                report.ParameterFields["@InvoiceNumber"].CurrentValues.Add(Parameter3);

                ParameterDiscreteValue Parameter4 = new ParameterDiscreteValue();
                Parameter4.Value = Reportype;
                report.ParameterFields["Reportype"].CurrentValues.Add(Parameter4);

                ParameterDiscreteValue Parameter5 = new ParameterDiscreteValue();
                Parameter5.Value = null;
                report.ParameterFields["@SaleInvoiceItemId"].CurrentValues.Add(Parameter5);

                ParameterDiscreteValue Parameter6 = new ParameterDiscreteValue();
                Parameter6.Value = null;
                report.ParameterFields["@barcode"].CurrentValues.Add(Parameter6);


                report.PrintToPrinter(1, false, 0, 0);

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void btnBackdiscper_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setValueInItem("DiscPer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rwtBarcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (rwtBarcode.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Enter Barcode.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    var item = clsSaleInvoiceBAL.getProductItem(rwtBarcode.Text);
                    if (item == null)
                    {
                        MessageBox.Show("Product Not Found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    AddButtonLogic(item);
                    rwtBarcode.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F5)
                {
                    SaveLogic();
                }
                else if (e.Key == Key.F7)
                {
                    ClientLoginc();
                }
                else if (e.Key == Key.F2)
                {
                    setValueInItem("QTY");
                }
                else if (e.Key == Key.F3)
                {
                    setValueInItem("Price");
                }
                else if (e.Key == Key.F4)
                {
                    setValueInItem("Disc.");
                }
                else if (e.Key == Key.F6)
                {
                    setValueInItem("DiscPer");
                }
                else if (e.Key == Key.F8)
                {
                    GridViewProduct.SelectedItem = GridViewProduct.Items[0];
                }
                //else if (char.IsDigit((Char)e.Key.GetHashCode()))
                //{
                //    if (e.Key.ToString() == "." && lblValue.Content.ToString().Contains(e.Key.ToString()))
                //        return;
                //    System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
                //    lblValue.Content += e.Key.ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
