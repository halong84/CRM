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

        public static void UPDATE_SYSTEM_DETAL(string manv, string col_id, string sys_id, string value)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@manv", manv),
                new SqlParameter("@col_id",col_id),
                new SqlParameter("@sys_id", sys_id),
                new SqlParameter("@value", value)
            };

            db.dt("UPDATE_SYSTEM_DETAIL");
        }

        public static void DT_IPCAS_CHANGE_REQUEST(string userid, string mac, string menu, string chucnang, bool hoatdong, string yeucaukhac, string mapb)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@userid", userid),
                new SqlParameter("@mac", mac),
                new SqlParameter("@menu", menu),
                new SqlParameter("@chucnang", chucnang),
                new SqlParameter("@hoatdong", hoatdong),
                new SqlParameter("@yeucaukhac", yeucaukhac),
                new SqlParameter("@mapb", mapb)
            };

            db.dt("DT_IPCAS_CHANGE_REQUEST", Params);
        }
    }
}
