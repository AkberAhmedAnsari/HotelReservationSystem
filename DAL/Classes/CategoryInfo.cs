using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class CategoryInfo : BaseClass
    {
        private int _CategoryId;

        [PrimaryKey("This Is Primary Key")]
        public int CategoryId
        {
            get { return _CategoryId; }
            set
            {
                _CategoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string _CategoryCode;
        public string CategoryCode
        {
            get { return _CategoryCode; }
            set
            {
                _CategoryCode = value;
                OnPropertyChanged("CategoryCode");
            }
        }

        private string _CategoryName;
        public string CategoryName
        {
            get { return _CategoryName; }
            set
            {
                _CategoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }
        private int dataEntryUserId;

        public int DataEntryUserId
        {
            get
            {
                return dataEntryUserId;
            }

            set
            {
                dataEntryUserId = value;
                this.OnPropertyChanged("DataEntryUserId");
            }
        }

        private DateTime dataEntryDate;

        public DateTime DataEntryDate
        {
            get
            {
                return dataEntryDate;
            }

            set
            {
                dataEntryDate = value;
                this.OnPropertyChanged("DataEntryDate");
            }
        }

        private int recordStatus;
        public int RecordStatus
        {
            get
            {
                return recordStatus;
            }

            set
            {
                recordStatus = value;
                this.OnPropertyChanged("RecordStatus");
            }
        }


    }
}
