using DAL.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Classes
{
    public class clsCustomerBAL : Customers
    {
        private int _MobileNumberId;
        private int _MobileNumberTypeId;
        private string _MobileNumber;
        private int _HomeNumberId;
        private int _HomeNumberTypeId;
        private string _HomeNumber;
        private string _Address;
        private int _CountryId;
        private int _CityId;
        private string _TitleName;
        private string _CustomerTypeName;
        private string _UserName;
        private string _CountryName;
        private string _CityName;
        private int _ContactAddressId;

        public string MobileNumber
        {
            get { return _MobileNumber; }
            set
            {
                _MobileNumber = value;
                this.OnPropertyChanged("MobileNumber");
            }
        }
        public string HomeNumber
        {
            get { return _HomeNumber; }
            set
            {
                _HomeNumber = value;
                this.OnPropertyChanged("HomeNumber");
            }
        }
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                this.OnPropertyChanged("Address");
            }
        }
        public int CountryId
        {
            get { return _CountryId; }
            set
            {
                _CountryId = value;
                this.OnPropertyChanged("CountryId");
            }
        }
        public int CityId
        {
            get { return _CityId; }
            set
            {
                _CityId = value;
                this.OnPropertyChanged("CityId");
            }
        }

        public int MobileNumberId
        {
            get
            {
                return _MobileNumberId;
            }

            set
            {
                _MobileNumberId = value;
                this.OnPropertyChanged("MobileNumberId");
            }
        }

        public int MobileNumberTypeId
        {
            get
            {
                return _MobileNumberTypeId;
            }

            set
            {
                _MobileNumberTypeId = value;
                this.OnPropertyChanged("MobileNumberTypeId");
            }
        }

        public int HomeNumberId
        {
            get
            {
                return _HomeNumberId;
            }

            set
            {
                _HomeNumberId = value;
                this.OnPropertyChanged("HomeNumberId");
            }
        }

        public int HomeNumberTypeId
        {
            get
            {
                return _HomeNumberTypeId;
            }

            set
            {
                _HomeNumberTypeId = value;
                this.OnPropertyChanged("HomeNumberTypeId");
            }
        }

        public string TitleName
        {
            get
            {
                return _TitleName;
            }

            set
            {
                _TitleName = value;
            }
        }

        public string CustomerTypeName
        {
            get
            {
                return _CustomerTypeName;
            }

            set
            {
                _CustomerTypeName = value;
            }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
            }
        }

        public string CountryName
        {
            get
            {
                return _CountryName;
            }

            set
            {
                _CountryName = value;
            }
        }

        public string CityName
        {
            get
            {
                return _CityName;
            }

            set
            {
                _CityName = value;
            }
        }

        public int ContactAddressId
        {
            get
            {
                return _ContactAddressId;
            }

            set
            {
                _ContactAddressId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsCustomerBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsCustomerBAL _clsCustomerBAL)
        {
            if (Valadation(_clsCustomerBAL) == false)
                return false;
            if (inserRecordintDataTable(_clsCustomerBAL) == false)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsCustomerBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsCustomerBAL _clsCustomerBAL)
        {
            if (_clsCustomerBAL.TitleTypeId == 0)
                throw new Exception("Please Select Title");
            if (string.IsNullOrEmpty(_clsCustomerBAL.FirstName))
                throw new Exception("Please enter Customer Name");
            if (_clsCustomerBAL.CustomerTypeId == 0)
                throw new Exception("Please Select Customer Type");
            if (string.IsNullOrEmpty(_clsCustomerBAL.NIC))
                throw new Exception("Please enter NIC");
            if (string.IsNullOrEmpty(_clsCustomerBAL.MobileNumber))
                throw new Exception("Please enter Mobile Number");
            if (string.IsNullOrEmpty(_clsCustomerBAL.Address))
                throw new Exception("Please enter Address");
            if (_clsCustomerBAL.CityId == 0)
                throw new Exception("Please Select City");
            if (_clsCustomerBAL.CountryId == 0)
                throw new Exception("Please Select Country");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsCustomerBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsCustomerBAL _clsCustomerBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _clsCustomerBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsCustomerBAL.RecordStatus = 1;
            ContactNumber _HomeNumber = new ContactNumber();
            _HomeNumber.ContactNumberTypeId = 1;
            _HomeNumber.Number = _clsCustomerBAL.HomeNumber;
            _HomeNumber.DataEntryUserId = _clsCustomerBAL.DataEntryUserId;
            _HomeNumber.RecordStatus = 1;

            ContactNumber _MobileNumber = new ContactNumber();
            _MobileNumber.ContactNumberTypeId = 2;
            _MobileNumber.Number = _clsCustomerBAL.MobileNumber;
            _MobileNumber.DataEntryUserId = _clsCustomerBAL.DataEntryUserId;
            _MobileNumber.RecordStatus = 1;

            ContactAddress _clsContactAddress = new ContactAddress();
            _clsContactAddress.AddressTypeId = 1;
            _clsContactAddress.AddressDetail = _clsCustomerBAL.Address;
            _clsContactAddress.CityId = _clsCustomerBAL.CityId;
            _clsContactAddress.CountryId = _clsCustomerBAL.CountryId;
            _clsContactAddress.DataEntryUserId = _clsCustomerBAL.DataEntryUserId;
            _clsContactAddress.RecordStatus = 1;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                Customers _clsCustomer = clsAppObject.Cast<Customers>(_clsCustomerBAL);
                if (_clsCustomerBAL.CustomerId > 0)
                    clsAppObject.clsCore.UpdateRecord<Customers>(_clsCustomer, "CustomerId", _clsCustomer.CustomerId.ToString(), trans, con);
                else
                    _clsCustomer.CustomerId = clsAppObject.clsCore.InsertRecord<Customers>(_clsCustomer, trans, con);

                _HomeNumber.CustomerId = _clsCustomer.CustomerId;
                _MobileNumber.CustomerId = _clsCustomer.CustomerId;
                _clsContactAddress.CustomerId = _clsCustomer.CustomerId;

                if (_clsCustomerBAL.HomeNumberId > 0)
                    clsAppObject.clsCore.UpdateRecord<ContactNumber>(_HomeNumber, "ContactNumberId", _clsCustomerBAL.HomeNumberId.ToString(), trans, con);
                else
                {
                    if (_HomeNumber.Number != null && _HomeNumber.Number != "")
                        _clsCustomerBAL._HomeNumberId = clsAppObject.clsCore.InsertRecord<ContactNumber>(_HomeNumber, trans, con);
                }

                if (_clsCustomerBAL.MobileNumberId > 0)
                    clsAppObject.clsCore.UpdateRecord<ContactNumber>(_MobileNumber, "ContactNumberId", _clsCustomerBAL.MobileNumberId.ToString(), trans, con);
                else
                {
                    if (_MobileNumber.Number != null && _MobileNumber.Number != "")
                        _clsCustomerBAL.MobileNumberId = clsAppObject.clsCore.InsertRecord<ContactNumber>(_MobileNumber, trans, con);
                }

                if (_clsCustomerBAL.ContactAddressId > 0)
                    clsAppObject.clsCore.UpdateRecord<ContactAddress>(_clsContactAddress, "ContactAddressId", _clsCustomerBAL.ContactAddressId.ToString(), trans, con);
                else
                    _clsCustomerBAL.ContactAddressId = clsAppObject.clsCore.InsertRecord<ContactAddress>(_clsContactAddress, trans, con);

                trans.Commit();
                con.Close();
                vbol = true;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    trans.Rollback();
                    con.Close();
                }
                throw new Exception(ex.Message);
            }

            return vbol;

        }

        public static ObservableCollection<clsCustomerBAL> getRecords()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCustomer", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCustomerBAL>(dt);
        }

        public static clsCustomerBAL getRecords(int CustomerId)
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCustomer", new string[] { "@TypeId" , "@CustomerId" }, 2, CustomerId);
            return clsAppObject.DataTableToList<clsCustomerBAL>(dt)[0];
        }
        public static bool DeleteCustomer(int CustomerId)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptCustomer", new string[] { "@TypeId", "@CustomerId" }, 3, CustomerId);
            return vbol;
        }
    }
}
