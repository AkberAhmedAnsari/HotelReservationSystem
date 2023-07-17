using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class AppUserRole
    {
        private int _UserRoleID;
        private int _RoleId;
        private int _UserId;
        private int _DataEntryUserId;
        private DateTime _DataEntryDate;
        private int _RecordStatus;

        [PrimaryKey("This Is Primary Key")]
        public int UserRoleID
        {
            get
            {
                return _UserRoleID;
            }

            set
            {
                _UserRoleID = value;
            }
        }

        public int RoleId
        {
            get
            {
                return _RoleId;
            }

            set
            {
                _RoleId = value;
            }
        }

        public int UserId
        {
            get
            {
                return _UserId;
            }

            set
            {
                _UserId = value;
            }
        }

        public int DataEntryUserId
        {
            get
            {
                return _DataEntryUserId;
            }

            set
            {
                _DataEntryUserId = value;
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
            }
        }
    }
}
