using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class ProductType : BaseClass
    {
        private int _ProductTypeId;
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }

        public int ProductTypeId
        {
            get
            {
                return _ProductTypeId;
            }
            set
            {
                _ProductTypeId = value;
                this.OnPropertyChanged("ProductTypeId");
            }
        }
    }
}
