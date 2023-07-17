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
    public class clsCustomerTypeBAL : CustomerType
    {
        /// <summary>
        /// get All Active customer Type
        /// </summary>
        /// <returns>Return customerType</returns>
        public static ObservableCollection<clsCustomerTypeBAL> getCoustomerType()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCustomerType", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCustomerTypeBAL>(dt);
        }
    }
}
