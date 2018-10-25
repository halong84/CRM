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
    public partial class frmImport_NV : Form
    {
        NHANVIENBUS nv_bus = new NHANVIENBUS();
        public frmImport_NV()
        {
            InitializeComponent();
        }

        private void btnChonFileNV_Click(object sender, EventArgs e)
        {
            if (ofdChonFileNV.ShowDialog() == DialogResult.OK)
            {
                string import_file_path = ofdChonFileNV.FileName;
                System.Data.DataTable dt_temp = CommonMethod.read_excel(import_file_path);

                if (dt_temp == null)
                {
                    MessageBox.Show("Không đọc được file. Đề nghị kiểm tra lại.");
                    return;
                }

                System.Data.DataTable dt_temp2 = new System.Data.DataTable();
                dt_temp2.Columns.AddRange
                (
                    new DataColumn[9]
                    {
                        new DataColumn("MANV", typeof(string)),
                        new DataColumn("HOTEN", typeof(string)),
                        new DataColumn("CHUCVU", typeof(string)),
                        new DataColumn("MAPB", typeof(string)),
                        new DataColumn("MACN", typeof(string)),
                        new DataColumn("UYQUYEN", typeof(string)),
                        new DataColumn("GIOITINH", typeof(bool)),
                        new DataColumn("NGAYSINH", typeof(string)),
                        new DataColumn("HOATDONG", typeof(bool)),
                    }
                );
                DataRow dr;

                if (dt_temp.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        try
                        {
                            dr = dt_temp2.NewRow();
                            dr["MANV"] = dt_temp.Rows[i][20].ToString();
                            dr["HOTEN"] = dt_temp.Rows[i][5].ToString();
                            dr["CHUCVU"] = dt_temp.Rows[i][8].ToString();
                            dr["MAPB"] = dt_temp.Rows[i][1].ToString() + "-" + dt_temp.Rows[i][3].ToString();
                            dr["MACN"] = dt_temp.Rows[i][1].ToString();
                            dr["UYQUYEN"] = "";
                            if (dt_temp.Rows[i][6].ToString() == "")
                            {
                                dr["GIOITINH"] = false;
                                if (dt_temp.Rows[i][7].ToString().Length >= 10)
                                {
                                    dr["NGAYSINH"] = dt_temp.Rows[i][7].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][7].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][7].ToString().Substring(6, 4);
                                }
                                else
                                {
                                    dr["NGAYSINH"] = "01/01/1900";
                                }
                                
                            }
                            else
                            {
                                dr["GIOITINH"] = true;
                                if (dt_temp.Rows[i][6].ToString().Length >= 10)
                                {
                                    dr["NGAYSINH"] = dt_temp.Rows[i][6].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][6].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][6].ToString().Substring(6, 4);
                                }
                                else
                                {
                                    dr["NGAYSINH"] = "01/01/1900";
                                }
                            }
                            
                            dr["HOATDONG"] = true;
                            dt_temp2.Rows.Add(dr);
                        }
                        catch
                        {}
                    }
                }
                //Cập nhật nhân viên
                if (nv_bus.UPDATE_NHANVIEN(dt_temp2))
                {
                    //Cập nhật tình trạng các nhân viên đã nghỉ việc (đưa cột HOATDONG về 0)
                    bool set_nv_nghiviec = nv_bus.UPDATE_NHANVIEN_HOATDONG(dt_temp2);

                    MessageBox.Show("Hoàn thành nhập dữ liệu nhân viên");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu nhân viên");
                }               
            }
        }
    }
}
