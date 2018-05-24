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
    class SDBQBUS
    {
        SDBQDAL dal = new SDBQDAL();

        public bool Update_SDBQ(DataTable dt)
        {
            return dal.Update_SDBQ(dt);
        }

        public bool UPDATE_SDBQ_PROFITRATIO(DataTable dt)
        {
            return dal.UPDATE_SDBQ_PROFITRATIO(dt);
        }

        //public void Update_SDBQ(DataTable dt)
        //{
        //    dal.Update_SDBQ(dt);
        //}
    }
}
