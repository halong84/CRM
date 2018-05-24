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
    class DIENBUS
    {
        DIENDAL dal = new DIENDAL();

        public bool UPDATE_DIEN(DataTable dt, string macn)
        {
            return dal.UPDATE_DIEN(dt, macn);
        }

        public bool UPDATE_DIEN_MAKH()
        {
            return dal.UPDATE_DIEN_MAKH();
        }
    }
}
