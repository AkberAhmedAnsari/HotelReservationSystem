using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class BMSImage : BaseClass
    {
        private int _ImageId;

        [PrimaryKey("This Is Primary Key")]
        public int ImageId
        {
            get { return _ImageId; }
            set
            {
                _ImageId = value;
                this.OnPropertyChanged("ImageId");
            }
        }

        private string _ImageName;
        public String ImageName
        {
            get { return _ImageName; }
            set
            {
                _ImageName = value;
                this.OnPropertyChanged("ImageName");
            }
        }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
                this.OnPropertyChanged("ImagePath");
            }
        }

        private Byte[] _RecordImage;
        public Byte[] RecordImage
        {
            get { return _RecordImage; }
            set
            {
                _RecordImage = value;
                this.OnPropertyChanged("RecordImage");
            }
        }

        private int? _ProductItemId;
        public int? ProductItemId
        {
            get { return _ProductItemId; }
            set
            {
                _ProductItemId = value;
                this.OnPropertyChanged("ProductItemId");
            }
        }

        private int? _EmployeeId;
        public int? EmployeeId
        {
            get { return _EmployeeId; }
            set
            {
                _EmployeeId = value;
                this.OnPropertyChanged("EmployeeId");
            }
        }

        private DateTime _DataEntryDate;
        public DateTime DataEntryDate
        {
            get { return _DataEntryDate; }
            set
            {
                _DataEntryDate = value;
                this.OnPropertyChanged("DataEntryDate");
            }
        }

        private int _RecordStatus;
        public int RecordStatus
        {
            get { return _RecordStatus; }
            set
            {
                _RecordStatus = value;
                this.OnPropertyChanged("RecordStatus");
            }
        }

    }
}
