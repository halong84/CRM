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
    class SDBQNTBUS
    {
        SDBQNTDAL dal = new SDBQNTDAL();

        public bool UPDATE_SDBQNT(DataTable dt)
        {
            return dal.UPDATE_SDBQNT(dt);
        }
    }
}
