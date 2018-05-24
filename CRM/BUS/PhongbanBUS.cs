using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Entities.DV;
using CRM.DAL.DV;

namespace CRM.BUS.DV
{
    class PhongbanBUS
    {
        public static List<Phongban> DS_PB(string ma_cn)
        {
            return phongbanDAL.DS_PB(ma_cn);
        }

        public static Phongban PB_THEO_MA(string ma_pb)
        {
            return phongbanDAL.PB_THEO_MA(ma_pb);
        }
    }
}
