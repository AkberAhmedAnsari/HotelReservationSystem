using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.ClassLiabrary;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace DAL
{
    public class Core
    {
        SqlConnection SCon = new SqlConnection();
        SqlCommand SCom = new SqlCommand();
        //public CoreClasses.AppObject.GetErrorMethod ErrorMethod;
        public string DataBase_name = "BMS";


        public errObjects GetConnection(ref string con)
        {
            errObjects err = new errObjects();

            try
            {
                con = ConfigurationManager.ConnectionStrings[DataBase_name].ToString();
                err.ErrNumber = "0";
            }

            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = "Connection String not found...";
                err.ErrSource = "core.getconnectionstring";
            }

            return err;
        }


        public errObjects OpenConnection()
        {
            errObjects err = new errObjects();
            try
            {
                if (SCon.State == System.Data.ConnectionState.Closed)
                {
                    string strcon = "";

                    err = GetConnection(ref strcon);

                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }

                    SCon.ConnectionString = strcon;
                    SCon.Open();
                }

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = "core.openConnection";

            }

            return err;
        }


        public errObjects CloesConnection()
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == System.Data.ConnectionState.Open)
                {
                    SCon.Close();
                }

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = "Connection not successfully Closed...";
                err.ErrSource = "core.closeConnection";
            }

            return err;
        }


        public errObjects BeginTransaction()
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == System.Data.ConnectionState.Open)
                {
                    SCom.Connection = SCon;
                    SCom.Transaction = SCon.BeginTransaction();
                }
                else
                {
                    err = OpenConnection();

                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }

                    SCom.Connection = SCon;
                    SCom.Transaction = SCon.BeginTransaction();

                }
                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = "Begin Transaction failed...";
                err.ErrSource = "Core.BeginTransaction";
            }

            return err;

        }


        public errObjects CommitTransaction()
        {
            errObjects err = new errObjects();

            try
            {

                if (SCom.Transaction != null)
                    SCom.Transaction.Commit();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = "Cammit Transaction Failed....";
                err.ErrSource = "Core.Commit Transaction";
            }


            return err;
        }


        public errObjects RollbackTransaction()
        {
            errObjects err = new errObjects();
            try
            {

                if (SCom.Transaction != null)
                    SCom.Transaction.Rollback();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = "core.Rollback Transaction";
            }

            return err;
        }


        public errObjects Getdatafromdb(string query, ref DataTable dtable)
        {
            errObjects err = new errObjects();
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter sda = null;

                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                sda = new SqlDataAdapter(query, SCon);
                sda.Fill(ds);
                dtable = ds.Tables[0];

                CloesConnection();
                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;
        }

        public errObjects Getdatafromdb(ref DataTable dtable, String SPName, string[] Parameters, params object[] Value)
        {
            errObjects err = new errObjects();
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter sda = null;
                SCom.Parameters.Clear();
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                SCom.Connection = SCon;
                SCom.CommandType = CommandType.StoredProcedure;
                SCom.CommandTimeout = 0;
                SCom.CommandText = SPName;

                for (int i = 0; i < Parameters.Count(); i++)
                {
                    string pr = Parameters[i];
                    SCom.Parameters.Add(new SqlParameter(pr, Value[i]));
                }

                sda = new SqlDataAdapter(SCom);
                sda.Fill(ds);
                dtable = ds.Tables[0];

                //CloesConnection();
                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }
            finally
            {
                CloesConnection();
            }

            return err;
        }

        public errObjects Getdatafromdb(ref DataSet dset, String SPName, string[] Parameters, params object[] Value)
        {
            errObjects err = new errObjects();
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter sda = null;
                SCom.Parameters.Clear();
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                SCom.Connection = SCon;
                SCom.CommandType = CommandType.StoredProcedure;
                SCom.CommandTimeout = 0;
                SCom.CommandText = SPName;

                for (int i = 0; i < Parameters.Count(); i++)
                {
                    string pr = Parameters[i];
                    SCom.Parameters.Add(new SqlParameter(pr, Value[i]));
                }

                sda = new SqlDataAdapter(SCom);
                sda.Fill(ds);
                dset = ds;

                //CloesConnection();
                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }
            finally
            {
                CloesConnection();
            }

            return err;
        }
        public errObjects ExecuteCommand(string commtext)
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                err = BeginTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                SCom.CommandType = CommandType.StoredProcedure;
                SCom.CommandText = commtext;
                SCom.ExecuteNonQuery();

                err = CommitTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;
        }


        public errObjects ExecuteScaler(string quary, ref int value)
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                err = BeginTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                SCom.CommandType = CommandType.Text;
                SCom.CommandText = quary;
                value = int.Parse(SCom.ExecuteScalar().ToString());

                err = CommitTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;

        }


        public errObjects ExecuteScaler(string quary, ref DateTime value)
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                err = BeginTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                SCom.CommandType = CommandType.Text;
                SCom.CommandText = quary;
                value = DateTime.Parse(SCom.ExecuteScalar().ToString());

                err = CommitTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;

        }


        public errObjects ExecuteScaler(string quary, ref object value)
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                err = BeginTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                SCom.CommandType = CommandType.Text;
                SCom.CommandText = quary;
                value = SCom.ExecuteScalar();

                err = CommitTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;

        }

        public errObjects ExecuteScaler(string quary, ref string value)
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                err = BeginTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                SCom.CommandType = CommandType.Text;
                SCom.CommandText = quary;
                value = (string)SCom.ExecuteScalar();

                err = CommitTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;

        }


        public errObjects ExecuteScaler(ref bool value, String SPName, string[] Parameters, params object[] Value)
        {
            errObjects err = new errObjects();

            try
            {
                SCom.Parameters.Clear();
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                err = BeginTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                //SCom.CommandType = CommandType.StoredProcedure;
                //SCom.CommandText = quary

                SCom.Connection = SCon;
                SCom.CommandType = CommandType.StoredProcedure;
                SCom.CommandTimeout = 0;
                SCom.CommandText = SPName;

                for (int i = 0; i < Parameters.Count(); i++)
                {
                    string pr = Parameters[i];
                    SCom.Parameters.Add(new SqlParameter(pr, Value[i]));
                }
                SCom.ExecuteNonQuery();

                err = CommitTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

                err.ErrNumber = "0";
                value = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;

        }


        public errObjects ExecuteScaler(ref bool value, SqlTransaction trans, SqlConnection connection, String SPName, string[] Parameters, params object[] Value)
        {
            errObjects err = new errObjects();

            try
            {
                //SCom.Parameters.Clear();
                //if (SCon.State == ConnectionState.Closed)
                //{
                //    err = OpenConnection();
                //    if (err.ErrNumber != "0")
                //    {
                //        throw new Exception(err.ErrMessage);
                //    }
                //}

                //err = BeginTransaction();
                //if (err.ErrNumber != "0")
                //{
                //    throw new Exception(err.ErrMessage);
                //}

                //SCom.CommandType = CommandType.StoredProcedure;
                //SCom.CommandText = quary

                SqlCommand objSqlCommand = new SqlCommand();
                objSqlCommand.Parameters.Clear();
                objSqlCommand.Transaction = trans;
                objSqlCommand.Connection = connection;
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.CommandTimeout = 0;
                objSqlCommand.CommandText = SPName;

                for (int i = 0; i < Parameters.Count(); i++)
                {
                    string pr = Parameters[i];
                    objSqlCommand.Parameters.Add(new SqlParameter(pr, Value[i]));
                }
                objSqlCommand.ExecuteNonQuery();

                //err = CommitTransaction();
                //if (err.ErrNumber != "0")
                //{
                //    throw new Exception(err.ErrMessage);
                //}

                err.ErrNumber = "0";
                value = true;
            }
            catch (Exception ex)
            {
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
                throw new Exception(ex.Message);

            }

            return err;

        }
        //public  errObjects ExecuteScaler(string quary, ref DateTime value)
        //{
        //    errObjects err = new errObjects();

        //    try
        //    {
        //        if (SCon.State == ConnectionState.Closed)
        //        {
        //            err = OpenConnection();
        //            if (err.ErrNumber != "0")
        //            {
        //                throw new Exception(err.ErrMessage);
        //            }
        //        }

        //        err = BeginTransaction();
        //        if (err.ErrNumber != "0")
        //        {
        //            throw new Exception(err.ErrMessage);
        //        }

        //        SCom.CommandType = CommandType.Text;
        //        SCom.CommandText = quary;
        //        value = DateTime.Parse(SCom.ExecuteScalar().ToString());

        //        err = CommitTransaction();
        //        if (err.ErrNumber != "0")
        //        {
        //            throw new Exception(err.ErrMessage);
        //        }

        //        CloesConnection();

        //        err.ErrNumber = "0";
        //    }
        //    catch (Exception ex)
        //    {
        //        RollbackTransaction();
        //        err.ErrNumber = "1";
        //        err.ErrMessage = ex.Message;
        //        err.ErrSource = ex.Source;
        //    }

        //    return err;

        //}


        //public  errObjects ExecuteScaler(string quary, ref object value)
        //{
        //    errObjects err = new errObjects();

        //    try
        //    {
        //        if (SCon.State == ConnectionState.Closed)
        //        {
        //            err = OpenConnection();
        //            if (err.ErrNumber != "0")
        //            {
        //                throw new Exception(err.ErrMessage);
        //            }
        //        }

        //        err = BeginTransaction();
        //        if (err.ErrNumber != "0")
        //        {
        //            throw new Exception(err.ErrMessage);
        //        }

        //        SCom.CommandType = CommandType.Text;
        //        SCom.CommandText = quary;
        //        value = SCom.ExecuteScalar();

        //        err = CommitTransaction();
        //        if (err.ErrNumber != "0")
        //        {
        //            throw new Exception(err.ErrMessage);
        //        }

        //        CloesConnection();

        //        err.ErrNumber = "0";
        //    }
        //    catch (Exception ex)
        //    {
        //        RollbackTransaction();
        //        err.ErrNumber = "1";
        //        err.ErrMessage = ex.Message;
        //        err.ErrSource = ex.Source;
        //    }

        //    return err;

        //}

        public int InsertRecord<T>(T t, SqlTransaction trans, SqlConnection connection)
        {

            SqlCommand objSqlCommand = new SqlCommand();
            objSqlCommand.Parameters.Clear();
            objSqlCommand.Transaction = trans;
            objSqlCommand.Connection = connection;
            objSqlCommand.CommandType = CommandType.Text;
            objSqlCommand.Parameters.Clear();
            string insertQuery = CreateInsertQuery<T>(t);
            //Debug.WriteLine(insertQuery);
            objSqlCommand.CommandText = insertQuery;
            objSqlCommand.CommandTimeout = 0;
            var ReturnVal = objSqlCommand.ExecuteScalar();
            Type TT = ReturnVal.GetType();

            if (TT.FullName == "System.DBNull")
                return 0;
            else
                return Convert.ToInt32(ReturnVal);


        }


        public int UpdateRecord<T>(T t, string conditionParameteName, string conditionParametevalue, SqlTransaction trans, SqlConnection connection)
        {

            SqlCommand objSqlCommand = new SqlCommand();
            objSqlCommand.Parameters.Clear();
            objSqlCommand.Transaction = trans;
            objSqlCommand.Connection = connection;
            objSqlCommand.CommandType = CommandType.Text;
            objSqlCommand.Parameters.Clear();
            string insertQuery = CreateUpdateQuery<T>(t, conditionParameteName, conditionParametevalue);
            //Debug.WriteLine(insertQuery);
            objSqlCommand.CommandText = insertQuery;
            objSqlCommand.CommandTimeout = 0;
            var ReturnVal = objSqlCommand.ExecuteScalar();
            Type TT = ReturnVal.GetType();

            if (TT.FullName == "System.DBNull")
                return 0;
            else
                return Convert.ToInt32(ReturnVal);
        }
        private  string CreateInsertQuery<T>(T t)
        {
            Type type = t.GetType();
            PropertyInfo[] pinfo = type.GetProperties();

            string PrimaryKeyFieldName;
            string ParametersList = null;
            string Values = null;



            foreach (PropertyInfo property in pinfo)
            {
                object[] objarray = property.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
                if (objarray.Count() == 1)
                {
                    PrimaryKeyFieldName = property.Name;

                }
                else
                {
                    ParametersList = ParametersList + property.Name + ",";

                    object value = t.GetType().GetProperty(property.Name).GetValue(t, null);

                    if (value != null)
                    {
                        string ss = value.GetType().Name;
                        switch (ss)
                        {
                            case "DateTime":
                                if (property.Name != "DataEntryDate")
                                    Values = Values + "'" + String.Format("{0:M/d/yyyy HH:mm:ss}", value) + "'" + ",";
                                else
                                {

                                    Values = Values + "GETDATE()" + ",";
                                }
                                break;
                            case "String":
                                {
                                    //string sss = value.ToString().Trim().Length == 0 ? "', '" : value.ToString();
                                    //Values = Values + sss + ",";
                                    string sss;
                                    int length = value.ToString().Trim().Length;

                                    string valueCopy = value.ToString().Replace("'", "''");
                                    if (length == 0)
                                    {
                                        sss = "' ',";
                                    }
                                    else
                                    {
                                        sss = "'" + valueCopy.ToString() + "',";
                                    }
                                    Values = Values + sss;
                                }
                                break;
                            case "Boolean":
                                {
                                    if (property.GetValue(t, null) != null)
                                    {
                                        string s = property.GetValue(t, null).GetType().Name;
                                        if (s == "Boolean")
                                        {
                                            string sss = (bool)property.GetValue(t, null) == false ? "0," : "1,";
                                            Values = Values + sss;
                                        }
                                    }
                                    else
                                    {
                                        Values = Values + "null,";
                                    }
                                }
                                break;
                            default:
                                Values = Values + value.ToString() + ",";
                                break;
                        }
                    }
                    else
                    {
                        Values = Values + "null,";
                    }
                }
            }
            int Paramlength = ParametersList.Length;

            int valuesLength = Values.Length;
            var Paramlist = ParametersList.Remove(Paramlength - 1);
            var ValuesList = Values.Remove(valuesLength - 1);


            string query = "Insert into " + t.GetType().Name + "(" + Paramlist + ") VALUES(" + ValuesList + ")" + " ; DECLARE @ID INT; SET @ID=SCOPE_IDENTITY();SELECT  @ID";
            return query;
        }

        private string CreateUpdateQuery<T>(T t, string conditionParameteName, string conditionParametevalue)
        {
            Type type = t.GetType();
            PropertyInfo[] pinfo = type.GetProperties();

            string PrimaryKeyFieldName;
            string ParametersList = null;
            string Values = null;



            foreach (PropertyInfo property in pinfo)
            {
                object[] objarray = property.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
                if (objarray.Count() == 1)
                {
                    PrimaryKeyFieldName = property.Name;

                }
                else
                {
                    ParametersList = ParametersList + property.Name + ",";

                    object value = t.GetType().GetProperty(property.Name).GetValue(t, null);

                    if (value != null)
                    {
                        string ss = value.GetType().Name;
                        switch (ss)
                        {
                            case "DateTime":
                                if (property.Name != "DataEntryDate")
                                    Values = Values + property.Name + " = " + "'" + String.Format("{0:M/d/yyyy HH:mm:ss}", value) + "'" + ",";
                                else
                                {

                                    Values = Values + property.Name + " = " + "GETDATE()" + ",";
                                }
                                break;
                            case "String":
                                {
                                    //string sss = value.ToString().Trim().Length == 0 ? "', '" : value.ToString();
                                    //Values = Values + sss + ",";
                                    string sss;
                                    int length = value.ToString().Trim().Length;

                                    string valueCopy = value.ToString().Replace("'", "''");
                                    if (length == 0)
                                    {
                                        sss = "' ',";
                                    }
                                    else
                                    {
                                        sss = "'" + valueCopy.ToString() + "',";
                                    }
                                    Values = Values + property.Name + " = " + sss;
                                }
                                break;
                            case "Boolean":
                                {
                                    if (property.GetValue(t, null) != null)
                                    {
                                        string s = property.GetValue(t, null).GetType().Name;
                                        if (s == "Boolean")
                                        {
                                            string sss = (bool)property.GetValue(t, null) == false ? "0," : "1,";
                                            Values = Values + property.Name + " = " + sss;
                                        }
                                    }
                                    else
                                    {
                                        Values = Values + property.Name + " = null,";
                                    }
                                }
                                break;
                            default:
                                Values = Values + property.Name + " = " + value.ToString() + ",";
                                break;
                        }
                    }
                    else
                    {
                        //Values = Values + "null,";
                    }
                }
            }
            int Paramlength = ParametersList.Length;

            int valuesLength = Values.Length;
            var Paramlist = ParametersList.Remove(Paramlength - 1);
            var ValuesList = Values.Remove(valuesLength - 1);


            string query = "Update " + t.GetType().Name + " set " + ValuesList + " where "+ conditionParameteName + "=" + conditionParametevalue +"; Select " + conditionParametevalue;
            return query;
        }


        public errObjects ExecuteRedar(string quary, ref SqlDataReader datareder)
        {
            errObjects err = new errObjects();

            try
            {
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                err = BeginTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                SCom.CommandType = CommandType.Text;
                SCom.CommandText = quary;
                datareder = SCom.ExecuteReader();

                err = CommitTransaction();
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;


        }


        public errObjects GetDataSetFromSP(string quary, ref DataSet dtset)
        {
            errObjects err = new errObjects();
            DataSet dset = new DataSet();
            try
            {
                if (SCon.State == ConnectionState.Closed)
                {
                    err = OpenConnection();
                    if (err.ErrNumber != "0")
                    {
                        throw new Exception(err.ErrMessage);
                    }
                }

                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                SqlDataAdapter da = new SqlDataAdapter(quary, SCon);
                da.Fill(dset);
                dtset = dset;
                if (err.ErrNumber != "0")
                {
                    throw new Exception(err.ErrMessage);
                }

                CloesConnection();

                err.ErrNumber = "0";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                err.ErrNumber = "1";
                err.ErrMessage = ex.Message;
                err.ErrSource = ex.Source;
            }

            return err;

        }

        //public errObjects Save(DataSet dataset,string database_name)
        //{
        //    errObjects err = new errObjects();
        //    try
        //    {
        //        DataBase_name = database_name;
        //        if (SCon.State == ConnectionState.Closed)
        //        {
        //            err = OpenConnection();
        //            if (err.ErrNumber != "0")
        //                throw new Exception(err.ErrMessage);


        //        }

        //        err = BeginTransaction();
        //        if (err.ErrNumber != "0")
        //            throw new Exception(err.ErrMessage);

        //        Utility.Utitlity_Method.ClearNullValues(dataset); 

        //        using (SqlBulkCopy sqlb = new SqlBulkCopy(SCon,SqlBulkCopyOptions.Default,SCom.Transaction))
        //           {
        //               foreach (DataTable datatable in dataset.Tables)
        //               {

        //                   sqlb.DestinationTableName = "dbo."+datatable.TableName;
        //                   sqlb.NotifyAfter = 1000;
        //                   sqlb.WriteToServer(datatable);
        //               }
        //           }


        //          err= CommitTransaction();
        //          if (err.ErrNumber != "0")
        //              throw new Exception(err.ErrMessage);

        //          err = CloesConnection();
        //          if (err.ErrNumber != "0")
        //              throw new Exception(err.ErrMessage);


        //        err.ErrNumber = "0";
        //    }
        //    catch (Exception ex)
        //    {
        //        RollbackTransaction();
        //        err.ErrNumber = "1";
        //        err.ErrMessage = ex.Message;
        //        err.ErrSource = ex.Source;

        //    }

        //    return err;
        //}
    }
    [AttributeUsage(AttributeTargets.Class |
     AttributeTargets.Constructor |
     AttributeTargets.Field |
     AttributeTargets.Method |
     AttributeTargets.Property,
     AllowMultiple = true)]
    public class PrimaryKeyAttribute : System.Attribute
    {
        // attribute constructor for 
        // positional parameters
        public PrimaryKeyAttribute(string comment)
        {
            this.comment = comment;
        }



        // property for named parameter
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }


        // private member data 

        private string comment;

    }


}
