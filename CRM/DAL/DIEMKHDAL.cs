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
    class DIEMKHDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_DIEMKH(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblDIEMKH", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_DIEMKH", Params);
            return count != 0;
        }

        public bool UPDATE_DIEMKH_DIEMSPDV(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblDIEMKH", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_DIEMKH_DIEMSPDV", Params);
            return count != 0;
        }

        public bool UPDATE_DIEMKH_XEPLOAI(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblDIEMKH", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_DIEMKH_XEPLOAI", Params);
            return count != 0;
        }

        public DataTable CREATE_DIEMKH_SDBQ(string macn, string ngaytinhdiem, string thang)
        {
            SqlParameter[] Params = new SqlParameter[]
                {
                new SqlParameter("@macn", macn),
                new SqlParameter("@ngaytinhdiem", ngaytinhdiem),
                new SqlParameter("@thang", thang)
                };
            DataTable dt = db.dt("CREATE_DIEMKH_SDBQ", Params);
            return dt;
        }

        public DataTable CREATE_DIEMKH_SPDV(string macn, string ngaytinhdiem, string thang)
        {
            SqlParameter[] Params = new SqlParameter[]
                {
                new SqlParameter("@macn", macn),
                new SqlParameter("@ngaytinhdiem", ngaytinhdiem),
                new SqlParameter("@thang", thang)
                };
            DataTable dt = db.dt("CREATE_DIEMKH_SPDV", Params);
            return dt;
        }

        public DataTable CREATE_DIEMKH_XEPLOAI(string macn, string ngaytinhdiem, string thang, byte loaikh)
        {
            SqlParameter[] Params = new SqlParameter[]
                {
                new SqlParameter("@macn", macn),
                new SqlParameter("@ngaytinhdiem", ngaytinhdiem),
                new SqlParameter("@thang", thang),
                new SqlParameter("@loaikh", loaikh)
                };
            DataTable dt = db.dt("CREATE_DIEMKH_XEPLOAI", Params);
            return dt;
        }
    }
}
