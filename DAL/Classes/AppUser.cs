using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class AppUser : BaseClass
    {
   
        private int _userid;

        [PrimaryKey("This Is Primary Key")]
        public int userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        private string _loginId;
        public string loginId
        {
            get { return _loginId; }
            set { _loginId = value; }
        }

        private string _username;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        private int? _empid;
        public int? empid
        {
            get { return _empid; }
            set { _empid = value; }
        }

        private int? _branchId;
        public int? branchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private int _DataEntryUserId;
        private DateTime _DataEntryDate;
        private int _RecordStatus;

        public int DataEntryUserId
        {
            get
            {
                return _DataEntryUserId;
            }

            set
            {
                _DataEntryUserId = value;
                OnPropertyChanged("DataEntryUserId");
            }
        }

        public DateTime DataEntryDate
        {
            get
            {
                return _DataEntryDate;
            }

            set
            {
                _DataEntryDate = value;
                OnPropertyChanged("DataEntryDate");
            }
        }

        public int RecordStatus
        {
            get
            {
                return _RecordStatus;
            }

            set
            {
                _RecordStatus = value;
                OnPropertyChanged("RecordStatus");
            }
        }

        private int _UserTypeId;

        public int UserTypeId
        {
            get { return _UserTypeId; }
            set
            {
                _UserTypeId = value;
                OnPropertyChanged("UserTypeId");
            }
        }


    }
}
