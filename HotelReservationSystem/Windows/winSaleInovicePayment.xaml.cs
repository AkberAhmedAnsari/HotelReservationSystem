using BAL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for winSaleInovicePayment.xaml
    /// </summary>
    public partial class winSaleInovicePayment : Window
    {
        public clsSaleInvoiceBAL _clsSaleInvoiceBAL = new clsSaleInvoiceBAL();
        public clsSaleInvoicePaymentModeBAL _clsSaleInvoicePaymentModeBAL = null;
        public winSaleInovicePayment()
        {
            InitializeComponent();
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            txtAmount.Text = "";
        }

        //private void btnBackprice_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        setValueInItem("Price");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void btnBackdisc_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        setValueInItem("Disc.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void btnBackqty_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        setValueInItem("QTY");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button _RadButton = sender as Button;
                if (_RadButton != null)
                {
                    if (_RadButton.Content.ToString() == "." && txtAmount.Text.Contains(_RadButton.Content.ToString()))
                        return;
                    if (txtAmount.SelectedText.Length == txtAmount.Text.Length)
                        txtAmount.Text = "";
                    txtAmount.Text += _RadButton.Content.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setValueInItem(string btnName)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal Amount = 0;
                decimal.TryParse(txtAmount.Text, out Amount);
                if (_clsSaleInvoicePaymentModeBAL == null)
                    _clsSaleInvoicePaymentModeBAL = new clsSaleInvoicePaymentModeBAL();


                _clsSaleInvoicePaymentModeBAL.Amount = _clsSaleInvoiceBAL.PaidAmount;
                _clsSaleInvoicePaymentModeBAL.SaleInvoiceId = _clsSaleInvoiceBAL.SaleInvoiceId;
                if(_clsSaleInvoiceBAL.IsRefOrderInvoice == true)
                {
                    _clsSaleInvoicePaymentModeBAL.PaymentModeTypeId = 1;
                }
                else if (btnAdvance.IsChecked == true)
                    _clsSaleInvoicePaymentModeBAL.PaymentModeTypeId = 2;
                else if (btnCash.IsChecked == true)
                    _clsSaleInvoicePaymentModeBAL.PaymentModeTypeId = 1;

                if (Valadation(_clsSaleInvoicePaymentModeBAL) == true)
                {
                    _clsSaleInvoicePaymentModeBAL.Amount = _clsSaleInvoiceBAL.PaidAmount;
                    this.Close();
                }
                else
                    _clsSaleInvoicePaymentModeBAL = null;
            }
            catch (Exception ex)
            {
                _clsSaleInvoicePaymentModeBAL = null;
                MessageBox.Show(ex.Message);
            }
        }


        private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "." && txtAmount.Text.Contains("."))
                e.Handled = true;
            else
            {
                e.Handled = !TextBoxTextAllowed(e.Text);
            }
        }

        private void textBoxValue_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate (Char c) { return Char.IsDigit(c) || Char.IsControl(c) || c == 46; });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_clsSaleInvoiceBAL.IsRefOrderInvoice == true)
                {
                    var payment = clsSaleInvoiceBAL.getPaymentOrder(_clsSaleInvoiceBAL.SaleInvoiceId);
                    if (payment != null)
                    {
                        _clsSaleInvoiceBAL.AdvanceAmount = payment.Amount;
                    }
                }
                _clsSaleInvoiceBAL.CurrentAmount = _clsSaleInvoiceBAL.NetAmount - _clsSaleInvoiceBAL.AdvanceAmount;
                if (_clsSaleInvoiceBAL.CurrentAmount <= 0)
                    txtAmount.IsEnabled = false;
                //lblCurrentAmount.Content = _clsSaleInvoiceBAL.CurrentAmount;
                //lblNetAmount.Content = _clsSaleInvoiceBAL.NetAmount;
                //lblPaidAmount.Content = _clsSaleInvoiceBAL.AdvanceAmount;
                stpPayment.DataContext = _clsSaleInvoiceBAL;
                txtAmount.Text = _clsSaleInvoiceBAL.CurrentAmount.ToString("#####");
                txtAmount.Focus();
                txtAmount.SelectAll();
                if (_clsSaleInvoiceBAL.IsOrder == true && _clsSaleInvoiceBAL.IsRefOrderInvoice == false)
                {
                    btnAdvance.Visibility = Visibility.Visible;
                    btnAdvance.IsChecked = true;
                    btnCash.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnAdvance.Visibility = Visibility.Collapsed;
                    btnCash.Visibility = Visibility.Visible;
                    btnCash.IsChecked = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Valadation(clsSaleInvoicePaymentModeBAL _clsSaleInvoicePaymentModeBAL)
        {
            if (_clsSaleInvoiceBAL.IsRefOrderInvoice)
            {
                return true;
            }
             if (_clsSaleInvoicePaymentModeBAL.Amount <= 0)
            {
                MessageBox.Show("Please Enter Amount.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (_clsSaleInvoicePaymentModeBAL.PaymentModeTypeId == 1 &&
                _clsSaleInvoicePaymentModeBAL.Amount < _clsSaleInvoiceBAL.CurrentAmount)
            {
                MessageBox.Show("Please Enter Valid Amount.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void txtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                decimal Amount = 0;
                decimal.TryParse(txtAmount.Text, out Amount);
                if (Amount > _clsSaleInvoiceBAL.CurrentAmount)
                {
                    lblBalance.Content = "Cash Back";
                }
                if (Amount <= _clsSaleInvoiceBAL.CurrentAmount)
                {
                    if (_clsSaleInvoiceBAL.IsOrder)
                        lblBalance.Content = "Balance";
                    else if (_clsSaleInvoiceBAL.IsOrder == false)
                        lblBalance.Content = "Balance";
                }
                decimal Balance = (_clsSaleInvoiceBAL.CurrentAmount - Amount);
                txtBalanceAmount.Text = Balance.ToString("#######");
                _clsSaleInvoiceBAL.PaidAmount = _clsSaleInvoiceBAL.CurrentAmount - Balance;
                if (_clsSaleInvoiceBAL.PaidAmount > _clsSaleInvoiceBAL.CurrentAmount)
                    _clsSaleInvoiceBAL.PaidAmount = _clsSaleInvoiceBAL.CurrentAmount;
                else
                    _clsSaleInvoiceBAL.PaidAmount = _clsSaleInvoiceBAL.CurrentAmount - Balance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                txtAmount.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
