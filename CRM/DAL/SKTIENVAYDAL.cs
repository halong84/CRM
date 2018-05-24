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
    class SKTIENVAYDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_SKTIENVAY(DataTable dt, string macn)
        {
            int count = 0;
            int count1 = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSKTIENVAY", dt)
             };
            try
            {
                count = db.cmdExecNonQueryProc("UPDATE_SKTIENVAY_TEMP", Params);
                try
                {
                    SqlParameter[] Params2 = new SqlParameter[] 
                    {
                         new SqlParameter("@macn", macn)
                    };
                    count1 = db.cmdExecNonQueryProc("UPDATE_SKTIENVAY", Params2);
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }

            return count1 != 0;
        }
    }
}
