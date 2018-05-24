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
    class CHUYENLUONGBUS
    {
        CHUYENLUONGDAL dal = new CHUYENLUONGDAL();

        public bool UPDATE_CHUYENLUONG(DataTable dt, string macn)
        {
            return dal.UPDATE_CHUYENLUONG(dt, macn);
        }
    }
}
