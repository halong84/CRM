using CRM.Utilities.DV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRM.Entities.DV
{
    class The
    {
        private int _ID;
        private string _soThe;
        private string _hoTen;
        private string _soTK;
        private string _loaiThe;
        private string _hangThe;
        private string _hinhThucPhatHanh;
        private string _hinhThucNhanThe;
        private string _dtdd;
        private int _hmgd;
        private bool _baoHiem;
        private string _userPhatHanh;
        private string _maPB;
        private DateTime _ngayDK;
        private DateTime _ngayNhan;
        private DateTime _ngayGiao;
        private bool _isPhatHanhMoi;

        public The(DataRow row)
        {
            _ID = Convert.ToInt32(row["ID"]);
            _soThe = row["SOTHE"].ToString();
            _hoTen = row["HOTEN"].ToString();
            _soTK = row["SOTK"].ToString();
            _loaiThe = row["LOAITHE"].ToString();
            _hangThe = row["HANGTHE"].ToString();
            _hinhThucPhatHanh = row["HINHTHUCPHATHANH"].ToString();
            _hinhThucNhanThe = row["HINHTHUCNHANTHE"].ToString();
            _dtdd = row["DTDDSMS"].ToString();
            _hmgd = (int)row["HMGD"];
            _baoHiem = (bool)row["BAOHIEM"];
            _userPhatHanh = row["USERID"].ToString();
            _maPB = row["MAPB"].ToString();
            if (!string.IsNullOrEmpty(row["NGAYDANGKY"].ToString()))
                _ngayDK = (DateTime)row["NGAYDANGKY"];
            if (!string.IsNullOrEmpty(row["NGAYNHANTHE"].ToString()))
                _ngayNhan = (DateTime)row["NGAYNHANTHE"];
            if (!string.IsNullOrEmpty(row["NGAYGIAOTHE"].ToString()))
                _ngayGiao = (DateTime)row["NGAYGIAOTHE"];
            isPhatHanhMoi = Convert.ToBoolean(row["PHATHANHMOI"]);
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string soThe
        {
            get { return _soThe; }
            set { _soThe = value; }
        }

        public string hoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }

        public string soTK
        {
            get { return _soTK; }
            set { _soTK = value; }
        }

        public string loaiThe
        {
            get { return _loaiThe; }
            set { _loaiThe = value; }
        }

        public string hangThe
        {
            get { return _hangThe; }
            set { _hangThe = value; }
        }

        public string hinhThucPhatHanh
        {
            get { return _hinhThucPhatHanh; }
            set { _hinhThucPhatHanh = value; }
        }

        public string hinhThucNhanThe
        {
            get { return _hinhThucNhanThe; }
            set { _hinhThucNhanThe = value; }
        }

        public string dtdd
        {
            get { return _dtdd; }
            set { _dtdd = value; }
        }

        public int hmgd
        {
            get { return _hmgd; }
            set { _hmgd = value; }
        }

        public bool baoHiem
        {
            get { return _baoHiem; }
            set { _baoHiem = value; }
        }

        public string userPhatHanh
        {
            get { return _userPhatHanh; }
            set { _userPhatHanh = value; }
        }

        public string maPB
        {
            get { return _maPB; }
            set { _maPB = value; }
        }

        public DateTime ngayDK
        {
            get { return _ngayDK; }
            set { _ngayDK = value; }
        }

        public DateTime ngayNhan
        {
            get { return _ngayNhan; }
            set { _ngayNhan = value; }
        }

        public DateTime ngayGiao
        {
            get { return _ngayGiao; }
            set { _ngayGiao = value; }
        }

        public bool isPhatHanhMoi
        {
            get { return _isPhatHanhMoi; }
            set { _isPhatHanhMoi = value; }
        }
    }
}
