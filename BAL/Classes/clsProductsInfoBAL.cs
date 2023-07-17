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
    public class clsProductsInfoBAL : ProductsInfo
    {
        private int _ImageId;
        public int ImageId
        {
            get { return _ImageId; }
            set
            {
                _ImageId = value;
                this.OnPropertyChanged("ImageId");
            }
        }

        private Byte[] _RecordImage;
        public Byte[] RecordImage
        {
            get { return _RecordImage; }
            set
            {
                _RecordImage = value;
                this.OnPropertyChanged("RecordImage");
            }
        }

        private string _ProductTypeName;

        public string ProductTypeName
        {
            get { return _ProductTypeName; }
            set { _ProductTypeName = value; }
        }

        private string _CategoryName;

        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsCustomerBAL"></param>
        /// <returns></returns>
        public static bool SaveLogic(clsProductsInfoBAL _clsCustomerBAL)
        {
            if (Valadation(_clsCustomerBAL) == false)
                return false;
            if (inserRecordintDataTable(_clsCustomerBAL) == false)
                return false;
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        private static bool Valadation(clsProductsInfoBAL _clsProductsInfoBAL)
        {
            if (_clsProductsInfoBAL.CategoryId == 0)
                throw new Exception("Please Select Product Catagre");
            if (_clsProductsInfoBAL.productItemname == "")
                throw new Exception("Please enter Product Name");
            if (_clsProductsInfoBAL.barcode == "")
                throw new Exception("Please enter Barcode");
            if (_clsProductsInfoBAL.ProductTypeId == 0)
                throw new Exception("Please Select Product Type");
            if (_clsProductsInfoBAL.costprice == 0)
                throw new Exception("Please enter Cost Price");
            if (_clsProductsInfoBAL.saleprice == 0)
                throw new Exception("Please enter Sale Price");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clsProductsInfoBAL"></param>
        /// <returns></returns>
        private static bool inserRecordintDataTable(clsProductsInfoBAL _clsProductsInfoBAL)
        {
            bool vbol = false;
            string strcon = "";
            clsAppObject.clsCore.GetConnection(ref strcon);

            _clsProductsInfoBAL.DataEntryUserId = clsAppObject.LoginUser.userid;
            _clsProductsInfoBAL.RecordStatus = 1;

            BMSImage _clsBMSImage = new BMSImage();
            _clsBMSImage.RecordImage = _clsProductsInfoBAL.RecordImage;
            _clsBMSImage.RecordStatus = 1;

            SqlTransaction trans = null;
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                con.Open();
                trans = con.BeginTransaction();
                ProductsInfo _clsProductsInfo = clsAppObject.Cast<ProductsInfo>(_clsProductsInfoBAL);
                if (_clsProductsInfoBAL.productItemId > 0)
                    clsAppObject.clsCore.UpdateRecord<ProductsInfo>(_clsProductsInfo, "productItemId", _clsProductsInfo.productItemId.ToString(), trans, con);
                else
                    _clsProductsInfoBAL.productItemId = clsAppObject.clsCore.InsertRecord<ProductsInfo>(_clsProductsInfo, trans, con);

                _clsBMSImage.ProductItemId = _clsProductsInfoBAL.productItemId;

                bool imgbool = false;
                if (_clsProductsInfoBAL.ImageId > 0 && _clsBMSImage.RecordImage != null)
                    clsAppObject.clsCore.ExecuteScaler(ref imgbool, trans, con, "SptBMSImage", new string[] { "@TypeId", "@RecordImage", "@ImageId" }, 1, _clsBMSImage.RecordImage, _clsProductsInfoBAL.ImageId);
                else
                    clsAppObject.clsCore.ExecuteScaler(ref imgbool, trans, con, "SptBMSImage", new string[] { "@TypeId", "@RecordImage", "@ProductItemId" }, 1, _clsBMSImage.RecordImage, _clsBMSImage.ProductItemId);

                trans.Commit();
                con.Close();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<clsProductsInfoBAL> getRecords()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptProductInfo", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsProductsInfoBAL>(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productItemId"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productItemId)
        {
            bool value = false;
            clsAppObject.clsCore.ExecuteScaler(ref value, "SptProductInfo", new string[] { "@TypeId", "@productItemId" }, 2, productItemId);
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public static ObservableCollection<clsProductsInfoBAL> GetProductItemByCategoryId(int CategoryId)
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptProductInfo", new string[] { "@TypeId", "@CategoryId" }, 3, CategoryId);
            return clsAppObject.DataTableToList<clsProductsInfoBAL>(dt);
        }
    }
}
