using CRM.Utilities.DV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CRM.DAL.DV
{
    class KiemQuyDAL
    {
        public static DataTable DV_DSNhanVien_MaCN(string macn)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@macn", macn)
            };
            return db.dt("DV_DSNHANVIEN_MACN", Params);
        }

        public static DataTable DV_ATM_MACN()
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
                new SqlParameter("@macn", Thong_tin_dang_nhap.ma_cn)
            };
            return db.dt("DV_ATM_MACN", Params);
        }

        public static DataTable DV_SEARCH_KIEMQUY(DateTime tuNgay, DateTime denNgay, string id)
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[] { 
                new SqlParameter("@tungay", tuNgay),
                new SqlParameter("@denngay", denNgay),
                new SqlParameter("@id", id)
            };

            return db.dt("DV_SEARCH_KIEMQUY", Params);
        }

        public static void DV_INSERT_KIEMQUY(
            DateTime tuNgay,
            DateTime denNgay,
            string id,
            string tpKiemQuy,
            long qtTienTHT,
            long qtMonCHT,
            long qtTienCHT,
            DateTime timeTCO,
            DateTime timeCO,
            DateTime timeCI,
            long soDuTCO,
            long soDuCO,
            long soDuCI,
            string ghiChuTCO,
            string ghiChuCO,
            string ghiChuCI,
            DateTime timeSC,
            DateTime timeCE,
            long fimiSC50,
            long fimiSC100,
            long fimiSC200,
            long fimiSC500,
            long fimiCE50,
            long fimiCE100,
            long fimiCE200,
            long fimiCE500,
            long demB50,
            long demB100,
            long demB200,
            long demB500,
            long demCC50,
            long demCC100,
            long demCC200,
            long demCC500,
            long demCL50,
            long demCL100,
            long demCL200,
            long demCL500,
            long thuaThieu,
            string nguyenNhan,
            string khacPhuc
        )
        {
            DataAccess db = new DataAccess();
            SqlParameter[] Params = new SqlParameter[]{
            new SqlParameter("@tungay", tuNgay),
            new SqlParameter("@denngay", denNgay),
            new SqlParameter("@atmid", id),
            new SqlParameter("@canbo", tpKiemQuy),
            new SqlParameter("@qttientht", qtTienTHT),
            new SqlParameter("@qtmoncht", qtMonCHT),
            new SqlParameter("@qttiencht", qtTienCHT),
            new SqlParameter("@time_tco", timeTCO),
            new SqlParameter("@time_co", timeCO),
            new SqlParameter("@time_ci", timeCI),
            new SqlParameter("@sodu_tco", soDuTCO),
            new SqlParameter("@sodu_co", soDuCO),
            new SqlParameter("@sodu_ci", soDuCI),
            new SqlParameter("@ghichu_tco", ghiChuTCO),
            new SqlParameter("@ghichu_co", ghiChuCO),
            new SqlParameter("@ghichu_ci", ghiChuCI),
            new SqlParameter("@fimitimesc", timeSC),
            new SqlParameter("@fimitimece", timeCE),
            new SqlParameter("@fimisc50", fimiSC50),
            new SqlParameter("@fimisc100", fimiSC100),
            new SqlParameter("@fimisc200", fimiSC200),
            new SqlParameter("@fimisc500", fimiSC500),
            new SqlParameter("@fimice50", fimiCE50),
            new SqlParameter("@fimice100", fimiCE100),
            new SqlParameter("@fimice200", fimiCE200),
            new SqlParameter("@fimice500", fimiCE500),
            new SqlParameter("@demb50", demB50),
            new SqlParameter("@demb100", demB100),
            new SqlParameter("@demb200", demB200),
            new SqlParameter("@demb500", demB500),
            new SqlParameter("@demcc50", demCC50),
            new SqlParameter("@demcc100", demCC100),
            new SqlParameter("@demcc200", demCC200),
            new SqlParameter("@demcc500", demCC500),
            new SqlParameter("@demcl50", demCL50),
            new SqlParameter("@demcl100", demCL100),
            new SqlParameter("@demcl200", demCL200),
            new SqlParameter("@demcl500", demCL500),
            new SqlParameter("@thuathieu", thuaThieu),
            new SqlParameter("@nguyennhan", nguyenNhan),
            new SqlParameter("@khacphuc", khacPhuc)
            };
            db.dt("DV_INSERT_KIEMQUY", Params);
        }
    }
}
