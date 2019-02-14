using CRM.Entities.DV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CRM.DAL.DV
{
    class DangKyDichVuDAL
    {
        public static KhachHangDV DV_DANGKYDICHVU_KHACHHANG(string tt)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@tt", tt)
            };

            DataTable tKH = db.dt("DV_DANGKYDICHVU_KHACHHANG", Params);
            if (tKH.Rows.Count == 0) return null;
            return new KhachHangDV(tKH.Rows[0]);
        }

        public static DataTable DV_KHACHHANG(string tt)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@tt", tt)
            };

            return db.dt("DV_DANGKYDICHVU_KHACHHANG", Params);
        }

        public static DataTable DV_GET_CHINHANH (string macn)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@macn", macn)
            };

            return db.dt("DV_GET_CHINHANH", Params);
        }

        public static DataTable GET_MESSAGE()
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@tt", "")
            };

            return db.dt("GET_MESSAGE", Params);
        }
    }
}
