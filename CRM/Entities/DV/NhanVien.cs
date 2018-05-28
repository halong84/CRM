using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Entities.DV
{
    class NhanVien
    {
        public string maNV;
        public string hoTen;
        public string chucVu;
        public string maPb;
        public string maCn;
        public string uyQuyen1;
        public string uyQuyen2;
        public string uyQuyen3;
        public bool gioiTinh;
        public DateTime ngaySinh;
        public bool hoatDong;
        public string CMND;
        public DateTime ngayCap;
        public string noiCap;

        public NhanVien(System.Data.DataRow r)
        {
            maNV = r["MANV"].ToString();
            hoTen = r["HOTEN"].ToString();
            chucVu = r["CHUCVU"].ToString();
            maPb = r["MAPB"].ToString();
            maCn = r["MACN"].ToString();
            uyQuyen1 = r["UYQUYEN1"].ToString();
            uyQuyen2 = r["UYQUYEN2"].ToString();
            uyQuyen3 = r["UYQUYEN3"].ToString();
            gioiTinh = Convert.ToBoolean(r["GIOITINH"].ToString());
            ngaySinh = Convert.ToDateTime(r["NGAYSINH"].ToString());
            hoatDong = Convert.ToBoolean(r["HOATDONG"].ToString());
            CMND = r["CMND"].ToString();
            ngayCap = Convert.ToDateTime(r["NGAYCAP"].ToString());
            noiCap = r["NOICAP"].ToString();
        }
    }
}
