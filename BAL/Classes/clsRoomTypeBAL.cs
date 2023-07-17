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
    public class clsRoomTypeBAL : RoomType
    {
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        public static ObservableCollection<clsRoomTypeBAL> GetRoomType()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptRoomType", new string[] { "@typeId" }, 1);
            return clsAppObject.DataTableToList<clsRoomTypeBAL>(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsRoomTypeBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsRoomTypeBAL _clsRoomTypeBAL)
        {
            if (Valadation(_clsRoomTypeBAL) == false)
                return false;
            if (inserRecordintDataTable(_clsRoomTypeBAL) == false)
                return false;
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsRoomTypeBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsRoomTypeBAL _clsRoomTypeBAL)
        {
            if (_clsRoomTypeBAL.RoomTypeName == "")
                throw new Exception("Please enter Room Type Name");
            if (_clsRoomTypeBAL.RoomPrice == 0)
                throw new Exception("Please enter Room Price");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsRoomTypeBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsRoomTypeBAL _clsRoomTypeBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _clsRoomTypeBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsRoomTypeBAL.RecordStatus = 1;
            _clsRoomTypeBAL.DataEntryDate = DateTime.Today;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                RoomType _clsRoomType = clsAppObject.Cast<RoomType>(_clsRoomTypeBAL);
                if (_clsRoomTypeBAL.RoomTypeId > 0)
                    clsAppObject.clsCore.UpdateRecord<RoomType>(_clsRoomType, "RoomTypeId", _clsRoomType.RoomTypeId.ToString(), trans, con);
                else
                    _clsRoomTypeBAL.RoomTypeId = clsAppObject.clsCore.InsertRecord<RoomType>(_clsRoomType, trans, con);

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
        /// 
        /// </summary>
        /// <param name="RoomTypeId"></param>
        /// <returns></returns>
        public static bool DeleteRoomType(int RoomTypeId)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptRoomType", new string[] { "@TypeId", "@RoomTypeId" }, 2, RoomTypeId);
            return vbol;
        }
    }
}
