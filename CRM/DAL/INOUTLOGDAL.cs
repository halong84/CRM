using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Entities;
using System.Data.SqlClient;
using System.Globalization;


namespace CRM.DAL
{
    class INOUTLOGDAL
    {
        DataAccess db = new DataAccess();

        public bool INSERT_INOUTLOG(string user_id, string ip_address, string action)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@user_id", user_id),
                 new SqlParameter("@ip_address", ip_address),
                 new SqlParameter("@action", action)
             };
            int count = db.cmdExecNonQueryProc("INSERT_INOUTLOG", Params);
            return count != 0;
        }
    }
}
