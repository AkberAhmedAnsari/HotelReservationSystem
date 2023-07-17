using DAL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Classes
{
    public class clsSaleInvoiceItemBAL : SaleInvoiceItem
    {
        private string _Barcode;

        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }

        private string _ProductItemName;

        public string ProductItemName
        {
            get { return _ProductItemName; }
            set { _ProductItemName = value; }
        }

        private decimal _DiscountPer;

        public decimal DiscountPer
        {
            get
            {
                return _DiscountPer;
            }
            set
            {
                _DiscountPer = value;
                DiscountAmount = ((Quantity * Price) * _DiscountPer) / 100;
                OnPropertyChanged("DiscountPer");
            }
        }

        private decimal _NetAmount;

        public decimal NetAmount
        {
            get
            {
                _NetAmount = ((Quantity * Price) - DiscountAmount);
                return _NetAmount;
            }
            set
            {
                _NetAmount = value;
                OnPropertyChanged("NetAmount");
            }
        }

    }
}
