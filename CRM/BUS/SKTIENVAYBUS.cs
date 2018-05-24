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
    class SKTIENVAYBUS
    {
        SKTIENVAYDAL dal = new SKTIENVAYDAL();

        public bool UPDATE_SKTIENVAY(DataTable dt, string macn)
        {
            return dal.UPDATE_SKTIENVAY(dt, macn);
        }
    }
}
