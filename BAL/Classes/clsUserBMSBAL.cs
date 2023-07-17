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
    public class clsUserBMSBAL : AppUser
    {
        private string ConfirmPassword;

        public string _ConfirmPassword
        {
            get { return ConfirmPassword; }
            set { ConfirmPassword = value; }
        }

        private string _DataEntryUserName;

        public string DataEntryUserName
        {
            get { return _DataEntryUserName; }
            set { _DataEntryUserName = value; }
        }

        public string _UserTypeName;
        public string UserTypeName
        {
            get { return _UserTypeName; }
            set
            {
                _UserTypeName = value;
                OnPropertyChanged("UserTypeName");
            }
        }

        public static clsUserBMSBAL GetUser(string loginId , string Password)
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptUser", new string[] { "@TypeId", "@loginId", "@Password" }, 1, loginId, Password);
            if (dt != null && dt.Rows.Count > 0)
                return clsAppObject.DataTableToList<clsUserBMSBAL>(dt)[0];
            else
                return null;
        }

        /// <summary>
        /// get All Active CategoryInfo
        /// </summary>
        /// <returns>Return CategoryInfo</returns>
        public static ObservableCollection<clsUserBMSBAL> getCategoryInfo()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCategoryInfo", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsUserBMSBAL>(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsUserBMSBAL _clsUserBMSBAL)
        {
            if (Valadation(_clsUserBMSBAL) == false)
                return false;
            if (inserRecordintDataTable(_clsUserBMSBAL) == false)
                return false;
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsUserBMSBAL _clsUserBMSBAL)
        {
            if (_clsUserBMSBAL.username == "")
                throw new Exception("Please enter User Name");
            if (_clsUserBMSBAL.loginId == "")
                throw new Exception("Please enter login Id");
            if (_clsUserBMSBAL.password == "")
                throw new Exception("Please enter User Name");
            if (_clsUserBMSBAL.ConfirmPassword == "")
                throw new Exception("Please enter Confirm Password.");
            if (_clsUserBMSBAL.password != _clsUserBMSBAL.ConfirmPassword)
                throw new Exception("Password does not match the confirm password.");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsUserBMSBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsUserBMSBAL _clsUserBMSBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _clsUserBMSBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsUserBMSBAL.RecordStatus = 1;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                AppUser _UserBMS = clsAppObject.Cast<AppUser>(_clsUserBMSBAL);
                if (_clsUserBMSBAL.userid > 0)
                    clsAppObject.clsCore.UpdateRecord<AppUser>(_UserBMS, "userid", _clsUserBMSBAL.userid.ToString(), trans, con);
                else
                    _clsUserBMSBAL.userid = clsAppObject.clsCore.InsertRecord<AppUser>(_UserBMS, trans, con);
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
        /// <returns></returns>
        public static ObservableCollection<clsUserBMSBAL> getRecords()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptUser", new string[] { "@TypeId" }, 2);
            return clsAppObject.DataTableToList<clsUserBMSBAL>(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productItemId"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int UserID)
        {
            bool value = false;
            clsAppObject.clsCore.ExecuteScaler(ref value, "SptUser", new string[] { "@TypeId", "@UserID" }, 3, UserID);
            return value;
        }
    }

    public class UserTypeBAL
    {
        private int _UserTypeId;

        public int UserTypeId
        {
            get { return _UserTypeId; }
            set { _UserTypeId = value; }
        }

        public string _UserTypeName;
        public string UserTypeName
        {
            get { return _UserTypeName; }
            set { _UserTypeName = value; }
        }

        public static ObservableCollection<UserTypeBAL> getUserType()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptUser", new string[] { "@TypeId" }, 4);
            return clsAppObject.DataTableToList<UserTypeBAL>(dt);
        }

    }
}
