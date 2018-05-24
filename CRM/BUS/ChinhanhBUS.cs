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
    class ChinhanhBUS
    {
        ChinhanhDAL dal = new ChinhanhDAL();

        public DataTable DANH_SACH_CHI_NHANH()
        {
            return dal.DANH_SACH_CHI_NHANH();
        }

        public DataTable DANH_SACH_MA_CHI_NHANH()
        {
            return dal.DANH_SACH_MA_CHI_NHANH();
        }

        public DataTable CHI_NHANH_THEO_MACN(string macn)
        {
            return dal.CHI_NHANH_THEO_MACN(macn);
        }
    }
}
