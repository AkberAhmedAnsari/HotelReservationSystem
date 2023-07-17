using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class CustomerType : BaseClass
    {
        private int _CustomerTypeId;
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }

        public int CustomerTypeId
        {
            get
            {
                return _CustomerTypeId;
            }

            set
            {
                _CustomerTypeId = value;
                this.OnPropertyChanged("CustomerTypeId");
            }
        }
    }
}
