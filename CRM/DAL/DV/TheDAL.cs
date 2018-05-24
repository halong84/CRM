using CRM.Utilities.DV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CRM.DAL.DV
{
    class TheDAL
    {
        public static DataTable TheChuaNhan(string tuNgay, string denNgay)
        {
            DateTime _tuNgay = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _denNgay = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@tungay", _tuNgay),
            new SqlParameter("@denngay", _denNgay),
            new SqlParameter("@mapb", Thong_tin_dang_nhap.ma_pb)
            };
            DataTable dt = db.dt("DV_THECHUANHAN", Params);

            return dt;
        }

        public static DataTable TheDaNhanChuaGiao(string tuNgay, string denNgay)
        {
            DateTime _tuNgay = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _denNgay = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@tungay", _tuNgay),
            new SqlParameter("@denngay", _denNgay),
            new SqlParameter("@mapb", Thong_tin_dang_nhap.ma_pb)
            };
            DataTable dt = db.dt("DV_THEDANHAN_CHUAGIAO", Params);

            return dt;
        }

        public static DataTable TheDaGiao(string tuNgay, string denNgay)
        {
            DateTime _tuNgay = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _denNgay = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@tungay", _tuNgay),
            new SqlParameter("@denngay", _denNgay),
            new SqlParameter("@mapb", Thong_tin_dang_nhap.ma_pb)
            };
            DataTable dt = db.dt("DV_THEDAGIAO", Params);

            return dt;
        }

        public static DataTable TatCaThe(string tuNgay, string denNgay)
        {
            DateTime _tuNgay = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _denNgay = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@tungay", _tuNgay),
            new SqlParameter("@denngay", _denNgay),
            new SqlParameter("@mapb", Thong_tin_dang_nhap.ma_pb)
            };
            DataTable dt = db.dt("DV_TATCATHE", Params);

            return dt;
        }

        public static DataTable TheTheoMaKH(string maKH)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@makh", maKH)
            };
            DataTable dt = db.dt("DV_THETHEOMAKH", Params);

            return dt;
        }

        public static DataTable TheTheoCMND(string cmnd)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@cmnd", cmnd)
            };
            DataTable dt = db.dt("DV_THETHEOCMND", Params);

            return dt;
        }

        public static void XoaThe_TheoSoThe(string soThe)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@sothe", soThe)
            };
            db.dt("DV_XOATHE_THEOSOTHE", Params);
        }

        public static void XoaThe_TheoSoTK_LoaiThe(string soTK, string loaiThe)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@sotk", soTK),
            new SqlParameter("@loaithe", loaiThe)
            };
            db.dt("DV_XOATHE_THEOSOTK_LOAITHE", Params);
        }
    }
}
