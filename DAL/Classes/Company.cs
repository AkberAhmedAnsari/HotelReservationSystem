using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class Company : BaseClass
    {
        private int _CompanyId;
        public string CompanyName { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }

        public int CompanyId
        {
            get
            {
                return _CompanyId;
            }

            set
            {
                _CompanyId = value;
                this.OnPropertyChanged("CompanyId");
            }
        }
    }
}
