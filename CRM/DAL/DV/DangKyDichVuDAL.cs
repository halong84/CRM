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

            DataTable dt = db.dt("DV_DANGKYDICHVU_KHACHHANG", Params);
            if (dt.Rows.Count == 0) return null;
            return new KhachHangDV(dt.Rows[0]);
        }
    }
}
