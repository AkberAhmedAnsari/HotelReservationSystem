using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class AppRole
    {
        private int _RoleId;
        private string _Name;
        private bool _Create;
        private bool _Edit;
        private bool _Delete;
        private bool _Read;
        private bool _OtherAction;
        private int _ParentRoleId;
        private int _DataEntryUserId;
        private DateTime _DataEntryDate;
        private int _RecordStatus;

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
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }
        public bool Create
        {
            get
            {
                return _Create;
            }

            set
            {
                _Create = value;
            }
        }
        public bool Edit
        {
            get
            {
                return _Edit;
            }

            set
            {
                _Edit = value;
            }
        }
        public bool Delete
        {
            get
            {
                return _Delete;
            }

            set
            {
                _Delete = value;
            }
        }
        public bool Read
        {
            get
            {
                return _Read;
            }

            set
            {
                _Read = value;
            }
        }
        public bool OtherAction
        {
            get
            {
                return _OtherAction;
            }

            set
            {
                _OtherAction = value;
            }
        }
        public int ParentRoleId
        {
            get
            {
                return _ParentRoleId;
            }

            set
            {
                _ParentRoleId = value;
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
