using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Entities.DV;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using CRM.Utilities.DV;
using System.Windows.Forms;

namespace CRM.DAL.DV
{
    class PhatHanhTheGhiNoDAL
    {
        public static KhachHangDV TimKiemKH_TheoMaKH(string makh) {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@makh", makh)
            };
            DataTable dt = db.dt("DV_TimKiemKH_TheoMaKH", Params);
            if (dt.Rows.Count == 0)
                return null;
            else
            {
                KhachHangDV kh = new KhachHangDV(dt.Rows[0]);
                return kh;
            }
        }

        public static KhachHangDV TimKiemKH_TheoCMND(string cmnd)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@cmnd", cmnd)
            };
            DataTable dt = db.dt("DV_TimKiemKH_TheoCMND", Params);
            if (dt.Rows.Count == 0)
                return null;
            else
            {
                KhachHangDV kh = new KhachHangDV(dt.Rows[0]);
                return kh;
            }
        }

        public static DataTable TimSoTK(string makh) {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@makh", makh)
            };
            DataTable dt = db.dt("TimSoTK", Params);
            return dt;
        }

        public static void ThemKH(KhachHangDV kh, string so_tk) {
            DataAccess db = new DataAccess();
            int gt = 0;
            if (kh.gioi_tinh) gt = 1;

            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@cmnd", kh.cmt),
            new SqlParameter("@hoten", kh.ho_ten),
            new SqlParameter("@makh", kh.ma_KH),
            new SqlParameter("@dienthoai", kh.dien_thoai),
            new SqlParameter("@email", kh.email),
            new SqlParameter("@diachi", kh.dia_chi),
            new SqlParameter("@ngaysinh", kh.ngay_sinh),
            new SqlParameter("@ngaycap", kh.ngay_cap),
            new SqlParameter("@noicap", kh.noi_cap),
            new SqlParameter("@quoctich", kh.quoc_tich),
            new SqlParameter("@sotk", so_tk),
            new SqlParameter("@gioitinh", gt)
            };
            db.dt("ThemKH", Params);
        }

        public static void SuaKH(KhachHangDV kh, string so_tk)
        {
            DataAccess db = new DataAccess();

            int gt = 0;
            if (kh.gioi_tinh) gt = 1;

            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@cmnd", kh.cmt),
            new SqlParameter("@hoten", kh.ho_ten),
            new SqlParameter("@makh", kh.ma_KH),
            new SqlParameter("@dienthoai", kh.dien_thoai),
            new SqlParameter("@email", kh.email),
            new SqlParameter("@diachi", kh.dia_chi),
            new SqlParameter("@ngaysinh", kh.ngay_sinh),
            new SqlParameter("@ngaycap", kh.ngay_cap),
            new SqlParameter("@noicap", kh.noi_cap),
            new SqlParameter("@quoctich", kh.quoc_tich),
            new SqlParameter("@sotk", so_tk),
            new SqlParameter("@gioitinh", gt)
            };
            db.dt("SuaKH", Params);
        }

        public static NguoiDaiDien[] DanhSachNguoiDaiDien(string mapb)
        {
            try
            {
                DataAccess db = new DataAccess();
                SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", mapb)
                };
                DataTable dt = db.dt("DV_NGUOIDAIDIEN", Params);
                NguoiDaiDien[] dsNguoiDaiDien = new NguoiDaiDien[dt.Rows.Count];
                for (int i = 0; i < dsNguoiDaiDien.Length; i++)
                {
                    dsNguoiDaiDien[i] = new NguoiDaiDien(dt.Rows[i]);
                }
                return dsNguoiDaiDien;
            }
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
                return null;
            }

        }

        public static void DangKyThe(string soTK, string loaiThe, string hangThe, string hinhThucPhatHanh, string hinhThucNhanThe, string dtdd, int hmgd, int baoHiem, string user, string mapb, bool phatHanhMoi)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                    new SqlParameter("@sotk", soTK),
                    new SqlParameter("@loaithe", loaiThe),
                    new SqlParameter("@hangthe", hangThe),
                    new SqlParameter("@hinhthucphathanh", hinhThucPhatHanh),
                    new SqlParameter("@hinhthucnhanthe", hinhThucNhanThe),
                    new SqlParameter("@dtdd", dtdd),
                    new SqlParameter("@hmgd", hmgd),
                    new SqlParameter("@baohiem", baoHiem),
                    new SqlParameter("@userid", user),
                    new SqlParameter("@mapb", mapb),
                    new SqlParameter("@phathanhmoi", phatHanhMoi)
                };
            db.dt("DV_INSERT_THEODOITHE", Params);
        }

        public static DataTable TimThe(string soTK, string loaiThe)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                    new SqlParameter("@sotk", soTK),
                    new SqlParameter("@loaithe", loaiThe)
            };
            DataTable dt = db.dt("DV_TIMTHE", Params);
            return dt;
        }

        public static bool UPDATE_KHACHHANG(DataTable dt, string nguoicapnhat)
        {
            int count = 0;
            int count1 = 0;
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKHACHHANG", dt)
             };
            try
            {
                count = db.cmdExecNonQueryProc("UPDATE_KHACHHANG_TEMP", Params);
                try
                {
                    SqlParameter[] Params2 = new SqlParameter[] 
                    {
                         new SqlParameter("@nguoicapnhat", nguoicapnhat)
                    };
                    count1 = db.cmdExecNonQueryProc("UPDATE_KHACHHANG", Params2);
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }

            return count1 != 0;
        }

        public static void DV_UPDATE_SOTK(string maKH, string soTK)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@makh", maKH),
                new SqlParameter("@soTK", soTK)
            };
            db.dt("DV_UPDATE_SOTK", Params);
        }

        public static string DV_GET_NOICAPCMND(string ma)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@ma", ma)
            };
            return db.dt("DV_GET_NOICAPCMND", Params).Rows[0]["NOICAP"].ToString();
        }


        public static DataTable DV_GIAYHEN_SELECT(string mapb)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", mapb)
            };
            return db.dt("DV_GIAYHEN_SELECT", Params);
        }

        public static void DV_GIAYHEN_UPDATE(string mapb, string giayto1, string giayto2, string giayto3)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", mapb),
                new SqlParameter("@giayto1", giayto1),
                new SqlParameter("@giayto2", giayto2),
                new SqlParameter("@giayto3", giayto3)
            };
            db.dt("DV_GIAYHEN_UPDATE", Params);
        }

        public static void DV_GIAYUYQUYEN_UPDATE(string manv, string uyquyen)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@manv", manv),
                new SqlParameter("@uyquyen", uyquyen),
            };
            db.dt("DV_GIAYUYQUYEN_UPDATE", Params);
        }
    }
}
