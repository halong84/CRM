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
    class LICHSUDIEMBUS
    {
        LICHSUDIEMDAL dal = new LICHSUDIEMDAL();

        public bool UPDATE_LICHSUDIEM(DataTable dt)
        {
            return dal.UPDATE_LICHSUDIEM(dt);
        }

        public bool UPDATE_LICHSUDIEM_PHEDUYET(DataTable dt)
        {
            return dal.UPDATE_LICHSUDIEM_PHEDUYET(dt);
        }
    }
}
