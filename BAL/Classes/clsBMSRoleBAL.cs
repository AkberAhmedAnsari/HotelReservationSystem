using DAL.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Classes
{
    public class clsBMSRoleBAL : AppRole
    {
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        /// <summary> 
        /// get All BMSRole recordStatus 1 
        /// </summary>   
        /// <returns>All BMSRole return</returns>
        public static ObservableCollection<clsBMSRoleBAL> GetBMSRole(int UserID)
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptUserRole", new string[] { "@TypeId", "@UserID" }, 1, UserID);
            return clsAppObject.DataTableToList<clsBMSRoleBAL>(dt);
        }

        /// <summary> 
        /// get All BMSUserRole recordStatus 1 
        /// </summary>   
        /// <returns>All BMSUserRole return</returns>
        public static ObservableCollection<clsBMSRoleBAL> GetBMSUserRole(int UserID)
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptUserRole", new string[] { "@TypeId", "@UserID" }, 2, UserID);
            return clsAppObject.DataTableToList<clsBMSRoleBAL>(dt);
        }

       
    }
}
