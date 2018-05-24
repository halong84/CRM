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
    class PhongDAL
    {
        DataAccess db = new DataAccess();
        public DataTable DANH_SACH_PHONG_BAN(string macn)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@macn", macn),
            };
            DataTable dt = db.dt("DANH_SACH_PHONG_BAN", Params);
            return dt;
        }
    }
}
