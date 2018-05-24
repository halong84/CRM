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
    class DIENDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_DIEN(DataTable dt, string macn)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblDIEN", dt),
                 new SqlParameter("@macn", macn)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_DIEN", Params);
            return count != 0;
        }

        public bool UPDATE_DIEN_MAKH()
        {
            int count = db.cmdExecNonQueryProc("UPDATE_DIEN_MAKH");
            return count != 0;
        }
    }
}
