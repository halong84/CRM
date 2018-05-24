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
    class DIEM_CNBUS
    {
        DIEM_CNDAL dal = new DIEM_CNDAL();

        public bool UPDATE_CONG_DIEM_CN(DataTable dt)
        {
            return dal.UPDATE_CONG_DIEM_CN(dt);
        }

        public bool UPDATE_TRU_DIEM_CN(DataTable dt)
        {
            return dal.UPDATE_TRU_DIEM_CN(dt);
        }
    }
}
