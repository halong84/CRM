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
    class PROFITCTDAL
    {
        DataAccess db = new DataAccess();
        public bool UPDATE_PROFITCT(string macn, string thang)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn),
                 new SqlParameter("@thang", thang)
             };
            count = db.cmdExecNonQueryProc("UPDATE_PROFITCT", Params);
            return count != 0;
        }
    }
}
