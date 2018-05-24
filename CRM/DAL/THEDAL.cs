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
    class THEDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_THE(DataTable dt)
        {
            int count = 0;
            int count1 = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblTHE", dt)
             };
            try
            {
                count = db.cmdExecNonQueryProc("UPDATE_THE_TEMP", Params);
                try
                {
                    count1 = db.cmdExecNonQueryProc("UPDATE_THE");
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
