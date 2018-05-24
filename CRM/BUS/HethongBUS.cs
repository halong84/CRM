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
    class HethongBUS
    {
        HethongDAL dal = new HethongDAL();

        public DataTable HE_THONG()
        {
            return dal.HE_THONG();
        }
    }
}
