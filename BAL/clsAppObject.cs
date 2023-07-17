
using BAL.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class clsAppObject 
    {
       public static DAL.Core clsCore = new DAL.Core();
        public static clsUserBMSBAL LoginUser = null;
        public static clsCompanyBAL Company = null;
        public static T Cast<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                propertyInfo.SetValue(x, value, null);
            }
            return (T)x;
        }

        public static ObservableCollection<TargetEntity> CastCollection<T,TargetEntity>(this ObservableCollection<T> collection) 
        {
            ObservableCollection<TargetEntity> CastCollection = new ObservableCollection<TargetEntity>();
            for (int i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                var castItem = Cast<TargetEntity>(item);
                CastCollection.Add(castItem);
            }
            return CastCollection;
        }
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static ObservableCollection<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                ObservableCollection<T> list = new ObservableCollection<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var item in row.Table.Columns)
                    {
                        try
                        {
                            var DataColumn = item as DataColumn;
                            string ColumnName = DataColumn.ColumnName;
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(DataColumn.ColumnName);
                            if (propertyInfo != null)
                            {
                                var value = row[ColumnName];
                                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                propertyInfo.SetValue(obj, safeValue, null);
                            }
                            //if (propertyInfo.PropertyType == typeof(Nullable<DateTime>))
                            //{
                               
                            //    propertyInfo.SetValue(obj, value, null);
                            //}
                            //else
                            //    propertyInfo.SetValue(obj, Convert.ChangeType(row[ColumnName], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    //foreach (var prop in obj.GetType().GetProperties())
                    //{
                    //    try
                    //    {
                    //        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                    //        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                    //    }
                    //    catch
                    //    {
                    //        continue;
                    //    }
                    //}

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static bool CheckUserRole(int RoleId, clsUserBMSBAL LoginUser)
        {
            if (LoginUser.UserTypeId == 1)
                return true;
            DataTable dt = null;
            clsCore.Getdatafromdb(ref dt, "SptUserRole", new string[] { "@TypeId", "@RoleID", "@UserID" }, 4, RoleId, LoginUser.userid);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

    }
}
