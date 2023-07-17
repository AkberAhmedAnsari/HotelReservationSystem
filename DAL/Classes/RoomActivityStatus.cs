using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class RoomActivityStatus : BaseClass
    {
        private int _RoomActivityStatusId;

        [PrimaryKey("This Is Primary Key")]
        public int RoomActivityStatusId
        {
            get { return _RoomActivityStatusId; }
            set { _RoomActivityStatusId = value; }
        }

        private int _RoomId;
        public int RoomId
        {
            get { return _RoomId; }
            set { _RoomId = value; }
        }

        private int _RoomStatusId;
        public int RoomStatusId
        {
            get { return _RoomStatusId; }
            set { _RoomStatusId = value; }
        }

        private int? _BookingId;
        public int? BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public int DataEntryUserId { get; set; }
        public DateTime? DataEntryDate { get; set; }
        public int RecordStatus { get; set; }
    }
}
