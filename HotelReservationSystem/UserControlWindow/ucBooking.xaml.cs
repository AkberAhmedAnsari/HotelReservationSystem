using HotelReservationSystem.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace HotelReservationSystem.UserControlWindow
{
    /// <summary>
    /// Interaction logic for ucBooking.xaml
    /// </summary>
    public partial class ucBooking : UserControl
    {
        MyViewModel m = new MyViewModel();

        ResourceTypeCollection rsc = new ResourceTypeCollection();
        ResourceType locationResource = new ResourceType("RoomWithType");
        ObservableAppointmentCollection appointments = new ObservableAppointmentCollection();
        RoomWithStatus _RoomWithStatus = new RoomWithStatus();
        clsRoomBAL SelectedItem = null; 
        public ucBooking()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cbRoomType.ItemsSource = clsRoomTypeBAL.GetRoomType();
                this.DataContext = _RoomWithStatus;
                //this.ScheduleViewBooking.GroupDescriptionsSource = null;        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        
        private void ScheduleViewBooking_AppointmentEditing(object sender, AppointmentEditingEventArgs e)
        {
            
        }


        private void cbRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbRoomType.SelectedItem != null)
                {                   
                    LoadData();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData()
        {
            if (cbRoomType.SelectedItem != null)
            {
                var _clsRoomTypeBAL = cbRoomType.SelectedItem as clsRoomTypeBAL;
                _RoomWithStatus = new RoomWithStatus();
                _RoomWithStatus.RoomCollection = clsRoomBAL.GetRoomsWithStatus(_clsRoomTypeBAL.RoomTypeId);
                RadTransitionBooking.Content = _RoomWithStatus;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStatusChange_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Please Select Record", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (SelectedItem.RoomStatusId == 1)
            {
                MessageBox.Show("You are not allowed to change room status", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            WindowStatusChange _WindowStatusChange = new WindowStatusChange();
            _WindowStatusChange._clsRoomBAL = SelectedItem;
            _WindowStatusChange.ShowDialog();
            LoadData();
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show("Please Select Record", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (SelectedItem.RoomStatusId != 1)
                {
                    MessageBox.Show("Room not available for Checkout", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                WindowBookingScheduler _WindowBookingScheduler = new WindowBookingScheduler();
                _WindowBookingScheduler._clsBookingBAL.RoomIds = SelectedItem.RoomId.ToString();
                _WindowBookingScheduler.IsCheckout = true;
                _WindowBookingScheduler.Title = "Checkout";
                var _clsBookingBAL = _WindowBookingScheduler._clsBookingBAL;
                var Customer = clsCustomerBAL.getRecords(SelectedItem.CustomerId);
                if(Customer != null)
                {
                    _clsBookingBAL.CustomerId = Customer.CustomerId;
                    _clsBookingBAL.FirstName = Customer.FirstName;
                    _clsBookingBAL.NIC = Customer.NIC;
                    _clsBookingBAL.MobileNumber = Customer.MobileNumber;
                }
                _clsBookingBAL.BookingDate = SelectedItem.BookingDate;
                _clsBookingBAL.BookingNumber = SelectedItem.BookingNumber;
                _clsBookingBAL.BookingId = SelectedItem.BookingId;
                _clsBookingBAL.CheckInDate = SelectedItem.CheckInDate;
                _clsBookingBAL.CheckOutDate = SelectedItem.CheckOutDate;
                _clsBookingBAL.Remarks = SelectedItem.Remarks;
                _clsBookingBAL.RoomName = SelectedItem.RoomName;
                _clsBookingBAL.RoomTypeName = SelectedItem.RoomTypeName;
                _clsBookingBAL.Price = SelectedItem.RoomPrice;
                _clsBookingBAL.GSTPercentage = SelectedItem.GSTPercentage;
                _clsBookingBAL.DepositedAmount = SelectedItem.Amount;
                _clsBookingBAL.DiscountPercentge = SelectedItem.DiscountPercentge;
                _clsBookingBAL.DiscountAmount = SelectedItem.DiscountAmount;
                _clsBookingBAL.PaymentAmount = 0;
                _clsBookingBAL.PaymentModeId = SelectedItem.PaymentModeId;
                _WindowBookingScheduler.ShowDialog();
                if (_WindowBookingScheduler.IsSave)
                    LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBookin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show("Please Select Record", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if(SelectedItem.RoomStatusId != 9)
                {
                    MessageBox.Show("Room not available for Booking", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                WindowBookingScheduler _WindowBookingScheduler = new WindowBookingScheduler();
                var _clsBookingBAL = _WindowBookingScheduler._clsBookingBAL;
                _WindowBookingScheduler.Title = "Booking";
                _clsBookingBAL.RoomIds = SelectedItem.RoomId.ToString();
                _clsBookingBAL.CheckInDate = DateTime.Now;
                _clsBookingBAL.CheckOutDate = DateTime.Now.AddDays(1);
                _clsBookingBAL.RoomName = SelectedItem.RoomName;
                _clsBookingBAL.RoomTypeName = SelectedItem.RoomTypeName;
                _clsBookingBAL.Price = SelectedItem.RoomPrice;
                _clsBookingBAL.GSTPercentage = SelectedItem.GSTPercentage;
                _clsBookingBAL.NoOfDays = 1;
                _clsBookingBAL.PaymentModeId = SelectedItem.PaymentModeId;
                _WindowBookingScheduler.ShowDialog();
                if (_WindowBookingScheduler.IsSave)
                    LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListOfTiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (e.AddedItems != null && e.AddedItems.Count > 0)
                    SelectedItem = e.AddedItems[0] as clsRoomBAL;
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
                if (SelectedItem == null)
                {
                    MessageBox.Show("Please Select Record", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (SelectedItem.RoomStatusId != 1)
                {
                    MessageBox.Show("Room not available for Edit", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                WindowBookingScheduler _WindowBookingScheduler = new WindowBookingScheduler();
                _WindowBookingScheduler._clsBookingBAL.RoomIds = SelectedItem.RoomId.ToString();
                _WindowBookingScheduler.Title = "Booking Edit";
                var _clsBookingBAL = _WindowBookingScheduler._clsBookingBAL;
                var Customer = clsCustomerBAL.getRecords(SelectedItem.CustomerId);
                if (Customer != null)
                {
                    _clsBookingBAL.CustomerId = Customer.CustomerId;
                    _clsBookingBAL.FirstName = Customer.FirstName;
                    _clsBookingBAL.NIC = Customer.NIC;
                    _clsBookingBAL.MobileNumber = Customer.MobileNumber;
                }
                _clsBookingBAL.BookingDate = SelectedItem.BookingDate;
                _clsBookingBAL.BookingNumber = SelectedItem.BookingNumber;
                _clsBookingBAL.BookingId = SelectedItem.BookingId;
                _clsBookingBAL.CheckInDate = SelectedItem.CheckInDate;
                _clsBookingBAL.CheckOutDate = SelectedItem.CheckOutDate;
                _clsBookingBAL.Remarks = SelectedItem.Remarks;
                _clsBookingBAL.PaymentAmount = SelectedItem.Amount;
                _clsBookingBAL.RoomName = SelectedItem.RoomName;
                _clsBookingBAL.RoomTypeName = SelectedItem.RoomTypeName;
                _clsBookingBAL.Price = SelectedItem.RoomPrice;
                _clsBookingBAL.GSTPercentage = SelectedItem.GSTPercentage;
                _clsBookingBAL.DiscountPercentge = SelectedItem.DiscountPercentge;
                _clsBookingBAL.DiscountAmount = SelectedItem.DiscountAmount;
                _clsBookingBAL.PaymentModeId = SelectedItem.PaymentModeId;
                _WindowBookingScheduler.ShowDialog();
                if (_WindowBookingScheduler.IsSave)
                    LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show("Please Select Record", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (SelectedItem.RoomStatusId != 1)
                {
                    MessageBox.Show("Room not available for Edit", "Informaation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to Cancel Booking?", "Cancel Reservation", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    bool bol = clsBookingBAL.CancelReservation(SelectedItem.BookingId);
                    if (bol)
                    {
                        MessageBox.Show("Successfully Cancelling Your Booking", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                       
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class MyViewModel
    {
        private ObservableCollection<Appointment> appointments;

        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }
            set
            {
                appointments = value;
            }
        }
    }
    public class OrientedGroupHeaderContentTemplateSelector : ScheduleViewDataTemplateSelector
    {
        private DataTemplate horizontalTemplate;
        private DataTemplate verticalTemplate;
        private DataTemplate horizontalResourceTemplate;
        private DataTemplate verticalResourceTemplate;

        public DataTemplate HorizontalTemplate
        {
            get
            {
                return this.horizontalTemplate;
            }
            set
            {
                this.horizontalTemplate = value;
            }
        }

        public DataTemplate VerticalTemplate
        {
            get
            {
                return this.verticalTemplate;
            }
            set
            {
                this.verticalTemplate = value;
            }
        }

        public DataTemplate HorizontalResourceTemplate
        {
            get
            {
                return this.horizontalResourceTemplate;
            }
            set
            {
                this.horizontalResourceTemplate = value;
            }
        }

        public DataTemplate VerticalResourceTemplate
        {
            get
            {
                return this.verticalResourceTemplate;
            }
            set
            {
                this.verticalResourceTemplate = value;
            }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container, ViewDefinitionBase activeViewDeifinition)
        {
            CollectionViewGroup cvg = item as CollectionViewGroup;
            if (cvg != null && cvg.Name is IResource)
            {
                if (activeViewDeifinition.GetOrientation() == Orientation.Vertical)
                {
                    if (this.HorizontalResourceTemplate != null)
                    {
                        return this.HorizontalResourceTemplate;
                    }
                }
                else
                {
                    if (this.VerticalResourceTemplate != null)
                    {
                        return this.VerticalResourceTemplate;
                    }
                }
            }

            if (cvg != null && cvg.Name is DateTime)
            {
                if (activeViewDeifinition.GetOrientation() == Orientation.Vertical)
                {
                    if (this.HorizontalTemplate != null)
                    {
                        return this.HorizontalTemplate;
                    }
                }
                else
                {
                    if (this.VerticalTemplate != null)
                    {
                        return this.VerticalTemplate;
                    }
                }
            }

            return base.SelectTemplate(item, container, activeViewDeifinition);
        }
    }
    public class CustomAppointmentTemplateSelector : Telerik.Windows.Controls.ScheduleViewDataTemplateSelector
    {
        public DataTemplate DayAppointmentTemplate { get; set; }
        public DataTemplate DefaultAppointmentTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container, Telerik.Windows.Controls.ViewDefinitionBase activeViewDefinition)
        {
            if (activeViewDefinition is WeekViewDefinition)
                return this.DefaultAppointmentTemplate;

            if (activeViewDefinition is TimelineViewDefinition)
                return this.DayAppointmentTemplate;

            return base.SelectTemplate(item, container, activeViewDefinition);
        }
    }


}
