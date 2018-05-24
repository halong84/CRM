using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities.DV
{
    class Chinhanh
    {
        private string _ma_CN;

        public string ma_CN
        {
            get { return _ma_CN; }
            set { _ma_CN = value; }
        }

        private string _ten_CN;
     
        public string ten_CN
        {
            get { return _ten_CN; }
            set { _ten_CN = value; }
        }

        private string _ten_cn_day_du;

        public string ten_cn_day_du
        {
            get { return _ten_cn_day_du; }
            set { _ten_cn_day_du = value; }
        }

        private string _dia_chi;

        public string dia_chi
        {
            get { return _dia_chi; }
            set { _dia_chi = value; }
        }

        private string _mst;

        public string mst
        {
            get { return _mst; }
            set { _mst = value; }
        }

        private string _dkkd;

        public string dkkd
        {
            get { return _dkkd; }
            set { _dkkd = value; }
        }

        private string _guq;

        public string guq
        {
            get { return _guq; }
            set { _guq = value; }
        }

        private string _ma_khtx;

        public string ma_khtx
        {
            get { return _ma_khtx; }
            set { _ma_khtx = value; }
        }

        private string _sdt;
        public string sdt
        {
            get { return _sdt; }
            set { _sdt = value; }
        }

        public Chinhanh(DataRow row)
        {
            this._ma_CN = row["MACN"].ToString();
            this._ten_CN = row["TENCN"].ToString();
            this._ten_cn_day_du = row["TEN_CN_DAY_DU"].ToString();
            this._dia_chi = row["DIACHI"].ToString();
            this._mst = row["MST"].ToString();
            this._dkkd = row["DKKD"].ToString();
            this._guq = row["GUQ"].ToString();
            this._ma_khtx = row["MA_KHTX"].ToString();
            this._sdt = row["SDT"].ToString();
        }

        public Chinhanh(string ma_cn, string ten_cn, string ten_cn_day_du, string dia_chi, string mst, string dkkd, string guq, string ma_khtx)
        {
            this._ma_CN = ma_cn;
            this._ten_CN = ten_cn;
            this._ten_cn_day_du = ten_cn_day_du;
            this._dia_chi = dia_chi;
            this._mst = mst;
            this._dkkd = dkkd;
            this._guq = guq;
            this._ma_khtx = ma_khtx;
        }
    }
}
