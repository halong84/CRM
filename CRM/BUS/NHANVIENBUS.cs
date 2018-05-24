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
    class NHANVIENBUS
    {
        NHANVIENDAL dal = new NHANVIENDAL();

        public DataTable DANH_SACH_NV_THEO_PB_CV(string mapb, string chucvu)
        {
            return dal.DANH_SACH_NV_THEO_PB_CV(mapb, chucvu);
        }

        public DataTable DANH_SACH_NV_THEO_CN_CV(string macn, string chucvu)
        {
            return dal.DANH_SACH_NV_THEO_CN_CV(macn, chucvu);
        }

        public DataTable DANH_SACH_NV_THEO_CN_PB(string macn, string mapb)
        {
            return dal.DANH_SACH_NV_THEO_CN_PB(macn, mapb);
        }

        public DataTable DANH_SACH_NV_THEO_CN_PB_CV(string macn, string mapb, string chucvu)
        {
            return dal.DANH_SACH_NV_THEO_CN_PB_CV(macn, mapb, chucvu);
        }

        public DataTable NHAN_VIEN_THEO_MANV(string manv)
        {
            return dal.NHAN_VIEN_THEO_MANV(manv);
        }

        public bool UPDATE_NHANVIEN(DataTable dt)
        {
            return dal.UPDATE_NHANVIEN(dt);
        }

        public bool UPDATE_NHANVIEN_HOATDONG(DataTable dt)
        {
            return dal.UPDATE_NHANVIEN_HOATDONG(dt);
        }
    }
}
