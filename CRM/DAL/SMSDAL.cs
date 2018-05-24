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
    class SMSDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_SMS(DataTable dt)
        {
            int count = 0;
            int count1 = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblSMS", dt)
             };
            try
            {
                count = db.cmdExecNonQueryProc("UPDATE_SMS_TEMP", Params);
                try
                {
                    count1 = db.cmdExecNonQueryProc("UPDATE_SMS");
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
