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
    public class clsProductTypeBAL : ProductType
    {
        /// <summary>
        /// get All Active Product Type
        /// </summary>
        /// <returns>Return ProductType</returns>
        public static ObservableCollection<clsProductTypeBAL> getProductType()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptProductType", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsProductTypeBAL>(dt);
        }
    }
}
