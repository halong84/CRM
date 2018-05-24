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
    class ABICDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_ABIC(DataTable dt, string macn)
        {
            int count = 0;
            
            SqlParameter[] Params = new SqlParameter[] 
            {
                new SqlParameter("@tblABIC", dt), 
                new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_ABIC", Params);
            return count != 0;
        }

        public bool UPDATE_ABIC_MAKH()
        {
            int count = 0;
            count = db.cmdExecNonQueryProc("UPDATE_ABIC_MAKH");
            return count != 0;
        }
    }
}
