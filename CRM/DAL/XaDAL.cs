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
    class XaDAL
    {
        DataAccess db = new DataAccess();

        public DataTable DANH_SACH_XA_ALL()
        {
            DataTable dt = db.dt("DANH_SACH_XA_ALL");
            return dt;
        }

        public DataTable DANH_SACH_XA(string mahuyen)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@mahuyen", mahuyen)
            };
            DataTable dt = db.dt("DANH_SACH_XA", Params);
            return dt;
        }

        public DataTable DANH_SACH_XA_THEO_TINH(string matinh)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@matinh", matinh)
            };
            DataTable dt = db.dt("DANH_SACH_XA_THEO_TINH", Params);
            return dt;
        }
    }
}
