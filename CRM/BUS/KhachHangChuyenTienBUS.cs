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
    class KhachHangChuyenTienBUS
    {
        KhachHangChuyenTienDAL dal = new KhachHangChuyenTienDAL();

        public bool UPDATE_KhachHangChuyenTien(DataTable dt)
        {
            return dal.UPDATE_KhachHangChuyenTien(dt);
        }

        public bool UPDATE_KHACHHANGCHUYENTIEN_WU(string macn, string thang)
        {
            return dal.UPDATE_KHACHHANGCHUYENTIEN_WU(macn, thang);
        }
    }
}
