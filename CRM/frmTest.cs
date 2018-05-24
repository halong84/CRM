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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using N_MicrosoftExcelClient;
using ExcelDataReader;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmTest : Form
    {
        string filename = "";
        KHACHHANGBUS khachhang_bus = new KHACHHANGBUS();
        KhachHangChuyenTienBUS khct_bus = new KhachHangChuyenTienBUS();
        WUBUS wu_bus = new WUBUS();
        MenuBUS menubus = new MenuBUS();
        string[] nhom_nguoi_dung = Thongtindangnhap.group_list.Split(',');
        public frmTest()
        {
            InitializeComponent();
        }
        public DataTable read_excel(string excel_path)
        {
            DataTable dt = new DataTable();
            var file = new FileInfo(excel_path);
            using (
                var stream = File.Open(excel_path, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;

                if (file.Extension.Equals(".xls"))
                    reader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
                else if (file.Extension.Equals(".xlsx"))
                    reader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
                else
                    throw new Exception("Invalid FileName");

                //// reader.IsFirstRowAsColumnNames
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = reader.AsDataSet(conf);
                dt = dataSet.Tables[0];
            }
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Check square unicode is 0x2611 (check), 0x2610 (uncheck)
            MessageBox.Show(((char)0x2611).ToString() + " va " + ((char)0x2610).ToString());
        }


        private void lay_WU()
        {
            DataTable dt_temp_wu = new DataTable();
            dt_temp_wu.Columns.AddRange
            (
                new DataColumn[15] 
                { 
                    new DataColumn("ID", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("SOTIEN", typeof(decimal)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("NGAYNHAN", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("DIENTHOAI", typeof(string)),
                    new DataColumn("MTCN", typeof(string)),
                    new DataColumn("HOTEN_GUI", typeof(string)),
                    new DataColumn("SOTIEN_GUI", typeof(decimal)),
                    new DataColumn("CCY_GUI", typeof(string))
                }
            );
            DataRow dr;
            String qyery_temp;
            DataTable dt_temp = new DataTable();
            String filename = "";
            filename = frmMain.ddimport + "\\WU1216.xls";
            //Kiem tra du lieu da duoc import chua
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','WU_THANG','12/2016')";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            //Xoa du lieu dang co trong table WU
            //try
            //{
            //    qyery_temp = "Delete WU where MaCN='" + Thongtindangnhap.macn + "' and Thang='" + dtpThang.Text + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}
            //Import du lieu WU VND

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        String ID = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                        string hoten = dt_temp.Rows[i][14].ToString() + " " + dt_temp.Rows[i][13].ToString();
                        string diachi = dt_temp.Rows[i][15].ToString() + " " + dt_temp.Rows[i][17].ToString();
                        string ngaynhan = dt_temp.Rows[i][40].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][40].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][40].ToString().Substring(6, 4);
                        string hoten_gui = dt_temp.Rows[i][8].ToString() + " " + dt_temp.Rows[i][9].ToString();

                        string cmt = dt_temp.Rows[i][24].ToString();
                        //DataTable dt1 = new DataTable();
                        //String sCommand = "SELECT makh from SKTIENGUI where CMND='" + cmt + "'";
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        //DataAccess.conn.Close();

                        String makh = Thongtindangnhap.macn + "000000000";
                        //int j = dt1.Rows.Count;
                        //if (j != 0)
                        //{
                        //    makh = dt1.Rows[j - 1]["makh"].ToString();
                        //}

                        dr = dt_temp_wu.NewRow();
                        dr["ID"] = ID;
                        if (ID.Length > 50)
                        {
                            MessageBox.Show("ID="+ID);
                        }
                        dr["MAKH"] = makh;
                        if (makh.Length > 50)
                        {
                            MessageBox.Show("MAKH = "+makh);
                        }
                        dr["HOTEN"] = hoten;
                        if (hoten.Length > 200)
                        {
                            MessageBox.Show("HOTEN = "+hoten);
                        }
                        dr["DIACHI"] = diachi;
                        dr["CMND"] = cmt;
                        if (cmt.Length > 20)
                        {
                            MessageBox.Show("CMT =" +cmt);
                        }
                        dr["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][39].ToString());
                        
                        dr["CCY"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYNHAN"] = ngaynhan;
                        if (ngaynhan.Length > 10)
                        {
                            MessageBox.Show("NGAYNHAN = " +ngaynhan);
                        }
                        dr["MACN"] = Thongtindangnhap.macn;
                        dr["THANG"] = "12/2016";
                        dr["DIENTHOAI"] = dt_temp.Rows[i][20].ToString();
                        if (dt_temp.Rows[i][20].ToString().Length > 15)
                        {
                            MessageBox.Show("DIENTHOAI = " + dt_temp.Rows[i][20].ToString() + "Length" + dt_temp.Rows[i][20].ToString().Length);
                        }
                        dr["MTCN"] = dt_temp.Rows[i][6].ToString();
                        if (dt_temp.Rows[i][6].ToString().Length > 20)
                        {
                            MessageBox.Show(dt_temp.Rows[i][6].ToString());
                        }
                        dr["HOTEN_GUI"] = hoten_gui;
                        if (hoten_gui.Length > 200)
                        {
                            MessageBox.Show("HOTEN_GUI = "+hoten_gui);
                        }
                        dr["SOTIEN_GUI"] = Convert.ToDecimal(dt_temp.Rows[i][28].ToString());
                        dr["CCY_GUI"] = dt_temp.Rows[i][32].ToString();
                        dt_temp_wu.Rows.Add(dr);

                       
                    }
                    Thread.Sleep(1);
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }

            //int maxStringLength = dt_temp_wu.AsEnumerable()
            //                  .SelectMany(row => row.ItemArray.OfType<string>())
            //                  .Max(str => str.Length);
            //MessageBox.Show(maxStringLength.ToString());
            //if (wu_bus.UPDATE_WU(dt_temp_wu, Thongtindangnhap.macn, "12/2016"))
            //{
            //    bool update_wu_makh = wu_bus.UPDATE_WU_MAKH("12/2016");
            //    MessageBox.Show("Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng 12/2016 vào bảng WU thành công");             
            //}
            //else
            //{
            //    MessageBox.Show("Có lỗi xảy ra khi Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng 12/2016 vào bảng WU.");
            //    return;
            //}
            ////Cập nhật khách hàng không có mã khách hàng vào bảng khách hàng chuyển tiền
            //if (khct_bus.UPDATE_KHACHHANGCHUYENTIEN_WU(Thongtindangnhap.macn, "12/2016"))
            //{
            //    MessageBox.Show("Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng 12/2016 vào bảng KHACHHANGCHUYENTIEN thành công");
            //}
            //else
            //{
            //    MessageBox.Show("Có lỗi xảy ra khi Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng 12/2016 vào bảng KHACHHANGCHUYENTIEN.");
            //}
            
            
        }
        public void lay_menu()
        {
            
            DataTable mainmenu_dt = menubus.DANH_SACH_MAIN_MENU();

            foreach (DataRow mainmenu_dr in mainmenu_dt.Rows)
            {
                bool bool_main = false;
                foreach (string nhom in nhom_nguoi_dung)
                {
                    if (mainmenu_dr["GROUP_LIST"].ToString().Contains(nhom))
                    {
                        bool_main = true;
                        break;
                    }
                }
                if (bool_main)
                {
                    ToolStripMenuItem MainMenuStripItem = new ToolStripMenuItem(mainmenu_dr["MENU_TITLE"].ToString());
                    SubMenu(MainMenuStripItem, mainmenu_dr["MENU_ID"].ToString());
                    MainMenuStrip.Items.Add(MainMenuStripItem);
                }
            }
            // The Form.MainMenuStrip property determines the merge target.
            this.MainMenuStrip = MainMenuStrip;
        }

        public void SubMenu(ToolStripMenuItem mnu, string parent_id)
        {
            DataTable submenu_dt = menubus.DANH_SACH_SUB_MENU(parent_id);
            foreach (DataRow submenu_dr in submenu_dt.Rows)
            {
                bool bool_sub = false;
                foreach (string nhom in nhom_nguoi_dung)
                {
                    if (submenu_dr["GROUP_LIST"].ToString().Contains(nhom))
                    {
                        bool_sub = true;
                        break;
                    }
                }
                if (bool_sub)
                {
                    ToolStripMenuItem SSMenu = new ToolStripMenuItem(submenu_dr["MENU_TITLE"].ToString(), null, new EventHandler(ChildClick));
                    mnu.DropDownItems.Add(SSMenu);
                }
            }
        }

        private void ChildClick(object sender, EventArgs e)
        {
            //if (sender.ToString() == "Thoát")
            //{
            //    if (inoutlog_bus.INSERT_INOUTLOG(Thongtindangnhap.user_id, Thongtindangnhap.ip_address, "Đăng xuất"))
            //    {
            //        //
            //    }
            //    Application.Exit();
            //}
            //MessageBox.Show(string.Concat("You have Clicked ", sender.ToString(), " Menu"), "Menu Items Event",MessageBoxButtons.OK, MessageBoxIcon.Information);

            //String Seqtx = "SELECT FRM_CODE FROM MNU_SUBMENU WHERE FRM_NAME='" + sender.ToString() + "'";
            //SqlDataAdapter datransaction = new SqlDataAdapter(Seqtx, conn);
            //DataTable dtransaction = new DataTable();
            //datransaction.Fill(dtransaction);
            DataTable dtransaction = menubus.DANH_SACH_FORM(sender.ToString());
            Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);
            foreach (Type type in frmAssembly.GetTypes())
            {
                //MessageBox.Show(type.Name);
                if (type.BaseType == typeof(Form))
                {
                    MessageBox.Show(type.Name + "=" + dtransaction.Rows[0][0].ToString());
                    if (type.Name == dtransaction.Rows[0][0].ToString())
                    {
                        Form frmShow = (Form)frmAssembly.CreateInstance(type.ToString());
                        // then when you want to close all of them simple call the below code

                        foreach (Form form in this.MdiChildren)
                        {
                            form.Close();
                        }

                        //frmShow.MdiParent = this;
                        //frmShow.WindowState = FormWindowState.Maximized;
                        //frmShow.ControlBox = false;
                        //frmShow.Show();
                    }
                }
            }
        }
        private void lay_KHCN()
        {
            DataTable dt_temp2 = new DataTable();
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
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCN1117.xls";

            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //}
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
                        Int16 loaikh;
                        if (dt_temp.Rows[i][7].ToString() == "Cá nhân")
                        {
                            loaikh = 1;
                        }
                        else
                        {
                            loaikh = 2;
                        }

                        String ngaytao = "11/01/2017";

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
                        dr["LOAIKH_IPCAS"] = "Cá nhân";
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
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3,4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Cá nhân','" + strngaytao + "','14')";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "',loaikh=" + loaikh + " where makh='" + dt_temp.Rows[i][0].ToString() + "'";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                    }
                }
                catch
                { }
            }
            //MessageBox.Show("Ket thuc");
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                MessageBox.Show("Nhập thông tin khách hàng cá nhân tháng 11/2017 thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng cá nhân tháng 11/2017");
            }
        }
    }
}