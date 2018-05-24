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
    class KHACHHANGDAL
    {
        DataAccess db = new DataAccess();
  
        public bool UPDATE_KHACHHANG(DataTable dt, string nguoicapnhat)
        {
            int count = 0;
            int count1 = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKHACHHANG", dt)
             };
            try
            {
                count = db.cmdExecNonQueryProc("UPDATE_KHACHHANG_TEMP", Params);
                try
                {
                    SqlParameter[] Params2 = new SqlParameter[] 
                    {
                         new SqlParameter("@nguoicapnhat", nguoicapnhat)
                    };
                    count1 = db.cmdExecNonQueryProc("UPDATE_KHACHHANG", Params2);
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

        public DataTable KH_THEO_MAKH(string makh)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@makh", makh)
            };
            DataTable dt = db.dt("KH_THEO_MAKH", Params);
            return dt;
        }
    }
}
