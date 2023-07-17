using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class Booking : BaseClass
    {
        private int _BookingId;

        [PrimaryKey("This Is Primary Key")]
        public int BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }
        public string BookingNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public int CustomerId { get; set; }
        public string RoomIds { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Remarks { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }
        public int NoOfNight { get; set; }

        private decimal _Price;

        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                this.OnPropertyChanged("Price");
            }
        }
        private decimal _TotalAmount;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set
            {
                _TotalAmount = value;
                this.OnPropertyChanged("TotalAmount");
            }
        }

        private decimal _GSTPercentage;

        public decimal GSTPercentage
        {
            get { return _GSTPercentage; }
            set
            {
                _GSTPercentage = value;

                this.OnPropertyChanged("GSTPercentage");
            }
        }
        private decimal _GSTAmount;

        public decimal GSTAmount
        {
            get { return _GSTAmount; }
            set
            {
                _GSTAmount = value;
                this.OnPropertyChanged("GSTAmount");
            }
        }

        private decimal _DiscountPercentge;

        public decimal DiscountPercentge
        {
            get { return _DiscountPercentge; }
            set
            {
                _DiscountPercentge = value;
                _DiscountAmount = CalculateDiscountAmount(_DiscountPercentge);
                _TotalAmount = (_Price * NoOfNight) + _GSTAmount - _DiscountAmount;
                this.OnPropertyChanged("DiscountPercentge");
                this.OnPropertyChanged("DiscountAmount");
                this.OnPropertyChanged("TotalAmount");
            }
        }
        private decimal _DiscountAmount;

        public decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set
            {
                _DiscountAmount = value;
                _DiscountPercentge = CalculateDiscountPercentge(_DiscountAmount);
                _TotalAmount = (_Price * NoOfNight) + _GSTAmount - _DiscountAmount;
                this.OnPropertyChanged("DiscountPercentge");
                this.OnPropertyChanged("DiscountAmount");
                this.OnPropertyChanged("TotalAmount");
            }
        }


        private decimal CalculateDiscountAmount(decimal DiscountPercentge)
        {
            decimal Amount = (NoOfNight * _Price) + _GSTAmount;
            if (DiscountPercentge > 0 && Amount > 0)
                return Math.Round((Amount * DiscountPercentge / 100), 4);
            else
                return 0;
            
        }

        private decimal CalculateDiscountPercentge(decimal DiscountAmount)
        {
            decimal Amount = (NoOfNight * _Price) + _GSTAmount;
            if (DiscountAmount > 0 && Amount > 0)
                return Math.Round((DiscountAmount / Amount * 100), 4);
            else
                return 0;
        }

    }
}
