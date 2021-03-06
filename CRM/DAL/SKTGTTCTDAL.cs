﻿using System;
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
    class SKTGTTCTDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_SKTGTTCT(string macn, string thang)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn),
                 new SqlParameter("@thang", thang)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_SKTGTTCT", Params);
            return count != 0;
        }
    }
}
