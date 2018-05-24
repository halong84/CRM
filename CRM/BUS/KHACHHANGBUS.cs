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
    class KHACHHANGBUS
    {
        KHACHHANGDAL dal = new KHACHHANGDAL();

        public bool UPDATE_KHACHHANG(DataTable dt, string nguoicapnhat)
        {
            return dal.UPDATE_KHACHHANG(dt, nguoicapnhat);
        }

        public DataTable KH_THEO_MAKH(string makh)
        {
            return dal.KH_THEO_MAKH(makh);
        }
    }
}
