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
    public class clsCountryBAL : Country
    {
        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// get All Country recordStatus 1 
        /// </summary>
        /// <returns>All Country return</returns>
        public static ObservableCollection<clsCountryBAL> GetAllCountry()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCountry", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCountryBAL>(dt);
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsCountryBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsCountryBAL clsCountryBAL)
        {
            if (Valadation(clsCountryBAL) == false)
                return false;
            if (inserRecordintDataTable(clsCountryBAL) == false)
                return false;
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsCountryBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsCountryBAL clsCountryBAL)
        {
            if (clsCountryBAL.CountryCode == "")
                throw new Exception("Please enter Country Code");
            if (clsCountryBAL.CountryName.Trim() == "")
                throw new Exception("Please enter Country Name");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsCountryBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsCountryBAL clsCountryBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            clsCountryBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            clsCountryBAL.RecordStatus = 1;
            clsCountryBAL.DataEntryDate = DateTime.Today;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                Country _clsCountry = clsAppObject.Cast<Country>(clsCountryBAL);
                if (clsCountryBAL.CountryId > 0)
                    clsAppObject.clsCore.UpdateRecord<Country>(_clsCountry, "CountryId", _clsCountry.CountryId.ToString(), trans, con);
                else
                    clsCountryBAL.CountryId = clsAppObject.clsCore.InsertRecord<Country>(_clsCountry, trans, con);

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
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public static bool DeleteCountry(int CountryId)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptCountry", new string[] { "@TypeId", "@CountryId" }, 2, CountryId);
            return vbol;
        }
    }
}
