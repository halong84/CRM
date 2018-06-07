using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CRM.Entities.DV;
using System.Data;

namespace CRM.DAL.DV
{
    class ThongTinTheDAL
    {
        public static DataRow LayThongTinThe(int ID)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@ID", ID)
            };

            DataRow row = db.dt("DV_LAYTHONGTINTHE", Params).Rows[0];
            return row;
        }

        public static void NhanThe(The the)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@ID", the.ID),
            new SqlParameter("@sothe", the.soThe),
            new SqlParameter("@ngaynhan", the.ngayNhan)
            };

            db.dt("DV_NHANTHE", Params);
        }

        public static void GiaoThe(The the)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@ID", the.ID),
            new SqlParameter("@ngaygiao", the.ngayGiao)
            };

            db.dt("DV_GIAOTHE", Params);
        }

        public static DataRow LayPhongBan(string maPB) {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@mapb", maPB)
            };
            return db.dt("DV_LAYPHONGBAN", Params).Rows[0];
        }

        public static string LayTenChiNhanh(string maPb)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@mapb", maPb)
            };
            return db.dt("DV_LAYTENCN_THONGTINTHE", Params).Rows[0]["TENCN"].ToString();
        }

        public static void DV_THONGTINTHE_UPDATE(int id, string sothe, DateTime ngaynhan, DateTime ngaygiao)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@id", id),
            new SqlParameter("@sothe", sothe),
            new SqlParameter("@ngaynhan", ngaynhan),
            new SqlParameter("@ngaygiao", ngaygiao),
            };
            db.dt("DV_THONGTINTHE_UPDATE", Params);
        }

        public static void DV_NHANTHE(int id, string sothe, DateTime ngaynhan)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@id", id),
            new SqlParameter("@sothe", sothe),
            new SqlParameter("@ngaynhan", ngaynhan)
            };
            db.dt("DV_NHANTHE", Params);
        }


        public static void DV_GIAOTHE(int id, DateTime ngaygiao)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
            new SqlParameter("@ID", id),
            new SqlParameter("@ngaygiao", ngaygiao)
            };

            db.dt("DV_GIAOTHE", Params);
        }
    }
}
