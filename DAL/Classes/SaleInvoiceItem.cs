using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class SaleInvoiceItem : BaseClass
    {
        private int _SaleInvoiceItemId;
        private int _SaleInvoiceId;
        private int _ProductItemId;
        private decimal _Price;
        private decimal _Quantity;
        private decimal _DiscountAmount;
        private string _Reamarks;
        private int? _ParentId;
        private int _DataEntryUserId;
        private DateTime _DataEntryDate;
        private int _RecordStatus;

        [PrimaryKey("This Is Primary Key")]
        public int SaleInvoiceItemId
        {
            get { return _SaleInvoiceItemId; }
            set
            {
                _SaleInvoiceItemId = value;
                OnPropertyChanged("SaleInvoiceItemId");
            }
        }

        public int SaleInvoiceId
        {
            get
            {
                return _SaleInvoiceId;
                
            }

            set
            {
                _SaleInvoiceId = value;
                OnPropertyChanged("SaleInvoiceId");
            }
        }

        public int ProductItemId
        {
            get
            {
                return _ProductItemId;
            }

            set
            {
                _ProductItemId = value;
                OnPropertyChanged("ProductItemId");
            }
        }

        public decimal Price
        {
            get
            {
                return _Price;
            }

            set
            {
                _Price = value;
                OnPropertyChanged("Price");
            }
        }

        public decimal Quantity
        {
            get
            {
                return _Quantity;
            }

            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public decimal DiscountAmount
        {
            get
            {
                return _DiscountAmount;
            }

            set
            {
                _DiscountAmount = value;
                OnPropertyChanged("DiscountAmount");
            }
        }

        public string Reamarks
        {
            get
            {
                return _Reamarks;
            }

            set
            {
                _Reamarks = value;
                OnPropertyChanged("Reamarks");
            }
        }

        public int? ParentId
        {
            get
            {
                return _ParentId;
            }

            set
            {
                _ParentId = value;
                OnPropertyChanged("ParentId");
            }
        }

        public int DataEntryUserId
        {
            get
            {
                return _DataEntryUserId;
            }

            set
            {
                _DataEntryUserId = value;
                OnPropertyChanged("DataEntryUserId");
            }
        }

        public DateTime DataEntryDate
        {
            get
            {
                return _DataEntryDate;
            }

            set
            {
                _DataEntryDate = value;
                OnPropertyChanged("DataEntryDate");
            }
        }

        public int RecordStatus
        {
            get
            {
                return _RecordStatus;
            }

            set
            {
                _RecordStatus = value;
                OnPropertyChanged("RecordStatus");
            }
        }
    }
}
