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
    class HuyenBUS
    {
        HuyenDAL dal = new HuyenDAL();

        public DataTable DANH_SACH_HUYEN_ALL()
        {
            return dal.DANH_SACH_HUYEN_ALL();
        }

        public DataTable DANH_SACH_HUYEN(string matinh)
        {
            return dal.DANH_SACH_HUYEN(matinh);
        }
    }
}
