using BAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Classes
{
    public class clsTitleBAL
    {
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int DataEntryUserId { get; set; }
        public DateTime DataEntryDate { get; set; }
        public int RecordStatus { get; set; }

        /// <summary>
        /// get All Active Title Type
        /// </summary>
        /// <returns>Return Title</returns>
        public static ObservableCollection<clsTitleBAL> getTitle()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptTitle", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsTitleBAL>(dt);
        }
    }
}
