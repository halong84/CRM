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
    class CHUYENTIENNBUS
    {
        CHUYENTIENNDAL dal = new CHUYENTIENNDAL();

        public bool UPDATE_CHUYENTIENN(DataTable dt, string macn, string thang, byte loaichuyentien)
        {
            return dal.UPDATE_CHUYENTIENN(dt, macn, thang, loaichuyentien);
        }

        public bool UPDATE_CHUYENTIENN_HOTEN()
        {
            return dal.UPDATE_CHUYENTIENN_HOTEN();
        }
    }
}
