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
    class CHUYENTIENBUS
    {
        CHUYENTIENDAL dal = new CHUYENTIENDAL();

        public bool UPDATE_CHUYENTIEN(DataTable dt, byte loaichuyentien, string macn, string ccy)
        {
            return dal.UPDATE_CHUYENTIEN(dt, loaichuyentien, macn, ccy);
        }

        public bool UPDATE_CHUYENTIEN_MAKH()
        {
            return dal.UPDATE_CHUYENTIEN_MAKH();
        }
    }
}
