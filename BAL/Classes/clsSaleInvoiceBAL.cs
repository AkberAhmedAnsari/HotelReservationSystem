using DAL.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Classes
{
    public class clsSaleInvoiceBAL : SaleInvoice
    {
        private bool _IsOrder;

        private int _ItemCount;
        private decimal _AdvanceAmount;
        private decimal _CurrentAmount;
        private decimal _PaidAmount;
        private bool _IsSale;
        private bool _isRefOrderInvoice;
        private string _Address;
        private string _ClientName;
        private string _InvoiceStatus;
        private string _UserName;

        public bool IsOrder
        {
            get
            {
                return _IsOrder;
            }
            set
            {
                _IsOrder = value;
            }
        }

        public int ItemCount
        {
            get { return _ItemCount; }
            set
            {
                _ItemCount = value;
                OnPropertyChanged("ItemCount");
            }
        }

        public decimal AdvanceAmount
        {
            get
            {
                return _AdvanceAmount;
            }
            set
            {
                _AdvanceAmount = value;
                OnPropertyChanged("AdvanceAmount");
            }
        }

        public decimal CurrentAmount
        {
            get
            {
                return _CurrentAmount;
            }

            set
            {
                _CurrentAmount = value;
                OnPropertyChanged("CurrentAmount");
            }
        }

        public bool IsSale
        {
            get
            {
                return _IsSale;
            }

            set
            {
                _IsSale = value;
                OnPropertyChanged("IsSale");
            }
        }

        public bool IsRefOrderInvoice
        {
            get
            {
                return _isRefOrderInvoice;
            }

            set
            {
                _isRefOrderInvoice = value;
            }
        }

        public decimal PaidAmount
        {
            get
            {
                return _PaidAmount;
            }

            set
            {
                _PaidAmount = value;
                OnPropertyChanged("PaidAmount");
            }
        }

        public string Address
        {
            get
            {
                return _Address;
            }

            set
            {
                _Address = value;
            }
        }

        public string ClientName
        {
            get
            {
                return _ClientName;
            }

            set
            {
                _ClientName = value;
            }
        }

        public string InvoiceStatus
        {
            get
            {
                return _InvoiceStatus;
            }

            set
            {
                _InvoiceStatus = value;
            }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
            }
        }

        public static bool SaveLogic(clsSaleInvoiceBAL _clsSaleInvoiceBAL, 
            ObservableCollection<clsSaleInvoiceItemBAL> collection,
            clsSaleInvoicePaymentModeBAL _clsSaleInvoicePaymentModeBAL,
            ObservableCollection<clsSaleInvoiceItemBAL> Deletecollection,
            bool Isedit,
            int itemStatus)
        {


            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);
            _clsSaleInvoiceBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsSaleInvoiceBAL.RecordStatus = itemStatus;
            SaleInvoice _SaleInvoice = clsAppObject.Cast<SaleInvoice>(_clsSaleInvoiceBAL);
            SaleInvoicePaymentMode _SaleInvoicePaymentMode = clsAppObject.Cast<SaleInvoicePaymentMode>(_clsSaleInvoicePaymentModeBAL);
            ObservableCollection<SaleInvoiceItem> _SaleInvoiceItemCollection = clsAppObject.CastCollection<clsSaleInvoiceItemBAL, SaleInvoiceItem>(collection);

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                if (_clsSaleInvoiceBAL.IsRefOrderInvoice)
                {
                    _SaleInvoice.SaleInvoiceParentId = _clsSaleInvoiceBAL.SaleInvoiceId;
                    _clsSaleInvoiceBAL.SaleInvoiceId = clsAppObject.clsCore.InsertRecord<SaleInvoice>(_SaleInvoice, trans, con);
                    _SaleInvoicePaymentMode.SaleInvoiceId = _clsSaleInvoiceBAL.SaleInvoiceId;
                    _SaleInvoicePaymentMode.DataEntryUserId = _clsSaleInvoiceBAL.DataEntryUserId;
                    _SaleInvoicePaymentMode.RecordStatus = _clsSaleInvoiceBAL.RecordStatus;
                    _clsSaleInvoicePaymentModeBAL.SaleInvoicePaymentModeId = clsAppObject.clsCore.InsertRecord<SaleInvoicePaymentMode>(_SaleInvoicePaymentMode, trans, con);
                }
                else
                {
                    if (_clsSaleInvoiceBAL.SaleInvoiceId > 0)
                        clsAppObject.clsCore.UpdateRecord<SaleInvoice>(_SaleInvoice, "SaleInvoiceId", _SaleInvoice.SaleInvoiceId.ToString(), trans, con);
                    else
                        _clsSaleInvoiceBAL.SaleInvoiceId = clsAppObject.clsCore.InsertRecord<SaleInvoice>(_SaleInvoice, trans, con);

                    foreach (var item in _SaleInvoiceItemCollection)
                    {
                        item.SaleInvoiceId = _clsSaleInvoiceBAL.SaleInvoiceId;
                        item.DataEntryUserId = _clsSaleInvoiceBAL.DataEntryUserId;
                        item.RecordStatus = _clsSaleInvoiceBAL.RecordStatus;
                        if (item.SaleInvoiceItemId > 0)
                            item.SaleInvoiceItemId = clsAppObject.clsCore.UpdateRecord<SaleInvoiceItem>(item, "SaleInvoiceItemId", item.SaleInvoiceItemId.ToString(), trans, con);
                        else
                            item.SaleInvoiceItemId = clsAppObject.clsCore.InsertRecord<SaleInvoiceItem>(item, trans, con);
                    }

                    if (Isedit)
                        DeleteSalesInvoicePayment(_clsSaleInvoiceBAL.SaleInvoiceId, con, trans);

                    foreach (var item in Deletecollection)
                    {
                        DeleteSalesInvoiceItem(item.SaleInvoiceItemId, con, trans);
                    }
                    _SaleInvoicePaymentMode.SaleInvoiceId = _clsSaleInvoiceBAL.SaleInvoiceId;
                    _SaleInvoicePaymentMode.DataEntryUserId = _clsSaleInvoiceBAL.DataEntryUserId;
                    _SaleInvoicePaymentMode.RecordStatus = _clsSaleInvoiceBAL.RecordStatus;
                    _clsSaleInvoicePaymentModeBAL.SaleInvoicePaymentModeId = clsAppObject.clsCore.InsertRecord<SaleInvoicePaymentMode>(_SaleInvoicePaymentMode, trans, con);
                }

                trans.Commit();
                con.Close();
                if (!Isedit)
                    UpdateInvoiceNumber(_clsSaleInvoiceBAL.SaleInvoiceId, _clsSaleInvoiceBAL.RecordStatus);
                vbol = true;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    trans.Rollback();
                    con.Close();
                }
                throw new Exception(ex.Message);
            }

            return vbol;

        }

        public static bool UpdateInvoiceNumber(int SaleInvoiceId, int RecordStatus)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptSaleinvoice", new string[] { "@TypeId", "@SaleInvoiceId", "@RecordStatus" }, 1, SaleInvoiceId, RecordStatus);
            return vbol;
        }

        public static void getOrder(int InvoiceNumber, ref clsSaleInvoiceBAL _clsSaleInvoiceBAL, ref ObservableCollection<clsSaleInvoiceItemBAL> Collection)
        {
            DataSet ds = null;
            clsAppObject.clsCore.Getdatafromdb(ref ds, "SptSaleinvoice", new string[] { "@TypeId", "@InvoiceNumber" }, 2, InvoiceNumber);
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    _clsSaleInvoiceBAL = clsAppObject.DataTableToList<clsSaleInvoiceBAL>(ds.Tables[0])[0];
                else
                    _clsSaleInvoiceBAL = null;

                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    Collection = clsAppObject.DataTableToList<clsSaleInvoiceItemBAL>(ds.Tables[1]);
                else
                    Collection = null;
            }
        }

        public static clsSaleInvoicePaymentModeBAL getPaymentOrder(int SaleInvoiceId)
        {
            DataTable dt = null;
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptSaleinvoice", new string[] { "@TypeId", "@SaleInvoiceId" }, 3, SaleInvoiceId);
            if (dt != null && dt.Rows.Count > 0)
            {
                return clsAppObject.DataTableToList<clsSaleInvoicePaymentModeBAL>(dt)[0];
            }
            else
                return null;
        }

        public static bool DeleteSalesInvoicePayment(int SaleInvoiceId, SqlConnection con, SqlTransaction trans)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, trans, con, "SptSaleinvoice", new string[] { "@TypeId", "@SaleInvoiceId" }, 7, SaleInvoiceId);
            return vbol;
        }

        public static bool DeleteSalesInvoiceItem(int SaleInvoiceItemId, SqlConnection con, SqlTransaction trans)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, trans, con, "SptSaleinvoice", new string[] { "@TypeId", "@SaleInvoiceItemId" }, 8, SaleInvoiceItemId);
            return vbol;
        }

        public static ObservableCollection<clsSaleInvoiceBAL> getRecords()
        {
            DataTable dt = null;
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptSaleinvoice", new string[] { "@TypeId",  }, 4);
            if (dt != null && dt.Rows.Count > 0)
            {
                return clsAppObject.DataTableToList<clsSaleInvoiceBAL>(dt);
            }
            else
                return null;
        }

        public static clsProductsInfoBAL getProductItem(string barcode)
        {
            DataTable dt = null;
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptSaleinvoice", new string[] { "@TypeId", "@barcode" }, 9, barcode);
            if (dt != null && dt.Rows.Count > 0)
            {
                return clsAppObject.DataTableToList<clsProductsInfoBAL>(dt)[0];
            }
            else
                return null;
        }


        public static ObservableCollection<clsSaleInvoiceItemBAL> getSalesInvoiceItemforEdit(int SaleInvoiceId)
        {
            DataTable dt = null;
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptSaleinvoice", new string[] { "@TypeId", "@SaleInvoiceId" }, 6, SaleInvoiceId);
            if (dt != null && dt.Rows.Count > 0)
            {
                return clsAppObject.DataTableToList<clsSaleInvoiceItemBAL>(dt);
            }
            else
                return null;
        }

        public static bool DeleteInvoice(int SaleInvoiceId)
        {
            bool vbol = false;
            clsAppObject.clsCore.ExecuteScaler(ref vbol, "SptSaleinvoice", new string[] { "@TypeId", "@SaleInvoiceId" }, 10, SaleInvoiceId);
            return vbol;
        }
    }
}
