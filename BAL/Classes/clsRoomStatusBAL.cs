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
    public class clsRoomStatusBAL
    {
        public int RoomStatusId { get; set; }
        public string RoomStatusName { get; set; }
        public string Description { get; set; }

        public static ObservableCollection<clsRoomStatusBAL> GetRoomStatus()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptRooms", new string[] { "@TypeId" }, 4);
            return clsAppObject.DataTableToList<clsRoomStatusBAL>(dt);
        }

        public static bool SaveLogic(RoomActivityStatus _RoomActivityStatus)
        {
            if (inserRecordintDataTable(_RoomActivityStatus) == false)
                return false;
            return true;
        }

        private static bool inserRecordintDataTable(RoomActivityStatus _RoomActivityStatus)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _RoomActivityStatus.RecordStatus = 1;
            _RoomActivityStatus.DataEntryDate = DateTime.Today;
            _RoomActivityStatus.DataEntryUserId = clsAppObject.LoginUser.userid;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();

                _RoomActivityStatus.RoomActivityStatusId = clsAppObject.clsCore.InsertRecord<RoomActivityStatus>(_RoomActivityStatus, trans, con);

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
