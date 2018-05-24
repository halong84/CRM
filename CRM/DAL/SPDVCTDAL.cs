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
    class SPDVCTDAL
    {
        DataAccess db = new DataAccess();

        public void DELETE_SPDVCT(string macn, string thang)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn),
                 new SqlParameter("@thang", thang)
             };
            int count = db.cmdExecNonQueryProc("DELETE_SPDVCT", Params);
        }

        public bool UPDATE_SPDVCT(string macn, string thang)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn),
                 new SqlParameter("@thang", thang)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDVCT", Params);
            return count != 0;
        }

        public bool UPDATE_SPDVCT_DIEMDV(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSPDVCT", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_SPDVCT_DIEMDV", Params);
            return count != 0;
        }
    }
}
