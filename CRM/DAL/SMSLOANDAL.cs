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
    class SMSLOANDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_SMSLOAN(DataTable dt)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                new SqlParameter("@tblSMSLOAN", dt), 
             };
            count = db.cmdExecNonQueryProc("UPDATE_SMSLOAN", Params);
            return count != 0;
        }
    }
}
