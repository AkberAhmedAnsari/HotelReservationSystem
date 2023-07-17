using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class PaymentMode
    {
        private int _PaymentModeId;
        [PrimaryKey("This Is Primary Key")]
        public int PaymentModeId
        {
            get { return _PaymentModeId; }
            set { _PaymentModeId = value; }
        }

        public int BookingId { get; set; }
        public int PaymentModeTypeId { get; set; }
        public decimal Amount { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime? DataEntryDate { get; set; }
        public int RecordStatus { get; set; }

    }
}
