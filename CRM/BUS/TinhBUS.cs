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
    class TinhBUS
    {
        TinhDAL dal = new TinhDAL();

        public DataTable DANH_SACH_TINH()
        {
            return dal.DANH_SACH_TINH();
        }
    }
}
