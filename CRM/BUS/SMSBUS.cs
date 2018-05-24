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
    class SMSBUS
    {
        SMSDAL dal = new SMSDAL();

        public bool UPDATE_SMS(DataTable dt)
        {
            return dal.UPDATE_SMS(dt);
        }
    }
}
