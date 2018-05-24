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
    class SKTIENGUIDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_SKTIENGUI(DataTable dt, string macn, string ccy)
        {
            int count = 0;
            int count1 = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSKTIENGUI", dt)
             };
            try
            {
                count = db.cmdExecNonQueryProc("UPDATE_SKTIENGUI_TEMP", Params);
                try
                {
                    SqlParameter[] Params2 = new SqlParameter[]
                    {
                        new SqlParameter("@macn", macn),
                        new SqlParameter("@ccy", ccy)
                    };
                    count1 = db.cmdExecNonQueryProc("UPDATE_SKTIENGUI", Params2);
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
