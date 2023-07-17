using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class RoomType : BaseClass
    {
        private int _RoomTypeId;

        [PrimaryKey("This Is Primary Key")]
        public int RoomTypeId
        {
            get { return _RoomTypeId; }
            set { _RoomTypeId = value; }
        }

        public string RoomTypeName { get; set; }
        public string Description { get; set; }
        public decimal RoomPrice { get; set; }
        public decimal GSTPercentage { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }
    }
}
