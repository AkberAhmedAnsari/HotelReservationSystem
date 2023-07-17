using BAL;
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
using System.Windows.Shapes;

namespace HotelReservationSystem.Windows
{
    /// <summary>
    /// Interaction logic for WindowStatusChange.xaml
    /// </summary>
    public partial class WindowStatusChange : Window
    {
        public clsRoomBAL _clsRoomBAL = null;
        public WindowStatusChange()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lblRome.Content = _clsRoomBAL.RoomName;
                ComboBoxRoomStatus.ItemsSource = clsRoomStatusBAL.GetRoomStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBoxRoomStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ComboBoxRoomStatus.SelectedItem != null)
                {
                    var item = ComboBoxRoomStatus.SelectedItem as clsRoomStatusBAL;
                    TextBlockDescription.Text = item.Description;
                }
                else
                {
                    TextBlockDescription.Text = "";
                }

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
                if (ComboBoxRoomStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Status.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (MessageBox.Show("Do you want to save record?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var item = ComboBoxRoomStatus.SelectedItem as clsRoomStatusBAL;
                    DAL.Classes.RoomActivityStatus _RoomActivityStatus =new DAL.Classes.RoomActivityStatus();
                    _RoomActivityStatus.RoomId = _clsRoomBAL.RoomId;
                    _RoomActivityStatus.RoomStatusId = item.RoomStatusId;
                    if (clsRoomStatusBAL.SaveLogic(_RoomActivityStatus))
                        this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
