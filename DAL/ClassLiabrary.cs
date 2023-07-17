using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.ClassLiabrary
{


    public static class AppObject
    {
        public delegate void GetErrorMethod(string errorno, string errorsource, string errorstart, string errorend);

        //public static void ShowErrorForm(errObjects err, string appname,string tcode,string formlabel,string userid)
        //{
        //    frmerrorMessBox f = new frmerrorMessBox();
        //    f.AppName = appname;
        //    f.ErrorObjects = err;
        //    f.ShowDialog();
        //}
    }
   public class errObjects
   {
       string vno;
       string vmesg;
       string vsource;


       public string ErrNumber
       {
           get { return vno; }
           set { vno = value; }
       }


       public string ErrMessage
       {
           get { return vmesg; }
           set { vmesg = value; }
       }

       public string ErrSource
       {
           get { return vsource; }
           set { vsource = value; }
       }

    }
}
