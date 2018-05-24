using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities.DV
{
    class KhachHangDV
    {
        private string _ma_KH;
        private string _ho_ten;
        private string _cmt;
        private DateTime _ngay_cap;
        private string _noi_cap;
        private string _quoc_tich;
        private bool _gioi_tinh;
        private DateTime _ngay_sinh;
        private string _dien_thoai;
        private string _email;
        private string _dia_chi;


        public string ma_KH
        {
            get { return _ma_KH; }
            set { _ma_KH = value; }
        }


        public string ho_ten
        {
            get { return _ho_ten; }
            set { _ho_ten = value; }
        }


        public string cmt
        {
            get { return _cmt; }
            set { _cmt = value; }
        }


        public DateTime ngay_cap
        {
            get { return _ngay_cap; }
            set { _ngay_cap = value; }
        }


        public string noi_cap
        {
            get { return _noi_cap; }
            set { _noi_cap = value; }
        }


        public string quoc_tich
        {
            get { return _quoc_tich; }
            set { _quoc_tich = value; }
        }

        public bool gioi_tinh {
            get { return _gioi_tinh; }
            set { _gioi_tinh = value; }
        }

        public DateTime ngay_sinh
        {
            get { return _ngay_sinh; }
            set { _ngay_sinh = value; }
        }

        public string dien_thoai
        {
            get { return _dien_thoai; }
            set { _dien_thoai = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }


        public string dia_chi
        {
            get { return _dia_chi; }
            set { _dia_chi = value; }
        }

        public KhachHangDV()
        {

        }

        public KhachHangDV(string ma_KH, string ho_ten, string cmt, DateTime ngay_cap, string noi_cap, string quoc_tich, string dien_thoai, string email, string dia_chi, DateTime ngay_sinh, bool gioi_tinh)
        {
            this._ma_KH = ma_KH;
            this._ho_ten = ho_ten;
            this._cmt = cmt;
            this._ngay_cap = ngay_cap;
            this._noi_cap = noi_cap;
            this._quoc_tich = quoc_tich;
            this._gioi_tinh = gioi_tinh;
            this._dien_thoai = dien_thoai;
            this._email = email;
            this._dia_chi = dia_chi;
            this._ngay_sinh = ngay_sinh;
        }

        public KhachHangDV(DataRow row)
        {
            this._ma_KH = row["MAKH"].ToString();
            this._ho_ten = row["HOTEN"].ToString();
            this._cmt = row["CMND"].ToString();
            this._ngay_cap = (DateTime)row["NGAYCAP"];
            this._noi_cap = row["NOICAP"].ToString();
            this._quoc_tich = "Viet Nam";
            this._gioi_tinh = (bool)row["GIOITINH"];
            this._dien_thoai = row["DIENTHOAI1"].ToString();
            this._email = row["EMAIL"].ToString();
            this._dia_chi = row["DIACHI1"].ToString();
            this._ngay_sinh = (DateTime)row["NGAYSINH"];
        }

    }
}
