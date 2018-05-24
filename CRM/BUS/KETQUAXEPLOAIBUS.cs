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
    class KETQUAXEPLOAIBUS
    {
        KETQUAXEPLOAIDAL dal = new KETQUAXEPLOAIDAL();

        public bool UPDATE_KETQUAXEPLOAI_FROM_DIEMKH(string tuthang, string denthang, string macn, byte loaikh, int sothangxl, string tungay, string denngay)
        {
            return dal.UPDATE_KETQUAXEPLOAI_FROM_DIEMKH(tuthang, denthang, macn, loaikh, sothangxl, tungay, denngay);
        }

        public DataTable CREATE_KETQUAXEPLOAI_XEPLOAI(string macn, byte loaikh, string tuthang, string denthang, string ngaytinhdiem)
        {
            return dal.CREATE_KETQUAXEPLOAI_XEPLOAI(macn, loaikh, tuthang, denthang, ngaytinhdiem);
        }

        public bool UPDATE_KETQUAXEPLOAI_XEPLOAI(DataTable dt)
        {
            return dal.UPDATE_KETQUAXEPLOAI_XEPLOAI(dt);
        }

        public bool UPDATE_KETQUAXEPLOAI_XACNHAN(DataTable dt)
        {
            return dal.UPDATE_KETQUAXEPLOAI_XACNHAN(dt);
        }

        public bool UPDATE_KETQUAXEPLOAI_PHEDUYET(DataTable dt)
        {
            return dal.UPDATE_KETQUAXEPLOAI_PHEDUYET(dt);
        }

        public bool UPDATE_KETQUAXEPLOAI_PHEDUYET_AD(DataTable dt)
        {
            return dal.UPDATE_KETQUAXEPLOAI_PHEDUYET_AD(dt);
        }
    }
}
