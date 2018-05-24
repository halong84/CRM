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
    class CHUYENTIENNDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_CHUYENTIENN(DataTable dt, string macn, string thang, byte loaichuyentien)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                new SqlParameter("@tblCHUYENTIENN", dt),
                new SqlParameter("@macn", macn),
                new SqlParameter("@thang", thang),
                new SqlParameter("@loaichuyentien", loaichuyentien)
             };
            count = db.cmdExecNonQueryProc("UPDATE_CHUYENTIENN", Params);
            return count != 0;
        }

        public bool UPDATE_CHUYENTIENN_HOTEN()
        {
            int count = 0;
            count = db.cmdExecNonQueryProc("UPDATE_CHUYENTIENN_HOTEN");
            return count != 0;
        }
    }
}
