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
    class HuyenDAL
    {
        DataAccess db = new DataAccess();

        public DataTable DANH_SACH_HUYEN_ALL()
        {
            DataTable dt = db.dt("DANH_SACH_HUYEN_ALL");
        return dt;
        }

        public DataTable DANH_SACH_HUYEN(string matinh)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@matinh", matinh)
            };
            DataTable dt = db.dt("DANH_SACH_HUYEN", Params);
            return dt;
        }
    }
}
