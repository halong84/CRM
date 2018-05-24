using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CRM.Utilities.DV;


namespace CRM.Entities.DV
{
    class NguoiDaiDien
    {
        private string _hoTen;
        private string _chucVu;
        private string _chiNhanh;
        private string _diaChi;
        private string _Sdt;
        private string _Fax;
        private string _giayUQ;
        

        public NguoiDaiDien(DataRow row)
        {
            _hoTen = row["HOTEN"].ToString();
            _chucVu = row["CHUCVU"].ToString();
            _giayUQ = row["UYQUYEN2"].ToString();
            _chiNhanh = Thong_tin_dang_nhap.ten_cn;
            _diaChi = Thong_tin_dang_nhap.dia_chi_cn;
            _Sdt = row["SDT"].ToString();
            _Fax = row["FAX"].ToString();
        }

        public string hoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }

        public string chucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; }
        }

        public string chiNhanh
        {
            get { return _chiNhanh; }
            set { _chiNhanh = value; }
        }

        public string diaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; }
        }

        public string Sdt
        {
            get { return _Sdt; }
            set { _Sdt = value; }
        }

        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        public string giayUQ
        {
            get { return _giayUQ; }
            set { _giayUQ = value; }
        }


        

    }
}
