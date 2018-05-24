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
    class PROFITCTBUS
    {
        PROFITCTDAL dal = new PROFITCTDAL();

        public bool UPDATE_PROFITCT(string macn, string thang)
        {
            return dal.UPDATE_PROFITCT(macn, thang);
        }
    }
}
