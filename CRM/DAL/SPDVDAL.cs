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
    class SPDVDAL
    {
        DataAccess db = new DataAccess();

        public void DELETE_SPDV(string macn)
        {
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            int count = db.cmdExecNonQueryProc("DELETE_SPDV", Params);
        }

        public bool UPDATE_SPDV_DIEN(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_DIEN", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_SKTIENGUI(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_SKTIENGUI", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_SKTIENVAY(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_SKTIENVAY", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_SMSLOAN(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_SMSLOAN", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_ABIC(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_ABIC", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_VNPT(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_VNPT", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_NUOC(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_NUOC", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_SMS(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_SMS", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_THE(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_THE", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_CHUYENTIEN(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_CHUYENTIEN", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_CHUYENTIENN(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_CHUYENTIENN", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_CHUYENLUONG(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_CHUYENLUONG", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_CMSPOS(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_CMSPOS", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_SKTIENGUI_TKHOCDUONG(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_SKTIENGUI_TKHOCDUONG", Params);
            return count != 0;
        }

        public bool UPDATE_SPDV_SKTIENGUI_ANSINH(string macn)
        {
            int count = 0;
            SqlParameter[] Params = new SqlParameter[] 
            {
                 new SqlParameter("@macn", macn)
             };
            count = db.cmdExecNonQueryProc("UPDATE_SPDV_SKTIENGUI_ANSINH", Params);
            return count != 0;
        }

        
    }
}
