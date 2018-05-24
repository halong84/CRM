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
    class SKTGTTCTBUS
    {
        SKTGTTCTDAL dal = new SKTGTTCTDAL();

        public bool UPDATE_SKTGTTCT(string macn, string thang)
        {
            return dal.UPDATE_SKTGTTCT(macn,thang);
        }
    }
}
