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
    class TAIKHOANDAL
    {
        DataAccess db = new DataAccess();
        public bool UPDATE_TAIKHOAN()
        {
            int count = db.cmdExecNonQueryProc("UPDATE_TAIKHOAN");
            return count != 0;
        }

        public bool UPDATE_TAIKHOAN_TUFILE(DataTable tbltaikhoan)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblTAIKHOAN", tbltaikhoan)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_TAIKHOAN_TUFILE", Params);
            return count != 0;
        }

        public bool MO_TAIKHOAN(string makh, string sotk, string ccy, string ngaymo)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@makh", makh),
                 new SqlParameter("@sotk", sotk),
                 new SqlParameter("@ccy", ccy),
                 new SqlParameter("@ngaymo", ngaymo)
             };
            int count = db.cmdExecNonQueryProc("MO_TAIKHOAN", Params);
            return count != 0;
        }

        public bool DONG_TAIKHOAN(string sotk, string ngaydong)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@sotk", sotk),
                 new SqlParameter("@ngaydong", ngaydong)
             };
            int count = db.cmdExecNonQueryProc("DONG_TAIKHOAN", Params);
            return count != 0;
        }

        public DataTable TAI_KHOAN_THEO_MAKH(string makh)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@makh", makh),
            };
            DataTable dt = db.dt("TAI_KHOAN_THEO_MAKH", Params);
            return dt;
        }

        public DataTable TAI_KHOAN_THEO_STK(string sotk)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@sotk", sotk),
            };
            DataTable dt = db.dt("TAI_KHOAN_THEO_STK", Params);
            return dt;
        }
    }
}
