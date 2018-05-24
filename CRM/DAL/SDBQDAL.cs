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
    class SDBQDAL
    {
        DataAccess db = new DataAccess();

        public bool Update_SDBQ(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSDBQ", dt)
             };
            int count = db.cmdExecNonQueryProc("Update_SDBQ", Params);
            return count != 0;
        }

        public bool UPDATE_SDBQ_PROFITRATIO(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSDBQ", dt)

             };
            int count = db.cmdExecNonQueryProc("UPDATE_SDBQ_PROFITRATIO", Params);
            return count != 0;
        }


        //public void Update_SDBQ(DataTable dt)
        //{
        //    SqlParameter[] Params = new SqlParameter[] 
        //    {
        //         new SqlParameter("@tblSDBQ", dt)
        //     };
        //    try
        //    {
        //        int count = db.cmdExecNonQueryProc("Update_SDBQ", Params);
        //        MessageBox.Show("Nhập dữ liệu số dư bình quân VND thành công"+Convert.ToString(count));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
