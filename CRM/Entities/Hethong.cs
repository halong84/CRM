using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities
{
    class Hethong
    {
        #region Instance Properties

        private string _ma_hoi_so;
        public string ma_hoi_so
        {
            get { return _ma_hoi_so; }
            set { _ma_hoi_so = value; }
        }

        private string _ddimport;
        public string ddimport
        {
            get { return _ddimport; }
            set { _ddimport = value; }
        }

        private string _ma_tinh_hien_tai;
        public string ma_tinh_hien_tai
        {
            get { return _ma_tinh_hien_tai; }
            set { _ma_tinh_hien_tai = value; }
        }

        #endregion Instance Properties

        public Hethong(DataRow row)
        {
            this._ma_hoi_so = row["MA_HOI_SO"].ToString();
            this._ddimport = row["DDIMPORT"].ToString();
            this._ma_tinh_hien_tai = row["MA_TINH_HIEN_TAI"].ToString();
        }

        public Hethong(string[] hethong)
        {
            this._ma_hoi_so = hethong[0];
            this._ddimport = hethong[1];
            this._ma_tinh_hien_tai = hethong[2];
        }
    }
}
