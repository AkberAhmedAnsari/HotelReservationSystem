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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using BAL;
using System.Data.SqlClient;
using System.IO;

namespace HotelReservationSystem.UserControlWindow
{
    /// <summary>
    /// Interaction logic for ucClientInformation.xaml
    /// </summary>
    public partial class ucBookingList : UserControl
    {
        public ucBookingList()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //winCustomerInformation win = new winCustomerInformation();
                //win.ShowDialog();
                //getrecord();
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
                if(gridviewBooking.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                winCustomerInformation win = new winCustomerInformation();
                clsCustomerBAL Customer =  gridviewBooking.SelectedItem as clsCustomerBAL;
                win.objCustomerBAL = Customer;
                win.ShowDialog();
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
                if (gridviewBooking.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to delete record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsCustomerBAL Customer = gridviewBooking.SelectedItem as clsCustomerBAL;
                    bool vbol = clsCustomerBAL.DeleteCustomer(Customer.CustomerId);
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
            if (dtStartDate.SelectedDate == null)
            {
                MessageBox.Show("Please select Start Date", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (dtEndDate.SelectedDate == null)
            {
                MessageBox.Show("Please select End Date", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var collection = clsBookingBAL.GetBookingData(dtStartDate.SelectedDate.Value, dtEndDate.SelectedDate.Value);
            gridviewBooking.ItemsSource = collection;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtStartDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 00, 00, 00);
                dtEndDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 11, 11, 00);
                dtEndDate.SelectedTime = new TimeSpan(23, 59, 59);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void PrintLogic(int BookingId,string reportName)
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
                ReportPath = dir + "//Reports//" + reportName + ".rpt";
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

                Peport_Viewer rp = new Peport_Viewer();
                rp.cryViewer.ViewerCore.ReportSource = report;
                rp.Show();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridviewBooking.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                clsBookingBAL _clsSaleInvoiceBAL = gridviewBooking.SelectedItem as clsBookingBAL;

                PrintLogic(_clsSaleInvoiceBAL.BookingId, "rptBooking");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrintShort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridviewBooking.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                clsBookingBAL _clsSaleInvoiceBAL = gridviewBooking.SelectedItem as clsBookingBAL;

                PrintLogic(_clsSaleInvoiceBAL.BookingId, "rptBookingShort");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
