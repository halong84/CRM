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
    class MAUBIEUDAL
    {
        DataAccess db = new DataAccess();
        public DataTable DANH_SACH_MAU_BIEU(string nghiepvu, string menu_id)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@nghiepvu", nghiepvu),
                new SqlParameter("@menu_id", menu_id),
            };
            DataTable dt = db.dt("DANH_SACH_MAU_BIEU", Params);
            return dt;
        }
    }
}
