using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CRM.DAL
{
    class DungChungDAL
    {
        //DataTable dt;

        public static DataTable GET_THONGTIN_NHANVIEN(string manv)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@manv", manv)
            };
            DataTable dt = db.dt("NHAN_VIEN_THEO_MANV", Params);
            return dt;
        }

        public static DataTable GET_ALL_NOICAP_CMND()
        {
            DataAccess db = new DataAccess();
            return db.dt("GET_ALL_NOICAP_CMND");
        }

        public static void UPDATE_NHANVIEN_DT(
            string manv,
            DateTime ngaySinh,
            string noiSinh,
            string diaChi,
            string cmnd,
            DateTime ngayCapCMND,
            string noiCap,
            string sdtNhaRieng,
            string diDong
            )
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@manv", manv),
                new SqlParameter("@ngaysinh", ngaySinh),
                new SqlParameter("@noisinh", noiSinh),
                new SqlParameter("@diachi", diaChi),
                new SqlParameter("@cmnd", cmnd),
                new SqlParameter("@ngaycap", ngayCapCMND),
                new SqlParameter("@noicap", noiCap),
                new SqlParameter("@sdtnharieng", sdtNhaRieng),
                new SqlParameter("@didong", diDong)
            };

            db.dt("UPDATE_NHANVIEN_DT", Params);
        }
    }
}
