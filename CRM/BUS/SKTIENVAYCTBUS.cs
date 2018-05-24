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
    class SKTIENVAYCTBUS
    {
        SKTIENVAYCTDAL dal = new SKTIENVAYCTDAL();

        public bool UPDATE_SKTIENVAYCT(string macn, string thang)
        {
            return dal.UPDATE_SKTIENVAYCT(macn, thang);
        }
    }
}
