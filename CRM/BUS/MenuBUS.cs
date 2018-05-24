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
    class MenuBUS
    {
        MenuDAL dal = new MenuDAL();

        public DataTable DANH_SACH_MENU()
        {
            return dal.DANH_SACH_MENU();
        }

        public DataTable DANH_SACH_MAIN_MENU()
        {
            return dal.DANH_SACH_MAIN_MENU();
        }

        public DataTable DANH_SACH_SUB_MENU(string parent_id)
        {
            return dal.DANH_SACH_SUB_MENU(parent_id);
        }

        public DataTable DANH_SACH_FORM(string menu_title)
        {
            return dal.DANH_SACH_FORM(menu_title);
        }
    }
}
