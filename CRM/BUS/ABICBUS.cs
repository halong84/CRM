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
    class ABICBUS
    {
        ABICDAL dal = new ABICDAL();

        public bool UPDATE_ABIC(DataTable dt, string macn)
        {
            return dal.UPDATE_ABIC(dt, macn);
        }

        public bool UPDATE_ABIC_MAKH()
        {
            return dal.UPDATE_ABIC_MAKH();
        }
    }
}
