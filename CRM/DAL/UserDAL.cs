using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Entities;
using System.Data.SqlClient;
using System.Globalization;


namespace CRM.DAL
{
    class UserDAL
    {
        DataAccess db = new DataAccess();
        public DataTable XAC_THUC_USER(string user_id, string user_pass)
        {
            SqlParameter[] Params = new SqlParameter[]
                {
                new SqlParameter("@user_id", user_id),
                new SqlParameter("@user_pass", user_pass)
                };
            DataTable dt = db.dt("XAC_THUC_USER", Params);
            return dt;
        }

        public DataTable KIEM_TRA_USER(string user_id)
        {
            SqlParameter[] Params = new SqlParameter[]
                {
                new SqlParameter("@user_id", user_id)
                };
            DataTable dt = db.dt("KIEM_TRA_USER", Params);
            return dt;
        }

        public bool DOI_MAT_KHAU(string user_id, string user_pass)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@user_id", user_id),
                 new SqlParameter("@user_pass", user_pass)
             };
            int count = db.cmdExecNonQueryProc("DOI_MAT_KHAU", Params);
            return count != 0;
        }

        public bool UPDATE__USER(DataTable dt)
        {
            int count = 0;

            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tbl_USER", dt)
             };
            count = db.cmdExecNonQueryProc("UPDATE__USER", Params);
            return count != 0;
        }

        public bool UPDATE__USER_HOATDONG(DataTable dt)
        {
            int count = 0;

            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tbl_USER", dt)
             };
            count = db.cmdExecNonQueryProc("UPDATE__USER_HOATDONG", Params);
            return count != 0;
        }
    }
}
