using HotelReservationSystem.UserControlWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Telerik.Windows.Controls;

namespace HotelReservationSystem.MainWindows
{
    /// <summary>
    /// Interaction logic for winMainManu.xaml
    /// </summary>
    public partial class winMainManu : Window
    {
        public winMainManu()
        {
            InitializeComponent();
        }

        private void RadMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucClientInformation uc = new ucClientInformation();
                uc.Margin = new Thickness(5);
                tb.Background = Brushes.White;
                tb.Content = uc;
                tb.Title = "Customer Information";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void RadMenuItemProduct_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucProductInfo uc = new ucProductInfo();
                uc.Margin = new Thickness(5);
                tb.Background = Brushes.White;
                tb.Content = uc;
                tb.Title = "Product Information";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadMenuBooking_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucBooking uc = new ucBooking();
                uc.Margin = new Thickness(5);
                tb.Background = Brushes.White;
                tb.Content = uc;
                tb.Title = "Booking";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadMenuCategory_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucProductCatagory uc = new ucProductCatagory();
                uc.Margin = new Thickness(5);
                tb.Background = Brushes.White;
                tb.Content = uc;
                tb.Title = "Catagory List";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadMenuUser_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucUserInfoList uc = new ucUserInfoList();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "User List";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void RadMenuPointOfSAleList_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucPointOfSaleList uc = new ucPointOfSaleList();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "Point Of Sale List";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BAL.clsAppObject.LoginUser.UserTypeId != 1)
                    MenuAdministration.Visibility = Visibility.Collapsed;

                    var company = BAL.Classes.clsCompanyBAL.GetCompany();
                if (company != null)
                {
                    lblCompany.Content = company.CompanyName;
                    BAL.clsAppObject.Company = company;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadMenuReports_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucReportMain uc = new ucReportMain();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "Reports";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadMenuRoomType_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucRoomType uc = new ucRoomType();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "Room Type";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RadMenuRooms_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucRooms uc = new ucRooms();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "Room";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void RadMenuCountry_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucCountry uc = new ucCountry();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "Country";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RadMenuCity_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucCity uc = new ucCity();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "City";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RadMenuBookingList_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                ClosableTab tb = new ClosableTab();
                ucBookingList uc = new ucBookingList();
                tb.Background = Brushes.White;
                uc.Margin = new Thickness(5);
                tb.Content = uc;
                tb.Title = "BooKing List";
                tb.IsSelected = true;
                RadTabControlMain.Items.Add(tb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
    class ClosableTab : TabItem
    {


        // Constructor
        public ClosableTab()
        {
            // Create an instance of the usercontrol
            CloseableHeader closableTabHeader = new CloseableHeader();

            // Assign the usercontrol to the tab header
            this.Header = closableTabHeader;

            // Attach to the CloseableHeader events (Mouse Enter/Leave, Button Click, and Label resize)
            closableTabHeader.button_close.MouseEnter += new MouseEventHandler(button_close_MouseEnter);
            closableTabHeader.button_close.MouseLeave += new MouseEventHandler(button_close_MouseLeave);
            closableTabHeader.button_close.Click += new RoutedEventHandler(button_close_Click);
            closableTabHeader.label_TabTitle.SizeChanged += new SizeChangedEventHandler(label_TabTitle_SizeChanged);
        }



        /// <summary>
        /// Property - Set the Title of the Tab
        /// </summary>
        public string Title
        {
            set
            {
                ((CloseableHeader)this.Header).label_TabTitle.Content = value;
            }
        }




        //
        // - - - Overrides  - - -
        //


        // Override OnSelected - Show the Close Button
        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
        }

        // Override OnUnSelected - Hide the Close Button
        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
        }

        // Override OnMouseEnter - Show the Close Button
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
        }

        // Override OnMouseLeave - Hide the Close Button (If it is NOT selected)
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.IsSelected)
            {
                ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
            }
        }





        //
        // - - - Event Handlers  - - -
        //


        // Button MouseEnter - When the mouse is over the button - change color to Red
        void button_close_MouseEnter(object sender, MouseEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Foreground = Brushes.Red;
        }

        // Button MouseLeave - When mouse is no longer over button - change color back to black
        void button_close_MouseLeave(object sender, MouseEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Foreground = Brushes.Black;
        }


        // Button Close Click - Remove the Tab - (or raise an event indicating a "CloseTab" event has occurred)
        void button_close_Click(object sender, RoutedEventArgs e)
        {
            ((TabControl)this.Parent).Items.Remove(this);
        }


        // Label SizeChanged - When the Size of the Label changes (due to setting the Title) set position of button properly
        void label_TabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Margin = new Thickness(((CloseableHeader)this.Header).label_TabTitle.ActualWidth + 5, 3, 4, 0);
        }

    }
}
