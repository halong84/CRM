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
    class UserBUS
    {
        UserDAL dal = new UserDAL();

        public DataTable XAC_THUC_USER(string user_id, string user_pass)
        {
           return dal.XAC_THUC_USER(user_id,user_pass);
        }

        public DataTable KIEM_TRA_USER(string user_id)
        {
            return dal.KIEM_TRA_USER(user_id);
        }

        public bool DOI_MAT_KHAU(string user_id, string user_pass)
        {
            return dal.DOI_MAT_KHAU(user_id, user_pass);
        }

        public bool UPDATE__USER(DataTable dt)
        {
            return dal.UPDATE__USER(dt);
        }
    }
}
