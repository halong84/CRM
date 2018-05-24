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
    class INOUTLOGBUS
    {
        INOUTLOGDAL dal = new INOUTLOGDAL();

        public bool INSERT_INOUTLOG(string user_id, string ip_address, string action)
        {
            return dal.INSERT_INOUTLOG(user_id, ip_address, action);
        }
    }
}
