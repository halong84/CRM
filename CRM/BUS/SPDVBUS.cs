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
    class SPDVBUS
    {
        SPDVDAL dal = new SPDVDAL();

        public void DELETE_SPDV(string macn)
        {
            dal.DELETE_SPDV(macn);
        }

        public bool UPDATE_SPDV_DIEN(string macn)
        {
            return dal.UPDATE_SPDV_DIEN(macn);
        }

        public bool UPDATE_SPDV_SKTIENGUI(string macn)
        {
            return dal.UPDATE_SPDV_SKTIENGUI(macn);
        }

        public bool UPDATE_SPDV_SKTIENVAY(string macn)
        {
            return dal.UPDATE_SPDV_SKTIENVAY(macn);
        }

        public bool UPDATE_SPDV_SMSLOAN(string macn)
        {
            return dal.UPDATE_SPDV_SMSLOAN(macn);
        }

        public bool UPDATE_SPDV_ABIC(string macn)
        {
            return dal.UPDATE_SPDV_ABIC(macn);
        }

        public bool UPDATE_SPDV_VNPT(string macn)
        {
            return dal.UPDATE_SPDV_VNPT(macn);
        }

        public bool UPDATE_SPDV_NUOC(string macn)
        {
            return dal.UPDATE_SPDV_NUOC(macn);
        }

        public bool UPDATE_SPDV_SMS(string macn)
        {
            return dal.UPDATE_SPDV_SMS(macn);
        }

        public bool UPDATE_SPDV_THE(string macn)
        {
            return dal.UPDATE_SPDV_THE(macn);
        }

        public bool UPDATE_SPDV_CHUYENTIEN(string macn)
        {
            return dal.UPDATE_SPDV_CHUYENTIEN(macn);
        }

        public bool UPDATE_SPDV_CHUYENTIENN(string macn)
        {
            return dal.UPDATE_SPDV_CHUYENTIENN(macn);
        }

        public bool UPDATE_SPDV_CHUYENLUONG(string macn)
        {
            return dal.UPDATE_SPDV_CHUYENLUONG(macn);
        }

        public bool UPDATE_SPDV_CMSPOS(string macn)
        {
            return dal.UPDATE_SPDV_CMSPOS(macn);
        }

        public bool UPDATE_SPDV_SKTIENGUI_TKHOCDUONG(string macn)
        {
            return dal.UPDATE_SPDV_SKTIENGUI_TKHOCDUONG(macn);
        }

        public bool UPDATE_SPDV_SKTIENGUI_ANSINH(string macn)
        {
            return dal.UPDATE_SPDV_SKTIENGUI_ANSINH(macn);
        }

        
    }
}
