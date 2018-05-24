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
    class SDBQCTBUS
    {
        SDBQCTDAL dal = new SDBQCTDAL();

        public bool UPDATE_SDBQCT(DataTable dt)
        {
            return dal.UPDATE_SDBQCT(dt);
        }

        public bool UPDATE_SDBQCT_DIEM(DataTable dt)
        {
            return dal.UPDATE_SDBQCT_DIEM(dt);
        }
    }
}
