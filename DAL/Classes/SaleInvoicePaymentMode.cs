using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class SaleInvoicePaymentMode : BaseClass
    {
        private int _SaleInvoicePaymentModeId;
        private int _SaleInvoiceId;
        private int _PaymentModeTypeId;
        private decimal _Amount;
        private decimal _DataEntryUserId;
        private DateTime _DataEntryDate;
        private int _RecordStatus;

        [PrimaryKey("This Is Primary Key")]
        public int SaleInvoicePaymentModeId
        {
            get
            {
                return _SaleInvoicePaymentModeId;
            }

            set
            {
                _SaleInvoicePaymentModeId = value;
                OnPropertyChanged("SaleInvoicePaymentModeId");
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

        public int PaymentModeTypeId
        {
            get
            {
                return _PaymentModeTypeId;
            }

            set
            {
                _PaymentModeTypeId = value;
                OnPropertyChanged("PaymentModeTypeId");
            }
        }

        public decimal Amount
        {
            get
            {
                return _Amount;
            }

            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public decimal DataEntryUserId
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
