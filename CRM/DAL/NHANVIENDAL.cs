using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Entities;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
namespace CRM.DAL
{
    class NHANVIENDAL
    {
        DataAccess db = new DataAccess();

        public DataTable DANH_SACH_NV_THEO_PB_CV(string mapb, string chucvu)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@mapb", mapb),
                new SqlParameter("@chucvu", chucvu)
            };
            DataTable dt = db.dt("DANH_SACH_NV_THEO_PB_CV", Params);
            return dt;
        }

        public DataTable DANH_SACH_NV_THEO_CN_CV(string macn, string chucvu)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@macn", macn),
                new SqlParameter("@chucvu", chucvu)
            };
            DataTable dt = db.dt("DANH_SACH_NV_THEO_CN_CV", Params);
            return dt;
        }

        public DataTable DANH_SACH_NV_THEO_CN_PB(string macn, string mapb)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@macn", macn),
                new SqlParameter("@mapb", mapb)
            };
            DataTable dt = db.dt("DANH_SACH_NV_THEO_CN_PB", Params);
            return dt;
        }

        public DataTable DANH_SACH_NV_THEO_CN_PB_CV(string macn, string mapb, string chucvu)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@macn", macn),
                new SqlParameter("@mapb", mapb),
                new SqlParameter("@chucvu", chucvu)
            };
            DataTable dt = db.dt("DANH_SACH_NV_THEO_CN_PB_CV", Params);
            return dt;
        }

        public DataTable NHAN_VIEN_THEO_MANV(string manv)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@manv", manv)
            };
            DataTable dt = db.dt("NHAN_VIEN_THEO_MANV", Params);
            return dt;
        }

        public bool UPDATE_NHANVIEN(DataTable dt)
        {
            int count = 0;
            int count1 = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblNHANVIEN", dt)
             };
            try
            {
                count = db.cmdExecNonQueryProc("UPDATE_NHANVIEN_TEMP", Params);
                try
                {
                    count1 = db.cmdExecNonQueryProc("UPDATE_NHANVIEN");
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

        public bool UPDATE_NHANVIEN_HOATDONG(DataTable dt)
        {
            int count = 0;
            
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblNHANVIEN", dt)
             };
            count = db.cmdExecNonQueryProc("UPDATE_NHANVIEN_HOATDONG", Params);
            return count != 0;
        }
    }
}
