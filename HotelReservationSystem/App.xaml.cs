using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Windows8Palette.Palette.MainColor = Colors.Black;
            Windows8Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0CC579");
            //Windows8Palette.Palette.BasicColor = Colors.Red; ;
            //Windows8Palette.Palette.StrongColor = Colors.Gray;
            //Windows8Palette.Palette.MarkerColor = Colors.LightGray;
            //Windows8Palette.Palette.ValidationColor = Colors.Red;
        }
    }
}
