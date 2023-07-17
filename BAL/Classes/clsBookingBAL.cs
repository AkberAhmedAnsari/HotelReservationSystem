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
    public class clsBookingBAL : Booking
    {
        private string _FirstName;
        private string _MobileNumber;
        private string _NIC;
        private string _RoomTypeName;
        private string _RoomName;
        private int _NoOfDays;
        private decimal _BalanceAmount;
        private decimal _DepositedAmount;
        private decimal? _PaymentAmount;
        private decimal _NetAmount;
        private decimal _Amount;
        private string _CustomerTypeName;
        private string _UserName;
        private string _CountryName;
        private string _CityName;
        private string _Address;
        public string FirstName
        {
            get
            {
                return _FirstName;
            }

            set
            {
                _FirstName = value;
                this.OnPropertyChanged("FirstName");
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
        public string RoomTypeName
        {
            get
            {
                return _RoomTypeName;
            }

            set
            {
                _RoomTypeName = value;
                this.OnPropertyChanged("RoomTypeName");
            }
        }
        public string RoomName
        {
            get
            {
                return _RoomName;
            }

            set
            {
                _RoomName = value;
                this.OnPropertyChanged("RoomName");
            }
        }
        public int NoOfDays
        {
            get
            {
                return _NoOfDays;    
            }
            set
            {
                _NoOfDays = value;
                NoOfNight = _NoOfDays;
                this.Amount = CalculateTotal(_NoOfDays, this.Price);
                if (GSTPercentage > 0)
                    this.GSTAmount = CalculateGstAmount(GSTPercentage, this.Amount);
                NetAmount = this.Amount + this.GSTAmount;
                DiscountAmount = DiscountAmount;
            }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                this.OnPropertyChanged("Amount");
            }
        }
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set
            {
                _NetAmount = value;
                this.OnPropertyChanged("NetAmount");
            }
        }
        public decimal? PaymentAmount
        {
            get { return _PaymentAmount; }
            set
            {
                _PaymentAmount = value;
                this.OnPropertyChanged("PaymentAmount");
            }
        }
        public decimal BalanceAmount
        {
            get { return _BalanceAmount; }
            set
            {
                _BalanceAmount = value;
                this.OnPropertyChanged("BalanceAmount");
            }
        }
        public decimal DepositedAmount
        {
            get { return _DepositedAmount; }
            set
            {
                _DepositedAmount = value;
                this.OnPropertyChanged("DepositedAmount");
            }
        }
        public int RoomStatusId { get; set; }
        public int PaymentModeId { get; set; }
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

        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                this.OnPropertyChanged("Address");
            }
        }
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set
            {
                _MobileNumber = value;
                this.OnPropertyChanged("MobileNumber");
            }
        }

        public string RoomStatusName { get; set; }
        private decimal CalculateTotal(decimal NoOfNight, decimal Price)
        {
            return (NoOfNight * Price);
        }

        private decimal CalculateGstAmount(decimal GstPer, decimal Total)
        {
            return Math.Round((Total * GstPer / 100), 4);
        }
        public static string GetNextBookingNumber()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptBooking", new string[] { "@TypeId" }, 1);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "0";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static ObservableCollection<clsBookingBAL> GetBookingData(DateTime StartDate, DateTime EndDate)
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptBooking", new string[] { "@TypeId", "@StartDate", "@EndDate" }, 4, StartDate, EndDate);
            return clsAppObject.DataTableToList<clsBookingBAL>(dt);
        }

        public static ObservableCollection<clsBookingBAL> GetBookingData()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptBooking", new string[] { "@TypeId" }, 2);
            return clsAppObject.DataTableToList<clsBookingBAL>(dt);
        }

        /// <summary>
        /// Cancel Reservation Delete
        /// </summary>
        /// <param name="BookingId">Int</param>
        /// <returns>bool</returns>
        public static bool CancelReservation(int BookingId)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptBooking", new string[] { "@TypeId", "@BookingId" }, 3, BookingId);
            return vbol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsBookingBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsBookingBAL _clsBookingBAL)
        {
            if (Valadation(_clsBookingBAL) == false)
                return false;
            if (inserRecordintDataTable(_clsBookingBAL) == false)
                return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsRoomBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsBookingBAL _clsBookingBAL)
        {
            if (_clsBookingBAL.CustomerId == 0)
                throw new Exception("Please Select Client");
            if (_clsBookingBAL.RoomIds == "")
                throw new Exception("Please select Room");
            if (_clsBookingBAL.CheckInDate == null)
                throw new Exception("Please select Check In Date");
            if (_clsBookingBAL.CheckOutDate == null)
                throw new Exception("Please Select Check Out Date");
            if (_clsBookingBAL.RoomStatusId == 16 && _clsBookingBAL.BalanceAmount > 0)
                throw new Exception("Please Enter Valid Payment Amount");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsBookingBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsBookingBAL _clsBookingBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _clsBookingBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsBookingBAL.RecordStatus = 1;
            _clsBookingBAL.BookingDate = DateTime.Today;
            _clsBookingBAL.DataEntryDate = DateTime.Today;

            PaymentMode _PaymentMode = new PaymentMode();
            _PaymentMode.RecordStatus = 1;
            _PaymentMode.DataEntryDate = DateTime.Today;
            _PaymentMode.PaymentModeId = _clsBookingBAL.PaymentModeId;
            _PaymentMode.DataEntryUserId = clsAppObject.LoginUser.userid;
            _PaymentMode.Amount = _clsBookingBAL.PaymentAmount != null? Convert.ToDecimal(_clsBookingBAL.PaymentAmount) : 0;
            if (_clsBookingBAL.RoomStatusId == 1)
                _PaymentMode.PaymentModeTypeId = 2;
            else
                _PaymentMode.PaymentModeTypeId = 1;

            RoomActivityStatus _RoomActivityStatus = new RoomActivityStatus();
            _RoomActivityStatus.RoomId = Convert.ToInt32(_clsBookingBAL.RoomIds);
            _RoomActivityStatus.RoomStatusId = _clsBookingBAL.RoomStatusId;
            _RoomActivityStatus.RecordStatus = 1;
            _RoomActivityStatus.DataEntryDate = DateTime.Today;
            _RoomActivityStatus.DataEntryUserId = clsAppObject.LoginUser.userid;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                con.Open();
                trans = con.BeginTransaction();
                Booking _clsBooking = clsAppObject.Cast<Booking>(_clsBookingBAL);
                _clsBooking.NoOfNight = _clsBookingBAL.NoOfDays;
                if (_clsBooking.BookingId > 0)
                {
                    clsAppObject.clsCore.UpdateRecord<Booking>(_clsBooking, "BookingId", _clsBookingBAL.BookingId.ToString(), trans, con);
                    _PaymentMode.BookingId = _clsBooking.BookingId;
                    if (_clsBookingBAL.RoomStatusId == 16)
                        clsAppObject.clsCore.InsertRecord<PaymentMode>(_PaymentMode, trans, con);
                    else
                        clsAppObject.clsCore.UpdateRecord<PaymentMode>(_PaymentMode, "PaymentModeId", _PaymentMode.PaymentModeId.ToString(), trans, con);
                    _RoomActivityStatus.BookingId = _clsBooking.BookingId;
                    clsAppObject.clsCore.InsertRecord<RoomActivityStatus>(_RoomActivityStatus, trans, con);
                }
                else
                {
                    _clsBooking.BookingNumber = GetNextBookingNumber();
                    _clsBooking.BookingId = clsAppObject.clsCore.InsertRecord<Booking>(_clsBooking, trans, con);
                    _clsBookingBAL.BookingId = _clsBooking.BookingId;
                    _PaymentMode.BookingId = _clsBooking.BookingId;
                    clsAppObject.clsCore.InsertRecord<PaymentMode>(_PaymentMode, trans, con);
                    _RoomActivityStatus.BookingId = _clsBooking.BookingId;
                    clsAppObject.clsCore.InsertRecord<RoomActivityStatus>(_RoomActivityStatus, trans, con);
                }
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
    }
}
