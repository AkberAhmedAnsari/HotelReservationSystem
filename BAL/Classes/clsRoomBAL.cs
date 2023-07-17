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
    public class clsRoomBAL : Room
    {
        private string _RoomTypeName;

        public string RoomTypeName
        {
            get { return _RoomTypeName; }
            set { _RoomTypeName = value; }
        }

        private string _RoomWithType;

        public string RoomWithType
        {
            get { return _RoomWithType; }
            set { _RoomWithType = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        
        private string _RoomStatusName;

        public string RoomStatusName
        {
            get { return _RoomStatusName; }
            set { _RoomStatusName = value; }
        }

        private int _RoomStatusId;

        public int RoomStatusId
        {
            get { return _RoomStatusId; }
            set { _RoomStatusId = value; }
        }
        private string _RoomActivityDate;

        public string RoomActivityDate
        {
            get { return _RoomActivityDate; }
            set { _RoomActivityDate = value; }
        }

        private string _ColorCode;

        public string ColorCode
        {
            get { return _ColorCode; }
            set { _ColorCode = value; }
        }


        private string _BookingNumber;

        public string BookingNumber
        {
            get { return _BookingNumber; }
            set { _BookingNumber = value; }
        }

        private string _CustomerName;

        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private string _NIC;

        public string NIC
        {
            get { return _NIC; }
            set { _NIC = value; }
        }

        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Remarks { get; set; }
        public DateTime BookingDate { get; set; }
        public int BookingId { get; set; }

        public decimal RoomPrice { get; set; }
        public decimal GSTPercentage { get; set; }
        public int PaymentModeId { get; set; }
        public decimal DiscountPercentge { get; set; }
        public decimal DiscountAmount { get; set; }

        public static ObservableCollection<clsRoomBAL> GetRooms()
        {
            DataTable dt = null;
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptRooms", new string[] { "@TypeId" }, 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                return clsAppObject.DataTableToList<clsRoomBAL>(dt);
            }
            else
                return null;
        }

        public static bool DeleteRooms(int RoomId)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptRooms", new string[] { "@TypeId", "@RoomsId" }, 2, RoomId);
            return vbol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsRoomBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsRoomBAL _clsRoomBAL)
        {
            if (Valadation(_clsRoomBAL) == false)
                return false;
            if (inserRecordintDataTable(_clsRoomBAL) == false)
                return false;
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsRoomBAL _clsRoomBAL)
        {
            if (_clsRoomBAL.RoomName == "")
                throw new Exception("Please enter Room Name");
            if (_clsRoomBAL.RoomTypeId == 0)
                throw new Exception("Please Select Room Type");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsRoomBAl"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsRoomBAL _clsRoomBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _clsRoomBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsRoomBAL.RecordStatus = 1;
            _clsRoomBAL.DataEntryDate = DateTime.Today;

             SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                Room _clsRoom = clsAppObject.Cast<Room>(_clsRoomBAL);
                if (_clsRoomBAL.RoomId > 0)
                    clsAppObject.clsCore.UpdateRecord<Room>(_clsRoom, "RoomId", _clsRoom.RoomId.ToString(), trans, con);
                else
                    _clsRoomBAL.RoomId = clsAppObject.clsCore.InsertRecord<Room>(_clsRoom, trans, con);

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

        /// <summary>
        /// get Rooms With status condation
        /// </summary>
        /// <param name="RoomTypeId">Int RoomTypeId </param>
        /// <returns>Collection clsRoomBAL</returns>
        public static ObservableCollection<clsRoomBAL> GetRoomsWithStatus(int RoomTypeId)
        {
            DataTable dt = null;
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptRooms", new string[] { "@TypeId", "@RoomTypeID" }, 3,RoomTypeId);
            if (dt != null && dt.Rows.Count > 0)
            {
                return clsAppObject.DataTableToList<clsRoomBAL>(dt);
            }
            else
                return null;
        }


    }

    public class RoomWithStatus : clsRoomTypeBAL
    {
        private ObservableCollection<clsRoomBAL> _RoomCollection;

        public ObservableCollection<clsRoomBAL> RoomCollection
        {
            get { return _RoomCollection; }
            set
            {
                _RoomCollection = value;
                OnPropertyChanged("RoomCollection");
            }
        }

        public string Name { get; set; }

    }
}
