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
using N_MicrosoftExcelClient;

namespace CRM
{
    public partial class frmKhachhangTN : Form
    {
        String strCmd = "";
        private DataTable dtResult = new DataTable();
        public static string makh = "";
        public static string flag = "";
        
        public frmKhachhangTN()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbGioitinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbGioitinh.SelectedIndex = 0;
            //cbbTinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbTinhtrang.SelectedIndex = 0;
            cbbSTinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbSTinhtrang.SelectedIndex = 0;
            cbbSNhomKH.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbSNhomKH.SelectedIndex = 0;
            //cbbDN_Tinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbDN_Tinhtrang.SelectedIndex = 0;
            cbbLH_Gioitinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbLH_Gioitinh.SelectedIndex = 0;
            cbbDN_STinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbDN_STinhtrang.SelectedIndex = 0;
            cbbDN_SNhomKH.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbLoaiKHIpcas.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_LoaiKHIpcas.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_SNhomKH.SelectedIndex = 0;

            //cbbTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbHuyen.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbXa.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_Tinh.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_Huyen.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_Xa.DropDownStyle = ComboBoxStyle.DropDownList;

            //txtMaNV.Enabled = false;
            //txtDN_MaNV.Enabled = false;

            dgvDanhsachCN.RowHeadersVisible = false;
            dgvDanhsachCN.AllowUserToAddRows = false;
            dgvDanhsachCN.ReadOnly = true;
            dgvDanhsachCN.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsachCN.MultiSelect = false;

            dgvDanhsachDN.RowHeadersVisible = false;
            dgvDanhsachDN.AllowUserToAddRows = false;
            dgvDanhsachDN.ReadOnly = true;
            dgvDanhsachDN.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsachDN.MultiSelect = false;

            dgvLienhe.RowHeadersVisible = false;
            dgvLienhe.AllowUserToAddRows = false;
            dgvLienhe.ReadOnly = true;
            dgvLienhe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLienhe.MultiSelect = false;
        }

        private void frmKhachhangTN_Load(object sender, EventArgs e)
        {
            dtpNgaysinh.Text = "01/01/1990";
            dtpNgaycap.Text = "01/01/1990";
            dtpLH_Ngaysinh.Text = "01/01/1990";
            dtpLH_Ngaycap.Text = "01/01/1990";
            dtpNgayKH.Text = "01/01/1990";
            dtpDN_NgayTL.Text = "01/01/1990";
            txtMaNV.Text = CRM.frmDangnhap.UserID;
            txtDN_MaNV.Text = CRM.frmDangnhap.UserID;

            layDS_Tinh();
            layDS_Huyen();
            layDS_Xa();
            layDSDN_Tinh();
            layDSDN_Huyen();
            layDSDN_Xa();
            layDS_Linhvuc();
            layDSDN_Linhvuc();
            layDS_NhomKH();
            layDSDN_NhomKH();
            layLoaiKH();
            layDN_LoaiKH();
            layKH2890();
            layKH2890DN();
            layLoaihinhDN2890();
        }

