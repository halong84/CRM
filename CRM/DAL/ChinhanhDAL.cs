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
    class ChinhanhDAL
    {
        DataAccess db = new DataAccess();
        public DataTable DANH_SACH_CHI_NHANH()
        {
            DataTable dt = db.dt("DANH_SACH_CHI_NHANH");
            return dt;
        }

        public DataTable DANH_SACH_MA_CHI_NHANH()
        {
            DataTable dt = db.dt("DANH_SACH_MA_CHI_NHANH");
            return dt;
        }

        public DataTable CHI_NHANH_THEO_MACN(string macn)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@macn", macn),
            };
            DataTable dt = db.dt("CHI_NHANH_THEO_MACN", Params);
            return dt;
        }
    }
}
