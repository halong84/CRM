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
    class CHUYENTIENDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_CHUYENTIEN(DataTable dt, byte loaichuyentien, string macn, string ccy)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblCHUYENTIEN", dt),
                 new SqlParameter("@loaichuyentien",loaichuyentien),
                 new SqlParameter("@macn", macn),
                 new SqlParameter("@ccy", ccy)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_CHUYENTIEN", Params);
            return count != 0;
        }

        public bool UPDATE_CHUYENTIEN_MAKH()
        {
            int count = db.cmdExecNonQueryProc("UPDATE_CHUYENTIEN_MAKH");
            return count != 0;
        }
    }
}
