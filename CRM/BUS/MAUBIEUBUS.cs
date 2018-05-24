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
    class MAUBIEUBUS
    {
        MAUBIEUDAL dal = new MAUBIEUDAL();

        public DataTable DANH_SACH_MAU_BIEU(string nghiepvu, string menu_id)
        {
            return dal.DANH_SACH_MAU_BIEU(nghiepvu, menu_id);
        }
    }
}
