using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class ContactAddress
    {
        [PrimaryKey("This Is Primary Key")]
        public int ContactAddressId { get; set; }
        public int AddressTypeId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployId { get; set; }
        public int? CompanyId { get; set; }
        public string AddressDetail { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int PostalCode { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }
    }
}
