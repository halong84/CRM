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
    class SPDVCTBUS
    {
        SPDVCTDAL dal = new SPDVCTDAL();

        public void DELETE_SPDVCT(string macn, string thang)
        {
            dal.DELETE_SPDVCT(macn, thang);
        }

        public bool UPDATE_SPDVCT(string macn, string thang)
        {
            return dal.UPDATE_SPDVCT(macn, thang);
        }

        public bool UPDATE_SPDVCT_DIEMDV(DataTable dt)
        {
            return dal.UPDATE_SPDVCT_DIEMDV(dt);
        }
    }
}
