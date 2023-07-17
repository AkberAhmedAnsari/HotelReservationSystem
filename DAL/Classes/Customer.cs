using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class Customers : BaseClass
    {
        private int customerId;
        private int titleTypeId;
        private string firstName;
        private string lastName;
        private string _NIC;
        private int customerTypeId;
        private int dataEntryUserId;
        private DateTime dataEntryDate;
        private int recordStatus;

        [PrimaryKey("This Is Primary Key")]
        public int CustomerId
        {
            get
            {
                return customerId;
            }

            set
            {
                customerId = value;
                this.OnPropertyChanged("customerId");
            }
        }

        public int TitleTypeId
        {
            get
            {
                return titleTypeId;
            }

            set
            {
                titleTypeId = value;
                this.OnPropertyChanged("TitleTypeId");
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

        public string NIC
        {
            get
            {
                return _NIC;
            }

            set
            {
                _NIC = value;
                this.OnPropertyChanged("NIC");
            }
        }
        public int CustomerTypeId
        {
            get
            {
                return customerTypeId;
            }

            set
            {
                customerTypeId = value;
                this.OnPropertyChanged("CustomerTypeId");
            }
        }

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
