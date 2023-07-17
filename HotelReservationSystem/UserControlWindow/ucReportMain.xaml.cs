using HotelReservationSystem.Windows;
using BAL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelReservationSystem.UserControlWindow
{
    /// <summary>
    /// Interaction logic for ucReportMain.xaml
    /// </summary>
    public partial class ucReportMain : UserControl
    {
        public ucReportMain()
        {
            InitializeComponent();
        }

        private void TreeViewReportList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                TreeViewItem _TreeViewItem = (TreeViewItem)TreeViewReportList.SelectedItem;
                if (_TreeViewItem.Header.ToString() == "Delivery Report")
                {
                    gbDateFilter.Visibility = Visibility.Visible;
                    gbInvoiceDeliveredFilter.Visibility = Visibility.Visible;
                    stpDateSelectionType.Visibility = Visibility.Visible;
                    lblreprtName.Content = _TreeViewItem.Header;
                }
                else if (_TreeViewItem.Header.ToString() == "Best Sales Report")
                {
                    gbInvoiceDeliveredFilter.Visibility = Visibility.Collapsed;
                    gbDateFilter.Visibility = Visibility.Visible;
                    rbDataEntryDate.IsChecked = true;
                    stpDateSelectionType.Visibility = Visibility.Collapsed;
                    lblreprtName.Content = _TreeViewItem.Header;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtStartDate.SelectedDate == null)
                {
                    MessageBox.Show("Please Select Start Date");
                    return;
                }

                if (dtEndDate.SelectedDate == null)
                {
                    MessageBox.Show("Please Select End Date");
                    return;
                }
                TreeViewItem _TreeViewItem = (TreeViewItem)TreeViewReportList.SelectedItem;
                if (_TreeViewItem.Header.ToString() == "Delivery Report")
                {
                    int DateFilterType = 0;
                    if (rbInvoiceDate.IsChecked == true)
                        DateFilterType = 1;
                    else if (rbDeliveryDate.IsChecked == true)
                        DateFilterType = 2;
                    else if (rbDataEntryDate.IsChecked == true)
                        DateFilterType = 0;

                    int InvoiceStatusId = -1;
                    if (rbDelivered.IsChecked == true)
                        InvoiceStatusId = 1;
                    else if (rbNotDelivered.IsChecked == true)
                        InvoiceStatusId = 0;
                    else
                        InvoiceStatusId = -1;
                    PrintOrderDeliveryReport(dtStartDate.SelectedDate.Value, dtEndDate.SelectedDate.Value, DateFilterType, InvoiceStatusId);
                }
                else if (_TreeViewItem.Header.ToString() == "Best Sales Report")
                {
                    PrintBestSalesReport(dtStartDate.SelectedDate.Value, dtEndDate.SelectedDate.Value);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void PrintBestSalesReport(DateTime StartDate, DateTime EndDate)
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
                ReportPath = dir + "//Reports//rptBestSalesReport" + ".rpt";
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
                Parameter.Value = 2;
                report.ParameterFields["@TypeId"].CurrentValues.Add(Parameter);

                ParameterDiscreteValue Parameter1 = new ParameterDiscreteValue();
                Parameter1.Value = StartDate;
                report.ParameterFields["@StartDate"].CurrentValues.Add(Parameter1);

                ParameterDiscreteValue Parameter2 = new ParameterDiscreteValue();
                Parameter2.Value = EndDate;
                report.ParameterFields["@EndDate"].CurrentValues.Add(Parameter2);

              
                ParameterDiscreteValue Parameter3 = new ParameterDiscreteValue();
                Parameter3.Value = null;
                report.ParameterFields["@DateFilterType"].CurrentValues.Add(Parameter3);

                ParameterDiscreteValue Parameter4 = new ParameterDiscreteValue();
                Parameter4.Value = null;
                report.ParameterFields["@InvoiceStatusId"].CurrentValues.Add(Parameter4);

                ParameterDiscreteValue Parameter5 = new ParameterDiscreteValue();
                Parameter5.Value = BAL.clsAppObject.Company.CompanyName;
                report.ParameterFields["CompanyName"].CurrentValues.Add(Parameter5);

                ParameterDiscreteValue Parameter6 = new ParameterDiscreteValue();
                Parameter6.Value = "Best Sales Report - With Quantity";
                report.ParameterFields["Reportype"].CurrentValues.Add(Parameter6);

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

        private void PrintOrderDeliveryReport(DateTime StartDate, DateTime EndDate, int DateFilterType, int InvoiceStatusId)
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
                ReportPath = dir + "//Reports//rptDeliveryReport" + ".rpt";
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
                Parameter1.Value = StartDate;
                report.ParameterFields["@StartDate"].CurrentValues.Add(Parameter1);

                ParameterDiscreteValue Parameter2 = new ParameterDiscreteValue();
                Parameter2.Value = EndDate;
                report.ParameterFields["@EndDate"].CurrentValues.Add(Parameter2);


                ParameterDiscreteValue Parameter3 = new ParameterDiscreteValue();
                Parameter3.Value = DateFilterType;
                report.ParameterFields["@DateFilterType"].CurrentValues.Add(Parameter3);

                ParameterDiscreteValue Parameter4 = new ParameterDiscreteValue();
                Parameter4.Value = InvoiceStatusId;
                report.ParameterFields["@InvoiceStatusId"].CurrentValues.Add(Parameter4);

                ParameterDiscreteValue Parameter5 = new ParameterDiscreteValue();
                Parameter5.Value = BAL.clsAppObject.Company.CompanyName;
                report.ParameterFields["CompanyName"].CurrentValues.Add(Parameter5);

                ParameterDiscreteValue Parameter6 = new ParameterDiscreteValue();
                Parameter6.Value = "Order Delivery Report";
                report.ParameterFields["Reportype"].CurrentValues.Add(Parameter6);

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

    }
}
