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
    class TGGTCTDALBUS
    {
        TGGTCTDAL dal = new TGGTCTDAL();

        public bool UPDATE_TGGTCT(string macn, string thang)
        {
            return dal.UPDATE_TGGTCT(macn,thang);
        }
    }
}
