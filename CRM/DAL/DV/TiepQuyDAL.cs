using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Entities.DV;
using System.Data.SqlClient;
using System.Data;

namespace CRM.DAL.DV
{
    class TiepQuyDAL
    {
        public static void DV_INSERT_TIEPQUY(DateTime ngayDeNghi, string maCN, string maCanBo, string atmID, int soTo50, int soTo100, int soTo200, int soTo500)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@ngaydenghi", ngayDeNghi),
                new SqlParameter("@maCN", maCN),
                new SqlParameter("@manv", maCanBo),
                new SqlParameter("@atmid", atmID),
                new SqlParameter("@soto50", soTo50),
                new SqlParameter("@soto100", soTo100),
                new SqlParameter("@soto200", soTo200),
                new SqlParameter("@soto500", soTo500)
            };
            db.dt("DV_INSERT_TIEPQUY", Params);
        }

        public static string DV_GET_DIADIEM_ATM(string atmID)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@atmid", atmID)
            };
            return db.dt("DV_GET_DIADIEM_ATM", Params).Rows[0]["DIADIEM"].ToString();
        }
    }
}
