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
    class THEBUS
    {
        THEDAL dal = new THEDAL();

        public bool UPDATE_THE(DataTable dt)
        {
            return dal.UPDATE_THE(dt);
        }
    }
}
