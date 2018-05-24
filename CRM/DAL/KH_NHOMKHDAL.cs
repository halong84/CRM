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
    class KH_NHOMKHDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_KH_NHOMKH(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKH_NHOMKH", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KH_NHOMKH", Params);
            return count != 0;
        }

        public bool UPDATE_KH_NHOMKH_XOA(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKH_NHOMKH", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KH_NHOMKH_XOA", Params);
            return count != 0;
        }
    }
}
