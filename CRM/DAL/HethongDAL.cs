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
    class HethongDAL
    {
        DataAccess db = new DataAccess();
        public DataTable HE_THONG()
        {
            DataTable dt = db.dt("HE_THONG");
            return dt;
        }
    }
}
