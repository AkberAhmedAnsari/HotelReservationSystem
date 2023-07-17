using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class City : BaseClass
    {
        private int _CityId;
        public int CountryId { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }
        [PrimaryKey("This Is Primary Key")]
        public int CityId
        {
            get
            {
                return _CityId;
            }

            set
            {
                _CityId = value;
                this.OnPropertyChanged("CityId");
            }
        }
    }
}
