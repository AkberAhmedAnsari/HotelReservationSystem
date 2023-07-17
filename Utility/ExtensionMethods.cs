using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public static class ExtensionMethods
    {

        public static object toconvert(this object value,Type datatype)
        {
           object str = null;
            try
            {
              
                if (datatype == typeof(string))
                    str = value.tostring();

                if (datatype == typeof(DateTime))
                    str = value.todatetime();

                if (datatype == typeof(double))
                    str = value.todouble();

                if (datatype == typeof(decimal))
                    str = value.todecimal();

                if (datatype == typeof(int))
                    str = value.toint();

                if (datatype == typeof(bool))
                    str = value.tobool();

            }
            catch (Exception)
            {


            }

            return str;
        }





        public static string tostring(this object value)
        {
            string str = "";
            try
            {
                if (value == null)
                {
                    str= "";
                }
                else
                {
                    str= value.ToString(); ;
                }
            }
            catch (Exception)
            {
                
                
            }

            return str;
        }


        public static DateTime todatetime(this object value)
        {
            DateTime datetime = new DateTime();
            try
            {
                if (value == null)
                {
                    datetime= DateTime.Parse("1999/01/01");
                }
                else
                {
                    datetime= DateTime.Parse(value.ToString()); ;
                }
            }
            catch (Exception )
            {
                
               
            }

            return datetime;
          
        }

        public static double todouble(this object value)
        {
            double v_double = 0.0;
            try
            {
                if (value == null)
                {
                    v_double= 0.0;
                }
                else
                {
                    v_double= double.Parse(value.ToString()); ;
                }
            }
            catch (Exception)
            {
                
                
            }
            return v_double;
        }


        public static decimal todecimal(this object value)
        {
            decimal dec = 0;
            try
            {

                if (value == null)
                {
                    dec = 0;
                }
                else
                {
                    dec =decimal.Parse(value.ToString());
                }
            }
            catch (Exception)
            {
                
                
            }
            return dec;
        }

        public static int toint(this object value)
        {
            int i = 0;
            try
            {
                if (value == null)
                {
                    i= 0;
                }
                else
                {
                    i=int.Parse( value.ToString());
                }
                return i;
            }
            catch (Exception)
            {
                
               
            }
            return i;
          
        }

        public static bool tobool(this object value)
        {
            bool i = false;
            try
            {
                if (value == null)
                {
                    i = false;
                }
                else
                {
                    i = bool.Parse(value.ToString());
                }
                return i;
            }
            catch (Exception)
            {


            }
            return i;

        }

    }
}