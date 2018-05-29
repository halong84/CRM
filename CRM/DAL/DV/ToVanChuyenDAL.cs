using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CRM.DAL.DV
{
    class ToVanChuyenDAL
    {
        public static DataTable DV_GET_THONGTINNHANVIEN_MANV(string manv)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@manv", manv)
            };
            DataTable dt = db.dt("DV_GET_THONGTINNHANVIEN_MANV", Params);
            return dt;
        }

        public static string DV_LAYPHONGBAN(string maPb)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", maPb)
            };
            DataTable dt = db.dt("DV_LAYPHONGBAN", Params);
            return dt.Rows[0]["TENPB"].ToString();
        }

        public static DataTable DV_TOVANCHUYEN_NHANVIEN_MANV(string manv)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@manv", manv)
            };
            DataTable dt = db.dt("DV_TOVANCHUYEN_NHANVIEN_MANV", Params);
            return dt;
        }

        public static DataTable DV_TOVANCHUYEN_NHANVIEN_CMND(string cmnd)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@cmnd", cmnd)
            };
            DataTable dt = db.dt("DV_TOVANCHUYEN_NHANVIEN_CMND", Params);
            return dt;
        }

        public static DataTable DV_TOVANCHUYEN_MAPB(string mapb)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", mapb)
            };
            DataTable dt = db.dt("DV_TOVANCHUYEN_MAPB", Params);
            return dt;
        }



        public static void DV_TOVANCHUYEN_UPDATE(
                string mapb,
                bool gtTT,
                bool gtGs1,
                bool gtGs2,
                bool gtBv,
                bool gtLx,
                string tenTT,
                string tenGs1,
                string tenGs2,
                string tenBv,
                string tenLx,
                string chucvuTT,
                string chucvuGs1,
                string chucvuGs2,
                string chucvuBv,
                string chucvuLx,
                string cmndTT,
                string cmndGs1,
                string cmndGs2,
                string cmndBv,
                string cmndLx,
                string ngaycapTT,
                string ngaycapGs1,
                string ngaycapGs2,
                string ngaycapBv,
                string ngaycapLx,
                string noicapTT,
                string noicapGs1,
                string noicapGs2,
                string noicapBv,
                string noicapLx,
                string loaihang,
                string bangso,
                string noiden,
                string phuongtien
                )
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", mapb),
                new SqlParameter("@gtTT", gtTT),
                new SqlParameter("@gtGs1", gtGs1),
                new SqlParameter("@gtGs2", gtGs2),
                new SqlParameter("@gtBv", gtBv),
                new SqlParameter("@gtLx", gtLx),
                new SqlParameter("@tenTT", tenTT),
                new SqlParameter("@tenGs1", tenGs1),
                new SqlParameter("@tenGs2", tenGs2),
                new SqlParameter("@tenBv", tenBv),
                new SqlParameter("@tenLx", tenLx),
                new SqlParameter("@chucvuTT", chucvuTT),
                new SqlParameter("@chucvuGs1", chucvuGs1),
                new SqlParameter("@chucvuGs2", chucvuGs2),
                new SqlParameter("@chucvuBv", chucvuBv),
                new SqlParameter("@chucvuLx", chucvuLx),
                new SqlParameter("@cmndTT", cmndTT),
                new SqlParameter("@cmndGs1", cmndGs1),
                new SqlParameter("@cmndGs2", cmndGs2),
                new SqlParameter("@cmndBv", cmndBv),
                new SqlParameter("@cmndLx", cmndLx),
                new SqlParameter("@ngaycapTT", ngaycapTT),
                new SqlParameter("@ngaycapGs1", ngaycapGs1),
                new SqlParameter("@ngaycapGs2", ngaycapGs2),
                new SqlParameter("@ngaycapBv", ngaycapBv),
                new SqlParameter("@ngaycapLx", ngaycapLx),
                new SqlParameter("@noicapTT", noicapTT),
                new SqlParameter("@noicapGs1", noicapGs1),
                new SqlParameter("@noicapGs2", noicapGs2),
                new SqlParameter("@noicapBv", noicapBv),
                new SqlParameter("@noicapLx", noicapLx),
                new SqlParameter("@loaihang", loaihang),
                new SqlParameter("@bangso", bangso),
                new SqlParameter("@noiden", noiden),
                new SqlParameter("@phuongtien", phuongtien),

            };
            DataTable dt = db.dt("DV_TOVANCHUYEN_UPDATE", Params);
        }

    }
}

