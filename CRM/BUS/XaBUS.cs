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
    class XaBUS
    {
        XaDAL dal = new XaDAL();

        public DataTable DANH_SACH_XA_ALL()
        {
            return dal.DANH_SACH_XA_ALL();
        }

        public DataTable DANH_SACH_XA(string mahuyen)
        {
            return dal.DANH_SACH_XA(mahuyen);
        }

        public DataTable DANH_SACH_XA_THEO_TINH(string matinh)
        {
            return dal.DANH_SACH_XA_THEO_TINH(matinh);
        }
    }
}
