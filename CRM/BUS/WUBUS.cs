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
    class WUBUS
    {
        WUDAL dal = new WUDAL();

        public bool UPDATE_WU(DataTable dt, string macn, string thang)
        {
            return dal.UPDATE_WU(dt, macn, thang);
        }

        public bool UPDATE_WU_MAKH(string macn)
        {
            return dal.UPDATE_WU_MAKH(macn);
        }
    }
}
