using HotelReservationSystem.Windows;
using BAL;
using BAL.Classes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SAPBusinessObjects.WPF.Viewer;
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
using CrystalDecisions.CrystalReports.Engine;

namespace HotelReservationSystem.UserControlWindow
{
    /// <summary>
    /// Interaction logic for ucPointOfSaleList.xaml
    /// </summary>
    public partial class ucPointOfSaleList : UserControl
    {
        public ucPointOfSaleList()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clsAppObject.CheckUserRole(1, clsAppObject.LoginUser))
                {
                    winPointOfSale pos = new winPointOfSale();
                    pos.ShowDialog();
                    getRecord();
                }
                else
                    MessageBox.Show("You are no Permeation for edit", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (clsAppObject.CheckUserRole(1, clsAppObject.LoginUser))
                {
                    if (gridviewProduct.SelectedItem == null)
                    {
                        MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    clsSaleInvoiceBAL _clsSaleInvoiceBAL = gridviewProduct.SelectedItem as clsSaleInvoiceBAL;

                    if (_clsSaleInvoiceBAL.InvoiceStatus == "Delivered")
                    {
                        MessageBox.Show("Record cannot be Edit. Order has been delivered.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    winPointOfSale pos = new winPointOfSale();
                    pos._clsSaleInvoiceBAL = _clsSaleInvoiceBAL;
                    pos.IsEdit = true;
                    pos.CollectionSaleInvoiceItem = clsSaleInvoiceBAL.getSalesInvoiceItemforEdit(_clsSaleInvoiceBAL.SaleInvoiceId);
                    pos.ShowDialog();
                    getRecord();
                }
                else
                    MessageBox.Show("You are no Permeation for edit", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (clsAppObject.CheckUserRole(2, clsAppObject.LoginUser))
                {
                    if (gridviewProduct.SelectedItem == null)
                    {
                        MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    clsSaleInvoiceBAL _clsProductsInfoBAL = gridviewProduct.SelectedItem as clsSaleInvoiceBAL;
                    if (_clsProductsInfoBAL.InvoiceStatus == "Delivered")
                    {
                        MessageBox.Show("Record cannot be deleted. Order has been delivered.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (MessageBox.Show("Do you want to delete record?", "Confirmation",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {

                        bool vob = clsSaleInvoiceBAL.DeleteInvoice(_clsProductsInfoBAL.SaleInvoiceId);
                        if (vob)
                        {
                            MessageBox.Show("Record deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            getRecord();
                        }
                    }
                }
                else
                    MessageBox.Show("You are no Permeation for delete", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                getRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getRecord()
        {
            var list = clsSaleInvoiceBAL.getRecords();
            gridviewProduct.ItemsSource = list;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridviewProduct.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                clsSaleInvoiceBAL _clsSaleInvoiceBAL = gridviewProduct.SelectedItem as clsSaleInvoiceBAL;
                if (_clsSaleInvoiceBAL.InvoiceStatus == "Not Delivered")
                {
                    PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Customer Slip");
                    PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Order Slip");
                    PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Original Slip");
                }
                else
                {
                    PrintLogic(Convert.ToInt32(_clsSaleInvoiceBAL.SaleInvoiceParentId), "Final Slip");
                }
                //PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Customer Slip");
                //PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "Order Slip");
                //PrintLogic(_clsSaleInvoiceBAL.SaleInvoiceId, "KOT Slip");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintLogic(int SaleInvoiceId, string Reportype)
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
                //report.PrintToPrinter(1, false, 0, 0);

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
