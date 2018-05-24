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
    class SDBQNTDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_SDBQNT(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSDBQNT", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_SDBQNT", Params);
            return count != 0;
        }
    }
}
