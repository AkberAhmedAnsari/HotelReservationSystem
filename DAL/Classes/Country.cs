using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class Country : BaseClass
    {
        private int _CountrId;
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }

        [PrimaryKey("This Is Primary Key")]
        public int CountryId
        {
            get
            {
                return _CountrId;
            }

            set
            {
                _CountrId = value;
                this.OnPropertyChanged("CountryId");
            }
        }
    }
}
