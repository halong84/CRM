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
    class TAIKHOANBUS
    {
        TAIKHOANDAL dal = new TAIKHOANDAL();
        public bool UPDATE_TAIKHOAN()
        {
            return dal.UPDATE_TAIKHOAN();
        }

        public bool UPDATE_TAIKHOAN_TUFILE(DataTable tbltaikhoan)
        {
            return dal.UPDATE_TAIKHOAN_TUFILE(tbltaikhoan);
        }

        public bool MO_TAIKHOAN(string makh, string sotk, string ccy, string ngaymo)
        {
            return dal.MO_TAIKHOAN(makh, sotk, ccy, ngaymo);
        }

        public DataTable TAI_KHOAN_THEO_MAKH(string makh)
        {
            return dal.TAI_KHOAN_THEO_MAKH(makh);
        }

        public DataTable TAI_KHOAN_THEO_STK(string sotk)
        {
            return dal.TAI_KHOAN_THEO_STK(sotk);
        }

        public bool DONG_TAIKHOAN(string sotk, string ngaydong)
        {
            return dal.DONG_TAIKHOAN(sotk, ngaydong);
        }
    }
}
