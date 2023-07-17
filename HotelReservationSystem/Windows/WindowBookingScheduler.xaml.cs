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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ScheduleView;

namespace HotelReservationSystem.Windows
{
    /// <summary>
    /// Interaction logic for WindowBookingScheduler.xaml
    /// </summary>
    public partial class WindowBookingScheduler : Window
    {
        public clsBookingBAL _clsBookingBAL = new clsBookingBAL();
        ObservableCollection<clsBookingBAL> _clsBookingBALCollection = new ObservableCollection<clsBookingBAL>();
        clsCustomerBAL Customer = null;
        public bool IsSave = false;
        public bool IsCheckout;
        public WindowBookingScheduler()
        {
            InitializeComponent();
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winCustomerList _winCustomerList = new winCustomerList();
                _winCustomerList.ShowDialog();
                Customer = _winCustomerList.Customer;
                if (Customer != null)
                {
                    _clsBookingBAL.CustomerId = Customer.CustomerId;
                    _clsBookingBAL.FirstName = Customer.FirstName;
                    _clsBookingBAL.MobileNumber = Customer.MobileNumber;
                    _clsBookingBAL.NIC = Customer.NIC;
                }
                else
                {
                    _clsBookingBAL.CustomerId = 0;
                    _clsBookingBAL.FirstName = "";
                    _clsBookingBAL.MobileNumber = "";
                    _clsBookingBAL.NIC = "";
                }
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsCheckout && _clsBookingBAL.BalanceAmount > 0)
                {
                    MessageBox.Show("Please Enter valid Payment Amount.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }    
                if (MessageBox.Show("Do you want to save record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (IsCheckout)
                    {
                        StackPanelDeposited.Visibility = Visibility.Visible;
                        _clsBookingBAL.RoomStatusId = 16;
                    }
                    else
                    {
                        _clsBookingBAL.RoomStatusId = 1;
                        lblPayment.Content = "Deposited Amount";
                    }
                        
                    bool vbol = clsBookingBAL.SaveLogic(_clsBookingBAL);
                    if (vbol)
                    {
                        IsSave = true;
                        if (IsCheckout)
                            PrintLogic(_clsBookingBAL.BookingId, "rptBooking");
                        else
                            PrintLogic(_clsBookingBAL.BookingId, "rptBookingShort");
                        MessageBox.Show("Record Save successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PrintLogic(int BookingId, string ReportName)
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
                ReportPath = dir + "//Reports//" + ReportName + ".rpt";
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
                Parameter.Value = 1;
                report.ParameterFields["@TypeId"].CurrentValues.Add(Parameter);

                ParameterDiscreteValue Parameter1 = new ParameterDiscreteValue();
                Parameter1.Value = null;
                report.ParameterFields["@DateFilterType"].CurrentValues.Add(Parameter1);

                ParameterDiscreteValue Parameter2 = new ParameterDiscreteValue();
                Parameter2.Value = BookingId;
                report.ParameterFields["@BookingId"].CurrentValues.Add(Parameter2);

                ParameterDiscreteValue Parameter3 = new ParameterDiscreteValue();
                Parameter3.Value = null;
                report.ParameterFields["@StartDate"].CurrentValues.Add(Parameter3);

                ParameterDiscreteValue Parameter4 = new ParameterDiscreteValue();
                Parameter4.Value = null;
                report.ParameterFields["@EndDate"].CurrentValues.Add(Parameter4);
                report.PrintToPrinter(1, false, 0, 0);
                //Peport_Viewer rp = new Peport_Viewer();
                //rp.cryViewer.ViewerCore.ReportSource = report;
                //rp.Show();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void clearRecord()
        {
            _clsBookingBAL = new clsBookingBAL();
            _clsBookingBAL.CheckInDate = DateTime.Today;
            _clsBookingBAL.CheckOutDate = DateTime.Today;
            this.DataContext = _clsBookingBAL;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CbRoom.ItemsSource = clsRoomBAL.GetRooms();
                //_Appointment.UniqueId;
                _clsBookingBALCollection.Add(_clsBookingBAL);
                dgBooking.ItemsSource = _clsBookingBALCollection;
                this.DataContext = _clsBookingBAL;
                if (IsCheckout)
                    StackPanelDeposited.Visibility = Visibility.Visible;                
                else                                    
                    lblPayment.Content = "Deposited Amount";                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdtCheckIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateChangeLogic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdtCheckOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateChangeLogic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DateChangeLogic()
        {
            if (_clsBookingBAL.CheckInDate > _clsBookingBAL.CheckOutDate)
            {
                MessageBox.Show("CheckOut Date must be greater than CheckIn Date", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                _clsBookingBAL.CheckOutDate = _clsBookingBAL.CheckInDate.AddDays(1);
                return;
            }
            _clsBookingBAL.NoOfDays = Convert.ToInt32((_clsBookingBAL.CheckOutDate.Date -_clsBookingBAL.CheckInDate.Date).TotalDays);
            if (_clsBookingBAL.NoOfDays == 0)
                _clsBookingBAL.NoOfDays = 1;
            calculateBalance();
        }

        private void txtPayment_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                calculateBalance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void calculateBalance()
        {
            //if (txtPayment.Value != null)
            //{
            decimal paymentamt = _clsBookingBAL.PaymentAmount != null ? Convert.ToDecimal(_clsBookingBAL.PaymentAmount) : 0;
            decimal Amount =Math.Round(_clsBookingBAL.TotalAmount,0) - Math.Round((_clsBookingBAL.DepositedAmount + paymentamt));
            _clsBookingBAL.BalanceAmount = Amount;
            if (Amount > 0)
                lblBalanceContent.Content = "Balance Amount:";
            else
                lblBalanceContent.Content = "Cash Back";
            //}
        }
    }
}
