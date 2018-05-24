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
    class PhongBUS
    {
        PhongDAL dal = new PhongDAL();
        public DataTable DANH_SACH_PHONG_BAN(string macn)
        {
            return dal.DANH_SACH_PHONG_BAN(macn);
        }
    }
}
