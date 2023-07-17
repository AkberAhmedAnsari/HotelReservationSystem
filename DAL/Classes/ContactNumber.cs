using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class ContactNumber
    {
        [PrimaryKey("This Is Primary Key")]
        public int ContactNumberId { get; set; }
        public int ContactNumberTypeId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployId { get; set; }
        public int? CompanyId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }
    }
}
