using DAL.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Classes
{
    public class clsCompanyBAL : Company
    {
        public static clsCompanyBAL GetCompany()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCompany", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCompanyBAL>(dt)[0];
        }
    }
}
