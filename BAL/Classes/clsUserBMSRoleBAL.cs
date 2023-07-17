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
    public class clsUserBMSRoleBAL : AppUserRole
    {

        public static bool SaveLogic(ObservableCollection<clsBMSRoleBAL> _BMSRoleCollection, ObservableCollection<clsBMSRoleBAL> _BMSDeleteRoleCollection,int userId)
        {
            if (inserRecordintDataTable(_BMSRoleCollection, _BMSDeleteRoleCollection,userId) == false)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsUserBMSBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(ObservableCollection<clsBMSRoleBAL> _BMSRoleCollection, ObservableCollection<clsBMSRoleBAL> _BMSDeleteRoleCollection,int userId)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);


            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                foreach (var item in _BMSRoleCollection)
                {
                    AppUserRole _UserBMSRole = new AppUserRole();
                    _UserBMSRole.DataEntryUserId = clsAppObject.LoginUser.userid;
                    _UserBMSRole.RecordStatus = 1;
                    _UserBMSRole.UserId = userId;
                    _UserBMSRole.RoleId = item.RoleId;
                    clsAppObject.clsCore.InsertRecord<AppUserRole>(_UserBMSRole, trans, con);
                }

                foreach (var item in _BMSDeleteRoleCollection)
                {
                    DeleteUserRole(trans, con, item.UserRoleID);
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

        public static bool DeleteUserRole(SqlTransaction trans, SqlConnection con, int UserRoleId)
        {
            bool value = false;
            clsAppObject.clsCore.ExecuteScaler(ref value, trans, con, "SptUserRole", new string[] { "@TypeId", "UserRoleID" }, 3, UserRoleId);
            return value;
        }
    }
}
