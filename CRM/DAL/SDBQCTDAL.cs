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
    class SDBQCTDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_SDBQCT(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSDBQCT", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_SDBQCT", Params);
            return count != 0;
        }

        public bool UPDATE_SDBQCT_DIEM(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSDBQCT", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_SDBQCT_DIEM", Params);
            return count != 0;
        }
    }
}
