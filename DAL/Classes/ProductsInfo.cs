using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class ProductsInfo : BaseClass
    {
       
        private int _productItemId;

        [PrimaryKey("This Is Primary Key")]
        public int productItemId
        {
            get { return _productItemId; }
            set
            {
                _productItemId = value;
                OnPropertyChanged("productItemId");
            }
        }

        private string _productItemcode;
        public string productItemcode
        {
            get { return _productItemcode; }
            set
            {
                _productItemcode = value;
                OnPropertyChanged("productItemcode");
            }
        }

        private string _productItemname;
        public string productItemname
        {
            get { return _productItemname; }
            set
            {
                _productItemname = value;
                OnPropertyChanged("productItemname");
            }
        }

        private int _CategoryId;
        public int CategoryId
        {
            get { return _CategoryId; }
            set
            {
                _CategoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private int _ProductTypeId;
        public int ProductTypeId
        {
            get { return _ProductTypeId; }
            set
            {
                _ProductTypeId = value;
                OnPropertyChanged("ProductTypeId");
            }
        }

        private string _prodescription;
        public string prodescription
        {
            get { return _prodescription; }
            set
            {
                _prodescription = value;
                OnPropertyChanged("prodescription");
            }
        }

        private int? _parentid;
        public int? parentid
        {
            get { return _parentid; }
            set
            {
                _parentid = value;
                OnPropertyChanged("parentid");
            }
        }

        private string _serialize;
        public string serialize
        {
            get { return _serialize; }
            set
            {
                _serialize = value;
                OnPropertyChanged("serialize");
            }
        }

        private string _barcode;
        public string barcode
        {
            get { return _barcode; }
            set
            {
                _barcode= value;
                OnPropertyChanged("barcode");
            }
        }

        private decimal _costprice;
        public decimal costprice
        {
            get { return _costprice; }
            set
            {
                _costprice = value;
                OnPropertyChanged("costprice");
            }
        }

        private decimal _saleprice;
        public decimal saleprice
        {
            get { return _saleprice; }
            set
            {
                _saleprice = value;
                OnPropertyChanged("saleprice");
            }
        }

        private int _DataEntryUserId;
        public int DataEntryUserId
        {
            get { return _DataEntryUserId; }
            set
            {
                _DataEntryUserId = value;
                OnPropertyChanged("DataEntryUserId");
            }
        }

        private DateTime _DataEntryDate;
        public DateTime DataEntryDate
        {
            get { return _DataEntryDate; }
            set
            {
                _DataEntryDate = value;
                OnPropertyChanged("DataEntryDate");
            }
        }

        private int _RecordStatus;
        public int RecordStatus
        {
            get { return _RecordStatus; }
            set
            {
                _RecordStatus = value;
                OnPropertyChanged("RecordStatus");
            }
        }
    }
}
