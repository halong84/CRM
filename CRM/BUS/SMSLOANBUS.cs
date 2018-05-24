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
    class SMSLOANBUS
    {
        SMSLOANDAL dal = new SMSLOANDAL();

        public bool UPDATE_SMSLOAN(DataTable dt)
        {
            return dal.UPDATE_SMSLOAN(dt);
        }
    }
}
