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
    class DIEM_CNDAL
    {
        DataAccess db = new DataAccess();
        public bool UPDATE_CONG_DIEM_CN(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblDIEM_CN", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_CONG_DIEM_CN", Params);
            return count != 0;
        }

        public bool UPDATE_TRU_DIEM_CN(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblDIEM_CN", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_TRU_DIEM_CN", Params);
            return count != 0;
        }
    }
}
