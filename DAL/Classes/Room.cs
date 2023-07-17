using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class Room
    {
        private int _RoomId;

        [PrimaryKey("This Is Primary Key")]
        public int RoomId
        {
            get { return _RoomId; }
            set { _RoomId = value; }
        }
        public string RoomName { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomDescription { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime? DataEntryDate { get; set; }
        public int RecordStatus { get; set; }

    }
}
