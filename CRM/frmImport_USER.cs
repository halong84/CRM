using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
//using N_MicrosoftExcelClient;
using ExcelDataReader;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmImport_USER : Form
    {
        NHANVIENBUS nvbus = new NHANVIENBUS();
        UserBUS usbus = new UserBUS();
        public frmImport_USER()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (ofdChonFileUser.ShowDialog() == DialogResult.OK)
            {
                string import_file_path = ofdChonFileUser.FileName;
                System.Data.DataTable dt_temp = CommonMethod.read_excel(import_file_path);

                if (dt_temp == null)
                {
                    MessageBox.Show("Không đọc được file. Đề nghị kiểm tra lại.");
                    return;
                }

                System.Data.DataTable dt_temp2 = new System.Data.DataTable();
                dt_temp2.Columns.AddRange
                (
                    new DataColumn[10]
                    {
                        new DataColumn("USER_ID", typeof(string)),
                        new DataColumn("USER_PASS", typeof(string)),
                        new DataColumn("GROUP_LIST", typeof(string)),
                        new DataColumn("MANV", typeof(string)),
                        new DataColumn("TENNV", typeof(string)),
                        new DataColumn("CHUCVU", typeof(string)),
                        new DataColumn("MACN", typeof(string)),
                        new DataColumn("GHICHU", typeof(string)),
                        new DataColumn("MAPB", typeof(string)),
                        new DataColumn("KICHHOAT", typeof(bool))
                    }
                );
                DataRow dr;

                if (dt_temp.Rows.Count > 0)
                {
                    DataTable nhanvien = new DataTable();
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        if (dt_temp.Rows[i][4].ToString() != "2")
                        {
                            try
                            {
                                nhanvien.Clear();

                                dr = dt_temp2.NewRow();
                                dr["USER_ID"] = dt_temp.Rows[i][1].ToString();
                                dr["USER_PASS"] = "123456";

                                if (dt_temp.Rows[i][36].ToString() == Thongtindangnhap.ma_hoi_so)
                                {
                                    dr["GROUP_LIST"] = "G_HS";
                                }
                                else
                                {
                                    dr["GROUP_LIST"] = "G_CN";
                                }
                                dr["MANV"] = dt_temp.Rows[i][35].ToString();

                                nhanvien = nvbus.NHAN_VIEN_THEO_MANV(dt_temp.Rows[i][35].ToString());
                                dr["TENNV"] = nhanvien.Rows[0]["HOTEN"].ToString();
                                dr["CHUCVU"] = nhanvien.Rows[0]["CHUCVU"].ToString();

                                dr["MACN"] = dt_temp.Rows[i][36].ToString();
                                dr["GHICHU"] = "";
                                dr["MAPB"] = nhanvien.Rows[0]["MAPB"].ToString(); ;
                                dr["KICHHOAT"] = true;
                                dt_temp2.Rows.Add(dr);
                            }
                            catch
                            { }
                       }                        
                    }
                }

                //Cập nhật thông tin người sử dụng
                if (usbus.UPDATE__USER(dt_temp2))
                {
                    //Khóa những user không hoạt động (đưa cột KICHHOAT về 0)
                    bool update_user = usbus.UPDATE__USER_HOATDONG(dt_temp2);
                    MessageBox.Show("Hoàn thành nhập thông tin người sử dụng");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập thông tin người sử dụng");
                }

                
            }
        }
    }
}
