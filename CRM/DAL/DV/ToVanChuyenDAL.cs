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
            string matotruong,
            string magiamsat1,
            string magiamsat2,
            string mabaove,
            string malaixe,
            string loaihang,
            string bangso,
            string noiden,
            string phuongtien
            )
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", mapb),
                new SqlParameter("@matotruong", matotruong),
                new SqlParameter("@magiamsat1", magiamsat1),
                new SqlParameter("@magiamsat2", magiamsat2),
                new SqlParameter("@mabaove", mabaove),
                new SqlParameter("@malaixe", malaixe),
                new SqlParameter("@loaihang", loaihang),
                new SqlParameter("@bangso", bangso),
                new SqlParameter("@noiden", noiden),
                new SqlParameter("@phuongtien", phuongtien),

            };
            DataTable dt = db.dt("DV_TOVANCHUYEN_UPDATE", Params);
        }




        public static void DV_TOVANCHUYEN_INSERT(
                string mapb,
                string matotruong,
                string magiamsat1,
                string magiamsat2,
                string mabaove,
                string malaixe,
                string loaihang,
                string bangso,
                string noiden,
                string phuongtien
                )
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", mapb),
                new SqlParameter("@matotruong", matotruong),
                new SqlParameter("@magiamsat1", magiamsat1),
                new SqlParameter("@magiamsat2", magiamsat2),
                new SqlParameter("@mabaove", mabaove),
                new SqlParameter("@malaixe", malaixe),
                new SqlParameter("@loaihang", loaihang),
                new SqlParameter("@bangso", bangso),
                new SqlParameter("@noiden", noiden),
                new SqlParameter("@phuongtien", phuongtien),

            };
            DataTable dt = db.dt("DV_TOVANCHUYEN_UPDATE", Params);
        }

    }
}

