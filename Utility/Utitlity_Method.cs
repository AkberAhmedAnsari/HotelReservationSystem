using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Utility
{
   public static class Utitlity_Method
    {

       public static void ClearNullValues(DataSet dataset)
       {
           try
           {
               foreach (DataTable  datatable in dataset.Tables  )
                   foreach (DataRow  datarow in datatable.Rows )
                       foreach (DataColumn col in datatable.Columns)
                           if (datarow[col.ColumnName] == null || datarow[col.ColumnName] == "" || datarow[col.ColumnName].ToString() == "")
                           {
                               if (col.DataType == typeof(string))
                                  // if (datarow[col.ColumnName].ToString() == "")
                                       datarow[col.ColumnName] = "";
                                   else
                                   {
                                       if (col.DataType == typeof(int) || col.DataType == typeof(decimal))
                                         datarow[col.ColumnName] = 0;
                                       else
                                           datarow[col.ColumnName] =DateTime.Parse( "1999/01/01");
                                   }
                           }
                           
               
           }
           catch (Exception ex)
           { 
               throw ex;
           }
       }


       public static void ClearNullValues(DataTable  datatable)
       {
           try
           {
                   foreach (DataRow datarow in datatable.Rows)
                       foreach (DataColumn col in datatable.Columns)
                           if (datarow[col.ColumnName] == null)
                           {
                               if (col.DataType == typeof(string))
                                   datarow[col.ColumnName] = "";
                               if (col.DataType == typeof(int) || col.DataType == typeof(decimal))
                                   datarow[col.ColumnName] = 0;
                               else
                                   datarow[col.ColumnName] = "";
                           }

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
