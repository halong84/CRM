using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Entities;
using CRM.DAL;
using System.Data;

namespace CRM.BUS
{
    class DIEMKHBUS
    {
        DIEMKHDAL dal = new DIEMKHDAL();

        public bool UPDATE_DIEMKH(DataTable dt)
        {
            return dal.UPDATE_DIEMKH(dt);
        }

        public bool UPDATE_DIEMKH_DIEMSPDV(DataTable dt)
        {
            return dal.UPDATE_DIEMKH_DIEMSPDV(dt);
        }

        public bool UPDATE_DIEMKH_XEPLOAI(DataTable dt)
        {
            return dal.UPDATE_DIEMKH_XEPLOAI(dt);
        }

        public DataTable CREATE_DIEMKH_SDBQ(string macn, string ngaytinhdiem, string thang)
        {
            return dal.CREATE_DIEMKH_SDBQ(macn, ngaytinhdiem, thang);
        }
        public DataTable CREATE_DIEMKH_SPDV(string macn, string ngaytinhdiem, string thang)
        {
            return dal.CREATE_DIEMKH_SPDV(macn, ngaytinhdiem, thang);
        }

        public DataTable CREATE_DIEMKH_XEPLOAI(string macn, string ngaytinhdiem, string thang, byte loaikh)
        {
            return dal.CREATE_DIEMKH_XEPLOAI(macn, ngaytinhdiem, thang, loaikh);
        }
    }
}
