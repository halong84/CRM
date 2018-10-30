using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CRM.DAL.DT
{
    class IPCASDAL
    {
        public static DataTable DT_GET_CHUCNANG_IPCAS()
        {
            DataAccess db = new DataAccess();
            DataTable dt = db.dt("DT_GET_CHUCNANG_IPCAS");
            if (dt.Rows.Count == 0) return null;
            return dt;
        }

        public static DataTable DT_GET_MENU_IPCAS()
        {
            DataAccess db = new DataAccess();
            DataTable dt = db.dt("DT_GET_MENU_IPCAS");
            if (dt.Rows.Count == 0) return null;
            return dt;
        }

        public static DataTable DANH_SACH_NV_THEO_PB_CV(string mapb, string chucvu)
        {
            DataAccess db = new DataAccess();

            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@mapb", mapb),
                new SqlParameter("@chucvu", chucvu)
            };
            DataTable dt = db.dt("DV_DS_NHANVIEN_PB_CV", Params);
            return dt;
        }

        public static DataTable DANHSACH_PB(string macn)
        {
            DataAccess db = new DataAccess();

            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@macn", macn)
            };
            return db.dt("DANH_SACH_PHONG_BAN", Params);
        }
    }
}
