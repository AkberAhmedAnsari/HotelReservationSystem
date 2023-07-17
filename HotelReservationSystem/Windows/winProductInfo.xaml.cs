using BAL.Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace HotelReservationSystem.Windows
{
    /// <summary>
    /// Interaction logic for winProductInfo.xaml
    /// </summary>
    public partial class winProductInfo : Window
    {
        public clsProductsInfoBAL _clsProductsInfoBAL = new clsProductsInfoBAL();
        public winProductInfo()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clearRecord();
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
                    _clsProductsInfoBAL.RecordImage = getImageByte();
                    bool vbol = clsProductsInfoBAL.SaveLogic(_clsProductsInfoBAL);
                    if (vbol)
                    {
                        MessageBox.Show("Record Save successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private byte[] getImageByte()
        {
            if (imageEditor.Image == null)
                return null;
            byte[] _selectedPhoto = null;
            IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".png");
            using (MemoryStream memStrm = new MemoryStream())
            {
                formatProvider.Export(imageEditor.Image, memStrm);

                if (memStrm.Length > int.MaxValue)
                {
                    throw new ApplicationException("Image is too large.");
                }
                _selectedPhoto = new byte[memStrm.Length];
                memStrm.Position = 0;
                memStrm.Read(_selectedPhoto, 0, (Int32)memStrm.Length);
            }
            return _selectedPhoto;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = _clsProductsInfoBAL;
                if (_clsProductsInfoBAL.RecordImage != null)
                {
                    IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".png");
                    imageEditor.Image = formatProvider.Import(_clsProductsInfoBAL.RecordImage);
                }
                fillComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillComboBox()
        {
            var List = clsCategoryInfoBAL.getCategoryInfo();
            CbProductCatagre.ItemsSource = List;

            var ProductTypeList = clsProductTypeBAL.getProductType();
            CbProductType.ItemsSource = ProductTypeList;
        }
        private void clearRecord()
        {
            _clsProductsInfoBAL = new clsProductsInfoBAL();
            DataContext = _clsProductsInfoBAL;
            imageEditor.Image = null;
        }
    }
}
