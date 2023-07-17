using BAL.Classes;
using HotelReservationSystem.Windows;
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

namespace HotelReservationSystem.UserControlWindow
{
    /// <summary>
    /// Interaction logic for ucRooms.xaml
    /// </summary>
    public partial class ucRooms : UserControl
    {
        public ucRooms()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowRoom _WindowRoom = new WindowRoom();
                _WindowRoom.ShowDialog();
                getrecord();
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
                if (gridviewRooms.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                WindowRoom _WindowRoom = new WindowRoom();
                _WindowRoom._clsRoomBAL = gridviewRooms.SelectedItem as clsRoomBAL;
                _WindowRoom.ShowDialog();
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
                if (gridviewRooms.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to delete record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsRoomBAL _clsRoomBAL = gridviewRooms.SelectedItem as clsRoomBAL;
                    bool vbol = clsRoomBAL.DeleteRooms(_clsRoomBAL.RoomId);
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
            var collection = clsRoomBAL.GetRooms();
            gridviewRooms.ItemsSource = collection;
        }
    }
}