        private void layDS_KhachhangCN()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on khachhang.doituongkh=doituongkh.ma ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + frmMain.cn + "' ";
            strCmd += " Order by kh.MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["Tenxa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;
                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }
                                        
                    //string loaiKH = "";
                    //loaiKH = "Cá nhân";
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["loaihinhtiepcan"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_KhachhangDN()
        {
            //dgvDanhsachDN.Visible = true;
            //dgvDanhsach.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA ";
            //strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + frmMain.cn + "' ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + frmMain.cn + "' ";
            strCmd += " Order by kh.MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();
                    row[5] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[6] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[7] = dtResult.Rows[i]["TenXa"].ToString();
                    row[8] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[9] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[10] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[11] = dtResult.Rows[i]["Email"].ToString();
                    row[12] = dtResult.Rows[i]["Website"].ToString();
                    row[13] = dtResult.Rows[i]["NHGiaodich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[14] = tinhtrang;

                    //string loaiKH = "";
                    //loaiKH = "Doanh nghiệp";
                    //row[15] = loaiKH;
                    row[15] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[16] = dtResult.Rows[i]["MaNV"].ToString();
                    row[17] = dtResult.Rows[i]["GPDK"].ToString();
                    row[18] = dtResult.Rows[i]["QDTL"].ToString();
                    row[19] = dtResult.Rows[i]["MST"].ToString();

                    string ngayThanhlap, ngayTL, thangTL, namTL;
                    ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

                    ngayTL = ngayThanhlap.Substring(0, 2);
                    thangTL = ngayThanhlap.Substring(3, 2);
                    namTL = ngayThanhlap.Substring(6, 4);

                    row[20] = ngayTL + "/" + thangTL + "/" + namTL;
                    row[21] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[22] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[23] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachDN.DataSource = dtDanhsach;
            dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachDN.Columns[0].Width = 60;
            dgvDanhsachDN.Columns[1].Width = 140;
            dgvDanhsachDN.Columns[2].Width = 200;
            dgvDanhsachDN.Columns[3].Width = 120;
            dgvDanhsachDN.Columns[4].Width = 120;
            dgvDanhsachDN.Columns[5].Width = 150;
            dgvDanhsachDN.Columns[6].Width = 200;
            dgvDanhsachDN.Columns[7].Width = 120;
            dgvDanhsachDN.Columns[8].Width = 120;
            dgvDanhsachDN.Columns[9].Width = 120;
            dgvDanhsachDN.Columns[10].Width = 200;
            dgvDanhsachDN.Columns[11].Width = 200;
            dgvDanhsachDN.Columns[12].Width = 200;
            dgvDanhsachDN.Columns[13].Width = 150;
            dgvDanhsachDN.Columns[14].Width = 150;
            dgvDanhsachDN.Columns[15].Width = 120;
            dgvDanhsachDN.Columns[16].Width = 120;
            dgvDanhsachDN.Columns[17].Width = 150;
            dgvDanhsachDN.Columns[18].Width = 150;
            dgvDanhsachDN.Columns[19].Width = 150;
            dgvDanhsachDN.Columns[20].Width = 150;
            dgvDanhsachDN.Columns[21].Width = 150;
            dgvDanhsachDN.Columns[22].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_TenKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + frmMain.cn + "' and kh.Hoten like N'%" + txtSTen.Text.Trim() + "%' ";
            strCmd += " Order by kh.Hoten, kh.MaKH ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and Hoten like N'%" + txtSTen.Text.Trim() + "%' and macn='" + frmMain.cn + "' ";
            //strCmd += " Order by Hoten, MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["GPDK"].ToString();
                    row[26] = dtResult.Rows[i]["QDTL"].ToString();
                    row[27] = dtResult.Rows[i]["MST"].ToString();
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 150;            
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Visible = false;
            dgvDanhsachCN.Columns[28].Width = 150;
            dgvDanhsachCN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_MaKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + frmMain.cn + "' and kh.MaKH like '%" + txtSMaKH.Text.Trim() + "%' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and MaKH like '%" + txtSMaKH.Text.Trim() + "%' and macn='" + frmMain.cn + "' ";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["GPDK"].ToString();
                    row[26] = dtResult.Rows[i]["QDTL"].ToString();
                    row[27] = dtResult.Rows[i]["MST"].ToString();
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 150;            
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Visible = false;
            dgvDanhsachCN.Columns[28].Width = 150;
            dgvDanhsachCN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Dienthoai()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + frmMain.cn + "' and (kh.Dienthoai1 like '%" + txtSTel.Text.Trim() + "%' or kh.Dienthoai2 like '%" + txtSTel.Text.Trim() + "%') ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and (Dienthoai1 like '%" + txtSTel.Text.Trim() + "%' or Dienthoai2 like '%" + txtSTel.Text.Trim() + "%') and macn='" + frmMain.cn + "'";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["GPDK"].ToString();
                    row[26] = dtResult.Rows[i]["QDTL"].ToString();
                    row[27] = dtResult.Rows[i]["MST"].ToString();
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 150;            
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Visible = false;
            dgvDanhsachCN.Columns[28].Width = 150;
            dgvDanhsachCN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_CMND()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + frmMain.cn + "' and kh.CMND like '%" + txtSCMND.Text.Trim() + "%' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and CMND like '%" + txtSCMND.Text.Trim() + "%' and macn='" + frmMain.cn + "' ";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;

                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["GPDK"].ToString();
                    row[26] = dtResult.Rows[i]["QDTL"].ToString();
                    row[27] = dtResult.Rows[i]["MST"].ToString();
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 150;            
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Visible = false;
            dgvDanhsachCN.Columns[28].Width = 150;
            dgvDanhsachCN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Ngaysinh()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + frmMain.cn + "' ";
            strCmd += " and (Day(kh.Ngaysinh) between '" + dtpSNgaysinhTu.Text.Substring(0, 2) + "' and '" + dtpSNgaysinhDen.Text.Substring(0, 2) + "') ";
            strCmd += " and (Month(kh.Ngaysinh) between '" + dtpSNgaysinhTu.Text.Substring(3, 2) + "' and '" + dtpSNgaysinhDen.Text.Substring(3, 2) + "') ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang ";
            //strCmd += " Where LoaiKH='1' and (Day(Ngaysinh) between '" + dtpSNgaysinhTu.Text.Substring(0,2) + "' and '" + dtpSNgaysinhDen.Text.Substring(0,2) + "') ";
            //strCmd += " and (Month(Ngaysinh) between '" + dtpSNgaysinhTu.Text.Substring(3, 2) + "' and '" + dtpSNgaysinhDen.Text.Substring(3, 2) + "') and macn='" + frmMain.cn + "' ";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["GPDK"].ToString();
                    row[26] = dtResult.Rows[i]["QDTL"].ToString();
                    row[27] = dtResult.Rows[i]["MST"].ToString();
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 150;            
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Visible = false;
            dgvDanhsachCN.Columns[28].Width = 150;
            dgvDanhsachCN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Tinhtrang()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            int tinhtrang = 1;
            if (cbbSTinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbSTinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma ";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + frmMain.cn + "' and kh.Tinhtrang ='" + tinhtrang + "' ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and Tinhtrang ='" + tinhtrang + "' and macn='" + frmMain.cn + "'";
            strCmd += " Order by kh.MaKH, kh.Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string Stinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        Stinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        Stinhtrang = "Không hoạt động";
                    }
                    row[17] = Stinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["GPDK"].ToString();
                    row[26] = dtResult.Rows[i]["QDTL"].ToString();
                    row[27] = dtResult.Rows[i]["MST"].ToString();
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 150;            
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Visible = false;
            dgvDanhsachCN.Columns[28].Width = 150;
            dgvDanhsachCN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_SNhomKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);           
            col = new DataColumn("Nhóm KH", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA, nhom.TENNHOM,doituongkh.ten ";
            strCmd += " from KHACHHANGTIEMNANG as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh left join DMTINH as tinh on kh.TINH=tinh.MaTinh ";
            strCmd += " left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join kh.doituongkh=doituongkh.ma ";
            strCmd += " left join KHTN_NHOMKHTN khnhom on kh.MAKH=khnhom.MAKH join NHOMKHACHHANGTN nhom on khnhom.MANHOM=nhom.MANHOM ";
            strCmd += " Where kh.MACN='" + frmMain.cn + "' and kh.LOAIKH='1' and nhom.MANHOM='" + cbbSNhomKH.SelectedValue.ToString() + "' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string Stinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        Stinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        Stinhtrang = "Không hoạt động";
                    }
                    row[17] = Stinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    ngayKH = ngayKethon.Substring(0, 2);
                    thangKH = ngayKethon.Substring(3, 2);
                    namKH = ngayKethon.Substring(6, 4);

                    row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["Tennhom"].ToString();
                    row[28] = dtResult.Rows[i]["Loahinhtiepcan"].ToString();
                    row[29] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 60;
            dgvDanhsachCN.Columns[1].Width = 140;
            dgvDanhsachCN.Columns[2].Width = 200;
            dgvDanhsachCN.Columns[3].Width = 120;
            dgvDanhsachCN.Columns[4].Width = 120;
            dgvDanhsachCN.Columns[5].Width = 100;
            dgvDanhsachCN.Columns[6].Width = 100;
            dgvDanhsachCN.Columns[7].Width = 150;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[26].Width = 250;
            dgvDanhsachCN.Columns[27].Width = 100;
            Cursor.Current = Cursors.Default;
        }

        private void layDSDN_TenKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng dn", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as tendtdn from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on doituongkh.ma=kh.doituongkh left join doituongdn on doituongdn.ma=kh.doituongdn";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + frmMain.cn + "' and kh.Hoten like N'%" + txtDN_STen.Text.Trim() + "%' ";
            //strCmd = "Select * from Khachhang Where LoaiKH='2' and Hoten like N'%" + txtDN_STen.Text.Trim() + "%' and macn='" + frmMain.cn + "'";
            strCmd += " Order by kh.Hoten, kh.MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;

                    row[23] = dtResult.Rows[i]["Noicap"].ToString();
                    row[24] = dtResult.Rows[i]["GPDK"].ToString();
                    row[25] = dtResult.Rows[i]["QDTL"].ToString();
                    row[26] = dtResult.Rows[i]["MST"].ToString();

                    string ngayThanhlap, ngayTL, thangTL, namTL;
                    ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

                    ngayTL = ngayThanhlap.Substring(0, 2);
                    thangTL = ngayThanhlap.Substring(3, 2);
                    namTL = ngayThanhlap.Substring(6, 4);

                    row[27] = ngayTL + "/" + thangTL + "/" + namTL;
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["tendtkh"].ToString();
                    row[32] = dtResult.Rows[i]["tendtdn"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachDN.DataSource = dtDanhsach;
            dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachDN.Columns[0].Width = 60;
            dgvDanhsachDN.Columns[1].Width = 140;
            dgvDanhsachDN.Columns[2].Width = 200;
            dgvDanhsachDN.Columns[3].Width = 120;
            dgvDanhsachDN.Columns[4].Width = 120;
            dgvDanhsachDN.Columns[5].Width = 100;
            dgvDanhsachDN.Columns[6].Width = 100;
            dgvDanhsachDN.Columns[5].Visible = false;
            dgvDanhsachDN.Columns[6].Visible = false;
            dgvDanhsachDN.Columns[7].Width = 150;
            dgvDanhsachDN.Columns[8].Width = 200;
            dgvDanhsachDN.Columns[9].Width = 120;
            dgvDanhsachDN.Columns[10].Width = 120;
            dgvDanhsachDN.Columns[11].Width = 120;
            dgvDanhsachDN.Columns[12].Width = 200;
            dgvDanhsachDN.Columns[13].Width = 200;
            dgvDanhsachDN.Columns[14].Width = 200;
            dgvDanhsachDN.Columns[15].Width = 150;
            dgvDanhsachDN.Columns[16].Width = 150;
            dgvDanhsachDN.Columns[16].Visible = false;
            dgvDanhsachDN.Columns[17].Width = 150;
            dgvDanhsachDN.Columns[18].Width = 120;
            dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachDN.Columns[18].Visible = false;
            dgvDanhsachDN.Columns[19].Width = 120;
            dgvDanhsachDN.Columns[20].Width = 120;
            dgvDanhsachDN.Columns[21].Width = 120;
            dgvDanhsachDN.Columns[22].Width = 100;
            dgvDanhsachDN.Columns[23].Width = 150;
            dgvDanhsachDN.Columns[21].Visible = false;
            dgvDanhsachDN.Columns[22].Visible = false;
            dgvDanhsachDN.Columns[23].Visible = false;
            dgvDanhsachDN.Columns[24].Width = 150;
            dgvDanhsachDN.Columns[25].Width = 150;
            dgvDanhsachDN.Columns[26].Width = 150;
            dgvDanhsachDN.Columns[27].Width = 150;
            dgvDanhsachDN.Columns[28].Width = 150;
            dgvDanhsachDN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDSDN_MaKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng dn", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as tendtdn from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on doituongkh.ma=kh.doituongkh left join doituongdn on doituongdn.ma=kh.doituongdn";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + frmMain.cn + "' and kh.MaKH like '%" + txtDN_SMaKH.Text.Trim() + "%' ";
            //strCmd = "Select * from Khachhang Where LoaiKH='2' and MaKH like '%" + txtDN_SMaKH.Text.Trim() + "%' and macn='" + frmMain.cn + "'";
            strCmd += " Order by kh.MaKH, kh.Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;

                    row[23] = dtResult.Rows[i]["Noicap"].ToString();
                    row[24] = dtResult.Rows[i]["GPDK"].ToString();
                    row[25] = dtResult.Rows[i]["QDTL"].ToString();
                    row[26] = dtResult.Rows[i]["MST"].ToString();

                    string ngayThanhlap, ngayTL, thangTL, namTL;
                    ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

                    ngayTL = ngayThanhlap.Substring(0, 2);
                    thangTL = ngayThanhlap.Substring(3, 2);
                    namTL = ngayThanhlap.Substring(6, 4);

                    row[27] = ngayTL + "/" + thangTL + "/" + namTL;
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31]=dtResult.Rows[i]["tendtkh"].ToString();
                    row[32] = dtResult.Rows[i]["tendtdn"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachDN.DataSource = dtDanhsach;
            dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachDN.Columns[0].Width = 60;
            dgvDanhsachDN.Columns[1].Width = 140;
            dgvDanhsachDN.Columns[2].Width = 200;
            dgvDanhsachDN.Columns[3].Width = 120;
            dgvDanhsachDN.Columns[4].Width = 120;
            dgvDanhsachDN.Columns[5].Width = 100;
            dgvDanhsachDN.Columns[6].Width = 100;
            dgvDanhsachDN.Columns[5].Visible = false;
            dgvDanhsachDN.Columns[6].Visible = false;
            dgvDanhsachDN.Columns[7].Width = 150;
            dgvDanhsachDN.Columns[8].Width = 200;
            dgvDanhsachDN.Columns[9].Width = 120;
            dgvDanhsachDN.Columns[10].Width = 120;
            dgvDanhsachDN.Columns[11].Width = 120;
            dgvDanhsachDN.Columns[12].Width = 200;
            dgvDanhsachDN.Columns[13].Width = 200;
            dgvDanhsachDN.Columns[14].Width = 200;
            dgvDanhsachDN.Columns[15].Width = 150;
            dgvDanhsachDN.Columns[16].Width = 150;
            dgvDanhsachDN.Columns[16].Visible = false;
            dgvDanhsachDN.Columns[17].Width = 150;
            dgvDanhsachDN.Columns[18].Width = 120;
            dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachDN.Columns[18].Visible = false;
            dgvDanhsachDN.Columns[19].Width = 120;
            dgvDanhsachDN.Columns[20].Width = 120;
            dgvDanhsachDN.Columns[21].Width = 120;
            dgvDanhsachDN.Columns[22].Width = 100;
            dgvDanhsachDN.Columns[23].Width = 150;
            dgvDanhsachDN.Columns[21].Visible = false;
            dgvDanhsachDN.Columns[22].Visible = false;
            dgvDanhsachDN.Columns[23].Visible = false;
            dgvDanhsachDN.Columns[24].Width = 150;
            dgvDanhsachDN.Columns[25].Width = 150;
            dgvDanhsachDN.Columns[26].Width = 150;
            dgvDanhsachDN.Columns[27].Width = 150;
            dgvDanhsachDN.Columns[28].Width = 150;
            dgvDanhsachDN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDSDN_Dienthoai()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng dn", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as tendtdn from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on doituongkh.ma=kh.doituongkh left join doituongdn on doituongdn.ma=kh.doituongdn";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + frmMain.cn + "' and (kh.Dienthoai1 like '%" + txtDN_STel.Text.Trim() + "%' or kh.Dienthoai2 like '%" + txtDN_STel.Text.Trim() + "%') ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='2' and (Dienthoai1 like '%" + txtDN_STel.Text.Trim() + "%' or Dienthoai2 like '%" + txtDN_STel.Text.Trim() + "%') and macn='" + frmMain.cn + "' ";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;

                    row[23] = dtResult.Rows[i]["Noicap"].ToString();
                    row[24] = dtResult.Rows[i]["GPDK"].ToString();
                    row[25] = dtResult.Rows[i]["QDTL"].ToString();
                    row[26] = dtResult.Rows[i]["MST"].ToString();

                    string ngayThanhlap, ngayTL, thangTL, namTL;
                    ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

                    ngayTL = ngayThanhlap.Substring(0, 2);
                    thangTL = ngayThanhlap.Substring(3, 2);
                    namTL = ngayThanhlap.Substring(6, 4);

                    row[27] = ngayTL + "/" + thangTL + "/" + namTL;
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["tendtkh"].ToString();
                    row[32] = dtResult.Rows[i]["tendtdn"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachDN.DataSource = dtDanhsach;
            dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachDN.Columns[0].Width = 60;
            dgvDanhsachDN.Columns[1].Width = 140;
            dgvDanhsachDN.Columns[2].Width = 200;
            dgvDanhsachDN.Columns[3].Width = 120;
            dgvDanhsachDN.Columns[4].Width = 120;
            dgvDanhsachDN.Columns[5].Width = 100;
            dgvDanhsachDN.Columns[6].Width = 100;
            dgvDanhsachDN.Columns[5].Visible = false;
            dgvDanhsachDN.Columns[6].Visible = false;
            dgvDanhsachDN.Columns[7].Width = 150;
            dgvDanhsachDN.Columns[8].Width = 200;
            dgvDanhsachDN.Columns[9].Width = 120;
            dgvDanhsachDN.Columns[10].Width = 120;
            dgvDanhsachDN.Columns[11].Width = 120;
            dgvDanhsachDN.Columns[12].Width = 200;
            dgvDanhsachDN.Columns[13].Width = 200;
            dgvDanhsachDN.Columns[14].Width = 200;
            dgvDanhsachDN.Columns[15].Width = 150;
            dgvDanhsachDN.Columns[16].Width = 150;
            dgvDanhsachDN.Columns[16].Visible = false;
            dgvDanhsachDN.Columns[17].Width = 150;
            dgvDanhsachDN.Columns[18].Width = 120;
            dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachDN.Columns[18].Visible = false;
            dgvDanhsachDN.Columns[19].Width = 120;
            dgvDanhsachDN.Columns[20].Width = 120;
            dgvDanhsachDN.Columns[21].Width = 120;
            dgvDanhsachDN.Columns[22].Width = 100;
            dgvDanhsachDN.Columns[23].Width = 150;
            dgvDanhsachDN.Columns[21].Visible = false;
            dgvDanhsachDN.Columns[22].Visible = false;
            dgvDanhsachDN.Columns[23].Visible = false;
            dgvDanhsachDN.Columns[24].Width = 150;
            dgvDanhsachDN.Columns[25].Width = 150;
            dgvDanhsachDN.Columns[26].Width = 150;
            dgvDanhsachDN.Columns[27].Width = 150;
            dgvDanhsachDN.Columns[28].Width = 150;
            dgvDanhsachDN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDSDN_Tinhtrang()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng dn", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            int tinhtrang = 1;
            if (cbbDN_STinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbDN_STinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as tendtdn from Khachhangtiemnang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on doituongkh.ma=kh.doituongkh left join doituongdn on doituongdn.ma=kh.doituongdn";
            //strCmd = "Select kh.*, lv.Tennghanh from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + frmMain.cn + "' and kh.Tinhtrang ='" + tinhtrang + "' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='2' and Tinhtrang ='" + tinhtrang + "' and macn='" + frmMain.cn + "' ";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string Stinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        Stinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        Stinhtrang = "Không hoạt động";
                    }
                    row[17] = Stinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;

                    row[23] = dtResult.Rows[i]["Noicap"].ToString();
                    row[24] = dtResult.Rows[i]["GPDK"].ToString();
                    row[25] = dtResult.Rows[i]["QDTL"].ToString();
                    row[26] = dtResult.Rows[i]["MST"].ToString();

                    string ngayThanhlap, ngayTL, thangTL, namTL;
                    ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

                    ngayTL = ngayThanhlap.Substring(0, 2);
                    thangTL = ngayThanhlap.Substring(3, 2);
                    namTL = ngayThanhlap.Substring(6, 4);

                    row[27] = ngayTL + "/" + thangTL + "/" + namTL;
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    row[31] = dtResult.Rows[i]["tendtkh"].ToString();
                    row[32] = dtResult.Rows[i]["tendtdn"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachDN.DataSource = dtDanhsach;
            dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachDN.Columns[0].Width = 60;
            dgvDanhsachDN.Columns[1].Width = 140;
            dgvDanhsachDN.Columns[2].Width = 200;
            dgvDanhsachDN.Columns[3].Width = 120;
            dgvDanhsachDN.Columns[4].Width = 120;
            dgvDanhsachDN.Columns[5].Width = 100;
            dgvDanhsachDN.Columns[6].Width = 100;
            dgvDanhsachDN.Columns[5].Visible = false;
            dgvDanhsachDN.Columns[6].Visible = false;
            dgvDanhsachDN.Columns[7].Width = 150;
            dgvDanhsachDN.Columns[8].Width = 200;
            dgvDanhsachDN.Columns[9].Width = 120;
            dgvDanhsachDN.Columns[10].Width = 120;
            dgvDanhsachDN.Columns[11].Width = 120;
            dgvDanhsachDN.Columns[12].Width = 200;
            dgvDanhsachDN.Columns[13].Width = 200;
            dgvDanhsachDN.Columns[14].Width = 200;
            dgvDanhsachDN.Columns[15].Width = 150;
            dgvDanhsachDN.Columns[16].Width = 150;
            dgvDanhsachDN.Columns[16].Visible = false;
            dgvDanhsachDN.Columns[17].Width = 150;
            dgvDanhsachDN.Columns[18].Width = 120;
            dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachDN.Columns[18].Visible = false;
            dgvDanhsachDN.Columns[19].Width = 120;
            dgvDanhsachDN.Columns[20].Width = 120;
            dgvDanhsachDN.Columns[21].Width = 120;
            dgvDanhsachDN.Columns[22].Width = 100;
            dgvDanhsachDN.Columns[23].Width = 150;
            dgvDanhsachDN.Columns[21].Visible = false;
            dgvDanhsachDN.Columns[22].Visible = false;
            dgvDanhsachDN.Columns[23].Visible = false;
            dgvDanhsachDN.Columns[24].Width = 150;
            dgvDanhsachDN.Columns[25].Width = 150;
            dgvDanhsachDN.Columns[26].Width = 150;
            dgvDanhsachDN.Columns[27].Width = 150;
            dgvDanhsachDN.Columns[28].Width = 150;
            dgvDanhsachDN.Columns[29].Width = 250;
            Cursor.Current = Cursors.Default;
        }

        private void layDSDN_SNhomKH()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày thành lập", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình tiếp cận", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng kh", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng dn", typeof(string));    //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nhóm KH", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA, nhom.TENNHOM ";
            strCmd += " from KHACHHANGTIEMNANG as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh left join DMTINH as tinh on kh.TINH=tinh.MaTinh ";
            strCmd += " left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA ";
            strCmd += " left join KHTN_NHOMKHTN khnhom on kh.MAKH=khnhom.MAKH join NHOMKHACHHANGTN nhom on khnhom.MANHOM=nhom.MANHOM ";
            strCmd += " Where kh.MACN='" + frmMain.cn + "' and kh.LOAIKH='2' and nhom.MANHOM='" + cbbDN_SNhomKH.SelectedValue.ToString() + "' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[5] = ngayS + "/" + thangS + "/" + namS;

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[6] = gioitinh;
                    row[7] = dtResult.Rows[i]["Tennghanh"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["TenXa"].ToString();
                    row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
                    row[11] = dtResult.Rows[i]["TenTinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string Stinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        Stinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        Stinhtrang = "Không hoạt động";
                    }
                    row[17] = Stinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    //string loaiKH = "";
                    //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    //{
                    //    loaiKH = "Cá nhân";
                    //}
                    //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    //{
                    //    loaiKH = "Doanh nghiệp";
                    //}
                    //row[19] = loaiKH;
                    row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[22] = ngayC + "/" + thangC + "/" + namC;

                    row[23] = dtResult.Rows[i]["Noicap"].ToString();
                    row[24] = dtResult.Rows[i]["GPDK"].ToString();
                    row[25] = dtResult.Rows[i]["QDTL"].ToString();
                    row[26] = dtResult.Rows[i]["MST"].ToString();

                    string ngayThanhlap, ngayTL, thangTL, namTL;
                    ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

                    ngayTL = ngayThanhlap.Substring(0, 2);
                    thangTL = ngayThanhlap.Substring(3, 2);
                    namTL = ngayThanhlap.Substring(6, 4);

                    row[27] = ngayTL + "/" + thangTL + "/" + namTL;
                    row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[29] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[30] = dtResult.Rows[i]["Tennhom"].ToString();
                    row[31] = dtResult.Rows[i]["Loaihinhtiepcan"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachDN.DataSource = dtDanhsach;
            dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachDN.Columns[0].Width = 60;
            dgvDanhsachDN.Columns[1].Width = 140;
            dgvDanhsachDN.Columns[2].Width = 200;
            dgvDanhsachDN.Columns[3].Width = 120;
            dgvDanhsachDN.Columns[4].Width = 120;
            dgvDanhsachDN.Columns[5].Width = 100;
            dgvDanhsachDN.Columns[6].Width = 100;
            dgvDanhsachDN.Columns[5].Visible = false;
            dgvDanhsachDN.Columns[6].Visible = false;
            dgvDanhsachDN.Columns[7].Width = 150;
            dgvDanhsachDN.Columns[8].Width = 200;
            dgvDanhsachDN.Columns[9].Width = 120;
            dgvDanhsachDN.Columns[10].Width = 120;
            dgvDanhsachDN.Columns[11].Width = 120;
            dgvDanhsachDN.Columns[12].Width = 200;
            dgvDanhsachDN.Columns[13].Width = 200;
            dgvDanhsachDN.Columns[14].Width = 200;
            dgvDanhsachDN.Columns[15].Width = 150;
            dgvDanhsachDN.Columns[16].Width = 150;
            dgvDanhsachDN.Columns[16].Visible = false;
            dgvDanhsachDN.Columns[17].Width = 150;
            dgvDanhsachDN.Columns[18].Width = 120;
            dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachDN.Columns[18].Visible = false;
            dgvDanhsachDN.Columns[19].Width = 120;
            dgvDanhsachDN.Columns[20].Width = 120;
            dgvDanhsachDN.Columns[21].Width = 120;
            dgvDanhsachDN.Columns[22].Width = 100;
            dgvDanhsachDN.Columns[23].Width = 150;
            dgvDanhsachDN.Columns[21].Visible = false;
            dgvDanhsachDN.Columns[22].Visible = false;
            dgvDanhsachDN.Columns[23].Visible = false;
            dgvDanhsachDN.Columns[24].Width = 150;
            dgvDanhsachDN.Columns[25].Width = 150;
            dgvDanhsachDN.Columns[26].Width = 150;
            dgvDanhsachDN.Columns[27].Width = 150;
            dgvDanhsachDN.Columns[28].Width = 150;
            dgvDanhsachDN.Columns[29].Width = 250;
            dgvDanhsachDN.Columns[30].Width = 100;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Lienhe(string s)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvLienhe.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã NLH", typeof(string));   //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chức vụ", typeof(string));   //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));   //12
            dtDanhsach.Columns.Add(col);

            strCmd = "Select * from NGUOILIENHETN Where MaKH = '" + s + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai"].ToString();
                    row[4] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();
                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[5] = ngayC + "/" + thangC + "/" + namC;
                    row[6] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();
                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[7] = ngayS + "/" + thangS + "/" + namS;
                    row[8] = dtResult.Rows[i]["Diachi"].ToString();
                    row[9] = dtResult.Rows[i]["Email"].ToString();

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[10] = gioitinh;
                    row[11] = dtResult.Rows[i]["Chucvu"].ToString();
                    row[12] = dtResult.Rows[i]["MaKH"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvLienhe.DataSource = dtDanhsach;
            dgvLienhe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLienhe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLienhe.Columns[0].Width = 60;
            dgvLienhe.Columns[1].Width = 120;
            dgvLienhe.Columns[2].Width = 200;
            dgvLienhe.Columns[3].Width = 120;
            dgvLienhe.Columns[4].Width = 100;
            dgvLienhe.Columns[5].Width = 110;
            dgvLienhe.Columns[6].Width = 150;
            dgvLienhe.Columns[7].Width = 110;
            dgvLienhe.Columns[8].Width = 200;
            dgvLienhe.Columns[9].Width = 200;
            dgvLienhe.Columns[10].Width = 90;
            dgvLienhe.Columns[11].Width = 150;
            dgvLienhe.Columns[12].Width = 140;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Lienhe_DN(string s)
        {
            strCmd = "Select kh.*, lv.Tennghanh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA from Khachhang as kh left join Nghanhnghe as lv on kh.LINHVUC=lv.MaNghanh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + frmMain.cn + "' and kh.MaKH like '%" + s + "%' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            txtDN_MaKH.Text = dtResult.Rows[0]["MaKH"].ToString();
            txtDN_TenKH.Text = dtResult.Rows[0]["Hoten"].ToString();
            txtDN_Mobile.Text = dtResult.Rows[0]["Dienthoai1"].ToString();
            txtDN_Tel.Text = dtResult.Rows[0]["Dienthoai2"].ToString();
            cbbDN_Linhvuc.Text = dtResult.Rows[0]["Tennghanh"].ToString();
            txtDN_Address.Text = dtResult.Rows[0]["Diachi1"].ToString();
            cbbDN_Xa.Text = dtResult.Rows[0]["TenXa"].ToString();
            cbbDN_Huyen.Text = dtResult.Rows[0]["TenHuyen"].ToString();
            cbbDN_Tinh.Text = dtResult.Rows[0]["TenTinh"].ToString();
            txtDN_Address2.Text = dtResult.Rows[0]["Diachi2"].ToString();
            txtDN_Email.Text = dtResult.Rows[0]["Email"].ToString();
            txtDN_Website.Text = dtResult.Rows[0]["Website"].ToString();
            txtDN_NHGD.Text = dtResult.Rows[0]["NHGiaodich"].ToString();

            string tinhtrang = "";
            if (Boolean.Parse(dtResult.Rows[0]["Tinhtrang"].ToString()) == true)
            {
                tinhtrang = "Hoạt động";
            }
            else if (Boolean.Parse(dtResult.Rows[0]["Tinhtrang"].ToString()) == false)
            {
                tinhtrang = "Không hoạt động";
            }
            cbbDN_Tinhtrang.Text = tinhtrang;
            txtDN_MaNV.Text = dtResult.Rows[0]["MaNV"].ToString();
            txtDN_GPDK.Text = dtResult.Rows[0]["GPDK"].ToString();
            txtDN_QDTL.Text = dtResult.Rows[0]["QDTL"].ToString();
            txtDN_MST.Text = dtResult.Rows[0]["MST"].ToString();

            string ngayThanhlap, ngayTL, thangTL, namTL;
            ngayThanhlap = dtResult.Rows[0]["Ngaythanhlap"].ToString();

            ngayTL = ngayThanhlap.Substring(0, 2);
            thangTL = ngayThanhlap.Substring(3, 2);
            namTL = ngayThanhlap.Substring(6, 4);

            dtpDN_NgayTL.Text = ngayTL + "/" + thangTL + "/" + namTL;
            txtDN_Chitiet.Text = dtResult.Rows[0]["CTLoaiKH"].ToString();
            txtDN_Ghichu.Text = dtResult.Rows[0]["Ghichu"].ToString();
        }

        private void layDS_TenNLH()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvLienhe.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã NLH", typeof(string));   //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chức vụ", typeof(string));   //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));   //12
            dtDanhsach.Columns.Add(col);

            strCmd = "Select * from NGUOILIENHETN Where HoTen like N'%" + txtNLH_STen.Text + "%' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai"].ToString();
                    row[4] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();
                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[5] = ngayC + "/" + thangC + "/" + namC;
                    row[6] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();
                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[7] = ngayS + "/" + thangS + "/" + namS;
                    row[8] = dtResult.Rows[i]["Diachi"].ToString();
                    row[9] = dtResult.Rows[i]["Email"].ToString();

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[10] = gioitinh;
                    row[11] = dtResult.Rows[i]["Chucvu"].ToString();
                    row[12] = dtResult.Rows[i]["MaKH"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvLienhe.DataSource = dtDanhsach;
            dgvLienhe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLienhe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLienhe.Columns[0].Width = 60;
            dgvLienhe.Columns[1].Width = 120;
            dgvLienhe.Columns[2].Width = 200;
            dgvLienhe.Columns[3].Width = 120;
            dgvLienhe.Columns[4].Width = 100;
            dgvLienhe.Columns[5].Width = 110;
            dgvLienhe.Columns[6].Width = 150;
            dgvLienhe.Columns[7].Width = 110;
            dgvLienhe.Columns[8].Width = 200;
            dgvLienhe.Columns[9].Width = 200;
            dgvLienhe.Columns[10].Width = 90;
            dgvLienhe.Columns[11].Width = 150;
            dgvLienhe.Columns[12].Width = 140;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_CMNDNLH()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvLienhe.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã NLH", typeof(string));   //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chức vụ", typeof(string));   //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));   //12
            dtDanhsach.Columns.Add(col);

            strCmd = "Select * from NGUOILIENHETN Where CMND like '%" + txtNLH_SCMND.Text + "%' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai"].ToString();
                    row[4] = dtResult.Rows[i]["CMND"].ToString();

                    string ngayCap, ngayC, thangC, namC;
                    ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();
                    ngayC = ngayCap.Substring(0, 2);
                    thangC = ngayCap.Substring(3, 2);
                    namC = ngayCap.Substring(6, 4);

                    row[5] = ngayC + "/" + thangC + "/" + namC;
                    row[6] = dtResult.Rows[i]["Noicap"].ToString();

                    string ngaySinh, ngayS, thangS, namS;
                    ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();
                    ngayS = ngaySinh.Substring(0, 2);
                    thangS = ngaySinh.Substring(3, 2);
                    namS = ngaySinh.Substring(6, 4);

                    row[7] = ngayS + "/" + thangS + "/" + namS;
                    row[8] = dtResult.Rows[i]["Diachi"].ToString();
                    row[9] = dtResult.Rows[i]["Email"].ToString();

                    string gioitinh = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                    {
                        gioitinh = "Nam";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                    {
                        gioitinh = "Nữ";
                    }

                    row[10] = gioitinh;
                    row[11] = dtResult.Rows[i]["Chucvu"].ToString();
                    row[12] = dtResult.Rows[i]["MaKH"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvLienhe.DataSource = dtDanhsach;
            dgvLienhe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLienhe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLienhe.Columns[0].Width = 60;
            dgvLienhe.Columns[1].Width = 120;
            dgvLienhe.Columns[2].Width = 200;
            dgvLienhe.Columns[3].Width = 120;
            dgvLienhe.Columns[4].Width = 100;
            dgvLienhe.Columns[5].Width = 110;
            dgvLienhe.Columns[6].Width = 150;
            dgvLienhe.Columns[7].Width = 110;
            dgvLienhe.Columns[8].Width = 200;
            dgvLienhe.Columns[9].Width = 200;
            dgvLienhe.Columns[10].Width = 90;
            dgvLienhe.Columns[11].Width = 150;
            dgvLienhe.Columns[12].Width = 140;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Tinh()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * FROM DMTINH ORDER BY MATINH ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbTinh.DataSource = dt;
            cbbTinh.DisplayMember = "TENTINH";
            cbbTinh.ValueMember = "MATINH";
            cbbTinh.SelectedValue = "470";
        }

        private void layDS_Huyen()
        {
            string maTinh;
            if (cbbTinh.SelectedValue == null)
            {
                maTinh = "";
            }
            else
            {
                maTinh = cbbTinh.SelectedValue.ToString();
            }

            try
            {
                DataTable dt = new DataTable();
                String sCommand = "SELECT * FROM DMHUYEN WHERE LEFT(MAHUYEN,3) LIKE '" + maTinh + "' ORDER BY MAHUYEN ";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                cbbHuyen.DataSource = dt;
                cbbHuyen.DisplayMember = "TENHUYEN";
                cbbHuyen.ValueMember = "MAHUYEN";
                //cbbHuyen.SelectedIndex = 0;
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
        }

        private void layDS_Xa()
        {
            string maHuyen;
            if (cbbHuyen.SelectedValue == null)
            {
                maHuyen = "";
            }
            else
            {
                maHuyen = cbbHuyen.SelectedValue.ToString();
            }

            try
            {
                DataTable dt = new DataTable();
                String sCommand = "SELECT * FROM DMXAPHUONG WHERE LEFT(MAXA,5) LIKE '" + maHuyen + "' ORDER BY MAXA ";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                cbbXa.DataSource = dt;
                cbbXa.DisplayMember = "TENXA";
                cbbXa.ValueMember = "MAXA";
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
        }

        private void layLoaiKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT maloai,tenloai from dmloaikhachhang where Maloai <> '9999' and Maloai='001' or Maloai='003'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbLoaiKHIpcas.DataSource = dt;
            cbbLoaiKHIpcas.DisplayMember = "Tenloai";
            cbbLoaiKHIpcas.ValueMember = "Maloai";
            cbbLoaiKHIpcas.SelectedValue = "001";
        }

        private void layDN_LoaiKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT maloai,tenloai from dmloaikhachhang where Maloai <> '9999' and Maloai<>'001' and Maloai<>'003'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbDN_LoaiKHIpcas.DataSource = dt;
            cbbDN_LoaiKHIpcas.DisplayMember = "Tenloai";
            cbbDN_LoaiKHIpcas.ValueMember = "Maloai";
            cbbDN_LoaiKHIpcas.SelectedValue = "002";
        }

        private void layDSDN_Tinh()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * FROM DMTINH ORDER BY MATINH ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbDN_Tinh.DataSource = dt;
            cbbDN_Tinh.DisplayMember = "TENTINH";
            cbbDN_Tinh.ValueMember = "MATINH";
            cbbDN_Tinh.SelectedValue = "470";
        }

        private void layDSDN_Huyen()
        {
            string maTinh;
            if (cbbDN_Tinh.SelectedValue == null)
            {
                maTinh = "";
            }
            else
            {
                maTinh = cbbDN_Tinh.SelectedValue.ToString();
            }
            try
            {
                DataTable dt = new DataTable();
                String sCommand = "SELECT * FROM DMHUYEN WHERE LEFT(MAHUYEN,3) LIKE '" + maTinh + "' ORDER BY MAHUYEN ";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                cbbDN_Huyen.DataSource = dt;
                cbbDN_Huyen.DisplayMember = "TENHUYEN";
                cbbDN_Huyen.ValueMember = "MAHUYEN";
                //cbbDN_Huyen.SelectedIndex = 0;
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
        }

        private void layDSDN_Xa()
        {
            string maHuyen;
            if (cbbDN_Huyen.SelectedValue == null)
            {
                maHuyen = "";
            }
            else
            {
                maHuyen = cbbDN_Huyen.SelectedValue.ToString();
            }

            try
            {
                DataTable dt = new DataTable();
                String sCommand = "SELECT * FROM DMXAPHUONG WHERE LEFT(MAXA,5) LIKE '" + maHuyen + "' ORDER BY MAXA ";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                cbbDN_Xa.DataSource = dt;
                cbbDN_Xa.DisplayMember = "TENXA";
                cbbDN_Xa.ValueMember = "MAXA";
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
        }

        private void layDS_Linhvuc()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * FROM NGHANHNGHE Where LoaiKH='1' ORDER BY MANGHANH ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbLinhvuc.DataSource = dt;
            cbbLinhvuc.DisplayMember = "TENNGHANH";
            cbbLinhvuc.ValueMember = "MANGHANH";
        }

        private void layDSDN_Linhvuc()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * FROM NGHANHNGHE Where LoaiKH='2' ORDER BY MANGHANH ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbDN_Linhvuc.DataSource = dt;
            cbbDN_Linhvuc.DisplayMember = "TENNGHANH";
            cbbDN_Linhvuc.ValueMember = "MANGHANH";

        }

        private void layDS_NhomKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT manhom,Diengiai from nhomkhachhangTN Where MaCN='" + frmMain.cn + "' ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbSNhomKH.DataSource = dt;
            cbbSNhomKH.DisplayMember = "Diengiai";
            cbbSNhomKH.ValueMember = "Manhom";
        }

        private void layDSDN_NhomKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT manhom,Diengiai from nhomkhachhangTN Where MaCN='" + frmMain.cn + "' ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbDN_SNhomKH.DataSource = dt;
            cbbDN_SNhomKH.DisplayMember = "Diengiai";
            cbbDN_SNhomKH.ValueMember = "Manhom";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string doituongkh = "";
            if (cbKH2890.SelectedValue == null)
            {
                doituongkh = "";
            }
            else
            {
                doituongkh = cbKH2890.SelectedValue.ToString();
            }
            makh = "T" + frmMain.cn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
            txtMaKH.Text = makh;
            //if (txtMaKH.Text.Trim() == "")
            //{
            //    MessageBox.Show("Chưa nhập mã khách hàng.", "Thông báo");
            //    txtMaKH.Focus();
            //    return;
            //}

            strCmd = "Select * from Khachhangtiemnang Where MaKH='" + txtMaKH.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            string ngayC, thangC, namC, ngayS, thangS, namS, ngayKH, thangKH, namKH;
            ngayC = dtpNgaycap.Text.Substring(0, 2);
            thangC = dtpNgaycap.Text.Substring(3, 2);
            namC = dtpNgaycap.Text.Substring(6, 4);
            ngayS = dtpNgaysinh.Text.Substring(0, 2);
            thangS = dtpNgaysinh.Text.Substring(3, 2);
            namS = dtpNgaysinh.Text.Substring(6, 4);
            ngayKH = dtpNgayKH.Text.Substring(0, 2);
            thangKH = dtpNgayKH.Text.Substring(3, 2);
            namKH = dtpNgayKH.Text.Substring(6, 4);

            int gioitinh = 1;
            if (cbbGioitinh.Text == "Nam")
            {
                gioitinh = 1;
            }
            else if (cbbGioitinh.Text == "Nữ")
            {
                gioitinh = 0;
            }

            int loaiKH = 1;

            int tinhtrang = 1;
            if (cbbTinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbTinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            string thunhap = "";
            if (txtThunhap.Text == "")
            {
                thunhap = "0";
            }
            else
            {
                thunhap = String.Format("{0:0}", Decimal.Parse(txtThunhap.Text));
            }

            string maTinh, maHuyen, maXa;
            if (cbbTinh.SelectedValue == null || cbbHuyen.SelectedValue == null || cbbXa.SelectedValue == null)
            {
                maTinh = "";
                maHuyen = "";
                maXa = "";
            }
            else
            {
                maTinh = cbbTinh.SelectedValue.ToString();
                maHuyen = cbbHuyen.SelectedValue.ToString();
                maXa = cbbXa.SelectedValue.ToString();
            }

            string maNghanh;
            if (cbbLinhvuc.SelectedValue == null)
            {
                maNghanh = "";
            }
            else
            {
                maNghanh = cbbLinhvuc.SelectedValue.ToString();
            }

            string loaiKH_ipcas = "";
            loaiKH_ipcas = cbbLoaiKHIpcas.Text;
            
            txtMaNV.Text = frmDangnhap.UserID;

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into Khachhangtiemnang(MaKH,Hoten,Diachi1,Diachi2,Dienthoai1,Dienthoai2,Email,CMND,Ngaycap,Noicap,Ngaysinh,Gioitinh,Linhvuc,Website,LoaiKH,Thunhap,Sothich,MaNV,NHGiaodich,Ghichu,MaCN,Tinhtrang,CTLoaiKH,Xa,Huyen,Tinh,Ngaykethon,LoaiKH_Ipcas,doituongkh) ";
                strCmd += "Values('" + txtMaKH.Text.Trim() + "',N'" + txtTenKH.Text.Trim() + "',N'" + txtAddress.Text.Trim() + "',N'" + txtAddress2.Text.Trim() + "','" + txtMobile.Text.Trim() + "','" + txtTel.Text.Trim() + "','" + txtEmail.Text.Trim() + "','";
                strCmd += txtCMND.Text.Trim() + "','" + thangC + "/" + ngayC + "/" + namC + "',N'" + txtNoicap.Text.Trim() + "','" + thangS + "/" + ngayS + "/" + namS + "','" + gioitinh + "','" + maNghanh + "','" + txtWebsite.Text.Trim() + "','";
                strCmd += loaiKH + "','" + thunhap + "',N'" + txtSothich.Text.Trim() + "','" + txtMaNV.Text.Trim() + "','" + txtNHGD.Text.Trim() + "',N'";
                strCmd += txtGhichu.Text.Trim() + "','" + frmMain.cn + "',N'" + tinhtrang + "',N'" + txtChitiet.Text.Trim() + "',N'" + maXa + "',N'" + maHuyen + "',N'" + maTinh + "','" + thangKH + "/" + ngayKH + "/" + namKH + "',N'" + loaiKH_ipcas + "','"+doituongkh+"')";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();

                    layDS_KhachhangCN();

                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtMaKH.Focus();
                txtTenKH.Text = "";
                dtpNgaysinh.Text = "01/01/1990";
                cbbGioitinh.SelectedIndex = 0;
                txtMobile.Text = "";
                txtTel.Text = "";
                txtAddress.Text = "";
                txtAddress2.Text = "";
                txtEmail.Text = "";
                txtWebsite.Text = "";
                txtNHGD.Text = "";
                txtSothich.Text = "";
                txtThunhap.Text = "";
                cbbTinhtrang.SelectedIndex = 0;
                txtCMND.Text = "";
                dtpNgaycap.Text = "01/01/1990";
                txtNoicap.Text = "";
                dtpNgayKH.Text = "01/01/1990";
                txtChitiet.Text = "";
                txtGhichu.Text = "";
                layDS_Tinh();
                layDS_Huyen();
                layDS_Xa();
                cbbLoaiKHIpcas.SelectedValue = "001";
            }
            else
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại.", "Cảnh báo");
                txtMaKH.Focus();
                return;
            }
        }

        private void btnDN_Add_Click(object sender, EventArgs e)
        {
            makh = "T" + frmMain.cn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
            txtDN_MaKH.Text = makh;
            //if (txtDN_MaKH.Text.Trim() == "")
            //{
            //    MessageBox.Show("Chưa nhập mã khách hàng.", "Thông báo");
            //    txtDN_MaKH.Focus();
            //    return;
            //}

            strCmd = "Select * from Khachhangtiemnang Where MaKH='" + txtDN_MaKH.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int loaiKH = 2;

            string ngayTL, thangTL, namTL;
            ngayTL = dtpDN_NgayTL.Text.Substring(0, 2);
            thangTL = dtpDN_NgayTL.Text.Substring(3, 2);
            namTL = dtpDN_NgayTL.Text.Substring(6, 4);

            int tinhtrang = 1;
            if (cbbTinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbTinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            string maTinh, maHuyen, maXa;
            if (cbbDN_Tinh.SelectedValue == null || cbbDN_Huyen.SelectedValue == null || cbbDN_Xa.SelectedValue == null)
            {
                maTinh = "";
                maHuyen = "";
                maXa = "";
            }
            else
            {
                maTinh = cbbDN_Tinh.SelectedValue.ToString();
                maHuyen = cbbDN_Huyen.SelectedValue.ToString();
                maXa = cbbDN_Xa.SelectedValue.ToString();
            }

            string maNghanh;
            if (cbbDN_Linhvuc.SelectedValue == null)
            {
                maNghanh = "";
            }
            else
            {
                maNghanh = cbbDN_Linhvuc.SelectedValue.ToString();
            }

            string loaiKH_ipcas = "";
            loaiKH_ipcas = cbbDN_LoaiKHIpcas.Text;

            txtDN_MaNV.Text = frmDangnhap.UserID;
            string loaihinhtiepcan = "";
            if (chkTG.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",TG";
                else
                    loaihinhtiepcan = "TG";
            }
            if (chkTV.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",TV";
                else
                    loaihinhtiepcan = "TV";
            }
            if (chkDV.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",DV";
                else
                    loaihinhtiepcan = "DV";
            }
            string doituongkh;
            if (cbKH2890DN.SelectedValue == null)
            {
                doituongkh = "";
            }
            else
            {
                doituongkh = cbKH2890DN.SelectedValue.ToString();
            }
            string loaihinhDN;
            if (cbDN2890.SelectedValue == null)
            {
                loaihinhDN = "";
            }
            else
            {
                loaihinhDN = cbDN2890.SelectedValue.ToString();
            }
            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into Khachhangtiemnang(MaKH,Hoten,Diachi1,Diachi2,Dienthoai1,Dienthoai2,Email,Linhvuc,Website,GPDK,QDTL,MST,LoaiKH,MaNV,NHGiaodich,Ghichu,MaCN,Tinhtrang,CTLoaiKH,Ngaysinh,Ngaycap,Gioitinh,Xa,Huyen,Tinh,Ngaythanhlap,loaihinhtiepcan,doituongkh,doituongdn) ";
                strCmd += "Values('" + txtDN_MaKH.Text.Trim() + "',N'" + txtDN_TenKH.Text.Trim() + "',N'" + txtDN_Address.Text.Trim() + "',N'" + txtDN_Address2.Text.Trim() + "','";
                strCmd += txtDN_Mobile.Text.Trim() + "','" + txtDN_Tel.Text.Trim() + "','" + txtDN_Email.Text.Trim() + "','" + maNghanh + "','" + txtDN_Website.Text.Trim() + "','";
                strCmd += txtDN_GPDK.Text.Trim() + "','" + txtDN_QDTL.Text.Trim() + "','" + txtDN_MST.Text.Trim() + "','";
                strCmd += loaiKH + "','" + txtDN_MaNV.Text.Trim() + "','" + txtDN_NHGD.Text.Trim() + "',N'";
                strCmd += txtDN_Ghichu.Text.Trim() + "','" + frmMain.cn + "',N'" + tinhtrang + "',N'" + txtDN_Chitiet.Text.Trim() + "','" + "01/01/1990" + "','" + "01/01/1990" + "','" + "0";
                strCmd += "',N'" + maXa + "',N'" + maHuyen + "',N'" + maTinh + "','" + thangTL + "/" + ngayTL + "/" + namTL + "',N'" + loaiKH_ipcas + "','"+loaihinhtiepcan+"','"+doituongkh+"','"+loaihinhDN+"')";
                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();

                    layDS_KhachhangDN();

                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtDN_MaKH.Focus();
                txtDN_TenKH.Text = "";
                txtDN_Mobile.Text = "";
                txtDN_Tel.Text = "";
                txtDN_Address.Text = "";
                txtDN_Address2.Text = "";
                txtDN_Email.Text = "";
                txtDN_Website.Text = "";
                txtDN_NHGD.Text = "";
                cbbDN_Tinhtrang.SelectedIndex = 0;
                txtDN_GPDK.Text = "";
                txtDN_QDTL.Text = "";
                txtDN_MST.Text = "";
                dtpDN_NgayTL.Text = "01/01/1990";
                txtDN_Chitiet.Text = "";
                txtDN_Ghichu.Text = "";
                layDSDN_Tinh();
                layDSDN_Huyen();
                layDSDN_Xa();
                cbbDN_LoaiKHIpcas.SelectedValue = "002";
            }
            else
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại.", "Cảnh báo");
                txtDN_MaKH.Focus();
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string doituongkh = "";
            if (cbKH2890.SelectedValue == null)
            {
                doituongkh = "";
            }
            else
            {
                doituongkh = cbKH2890.SelectedValue.ToString();
            }
            if (txtMaKH.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn mã khách hàng.", "Thông báo");
                txtMaKH.Focus();
                return;
            }

            strCmd = "Select * from Khachhangtiemnang Where MaKH='" + txtMaKH.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            string ngayC, thangC, namC, ngayS, thangS, namS, ngayKH, thangKH, namKH;
            ngayC = dtpNgaycap.Text.Substring(0, 2);
            thangC = dtpNgaycap.Text.Substring(3, 2);
            namC = dtpNgaycap.Text.Substring(6, 4);
            ngayS = dtpNgaysinh.Text.Substring(0, 2);
            thangS = dtpNgaysinh.Text.Substring(3, 2);
            namS = dtpNgaysinh.Text.Substring(6, 4);
            ngayKH = dtpNgayKH.Text.Substring(0, 2);
            thangKH = dtpNgayKH.Text.Substring(3, 2);
            namKH = dtpNgayKH.Text.Substring(6, 4);

            int gioitinh = 1;
            if (cbbGioitinh.Text == "Nam")
            {
                gioitinh = 1;
            }
            else if (cbbGioitinh.Text == "Nữ")
            {
                gioitinh = 0;
            }

            int loaiKH = 1;

            int tinhtrang = 1;
            if (cbbTinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbTinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            string thunhap;
            if (txtThunhap.Text == "")
            {
                thunhap = "0";
            }
            else
            {
                thunhap = String.Format("{0:0}", Decimal.Parse(txtThunhap.Text));
            }

            string maTinh, maHuyen, maXa;
            if (cbbTinh.SelectedValue == null || cbbHuyen.SelectedValue == null || cbbXa.SelectedValue == null)
            {
                maTinh = "";
                maHuyen = "";
                maXa = "";
            }
            else
            {
                maTinh = cbbTinh.SelectedValue.ToString();
                maHuyen = cbbHuyen.SelectedValue.ToString();
                maXa = cbbXa.SelectedValue.ToString();
            }

            string maNghanh;
            if (cbbLinhvuc.SelectedValue == null)
            {
                maNghanh = "";
            }
            else
            {
                maNghanh = cbbLinhvuc.SelectedValue.ToString();
            }

            string loaiKH_ipcas = "";
            loaiKH_ipcas = cbbLoaiKHIpcas.Text;

            txtMaNV.Text = frmDangnhap.UserID;
            string loaihinhtiepcan = "";
            if (chkTG.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",TG";
                else
                    loaihinhtiepcan = "TG";
            }
            if (chkTV.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",TV";
                else
                    loaihinhtiepcan = "TV";
            }
            if (chkDV.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",DV";
                else
                    loaihinhtiepcan = "DV";
            }
            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update Khachhangtiemnang ";
                strCmd += "Set Hoten=N'" + txtTenKH.Text.Trim() + "',Diachi1=N'" + txtAddress.Text.Trim() + "',Diachi2=N'" + txtAddress2.Text.Trim() + "',Dienthoai1='" + txtMobile.Text.Trim() + "',Dienthoai2='" + txtTel.Text.Trim() + "',Email='" + txtEmail.Text.Trim();
                strCmd += "',CMND='" + txtCMND.Text.Trim() + "',Ngaycap='" + thangC + "/" + ngayC + "/" + namC + "',Noicap=N'" + txtNoicap.Text.Trim() + "',Ngaysinh='" + thangS + "/" + ngayS + "/" + namS + "',Gioitinh='" + gioitinh + "',Linhvuc='" + maNghanh + "',Website='" + txtWebsite.Text.Trim();
                strCmd += "',LoaiKH='" + loaiKH + "',Thunhap='" + thunhap + "',Sothich=N'" + txtSothich.Text.Trim() + "',MaNV='" + txtMaNV.Text.Trim() + "',NHGiaodich='" + txtNHGD.Text.Trim();
                strCmd += "',Ghichu=N'" + txtGhichu.Text.Trim() + "',MaCN='" + frmMain.cn + "',Tinhtrang='" + tinhtrang + "',CTLoaiKH=N'" + txtChitiet.Text.Trim();
                strCmd += "',Xa=N'" + maXa + "',Huyen=N'" + maHuyen + "',Tinh=N'" + maTinh + "',Ngaykethon='" + thangKH + "/" + ngayKH + "/" + namKH + "',LoaiKH_IPCAS=N'" + loaiKH_ipcas + "',loaihinhtiepcan='"+loaihinhtiepcan+"',doituongkh='"+doituongkh+"' ";
                strCmd += " Where MaKH='" + txtMaKH.Text.Trim() + "' ";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    frmMain.conn.Close();

                    layDS_KhachhangCN();

                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtMaKH.Focus();
                txtTenKH.Text = "";
                dtpNgaysinh.Text = "01/01/1990";
                cbbGioitinh.SelectedIndex = 0;
                txtMobile.Text = "";
                txtTel.Text = "";
                txtAddress.Text = "";
                txtAddress2.Text = "";
                txtEmail.Text = "";
                txtWebsite.Text = "";
                txtNHGD.Text = "";
                txtSothich.Text = "";
                txtThunhap.Text = "";
                cbbTinhtrang.SelectedIndex = 0;
                txtCMND.Text = "";
                dtpNgaycap.Text = "01/01/1990";
                txtNoicap.Text = "";
                dtpNgayKH.Text = "01/01/1990";
                txtChitiet.Text = "";
                txtGhichu.Text = "";
                layDS_Tinh();
                layDS_Huyen();
                layDS_Xa();
                cbbLoaiKHIpcas.SelectedValue = "001";
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại.", "Cảnh báo");
                txtMaKH.Focus();
                return;
            }
        }

        private void btnDN_Modify_Click(object sender, EventArgs e)
        {
            if (txtDN_MaKH.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn mã khách hàng.", "Thông báo");
                txtDN_MaKH.Focus();
                return;
            }

            strCmd = "Select * from Khachhangtiemnang Where MaKH='" + txtDN_MaKH.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int loaiKH = 2;

            string ngayTL, thangTL, namTL;
            ngayTL = dtpDN_NgayTL.Text.Substring(0, 2);
            thangTL = dtpDN_NgayTL.Text.Substring(3, 2);
            namTL = dtpDN_NgayTL.Text.Substring(6, 4);

            int tinhtrang = 1;
            if (cbbTinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbTinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            string maTinh, maHuyen, maXa;
            if (cbbDN_Tinh.SelectedValue == null || cbbDN_Huyen.SelectedValue == null || cbbDN_Xa.SelectedValue == null)
            {
                maTinh = "";
                maHuyen = "";
                maXa = "";
            }
            else
            {
                maTinh = cbbDN_Tinh.SelectedValue.ToString();
                maHuyen = cbbDN_Huyen.SelectedValue.ToString();
                maXa = cbbDN_Xa.SelectedValue.ToString();
            }

            string maNghanh;
            if (cbbDN_Linhvuc.SelectedValue == null)
            {
                maNghanh = "";
            }
            else
            {
                maNghanh = cbbDN_Linhvuc.SelectedValue.ToString();
            }

            string loaiKH_ipcas = "";
            loaiKH_ipcas = cbbDN_LoaiKHIpcas.Text;
            string loaihinhtiepcan = "";
            if (chkTG.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",TG";
                else
                    loaihinhtiepcan = "TG";
            }
            if (chkTV.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",TV";
                else
                    loaihinhtiepcan = "TV";
            }
            if (chkDV.Checked == true)
            {
                if (loaihinhtiepcan != "")
                    loaihinhtiepcan = loaihinhtiepcan + ",DV";
                else
                    loaihinhtiepcan = "DV";
            }
            txtDN_MaNV.Text = frmDangnhap.UserID;
            string doituongkh;
            if (cbKH2890DN.SelectedValue == null)
            {
                doituongkh = "";
            }
            else
            {
                doituongkh = cbKH2890DN.SelectedValue.ToString();
            }
            string loaihinhDN;
            if (cbDN2890.SelectedValue == null)
            {
                loaihinhDN = "";
            }
            else
            {
                loaihinhDN = cbDN2890.SelectedValue.ToString();
            }

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update Khachhangtiemnang ";
                strCmd += "Set Hoten=N'" + txtDN_TenKH.Text.Trim() + "',Diachi1=N'" + txtDN_Address.Text.Trim() + "',Diachi2=N'" + txtDN_Address2.Text.Trim() + "',Dienthoai1='" + txtDN_Mobile.Text.Trim() + "',Dienthoai2='" + txtDN_Tel.Text.Trim();
                strCmd += "',Email='" + txtDN_Email.Text.Trim() + "',Linhvuc='" + maNghanh + "',Website='" + txtDN_Website.Text.Trim();
                strCmd += "',GPDK='" + txtDN_GPDK.Text.Trim() + "',QDTL='" + txtDN_QDTL.Text.Trim() + "',MST='" + txtDN_MST.Text.Trim();
                strCmd += "',LoaiKH='" + loaiKH + "',MaNV='" + txtDN_MaNV.Text.Trim() + "',NHGiaodich='" + txtDN_NHGD.Text.Trim();
                strCmd += "',Ghichu=N'" + txtDN_Ghichu.Text.Trim() + "',MaCN='" + frmMain.cn + "',Tinhtrang='" + tinhtrang + "',CTLoaiKH=N'" + txtDN_Chitiet.Text.Trim();
                strCmd += "',Xa=N'" + maXa + "',Huyen=N'" + maHuyen + "',Tinh=N'" + maTinh + "',Ngaythanhlap='" + thangTL + "/" + ngayTL + "/" + namTL + "',LoaiKH_IPCAS=N'" + loaiKH_ipcas + "',loaihinhtiepcan='"+loaihinhtiepcan+"',doituongkh='"+doituongkh+"',doituongdn='"+loaihinhDN+"' ";
                strCmd += " Where MaKH='" + txtDN_MaKH.Text.Trim() + "' ";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    frmMain.conn.Close();

                    layDS_KhachhangDN();

                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtDN_MaKH.Focus();
                txtDN_TenKH.Text = "";
                txtDN_Mobile.Text = "";
                txtDN_Tel.Text = "";
                txtDN_Address.Text = "";
                txtDN_Address2.Text = "";
                txtDN_Email.Text = "";
                txtDN_Website.Text = "";
                txtDN_NHGD.Text = "";
                cbbDN_Tinhtrang.SelectedIndex = 0;
                txtDN_GPDK.Text = "";
                txtDN_QDTL.Text = "";
                txtDN_MST.Text = "";
                dtpDN_NgayTL.Text = "01/01/1990";
                txtDN_Chitiet.Text = "";
                txtDN_Ghichu.Text = "";
                layDSDN_Tinh();
                layDSDN_Huyen();
                layDSDN_Xa();
                cbbDN_LoaiKHIpcas.SelectedValue = "002";
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại.", "Cảnh báo");
                txtDN_MaKH.Focus();
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachCN.RowCount > 0)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa thông tin khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string maKH = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        strCmd = "Delete from Khachhangtiemnang Where MaKH='" + maKH + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        frmMain.conn.Close();

                        layDS_KhachhangCN();

                        MessageBox.Show("Đã xóa", "Thông báo");
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }

                    txtMaKH.Text = "";
                    txtTenKH.Text = "";
                    dtpNgaysinh.Text = "01/01/1990";
                    cbbGioitinh.SelectedIndex = 0;
                    txtMobile.Text = "";
                    txtTel.Text = "";
                    txtAddress.Text = "";
                    txtAddress2.Text = "";
                    txtEmail.Text = "";
                    txtWebsite.Text = "";
                    txtNHGD.Text = "";
                    txtSothich.Text = "";
                    txtThunhap.Text = "";
                    cbbTinhtrang.SelectedIndex = 0;
                    txtCMND.Text = "";
                    dtpNgaycap.Text = "01/01/1990";
                    txtNoicap.Text = "";
                    dtpNgayKH.Text = "01/01/1990";
                    txtChitiet.Text = "";
                    txtGhichu.Text = "";
                    layDS_Tinh();
                    layDS_Huyen();
                    layDS_Xa();
                    cbbLoaiKHIpcas.SelectedValue = "001";
                }
            }

        }

        private void btnDN_Del_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachDN.RowCount > 0)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa thông tin khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string maKH = dgvDanhsachDN.CurrentRow.Cells["Mã KH"].Value.ToString();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        if (dgvLienhe.RowCount > 0)
                        {
                            MessageBox.Show("Bạn phải xóa thông tin liên hệ của khách hàng này trước.", "Thông báo");
                            return;
                        }

                        strCmd = "Delete from Khachhangtiemnang Where MaKH='" + maKH + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        frmMain.conn.Close();

                        layDS_KhachhangDN();

                        MessageBox.Show("Đã xóa", "Thông báo");
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }

                    txtDN_MaKH.Focus();
                    txtDN_TenKH.Text = "";
                    txtDN_Mobile.Text = "";
                    txtDN_Tel.Text = "";
                    txtDN_Address.Text = "";
                    txtDN_Address2.Text = "";
                    txtDN_Email.Text = "";
                    txtDN_Website.Text = "";
                    txtDN_NHGD.Text = "";
                    cbbDN_Tinhtrang.SelectedIndex = 0;
                    txtDN_GPDK.Text = "";
                    txtDN_QDTL.Text = "";
                    txtDN_MST.Text = "";
                    dtpDN_NgayTL.Text = "01/01/1990";
                    txtDN_Chitiet.Text = "";
                    txtDN_Ghichu.Text = "";
                    layDSDN_Tinh();
                    layDSDN_Huyen();
                    layDSDN_Xa();
                    cbbDN_LoaiKHIpcas.SelectedValue = "002";
                }
            }
        }

        private void btnLH_Del_Click(object sender, EventArgs e)
        {
            if (dgvLienhe.RowCount > 0)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa thông tin người liên hệ này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string maKH = dgvDanhsachDN.CurrentRow.Cells["Mã KH"].Value.ToString();
                        string maNLH = dgvLienhe.CurrentRow.Cells["Mã NLH"].Value.ToString();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        strCmd = "Delete from NguoilienheTN Where MaNLH='" + maNLH + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        frmMain.conn.Close();

                        layDS_Lienhe(maKH);

                        MessageBox.Show("Đã xóa", "Thông báo");
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }
                    txtLH_MaNLH.Text = "";
                    txtLH_Hoten.Text = "";
                    dtpNgaysinh.Text = "01/01/1990";
                    cbbGioitinh.SelectedIndex = 0;
                    txtLH_Tel.Text = "";
                    txtLH_CMND.Text = "";
                    dtpLH_Ngaycap.Text = "01/01/1990";
                    txtLH_Noicap.Text = "";
                    txtLH_Address.Text = "";
                    txtLH_Email.Text = "";
                    txtLH_Chucvu.Text = "";
                }
            }
        }

        private void btnLH_Add_Click(object sender, EventArgs e)
        {
            txtLH_MaKH.Text = txtDN_MaKH.Text.Trim();
            if (txtLH_MaKH.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập mã khách hàng.", "Thông báo");
                txtDN_MaKH.Focus();
                return;
            }

            string ngayC, thangC, namC, ngayS, thangS, namS;
            ngayC = dtpLH_Ngaycap.Text.Substring(0, 2);
            thangC = dtpLH_Ngaycap.Text.Substring(3, 2);
            namC = dtpLH_Ngaycap.Text.Substring(6, 4);
            ngayS = dtpLH_Ngaysinh.Text.Substring(0, 2);
            thangS = dtpLH_Ngaysinh.Text.Substring(3, 2);
            namS = dtpLH_Ngaysinh.Text.Substring(6, 4);

            int gioitinh = 1;
            if (cbbLH_Gioitinh.Text == "Nam")
            {
                gioitinh = 1;
            }
            else if (cbbLH_Gioitinh.Text == "Nữ")
            {
                gioitinh = 0;
            }

            strCmd = "SELECT * FROM NGUOILIENHETN ";
            strCmd += "WHERE Hoten='" + txtLH_Hoten.Text.Trim() + "' and CMND='" + txtLH_CMND.Text.Trim() + "' and Dienthoai='" + txtLH_Tel.Text.Trim() + "' and MaKH='" + txtLH_MaKH.Text.Trim() + "' ";
            strCmd += " and Diachi='" + txtLH_Address.Text.Trim() + "' and Email='" + txtLH_Email.Text.Trim() + "' and Chucvu='" + txtLH_Chucvu.Text.Trim() + "' and Noicap='" + txtLH_Noicap.Text.Trim() + "' ";
            strCmd += " and Gioitinh='" + gioitinh + "' and Ngaysinh='" + thangS + "/" + ngayS + "/" + namS + "' and Ngaycap='" + thangC + "/" + ngayC + "/" + namC + "' and MaCN='" + frmMain.cn + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                string maNLH = "";
                string nam, thang, ngay, gio, phut, giay, miligiay;
                nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString().Substring(2, 2)));
                thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                maNLH = nam + thang + ngay + gio + phut + giay + miligiay;
                txtLH_MaNLH.Text = maNLH;

                strCmd = "Insert into NguoiLienHeTN(MaNLH,Hoten,CMND,Ngaycap,Noicap,Ngaysinh,Dienthoai,Diachi,Email,Chucvu,Gioitinh,MaKH,MaCN) ";
                strCmd += "Values('" + txtLH_MaNLH.Text.Trim() + "',N'" + txtLH_Hoten.Text.Trim() + "','" + txtLH_CMND.Text.Trim() + "','" + thangC + "/" + ngayC + "/" + namC + "',N'" + txtLH_Noicap.Text.Trim();
                strCmd += "','" + thangS + "/" + ngayS + "/" + namS + "','" + txtLH_Tel.Text.Trim() + "',N'" + txtLH_Address.Text.Trim() + "','" + txtLH_Email.Text.Trim();
                strCmd += "',N'" + txtLH_Chucvu.Text.Trim() + "','" + gioitinh + "','" + txtLH_MaKH.Text.Trim() + "','" + frmMain.cn + "')";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDS_Lienhe(txtDN_MaKH.Text);
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtLH_MaNLH.Text = "";
                txtLH_Hoten.Text = "";
                dtpLH_Ngaysinh.Text = "01/01/1990";
                cbbLH_Gioitinh.SelectedIndex = 0;
                txtLH_Address.Text = "";
                txtLH_Email.Text = "";
                txtLH_Chucvu.Text = "";
                txtLH_Tel.Text = "";
                txtLH_CMND.Text = "";
                dtpLH_Ngaycap.Text = "01/01/1990";
                txtLH_Noicap.Text = "";
                txtLH_MaKH.Text = "";
            }
            else
            {
                MessageBox.Show("Người liên hệ này đã tồn tại.", "Cảnh báo");
                txtLH_Tel.Focus();
                return;
            }
        }

        private void btnLH_Modify_Click(object sender, EventArgs e)
        {
            txtLH_MaKH.Text = txtDN_MaKH.Text.Trim();
            if (txtLH_MaKH.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập mã khách hàng.", "Thông báo");
                txtDN_MaKH.Focus();
                return;
            }

            string ngayC, thangC, namC, ngayS, thangS, namS;
            ngayC = dtpLH_Ngaycap.Text.Substring(0, 2);
            thangC = dtpLH_Ngaycap.Text.Substring(3, 2);
            namC = dtpLH_Ngaycap.Text.Substring(6, 4);
            ngayS = dtpLH_Ngaysinh.Text.Substring(0, 2);
            thangS = dtpLH_Ngaysinh.Text.Substring(3, 2);
            namS = dtpLH_Ngaysinh.Text.Substring(6, 4);

            int gioitinh = 1;
            if (cbbLH_Gioitinh.Text == "Nam")
            {
                gioitinh = 1;
            }
            else if (cbbLH_Gioitinh.Text == "Nữ")
            {
                gioitinh = 0;
            }

            strCmd = "SELECT * FROM NGUOILIENHETN ";
            strCmd += " Where MaNLH='" + txtLH_MaNLH.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update NguoiLienHeTN ";
                strCmd += "Set Hoten=N'" + txtLH_Hoten.Text.Trim() + "',CMND='" + txtLH_CMND.Text.Trim() + "',Ngaycap='" + thangC + "/" + ngayC + "/" + namC + "',Noicap=N'" + txtLH_Noicap.Text.Trim();
                strCmd += "',Ngaysinh='" + thangS + "/" + ngayS + "/" + namS + "',Dienthoai='" + txtLH_Tel.Text.Trim() + "',Diachi=N'" + txtLH_Address.Text.Trim() + "',Email='" + txtLH_Email.Text.Trim();
                strCmd += "',Chucvu=N'" + txtLH_Chucvu.Text.Trim() + "',Gioitinh='" + gioitinh + "',MaKH='" + txtLH_MaKH.Text.Trim() + "' ";
                strCmd += " Where MaNLH='" + txtLH_MaNLH.Text.Trim() + "' ";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDS_Lienhe(txtDN_MaKH.Text);
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtLH_MaNLH.Text = "";
                txtLH_Hoten.Text = "";
                dtpLH_Ngaysinh.Text = "01/01/1990";
                cbbLH_Gioitinh.SelectedIndex = 0;
                txtLH_Address.Text = "";
                txtLH_Email.Text = "";
                txtLH_Chucvu.Text = "";
                txtLH_Tel.Text = "";
                txtLH_CMND.Text = "";
                dtpLH_Ngaycap.Text = "01/01/1990";
                txtLH_Noicap.Text = "";

            }
            else
            {
                MessageBox.Show("Người liên hệ này không tồn tại.", "Cảnh báo");
                txtLH_MaNLH.Focus();
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDN_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDanhsachCN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnChuyen.Enabled = true;
            try
            {
                txtMaKH.Text = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtTenKH.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtMobile.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                txtTel.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT nhà"].Value.ToString();
                dtpNgaysinh.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                cbbGioitinh.Text = dgvDanhsachCN.CurrentRow.Cells["Giới tính"].Value.ToString();
                cbKH2890.Text = dgvDanhsachCN.CurrentRow.Cells["Đối tượng kh"].Value.ToString();
                txtAddress.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                txtAddress2.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ khác"].Value.ToString();
                txtEmail.Text = dgvDanhsachCN.CurrentRow.Cells["Email"].Value.ToString();

                txtWebsite.Text = dgvDanhsachCN.CurrentRow.Cells["Website"].Value.ToString();
                txtNHGD.Text = dgvDanhsachCN.CurrentRow.Cells["NH giao dịch"].Value.ToString();
                txtSothich.Text = dgvDanhsachCN.CurrentRow.Cells["Sở thích"].Value.ToString();
                cbbTinhtrang.Text = dgvDanhsachCN.CurrentRow.Cells["tình trạng"].Value.ToString();
                txtThunhap.Text = dgvDanhsachCN.CurrentRow.Cells["Thu nhập"].Value.ToString();
                txtMaNV.Text = dgvDanhsachCN.CurrentRow.Cells["Tên đ.nhập"].Value.ToString();

                txtCMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
                dtpNgaycap.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtNoicap.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                dtpNgayKH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày kết hôn"].Value.ToString();
                txtChitiet.Text = dgvDanhsachCN.CurrentRow.Cells["Chi tiết"].Value.ToString();
                txtGhichu.Text = dgvDanhsachCN.CurrentRow.Cells["Ghi chú"].Value.ToString();

                cbbTinh.Text = dgvDanhsachCN.CurrentRow.Cells["Tỉnh"].Value.ToString();
                layDS_Huyen();
                cbbHuyen.Text = dgvDanhsachCN.CurrentRow.Cells["Huyện"].Value.ToString();
                layDS_Xa();
                cbbXa.Text = dgvDanhsachCN.CurrentRow.Cells["Xã"].Value.ToString();
                cbbLinhvuc.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                cbbLoaiKHIpcas.Text = dgvDanhsachCN.CurrentRow.Cells["Loại KH"].Value.ToString();
                if (dgvDanhsachCN.CurrentRow.Cells["Loại hình tiếp cận"].Value.ToString().Contains("TG") == true)
                    chkTG.Checked = true;
                else
                    chkTG.Checked = false;
                if (dgvDanhsachCN.CurrentRow.Cells["Loại hình tiếp cận"].Value.ToString().Contains("TV") == true)
                    chkTV.Checked = true;
                else
                    chkTV.Checked = false;
                if (dgvDanhsachCN.CurrentRow.Cells["Loại hình tiếp cận"].Value.ToString().Contains("DV") == true)
                    chkDV.Checked = true;
                else
                    chkDV.Checked = false;
                
            }
            catch { }
        }

        private void dgvDanhsachDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDN_Chuyen.Enabled = true;
            try
            {
                txtDN_MaKH.Text = dgvDanhsachDN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtDN_TenKH.Text = dgvDanhsachDN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtDN_Mobile.Text = dgvDanhsachDN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                txtDN_Tel.Text = dgvDanhsachDN.CurrentRow.Cells["ĐT nhà"].Value.ToString();

                txtDN_Address.Text = dgvDanhsachDN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                txtDN_Address2.Text = dgvDanhsachDN.CurrentRow.Cells["Địa chỉ khác"].Value.ToString();
                txtDN_Email.Text = dgvDanhsachDN.CurrentRow.Cells["Email"].Value.ToString();

                txtDN_Website.Text = dgvDanhsachDN.CurrentRow.Cells["Website"].Value.ToString();
                txtDN_NHGD.Text = dgvDanhsachDN.CurrentRow.Cells["NH giao dịch"].Value.ToString();
                cbbDN_Tinhtrang.Text = dgvDanhsachDN.CurrentRow.Cells["tình trạng"].Value.ToString();
                txtDN_MaNV.Text = dgvDanhsachDN.CurrentRow.Cells["Tên đ.nhập"].Value.ToString();

                txtDN_GPDK.Text = dgvDanhsachDN.CurrentRow.Cells["Giấy phép ĐK"].Value.ToString();
                txtDN_QDTL.Text = dgvDanhsachDN.CurrentRow.Cells["QĐ thành lập"].Value.ToString();
                txtDN_MST.Text = dgvDanhsachDN.CurrentRow.Cells["MST"].Value.ToString();
                dtpDN_NgayTL.Text = dgvDanhsachDN.CurrentRow.Cells["Ngày thành lập"].Value.ToString();
                txtDN_Chitiet.Text = dgvDanhsachDN.CurrentRow.Cells["Chi tiết"].Value.ToString();
                txtDN_Ghichu.Text = dgvDanhsachDN.CurrentRow.Cells["Ghi chú"].Value.ToString();

                txtLH_Hoten.Text = "";
                txtLH_Tel.Text = "";
                txtLH_Address.Text = "";
                txtLH_CMND.Text = "";
                dtpLH_Ngaycap.Text = "01/01/1990";
                txtLH_Noicap.Text = "";
                dtpLH_Ngaysinh.Text = "01/01/1990";
                cbbLH_Gioitinh.SelectedIndex = 0;
                txtLH_Email.Text = "";
                txtLH_Chucvu.Text = "";

                layDS_Lienhe(txtDN_MaKH.Text);
                //if (dgvLienhe.RowCount > 0)
                //{
                //    btnLH_Del.Enabled = true;
                //}
                //else
                //{
                //    btnLH_Del.Enabled = false;
                //}

                cbbDN_Tinh.Text = dgvDanhsachDN.CurrentRow.Cells["Tỉnh"].Value.ToString();
                layDSDN_Huyen();
                cbbDN_Huyen.Text = dgvDanhsachDN.CurrentRow.Cells["Huyện"].Value.ToString();
                layDSDN_Xa();
                cbbDN_Xa.Text = dgvDanhsachDN.CurrentRow.Cells["Xã"].Value.ToString();
                cbbDN_Linhvuc.Text = dgvDanhsachDN.CurrentRow.Cells["Lĩnh vực"].Value.ToString();
                cbbDN_LoaiKHIpcas.Text = dgvDanhsachDN.CurrentRow.Cells["Loại KH"].Value.ToString();
                if (dgvDanhsachDN.CurrentRow.Cells["Loại hình tiếp cận"].Value.ToString().Contains("TG") == true)
                    chkTGDN.Checked = true;
                else
                    chkTGDN.Checked = false;
                if (dgvDanhsachDN.CurrentRow.Cells["Loại hình tiếp cận"].Value.ToString().Contains("TV") == true)
                    chkTVDN.Checked = true;
                else
                    chkTVDN.Checked = false;
                if (dgvDanhsachDN.CurrentRow.Cells["Loại hình tiếp cận"].Value.ToString().Contains("DV") == true)
                    chkDVDN.Checked = true;
                else
                    chkDVDN.Checked = false;
                cbDN2890.Text = dgvDanhsachDN.CurrentRow.Cells["Đối tượng kh"].Value.ToString();
                cbKH2890DN.Text = dgvDanhsachDN.CurrentRow.Cells["Đối tượng dn"].Value.ToString();
                
            }
            catch { }
        }

        private void dgvLienhe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtLH_MaNLH.Text = dgvLienhe.CurrentRow.Cells["Mã NLH"].Value.ToString();
                txtLH_Hoten.Text = dgvLienhe.CurrentRow.Cells["Họ tên"].Value.ToString();
                txtLH_Tel.Text = dgvLienhe.CurrentRow.Cells["Điện thoại"].Value.ToString();
                dtpLH_Ngaysinh.Text = dgvLienhe.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                txtLH_CMND.Text = dgvLienhe.CurrentRow.Cells["CMND"].Value.ToString();
                dtpLH_Ngaycap.Text = dgvLienhe.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtLH_Noicap.Text = dgvLienhe.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                txtLH_Address.Text = dgvLienhe.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                txtLH_Email.Text = dgvLienhe.CurrentRow.Cells["Email"].Value.ToString();
                txtLH_Chucvu.Text = dgvLienhe.CurrentRow.Cells["Chức vụ"].Value.ToString();
                cbbLH_Gioitinh.Text = dgvLienhe.CurrentRow.Cells["Giới tính"].Value.ToString();
                txtLH_MaKH.Text = dgvLienhe.CurrentRow.Cells["Mã KH"].Value.ToString();

                layDS_Lienhe_DN(txtLH_MaKH.Text);
            }
            catch { }
        }

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            layDS_MaKH();
        }

        private void btnSTen_Click(object sender, EventArgs e)
        {
            layDS_TenKH();
        }

        private void btnSTel_Click(object sender, EventArgs e)
        {
            layDS_Dienthoai();
        }

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            layDS_CMND();
        }

        private void btnSTinhtrang_Click(object sender, EventArgs e)
        {
            layDS_Tinhtrang();
        }

        private void btnSNgaysinh_Click(object sender, EventArgs e)
        {
            layDS_Ngaysinh();
        }

        private void btnDN_STen_Click(object sender, EventArgs e)
        {
            layDSDN_TenKH();
        }

        private void btnDN_SMaKH_Click(object sender, EventArgs e)
        {
            layDSDN_MaKH();
        }

        private void btnDN_STel_Click(object sender, EventArgs e)
        {
            layDSDN_Dienthoai();
        }

        private void btnDN_STinhtrang_Click(object sender, EventArgs e)
        {
            layDSDN_Tinhtrang();
        }

        private void dgvDanhsachCN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvDanhsachDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnNLH_STen_Click(object sender, EventArgs e)
        {
            layDS_TenNLH();
        }

        private void btnNLH_SCMND_Click(object sender, EventArgs e)
        {
            layDS_CMNDNLH();
        }

        private void txtDN_MaKH_Validating(object sender, CancelEventArgs e)
        {
            txtLH_MaKH.Text = txtDN_MaKH.Text;
        }

        private void cbbDN_Linhvuc_Click(object sender, EventArgs e)
        {
            //cbbDN_Linhvuc.DropDownWidth = 150;
        }

        private void cbbTinh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbHuyen.Text = "";
            cbbXa.Text = "";
            layDS_Huyen();
            layDS_Xa();
        }

        private void cbbHuyen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbXa.Text = "";
            layDS_Xa();
        }

        private void cbbDN_Tinh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbDN_Huyen.Text = "";
            cbbDN_Xa.Text = "";
            layDSDN_Huyen();
            layDSDN_Xa();
        }

        private void cbbDN_Huyen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbDN_Xa.Text = "";
            layDSDN_Xa();
        }

        private void llbCN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flag = "1";
            CRM.frmKHThamkhao frmTK = new frmKHThamkhao();
            frmTK.ShowDialog();

            txtMaKH.Text = frmKHThamkhao.maKH;
            txtTenKH.Text = frmKHThamkhao.hoten;
            txtMobile.Text = frmKHThamkhao.mobile;
            txtTel.Text = frmKHThamkhao.tel;
            if (frmKHThamkhao.ngaysinh != "")
            {
                dtpNgaysinh.Text = frmKHThamkhao.ngaysinh;
            }
            else
            {
                dtpNgaysinh.Text = "01/01/1990";
            }
            cbbGioitinh.Text = frmKHThamkhao.gioitinh;
            cbbLinhvuc.Text = frmKHThamkhao.linhvuc;
            txtAddress.Text = frmKHThamkhao.address;

            if (frmKHThamkhao.tinh == "")
            {
                cbbTinh.SelectedValue = "470";
            }
            else
            {
                cbbTinh.SelectedValue = frmKHThamkhao.tinh;
            }
            if (frmKHThamkhao.huyen == "")
            {
                cbbHuyen.SelectedValue = "47001";
            }
            else
            {
                cbbHuyen.SelectedValue = frmKHThamkhao.huyen;
            }
            if (frmKHThamkhao.xa == "")
            {
                cbbXa.SelectedValue = "4700101";
            }
            else
            {
                cbbXa.SelectedValue = frmKHThamkhao.xa;
            }

            txtAddress2.Text = frmKHThamkhao.address2;
            txtEmail.Text = frmKHThamkhao.email;
            txtWebsite.Text = frmKHThamkhao.website;
            txtNHGD.Text = frmKHThamkhao.NHGD;
            txtSothich.Text = frmKHThamkhao.sothich;
            cbbTinhtrang.Text = frmKHThamkhao.tinhtrang;
            txtThunhap.Text = frmKHThamkhao.thunhap;
            txtCMND.Text = frmKHThamkhao.CMND;
            if (frmKHThamkhao.ngaycap != "")
            {
                dtpNgaycap.Text = frmKHThamkhao.ngaycap;
            }
            else
            {
                dtpNgaycap.Text = "01/01/1990";
            }
            txtNoicap.Text = frmKHThamkhao.noicap;
            if (frmKHThamkhao.ngaykethon != "")
            {
                dtpNgayKH.Text = frmKHThamkhao.ngaykethon;
            }
            else
            {
                dtpNgayKH.Text = "01/01/1990";
            }
            txtChitiet.Text = frmKHThamkhao.chitiet;
            txtGhichu.Text = frmKHThamkhao.ghichu;
        }

        private void llbDN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flag = "2";
            CRM.frmKHThamkhao frmTK = new frmKHThamkhao();
            frmTK.ShowDialog();

            txtDN_MaKH.Text = frmKHThamkhao.maKH;
            txtDN_TenKH.Text = frmKHThamkhao.hoten;
            txtDN_Mobile.Text = frmKHThamkhao.mobile;
            txtDN_Tel.Text = frmKHThamkhao.tel;
            cbbDN_Linhvuc.Text = frmKHThamkhao.linhvuc;
            txtDN_Address.Text = frmKHThamkhao.address;

            if (frmKHThamkhao.tinh == "")
            {
                cbbDN_Tinh.SelectedValue = "470";
            }
            else
            {
                cbbDN_Tinh.SelectedValue = frmKHThamkhao.tinh;
            }
            if (frmKHThamkhao.huyen == "")
            {
                cbbDN_Huyen.SelectedValue = "47001";
            }
            else
            {
                cbbDN_Huyen.SelectedValue = frmKHThamkhao.huyen;
            }
            if (frmKHThamkhao.xa == "")
            {
                cbbDN_Xa.SelectedValue = "4700101";
            }
            else
            {
                cbbDN_Xa.SelectedValue = frmKHThamkhao.xa;
            }

            txtDN_Address2.Text = frmKHThamkhao.address2;
            txtDN_Email.Text = frmKHThamkhao.email;
            txtDN_Website.Text = frmKHThamkhao.website;
            txtDN_NHGD.Text = frmKHThamkhao.NHGD;
            cbbDN_Tinhtrang.Text = frmKHThamkhao.tinhtrang;
            txtDN_QDTL.Text = frmKHThamkhao.QDTL;
            txtDN_GPDK.Text = frmKHThamkhao.GPDK;
            txtDN_MST.Text = frmKHThamkhao.MST;
            if (frmKHThamkhao.ngaythanhlap != "")
            {
                dtpDN_NgayTL.Text = frmKHThamkhao.ngaythanhlap;
            }
            else
            {
                dtpDN_NgayTL.Text = "01/01/1990";
            }
            txtDN_Chitiet.Text = frmKHThamkhao.chitiet;
            txtDN_Ghichu.Text = frmKHThamkhao.ghichu;
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            makh = dgvDanhsachCN.Rows[dgvDanhsachCN.CurrentRow.Index].Cells[1].Value.ToString();
            CRM.frmKHChuyen form_chuyen = new frmKHChuyen();
            form_chuyen.ShowDialog();
            //Insert khach hang tiem nang nay vao danh sach khach hang hien huu
            strCmd = "Select * from Khachhang Where MaKH='" + CRM.frmKHChuyen.makhhh + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            string ngayC, thangC, namC, ngayS, thangS, namS, ngayKH, thangKH, namKH;
            if (dtpNgaycap.Text != DateTime.Now.ToShortDateString())
            {
                ngayC = dtpNgaycap.Text.Substring(0, 2);
                thangC = dtpNgaycap.Text.Substring(3, 2);
                namC = dtpNgaycap.Text.Substring(6, 4);

            }
            else
            {
                ngayC = "01";
                thangC = "01";
                namC = "1990";
            }
            if (dtpNgaysinh.Text != DateTime.Now.ToShortDateString())
            {
                ngayS = dtpNgaysinh.Text.Substring(0, 2);
                thangS = dtpNgaysinh.Text.Substring(3, 2);
                namS = dtpNgaysinh.Text.Substring(6, 4);
            }
            else
            {
                ngayS = "01";
                thangS = "01";
                namS = "1990";
            }
            if (dtpNgayKH.Text != DateTime.Now.ToShortDateString())
            {
                ngayKH = dtpNgayKH.Text.Substring(0, 2);
                thangKH = dtpNgayKH.Text.Substring(3, 2);
                namKH = dtpNgayKH.Text.Substring(6, 4);

            }
            else
            {
                ngayKH = "01";
                thangKH = "01";
                namKH = "1990";
            }

            int gioitinh = 1;
            if (cbbGioitinh.Text == "Nam")
            {
                gioitinh = 1;
            }
            else if (cbbGioitinh.Text == "Nữ")
            {
                gioitinh = 0;
            }

            int loaiKH = 1;
            
            int tinhtrang = 1;
            if (cbbTinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbTinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            string thunhap = "";
            if (txtThunhap.Text == "")
            {
                thunhap = "0";
            }
            else
            {
                thunhap = String.Format("{0:0}", Decimal.Parse(txtThunhap.Text));
            }

            string maNghanh;
            if (cbbLinhvuc.SelectedValue == null)
            {
                maNghanh = "";
            }
            else
            {
                maNghanh = cbbLinhvuc.SelectedValue.ToString();
            }

            string loaiKH_ipcas = "";
            loaiKH_ipcas = cbbLoaiKHIpcas.Text;

            txtMaNV.Text = frmDangnhap.UserID;

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into Khachhang(MaKH,Hoten,Diachi1,Diachi2,Dienthoai1,Dienthoai2,Email,CMND,Ngaycap,Noicap,Ngaysinh,Gioitinh,Linhvuc,Website,LoaiKH,Thunhap,Sothich,MaNV,NHGiaodich,Ghichu,MaCN,Tinhtrang,CTLoaiKH,Xa,Huyen,Tinh,Ngaykethon,LoaiKH_IPCAS) ";
                strCmd += "Values('" + CRM.frmKHChuyen.makhhh + "',N'" + txtTenKH.Text.Trim() + "',N'" + txtAddress.Text.Trim() + "',N'" + txtAddress2.Text.Trim() + "','" + txtMobile.Text.Trim() + "','" + txtTel.Text.Trim() + "','" + txtEmail.Text.Trim() + "','";
                strCmd += txtCMND.Text.Trim() + "','" + thangC + "/" + ngayC + "/" + namC + "',N'" + txtNoicap.Text.Trim() + "','" + thangS + "/" + ngayS + "/" + namS + "','" + gioitinh + "',N'" + maNghanh + "','" + txtWebsite.Text.Trim() + "','";
                //strCmd += txtGPDK.Text.Trim() + "','" + txtQDTL.Text.Trim() + "','" + txtDN_MST.Text.Trim() + "','" + loaiKH + "','" + txtThunhap.Text.Trim() + "',N'" + txtSothich.Text.Trim() + "','" + txtMaNV.Text.Trim() + "','" + txtNHGD.Text.Trim() + "',N'";
                strCmd += loaiKH + "','" + txtThunhap.Text.Trim() + "',N'" + txtSothich.Text.Trim() + "','" + txtMaNV.Text.Trim() + "','" + txtNHGD.Text.Trim() + "',N'";
                strCmd += txtGhichu.Text.Trim() + "','" + frmMain.cn + "',N'" + tinhtrang + "',N'" + txtChitiet.Text.Trim() + "','" + cbbXa.SelectedValue.ToString() + "','" + cbbHuyen.SelectedValue.ToString() + "','" + cbbTinh.SelectedValue.ToString();
                strCmd += "','" + thangKH + "/" + ngayKH + "/" + namKH + "',N'" + loaiKH_ipcas + "')";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();

                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại.", "Cảnh báo");
                txtMaKH.Focus();
                return;
            }
            btnChuyen.Enabled = false;
        }

        private void btnDN_Chuyen_Click(object sender, EventArgs e)
        {
            makh = dgvDanhsachDN.Rows[dgvDanhsachDN.CurrentRow.Index].Cells[1].Value.ToString();
            CRM.frmKHChuyen form_chuyen = new frmKHChuyen();
            form_chuyen.ShowDialog();
            //Insert khach hang tiem nang nay vao danh sach khach hang hien huu
            strCmd = "Select * from Khachhang Where MaKH='" + CRM.frmKHChuyen.makhhh + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            string ngayC, thangC, namC, ngayS, thangS, namS;
            ngayC = "01";
            thangC = "01";
            namC = "1990";
            ngayS = "01";
            thangS = "01";
            namS = "1990";
            string ngayTL, thangTL, namTL;
            if (dtpDN_NgayTL.Text != DateTime.Now.ToShortDateString())
            {
                ngayTL = dtpDN_NgayTL.Text.Substring(0, 2);
                thangTL = dtpDN_NgayTL.Text.Substring(3, 2);
                namTL = dtpDN_NgayTL.Text.Substring(6, 4);

            }
            else
            {
                ngayTL = "01";
                thangTL = "01";
                namTL = "1990";
            }

            int gioitinh = 0;
            int loaiKH = 2;

            int tinhtrang = 1;
            if (cbbTinhtrang.Text == "Hoạt động")
            {
                tinhtrang = 1;
            }
            else if (cbbTinhtrang.Text == "Không hoạt động")
            {
                tinhtrang = 0;
            }

            string maNghanh;
            if (cbbDN_Linhvuc.SelectedValue == null)
            {
                maNghanh = "";
            }
            else
            {
                maNghanh = cbbDN_Linhvuc.SelectedValue.ToString();
            }

            string loaiKH_ipcas = "";
            loaiKH_ipcas = cbbDN_LoaiKHIpcas.Text;

            txtDN_MaNV.Text = frmDangnhap.UserID;

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into Khachhang(MaKH,Hoten,Diachi1,Diachi2,Dienthoai1,Dienthoai2,Email,Ngaycap,Ngaysinh,Gioitinh,Linhvuc,Website,GPDK,QDTL,MST,LoaiKH,MaNV,NHGiaodich,Ghichu,MaCN,Tinhtrang,CTLoaiKH,Xa,Huyen,Tinh,NgayThanhLap,LoaiKH_IPCAS) ";
                strCmd += "Values('" + CRM.frmKHChuyen.makhhh + "',N'" + txtDN_TenKH.Text.Trim() + "',N'" + txtDN_Address.Text.Trim() + "',N'" + txtDN_Address2.Text.Trim() + "','" + txtDN_Mobile.Text.Trim() + "','" + txtDN_Tel.Text.Trim() + "','" + txtDN_Email.Text.Trim() + "','";
                strCmd += thangC + "/" + ngayC + "/" + namC + "','" + thangS + "/" + ngayS + "/" + namS + "','" + gioitinh + "',N'" + maNghanh + "','" + txtDN_Website.Text.Trim() + "','";
                strCmd += txtDN_GPDK.Text.Trim() + "','" + txtDN_QDTL.Text.Trim() + "','" + txtDN_MST.Text.Trim() + "','" + loaiKH + "','" + txtDN_MaNV.Text.Trim() + "','" + txtDN_NHGD.Text.Trim() + "',N'";
                strCmd += txtDN_Ghichu.Text.Trim() + "','" + frmMain.cn + "',N'" + tinhtrang + "',N'" + txtDN_Chitiet.Text.Trim() + "','" + cbbDN_Xa.SelectedValue.ToString() + "','" + cbbDN_Huyen.SelectedValue.ToString() + "','" + cbbDN_Tinh.SelectedValue.ToString();
                strCmd += "','" + thangTL + "/" + ngayTL + "/" + namTL + "',N'" + loaiKH_ipcas + "')";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();

                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại.", "Cảnh báo");
                txtDN_MaNV.Focus();
                return;
            }
            btnDN_Chuyen.Enabled = false;
        }

        private void txtThunhap_TextChanged(object sender, EventArgs e)
        {
            if (txtThunhap.Text != "")
            {
                string sDummy = txtThunhap.Text;
                try
                {
                    int iKeep = txtThunhap.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtThunhap.Text[i] == ',')
                        {
                            iKeep -= 1;
                        }
                    }
                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                    {
                        if (sDummy[i] == ',')
                        {
                            iKeep += 1;
                        }
                    }
                    txtThunhap.Text = sDummy;
                    txtThunhap.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtThunhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSNhomKH_Click(object sender, EventArgs e)
        {
            layDS_SNhomKH();
        }
        private void layKH2890()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT ma,ten from doituongkh where left(ma,1)='1' and ma<>'1' order by ma";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbKH2890.DataSource = dt;
            cbKH2890.DisplayMember = "Ten";
            cbKH2890.ValueMember = "Ma";
            cbKH2890.SelectedValue = "1";
        }
        private void layKH2890DN()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT ma,ten from doituongkh where left(ma,1)<>'1' and ma<>'32' order by ma";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbKH2890DN.DataSource = dt;
            cbKH2890DN.DisplayMember = "Ten";
            cbKH2890DN.ValueMember = "Ma";
            cbKH2890DN.SelectedValue = "1";
        }
        private void layLoaihinhDN2890()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT ma,ten from doituongdn order by ma";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbDN2890.DataSource = dt;
            cbDN2890.DisplayMember = "Ten";
            cbDN2890.ValueMember = "Ma";
            cbDN2890.SelectedValue = "1";
        }

        private void btnDN_SNhomKH_Click(object sender, EventArgs e)
        {

        }
    }
}