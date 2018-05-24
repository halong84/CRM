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
    class LICHSUDIEMDAL
    {
        DataAccess db = new DataAccess();
        public bool UPDATE_LICHSUDIEM(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblLICHSUDIEM", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_LICHSUDIEM", Params);
            return count != 0;
        }

        public bool UPDATE_LICHSUDIEM_PHEDUYET(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblLICHSUDIEM", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_LICHSUDIEM_PHEDUYET", Params);
            return count != 0;
        }
    }
}
