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
    public partial class ucRoomType : UserControl
    {
        public ucRoomType()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowRoomType _WindowRoomType = new WindowRoomType();
                _WindowRoomType.ShowDialog();
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
                if (gridviewRoomType.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                WindowRoomType _WindowRoomType = new WindowRoomType();
                _WindowRoomType._clsRoomTypeBAL = gridviewRoomType.SelectedItem as clsRoomTypeBAL;
                _WindowRoomType.ShowDialog();
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
                if (gridviewRoomType.SelectedItem == null)
                {
                    MessageBox.Show("Please select Record", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to delete record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsRoomTypeBAL _clsRoomTypeBAL = gridviewRoomType.SelectedItem as clsRoomTypeBAL;
                    bool vbol = clsRoomTypeBAL.DeleteRoomType(_clsRoomTypeBAL.RoomTypeId);
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
            var collection = clsRoomTypeBAL.GetRoomType();
            gridviewRoomType.ItemsSource = collection;
        }
    }
}
