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
    public partial class frmNhapKH_TuFile : Form
    {
        KHACHHANGBUS khachhang_bus = new KHACHHANGBUS();
        public frmNhapKH_TuFile()
        {
            InitializeComponent();
        }

        private void btnChonFileKH_Click(object sender, EventArgs e)
        {
            if (ofdNhapfileKH.ShowDialog() == DialogResult.OK)
            {
                string import_file_path = ofdNhapfileKH.FileName;
                System.Data.DataTable dt_temp = CommonMethod.read_excel(import_file_path);
                if (dt_temp.Rows.Count == 0 || dt_temp == null)
                {
                    MessageBox.Show("File không có dữ liệu");
                    return;
                }
                if (dt_temp.Rows[0][7].ToString() == "Cá nhân")
                {
                    lay_KHCN(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Công ty cổ phần")
                {
                    lay_KHCTCP(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Công ty TNHH")
                {
                    lay_KHCTTNHH(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Doanh nghiệp có vốn ĐT nước ngoài")
                {
                    lay_KHDNDTNN(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Công ty Nhà nước")
                {
                    lay_KHDNNN(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Doanh nghiệp tư nhân")
                {
                    lay_KHDNTN(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Hộ gia đình")
                {
                    lay_KHHGD(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Hợp tác xã")
                {
                    lay_KHHTX(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Tổ chức")
                {
                    lay_KHTC(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Tổ chức Tài chính")
                {
                    lay_KHTCTC(dt_temp);
                }
                else if (dt_temp.Rows[0][7].ToString() == "Tổ chức XH TƯ & Địa phương")
                {
                    lay_KHTCXH(dt_temp);
                }
                else
                {
                    MessageBox.Show("File dữ liệu không đúng. Đề nghị kiểm tra lại.");
                    return;
                }
            }
        }

        //Các hàm lấy thông tin khách hàng từ file

        //Nhập thông tin khách hàng cá nhân
        private void lay_KHCN(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            //Định dạng ngày tháng theo dạng en-US cho hàm convert.todatetie
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {

                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            //định dạng mm/dd/yyy
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            //định dạng mm/dd/yyy
                            ngaysinh = "01/01/1900";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 1;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");

                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        if (dt_temp.Rows[i][14].ToString() != "")
                        {
                            //Khách hàng sử dụng chứng minh nhân dân
                            dr["CMND"] = dt_temp.Rows[i][14].ToString();
                            ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                            if (ngaycap != "")
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                            }
                            else
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = "01/01/1900";
                            }
                            dr["NGAYCAP"] = ngaycap;
                            dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        }
                        else if (dt_temp.Rows[i][15].ToString() != "")
                        {
                            //Khách hàng sử dụng hộ chiếu
                            dr["CMND"] = dt_temp.Rows[i][15].ToString();
                            ngaycap = dt_temp.Rows[i][36].ToString().Trim();
                            if (ngaycap != "")
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                            }
                            else
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = "01/01/1900";
                            }
                            dr["NGAYCAP"] = ngaycap;
                            dr["NOICAP"] = dt_temp.Rows[i][35].ToString();
                        }
                        else
                        {
                            dr["CMND"] = "";
                            dr["NGAYCAP"] = "01/01/1900";
                            dr["NOICAP"] = "";
                        }
                        //dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        //dr["NGAYCAP"] = ngaycap;
                        //dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Cá nhân"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "14";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Doanh nghiệp tư nhân
        private void lay_KHDNTN(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Doanh nghiệp tư nhân"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "23";
                        dr["DOITUONGDN"] = "2";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Hộ gia đình
        private void lay_KHHGD(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {

                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Hộ gia đình"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "13";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Hợp tác xã
        private void lay_KHHTX(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Hợp tác xã"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "24";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Công ty cổ phần
        private void lay_KHCTCP(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty cổ phần"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "21";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Công ty TNHH
        private void lay_KHCTTNHH(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty TNHH"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "21";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng công ty liên doanh
        private void lay_KHCTLD(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty liên doanh"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "21";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Doanh nghiệp có vốn đầu tư nước ngoài
        private void lay_KHDNDTNN(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Doanh nghiệp có vốn ĐT nước ngoài"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "14";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Doanh nghiệp nhà nước
        private void lay_KHDNNN(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {

                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Doanh nghiệp Nhà nước"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "41";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng tổ chức tài chính
        private void lay_KHTCTC(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {

                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức Tài chính"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "41";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng Tổ chức xã hội
        private void lay_KHTCXH(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {

                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức XH TƯ & Địa phương"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "34";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        //Nhập thông tin khách hàng tổ chức
        private void lay_KHTC(System.Data.DataTable dt_temp)
        {
            System.Data.DataTable dt_temp2 = new System.Data.DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(int)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            ngaysinh = "01/01/1990";
                        }
                        ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                        if (ngaycap != "")
                        {
                            ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                        }
                        else
                        {
                            ngaycap = "01/01/1990";
                        }
                        String gt = dt_temp.Rows[i][10].ToString();
                        Int16 gioitinh;
                        if (gt == "Nam" || gt == "Male" || gt == "nam")
                        {
                            gioitinh = 1;
                        }
                        else
                        {
                            gioitinh = 0;
                        }
                        Int16 loaikh = 2;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "35";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }
    }
}
