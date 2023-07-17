using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class SaleInvoice : BaseClass
    {
        private int _SaleInvoiceId;
        private int _InvoiceNumber;
        private DateTime _InvoiceDate;
        private DateTime? _DeliveryDate;
        private int? _SaleInvoiceParentId;
        private int? _CounterId;
        private int _SaleTypeId;
        private int? _BranchId;
        private int _CustomerId;
        private decimal _NetAmount;
        private decimal _TotalItemDiscount;
        private decimal _InvoiceDiscount;
        private string _reamarks;
        private int _DataEntryUserId;
        private DateTime _DataEntryDate;
        private int _RecordStatus;

        [PrimaryKey("This Is Primary Key")]
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

        public int InvoiceNumber
        {
            get
            {
                return _InvoiceNumber;
            }

            set
            {
                _InvoiceNumber = value;
                OnPropertyChanged("InvoiceNumber");
            }
        }

        public DateTime InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }

            set
            {
                _InvoiceDate = value;
                OnPropertyChanged("InvoiceDate");
            }
        }

        public DateTime? DeliveryDate
        {
            get
            {
                return _DeliveryDate;
            }

            set
            {
                _DeliveryDate = value;
                OnPropertyChanged("DeliveryDate");
            }
        }

        public int? SaleInvoiceParentId
        {
            get
            {
                return _SaleInvoiceParentId;
            }

            set
            {
                _SaleInvoiceParentId = value;
                OnPropertyChanged("SaleInvoiceParentId");
            }
        }

        public int? CounterId
        {
            get
            {
                return _CounterId;
            }

            set
            {
                _CounterId = value;
                OnPropertyChanged("CounterId");
            }
        }

        public int SaleTypeId
        {
            get
            {
                return _SaleTypeId;
            }

            set
            {
                _SaleTypeId = value;
                OnPropertyChanged("SaleTypeId");
            }
        }

        public int? BranchId
        {
            get
            {
                return _BranchId;
            }

            set
            {
                _BranchId = value;
                OnPropertyChanged("BranchId");
            }
        }

        public int CustomerId
        {
            get
            {
                return _CustomerId;
            }

            set
            {
                _CustomerId = value;
                OnPropertyChanged("CustomerId");
            }
        }

        public decimal NetAmount
        {
            get
            {
                return _NetAmount;
            }

            set
            {
                _NetAmount = value;
                OnPropertyChanged("NetAmount");
            }
        }

        public decimal TotalItemDiscount
        {
            get
            {
                return _TotalItemDiscount;
            }

            set
            {
                _TotalItemDiscount = value;
                OnPropertyChanged("TotalItemDiscount");
            }
        }

        public decimal InvoiceDiscount
        {
            get
            {
                return _InvoiceDiscount;
            }

            set
            {
                _InvoiceDiscount = value;
                OnPropertyChanged("InvoiceDiscount");
            }
        }

        public string Reamarks
        {
            get
            {
                return _reamarks;
            }

            set
            {
                _reamarks = value;
                OnPropertyChanged("Reamarks");
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
