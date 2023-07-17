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
    public class clsCategoryInfoBAL : CategoryInfo
    {
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// get All Active CategoryInfo
        /// </summary>
        /// <returns>Return CategoryInfo</returns>
        public static ObservableCollection<clsCategoryInfoBAL> getCategoryInfo()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCategoryInfo", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCategoryInfoBAL>(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsCategoryInfoBAL _clsCategoryInfoBAL)
        {
            if (Valadation(_clsCategoryInfoBAL) == false)
                return false;
            if (inserRecordintDataTable(_clsCategoryInfoBAL) == false)
                return false;
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsCategoryInfoBAL _clsCategoryInfoBAL)
        {
            if (_clsCategoryInfoBAL.CategoryName == "")
                throw new Exception("Please enter Category Name");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsCategoryInfoBAL _clsCategoryInfoBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _clsCategoryInfoBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsCategoryInfoBAL.RecordStatus = 1;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                CategoryInfo _CategoryInfo = clsAppObject.Cast<CategoryInfo>(_clsCategoryInfoBAL);
                if (_clsCategoryInfoBAL.CategoryId > 0)
                    clsAppObject.clsCore.UpdateRecord<CategoryInfo>(_CategoryInfo, "CategoryId", _clsCategoryInfoBAL.CategoryId.ToString(), trans, con);
                else
                    _clsCategoryInfoBAL.CategoryId = clsAppObject.clsCore.InsertRecord<CategoryInfo>(_CategoryInfo, trans, con);
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
        public static ObservableCollection<clsCategoryInfoBAL> getRecords()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCategoryInfo", new string[] { "@TypeId" }, 2);
            return clsAppObject.DataTableToList<clsCategoryInfoBAL>(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productItemId"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int CategoryId)
        {
            bool value = false;
            clsAppObject.clsCore.ExecuteScaler(ref value, "SptCategoryInfo", new string[] { "@TypeId", "@CategoryId" }, 3, CategoryId);
            return value;
        }
    }
}
