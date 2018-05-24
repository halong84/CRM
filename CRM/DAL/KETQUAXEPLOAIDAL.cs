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
    class KETQUAXEPLOAIDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_KETQUAXEPLOAI_FROM_DIEMKH(string tuthang, string denthang, string macn, byte loaikh, int sothangxl, string tungay, string denngay)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tuthang", tuthang),
                 new SqlParameter("@denthang", denthang),
                 new SqlParameter("@macn", macn),
                 new SqlParameter("@loaikh", loaikh),
                 new SqlParameter("@sothangxl", sothangxl),
                 new SqlParameter("@tungay", tungay),
                 new SqlParameter("@denngay", denngay)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KETQUAXEPLOAI_FROM_DIEMKH", Params);
            return count != 0;
        }

        public DataTable CREATE_KETQUAXEPLOAI_XEPLOAI(string macn, byte loaikh, string tuthang, string denthang, string ngaytinhdiem)
        {
            SqlParameter[] Params = new SqlParameter[]
                {
                new SqlParameter("@macn", macn),
                new SqlParameter("@loaikh", loaikh),
                new SqlParameter("@tuthang", tuthang),
                new SqlParameter("@denthang", denthang),
                new SqlParameter("@ngaytinhdiem", ngaytinhdiem)
                };
            DataTable dt = db.dt("CREATE_KETQUAXEPLOAI_XEPLOAI", Params);
            return dt;
        }

        public bool UPDATE_KETQUAXEPLOAI_XEPLOAI(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKETQUAXEPLOAI", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KETQUAXEPLOAI_XEPLOAI", Params);
            return count != 0;
        }

        public bool UPDATE_KETQUAXEPLOAI_XACNHAN(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKETQUAXEPLOAI", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KETQUAXEPLOAI_XACNHAN", Params);
            return count != 0;
        }

        public bool UPDATE_KETQUAXEPLOAI_PHEDUYET(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKETQUAXEPLOAI", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KETQUAXEPLOAI_PHEDUYET", Params);
            return count != 0;
        }

        public bool UPDATE_KETQUAXEPLOAI_PHEDUYET_AD(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblKETQUAXEPLOAI", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_KETQUAXEPLOAI_PHEDUYET_AD", Params);
            return count != 0;
        }
    }
}
