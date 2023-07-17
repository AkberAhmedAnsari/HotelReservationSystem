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
    public class clsCityBAL : City
    {
        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }


        private string _CountryName;

        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }
        /// <summary> 
        /// get All City recordStatus 1 
        /// </summary>   
        /// <returns>All City return</returns>
        public static ObservableCollection<clsCityBAL> GetAllCity()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCity", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCityBAL>(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsCityBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsCityBAL clsCityBAL)
        {
            if (Valadation(clsCityBAL) == false)
                return false;
            if (inserRecordintDataTable(clsCityBAL) == false)
                return false;
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsCityBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsCityBAL clsCityBAL)
        {
            if (clsCityBAL.CityCode == "")
                throw new Exception("Please enter City Code");
            if (clsCityBAL.CityName.Trim() == "")
                throw new Exception("Please enter City Name");
            if (clsCityBAL.CountryId == 0)
                throw new Exception("Please select Country");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsCityBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsCityBAL clsCityBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            clsCityBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            clsCityBAL.RecordStatus = 1;
            clsCityBAL.DataEntryDate = DateTime.Today;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                City _clscity = clsAppObject.Cast<City>(clsCityBAL);
                if (clsCityBAL.CityId > 0)
                    clsAppObject.clsCore.UpdateRecord<City>(_clscity, "CityId", _clscity.CityId.ToString(), trans, con);
                else
                    clsCityBAL.CityId = clsAppObject.clsCore.InsertRecord<City>(_clscity, trans, con);

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
        /// <param name="CityId"></param>
        /// <returns></returns>
        public static bool DeleteCountry(int CityId)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptCity", new string[] { "@TypeId", "@CityId" }, 2, CityId);
            return vbol;
        }
    }
}

