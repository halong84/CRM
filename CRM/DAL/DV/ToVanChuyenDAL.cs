using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CRM.DAL.DV
{
    class ToVanChuyenDAL
    {
        public static DataTable DV_GET_THONGTINNHANVIEN_MACN(string maCN)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@manv", maCN)
            };
            DataTable dt = db.dt("DV_GET_THONGTINNHANVIEN_MANV", Params);
            return dt;
        }

        public static string DV_LAYPHONGBAN(string maPb)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@mapb", maPb)
            };
            DataTable dt = db.dt("DV_LAYPHONGBAN", Params);
            return dt.Rows[0]["TENPB"].ToString();
        }
    }
}
