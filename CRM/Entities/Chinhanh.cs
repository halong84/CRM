using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities
{
    class Chinhanh
    {
        #region Instance Properties

        private string _macn;
        public string macn
        {
            get { return _macn; }
            set { _macn = value; }
        }

        private string _tencn;
        public string tencn
        {
            get { return _tencn; }
            set { _tencn = value; }
        }

        private string _sdt;
        public string sdt
        {
            get { return _sdt; }
            set { _sdt = value; }
        }

        private string _diachi;
        public string diachi
        {
            get { return _diachi; }
            set { _diachi = value; }
        }

        private string _ten_cn_day_du;
        public string ten_cn_day_du
        {
            get { return _ten_cn_day_du; }
            set { _ten_cn_day_du = value; }
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

        private bool _kich_hoat;

        public bool kich_hoat
        {
            get { return _kich_hoat; }
            set { _kich_hoat = value; }
        }

        #endregion Instance Properties

        public Chinhanh(DataRow row)
        {
            this._macn = row["MaCN"].ToString();
            this._tencn = row["TenCN"].ToString();
            this._sdt = row["SDT"].ToString();
            this._diachi = row["DiaChi"].ToString();
            this._ten_cn_day_du = row["TEN_CN_DAY_DU"].ToString();
            this._mst = row["MST"].ToString();
            this._dkkd = row["DKKD"].ToString();
            this._guq = row["GUQ"].ToString();
            this._ma_khtx = row["MA_KHTX"].ToString();
            this._kich_hoat = Convert.ToBoolean(row["KICH_HOAT"].ToString());
        }

        public Chinhanh(string[] chinhanh)
        {
            this._macn = chinhanh[0];
            this._tencn = chinhanh[1];
            this._sdt = chinhanh[2];
            this._diachi = chinhanh[3];
            this._ten_cn_day_du = chinhanh[4];
            this._mst = chinhanh[5];
            this._dkkd = chinhanh[6];
            this._guq = chinhanh[7];
            this._ma_khtx = chinhanh[8];
            this._kich_hoat = Convert.ToBoolean(chinhanh[9]);
        }
    }
}
