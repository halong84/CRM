using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Entities;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace CRM.DAL
{
    class KhachHangChuyenTienDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_KhachHangChuyenTien(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKhachHangChuyenTien", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KhachHangChuyenTien", Params);
            return count != 0;
        }

        public bool UPDATE_KHACHHANGCHUYENTIEN_WU(string macn, string thang)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn),
                 new SqlParameter("@thang", thang)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KHACHHANGCHUYENTIEN_WU", Params);
            return count != 0;
        }
    }
}
