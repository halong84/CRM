using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Entities.DV;
using System.Data.SqlClient;
using System.Globalization;


namespace CRM.DAL.DV
{
    class phongbanDAL
    {
        DataAccess db = new DataAccess();
        public static List<Phongban> DS_PB(string ma_cn)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@ma_cn", ma_cn)
            };
            DataTable dt = db.dt("DS_PB", Params);

            List<Phongban> list = new List<Phongban>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Phongban(row));
            }

            return list;
        }

        public static Phongban PB_THEO_MA(string ma_pb)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]
            {
            new SqlParameter("@ma_pb", ma_pb)
            };

            DataTable dt = db.dt("Pb_THEO_MA", Params);

            Phongban pb = new Phongban(dt.Rows[0]["MA_CN"].ToString(), dt.Rows[0]["MA_PB"].ToString(), dt.Rows[0]["TEN_PB"].ToString());
            return pb;
        }
    }
}
