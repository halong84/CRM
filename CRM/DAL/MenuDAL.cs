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
    class MenuDAL
    {
        DataAccess db = new DataAccess();
        public DataTable DANH_SACH_MENU()
        {
            DataTable dt = db.dt("DANH_SACH_MENU");
            return dt;
        }

        public DataTable DANH_SACH_MAIN_MENU()
        {
            DataTable dt = db.dt("DANH_SACH_MAIN_MENU");
            return dt;
        }

        public DataTable DANH_SACH_SUB_MENU(string parent_id)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@parent_id", parent_id)
            };
            DataTable dt = db.dt("DANH_SACH_SUB_MENU", Params);
            return dt;
        }

        public DataTable DANH_SACH_FORM(string menu_title)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@menu_title", menu_title)
            };
            DataTable dt = db.dt("DANH_SACH_FORM", Params);
            return dt;
        }
    }
}
