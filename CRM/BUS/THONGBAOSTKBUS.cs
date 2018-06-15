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
    class THONGBAOSTKBUS
    {
        THONGBAOSTKDAL dal = new THONGBAOSTKDAL();

        public bool UPDATE_THONGBAOSTK_KH(DataTable dt)
        {
            return dal.UPDATE_THONGBAOSTK_KH(dt);
        }

        public bool UPDATE_THONGBAOSTK_CN2(DataTable dt)
        {
            return dal.UPDATE_THONGBAOSTK_CN2(dt);
        }

        public bool UPDATE_THONGBAOSTK_CN1(DataTable dt)
        {
            return dal.UPDATE_THONGBAOSTK_CN1(dt);
        }

        public DataTable STK_MAT_THEO_NGAY_KH_BAO(string ngay_kh_bao)
        {
            return dal.STK_MAT_THEO_NGAY_KH_BAO(ngay_kh_bao);
        }

        public DataTable STK_MAT_THEO_NGAY_KH_BAO_MACN(string ngay_kh_bao, string macn)
        {
            return dal.STK_MAT_THEO_NGAY_KH_BAO_MACN(ngay_kh_bao, macn);
        }

        public DataTable STK_MAT_THEO_MA_KH(string makh)
        {
            return dal.STK_MAT_THEO_MA_KH(makh);
        }

        public DataTable STK_MAT_THEO_SO_SO(string soso)
        {
            return dal.STK_MAT_THEO_SO_SO(soso);
        }
    }
}
