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
    /// Interaction logic for WindowRoom.xaml
    /// </summary>
    public partial class WindowRoomType : Window
    {
        public clsRoomTypeBAL _clsRoomTypeBAL = new clsRoomTypeBAL();
        public WindowRoomType()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               ClearRecord();
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
                if (MessageBox.Show("Do you want to save record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    bool vbol = clsRoomTypeBAL.SaveLogic(_clsRoomTypeBAL);
                    if (vbol)
                    {
                        MessageBox.Show("Record save successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearRecord()
        {
            _clsRoomTypeBAL = new clsRoomTypeBAL();
            this.DataContext = _clsRoomTypeBAL;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = _clsRoomTypeBAL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
