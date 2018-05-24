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
    class KH_NHOMKHBUS
    {
        KH_NHOMKHDAL dal = new KH_NHOMKHDAL();

        public bool UPDATE_KH_NHOMKH(DataTable dt)
        {
            return dal.UPDATE_KH_NHOMKH(dt);
        }

        public bool UPDATE_KH_NHOMKH_XOA(DataTable dt)
        {
            return dal.UPDATE_KH_NHOMKH_XOA(dt);
        }
    }
}
