using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DAL.Classes
{
    public class BaseClass : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    //public static class clsTabe
    //{
    //    public static string CurTCode;
    //    public static Form CurForm;
    //    public static string CurDocNo;
    //    public static Control  BeforActiveControl;
    //    public static Control AfterActiveControl;
    //}



    public class clsTabe
   {
       public  string CurTCode;
       public  Form CurForm;
       public  string CurDocNo;
       public  DataSet vgConfi;
       public DataSet save_dataset;
       public Control BeforActiveControl;
       public  Control AfterActiveControl;
       public  string primarykey;
       public  string primarykeyvalue;
       public string page;
       public string subpage;
   }

   //public class clsForm
   //{
   //    public int Print;
   //    public int Email;
   //    public string GLCnfg;
   //    public string GLTran;
   //}
}
