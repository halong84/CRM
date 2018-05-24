using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Entities.DV;
using System.Data.SqlClient;
using System.Globalization;
using CRM.DAL.DV;


namespace CRM.DAL.DV
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
            return count > 0;
        }
    }
}
