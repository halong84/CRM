using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities.DV
{
    class Phongban
    {
        private string _ma_cn;

        public string ma_cn
        {
            get { return _ma_cn; }
            set { _ma_cn = value; }
        }

        private string _ma_pb;

        public string ma_pb
        {
            get { return _ma_pb; }
            set { _ma_pb = value; }
        }

        private string _ten_pb;

        public string ten_pb
        {
            get { return _ten_pb; }
            set { _ten_pb = value; }
        }

        public Phongban(DataRow row)
        {
            this._ma_cn = row["MA_CN"].ToString();
            this._ma_pb = row["MA_PB"].ToString();
            this._ten_pb= row["TEN_PB"].ToString();
        }

        public Phongban(string ma_cn, string ma_pb, string ten_pb)
        {
            this._ma_cn = ma_cn;
            this._ma_pb = ma_pb;
            this._ten_pb = ten_pb;        
        }
    }
}
