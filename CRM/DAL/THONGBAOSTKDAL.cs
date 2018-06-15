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
    class THONGBAOSTKDAL
    {
        DataAccess db = new DataAccess();

        public bool UPDATE_THONGBAOSTK_KH(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblTHONGBAOSTK", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_THONGBAOSTK_KH", Params);
            return count != 0;
        }
        public bool UPDATE_THONGBAOSTK_CN2(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblTHONGBAOSTK", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_THONGBAOSTK_CN2", Params);
            return count != 0;
        }

        public bool UPDATE_THONGBAOSTK_CN1(DataTable dt)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@tblTHONGBAOSTK", dt)
             };
            int count = db.cmdExecNonQueryProc("UPDATE_THONGBAOSTK_CN1", Params);
            return count != 0;
        }


        public DataTable STK_MAT_THEO_NGAY_KH_BAO(string ngay_kh_bao)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@ngay_kh_bao", ngay_kh_bao)
             };
            DataTable dt = db.dt("STK_MAT_THEO_NGAY_KH_BAO", Params);
            return dt;
        }

        public DataTable STK_MAT_THEO_NGAY_KH_BAO_MACN(string ngay_kh_bao, string macn)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@ngay_kh_bao", ngay_kh_bao),
                 new SqlParameter("@macn", macn)
             };
            DataTable dt = db.dt("STK_MAT_THEO_NGAY_KH_BAO_MACN", Params);
            return dt;
        }

        public DataTable STK_MAT_THEO_MA_KH(string makh)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@makh", makh)
             };
            DataTable dt = db.dt("STK_MAT_THEO_MA_KH", Params);
            return dt;
        }

        public DataTable STK_MAT_THEO_SO_SO(string soso)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@soso", soso)
             };
            DataTable dt = db.dt("STK_MAT_THEO_SO_SO", Params);
            return dt;
        }
    }
}
