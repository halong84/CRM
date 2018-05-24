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
    class SKTIENGUIBUS
    {
        SKTIENGUIDAL dal = new SKTIENGUIDAL();

        public bool UPDATE_SKTIENGUI(DataTable dt, string macn, string ccy)
        {
            return dal.UPDATE_SKTIENGUI(dt, macn, ccy);
        }
    }
}
