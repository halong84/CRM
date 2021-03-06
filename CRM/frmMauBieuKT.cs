using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;
//using N_MicrosoftExcelClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmMauBieuKT : Form
    {
        TinhBUS t_bus = new TinhBUS();
        HuyenBUS h_bus = new HuyenBUS();
        XaBUS x_bus = new XaBUS();
        MAUBIEUBUS mb_bus = new MAUBIEUBUS();
        ChinhanhBUS cn_bus = new ChinhanhBUS();
        NHANVIENBUS nv_bus = new NHANVIENBUS();
        TAIKHOANBUS tk_bus = new TAIKHOANBUS();
        KHACHHANGBUS khachhang_bus = new KHACHHANGBUS();

        String strCmd = "";
        private System.Data.DataTable dtResult = new System.Data.DataTable();
        private System.Data.DataTable dtDanhsach = new System.Data.DataTable();
        private System.Data.DataTable dtDanhsachDN = new System.Data.DataTable();

        public static string makh = "";
        private string chucvu_lanhdao = "";

        //Khai báo danh sách các dữ liệu đầu vào phục vụ tạo mẫu biểu
        private List<string> nguon_TTKH = new List<string>();
        private List<string> dich_TTKH = new List<string>();

        //Đường dẫn chứa file hồ sơ xuất ra từ chương trình
        public string output_file_path = @"C:\CRM1";

        public frmMauBieuKT()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            //cbbGioitinh.DropDownStyle = ComboBoxStyle.DropDownList;
            ////cbbTinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbGioitinh.SelectedIndex = 0;
            //cbbTinhtrang.SelectedIndex = 0;
            //cbbSTinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbSTinhtrang.SelectedIndex = 0;

            //cbbDN_Tinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_STinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbLH_Gioitinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbLH_Gioitinh.SelectedIndex = 0;
            //cbbDN_STinhtrang.SelectedIndex = 0;
            //cbbDN_Tinhtrang.SelectedIndex = 0;
            //cbbSNhomKH.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbSNhomKH.SelectedIndex = 0;
            //cbbDN_SNhomKH.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_SNhomKH.SelectedIndex = 0;
            //cbbLoaiKHIpcas.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_LoaiKHIpcas.DropDownStyle = ComboBoxStyle.DropDownList;

            //cbbTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbHuyen.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbXa.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_Tinh.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_Huyen.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_Xa.DropDownStyle = ComboBoxStyle.DropDownList;

            //cbbLinhvuc.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbDN_Linhvuc.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvLienhe.RowHeadersVisible = false;
            dgvLienhe.AllowUserToAddRows = false;
            dgvLienhe.ReadOnly = true;
            dgvLienhe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLienhe.MultiSelect = false;

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

            txtUQ_CMND.Enabled = false;
            txtUQ_HoTen.Enabled = false;
            txtUQ_NgayCapCMND.Enabled = false;
            txtUQ_NoiCapCMND.Enabled = false;

            txtInternet_DTDD.Enabled = false;
            txtInternet_HMGD.Enabled = false;

            
            txtTDTT_LoaiKH_moi.Enabled = false;
            txtTDTT_HoTenViet_moi.Enabled = false;
            txtTDTT_CMND_moi.Enabled = false;
            txtTDTT_NgayCapCMND_moi.Enabled = false;
            txtTDTT_NoiCapCMND_moi.Enabled = false;
            txtTDTT_DienThoai_moi.Enabled = false;
            txtTDTT_DiaChi_moi.Enabled = false;
            txtTDTT_Khac.Enabled = false;

        }

        private void frmMauBieuKT_Load(object sender, EventArgs e)
        {
            //Loại tài khoản
            cboxDong_LoaiTK.SelectedItem = "Cá nhân";
            
            //Gán danh sách mẫu biểu vào cboxMaubieu
            System.Data.DataTable dt_mb = mb_bus.DANH_SACH_MAU_BIEU("Kế toán", "KT01");
            cboxMaubieu.DataSource = dt_mb;
            cboxMaubieu.DisplayMember = "TEN_MAUBIEU";
            cboxMaubieu.ValueMember = "TEN_FILEMAUBIEU";

            //Gán mã chi nhánh vào txtMaCN
            txtMaCN.Text = Thongtindangnhap.macn;

            //Thiết lập tabpage hiển thị đầu tiên
            tctTTKHCN.SelectedTab = tpThongtinchung1;
            cboxMaubieu.Text = "Mẫu 01/TKDV.vn - Đề nghị kiêm hợp đồng mở và sử dụng tài khoản thanh toán";
            
            System.Data.DataTable chinhanh = cn_bus.CHI_NHANH_THEO_MACN(txtMaCN.Text);
            if (chinhanh.Rows.Count > 0)
            {
                txtChinhanhgoc.Text = chinhanh.Rows[0]["TENCN"].ToString();
            }

            //Gán danh sách kiểm soát, lãnh đạo vào cboxKiemsoat, cboxLanhdao
            if (Thongtindangnhap.hs)
            {
                //Đối với phòng thuộc trung tâm
                System.Data.DataTable dt_kiemsoat = nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Trưởng phòng");
                dt_kiemsoat.Merge(nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Phó phòng"));
                cboxKiemsoat.DataSource = dt_kiemsoat;
                cboxKiemsoat.DisplayMember = "HOTEN";
                cboxKiemsoat.ValueMember = "MANV";

                System.Data.DataTable dt_lanhdao = nv_bus.DANH_SACH_NV_THEO_CN_PB(Thongtindangnhap.macn, Thongtindangnhap.macn+"-01");
                //dt_lanhdao.Merge(nv_bus.DANH_SACH_NV_THEO_CN_PB_CV(Thongtindangnhap.macn, Thongtindangnhap.mapb, "Giám đốc"));
                cboxLanhdao.DataSource = dt_lanhdao;
                cboxLanhdao.DisplayMember = "HOTEN";
                cboxLanhdao.ValueMember = "MANV";
            }
            else
            {
                //Đối với phòng giao dịch
                System.Data.DataTable dt_kiemsoat = nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Phó Giám đốc");
                dt_kiemsoat.Merge(nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Giám đốc"));
                cboxKiemsoat.DataSource = dt_kiemsoat;
                cboxKiemsoat.DisplayMember = "HOTEN";
                cboxKiemsoat.ValueMember = "MANV";

                System.Data.DataTable dt_lanhdao = nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Phó Giám đốc");
                dt_lanhdao.Merge(nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Giám đốc"));
                cboxLanhdao.DataSource = dt_lanhdao;
                cboxLanhdao.DisplayMember = "HOTEN";
                cboxLanhdao.ValueMember = "MANV";
            }

            System.Data.DataTable nhanvien = nv_bus.NHAN_VIEN_THEO_MANV(cboxLanhdao.SelectedValue.ToString());
            if (nhanvien.Rows.Count > 0)
            {
                if (Convert.ToBoolean(nhanvien.Rows[0]["GIOITINH"].ToString()) == false)
                {
                    txtUQMNT_DAIDIENNH.Text = "Bà " + cboxLanhdao.Text;
                }
                else
                {
                    txtUQMNT_DAIDIENNH.Text = "Ông " + cboxLanhdao.Text;
                }

                txtUQMNT_GUQ.Text = nhanvien.Rows[0]["UYQUYEN"].ToString();

                //Lấy chức vụ lãnh đạo
                chucvu_lanhdao = nhanvien.Rows[0]["CHUCVU"].ToString();
                txtUQMNT_CHUCVUDAIDIENNH.Text = chucvu_lanhdao;
            }

            //Tạo thư mục chứ file hồ sơ khách hàng sau khi xuất ra từ chương trình
            //string file_path = @"C:\CRM";
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(output_file_path))
                {
                    //Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(output_file_path);
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                //di.Delete();
                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
            }
            finally { }
        }

        //private void frmKhachhangHH_Load(object sender, EventArgs e)
        //{
        //    //dtpNgaysinh.Text = "01/01/1990";
        //    //dtpNgaycap.Text = "01/01/1990";
        //    //dtpLH_Ngaysinh.Text = "01/01/1990";
        //    //dtpLH_Ngaycap.Text = "01/01/1990";
        //    //dtpNgayKH.Text = "01/01/1990";
        //    //dtpDN_NgayTL.Text = "01/01/1990";
        //    //txtMaNV.Text = Thongtindangnhap.user_id;
        //    txtDN_MaNV.Text = Thongtindangnhap.user_id;

        //    layDS_Tinh();
        //    layDS_Huyen();
        //    layDS_Xa();
        //    layDSDN_Tinh();
        //    layDSDN_Huyen();
        //    layDSDN_Xa();
        //    layDS_Linhvuc();
        //    layDSDN_Linhvuc();
        //    layDS_NhomKH();
        //    layDSDN_NhomKH();
        //    layLoaiKH();
        //    layDN_LoaiKH();
        //    layKH2890();
        //    layKH2890DN();
        //    layLoaihinhDN2890();
        //}

        private void layDS_KhachhangCN()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            dtDanhsach = new System.Data.DataTable();
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
            col = new DataColumn("Xã", typeof(string));    //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //26
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng KH", typeof(string));    //27
            dtDanhsach.Columns.Add(col);
            
            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA, doituongkh.ten, noicap.NOICAP as noicapcmnd from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            strCmd += " left join NOICAPCMND AS noicap on kh.NOICAP = noicap.MA_NOICAP";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + txtMaCN.Text + "' ";
            strCmd += " Order by kh.MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    row[7] = dtResult.Rows[i]["Tennganh"].ToString();
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

                    row[23] = dtResult.Rows[i]["noicapcmnd"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    if (ngayKethon.Length > 0)
                    {
                        ngayKH = ngayKethon.Substring(0, 2);
                        thangKH = ngayKethon.Substring(3, 2);
                        namKH = ngayKethon.Substring(6, 4);

                        row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    }
                    else
                    {
                        row[24] = "";
                    }

                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["ten"].ToString();
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
            dgvDanhsachCN.Columns[7].Visible = false;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[12].Visible = false;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[13].Visible = false;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[14].Visible = false;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[15].Visible = false;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[16].Visible = false;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].Visible = false; 
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[20].Visible = false; 
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false; 
            dgvDanhsachCN.Columns[26].Width = 150;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[27].Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_KhachhangDN()
        {
            //dgvDanhsachDN.Visible = true;
            //dgvDanhsach.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
            dtDanhsach = new System.Data.DataTable();
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

            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA ";
            //strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + Thongtindangnhap.macn + "' ";
            //strCmd = "Select kh.*, lv.Tennganh from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + txtMaCN.Text + "' ";
            strCmd += " Order by kh.MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    row[5] = dtResult.Rows[i]["Tennganh"].ToString();
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

                    if (ngayThanhlap.Length > 0)
                    {
                        ngayTL = ngayThanhlap.Substring(0, 2);
                        thangTL = ngayThanhlap.Substring(3, 2);
                        namTL = ngayThanhlap.Substring(6, 4);

                        row[20] = ngayTL + "/" + thangTL + "/" + namTL;
                    }
                    else
                    {
                        row[20] = "";
                    }

                    row[21] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[22] = dtResult.Rows[i]["Ghichu"].ToString();
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
            dgvDanhsachDN.Columns[22].Width = 150;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_TenKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            dtDanhsach = new System.Data.DataTable();
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
            col = new DataColumn("Xã", typeof(string));    //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //26
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tương KH", typeof(string));    //27
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten, noicap.noicap as noicapcmnd  from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            strCmd += " left join NOICAPCMND as noicap on kh.NOICAP = noicap.MA_NOICAP";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + txtMaCN.Text + "' and kh.Hoten like N'%" + txtSTen.Text.Trim() + "%' ";
            strCmd += " Order by kh.Hoten, kh.MaKH ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and Hoten like N'%" + txtSTen.Text.Trim() + "%' and macn='" + Thongtindangnhap.macn + "' ";
            //strCmd += " Order by Hoten, MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    row[7] = dtResult.Rows[i]["Tennganh"].ToString();
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
                    row[23] = dtResult.Rows[i]["Noicapcmnd"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    if (ngayKethon.Length > 0)
                    {
                        ngayKH = ngayKethon.Substring(0, 2);
                        thangKH = ngayKethon.Substring(3, 2);
                        namKH = ngayKethon.Substring(6, 4);

                        row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    }
                    else
                    {
                        row[24] = "";
                    }

                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["ten"].ToString();
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
            dgvDanhsachCN.Columns[27].Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_MaKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            dtDanhsach = new System.Data.DataTable();
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
            col = new DataColumn("Xã", typeof(string));    //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //26
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng KH", typeof(string));    //27
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten, noicap.noicap as noicapcmnd  from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            strCmd += " left join NOICAPCMND as noicap on kh.NOICAP = noicap.MA_NOICAP";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + txtMaCN.Text + "' and kh.MaKH like '%" + txtSMaKH.Text.Trim() + "%' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and MaKH like '%" + txtSMaKH.Text.Trim() + "%' and macn='" + Thongtindangnhap.macn + "' ";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    row[7] = dtResult.Rows[i]["Tennganh"].ToString();
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
                    row[23] = dtResult.Rows[i]["Noicapcmnd"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    if (ngayKethon.Length > 0)
                    {
                        ngayKH = ngayKethon.Substring(0, 2);
                        thangKH = ngayKethon.Substring(3, 2);
                        namKH = ngayKethon.Substring(6, 4);

                        row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    }
                    else
                    {
                        row[24] = "";
                    }

                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["ten"].ToString();
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
            dgvDanhsachCN.Columns[7].Visible = false;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[12].Visible = false;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[13].Visible = false;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[14].Visible = false;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[15].Visible = false;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[16].Visible = false;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].Visible = false;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[20].Visible = false;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Width = 150;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[27].Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Dienthoai()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            dtDanhsach = new System.Data.DataTable();
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
            col = new DataColumn("Xã", typeof(string));    //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //26
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng KH", typeof(string));    //27
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten, noicap.noicap as noicapcmnd  from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            strCmd += " left join NOICAPCMND as noicap on kh.NOICAP = noicap.MA_NOICAP";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + txtMaCN.Text + "' and (kh.Dienthoai1 like '%" + txtSTel.Text.Trim() + "%' or kh.Dienthoai2 like '%" + txtSTel.Text.Trim() + "%') ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and (Dienthoai1 like '%" + txtSTel.Text.Trim() + "%' or Dienthoai2 like '%" + txtSTel.Text.Trim() + "%') and macn='" + Thongtindangnhap.macn + "'";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    row[7] = dtResult.Rows[i]["Tennganh"].ToString();
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
                    row[23] = dtResult.Rows[i]["Noicapcmnd"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    if (ngayKethon.Length > 0)
                    {
                        ngayKH = ngayKethon.Substring(0, 2);
                        thangKH = ngayKethon.Substring(3, 2);
                        namKH = ngayKethon.Substring(6, 4);

                        row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    }
                    else
                    {
                        row[24] = "";
                    }

                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["ten"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
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
            dgvDanhsachCN.Columns[7].Visible = false;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[12].Visible = false;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[13].Visible = false;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[14].Visible = false;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[15].Visible = false;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[16].Visible = false;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].Visible = false;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[20].Visible = false;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Width = 150;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[27].Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_CMND()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            dtDanhsach = new System.Data.DataTable();
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
            col = new DataColumn("Xã", typeof(string));    //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //26
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng KH", typeof(string));    //27
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten, noicap.noicap as noicapcmnd  from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            strCmd += " left join NOICAPCMND as noicap on kh.NOICAP = noicap.MA_NOICAP";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + txtMaCN.Text + "' and kh.CMND like '%" + txtSCMND.Text.Trim() + "%' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";
            //strCmd = "Select * from Khachhang Where LoaiKH='1' and CMND like '%" + txtSCMND.Text.Trim() + "%' and macn='" + Thongtindangnhap.macn + "' ";
            //strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    row[7] = dtResult.Rows[i]["Tennganh"].ToString();
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
                    row[23] = dtResult.Rows[i]["Noicapcmnd"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    if (ngayKethon.Length > 0)
                    {
                        ngayKH = ngayKethon.Substring(0, 2);
                        thangKH = ngayKethon.Substring(3, 2);
                        namKH = ngayKethon.Substring(6, 4);

                        row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    }
                    else
                    {
                        row[24] = "";
                    }

                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["ten"].ToString();
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
            dgvDanhsachCN.Columns[7].Visible = false;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[12].Visible = false;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[13].Visible = false;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[14].Visible = false;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[15].Visible = false;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[16].Visible = false;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].Visible = false;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[20].Visible = false;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Width = 150;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[27].Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Ngaysinh()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
            dtDanhsach = new System.Data.DataTable();
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
            col = new DataColumn("Xã", typeof(string));    //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đ.nhập", typeof(string));  //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết hôn", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //25
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //26
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng KH", typeof(string));    //27
            dtDanhsach.Columns.Add(col);

            string ngaysinh = txtNgaysinh.Text.Substring(3, 2) + "/" + txtNgaysinh.Text.Substring(0, 2) + "/" + txtNgaysinh.Text.Substring(6, 4);
            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten, noicap.noicap as noicapcmnd  from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            strCmd += " left join NOICAPCMND as noicap on kh.NOICAP = noicap.MA_NOICAP";
            strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + txtMaCN.Text + "' ";
            strCmd += " and Ngaysinh = '" + ngaysinh + "' Order by kh.MaKH, kh.Hoten";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    row[7] = dtResult.Rows[i]["Tennganh"].ToString();
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

                    row[23] = dtResult.Rows[i]["Noicapcmnd"].ToString();

                    string ngayKethon, ngayKH, thangKH, namKH;
                    ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                    if (ngayKethon.Length > 0)
                    {
                        ngayKH = ngayKethon.Substring(0, 2);
                        thangKH = ngayKethon.Substring(3, 2);
                        namKH = ngayKethon.Substring(6, 4);

                        row[24] = ngayKH + "/" + thangKH + "/" + namKH;
                    }
                    else
                    {
                        row[24] = "";
                    }

                    row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[26] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[27] = dtResult.Rows[i]["ten"].ToString();
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
            dgvDanhsachCN.Columns[7].Visible = false;
            dgvDanhsachCN.Columns[8].Width = 200;
            dgvDanhsachCN.Columns[9].Width = 120;
            dgvDanhsachCN.Columns[10].Width = 120;
            dgvDanhsachCN.Columns[11].Width = 120;
            dgvDanhsachCN.Columns[12].Width = 200;
            dgvDanhsachCN.Columns[12].Visible = false;
            dgvDanhsachCN.Columns[13].Width = 200;
            dgvDanhsachCN.Columns[13].Visible = false;
            dgvDanhsachCN.Columns[14].Width = 200;
            dgvDanhsachCN.Columns[14].Visible = false;
            dgvDanhsachCN.Columns[15].Width = 150;
            dgvDanhsachCN.Columns[15].Visible = false;
            dgvDanhsachCN.Columns[16].Width = 150;
            dgvDanhsachCN.Columns[16].Visible = false;
            dgvDanhsachCN.Columns[17].Width = 150;
            dgvDanhsachCN.Columns[18].Width = 120;
            dgvDanhsachCN.Columns[18].Visible = false;
            dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            dgvDanhsachCN.Columns[19].Width = 120;
            dgvDanhsachCN.Columns[20].Width = 120;
            dgvDanhsachCN.Columns[20].Visible = false;
            dgvDanhsachCN.Columns[21].Width = 120;
            dgvDanhsachCN.Columns[22].Width = 100;
            dgvDanhsachCN.Columns[23].Width = 150;
            dgvDanhsachCN.Columns[24].Width = 150;
            dgvDanhsachCN.Columns[25].Width = 150;
            dgvDanhsachCN.Columns[25].Visible = false;
            dgvDanhsachCN.Columns[26].Width = 150;
            dgvDanhsachCN.Columns[26].Visible = false;
            dgvDanhsachCN.Columns[27].Width = 150;
            dgvDanhsachCN.Columns[27].Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Tinhtrang()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            
            //Cursor.Current = Cursors.WaitCursor;
            //dgvDanhsachCN.Refresh();
            //dtDanhsach = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));  //1
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tên KH", typeof(string)); //2
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("ĐT di động", typeof(string)); //3
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("ĐT nhà", typeof(string)); //4
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));  //5
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));  //6
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Xã", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Huyện", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tỉnh", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //10
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Website", typeof(string));    //11
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("NH giao dịch", typeof(string));   //12
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Sở thích", typeof(string));   //13
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tình trạng", typeof(string)); //14
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Thu nhập", typeof(decimal));   //15
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Loại KH", typeof(string));    //16
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string));   //18
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));   //19
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));    //20
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Chi tiết", typeof(string));   //24
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ghi chú", typeof(string));    //25
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Đối tượng KH", typeof(string));    //25
            //dtDanhsach.Columns.Add(col);

            ////int tinhtrang = 1;
            ////if (cbbSTinhtrang.Text == "Hoạt động")
            ////{
            ////    tinhtrang = 1;
            ////}
            ////else if (cbbSTinhtrang.Text == "Không hoạt động")
            ////{
            ////    tinhtrang = 0;
            ////}

            //strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            //strCmd += " Where kh.LOAIKH='1' and kh.MACN='" + Thongtindangnhap.macn + "' and kh.Tinhtrang ='" + tinhtrang + "' ";
            //strCmd += " Order by kh.MaKH, kh.Hoten ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsach.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaKH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
            //        row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[5] = ngayS + "/" + thangS + "/" + namS;

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[6] = gioitinh;
            //        row[7] = dtResult.Rows[i]["Tennganh"].ToString();
            //        row[8] = dtResult.Rows[i]["Diachi1"].ToString();
            //        row[9] = dtResult.Rows[i]["TenXa"].ToString();
            //        row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
            //        row[11] = dtResult.Rows[i]["TenTinh"].ToString();
            //        row[12] = dtResult.Rows[i]["Diachi2"].ToString();
            //        row[13] = dtResult.Rows[i]["Email"].ToString();
            //        row[14] = dtResult.Rows[i]["Website"].ToString();
            //        row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
            //        row[16] = dtResult.Rows[i]["Sothich"].ToString();

            //        string Stinhtrang = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
            //        {
            //            Stinhtrang = "Hoạt động";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
            //        {
            //            Stinhtrang = "Không hoạt động";
            //        }
            //        row[17] = Stinhtrang;

            //        if (dtResult.Rows[i]["Thunhap"].ToString() == "")
            //        {
            //            row[18] = 0;
            //        }
            //        else
            //        {
            //            row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
            //        }

            //        //string loaiKH = "";
            //        //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
            //        //{
            //        //    loaiKH = "Cá nhân";
            //        //}
            //        //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
            //        //{
            //        //    loaiKH = "Doanh nghiệp";
            //        //}
            //        //row[19] = loaiKH;
            //        row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
            //        row[20] = dtResult.Rows[i]["MaNV"].ToString();
            //        row[21] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[22] = ngayC + "/" + thangC + "/" + namC;
            //        row[23] = dtResult.Rows[i]["Noicap"].ToString();

            //        string ngayKethon, ngayKH, thangKH, namKH;
            //        ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

            //        if (ngayKethon.Length > 0)
            //        {
            //            ngayKH = ngayKethon.Substring(0, 2);
            //            thangKH = ngayKethon.Substring(3, 2);
            //            namKH = ngayKethon.Substring(6, 4);

            //            row[24] = ngayKH + "/" + thangKH + "/" + namKH;
            //        }
            //        else
            //        {
            //            row[24] = "";
            //        }

            //        row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
            //        row[26] = dtResult.Rows[i]["Ghichu"].ToString();
            //        row[27] = dtResult.Rows[i]["ten"].ToString();
            //        dtDanhsach.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvDanhsachCN.DataSource = dtDanhsach;
            //dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDanhsachCN.Columns[0].Width = 60;
            //dgvDanhsachCN.Columns[1].Width = 140;
            //dgvDanhsachCN.Columns[2].Width = 200;
            //dgvDanhsachCN.Columns[3].Width = 120;
            //dgvDanhsachCN.Columns[4].Width = 120;
            //dgvDanhsachCN.Columns[5].Width = 100;
            //dgvDanhsachCN.Columns[6].Width = 100;
            //dgvDanhsachCN.Columns[7].Width = 150;
            //dgvDanhsachCN.Columns[8].Width = 200;
            //dgvDanhsachCN.Columns[9].Width = 120;
            //dgvDanhsachCN.Columns[10].Width = 120;
            //dgvDanhsachCN.Columns[11].Width = 120;
            //dgvDanhsachCN.Columns[12].Width = 200;
            //dgvDanhsachCN.Columns[13].Width = 200;
            //dgvDanhsachCN.Columns[14].Width = 200;
            //dgvDanhsachCN.Columns[15].Width = 150;
            //dgvDanhsachCN.Columns[16].Width = 150;
            //dgvDanhsachCN.Columns[17].Width = 150;
            //dgvDanhsachCN.Columns[18].Width = 120;
            //dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            //dgvDanhsachCN.Columns[19].Width = 120;
            //dgvDanhsachCN.Columns[20].Width = 120;
            //dgvDanhsachCN.Columns[21].Width = 120;
            //dgvDanhsachCN.Columns[22].Width = 100;
            //dgvDanhsachCN.Columns[23].Width = 150;
            //dgvDanhsachCN.Columns[24].Width = 150;
            //dgvDanhsachCN.Columns[25].Width = 150;
            //dgvDanhsachCN.Columns[26].Width = 150;
            //Cursor.Current = Cursors.Default;
        }

        private void layDS_SNhomKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            
            //Cursor.Current = Cursors.WaitCursor;
            //dgvDanhsachCN.Refresh();
            //dtDanhsach = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));  //1
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tên KH", typeof(string)); //2
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("ĐT di động", typeof(string)); //3
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("ĐT nhà", typeof(string)); //4
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));  //5
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));  //6
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Nghề nghiệp", typeof(string));   //7
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Xã", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Huyện", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tỉnh", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //10
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Website", typeof(string));    //11
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("NH giao dịch", typeof(string));   //12
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Sở thích", typeof(string));   //13
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tình trạng", typeof(string)); //14
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Thu nhập", typeof(decimal));   //15
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Loại KH", typeof(string));    //16
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string));   //18
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));   //19
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));    //20
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày kết hôn", typeof(string));   //19
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Chi tiết", typeof(string));   //24
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ghi chú", typeof(string));    //25
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Nhóm KH", typeof(string));    //25
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Đối tượng KH", typeof(string));    //25
            //dtDanhsach.Columns.Add(col);

            //strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA, nhom.TENNHOM,doituongkh.ten ";
            //strCmd += " from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh left join DMTINH as tinh on kh.TINH=tinh.MaTinh ";
            //strCmd += " left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma";
            //strCmd += " left join KH_NHOMKH khnhom on kh.MAKH=khnhom.MAKH join NHOMKHACHHANG nhom on khnhom.MANHOM=nhom.MANHOM ";
            //strCmd += " Where kh.MACN='" + Thongtindangnhap.macn + "' and kh.LOAIKH='1' and nhom.MANHOM='" + cbbSNhomKH.SelectedValue.ToString() + "' ";
            //strCmd += " Order by kh.MaKH, kh.Hoten ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsach.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaKH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
            //        row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[5] = ngayS + "/" + thangS + "/" + namS;

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[6] = gioitinh;
            //        row[7] = dtResult.Rows[i]["Tennganh"].ToString();
            //        row[8] = dtResult.Rows[i]["Diachi1"].ToString();
            //        row[9] = dtResult.Rows[i]["TenXa"].ToString();
            //        row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
            //        row[11] = dtResult.Rows[i]["TenTinh"].ToString();
            //        row[12] = dtResult.Rows[i]["Diachi2"].ToString();
            //        row[13] = dtResult.Rows[i]["Email"].ToString();
            //        row[14] = dtResult.Rows[i]["Website"].ToString();
            //        row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
            //        row[16] = dtResult.Rows[i]["Sothich"].ToString();

            //        string Stinhtrang = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
            //        {
            //            Stinhtrang = "Hoạt động";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
            //        {
            //            Stinhtrang = "Không hoạt động";
            //        }
            //        row[17] = Stinhtrang;

            //        if (dtResult.Rows[i]["Thunhap"].ToString() == "")
            //        {
            //            row[18] = 0;
            //        }
            //        else
            //        {
            //            row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
            //        }

            //        //string loaiKH = "";
            //        //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
            //        //{
            //        //    loaiKH = "Cá nhân";
            //        //}
            //        //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
            //        //{
            //        //    loaiKH = "Doanh nghiệp";
            //        //}
            //        //row[19] = loaiKH;
            //        row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
            //        row[20] = dtResult.Rows[i]["MaNV"].ToString();
            //        row[21] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[22] = ngayC + "/" + thangC + "/" + namC;
            //        row[23] = dtResult.Rows[i]["Noicap"].ToString();

            //        string ngayKethon, ngayKH, thangKH, namKH;
            //        ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

            //        if (ngayKethon.Length > 0)
            //        {
            //            ngayKH = ngayKethon.Substring(0, 2);
            //            thangKH = ngayKethon.Substring(3, 2);
            //            namKH = ngayKethon.Substring(6, 4);

            //            row[24] = ngayKH + "/" + thangKH + "/" + namKH;
            //        }
            //        else
            //        {
            //            row[24] = "";
            //        }

            //        row[25] = dtResult.Rows[i]["CTLoaiKH"].ToString();
            //        row[26] = dtResult.Rows[i]["Ghichu"].ToString();
            //        row[27] = dtResult.Rows[i]["Tennhom"].ToString();
            //        row[28] = dtResult.Rows[i]["ten"].ToString();
            //        dtDanhsach.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvDanhsachCN.DataSource = dtDanhsach;
            //dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDanhsachCN.Columns[0].Width = 60;
            //dgvDanhsachCN.Columns[1].Width = 140;
            //dgvDanhsachCN.Columns[2].Width = 200;
            //dgvDanhsachCN.Columns[3].Width = 120;
            //dgvDanhsachCN.Columns[4].Width = 120;
            //dgvDanhsachCN.Columns[5].Width = 100;
            //dgvDanhsachCN.Columns[6].Width = 100;
            //dgvDanhsachCN.Columns[7].Width = 150;
            //dgvDanhsachCN.Columns[8].Width = 200;
            //dgvDanhsachCN.Columns[9].Width = 120;
            //dgvDanhsachCN.Columns[10].Width = 120;
            //dgvDanhsachCN.Columns[11].Width = 120;
            //dgvDanhsachCN.Columns[12].Width = 200;
            //dgvDanhsachCN.Columns[13].Width = 200;
            //dgvDanhsachCN.Columns[14].Width = 200;
            //dgvDanhsachCN.Columns[15].Width = 150;
            //dgvDanhsachCN.Columns[16].Width = 150;
            //dgvDanhsachCN.Columns[17].Width = 150;
            //dgvDanhsachCN.Columns[18].Width = 120;
            //dgvDanhsachCN.Columns[18].DefaultCellStyle.Format = "N0";
            //dgvDanhsachCN.Columns[19].Width = 120;
            //dgvDanhsachCN.Columns[20].Width = 120;
            //dgvDanhsachCN.Columns[21].Width = 120;
            //dgvDanhsachCN.Columns[22].Width = 100;
            //dgvDanhsachCN.Columns[23].Width = 150;
            //dgvDanhsachCN.Columns[24].Width = 150;
            //dgvDanhsachCN.Columns[25].Width = 150;
            //dgvDanhsachCN.Columns[26].Width = 150;
            //dgvDanhsachCN.Columns[27].Width = 100;
            //Cursor.Current = Cursors.Default;
        }

        private void layDSDN_TenKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            
            //Cursor.Current = Cursors.WaitCursor;
            //dgvDanhsachDN.Refresh();
            //dtDanhsachDN = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));  //1
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên KH", typeof(string)); //2
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT di động", typeof(string)); //3
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT nhà", typeof(string)); //4
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));  //5
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));  //6
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Lĩnh vực", typeof(string));   //7
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Xã", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Huyện", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tỉnh", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //10
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Website", typeof(string));    //11
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("NH giao dịch", typeof(string));   //12
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Sở thích", typeof(string));   //13
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tình trạng", typeof(string)); //14
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Thu nhập", typeof(decimal));   //15
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại KH", typeof(string));    //16
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string));   //18
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));    //20
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("QĐ thành lập", typeof(string));   //22
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("MST", typeof(string));    //23
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày thành lập", typeof(string));   //22
            //dtDanhsachDN.Columns.Add(col);

            //col = new DataColumn("Chi tiết", typeof(string));   //24
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ghi chú", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Đối tượng KH", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại hình DN", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày tl ngành", typeof(string));   //22
            //dtDanhsachDN.Columns.Add(col);

            //strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as loaihinhdn from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma left join doituongdn on kh.doituongdn=doituongdn.ma ";
            ////strCmd = "Select kh.*, lv.Tennganh from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + Thongtindangnhap.macn + "' and kh.Hoten like N'%" + txtDN_STen.Text.Trim() + "%' ";
            ////strCmd = "Select * from Khachhang Where LoaiKH='2' and Hoten like N'%" + txtDN_STen.Text.Trim() + "%' and macn='" + Thongtindangnhap.macn + "'";
            //strCmd += " Order by kh.Hoten, kh.MaKH ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsachDN.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaKH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
            //        row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[5] = ngayS + "/" + thangS + "/" + namS;

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[6] = gioitinh;
            //        row[7] = dtResult.Rows[i]["Tennganh"].ToString();
            //        row[8] = dtResult.Rows[i]["Diachi1"].ToString();
            //        row[9] = dtResult.Rows[i]["TenXa"].ToString();
            //        row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
            //        row[11] = dtResult.Rows[i]["TenTinh"].ToString();
            //        row[12] = dtResult.Rows[i]["Diachi2"].ToString();
            //        row[13] = dtResult.Rows[i]["Email"].ToString();
            //        row[14] = dtResult.Rows[i]["Website"].ToString();
            //        row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
            //        row[16] = dtResult.Rows[i]["Sothich"].ToString();

            //        string tinhtrang = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
            //        {
            //            tinhtrang = "Hoạt động";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
            //        {
            //            tinhtrang = "Không hoạt động";
            //        }
            //        row[17] = tinhtrang;

            //        if (dtResult.Rows[i]["Thunhap"].ToString() == "")
            //        {
            //            row[18] = 0;
            //        }
            //        else
            //        {
            //            row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
            //        }

            //        //string loaiKH = "";
            //        //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
            //        //{
            //        //    loaiKH = "Cá nhân";
            //        //}
            //        //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
            //        //{
            //        //    loaiKH = "Doanh nghiệp";
            //        //}
            //        //row[19] = loaiKH;
            //        row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
            //        row[20] = dtResult.Rows[i]["MaNV"].ToString();
            //        row[21] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[22] = ngayC + "/" + thangC + "/" + namC;

            //        row[23] = dtResult.Rows[i]["Noicap"].ToString();
            //        row[24] = dtResult.Rows[i]["GPDK"].ToString();
            //        row[25] = dtResult.Rows[i]["QDTL"].ToString();
            //        row[26] = dtResult.Rows[i]["MST"].ToString();

            //        string ngayThanhlap, ngayTL, thangTL, namTL;
            //        ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

            //        if (ngayThanhlap.Length > 0)
            //        {
            //            ngayTL = ngayThanhlap.Substring(0, 2);
            //            thangTL = ngayThanhlap.Substring(3, 2);
            //            namTL = ngayThanhlap.Substring(6, 4);

            //            row[27] = ngayTL + "/" + thangTL + "/" + namTL;
            //        }
            //        else
            //        {
            //            row[27] = "";
            //        }

            //        row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
            //        row[29] = dtResult.Rows[i]["Ghichu"].ToString();
            //        row[30] = dtResult.Rows[i]["tendtkh"].ToString();
            //        row[31] = dtResult.Rows[i]["loaihinhdn"].ToString();

            //        string ngayThanhlapN, ngayTLN, thangTLN, namTLN;
            //        ngayThanhlapN = dtResult.Rows[i]["Ngaytlnganh"].ToString();

            //        if (ngayThanhlapN.Length > 0)
            //        {
            //            ngayTLN = ngayThanhlapN.Substring(0, 2);
            //            thangTLN = ngayThanhlapN.Substring(3, 2);
            //            namTLN = ngayThanhlapN.Substring(6, 4);

            //            row[32] = ngayTLN + "/" + thangTLN + "/" + namTLN;
            //        }
            //        else
            //        {
            //            row[32] = "";
            //        }

            //        dtDanhsachDN.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvDanhsachDN.DataSource = dtDanhsachDN;
            //dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDanhsachDN.Columns[0].Width = 60;
            //dgvDanhsachDN.Columns[1].Width = 140;
            //dgvDanhsachDN.Columns[2].Width = 200;
            //dgvDanhsachDN.Columns[3].Width = 120;
            //dgvDanhsachDN.Columns[4].Width = 120;
            //dgvDanhsachDN.Columns[5].Width = 100;
            //dgvDanhsachDN.Columns[6].Width = 100;
            //dgvDanhsachDN.Columns[5].Visible = false;
            //dgvDanhsachDN.Columns[6].Visible = false;
            //dgvDanhsachDN.Columns[7].Width = 150;
            //dgvDanhsachDN.Columns[8].Width = 200;
            //dgvDanhsachDN.Columns[9].Width = 120;
            //dgvDanhsachDN.Columns[10].Width = 120;
            //dgvDanhsachDN.Columns[11].Width = 120;
            //dgvDanhsachDN.Columns[12].Width = 200;
            //dgvDanhsachDN.Columns[13].Width = 200;
            //dgvDanhsachDN.Columns[14].Width = 200;
            //dgvDanhsachDN.Columns[15].Width = 150;
            //dgvDanhsachDN.Columns[16].Width = 150;
            //dgvDanhsachDN.Columns[16].Visible = false;
            //dgvDanhsachDN.Columns[17].Width = 150;
            //dgvDanhsachDN.Columns[18].Width = 120;
            //dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            //dgvDanhsachDN.Columns[18].Visible = false;
            //dgvDanhsachDN.Columns[19].Width = 120;
            //dgvDanhsachDN.Columns[20].Width = 120;
            //dgvDanhsachDN.Columns[21].Width = 120;
            //dgvDanhsachDN.Columns[22].Width = 100;
            //dgvDanhsachDN.Columns[23].Width = 150;
            //dgvDanhsachDN.Columns[21].Visible = false;
            //dgvDanhsachDN.Columns[22].Visible = false;
            //dgvDanhsachDN.Columns[23].Visible = false;
            //dgvDanhsachDN.Columns[24].Width = 150;
            //dgvDanhsachDN.Columns[25].Width = 150;
            //dgvDanhsachDN.Columns[26].Width = 150;
            //dgvDanhsachDN.Columns[27].Width = 150;
            //dgvDanhsachDN.Columns[28].Width = 150;
            //dgvDanhsachDN.Columns[29].Width = 150;
            //Cursor.Current = Cursors.Default;
        }

        private void layDSDN_MaKH()
        {
            //dgvDanhsach.Visible = true;
            //dgvDanhsachDN.Visible = false;
            
            //Cursor.Current = Cursors.WaitCursor;
            //dgvDanhsachDN.Refresh();
            //dtDanhsachDN = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));  //1
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên KH", typeof(string)); //2
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT di động", typeof(string)); //3
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT nhà", typeof(string)); //4
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));  //5
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));  //6
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Lĩnh vực", typeof(string));   //7
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Xã", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Huyện", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tỉnh", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //10
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Website", typeof(string));    //11
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("NH giao dịch", typeof(string));   //12
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Sở thích", typeof(string));   //13
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tình trạng", typeof(string)); //14
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Thu nhập", typeof(decimal));   //15
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại KH", typeof(string));    //16
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string));   //18
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));    //20
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("QĐ thành lập", typeof(string));   //22
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("MST", typeof(string));    //23
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày thành lập", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày tl ngành", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Chi tiết", typeof(string));   //24
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ghi chú", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Đối tượng KH", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại hình DN", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);

            //strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as loaihinhdn from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma left join doituongdn on kh.doituongdn=doituongdn.ma ";
            ////strCmd = "Select kh.*, lv.Tennganh from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + Thongtindangnhap.macn + "' and kh.MaKH like '%" + txtDN_SMaKH.Text.Trim() + "%' ";
            ////strCmd = "Select * from Khachhang Where LoaiKH='2' and MaKH like '%" + txtDN_SMaKH.Text.Trim() + "%' and macn='" + Thongtindangnhap.macn + "'";
            //strCmd += " Order by kh.MaKH, kh.Hoten ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{

            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsachDN.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaKH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
            //        row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[5] = ngayS + "/" + thangS + "/" + namS;

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[6] = gioitinh;
            //        row[7] = dtResult.Rows[i]["Tennganh"].ToString();
            //        row[8] = dtResult.Rows[i]["Diachi1"].ToString();
            //        row[9] = dtResult.Rows[i]["TenXa"].ToString();
            //        row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
            //        row[11] = dtResult.Rows[i]["TenTinh"].ToString();
            //        row[12] = dtResult.Rows[i]["Diachi2"].ToString();
            //        row[13] = dtResult.Rows[i]["Email"].ToString();
            //        row[14] = dtResult.Rows[i]["Website"].ToString();
            //        row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
            //        row[16] = dtResult.Rows[i]["Sothich"].ToString();

            //        string tinhtrang = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
            //        {
            //            tinhtrang = "Hoạt động";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
            //        {
            //            tinhtrang = "Không hoạt động";
            //        }
            //        row[17] = tinhtrang;

            //        if (dtResult.Rows[i]["Thunhap"].ToString() == "")
            //        {
            //            row[18] = 0;
            //        }
            //        else
            //        {
            //            row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
            //        }

            //        //string loaiKH = "";
            //        //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
            //        //{
            //        //    loaiKH = "Cá nhân";
            //        //}
            //        //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
            //        //{
            //        //    loaiKH = "Doanh nghiệp";
            //        //}
            //        //row[19] = loaiKH;
            //        row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
            //        row[20] = dtResult.Rows[i]["MaNV"].ToString();
            //        row[21] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[22] = ngayC + "/" + thangC + "/" + namC;

            //        row[23] = dtResult.Rows[i]["Noicap"].ToString();
            //        row[24] = dtResult.Rows[i]["GPDK"].ToString();
            //        row[25] = dtResult.Rows[i]["QDTL"].ToString();
            //        row[26] = dtResult.Rows[i]["MST"].ToString();

            //        string ngayThanhlap, ngayTL, thangTL, namTL;
            //        ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

            //        if (ngayThanhlap.Length > 0)
            //        {
            //            ngayTL = ngayThanhlap.Substring(0, 2);
            //            thangTL = ngayThanhlap.Substring(3, 2);
            //            namTL = ngayThanhlap.Substring(6, 4);

            //            row[27] = ngayTL + "/" + thangTL + "/" + namTL;
            //        }
            //        else
            //        {
            //            row[27] = "";
            //        }

            //        string ngayThanhlapN, ngayTLN, thangTLN, namTLN;
            //        ngayThanhlapN = dtResult.Rows[i]["Ngaytlnganh"].ToString();

            //        if (ngayThanhlapN.Length > 0)
            //        {
            //            ngayTLN = ngayThanhlapN.Substring(0, 2);
            //            thangTLN = ngayThanhlapN.Substring(3, 2);
            //            namTLN = ngayThanhlapN.Substring(6, 4);

            //            row[28] = ngayTLN + "/" + thangTLN + "/" + namTLN;
            //        }
            //        else
            //        {
            //            row[28] = "";
            //        }

            //        row[29] = dtResult.Rows[i]["CTLoaiKH"].ToString();
            //        row[30] = dtResult.Rows[i]["Ghichu"].ToString();
            //        row[31] = dtResult.Rows[i]["tendtkh"].ToString();
            //        row[32] = dtResult.Rows[i]["loaihinhdn"].ToString();



            //        dtDanhsachDN.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvDanhsachDN.DataSource = dtDanhsachDN;
            //dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDanhsachDN.Columns[0].Width = 60;
            //dgvDanhsachDN.Columns[1].Width = 140;
            //dgvDanhsachDN.Columns[2].Width = 200;
            //dgvDanhsachDN.Columns[3].Width = 120;
            //dgvDanhsachDN.Columns[4].Width = 120;
            //dgvDanhsachDN.Columns[5].Width = 100;
            //dgvDanhsachDN.Columns[6].Width = 100;
            //dgvDanhsachDN.Columns[5].Visible = false;
            //dgvDanhsachDN.Columns[6].Visible = false;
            //dgvDanhsachDN.Columns[7].Width = 150;
            //dgvDanhsachDN.Columns[8].Width = 200;
            //dgvDanhsachDN.Columns[9].Width = 120;
            //dgvDanhsachDN.Columns[10].Width = 120;
            //dgvDanhsachDN.Columns[11].Width = 120;
            //dgvDanhsachDN.Columns[12].Width = 200;
            //dgvDanhsachDN.Columns[13].Width = 200;
            //dgvDanhsachDN.Columns[14].Width = 200;
            //dgvDanhsachDN.Columns[15].Width = 150;
            //dgvDanhsachDN.Columns[16].Width = 150;
            //dgvDanhsachDN.Columns[16].Visible = false;
            //dgvDanhsachDN.Columns[17].Width = 150;
            //dgvDanhsachDN.Columns[18].Width = 120;
            //dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            //dgvDanhsachDN.Columns[18].Visible = false;
            //dgvDanhsachDN.Columns[19].Width = 120;
            //dgvDanhsachDN.Columns[20].Width = 120;
            //dgvDanhsachDN.Columns[21].Width = 120;
            //dgvDanhsachDN.Columns[22].Width = 100;
            //dgvDanhsachDN.Columns[23].Width = 150;
            //dgvDanhsachDN.Columns[21].Visible = false;
            //dgvDanhsachDN.Columns[22].Visible = false;
            //dgvDanhsachDN.Columns[23].Visible = false;
            //dgvDanhsachDN.Columns[24].Width = 150;
            //dgvDanhsachDN.Columns[25].Width = 150;
            //dgvDanhsachDN.Columns[26].Width = 150;
            //dgvDanhsachDN.Columns[27].Width = 150;
            //dgvDanhsachDN.Columns[28].Width = 150;
            //dgvDanhsachDN.Columns[29].Width = 150;
            //Cursor.Current = Cursors.Default;
        }

        private void layDSDN_Dienthoai()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dgvDanhsachDN.Refresh();
            //dtDanhsachDN = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));  //1
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên KH", typeof(string)); //2
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT di động", typeof(string)); //3
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT nhà", typeof(string)); //4
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));  //5
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));  //6
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Lĩnh vực", typeof(string));   //7
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Xã", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Huyện", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tỉnh", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //10
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Website", typeof(string));    //11
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("NH giao dịch", typeof(string));   //12
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Sở thích", typeof(string));   //13
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tình trạng", typeof(string)); //14
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Thu nhập", typeof(decimal));   //15
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại KH", typeof(string));    //16
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string));   //18
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));    //20
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("QĐ thành lập", typeof(string));   //22
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("MST", typeof(string));    //23
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày thành lập", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày tl ngành", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Chi tiết", typeof(string));   //24
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ghi chú", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Đối tượng KH", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại hình DN", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);

            //strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as loaihinhdn from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma left join doituongdn on kh.doituongdn=doituongdn.ma ";
            ////strCmd = "Select kh.*, lv.Tennganh from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + Thongtindangnhap.macn + "' and (kh.Dienthoai1 like '%" + txtDN_STel.Text.Trim() + "%' or kh.Dienthoai2 like '%" + txtDN_STel.Text.Trim() + "%') ";
            //strCmd += " Order by kh.MaKH, kh.Hoten ";
            ////strCmd = "Select * from Khachhang Where LoaiKH='2' and (Dienthoai1 like '%" + txtDN_STel.Text.Trim() + "%' or Dienthoai2 like '%" + txtDN_STel.Text.Trim() + "%') and macn='" + Thongtindangnhap.macn + "' ";
            ////strCmd += " Order by MaKH, Hoten ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsachDN.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaKH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
            //        row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[5] = ngayS + "/" + thangS + "/" + namS;

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[6] = gioitinh;
            //        row[7] = dtResult.Rows[i]["Tennganh"].ToString();
            //        row[8] = dtResult.Rows[i]["Diachi1"].ToString();
            //        row[9] = dtResult.Rows[i]["TenXa"].ToString();
            //        row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
            //        row[11] = dtResult.Rows[i]["TenTinh"].ToString();
            //        row[12] = dtResult.Rows[i]["Diachi2"].ToString();
            //        row[13] = dtResult.Rows[i]["Email"].ToString();
            //        row[14] = dtResult.Rows[i]["Website"].ToString();
            //        row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
            //        row[16] = dtResult.Rows[i]["Sothich"].ToString();

            //        string tinhtrang = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
            //        {
            //            tinhtrang = "Hoạt động";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
            //        {
            //            tinhtrang = "Không hoạt động";
            //        }
            //        row[17] = tinhtrang;

            //        if (dtResult.Rows[i]["Thunhap"].ToString() == "")
            //        {
            //            row[18] = 0;
            //        }
            //        else
            //        {
            //            row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
            //        }

            //        //string loaiKH = "";
            //        //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
            //        //{
            //        //    loaiKH = "Cá nhân";
            //        //}
            //        //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
            //        //{
            //        //    loaiKH = "Doanh nghiệp";
            //        //}
            //        //row[19] = loaiKH;
            //        row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
            //        row[20] = dtResult.Rows[i]["MaNV"].ToString();
            //        row[21] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[22] = ngayC + "/" + thangC + "/" + namC;

            //        row[23] = dtResult.Rows[i]["Noicap"].ToString();
            //        row[24] = dtResult.Rows[i]["GPDK"].ToString();
            //        row[25] = dtResult.Rows[i]["QDTL"].ToString();
            //        row[26] = dtResult.Rows[i]["MST"].ToString();

            //        string ngayThanhlap, ngayTL, thangTL, namTL;
            //        ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

            //        if (ngayThanhlap.Length > 0)
            //        {
            //            ngayTL = ngayThanhlap.Substring(0, 2);
            //            thangTL = ngayThanhlap.Substring(3, 2);
            //            namTL = ngayThanhlap.Substring(6, 4);

            //            row[27] = ngayTL + "/" + thangTL + "/" + namTL;
            //        }
            //        else
            //        {
            //            row[27] = "";
            //        }

            //        row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
            //        row[29] = dtResult.Rows[i]["Ghichu"].ToString();
            //        row[30] = dtResult.Rows[i]["tendtkh"].ToString();
            //        row[31] = dtResult.Rows[i]["loaihinhdn"].ToString();
            //        string ngayThanhlapN, ngayTLN, thangTLN, namTLN;
            //        ngayThanhlapN = dtResult.Rows[i]["Ngaytlnganh"].ToString();

            //        if (ngayThanhlapN.Length > 0)
            //        {
            //            ngayTLN = ngayThanhlapN.Substring(0, 2);
            //            thangTLN = ngayThanhlapN.Substring(3, 2);
            //            namTLN = ngayThanhlapN.Substring(6, 4);

            //            row[32] = ngayTLN + "/" + thangTLN + "/" + namTLN;
            //        }
            //        else
            //        {
            //            row[32] = "";
            //        }
            //        dtDanhsachDN.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvDanhsachDN.DataSource = dtDanhsachDN;
            //dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDanhsachDN.Columns[0].Width = 60;
            //dgvDanhsachDN.Columns[1].Width = 140;
            //dgvDanhsachDN.Columns[2].Width = 200;
            //dgvDanhsachDN.Columns[3].Width = 120;
            //dgvDanhsachDN.Columns[4].Width = 120;
            //dgvDanhsachDN.Columns[5].Width = 100;
            //dgvDanhsachDN.Columns[6].Width = 100;
            //dgvDanhsachDN.Columns[5].Visible = false;
            //dgvDanhsachDN.Columns[6].Visible = false;
            //dgvDanhsachDN.Columns[7].Width = 150;
            //dgvDanhsachDN.Columns[8].Width = 200;
            //dgvDanhsachDN.Columns[9].Width = 120;
            //dgvDanhsachDN.Columns[10].Width = 120;
            //dgvDanhsachDN.Columns[11].Width = 120;
            //dgvDanhsachDN.Columns[12].Width = 200;
            //dgvDanhsachDN.Columns[13].Width = 200;
            //dgvDanhsachDN.Columns[14].Width = 200;
            //dgvDanhsachDN.Columns[15].Width = 150;
            //dgvDanhsachDN.Columns[16].Width = 150;
            //dgvDanhsachDN.Columns[16].Visible = false;
            //dgvDanhsachDN.Columns[17].Width = 150;
            //dgvDanhsachDN.Columns[18].Width = 120;
            //dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            //dgvDanhsachDN.Columns[18].Visible = false;
            //dgvDanhsachDN.Columns[19].Width = 120;
            //dgvDanhsachDN.Columns[20].Width = 120;
            //dgvDanhsachDN.Columns[21].Width = 120;
            //dgvDanhsachDN.Columns[22].Width = 100;
            //dgvDanhsachDN.Columns[23].Width = 150;
            //dgvDanhsachDN.Columns[21].Visible = false;
            //dgvDanhsachDN.Columns[22].Visible = false;
            //dgvDanhsachDN.Columns[23].Visible = false;
            //dgvDanhsachDN.Columns[24].Width = 150;
            //dgvDanhsachDN.Columns[25].Width = 150;
            //dgvDanhsachDN.Columns[26].Width = 150;
            //dgvDanhsachDN.Columns[27].Width = 150;
            //dgvDanhsachDN.Columns[28].Width = 150;
            //dgvDanhsachDN.Columns[29].Width = 150;
            //Cursor.Current = Cursors.Default;
        }

        private void layDSDN_Tinhtrang()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dgvDanhsachDN.Refresh();
            //dtDanhsachDN = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));  //1
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên KH", typeof(string)); //2
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT di động", typeof(string)); //3
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT nhà", typeof(string)); //4
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));  //5
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));  //6
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Lĩnh vực", typeof(string));   //7
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Xã", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Huyện", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tỉnh", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //10
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Website", typeof(string));    //11
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("NH giao dịch", typeof(string));   //12
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Sở thích", typeof(string));   //13
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tình trạng", typeof(string)); //14
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Thu nhập", typeof(decimal));   //15
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại KH", typeof(string));    //16
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string));   //18
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));    //20
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("QĐ thành lập", typeof(string));   //22
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("MST", typeof(string));    //23
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày thành lập", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày tl ngành", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Chi tiết", typeof(string));   //24
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ghi chú", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Đối tượng KH", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại hình DN", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //int tinhtrang = 1;
            //if (cbbDN_STinhtrang.Text == "Hoạt động")
            //{
            //    tinhtrang = 1;
            //}
            //else if (cbbDN_STinhtrang.Text == "Không hoạt động")
            //{
            //    tinhtrang = 0;
            //}

            //strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as loaihinhdn from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma left join doituongdn on kh.doituongdn=doituongdn.ma ";
            ////strCmd = "Select kh.*, lv.Tennganh from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            //strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + Thongtindangnhap.macn + "' and kh.Tinhtrang ='" + tinhtrang + "' ";
            //strCmd += " Order by kh.MaKH, kh.Hoten ";
            ////strCmd = "Select * from Khachhang Where LoaiKH='2' and Tinhtrang ='" + tinhtrang + "' and macn='" + Thongtindangnhap.macn + "' ";
            ////strCmd += " Order by MaKH, Hoten ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsachDN.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaKH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
            //        row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[5] = ngayS + "/" + thangS + "/" + namS;

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[6] = gioitinh;
            //        row[7] = dtResult.Rows[i]["Tennganh"].ToString();
            //        row[8] = dtResult.Rows[i]["Diachi1"].ToString();
            //        row[9] = dtResult.Rows[i]["TenXa"].ToString();
            //        row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
            //        row[11] = dtResult.Rows[i]["TenTinh"].ToString();
            //        row[12] = dtResult.Rows[i]["Diachi2"].ToString();
            //        row[13] = dtResult.Rows[i]["Email"].ToString();
            //        row[14] = dtResult.Rows[i]["Website"].ToString();
            //        row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
            //        row[16] = dtResult.Rows[i]["Sothich"].ToString();

            //        string Stinhtrang = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
            //        {
            //            Stinhtrang = "Hoạt động";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
            //        {
            //            Stinhtrang = "Không hoạt động";
            //        }
            //        row[17] = Stinhtrang;

            //        if (dtResult.Rows[i]["Thunhap"].ToString() == "")
            //        {
            //            row[18] = 0;
            //        }
            //        else
            //        {
            //            row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
            //        }

            //        //string loaiKH = "";
            //        //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
            //        //{
            //        //    loaiKH = "Cá nhân";
            //        //}
            //        //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
            //        //{
            //        //    loaiKH = "Doanh nghiệp";
            //        //}
            //        //row[19] = loaiKH;
            //        row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
            //        row[20] = dtResult.Rows[i]["MaNV"].ToString();
            //        row[21] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[22] = ngayC + "/" + thangC + "/" + namC;

            //        row[23] = dtResult.Rows[i]["Noicap"].ToString();
            //        row[24] = dtResult.Rows[i]["GPDK"].ToString();
            //        row[25] = dtResult.Rows[i]["QDTL"].ToString();
            //        row[26] = dtResult.Rows[i]["MST"].ToString();

            //        string ngayThanhlap, ngayTL, thangTL, namTL;
            //        ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

            //        if (ngayThanhlap.Length > 0)
            //        {
            //            ngayTL = ngayThanhlap.Substring(0, 2);
            //            thangTL = ngayThanhlap.Substring(3, 2);
            //            namTL = ngayThanhlap.Substring(6, 4);

            //            row[27] = ngayTL + "/" + thangTL + "/" + namTL;
            //        }
            //        else
            //        {
            //            row[27] = "";
            //        }

            //        row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
            //        row[29] = dtResult.Rows[i]["Ghichu"].ToString();
            //        row[30] = dtResult.Rows[i]["tendtkh"].ToString();
            //        row[31] = dtResult.Rows[i]["loaihinhdn"].ToString();
            //        string ngayThanhlapN, ngayTLN, thangTLN, namTLN;
            //        ngayThanhlapN = dtResult.Rows[i]["Ngaytlnganh"].ToString();

            //        if (ngayThanhlapN.Length > 0)
            //        {
            //            ngayTLN = ngayThanhlapN.Substring(0, 2);
            //            thangTLN = ngayThanhlapN.Substring(3, 2);
            //            namTLN = ngayThanhlapN.Substring(6, 4);

            //            row[32] = ngayTLN + "/" + thangTLN + "/" + namTLN;
            //        }
            //        else
            //        {
            //            row[32] = "";
            //        }
            //        dtDanhsachDN.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvDanhsachDN.DataSource = dtDanhsachDN;
            //dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDanhsachDN.Columns[0].Width = 60;
            //dgvDanhsachDN.Columns[1].Width = 140;
            //dgvDanhsachDN.Columns[2].Width = 200;
            //dgvDanhsachDN.Columns[3].Width = 120;
            //dgvDanhsachDN.Columns[4].Width = 120;
            //dgvDanhsachDN.Columns[5].Width = 100;
            //dgvDanhsachDN.Columns[6].Width = 100;
            //dgvDanhsachDN.Columns[5].Visible = false;
            //dgvDanhsachDN.Columns[6].Visible = false;
            //dgvDanhsachDN.Columns[7].Width = 150;
            //dgvDanhsachDN.Columns[8].Width = 200;
            //dgvDanhsachDN.Columns[9].Width = 120;
            //dgvDanhsachDN.Columns[10].Width = 120;
            //dgvDanhsachDN.Columns[11].Width = 120;
            //dgvDanhsachDN.Columns[12].Width = 200;
            //dgvDanhsachDN.Columns[13].Width = 200;
            //dgvDanhsachDN.Columns[14].Width = 200;
            //dgvDanhsachDN.Columns[15].Width = 150;
            //dgvDanhsachDN.Columns[16].Width = 150;
            //dgvDanhsachDN.Columns[16].Visible = false;
            //dgvDanhsachDN.Columns[17].Width = 150;
            //dgvDanhsachDN.Columns[18].Width = 120;
            //dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            //dgvDanhsachDN.Columns[18].Visible = false;
            //dgvDanhsachDN.Columns[19].Width = 120;
            //dgvDanhsachDN.Columns[20].Width = 120;
            //dgvDanhsachDN.Columns[21].Width = 120;
            //dgvDanhsachDN.Columns[22].Width = 100;
            //dgvDanhsachDN.Columns[23].Width = 150;
            //dgvDanhsachDN.Columns[21].Visible = false;
            //dgvDanhsachDN.Columns[22].Visible = false;
            //dgvDanhsachDN.Columns[23].Visible = false;
            //dgvDanhsachDN.Columns[24].Width = 150;
            //dgvDanhsachDN.Columns[25].Width = 150;
            //dgvDanhsachDN.Columns[26].Width = 150;
            //dgvDanhsachDN.Columns[27].Width = 150;
            //dgvDanhsachDN.Columns[28].Width = 150;
            //dgvDanhsachDN.Columns[29].Width = 150;
            //Cursor.Current = Cursors.Default;
        }

        private void layDSDN_SNhomKH()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dgvDanhsachDN.Refresh();
            //dtDanhsachDN = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));  //1
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên KH", typeof(string)); //2
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT di động", typeof(string)); //3
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("ĐT nhà", typeof(string)); //4
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));  //5
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));  //6
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Lĩnh vực", typeof(string));   //7
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Xã", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Huyện", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tỉnh", typeof(string));    //8
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //10
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Website", typeof(string));    //11
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("NH giao dịch", typeof(string));   //12
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Sở thích", typeof(string));   //13
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tình trạng", typeof(string)); //14
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Thu nhập", typeof(decimal));   //15
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại KH", typeof(string));    //16
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Tên đ.nhập", typeof(string));  //17
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string));   //18
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));    //20
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("QĐ thành lập", typeof(string));   //22
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("MST", typeof(string));    //23
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày thành lập", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ngày tl ngành", typeof(string));   //19
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Chi tiết", typeof(string));   //24
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Ghi chú", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Nhóm KH", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Đối tượng KH", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);
            //col = new DataColumn("Loại hình DN", typeof(string));    //25
            //dtDanhsachDN.Columns.Add(col);

            //strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA,doituongkh.ten as tendtkh,doituongdn.ten as loaihinhdn,nhom.TENNHOM  ";
            //strCmd += " from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh left join DMTINH as tinh on kh.TINH=tinh.MaTinh ";
            //strCmd += " left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA left join doituongkh on kh.doituongkh=doituongkh.ma left join doituongdn on kh.doituongdn=doituongdn.ma ";
            //strCmd += " left join KH_NHOMKH khnhom on kh.MAKH=khnhom.MAKH join NHOMKHACHHANG nhom on khnhom.MANHOM=nhom.MANHOM ";
            //strCmd += " Where kh.MACN='" + Thongtindangnhap.macn + "' and kh.LOAIKH='2' and nhom.MANHOM='" + cbbDN_SNhomKH.SelectedValue.ToString() + "' ";
            //strCmd += " Order by kh.MaKH, kh.Hoten ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsachDN.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaKH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
            //        row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[5] = ngayS + "/" + thangS + "/" + namS;

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[6] = gioitinh;
            //        row[7] = dtResult.Rows[i]["Tennganh"].ToString();
            //        row[8] = dtResult.Rows[i]["Diachi1"].ToString();
            //        row[9] = dtResult.Rows[i]["TenXa"].ToString();
            //        row[10] = dtResult.Rows[i]["TenHuyen"].ToString();
            //        row[11] = dtResult.Rows[i]["TenTinh"].ToString();
            //        row[12] = dtResult.Rows[i]["Diachi2"].ToString();
            //        row[13] = dtResult.Rows[i]["Email"].ToString();
            //        row[14] = dtResult.Rows[i]["Website"].ToString();
            //        row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
            //        row[16] = dtResult.Rows[i]["Sothich"].ToString();

            //        string Stinhtrang = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
            //        {
            //            Stinhtrang = "Hoạt động";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
            //        {
            //            Stinhtrang = "Không hoạt động";
            //        }
            //        row[17] = Stinhtrang;

            //        if (dtResult.Rows[i]["Thunhap"].ToString() == "")
            //        {
            //            row[18] = 0;
            //        }
            //        else
            //        {
            //            row[18] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
            //        }

            //        //string loaiKH = "";
            //        //if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
            //        //{
            //        //    loaiKH = "Cá nhân";
            //        //}
            //        //else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
            //        //{
            //        //    loaiKH = "Doanh nghiệp";
            //        //}
            //        //row[19] = loaiKH;
            //        row[19] = dtResult.Rows[i]["LoaiKH_IPCAS"].ToString();
            //        row[20] = dtResult.Rows[i]["MaNV"].ToString();
            //        row[21] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[22] = ngayC + "/" + thangC + "/" + namC;

            //        row[23] = dtResult.Rows[i]["Noicap"].ToString();
            //        row[24] = dtResult.Rows[i]["GPDK"].ToString();
            //        row[25] = dtResult.Rows[i]["QDTL"].ToString();
            //        row[26] = dtResult.Rows[i]["MST"].ToString();

            //        string ngayThanhlap, ngayTL, thangTL, namTL;
            //        ngayThanhlap = dtResult.Rows[i]["Ngaythanhlap"].ToString();

            //        if (ngayThanhlap.Length > 0)
            //        {
            //            ngayTL = ngayThanhlap.Substring(0, 2);
            //            thangTL = ngayThanhlap.Substring(3, 2);
            //            namTL = ngayThanhlap.Substring(6, 4);

            //            row[27] = ngayTL + "/" + thangTL + "/" + namTL;
            //        }
            //        else
            //        {
            //            row[27] = "";
            //        }

            //        row[28] = dtResult.Rows[i]["CTLoaiKH"].ToString();
            //        row[29] = dtResult.Rows[i]["Ghichu"].ToString();
            //        row[30] = dtResult.Rows[i]["Tennhom"].ToString();
            //        row[31] = dtResult.Rows[i]["tendtkh"].ToString();
            //        row[32] = dtResult.Rows[i]["loaihinhdn"].ToString();
            //        string ngayThanhlapN, ngayTLN, thangTLN, namTLN;
            //        ngayThanhlapN = dtResult.Rows[i]["Ngaytlnganh"].ToString();

            //        if (ngayThanhlapN.Length > 0)
            //        {
            //            ngayTLN = ngayThanhlapN.Substring(0, 2);
            //            thangTLN = ngayThanhlapN.Substring(3, 2);
            //            namTLN = ngayThanhlapN.Substring(6, 4);

            //            row[33] = ngayTLN + "/" + thangTLN + "/" + namTLN;
            //        }
            //        else
            //        {
            //            row[33] = "";
            //        }
            //        dtDanhsachDN.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvDanhsachDN.DataSource = dtDanhsachDN;
            //dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDanhsachDN.Columns[0].Width = 60;
            //dgvDanhsachDN.Columns[1].Width = 140;
            //dgvDanhsachDN.Columns[2].Width = 200;
            //dgvDanhsachDN.Columns[3].Width = 120;
            //dgvDanhsachDN.Columns[4].Width = 120;
            //dgvDanhsachDN.Columns[5].Width = 100;
            //dgvDanhsachDN.Columns[6].Width = 100;
            //dgvDanhsachDN.Columns[5].Visible = false;
            //dgvDanhsachDN.Columns[6].Visible = false;
            //dgvDanhsachDN.Columns[7].Width = 150;
            //dgvDanhsachDN.Columns[8].Width = 200;
            //dgvDanhsachDN.Columns[9].Width = 120;
            //dgvDanhsachDN.Columns[10].Width = 120;
            //dgvDanhsachDN.Columns[11].Width = 120;
            //dgvDanhsachDN.Columns[12].Width = 200;
            //dgvDanhsachDN.Columns[13].Width = 200;
            //dgvDanhsachDN.Columns[14].Width = 200;
            //dgvDanhsachDN.Columns[15].Width = 150;
            //dgvDanhsachDN.Columns[16].Width = 150;
            //dgvDanhsachDN.Columns[16].Visible = false;
            //dgvDanhsachDN.Columns[17].Width = 150;
            //dgvDanhsachDN.Columns[18].Width = 120;
            //dgvDanhsachDN.Columns[18].DefaultCellStyle.Format = "N0";
            //dgvDanhsachDN.Columns[18].Visible = false;
            //dgvDanhsachDN.Columns[19].Width = 120;
            //dgvDanhsachDN.Columns[20].Width = 120;
            //dgvDanhsachDN.Columns[21].Width = 120;
            //dgvDanhsachDN.Columns[22].Width = 100;
            //dgvDanhsachDN.Columns[23].Width = 150;
            //dgvDanhsachDN.Columns[21].Visible = false;
            //dgvDanhsachDN.Columns[22].Visible = false;
            //dgvDanhsachDN.Columns[23].Visible = false;
            //dgvDanhsachDN.Columns[24].Width = 150;
            //dgvDanhsachDN.Columns[25].Width = 150;
            //dgvDanhsachDN.Columns[26].Width = 150;
            //dgvDanhsachDN.Columns[27].Width = 150;
            //dgvDanhsachDN.Columns[28].Width = 150;
            //dgvDanhsachDN.Columns[29].Width = 150;
            //dgvDanhsachDN.Columns[30].Width = 100;
            //Cursor.Current = Cursors.Default;
        }

        private void layDS_Lienhe(string s)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvLienhe.Refresh();
            dtDanhsach = new System.Data.DataTable();
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
            col = new DataColumn("Có con nhỏ", typeof(string));   //12
            dtDanhsach.Columns.Add(col);


            strCmd = "Select * from NGUOILIENHE Where MaKH = '" + s + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                    string connho = "";
                    if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
                    {
                        connho = "Có";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
                    {
                        connho = "Không";
                    }
                    row[13] = connho;
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
            strCmd = "Select kh.*, lv.Tennganh, tinh.TenTinh, huyen.TenHuyen, xa.TENXA from Khachhang as kh left join Nganhnghe as lv on kh.LINHVUC=lv.Manganh ";
            strCmd += " left join DMTINH as tinh on kh.TINH=tinh.MaTinh left join DMHUYEN as huyen on kh.HUYEN=huyen.MaHuyen left join DMXAPHUONG as xa on kh.XA=xa.MAXA ";
            strCmd += " Where kh.LOAIKH='2' and kh.MACN='" + Thongtindangnhap.macn + "' and kh.MaKH like '%" + s + "%' ";
            strCmd += " Order by kh.MaKH, kh.Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            txtDN_MaKH.Text = dtResult.Rows[0]["MaKH"].ToString();
            txtDN_TenKH.Text = dtResult.Rows[0]["Hoten"].ToString();
            txtDN_Mobile.Text = dtResult.Rows[0]["Dienthoai1"].ToString();
            //txtDN_Tel.Text = dtResult.Rows[0]["Dienthoai2"].ToString();
            //cbbDN_Linhvuc.Text = dtResult.Rows[0]["Tennganh"].ToString();
            //txtDN_Address.Text = dtResult.Rows[0]["Diachi1"].ToString();
            //cbbDN_Xa.Text = dtResult.Rows[0]["TenXa"].ToString();
            //cbbDN_Huyen.Text = dtResult.Rows[0]["TenHuyen"].ToString();
            //cbbDN_Tinh.Text = dtResult.Rows[0]["TenTinh"].ToString();
            //txtDN_Address2.Text = dtResult.Rows[0]["Diachi2"].ToString();
            //txtDN_Email.Text = dtResult.Rows[0]["Email"].ToString();
            //txtDN_Website.Text = dtResult.Rows[0]["Website"].ToString();
            //txtDN_NHGD.Text = dtResult.Rows[0]["NHGiaodich"].ToString();

            string tinhtrang = "Hoạt động";
            if (Boolean.Parse(dtResult.Rows[0]["Tinhtrang"].ToString()) == true)
            {
                tinhtrang = "Hoạt động";
            }
            else if (Boolean.Parse(dtResult.Rows[0]["Tinhtrang"].ToString()) == false)
            {
                tinhtrang = "Không hoạt động";
            }
            //cbbDN_Tinhtrang.Text = tinhtrang;
            //txtDN_MaNV.Text = dtResult.Rows[0]["MaNV"].ToString();
            //txtDN_GPDK.Text = dtResult.Rows[0]["GPDK"].ToString();
            //txtDN_QDTL.Text = dtResult.Rows[0]["QDTL"].ToString();
            //txtDN_MST.Text = dtResult.Rows[0]["MST"].ToString();

            string ngayThanhlap, ngayTL, thangTL, namTL;
            ngayThanhlap = dtResult.Rows[0]["Ngaythanhlap"].ToString();

            ngayTL = ngayThanhlap.Substring(0, 2);
            thangTL = ngayThanhlap.Substring(3, 2);
            namTL = ngayThanhlap.Substring(6, 4);

            //dtpDN_NgayTL.Text = ngayTL + "/" + thangTL + "/" + namTL;
            //txtDN_Chitiet.Text = dtResult.Rows[0]["CTLoaiKH"].ToString();
            //txtDN_Ghichu.Text = dtResult.Rows[0]["Ghichu"].ToString();
        }

        private void layDS_TenNLH()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dgvLienhe.Refresh();
            //dtDanhsach = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Mã NLH", typeof(string));   //1
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Họ tên", typeof(string)); //2
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Điện thoại", typeof(string)); //3
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string)); //4
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));  //5
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));  //6
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));   //7
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //9
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));    //10
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Chức vụ", typeof(string));   //11
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));   //12
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Có con nhỏ", typeof(string));   //12
            //dtDanhsach.Columns.Add(col);

            //strCmd = "Select * from NGUOILIENHE Where HoTen like N'%" + txtNLH_STen.Text + "%' ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsach.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaNLH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai"].ToString();
            //        row[4] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();
            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[5] = ngayC + "/" + thangC + "/" + namC;
            //        row[6] = dtResult.Rows[i]["Noicap"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();
            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[7] = ngayS + "/" + thangS + "/" + namS;
            //        row[8] = dtResult.Rows[i]["Diachi"].ToString();
            //        row[9] = dtResult.Rows[i]["Email"].ToString();

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[10] = gioitinh;
            //        row[11] = dtResult.Rows[i]["Chucvu"].ToString();
            //        row[12] = dtResult.Rows[i]["MaKH"].ToString();
            //        string connho = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
            //        {
            //            connho = "Có";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
            //        {
            //            connho = "Không";
            //        }
            //        dtDanhsach.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvLienhe.DataSource = dtDanhsach;
            //dgvLienhe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvLienhe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvLienhe.Columns[0].Width = 60;
            //dgvLienhe.Columns[1].Width = 120;
            //dgvLienhe.Columns[2].Width = 200;
            //dgvLienhe.Columns[3].Width = 120;
            //dgvLienhe.Columns[4].Width = 100;
            //dgvLienhe.Columns[5].Width = 110;
            //dgvLienhe.Columns[6].Width = 150;
            //dgvLienhe.Columns[7].Width = 110;
            //dgvLienhe.Columns[8].Width = 200;
            //dgvLienhe.Columns[9].Width = 200;
            //dgvLienhe.Columns[10].Width = 90;
            //dgvLienhe.Columns[11].Width = 150;
            //dgvLienhe.Columns[12].Width = 140;
            //Cursor.Current = Cursors.Default;
        }

        private void layDS_CMNDNLH()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dgvLienhe.Refresh();
            //dtDanhsach = new DataTable();
            //DataColumn col = null;
            //col = new DataColumn("STT", typeof(int));   //0
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Mã NLH", typeof(string));   //1
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Họ tên", typeof(string)); //2
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Điện thoại", typeof(string)); //3
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("CMND", typeof(string)); //4
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày cấp", typeof(string));  //5
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Nơi cấp", typeof(string));  //6
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Ngày sinh", typeof(string));   //7
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Địa chỉ", typeof(string));    //8
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Email", typeof(string));  //9
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Giới tính", typeof(string));    //10
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Chức vụ", typeof(string));   //11
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Mã KH", typeof(string));   //12
            //dtDanhsach.Columns.Add(col);
            //col = new DataColumn("Có con nhỏ", typeof(string));    //10
            //dtDanhsach.Columns.Add(col);

            //strCmd = "Select * from NGUOILIENHE Where CMND like '%" + txtNLH_SCMND.Text + "%' ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        DataRow row = dtDanhsach.NewRow();
            //        row[0] = i + 1;
            //        row[1] = dtResult.Rows[i]["MaNLH"].ToString();
            //        row[2] = dtResult.Rows[i]["Hoten"].ToString();
            //        row[3] = dtResult.Rows[i]["Dienthoai"].ToString();
            //        row[4] = dtResult.Rows[i]["CMND"].ToString();

            //        string ngayCap, ngayC, thangC, namC;
            //        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();
            //        ngayC = ngayCap.Substring(0, 2);
            //        thangC = ngayCap.Substring(3, 2);
            //        namC = ngayCap.Substring(6, 4);

            //        row[5] = ngayC + "/" + thangC + "/" + namC;
            //        row[6] = dtResult.Rows[i]["Noicap"].ToString();

            //        string ngaySinh, ngayS, thangS, namS;
            //        ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();
            //        ngayS = ngaySinh.Substring(0, 2);
            //        thangS = ngaySinh.Substring(3, 2);
            //        namS = ngaySinh.Substring(6, 4);

            //        row[7] = ngayS + "/" + thangS + "/" + namS;
            //        row[8] = dtResult.Rows[i]["Diachi"].ToString();
            //        row[9] = dtResult.Rows[i]["Email"].ToString();

            //        string gioitinh = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
            //        {
            //            gioitinh = "Nam";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
            //        {
            //            gioitinh = "Nữ";
            //        }

            //        row[10] = gioitinh;
            //        row[11] = dtResult.Rows[i]["Chucvu"].ToString();
            //        row[12] = dtResult.Rows[i]["MaKH"].ToString();
            //        string connho = "";
            //        if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
            //        {
            //            connho = "Có";
            //        }
            //        else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
            //        {
            //            connho = "Không";
            //        }
            //        row[13] = connho;
            //        dtDanhsach.Rows.Add(row);
            //    }
            //    catch { }
            //}
            //dgvLienhe.DataSource = dtDanhsach;
            //dgvLienhe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvLienhe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvLienhe.Columns[0].Width = 60;
            //dgvLienhe.Columns[1].Width = 120;
            //dgvLienhe.Columns[2].Width = 200;
            //dgvLienhe.Columns[3].Width = 120;
            //dgvLienhe.Columns[4].Width = 100;
            //dgvLienhe.Columns[5].Width = 110;
            //dgvLienhe.Columns[6].Width = 150;
            //dgvLienhe.Columns[7].Width = 110;
            //dgvLienhe.Columns[8].Width = 200;
            //dgvLienhe.Columns[9].Width = 200;
            //dgvLienhe.Columns[10].Width = 90;
            //dgvLienhe.Columns[11].Width = 150;
            //dgvLienhe.Columns[12].Width = 140;
            //Cursor.Current = Cursors.Default;
        }

        private void layDS_Tinh()
        {
            //DataTable dt = t_bus.DANH_SACH_TINH();
            //
            //cbbTinh.DisplayMember = "TENTINH";
            //cbbTinh.ValueMember = "MATINH";
            //cbbTinh.DataSource = dt;
            ////cbbTinh.SelectedValue = "240";            
            //cbbTinh.SelectedValue = Thongtindangnhap.ma_tinh_hien_tai;
        }

        private void layDS_Huyen()
        {
            //string maTinh;
            //if (cbbTinh.SelectedValue == null)
            //{
            //    maTinh = "";
            //}
            //else
            //{
            //    maTinh = cbbTinh.SelectedValue.ToString();
            //}

            //DataTable dt = h_bus.DANH_SACH_HUYEN(maTinh);
            ////cbbHuyen.DataSource = dt;
            //cbbHuyen.DisplayMember = "TENHUYEN";
            //cbbHuyen.ValueMember = "MAHUYEN";
            //cbbHuyen.DataSource = dt;

            //try
            //{
            //    DataTable dt = new DataTable();
            //    String sCommand = "SELECT * FROM DMHUYEN WHERE LEFT(MAHUYEN,3) LIKE '" + maTinh + "' ORDER BY MAHUYEN ";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //    DataAccess.conn.Close();
            //    cbbHuyen.DataSource = dt;
            //    cbbHuyen.DisplayMember = "TENHUYEN";
            //    cbbHuyen.ValueMember = "MAHUYEN";
            //    //cbbHuyen.SelectedIndex = 0;
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}            
        }

        private void layDS_Xa()
        {
            //string maHuyen;
            //if (cbbHuyen.SelectedValue == null)
            //{
            //    maHuyen = "";
            //}
            //else
            //{
            //    maHuyen = cbbHuyen.SelectedValue.ToString();
            //}

            //DataTable dt = x_bus.DANH_SACH_XA(maHuyen);
            ////cbbXa.DataSource = dt;
            //cbbXa.DisplayMember = "TENXA";
            //cbbXa.ValueMember = "MAXA";
            //cbbXa.DataSource = dt;



            //try
            //{
            //    DataTable dt = new DataTable();
            //    String sCommand = "SELECT * FROM DMXAPHUONG WHERE LEFT(MAXA,5) LIKE '" + maHuyen + "' ORDER BY MAXA ";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //    DataAccess.conn.Close();
            //    cbbXa.DataSource = dt;
            //    cbbXa.DisplayMember = "TENXA";
            //    cbbXa.ValueMember = "MAXA";
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}
        }

        private void layLoaiKH()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            String sCommand = "SELECT maloai,tenloai from dmloaikhachhang where Maloai <> '9999' and Maloai='001' or Maloai='003'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbLoaiKHIpcas.DataSource = dt;
            //cbbLoaiKHIpcas.DisplayMember = "Tenloai";
            //cbbLoaiKHIpcas.ValueMember = "Maloai";
            //cbbLoaiKHIpcas.DataSource = dt;
            //cbbLoaiKHIpcas.SelectedValue = "001";
        }
        //private void layKH2890()
        //{
        //    DataTable dt = new DataTable();
        //    String sCommand = "SELECT ma,ten from doituongkh where left(ma,1)='1' and ma<>1 order by ma";
        //    if (DataAccess.conn.State == ConnectionState.Open)
        //    {
        //        DataAccess.conn.Close();
        //    }
        //    DataAccess.conn.Open();
        //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
        //    DataAccess.conn.Close();
        //    //cbKH2890.DataSource = dt;
        //    //cbKH2890.DisplayMember = "Ten";
        //    //cbKH2890.ValueMember = "Ma";
        //    //cbKH2890.DataSource = dt;
        //    //cbKH2890.SelectedValue = "1";
        //}
        //private void layKH2890DN()
        //{
        //    DataTable dt = new DataTable();
        //    String sCommand = "SELECT ma,ten from doituongkh where left(ma,1)<>'1' and ma<>'32' order by ma";
        //    if (DataAccess.conn.State == ConnectionState.Open)
        //    {
        //        DataAccess.conn.Close();
        //    }
        //    DataAccess.conn.Open();
        //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
        //    DataAccess.conn.Close();
        //    //cbKH2890DN.DataSource = dt;
        //    //cbKH2890DN.DisplayMember = "Ten";
        //    //cbKH2890DN.ValueMember = "Ma";
        //    //cbKH2890DN.DataSource = dt;
        //    //cbKH2890DN.SelectedValue = "1";
        //}
        //private void layLoaihinhDN2890()
        //{
        //    DataTable dt = new DataTable();
        //    String sCommand = "SELECT ma,ten from doituongdn order by ma";
        //    if (DataAccess.conn.State == ConnectionState.Open)
        //    {
        //        DataAccess.conn.Close();
        //    }
        //    DataAccess.conn.Open();
        //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
        //    DataAccess.conn.Close();
        //    //cbDN2890.DataSource = dt;
        //    //cbDN2890.DisplayMember = "Ten";
        //    //cbDN2890.ValueMember = "Ma";
        //    //cbDN2890.DataSource = dt;
        //    //cbDN2890.SelectedValue = "1";
        //}
        private void layDN_LoaiKH()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            String sCommand = "SELECT maloai,tenloai from dmloaikhachhang where Maloai <> '9999' and Maloai<>'001' and Maloai<>'003'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbDN_LoaiKHIpcas.DataSource = dt;
            //cbbDN_LoaiKHIpcas.DisplayMember = "Tenloai";
            //cbbDN_LoaiKHIpcas.ValueMember = "Maloai";
            //cbbDN_LoaiKHIpcas.DataSource = dt;
            //cbbDN_LoaiKHIpcas.SelectedValue = "002";
        }

        private void layDSDN_Tinh()
        {
            System.Data.DataTable dt = t_bus.DANH_SACH_TINH();
            //cbbDN_Tinh.DataSource = dt;
            //cbbDN_Tinh.DisplayMember = "TENTINH";
            //cbbDN_Tinh.ValueMember = "MATINH";
            //cbbDN_Tinh.DataSource = dt;
            ////cbbTinh.SelectedValue = "240";         
            //cbbDN_Tinh.SelectedValue = Thongtindangnhap.ma_tinh_hien_tai;
        }

        private void layDSDN_Huyen()
        {
            //string maTinh;
            //if (cbbDN_Tinh.SelectedValue == null)
            //{
            //    maTinh = "";
            //}
            //else
            //{
            //    maTinh = cbbDN_Tinh.SelectedValue.ToString();
            //}

            //DataTable dt = h_bus.DANH_SACH_HUYEN(maTinh);
            ////cbbDN_Huyen.DataSource = dt;
            //cbbDN_Huyen.DisplayMember = "TENHUYEN";
            //cbbDN_Huyen.ValueMember = "MAHUYEN";
            //cbbDN_Huyen.DataSource = dt;
        }

        private void layDSDN_Xa()
        {
            //string maHuyen;
            //if (cbbDN_Huyen.SelectedValue == null)
            //{
            //    maHuyen = "";
            //}
            //else
            //{
            //    maHuyen = cbbDN_Huyen.SelectedValue.ToString();
            //}

            //DataTable dt = x_bus.DANH_SACH_XA(maHuyen);
            ////cbbDN_Xa.DataSource = dt;
            //cbbDN_Xa.DisplayMember = "TENXA";
            //cbbDN_Xa.ValueMember = "MAXA";
            //cbbDN_Xa.DataSource = dt;
        }

        private void layDS_Linhvuc()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            String sCommand = "SELECT * FROM Nganhnghe Where LoaiKH='1' ORDER BY Manganh ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbLinhvuc.DataSource = dt;
            //cbbLinhvuc.DisplayMember = "Tennganh";
            //cbbLinhvuc.ValueMember = "Manganh";
            //cbbLinhvuc.DataSource = dt;
        }

        private void layDSDN_Linhvuc()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            String sCommand = "SELECT * FROM Nganhnghe Where LoaiKH='2' ORDER BY Manganh ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbDN_Linhvuc.DataSource = dt;
            //cbbDN_Linhvuc.DisplayMember = "Tennganh";
            //cbbDN_Linhvuc.ValueMember = "Manganh";
            //cbbDN_Linhvuc.DataSource = dt;
        }

        private void layDS_NhomKH()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            String sCommand = "SELECT manhom,Diengiai from nhomkhachhang Where MaCN='" + Thongtindangnhap.macn + "' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbSNhomKH.DataSource = dt;
            //cbbSNhomKH.DisplayMember = "Diengiai";
            //cbbSNhomKH.ValueMember = "Manhom";
            //cbbSNhomKH.DataSource = dt;
        }

        private void layDSDN_NhomKH()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            String sCommand = "SELECT manhom,Diengiai from nhomkhachhang Where MaCN='" + Thongtindangnhap.macn + "' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbDN_SNhomKH.DataSource = dt;
            //cbbDN_SNhomKH.DisplayMember = "Diengiai";
            //cbbDN_SNhomKH.ValueMember = "Manhom";
            //cbbDN_SNhomKH.DataSource = dt;
        }

        private void addRow_CN()
        {
            //try
            //{
            //    DataRow row = dtDanhsach.NewRow();
            //    dtDanhsach.Rows.Add(row);
            //    dgvDanhsachCN.DataSource = dtDanhsach;
            //    int irow = dgvDanhsachCN.Rows.Count;

            //    dgvDanhsachCN.Rows[irow - 1].Cells["STT"].Value = irow;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Mã KH"].Value = txtMaKH.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Tên KH"].Value = txtTenKH.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["ĐT di động"].Value = txtMobile.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["ĐT nhà"].Value = txtTel.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Ngày sinh"].Value = dtpNgaysinh.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Giới tính"].Value = cbbGioitinh.Text;

            //    dgvDanhsachCN.Rows[irow - 1].Cells["Địa chỉ"].Value = txtAddress.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Địa chỉ khác"].Value = txtAddress2.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Email"].Value = txtEmail.Text;

            //    dgvDanhsachCN.Rows[irow - 1].Cells["Website"].Value = txtWebsite.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["NH giao dịch"].Value = txtNHGD.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Sở thích"].Value = txtSothich.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Tình trạng"].Value = cbbTinhtrang.Text;

            //    //string thunhap;
            //    //if (txtThunhap.Text == "")
            //    //{
            //    //    thunhap = "0";
            //    //}
            //    //else
            //    //{
            //    //    thunhap = String.Format("{0:0}", Decimal.Parse(txtThunhap.Text));
            //    //}
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Thu nhập"].Value = thunhap;

            //    dgvDanhsachCN.Rows[irow - 1].Cells["Tên đ.nhập"].Value = txtMaNV.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["CMND"].Value = txtCMND.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Ngày cấp"].Value = dtpNgaycap.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Nơi cấp"].Value = txtNoicap.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Ngày kết hôn"].Value = dtpNgayKH.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Chi tiết"].Value = txtChitiet.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Ghi chú"].Value = txtGhichu.Text;

            //    dgvDanhsachCN.Rows[irow - 1].Cells["Tỉnh"].Value = cbbTinh.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Huyện"].Value = cbbHuyen.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Xã"].Value = cbbXa.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Nghề nghiệp"].Value = cbbLinhvuc.Text;
            //    dgvDanhsachCN.Rows[irow - 1].Cells["Loại KH"].Value = cbbLoaiKHIpcas.Text;
            //}
            //catch { }
        }

        private void addRow_DN()
        {
            //try
            //{
            //    DataRow row = dtDanhsachDN.NewRow();
            //    dtDanhsachDN.Rows.Add(row);
            //    dgvDanhsachDN.DataSource = dtDanhsachDN;
            //    int irow = dgvDanhsachDN.Rows.Count;

            //    dgvDanhsachDN.Rows[irow - 1].Cells["STT"].Value = irow;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Mã KH"].Value = txtDN_MaKH.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Tên KH"].Value = txtDN_TenKH.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["ĐT di động"].Value = txtDN_Mobile.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["ĐT nhà"].Value = txtDN_Tel.Text;

            //    dgvDanhsachDN.Rows[irow - 1].Cells["Địa chỉ"].Value = txtDN_Address.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Địa chỉ khác"].Value = txtDN_Address2.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Email"].Value = txtDN_Email.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Website"].Value = txtDN_Website.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["NH giao dịch"].Value = txtDN_NHGD.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Tình trạng"].Value = cbbDN_Tinhtrang.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Tên đ.nhập"].Value = txtDN_MaNV.Text;

            //    dgvDanhsachDN.Rows[irow - 1].Cells["Giấy phép ĐK"].Value = txtDN_GPDK.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["QĐ thành lập"].Value = txtDN_QDTL.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["MST"].Value = txtDN_MST.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Ngày thành lập"].Value = dtpDN_NgayTL.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Chi tiết"].Value = txtDN_Chitiet.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Ghi chú"].Value = txtDN_Ghichu.Text;

            //    dgvDanhsachDN.Rows[irow - 1].Cells["Tỉnh"].Value = cbbDN_Tinh.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Huyện"].Value = cbbDN_Huyen.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Xã"].Value = cbbDN_Xa.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Lĩnh vực"].Value = cbbDN_Linhvuc.Text;
            //    dgvDanhsachDN.Rows[irow - 1].Cells["Loại KH"].Value = cbbDN_LoaiKHIpcas.Text;
            //}
            //catch { }
        }

        private void modifyRow_CN()
        {
            //try
            //{
            //    dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value = txtTenKH.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value = txtMobile.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["ĐT nhà"].Value = txtTel.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value = dtpNgaysinh.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Giới tính"].Value = cbbGioitinh.Text;

            //    dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value = txtAddress.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Địa chỉ khác"].Value = txtAddress2.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Email"].Value = txtEmail.Text;

            //    dgvDanhsachCN.CurrentRow.Cells["Website"].Value = txtWebsite.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["NH giao dịch"].Value = txtNHGD.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Sở thích"].Value = txtSothich.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Tình trạng"].Value = cbbTinhtrang.Text;

            //    string thunhap;
            //    if (txtThunhap.Text == "")
            //    {
            //        thunhap = "0";
            //    }
            //    else
            //    {
            //        thunhap = String.Format("{0:0}", Decimal.Parse(txtThunhap.Text));
            //    }
            //    dgvDanhsachCN.CurrentRow.Cells["Thu nhập"].Value = thunhap;

            //    dgvDanhsachCN.CurrentRow.Cells["Tên đ.nhập"].Value = txtMaNV.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["CMND"].Value = txtCMND.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value = dtpNgaycap.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value = txtNoicap.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Ngày kết hôn"].Value = dtpNgayKH.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Chi tiết"].Value = txtChitiet.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Ghi chú"].Value = txtGhichu.Text;

            //    dgvDanhsachCN.CurrentRow.Cells["Tỉnh"].Value = cbbTinh.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Huyện"].Value = cbbHuyen.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Xã"].Value = cbbXa.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value = cbbLinhvuc.Text;
            //    dgvDanhsachCN.CurrentRow.Cells["Loại KH"].Value = cbbLoaiKHIpcas.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Đối tượng KH"].Value = cbKH2890.Text;
            //}
            //catch { }
        }

        private void modifyRow_DN()
        {
            //try
            //{
            //    dgvDanhsachDN.CurrentRow.Cells["Tên KH"].Value = txtDN_TenKH.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["ĐT di động"].Value = txtDN_Mobile.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["ĐT nhà"].Value = txtDN_Tel.Text;

            //    dgvDanhsachDN.CurrentRow.Cells["Địa chỉ"].Value = txtDN_Address.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Địa chỉ khác"].Value = txtDN_Address2.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Email"].Value = txtDN_Email.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Website"].Value = txtDN_Website.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["NH giao dịch"].Value = txtDN_NHGD.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Tình trạng"].Value = cbbDN_Tinhtrang.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Tên đ.nhập"].Value = txtDN_MaNV.Text;

            //    dgvDanhsachDN.CurrentRow.Cells["Giấy phép ĐK"].Value = txtDN_GPDK.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["QĐ thành lập"].Value = txtDN_QDTL.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["MST"].Value = txtDN_MST.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Ngày thành lập"].Value = dtpDN_NgayTL.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Chi tiết"].Value = txtDN_Chitiet.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Ghi chú"].Value = txtDN_Ghichu.Text;

            //    dgvDanhsachDN.CurrentRow.Cells["Tỉnh"].Value = cbbDN_Tinh.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Huyện"].Value = cbbDN_Huyen.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Xã"].Value = cbbDN_Xa.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Lĩnh vực"].Value = cbbDN_Linhvuc.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Loại KH"].Value = cbbDN_LoaiKHIpcas.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Đối tượng KH"].Value = cbKH2890DN.Text;
            //    dgvDanhsachDN.CurrentRow.Cells["Loại hình DN"].Value = cbDN2890.Text;
            //}
            //catch { }
        }

        private void delRow_CN()
        {
            try
            {
                dtDanhsach.Rows.RemoveAt(dgvDanhsachCN.CurrentRow.Index);
                for (int i = dgvDanhsachCN.CurrentRow.Index; i < dgvDanhsachCN.RowCount; i++)
                {
                    dtDanhsach.Rows[i]["STT"] = i + 1;
                }
                dgvDanhsachCN.DataSource = dtDanhsach;
            }
            catch { }
        }

        private void delRow_DN()
        {
            try
            {
                dtDanhsachDN.Rows.RemoveAt(dgvDanhsachDN.CurrentRow.Index);
                for (int i = dgvDanhsachDN.CurrentRow.Index; i < dgvDanhsachDN.RowCount; i++)
                {
                    dtDanhsachDN.Rows[i]["STT"] = i + 1;
                }
                dgvDanhsachDN.DataSource = dtDanhsachDN;
            }
            catch { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDN_Add_Click(object sender, EventArgs e)
        {
            //if (txtDN_MaKH.Text.Trim() == "")
            //{
            //    MessageBox.Show("Chưa nhập mã khách hàng.", "Thông báo");
            //    txtDN_MaKH.Focus();
            //    return;
            //}

            //strCmd = "Select * from Khachhang Where MaKH='" + txtDN_MaKH.Text.Trim() + "' ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int loaiKH = 2;

            //string ngayTL, thangTL, namTL;
            //ngayTL = dtpDN_NgayTL.Text.Substring(0, 2);
            //thangTL = dtpDN_NgayTL.Text.Substring(3, 2);
            //namTL = dtpDN_NgayTL.Text.Substring(6, 4);

            //string ngayTLN, thangTLN, namTLN;
            //ngayTLN = dtpDN_NgayTLN.Text.Substring(0, 2);
            //thangTLN = dtpDN_NgayTLN.Text.Substring(3, 2);
            //namTLN = dtpDN_NgayTLN.Text.Substring(6, 4);

            //int tinhtrang = 1;
            //if (cbbTinhtrang.Text == "Hoạt động")
            //{
            //    tinhtrang = 1;
            //}
            //else if (cbbTinhtrang.Text == "Không hoạt động")
            //{
            //    tinhtrang = 0;
            //}

            //string maTinh, maHuyen, maXa;
            //if (cbbDN_Tinh.SelectedValue == null || cbbDN_Huyen.SelectedValue == null || cbbDN_Xa.SelectedValue == null)
            //{
            //    maTinh = "";
            //    maHuyen = "";
            //    maXa = "";
            //}
            //else
            //{
            //    maTinh = cbbDN_Tinh.SelectedValue.ToString();
            //    maHuyen = cbbDN_Huyen.SelectedValue.ToString();
            //    maXa = cbbDN_Xa.SelectedValue.ToString();
            //}

            //string Manganh;
            //if (cbbDN_Linhvuc.SelectedValue == null)
            //{
            //    Manganh = "";
            //}
            //else
            //{
            //    Manganh = cbbDN_Linhvuc.SelectedValue.ToString();
            //}
            //string doituongkh;
            //if (cbKH2890DN.SelectedValue == null)
            //{
            //    doituongkh = "";
            //}
            //else
            //{
            //    doituongkh = cbKH2890DN.SelectedValue.ToString();
            //}
            //string loaihinhDN;
            //if (cbDN2890.SelectedValue == null)
            //{
            //    loaihinhDN = "";
            //}
            //else
            //{
            //    loaihinhDN = cbDN2890.SelectedValue.ToString();
            //}
            //string loaiKH_ipcas = "";
            //loaiKH_ipcas = cbbDN_LoaiKHIpcas.Text;

            //txtDN_MaNV.Text = Thongtindangnhap.user_id;

            //if (dtResult.Rows.Count == 0)
            //{
            //    strCmd = "Insert into Khachhang(MaKH,Hoten,Diachi1,Diachi2,Dienthoai1,Dienthoai2,Email,Linhvuc,Website,GPDK,QDTL,MST,LoaiKH,MaNV,NHGiaodich,Ghichu,MaCN,Tinhtrang,CTLoaiKH,Ngaysinh,Ngaycap,Gioitinh,Xa,Huyen,Tinh,Ngaythanhlap,ngaytlnganh,LoaiKH_IPCAS,doituongkh,doituongdn) ";
            //    strCmd += "Values('" + txtDN_MaKH.Text.Trim() + "',N'" + txtDN_TenKH.Text.Trim() + "',N'" + txtDN_Address.Text.Trim() + "',N'" + txtDN_Address2.Text.Trim() + "','";
            //    strCmd += txtDN_Mobile.Text.Trim() + "','" + txtDN_Tel.Text.Trim() + "','" + txtDN_Email.Text.Trim() + "','" + Manganh + "','" + txtDN_Website.Text.Trim() + "','";
            //    strCmd += txtDN_GPDK.Text.Trim() + "','" + txtDN_QDTL.Text.Trim() + "','" + txtDN_MST.Text.Trim() + "','";
            //    strCmd += loaiKH + "','" + txtDN_MaNV.Text.Trim() + "','" + txtDN_NHGD.Text.Trim() + "',N'";
            //    strCmd += txtDN_Ghichu.Text.Trim() + "','" + Thongtindangnhap.macn + "',N'" + tinhtrang + "',N'" + txtDN_Chitiet.Text.Trim() + "','" + "01/01/1990" + "','" + "01/01/1990" + "','" + "0";
            //    strCmd += "',N'" + maXa + "',N'" + maHuyen + "',N'" + maTinh + "','" + thangTL + "/" + ngayTL + "/" + namTL + "','" + thangTLN + "/" + ngayTLN + "/" + namTLN + "','" + loaiKH_ipcas + "','" + doituongkh + "','" + loaihinhDN + "')";
            //    try
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //        DataAccess.conn.Open();
            //        adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
            //        adapter.InsertCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();

            //        addRow_DN();

            //        MessageBox.Show("Đã thêm.", "Thông báo");
            //    }
            //    catch
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //    }
            //    txtDN_MaKH.Focus();
            //    txtDN_TenKH.Text = "";
            //    txtDN_Mobile.Text = "";
            //    txtDN_Tel.Text = "";
            //    txtDN_Address.Text = "";
            //    txtDN_Address2.Text = "";
            //    txtDN_Email.Text = "";
            //    txtDN_Website.Text = "";
            //    txtDN_NHGD.Text = "";
            //    cbbDN_Tinhtrang.SelectedIndex = 0;
            //    txtDN_GPDK.Text = "";
            //    txtDN_QDTL.Text = "";
            //    txtDN_MST.Text = "";
            //    dtpDN_NgayTL.Text = "01/01/1990";
            //    txtDN_Chitiet.Text = "";
            //    txtDN_Ghichu.Text = "";
            //    layDSDN_Tinh();
            //    layDSDN_Huyen();
            //    layDSDN_Xa();
            //    cbbDN_LoaiKHIpcas.SelectedValue = "002";
            //}
            //else
            //{
            //    MessageBox.Show("Mã khách hàng này đã tồn tại.", "Cảnh báo");
            //    txtDN_MaKH.Focus();
            //    return;
            //}
        }

        private void btnModify_Click(object sender, EventArgs e)
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

        private void btnDN_Modify_Click(object sender, EventArgs e)
        {
            //if (txtDN_MaKH.Text.Trim() == "")
            //{
            //    MessageBox.Show("Chưa nhập mã khách hàng.", "Thông báo");
            //    txtDN_MaKH.Focus();
            //    return;
            //}

            //strCmd = "Select * from Khachhang Where MaKH='" + txtDN_MaKH.Text.Trim() + "' ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int loaiKH = 2;

            //string ngayTL, thangTL, namTL;
            //ngayTL = dtpDN_NgayTL.Text.Substring(0, 2);
            //thangTL = dtpDN_NgayTL.Text.Substring(3, 2);
            //namTL = dtpDN_NgayTL.Text.Substring(6, 4);

            //string ngayTLN, thangTLN, namTLN;
            //ngayTLN = dtpDN_NgayTLN.Text.Substring(0, 2);
            //thangTLN = dtpDN_NgayTLN.Text.Substring(3, 2);
            //namTLN = dtpDN_NgayTLN.Text.Substring(6, 4);

            //int tinhtrang = 1;
            //if (cbbTinhtrang.Text == "Hoạt động")
            //{
            //    tinhtrang = 1;
            //}
            //else if (cbbTinhtrang.Text == "Không hoạt động")
            //{
            //    tinhtrang = 0;
            //}

            //string maTinh, maHuyen, maXa;
            //if (cbbDN_Tinh.SelectedValue == null || cbbDN_Huyen.SelectedValue == null || cbbDN_Xa.SelectedValue == null)
            //{
            //    maTinh = "";
            //    maHuyen = "";
            //    maXa = "";
            //}
            //else
            //{
            //    maTinh = cbbDN_Tinh.SelectedValue.ToString();
            //    maHuyen = cbbDN_Huyen.SelectedValue.ToString();
            //    maXa = cbbDN_Xa.SelectedValue.ToString();
            //}

            //string Manganh;
            //if (cbbDN_Linhvuc.SelectedValue == null)
            //{
            //    Manganh = "";
            //}
            //else
            //{
            //    Manganh = cbbDN_Linhvuc.SelectedValue.ToString();
            //}
            //string doituongkh;
            //if (cbKH2890DN.SelectedValue == null)
            //{
            //    doituongkh = "";
            //}
            //else
            //{
            //    doituongkh = cbKH2890DN.SelectedValue.ToString();
            //}
            //string loaihinhDN;
            //if (cbDN2890.SelectedValue == null)
            //{
            //    loaihinhDN = "";
            //}
            //else
            //{
            //    loaihinhDN = cbDN2890.SelectedValue.ToString();
            //}
            //string loaiKH_ipcas = "";
            //loaiKH_ipcas = cbbDN_LoaiKHIpcas.Text;

            //txtDN_MaNV.Text = Thongtindangnhap.user_id;

            //if (dtResult.Rows.Count > 0)
            //{
            //    strCmd = "Update Khachhang ";
            //    strCmd += "Set Hoten=N'" + txtDN_TenKH.Text.Trim() + "',Diachi1=N'" + txtDN_Address.Text.Trim() + "',Diachi2=N'" + txtDN_Address2.Text.Trim() + "',Dienthoai1='" + txtDN_Mobile.Text.Trim() + "',Dienthoai2='" + txtDN_Tel.Text.Trim();
            //    strCmd += "',Email='" + txtDN_Email.Text.Trim() + "',Linhvuc='" + Manganh + "',Website='" + txtDN_Website.Text.Trim();
            //    strCmd += "',GPDK='" + txtDN_GPDK.Text.Trim() + "',QDTL='" + txtDN_QDTL.Text.Trim() + "',MST='" + txtDN_MST.Text.Trim();
            //    strCmd += "',LoaiKH='" + loaiKH + "',MaNV='" + txtDN_MaNV.Text.Trim() + "',NHGiaodich='" + txtDN_NHGD.Text.Trim();
            //    strCmd += "',Ghichu=N'" + txtDN_Ghichu.Text.Trim() + "',MaCN='" + Thongtindangnhap.macn + "',Tinhtrang='" + tinhtrang + "',CTLoaiKH=N'" + txtDN_Chitiet.Text.Trim();
            //    strCmd += "',Xa=N'" + maXa + "',Huyen=N'" + maHuyen + "',Tinh=N'" + maTinh + "',Ngaythanhlap='" + thangTL + "/" + ngayTL + "/" + namTL + "',Ngaytlnganh='" + thangTLN + "/" + ngayTLN + "/" + namTLN + "',LoaiKH_IPCAS=N'" + loaiKH_ipcas + "',doituongkh='" + doituongkh + "',doituongdn='" + loaihinhDN + "' ";
            //    strCmd += " Where MaKH='" + txtDN_MaKH.Text.Trim() + "' ";

            //    try
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //        DataAccess.conn.Open();
            //        adapter.UpdateCommand = new SqlCommand(strCmd, DataAccess.conn);
            //        adapter.UpdateCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();

            //        modifyRow_DN();

            //        MessageBox.Show("Đã thay đổi.", "Thông báo");
            //    }
            //    catch
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //    }
            //    txtDN_MaKH.Focus();
            //    txtDN_TenKH.Text = "";
            //    txtDN_Mobile.Text = "";
            //    txtDN_Tel.Text = "";
            //    txtDN_Address.Text = "";
            //    txtDN_Address2.Text = "";
            //    txtDN_Email.Text = "";
            //    txtDN_Website.Text = "";
            //    txtDN_NHGD.Text = "";
            //    cbbDN_Tinhtrang.SelectedIndex = 0;
            //    txtDN_GPDK.Text = "";
            //    txtDN_QDTL.Text = "";
            //    txtDN_MST.Text = "";
            //    dtpDN_NgayTL.Text = "01/01/1990";
            //    txtDN_Chitiet.Text = "";
            //    txtDN_Ghichu.Text = "";
            //    layDSDN_Tinh();
            //    layDSDN_Huyen();
            //    layDSDN_Xa();
            //    cbbDN_LoaiKHIpcas.SelectedValue = "002";
            //}
            //else
            //{
            //    MessageBox.Show("Mã khách hàng này không tồn tại.", "Cảnh báo");
            //    txtDN_MaKH.Focus();
            //    return;
            //}
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //if (dgvDanhsachCN.RowCount > 0)
            //{
            //    if (MessageBox.Show("Bạn chắc chắn muốn xóa thông tin khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            string maKH = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
            //            SqlDataAdapter adapter = new SqlDataAdapter();

            //            strCmd = "Delete from Khachhang Where MaKH='" + maKH + "'";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
            //            adapter.DeleteCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();

            //            delRow_CN();

            //            MessageBox.Show("Đã xóa", "Thông báo");
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //        }

            //        txtMaKH.Text = "";
            //        txtTenKH.Text = "";
            //        dtpNgaysinh.Text = "01/01/1990";
            //        cbbGioitinh.SelectedIndex = 0;
            //        txtMobile.Text = "";
            //        txtTel.Text = "";
            //        txtAddress.Text = "";
            //        txtAddress2.Text = "";
            //        txtEmail.Text = "";
            //        txtWebsite.Text = "";
            //        txtNHGD.Text = "";
            //        txtSothich.Text = "";
            //        txtThunhap.Text = "";
            //        cbbTinhtrang.SelectedIndex = 0;
            //        txtCMND.Text = "";
            //        dtpNgaycap.Text = "01/01/1990";
            //        txtNoicap.Text = "";
            //        dtpNgayKH.Text = "01/01/1990";
            //        txtChitiet.Text = "";
            //        txtGhichu.Text = "";
            //        layDS_Tinh();
            //        layDS_Huyen();
            //        layDS_Xa();
            //        cbbLoaiKHIpcas.SelectedValue = "001";
            //    }
            //}

        }

        private void btnDN_Del_Click(object sender, EventArgs e)
        {
            //if (dgvDanhsachDN.RowCount > 0)
            //{
            //    if (MessageBox.Show("Bạn chắc chắn muốn xóa thông tin khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            string maKH = dgvDanhsachDN.CurrentRow.Cells["Mã KH"].Value.ToString();
            //            SqlDataAdapter adapter = new SqlDataAdapter();
            //            if (dgvLienhe.RowCount > 0)
            //            {
            //                MessageBox.Show("Bạn phải xóa thông tin liên hệ của khách hàng này trước.", "Thông báo");
            //                return;
            //            }

            //            strCmd = "Delete from Khachhang Where MaKH='" + maKH + "'";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
            //            adapter.DeleteCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();

            //            delRow_DN();

            //            MessageBox.Show("Đã xóa", "Thông báo");
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //        }

            //        txtDN_MaKH.Focus();
            //        txtDN_TenKH.Text = "";
            //        txtDN_Mobile.Text = "";
            //        txtDN_Tel.Text = "";
            //        txtDN_Address.Text = "";
            //        txtDN_Address2.Text = "";
            //        txtDN_Email.Text = "";
            //        txtDN_Website.Text = "";
            //        txtDN_NHGD.Text = "";
            //        cbbDN_Tinhtrang.SelectedIndex = 0;
            //        txtDN_GPDK.Text = "";
            //        txtDN_QDTL.Text = "";
            //        txtDN_MST.Text = "";
            //        dtpDN_NgayTL.Text = "01/01/1990";
            //        txtDN_Chitiet.Text = "";
            //        txtDN_Ghichu.Text = "";
            //        layDSDN_Tinh();
            //        layDSDN_Huyen();
            //        layDSDN_Xa();
            //        cbbDN_LoaiKHIpcas.SelectedValue = "002";
            //    }
            //}
        }

        private void btnLH_Del_Click(object sender, EventArgs e)
        {
            //if (dgvLienhe.RowCount > 0)
            //{
            //    if (MessageBox.Show("Bạn chắc chắn muốn xóa thông tin người liên hệ này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            string maKH = dgvDanhsachDN.CurrentRow.Cells["Mã KH"].Value.ToString();
            //            string maNLH = dgvLienhe.CurrentRow.Cells["Mã NLH"].Value.ToString();
            //            SqlDataAdapter adapter = new SqlDataAdapter();
            //            strCmd = "Delete from Nguoilienhe Where MaNLH='" + maNLH + "'";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
            //            adapter.DeleteCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();

            //            layDS_Lienhe(maKH);

            //            MessageBox.Show("Đã xóa", "Thông báo");
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //        }
            //        txtLH_MaNLH.Text = "";
            //        txtLH_Hoten.Text = "";
            //        dtpNgaysinh.Text = "01/01/1990";
            //        cbbGioitinh.SelectedIndex = 0;
            //        txtLH_Tel.Text = "";
            //        txtLH_CMND.Text = "";
            //        dtpLH_Ngaycap.Text = "01/01/1990";
            //        txtLH_Noicap.Text = "";
            //        txtLH_Address.Text = "";
            //        txtLH_Email.Text = "";
            //        txtLH_Chucvu.Text = "";
            //    }
            //}
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
            int connho = 0;
            if (cbbLH_Connho.Text == "Có")
            {
                connho = 1;
            }
            else if (cbbLH_Connho.Text == "Không")
            {
                connho = 0;
            }

            strCmd = "SELECT * FROM NGUOILIENHE ";
            strCmd += "WHERE Hoten='" + txtLH_Hoten.Text.Trim() + "' and CMND='" + txtLH_CMND.Text.Trim() + "' and Dienthoai='" + txtLH_Tel.Text.Trim() + "' and MaKH='" + txtLH_MaKH.Text.Trim() + "' ";
            strCmd += " and Diachi='" + txtLH_Address.Text.Trim() + "' and Email='" + txtLH_Email.Text.Trim() + "' and Chucvu='" + txtLH_Chucvu.Text.Trim() + "' and Noicap='" + txtLH_Noicap.Text.Trim() + "' ";
            strCmd += " and Gioitinh='" + gioitinh + "' and Ngaysinh='" + thangS + "/" + ngayS + "/" + namS + "' and Ngaycap='" + thangC + "/" + ngayC + "/" + namC + "' and MaCN='" + Thongtindangnhap.macn + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }

                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
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
                maNLH = Thongtindangnhap.macn + nam + thang + ngay + gio + phut + giay + miligiay;
                txtLH_MaNLH.Text = maNLH;

                strCmd = "Insert into NguoiLienHe(MaNLH,Hoten,CMND,Ngaycap,Noicap,Ngaysinh,Dienthoai,Diachi,Email,Chucvu,Gioitinh,Connho,MaKH,MaCN) ";
                strCmd += "Values('" + txtLH_MaNLH.Text.Trim() + "',N'" + txtLH_Hoten.Text.Trim() + "','" + txtLH_CMND.Text.Trim() + "','" + thangC + "/" + ngayC + "/" + namC + "',N'" + txtLH_Noicap.Text.Trim();
                strCmd += "','" + thangS + "/" + ngayS + "/" + namS + "','" + txtLH_Tel.Text.Trim() + "',N'" + txtLH_Address.Text.Trim() + "','" + txtLH_Email.Text.Trim();
                strCmd += "',N'" + txtLH_Chucvu.Text.Trim() + "','" + gioitinh + "','" + connho + "','" + txtLH_MaKH.Text.Trim() + "','" + Thongtindangnhap.macn + "')";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }

                    DataAccess.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layDS_Lienhe(txtDN_MaKH.Text);
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                txtLH_MaNLH.Text = "";
                txtLH_Hoten.Text = "";
                dtpLH_Ngaysinh.Text = "01/01/1990";
                cbbLH_Gioitinh.SelectedIndex = 0;
                cbbLH_Connho.SelectedIndex = 0;
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
            int connho = 0;
            if (cbbLH_Connho.Text == "Có")
            {
                connho = 1;
            }
            else if (cbbLH_Connho.Text == "Không")
            {
                connho = 0;
            }

            strCmd = "SELECT * FROM NGUOILIENHE ";
            strCmd += " Where MaNLH='" + txtLH_MaNLH.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update NguoiLienHe ";
                strCmd += "Set Hoten=N'" + txtLH_Hoten.Text.Trim() + "',CMND='" + txtLH_CMND.Text.Trim() + "',Ngaycap='" + thangC + "/" + ngayC + "/" + namC + "',Noicap=N'" + txtLH_Noicap.Text.Trim();
                strCmd += "',Ngaysinh='" + thangS + "/" + ngayS + "/" + namS + "',Dienthoai='" + txtLH_Tel.Text.Trim() + "',Diachi=N'" + txtLH_Address.Text.Trim() + "',Email='" + txtLH_Email.Text.Trim();
                strCmd += "',Chucvu=N'" + txtLH_Chucvu.Text.Trim() + "',Gioitinh='" + gioitinh + "',connho='" + connho + "',MaKH='" + txtLH_MaKH.Text.Trim() + "' ";
                strCmd += " Where MaNLH='" + txtLH_MaNLH.Text.Trim() + "' ";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layDS_Lienhe(txtDN_MaKH.Text);
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
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

        //private void dgvDanhsachCN_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        txtMaKH.Text = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
        //        txtTenKH.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
        //        //txtMobile.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
        //        //txtTel.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT nhà"].Value.ToString();
        //        //dtpNgaysinh.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
        //        //cbbGioitinh.Text = dgvDanhsachCN.CurrentRow.Cells["Giới tính"].Value.ToString();
        //        //cbKH2890.Text = dgvDanhsachCN.CurrentRow.Cells["Đối tượng KH"].Value.ToString();
        //        //txtAddress.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
        //        //txtAddress2.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ khác"].Value.ToString();
        //        //txtEmail.Text = dgvDanhsachCN.CurrentRow.Cells["Email"].Value.ToString();

        //        //txtWebsite.Text = dgvDanhsachCN.CurrentRow.Cells["Website"].Value.ToString();
        //        //txtNHGD.Text = dgvDanhsachCN.CurrentRow.Cells["NH giao dịch"].Value.ToString();
        //        //txtSothich.Text = dgvDanhsachCN.CurrentRow.Cells["Sở thích"].Value.ToString();
        //        //cbbTinhtrang.Text = dgvDanhsachCN.CurrentRow.Cells["Tình trạng"].Value.ToString();
        //        //txtThunhap.Text = dgvDanhsachCN.CurrentRow.Cells["Thu nhập"].Value.ToString();
        //        //txtMaNV.Text = dgvDanhsachCN.CurrentRow.Cells["Tên đ.nhập"].Value.ToString();

        //        txtCMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
        //        //dtpNgaycap.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
        //        //txtNoicap.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
        //        //dtpNgayKH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày kết hôn"].Value.ToString();
        //        //txtChitiet.Text = dgvDanhsachCN.CurrentRow.Cells["Chi tiết"].Value.ToString();
        //        //txtGhichu.Text = dgvDanhsachCN.CurrentRow.Cells["Ghi chú"].Value.ToString();

        //        //cbbTinh.Text = dgvDanhsachCN.CurrentRow.Cells["Tỉnh"].Value.ToString();
        //        //layDS_Huyen();
        //        //cbbHuyen.Text = dgvDanhsachCN.CurrentRow.Cells["Huyện"].Value.ToString();
        //        //layDS_Xa();
        //        //cbbXa.Text = dgvDanhsachCN.CurrentRow.Cells["Xã"].Value.ToString();
        //        //cbbLinhvuc.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
        //        //cbbLoaiKHIpcas.Text = dgvDanhsachCN.CurrentRow.Cells["Loại KH"].Value.ToString();
        //    }
        //    catch { }
        //}

        private void dgvDanhsachDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtDN_MaKH.Text = dgvDanhsachDN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtDN_TenKH.Text = dgvDanhsachDN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtDN_Mobile.Text = dgvDanhsachDN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                //txtDN_Tel.Text = dgvDanhsachDN.CurrentRow.Cells["ĐT nhà"].Value.ToString();

                txtDN_Address.Text = dgvDanhsachDN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                //txtDN_Address2.Text = dgvDanhsachDN.CurrentRow.Cells["Địa chỉ khác"].Value.ToString();
                //txtDN_Email.Text = dgvDanhsachDN.CurrentRow.Cells["Email"].Value.ToString();

                //txtDN_Website.Text = dgvDanhsachDN.CurrentRow.Cells["Website"].Value.ToString();
                //txtDN_NHGD.Text = dgvDanhsachDN.CurrentRow.Cells["NH giao dịch"].Value.ToString();
                //cbbDN_Tinhtrang.Text = dgvDanhsachDN.CurrentRow.Cells["Tình trạng"].Value.ToString();
                txtDN_MaNV.Text = dgvDanhsachDN.CurrentRow.Cells["Tên đ.nhập"].Value.ToString();

                //txtDN_GPDK.Text = dgvDanhsachDN.CurrentRow.Cells["Giấy phép ĐK"].Value.ToString();
                //txtDN_QDTL.Text = dgvDanhsachDN.CurrentRow.Cells["QĐ thành lập"].Value.ToString();
                txtDN_MST.Text = dgvDanhsachDN.CurrentRow.Cells["MST"].Value.ToString();
                //dtpDN_NgayTL.Text = dgvDanhsachDN.CurrentRow.Cells["Ngày thành lập"].Value.ToString();
                //dtpDN_NgayTLN.Text = dgvDanhsachDN.CurrentRow.Cells["Ngày tl ngành"].Value.ToString();
                //txtDN_Chitiet.Text = dgvDanhsachDN.CurrentRow.Cells["Chi tiết"].Value.ToString();
                //txtDN_Ghichu.Text = dgvDanhsachDN.CurrentRow.Cells["Ghi chú"].Value.ToString();

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

                //cbbDN_Tinh.Text = dgvDanhsachDN.CurrentRow.Cells["Tỉnh"].Value.ToString();
                layDSDN_Huyen();
                //cbbDN_Huyen.Text = dgvDanhsachDN.CurrentRow.Cells["Huyện"].Value.ToString();
                layDSDN_Xa();
                //cbbDN_Xa.Text = dgvDanhsachDN.CurrentRow.Cells["Xã"].Value.ToString();
                //cbbDN_Linhvuc.Text = dgvDanhsachDN.CurrentRow.Cells["Lĩnh vực"].Value.ToString();
                //cbbDN_LoaiKHIpcas.Text = dgvDanhsachDN.CurrentRow.Cells["Loại KH"].Value.ToString();
                //cbKH2890DN.Text = dgvDanhsachDN.CurrentRow.Cells["Đối tượng KH"].Value.ToString();
                //cbDN2890.Text = dgvDanhsachDN.CurrentRow.Cells["Loại hình DN"].Value.ToString();
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
                cbbLH_Connho.Text = dgvLienhe.CurrentRow.Cells["Có con nhỏ"].Value.ToString();
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
            if (!CommonMethod.KiemTraNhapNgay(txtNgaysinh.Text))
            {
                MessageBox.Show("Dữ liệu ngày nhập chưa đúng định dạng dd/MM/yyyy");
                txtNgaysinh.Focus();
                return;
            }
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

        //private void dgvDanhsachCN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //DataAccess.conn.Close();
        //    makh = dgvDanhsachCN.Rows[dgvDanhsachCN.CurrentRow.Index].Cells[1].Value.ToString();
        //    CRM.frmHH_CTKHHH form_ct = new frmHH_CTKHHH();
        //    form_ct.ShowDialog();
        //    //DataAccess.conn.Open();
        //}

        private void dgvDanhsachDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataAccess.conn.Close();
            makh = dgvDanhsachDN.Rows[dgvDanhsachDN.CurrentRow.Index].Cells[1].Value.ToString();
            CRM.frmHH_CTKHHH form_ct = new frmHH_CTKHHH();
            form_ct.ShowDialog();
            //DataAccess.conn.Open();
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
            //cbbHuyen.Text = "";
            //cbbXa.Text = "";
            //layDS_Huyen();
            //layDS_Xa();
        }

        private void cbbHuyen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //cbbXa.Text = "";
            //layDS_Xa();
        }

        private void cbbDN_Tinh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //cbbDN_Huyen.Text = "";
            //cbbDN_Xa.Text = "";
            //layDSDN_Huyen();
            //layDSDN_Xa();
        }

        private void cbbDN_Huyen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //cbbDN_Xa.Text = "";
            //layDSDN_Xa();
        }

        private void btnSNhomKH_Click(object sender, EventArgs e)
        {
            layDS_SNhomKH();
        }

        private void btnDN_SNhomKH_Click(object sender, EventArgs e)
        {
            layDSDN_SNhomKH();
        }

        private void txtThunhap_TextChanged(object sender, EventArgs e)
        {
            //if (txtThunhap.Text != "")
            //{
            //    string sDummy = txtThunhap.Text;
            //    try
            //    {
            //        int iKeep = txtThunhap.SelectionStart - 1;
            //        for (int i = iKeep; i >= 0; i--)
            //        {
            //            if (txtThunhap.Text[i] == ',')
            //            {
            //                iKeep -= 1;
            //            }
            //        }
            //        sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
            //        for (int i = 0; i <= iKeep; i++)
            //        {
            //            if (sDummy[i] == ',')
            //            {
            //                iKeep += 1;
            //            }
            //        }
            //        txtThunhap.Text = sDummy;
            //        txtThunhap.SelectionStart = iKeep + 1;
            //    }
            //    catch
            //    {
            //    }
            //}
        }

        private void txtThunhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbbDN_Tinhtrang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelX67_Click(object sender, EventArgs e)
        {

        }

        //Hàm tạo mẫu biểu kế toán
        internal void TAO_MAU_BIEU_KE_TOAN(string file_mau_bieu)
        {
            //Lấy đường dẫn file template
            string TemplateFileLocation = CommonMethod.TemplateFileLocation(file_mau_bieu);

            //Xác định đường dẫn file xuất ra từ chương trình
            string output_file_name = output_file_path + @"\" + Path.GetFileNameWithoutExtension(TemplateFileLocation) + "_"  + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".docx";
            //MessageBox.Show(output_file_name);
            
            //Tạo mẫu biểu
            CommonMethod.CreateWordDocument(TemplateFileLocation, output_file_name, this.nguon_TTKH, this.dich_TTKH);

            var file = new FileInfo(output_file_name);
            if (File.Exists(output_file_name))
            {
                //Mở file đã tạo
                Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
                Document document = ap.Documents.Open(output_file_name);
                ap.Visible = true;
            }          
        }

        //Lấy thông tin chung của chi nhánh, khách hàng
        internal void LAY_TT_CHUNG()
        {
            nguon_TTKH.Clear();
            dich_TTKH.Clear();

            if (Thongtindangnhap.hs)
            {
                //Đối với trung tâm các chi nhánh
                nguon_TTKH.Add("<CHI_NHANH>");
                dich_TTKH.Add(Thongtindangnhap.tencn);
            }
            else
            {
                //Đối với phòng giao dịch
                nguon_TTKH.Add("<CHI_NHANH>");
                dich_TTKH.Add(Thongtindangnhap.tencn + " - " + Thongtindangnhap.tenpb);
            }

            nguon_TTKH.Add("<DC_CHI_NHANH>");
            dich_TTKH.Add(Thongtindangnhap.diachipb);

            nguon_TTKH.Add("<KH_MAKH>");
            dich_TTKH.Add(txtMaKH.Text);

            nguon_TTKH.Add("<KH_HOTEN>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString());

            nguon_TTKH.Add("<KH_NGAYSINH>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString());

            nguon_TTKH.Add("<KH_NGHENGHIEP>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString());

            nguon_TTKH.Add("<KH_CHUCVU>");
            dich_TTKH.Add("                            ");

            nguon_TTKH.Add("<KH_MST>");
            dich_TTKH.Add("                            ");

            if (dgvDanhsachCN.CurrentRow.Cells["Giới tính"].Value.ToString() == "Nam")
            {
                nguon_TTKH.Add("<KH_GT_NAM>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<KH_GT_NU>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }
            else
            {
                nguon_TTKH.Add("<KH_GT_NAM>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<KH_GT_NU>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }

            nguon_TTKH.Add("<KH_QUOCTICH>");
            dich_TTKH.Add(txtQuoctich.Text);

            nguon_TTKH.Add("<KH_DANTOC>");
            dich_TTKH.Add(txtDantoc.Text);

            nguon_TTKH.Add("<KH_TONGIAO>");
            dich_TTKH.Add(txtTongiao.Text);

            if (rdbNCT_CO.Checked == true)
            {
                nguon_TTKH.Add("<KH_NCT_CO>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<KH_NCT_KHONG>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }
            else
            {
                nguon_TTKH.Add("<KH_NCT_CO>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<KH_NCT_KHONG>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }

            nguon_TTKH.Add("<KH_CMND>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString());

            nguon_TTKH.Add("<KH_NGAYCAPCMND>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString());

            nguon_TTKH.Add("<KH_NOICAPCMND>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString());

            nguon_TTKH.Add("<KH_DTDD1>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString());

            nguon_TTKH.Add("<KH_EMAIL>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["Email"].Value.ToString());

            nguon_TTKH.Add("<KH_DIACHI>");
            dich_TTKH.Add(dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString());

            nguon_TTKH.Add("<KH_DKKD>");
            dich_TTKH.Add(".....");

            nguon_TTKH.Add("<KH_NGAYCAPDKKD>");
            dich_TTKH.Add(".../.../......");

            nguon_TTKH.Add("<KH_NOICAPDKKD>");
            dich_TTKH.Add(".....");

            nguon_TTKH.Add("<KH_SOQDTL>");
            dich_TTKH.Add(".....");

            nguon_TTKH.Add("<KH_MST>");
            dich_TTKH.Add(".....");

            nguon_TTKH.Add("<KH_NGAYCAPMST>");
            dich_TTKH.Add(".../.../......");

            nguon_TTKH.Add("<KH_NOICAPMST>");
            dich_TTKH.Add(".....");

            if (Thongtindangnhap.macn == "2300" || Thongtindangnhap.macn == "2301" || Thongtindangnhap.macn == "2313")
            {
                nguon_TTKH.Add("<DIA_BAN>");
                dich_TTKH.Add("Hải Dương");
            }
            else
            {
                nguon_TTKH.Add("<DIA_BAN>");
                dich_TTKH.Add(Thongtindangnhap.tencn.Substring(25));
            }
                       
            nguon_TTKH.Add("<GDV>");
            dich_TTKH.Add(Thongtindangnhap.tennv);

            nguon_TTKH.Add("<GDV_TENDANGNHAP>");
            dich_TTKH.Add(Thongtindangnhap.user_id);

            nguon_TTKH.Add("<KSV>");
            dich_TTKH.Add(cboxKiemsoat.Text);

            nguon_TTKH.Add("<LANHDAO>");
            dich_TTKH.Add(cboxLanhdao.Text);

            if (chucvu_lanhdao == "Giám đốc")
            {
                nguon_TTKH.Add("<CHUC_DANH1>");
                dich_TTKH.Add("Giám đốc");

                nguon_TTKH.Add("<CHUC_DANH2>");
                dich_TTKH.Add("");
            }
            else if (chucvu_lanhdao == "Phó Giám đốc")
            {
                nguon_TTKH.Add("<CHUC_DANH1>");
                dich_TTKH.Add("KT. Giám đốc");

                nguon_TTKH.Add("<CHUC_DANH2>");
                dich_TTKH.Add("Phó Giám đốc");
            }

            nguon_TTKH.Add("<SDT_AGRIBANK>");
            dich_TTKH.Add(Thongtindangnhap.sdt_pb);

            nguon_TTKH.Add("<FAX_AGRIBANK>");
            dich_TTKH.Add(Thongtindangnhap.fax_pb);

            nguon_TTKH.Add("<NTN_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"));

            nguon_TTKH.Add("<NGAY_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("dd"));

            nguon_TTKH.Add("<THANG_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("MM"));

            nguon_TTKH.Add("<NAM_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("yyyy"));

            nguon_TTKH.Add("<GIO_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("HH"));

            nguon_TTKH.Add("<PHUT_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("mm"));
        }


        //Lấy thông tin mở tài khoản dùng cho mẫu 01TKDVVN - MO TAI KHOAN CA NHAN)
        internal void LAY_TT_MOTK()
        {
            if (chkVND.Checked == true)
            {
                nguon_TTKH.Add("<LTT_VND>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<LTT_VND>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkUSD.Checked == true)
            {
                nguon_TTKH.Add("<LTT_USD>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<LTT_USD>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkEURO.Checked == true)
            {
                nguon_TTKH.Add("<LTT_EUR>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<LTT_EUR>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkKhac.Checked == true)
            {
                nguon_TTKH.Add("<LTT_KHAC>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<LOAITIENTE_KHAC>");
                dich_TTKH.Add(txtLTT_KHAC.Text);
            }
            else
            {
                nguon_TTKH.Add("<LTT_KHAC>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<LOAITIENTE_KHAC>");
                dich_TTKH.Add("");
            }

            if (chkMB_SMS.Checked == true)
            {
                nguon_TTKH.Add("<MB_SMS>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<MB_SMS>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkMB_EMB.Checked == true)
            {
                nguon_TTKH.Add("<MB_EMB>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<MB_EMB>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkMB_BP.Checked == true)
            {
                nguon_TTKH.Add("<MB_BP>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<MB_BP>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkMB_MP.Checked == true)
            {
                nguon_TTKH.Add("<MB_MP>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<MB_MP>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            nguon_TTKH.Add("<CN_DTDD1>");
            dich_TTKH.Add(txtCN_DTDD1.Text);

            nguon_TTKH.Add("<CN_DTDD2>");
            dich_TTKH.Add(txtCN_DTDD2.Text);

            nguon_TTKH.Add("<CN_DTDD3>");
            dich_TTKH.Add(txtCN_DTDD3.Text);

            nguon_TTKH.Add("<CN_DTDD4>");
            dich_TTKH.Add(txtCN_DTDD4.Text);

            nguon_TTKH.Add("<CN_DTDD5>");
            dich_TTKH.Add(txtCN_DTDD5.Text);

            if (chkIB_TAICHINH.Checked == true)
            {
                nguon_TTKH.Add("<IB_TAICHINH>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<IB_TAICHINH>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkIB_THANHTOAN.Checked == true)
            {
                nguon_TTKH.Add("<IB_THANHTOAN>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<IB_THANHTOAN>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkIB_PHITAICHINH.Checked == true)
            {
                nguon_TTKH.Add("<IB_PHITAICHINH>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<IB_PHITAICHINH>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (rdbOTPSoftToken.Checked == true)
            {
                nguon_TTKH.Add("<OTP_SOFT>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<OTP_SOFT>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (rdbOTPHardToken.Checked == true)
            {
                nguon_TTKH.Add("<OTP_HARD>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<OTP_HARD>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (rdbOTPSMSToken.Checked == true)
            {
                nguon_TTKH.Add("<OTP_SMS>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<OTP_SMS>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkNTTD_Nuoc.Checked == true)
            {
                nguon_TTKH.Add("<NTTD_NUOC>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<NTTD_NUOC>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkNTTD_Dien.Checked == true)
            {
                nguon_TTKH.Add("<NTTD_DIEN>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<NTTD_DIEN>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkNTTD_Vienthong.Checked == true)
            {
                nguon_TTKH.Add("<NTTD_VIENTHONG>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<NTTD_VIENTHONG>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkNTTD_Hocphi.Checked == true)
            {
                nguon_TTKH.Add("<NTTD_HOCPHI>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<NTTD_HOCPHI>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkNTTD_Khac.Checked == true)
            {
                nguon_TTKH.Add("<NTTD_KHAC>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<NTTD_NOIDUNGKHAC>");
                dich_TTKH.Add(txtNTTD_Noidungkhac.Text);
            }
            else
            {
                nguon_TTKH.Add("<NTTD_KHAC>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<NTTD_NOIDUNGKHAC>");
                dich_TTKH.Add(txtNTTD_Noidungkhac.Text);
            }

            if (chkThe_tra_truoc.Checked == true)
            {
                nguon_TTKH.Add("<THE_TRA_TRUOC>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<THE_TRA_TRUOC>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTTT_Ghinonoidia.Checked == true)
            {
                nguon_TTKH.Add("<TTT_GHINONOIDIA>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_GHINONOIDIA>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTTT_Lapnghiep.Checked == true)
            {
                nguon_TTKH.Add("<TTT_LAPNGHIEP>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_LAPNGHIEP>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTTT_VisaDebit.Checked == true)
            {
                nguon_TTKH.Add("<TTT_VISADEBIT>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_VISADEBIT>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTTT_MasterCardDebit.Checked == true)
            {
                nguon_TTKH.Add("<TTT_MASTERCARDDEBIT>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_MASTERCARDDEBIT>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTTT_Lienketthuonghieu.Checked == true)
            {
                nguon_TTKH.Add("<TTT_LIENKETTHUONGHIEU>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_LIENKETTHUONGHIEU>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkHangthechuan.Checked == true)
            {
                nguon_TTKH.Add("<HANG_THE_CHUAN>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<HANG_THE_CHUAN>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkHangthevang.Checked == true)
            {
                nguon_TTKH.Add("<HANG_THE_VANG>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<HANG_THE_VANG>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkPhatHanhThuong.Checked == true)
            {
                nguon_TTKH.Add("<PHAT_HANH_THUONG>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<PHAT_HANH_THUONG>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkPhatHanhNhanh.Checked == true)
            {
                nguon_TTKH.Add("<PHAT_HANH_NHANH>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<PHAT_HANH_NHANH>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (rdbTTT_Nhantructiep.Checked == true)
            {
                nguon_TTKH.Add("<TTT_NHANTRUCTIEP>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_NHANTRUCTIEP>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (rdbTTT_UyQuyen.Checked == true)
            {
                nguon_TTKH.Add("<TTT_UYQUYEN>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_UYQUYEN>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            nguon_TTKH.Add("<UQ_HOTEN>");
            dich_TTKH.Add(txtUQ_HoTen.Text);

            nguon_TTKH.Add("<UQ_CMND>");
            dich_TTKH.Add(txtUQ_CMND.Text);

            nguon_TTKH.Add("<UQ_NGAYCAPCMND>");
            dich_TTKH.Add(txtUQ_NgayCapCMND.Text);

            nguon_TTKH.Add("<UQ_NOICAPCMND>");
            dich_TTKH.Add(txtUQ_NoiCapCMND.Text);

            if (chkTTT_BHCT.Checked == true)
            {
                nguon_TTKH.Add("<TTT_BHCT>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_BHCT>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTTT_DKITN.Checked == true)
            {
                nguon_TTKH.Add("<TTT_DKITN>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TTT_DKITN>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            nguon_TTKH.Add("<INTERNET_HMGD>");
            dich_TTKH.Add(txtInternet_HMGD.Text);

            nguon_TTKH.Add("<INTERNET_DTDD>");
            dich_TTKH.Add(txtInternet_DTDD.Text);

            nguon_TTKH.Add("<TBSD_DINHKYGUI>");
            dich_TTKH.Add(txtTBSD_DinhKyGui.Text);

            if (chkTBSD_Quay.Checked == true)
            {
                nguon_TTKH.Add("<TBSD_QUAY>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TBSD_QUAY>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTBSD_Thu.Checked == true)
            {
                nguon_TTKH.Add("<TBSD_THU>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TBSD_THU>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTBSD_Fax.Checked == true)
            {
                nguon_TTKH.Add("<TBSD_FAX>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TBSD_FAX>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkTBSD_Khac.Checked == true)
            {
                nguon_TTKH.Add("<TBSD_KHAC>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TBSD_KHAC>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            nguon_TTKH.Add("<TBSD_HINHTHUCKHAC>");
            dich_TTKH.Add(txtTBSD_HinhThucKhac.Text);

            nguon_TTKH.Add("<NGAY_HIEU_LUC>");
            dich_TTKH.Add(DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"));

            string ccy = "VND";
            if (chkVND.Checked == true)
            {
                ccy = "VND";
            }

            if (chkUSD.Checked == true)
            {
                ccy = "USD";
            }

            if (chkEURO.Checked == true)
            {
                ccy = "EUR";
            }

            if (chkKhac.Checked == true)
            {
                ccy = txtLTT_KHAC.Text;
            }

            nguon_TTKH.Add("<KH_LTT>");
            dich_TTKH.Add(ccy);

            if (chkCongDanMy.Checked == true)
            {
                nguon_TTKH.Add("<CONG_DAN_MY>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<CONG_DAN_MY>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkDauHieuMy.Checked == true)
            {
                nguon_TTKH.Add("<DAU_HIEU_MY>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<DAU_HIEU_MY>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            if (chkNo_CongDanMy.Checked == true)
            {
                nguon_TTKH.Add("<NO_CONG_DAN_MY>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<NO_CONG_DAN_MY>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            nguon_TTKH.Add("<NGAY_MOTK>");
            dich_TTKH.Add(DateTime.Now.ToString("dd"));

            nguon_TTKH.Add("<THANG_MOTK>");
            dich_TTKH.Add(DateTime.Now.ToString("MM"));

            nguon_TTKH.Add("<NAM_MOTK>");
            dich_TTKH.Add(DateTime.Now.ToString("yyyy"));
        }

        //Lấy thông tin đóng tài khoản dùng cho mẫu 06TKDVVN - DONG TAI KHOAN THANH TOAN.docx
        internal void LAY_TT_DONGTK()
        {
            nguon_TTKH.Add("<KH_DONG_STK>");
            dich_TTKH.Add(cboxCN_DONG_TK.Text);

            nguon_TTKH.Add("<KH_DONG_LOAITK>");
            dich_TTKH.Add(cboxDong_LoaiTK.Text);

            nguon_TTKH.Add("<KH_DONG_LTT>");
            dich_TTKH.Add(tk_bus.TAI_KHOAN_THEO_STK(cboxCN_DONG_TK.Text).Rows[0]["CCY"].ToString());

            string ngaydongtk = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
            nguon_TTKH.Add("<KH_NGAYDONGTK>");
            dich_TTKH.Add(ngaydongtk);

            nguon_TTKH.Add("<NGAY_DONGTK>");
            dich_TTKH.Add(DateTime.Now.ToString("dd"));

            nguon_TTKH.Add("<THANG_DONGTK>");
            dich_TTKH.Add(DateTime.Now.ToString("MM"));

            nguon_TTKH.Add("<NAM_DONGTK>");
            dich_TTKH.Add(DateTime.Now.ToString("yyyy"));

            nguon_TTKH.Add("<LYDODONGTK>");
            dich_TTKH.Add(txtLyDoDongTK.Text);

            nguon_TTKH.Add("<XULYSODU>");
            dich_TTKH.Add(txtXuLySoDu.Text);
        }


        //Lấy thông tin thay đổi thông tin dùng cho mẫu 07TKDVVN - CHINH SUA BO SUNG THONG TIN KHACH HANG CA NHAN 
        //và 09TKDVVN - CHINH SUA BO SUNG THONG TIN KHACH HANG DANH CHO NGAN HANG
        internal void LAY_TT_TDTT()
        {
            //Thay đổi thông tin khách hàng cá nhân
            string ngaytdtt = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
            nguon_TTKH.Add("<KH_NGAY_TDTT>");
            dich_TTKH.Add(ngaytdtt);

            nguon_TTKH.Add("<NGAY_TDTT>");
            dich_TTKH.Add(DateTime.Now.ToString("dd"));

            nguon_TTKH.Add("<THANG_TDTT>");
            dich_TTKH.Add(DateTime.Now.ToString("MM"));

            nguon_TTKH.Add("<NAM_TDTT>");
            dich_TTKH.Add(DateTime.Now.ToString("yyyy"));

            nguon_TTKH.Add("<TDTT_KH_MAKH>");
            dich_TTKH.Add(txtTDTT_MAKH.Text);

            nguon_TTKH.Add("<TDTT_KH_HOTEN>");
            dich_TTKH.Add(txtTDTT_KH_HOTEN.Text);

            if (chkTDTT_LoaiKH.Checked == true)
            {
                nguon_TTKH.Add("<TDTT_LOAIKH>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<TDTT_LOAIKH_CU>");
                dich_TTKH.Add(txtTDTT_LoaiKH_cu.Text);

                nguon_TTKH.Add("<TDTT_LOAIKH_MOI>");
                dich_TTKH.Add(txtTDTT_LoaiKH_moi.Text);
            }
            else
            {
                nguon_TTKH.Add("<TDTT_LOAIKH>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<TDTT_LOAIKH_CU>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<TDTT_LOAIKH_MOI>");
                dich_TTKH.Add("");
            }

            if (chkTDTT_HoTen_Viet.Checked == true)
            {
                nguon_TTKH.Add("<TDTT_HOTEN>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<TDTT_HOTENVIET_CU>");
                dich_TTKH.Add(txtTDTT_HoTenViet_cu.Text);

                nguon_TTKH.Add("<TDTT_HOTENVIET_MOI>");
                dich_TTKH.Add(txtTDTT_HoTenViet_moi.Text);
            }
            else
            {
                nguon_TTKH.Add("<TDTT_HOTEN>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<TDTT_HOTENVIET_CU>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<TDTT_HOTENVIET_MOI>");
                dich_TTKH.Add("");
            }

            if (chkTDTT_CMND.Checked == true)
            {
                nguon_TTKH.Add("<TDTT_CMND>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<TDTT_CMND_CU_DAY_DU>");
                dich_TTKH.Add("Số CMND/Hộ chiếu/Căn cước: " + txtTDTT_CMND_cu.Text + ", Ngày cấp: " + txtTDTT_NgayCapCMND_cu.Text + ", Nơi cấp: " + txtTDTT_NoiCapCMND_cu.Text);

                nguon_TTKH.Add("<TDTT_CMND_MOI>");
                dich_TTKH.Add(txtTDTT_CMND_moi.Text);

                nguon_TTKH.Add("<TDTT_NGAYCAPCMND_MOI>");
                dich_TTKH.Add(txtTDTT_NgayCapCMND_moi.Text);

                nguon_TTKH.Add("<TDTT_NOICAPCMND_MOI>");
                dich_TTKH.Add(txtTDTT_NoiCapCMND_moi.Text);

                nguon_TTKH.Add("<TDTT_CMND_MOI_DAY_DU>");
                dich_TTKH.Add("Số CMND/Hộ chiếu/Căn cước: " + txtTDTT_CMND_moi.Text + ", Ngày cấp: " + txtTDTT_NgayCapCMND_moi.Text + ", Nơi cấp: " + txtTDTT_NoiCapCMND_moi.Text);

                nguon_TTKH.Add("<TDTT_HOSO1>");
                dich_TTKH.Add("1. CMND/Hộ chiếu/Thẻ căn cước, Số " + txtTDTT_CMND_cu.Text + ", Ngày cấp: " + txtTDTT_NgayCapCMND_cu.Text + ", Nơi cấp: " + txtTDTT_NoiCapCMND_cu.Text);

                nguon_TTKH.Add("<TDTT_HOSO2>");
                dich_TTKH.Add("2. CMND/Hộ chiếu/Thẻ căn cước, Số " + txtTDTT_CMND_moi.Text + ", Ngày cấp: " + txtTDTT_NgayCapCMND_moi.Text + ", Nơi cấp: " + txtTDTT_NoiCapCMND_moi.Text);
            }
            else
            {
                nguon_TTKH.Add("<TDTT_CMND_CU_DAY_DU>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<TDTT_CMND_MOI_DAY_DU>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<TDTT_CMND>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<TDTT_CMND_MOI>");
                dich_TTKH.Add("................");

                nguon_TTKH.Add("<TDTT_NGAYCAPCMND_MOI>");
                dich_TTKH.Add("................");

                nguon_TTKH.Add("<TDTT_NOICAPCMND_MOI>");
                dich_TTKH.Add("................");

                nguon_TTKH.Add("<TDTT_HOSO1>");
                dich_TTKH.Add("1. CMND/Hộ chiếu/Căn cước, Số " + txtTDTT_CMND_cu.Text + ", Ngày cấp: " + txtTDTT_NgayCapCMND_cu.Text + ", Nơi cấp: " + txtTDTT_NoiCapCMND_cu.Text);

                nguon_TTKH.Add("<TDTT_HOSO2>");
                dich_TTKH.Add("");
            }

            if (chkTDTT_DiaChi.Checked == true || chkTDTT_Dienthoai.Checked == true)
            {
                nguon_TTKH.Add("<TDTT_LIENLAC>");
                dich_TTKH.Add(((char)0x2611).ToString());

                if (chkTDTT_DiaChi.Checked == true)
                {
                    nguon_TTKH.Add("<TDTT_DIACHI_MOI>");
                    dich_TTKH.Add(txtTDTT_DiaChi_moi.Text);
                }
                else
                {
                    nguon_TTKH.Add("<TDTT_DIACHI_MOI>");
                    dich_TTKH.Add(txtTDTT_DiaChi_cu.Text);
                }

                if (chkTDTT_Dienthoai.Checked == true)
                {
                    nguon_TTKH.Add("<TDTT_DIENTHOAI_MOI>");
                    dich_TTKH.Add(txtTDTT_DienThoai_moi.Text);
                }
                else
                {
                    nguon_TTKH.Add("<TDTT_DIENTHOAI_MOI>");
                    dich_TTKH.Add(txtTDTT_DienThoai_cu.Text);
                }
            }
            else
            {
                nguon_TTKH.Add("<TDTT_LIENLAC>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<TDTT_DIACHI_MOI>");
                dich_TTKH.Add("................");

                nguon_TTKH.Add("<TDTT_DIENTHOAI_MOI>");
                dich_TTKH.Add("................");
            }

            if (chkTDTT_Khac.Checked == true)
            {
                nguon_TTKH.Add("<TDTT_KHAC>");
                dich_TTKH.Add(((char)0x2611).ToString());

                nguon_TTKH.Add("<TDTT_KHAC_MOI>");
                dich_TTKH.Add(txtTDTT_Khac.Text);
            }
            else
            {
                nguon_TTKH.Add("<TDTT_KHAC>");
                dich_TTKH.Add(((char)0x2610).ToString());

                nguon_TTKH.Add("<TDTT_KHAC_MOI>");
                dich_TTKH.Add("");
            }

            if (chkTDTT_ChuKy.Checked == true)
            {
                nguon_TTKH.Add("<TDTT_CHUKY>");
                dich_TTKH.Add(((char)0x2611).ToString());
            }
            else
            {
                nguon_TTKH.Add("<TDTT_CHUKY>");
                dich_TTKH.Add(((char)0x2610).ToString());
            }

            nguon_TTKH.Add("<TEN_CHI_NHANH_GOC>");
            dich_TTKH.Add(txtChinhanhgoc.Text);
        }

        //Thay đổi thông tin khách hàng trong cơ sở dữ liệu
        internal void UPDATE_TTKH()
        {
            if (chkTDTT_HoTen_Viet.Checked == true && txtTDTT_HoTenViet_moi.Text != "")
            {
                //Cập nhật tên vào cơ sở dữ liệu
            }
            if (chkTDTT_CMND.Checked == true && txtTDTT_CMND_moi.Text != "" && txtTDTT_NgayCapCMND_moi.Text != "" && txtTDTT_NoiCapCMND_moi.Text != "")
            {
                //Cập nhật thông tin CMND mới vào cơ sở dữ liệu
            }
            if (chkTDTT_DiaChi.Checked == true && txtTDTT_DiaChi_moi.Text != "")
            {
                //Cập nhật thông tin địa chỉ mới vào cơ sở dữ liệu
            }
            if (chkTDTT_Dienthoai.Checked == true && txtTDTT_DienThoai_moi.Text != "")
            {
                //Cập nhật thông tin điện thoại mới vào cơ sở dữ liệu
            }
        }

        //Lấy thông tin ủy quyền mua ngoại tệ dùng cho mẫu 01VBAHD - UY QUYEN MUA NGOAI TE.docx
        internal void LAY_TT_UQMNT()
        {
            nguon_TTKH.Add("<KH_UQMNT_STK>");
            dich_TTKH.Add(cboxUQMNT_TKKH.Text);

            nguon_TTKH.Add("<UQMNT_DAIDIENNH>");
            dich_TTKH.Add(txtUQMNT_DAIDIENNH.Text);

            nguon_TTKH.Add("<UQMNT_CHUCVUDAIDIENNH>");
            dich_TTKH.Add(txtUQMNT_CHUCVUDAIDIENNH.Text);

            if (txtUQMNT_GUQ.Text == "")
            {
                nguon_TTKH.Add("<UQMNT_GUQ>");
                dich_TTKH.Add("");
            }
            else
            {
                nguon_TTKH.Add("<UQMNT_GUQ>");
                dich_TTKH.Add(txtUQMNT_GUQ.Text);
            }           

            nguon_TTKH.Add("<UQMNT_DAIDIENNH_CHUKY>");
            dich_TTKH.Add(cboxLanhdao.Text);
        }

        //Lấy thông tin ủy quyền sử dụng tài khoản dùng cho mẫu 02/VBAHD - UY QUYEN SU DUNG TAI KHOAN.docx
        internal void LAY_TT_UQSDTK()
        {
            nguon_TTKH.Add("<UQSDTK_KH_MAKH>");
            dich_TTKH.Add(txtUQSDTK_KH_MAKH.Text);

            nguon_TTKH.Add("<UQSDTK_KH_HOTEN>");
            dich_TTKH.Add(txtUQSDTK_KH_HOTEN.Text);

            nguon_TTKH.Add("<UQSDTK_KH_HOTEN>");
            dich_TTKH.Add(txtUQSDTK_KH_HOTEN.Text);

            nguon_TTKH.Add("<UQSDTK_KH_NGAYSINH>");
            dich_TTKH.Add(txtUQSDTK_KH_NGAYSINH.Text);

            nguon_TTKH.Add("<UQSDTK_KH_DTDD>");
            dich_TTKH.Add(txtUQSDTK_KH_DTDD.Text);

            nguon_TTKH.Add("<UQSDTK_KH_NGHENGHIEP>");
            dich_TTKH.Add(txtUQSDTK_KH_NGHENGHIEP.Text);

            nguon_TTKH.Add("<UQSDTK_KH_CMND>");
            dich_TTKH.Add(txtUQSDTK_KH_CMND.Text);

            nguon_TTKH.Add("<UQSDTK_KH_NGAYCAPCMND>");
            dich_TTKH.Add(txtUQSDTK_KH_NGAYCAPCMND.Text);

            nguon_TTKH.Add("<UQSDTK_KH_NOICAPCMND>");
            dich_TTKH.Add(txtUQSDTK_KH_NOICAPCMND.Text);

            nguon_TTKH.Add("<UQSDTK_KH_DIACHI>");
            dich_TTKH.Add(txtUQSDTK_KH_DIACHI.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_HOTEN>");
            dich_TTKH.Add(txtDUQSDTK_KH_HOTEN.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_HOTEN>");
            dich_TTKH.Add(txtDUQSDTK_KH_HOTEN.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_NGAYSINH>");
            dich_TTKH.Add(txtDUQSDTK_KH_NGAYSINH.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_DTDD>");
            dich_TTKH.Add(txtDUQSDTK_KH_DTDD.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_NGHENGHIEP>");
            dich_TTKH.Add(txtDUQSDTK_KH_NGHENGHIEP.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_CMND>");
            dich_TTKH.Add(txtDUQSDTK_KH_CMND.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_NGAYCAPCMND>");
            dich_TTKH.Add(txtDUQSDTK_KH_NGAYCAPCMND.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_NOICAPCMND>");
            dich_TTKH.Add(txtDUQSDTK_KH_NOICAPCMND.Text);

            nguon_TTKH.Add("<DUQSDTK_KH_DIACHI>");
            dich_TTKH.Add(txtDUQSDTK_KH_DIACHI.Text);

            nguon_TTKH.Add("<KH_UQSDTK_STK>");
            dich_TTKH.Add(cboxUQSDTK_STK.Text);
           
            if (rdbUQSDTK_HL_Macdinh.Checked == true)
            {
                string hlmd = "Từ ngày ";
                hlmd += DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                hlmd += " cho đến khi ";
                hlmd += Thongtindangnhap.tencn;
                hlmd += " nhận được, xác nhận hợp lệ đối với văn bản về việc hủy/chấm dứt/thay đổi việc ủy quyền và không bị giới hạn hiệu lực bởi thời hạn (01) năm theo quy định tại Điều 582 Bộ luật dân sự.";
                nguon_TTKH.Add("<UQSDTK_HIEU_LUC_MAC_DINH>");
                dich_TTKH.Add(hlmd);

                nguon_TTKH.Add("<UQSDTK_HIEU_LUC_TU_NGAY>");
                dich_TTKH.Add("");
            }
            else
            {
                nguon_TTKH.Add("<UQSDTK_HIEU_LUC_MAC_DINH>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<UQSDTK_HIEU_LUC_TU_NGAY>");
                dich_TTKH.Add("Từ ngày: "+txtUQSDTK_HL_Tungay.Text + ".");
            }
        }

        //Lấy thông tin ủy quyền giao dịch tài khoản tiết kiệm dùng cho mẫu 03/VBAHD - 03VBAHD - UY QUYEN GIAO DICH TAI KHOAN TIET KIEM.docx
        internal void LAY_TT_UQGDTKTK()
        {
            nguon_TTKH.Add("<UQGDTKTK_KH_MAKH>");
            dich_TTKH.Add(txtUQGDTKTK_KH_MAKH.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_HOTEN>");
            dich_TTKH.Add(txtUQGDTKTK_KH_HOTEN.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_NGAYSINH>");
            dich_TTKH.Add(txtUQGDTKTK_KH_NGAYSINH.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_DTDD>");
            dich_TTKH.Add(txtUQGDTKTK_KH_DTDD.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_NGHENGHIEP>");
            dich_TTKH.Add(txtUQGDTKTK_KH_NGHENGHIEP.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_CMND>");
            dich_TTKH.Add(txtUQGDTKTK_KH_CMND.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_NGAYCAPCMND>");
            dich_TTKH.Add(txtUQGDTKTK_KH_NGAYCAPCMND.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_NOICAPCMND>");
            dich_TTKH.Add(txtUQGDTKTK_KH_NOICAPCMND.Text);

            nguon_TTKH.Add("<UQGDTKTK_KH_DIACHI>");
            dich_TTKH.Add(txtUQGDTKTK_KH_DIACHI.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_HOTEN>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_HOTEN.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_NGAYSINH>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_NGAYSINH.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_DTDD>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_DTDD.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_NGHENGHIEP>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_NGHENGHIEP.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_CMND>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_CMND.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_NGAYCAPCMND>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_NGAYCAPCMND.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_NOICAPCMND>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_NOICAPCMND.Text);

            nguon_TTKH.Add("<DUQGDTKTK_KH_DIACHI>");
            dich_TTKH.Add(txtDUQGDTKTK_KH_DIACHI.Text);

            if (chkUQGDTKTK_TK1.Checked == true)
            {
                string sotk1 = cboxUQGDTKTK_TK1.Text;
                string serial1 = txtUQGDTKTK_Serial_TK1.Text;
                string sotien1 = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK1.Text)));
                string sotienbangchu1 = CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK1.Text)));
                nguon_TTKH.Add("<UQGDTKTK_TK1>");
                dich_TTKH.Add("1. Số TK: " + sotk1 + "   Số Sêri: " + serial1 + "   Số tiền: " + sotien1 + " đồng.");

                nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU1>");
                dich_TTKH.Add("(Bằng chữ: " + sotienbangchu1 + "đồng).");

            }
            else
            {
                nguon_TTKH.Add("<UQGDTKTK_TK1>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU1>");
                dich_TTKH.Add("");
            }

            if (chkUQGDTKTK_TK2.Checked == true)
            {
                string sotk2 = cboxUQGDTKTK_TK2.Text;
                string serial2 = txtUQGDTKTK_Serial_TK2.Text;
                string sotien2 = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK2.Text)));
                string sotienbangchu2 = CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK2.Text)));
                nguon_TTKH.Add("<UQGDTKTK_TK2>");
                dich_TTKH.Add("2. Số TK: " + sotk2 + "   Số Sêri: " + serial2 + "   Số tiền: " + sotien2 + " đồng.");

                nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU2>");
                dich_TTKH.Add("(Bằng chữ: " + sotienbangchu2 + "đồng).");

            }
            else
            {
                nguon_TTKH.Add("<UQGDTKTK_TK2>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU2>");
                dich_TTKH.Add("");
            }

            if (chkUQGDTKTK_TK3.Checked == true)
            {
                string sotk3 = cboxUQGDTKTK_TK3.Text;
                string serial3 = txtUQGDTKTK_Serial_TK3.Text;
                string sotien3 = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK3.Text)));
                string sotienbangchu3 = CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK3.Text)));
                nguon_TTKH.Add("<UQGDTKTK_TK3>");
                dich_TTKH.Add("3. Số TK: " + sotk3 + "   Số Sêri: " + serial3 + "   Số tiền: " + sotien3 + " đồng.");

                nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU3>");
                dich_TTKH.Add("(Bằng chữ: " + sotienbangchu3 + "đồng).");

            }
            else
            {
                nguon_TTKH.Add("<UQGDTKTK_TK3>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU3>");
                dich_TTKH.Add("");
            }

            nguon_TTKH.Add("<UQGDTKTK_LY_DO>");
            dich_TTKH.Add(txtUQGDTKTK_LY_DO.Text);

            if (rdbUQGDTKTK_HIEU_LUC_MAC_DINH.Checked == true)
            {
                string hlmd = "Từ ngày ";
                hlmd += DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                hlmd += " cho đến khi ";
                hlmd += Thongtindangnhap.tencn;
                hlmd += " nhận được, xác nhận hợp lệ đối với văn bản về việc hủy/chấm dứt/thay đổi việc ủy quyền và không bị giới hạn hiệu lực bởi thời hạn (01) năm theo quy định tại Điều 582 Bộ luật dân sự.";
                nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_MAC_DINH>");
                dich_TTKH.Add(hlmd);

                nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_TU_NGAY>");
                dich_TTKH.Add("");
            }
            else
            {
                nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_MAC_DINH>");
                dich_TTKH.Add("");

                nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_TU_NGAY>");
                dich_TTKH.Add("Từ ngày: " + txtUQGDTKTK_HIEU_LUC_TU_NGAY.Text + " đến ngày " + txtUQGDTKTK_HIEU_LUC_TU_NGAY.Text + ".");
            }
        }
        internal void MO_TAIKHOAN()
        {
            string ccy = "VND";
            if (chkVND.Checked == true)
            {
                ccy = "VND";
            }

            if (chkUSD.Checked == true)
            {
                ccy = "USD";
            }

            if (chkEURO.Checked == true)
            {
                ccy = "EUR";
            }

            if(chkKhac.Checked == true)
            {
                ccy = txtLTT_KHAC.Text;
            }

            string ngaymo = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
            if (tk_bus.MO_TAIKHOAN(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString(),txtCN_MO_TK.Text, ccy, ngaymo) == true)
            { 
                MessageBox.Show("Đã thêm tài khoản mới");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm tài khoản mới");
            }                     
        }

        internal void DONG_TAIKHOAN()
        {
            string ngaydong = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
            bool dongtk = tk_bus.DONG_TAIKHOAN(cboxCN_DONG_TK.Text, ngaydong);
        }
        private void btnTaomaubieu_Click(object sender, EventArgs e)
        {
            if (cboxMaubieu.Text.Contains(@"01/TKDV.vn"))
            {
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Chưa chọn khách hàng nào.");
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin mở tài khoản
                LAY_TT_MOTK();

            }
            else if (cboxMaubieu.Text.Contains(@"06/TKDV.vn"))
            {
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Chưa chọn khách hàng nào.");
                    return;
                }

                //Kiểm tra đã nhập đủ thông tin hay chưa
                tctTTKHCN.SelectedTab = tpDongtaikhoan;
                if (txtLyDoDongTK.Text == "")
                {
                    MessageBox.Show("Chưa nhập lý do đóng tài khoản");
                    tctTTKHCN.SelectedTab = tpDongtaikhoan;
                    txtLyDoDongTK.Focus();
                    return;
                }

                if (txtXuLySoDu.Text == "")
                {
                    MessageBox.Show("Chưa nhập hình thức xử lý số dư trong tài khoản.");
                    tctTTKHCN.SelectedTab = tpDongtaikhoan;
                    txtXuLySoDu.Focus();
                    return;
                }

                if (cboxCN_DONG_TK.Text == "")
                {
                    MessageBox.Show("Khách hàng hiện không có tài khoản nào đang hoạt động");
                    return;
                }
                //Khách hàng đóng tài khoản
                if (MessageBox.Show("Số tài khoản "+ cboxCN_DONG_TK.Text +" sẽ bị đóng. Bạn có chắc muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Lấy thông tin chung
                    LAY_TT_CHUNG();

                    //Lấy thông tin đóng tài khoản
                    LAY_TT_DONGTK();

                    //Cập nhật tình trạng tài khoản trong bảng TAIKHOAN nếu thuộc phạm vi chi nhánh
                    if (cboxCN_DONG_TK.Text.Substring(0,4) == Thongtindangnhap.macn)
                    {
                        DONG_TAIKHOAN();
                    }                    
                }
                else
                {
                    return;
                }               
            }
            else if (cboxMaubieu.Text.Contains(@"07/TKDV.vn") || cboxMaubieu.Text.Contains(@"09/TKDV.vn"))
            {
                tctTTKHCN.SelectedTab = tpThaydoiTTKH;
                if (txtTDTT_MAKH.Text == "")
                {
                    MessageBox.Show("Chưa chọn khách hàng nào.");
                    txtTDTT_MAKH.Focus();
                    return;
                }               
                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin thay đổi thông tin
                LAY_TT_TDTT();

                //Cập nhật thông tin mới vào cơ sở dữ liệu
                //UPDATE_TTKH();
            }
            else if (cboxMaubieu.Text.Contains(@"01/VBAHD"))
            {
                tctTTKHCN.SelectedTab = tpUyquyenmuaNT;
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Chưa chọn khách hàng nào.");
                    return;
                }
              
                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin ủy quyền mua ngoại tệ
                LAY_TT_UQMNT();
            }
            else if (cboxMaubieu.Text.Contains(@"02/VBAHD"))
            {
                tctTTKHCN.SelectedTab = tpUyquyensudungTK;
                if (txtUQSDTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa chọn khách hàng nào.");
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin ủy quyền sử dụng tài khoản
                LAY_TT_UQSDTK();
            }
            else if (cboxMaubieu.Text.Contains(@"03/VBAHD"))
            {
                tctTTKHCN.SelectedTab = tpUQGDTKTK;
                if (txtUQGDTKTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa chọn khách hàng nào.");
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin ủy quyền giao dịch tài khoản tiết kiệm
                LAY_TT_UQGDTKTK();
            }
            //Tạo mẫu biểu
            string file_mau_bieu = cboxMaubieu.SelectedValue.ToString();
            this.TAO_MAU_BIEU_KE_TOAN(file_mau_bieu);
        }

        private void dgvDanhsachCN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {   
                //Điền thông tin vào tabPage Thông tin chung 1
                txtMaKH.Text = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtTDTT_MAKH.Text = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtTDTT_KH_HOTEN.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtTenKH.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtCN_DTDD1.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();

                //Điền thông tin vào tabPage Thay đổi thông tin
                txtTDTT_LoaiKH_cu.Text = dgvDanhsachCN.CurrentRow.Cells["Loại KH"].Value.ToString();
                txtTDTT_HoTenViet_cu.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtTDTT_CMND_cu.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
                txtTDTT_NgayCapCMND_cu.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtTDTT_NoiCapCMND_cu.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                txtTDTT_DiaChi_cu.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                txtTDTT_DienThoai_cu.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                //txtMobile.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                //txtTel.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT nhà"].Value.ToString();
                //dtpNgaysinh.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                //cbbGioitinh.Text = dgvDanhsachCN.CurrentRow.Cells["Giới tính"].Value.ToString();
                //cbKH2890.Text = dgvDanhsachCN.CurrentRow.Cells["Đối tượng KH"].Value.ToString();
                //txtAddress.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                //txtAddress2.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ khác"].Value.ToString();
                //txtEmail.Text = dgvDanhsachCN.CurrentRow.Cells["Email"].Value.ToString();

                //txtWebsite.Text = dgvDanhsachCN.CurrentRow.Cells["Website"].Value.ToString();
                //txtNHGD.Text = dgvDanhsachCN.CurrentRow.Cells["NH giao dịch"].Value.ToString();
                //txtSothich.Text = dgvDanhsachCN.CurrentRow.Cells["Sở thích"].Value.ToString();
                //cbbTinhtrang.Text = dgvDanhsachCN.CurrentRow.Cells["Tình trạng"].Value.ToString();
                //txtThunhap.Text = dgvDanhsachCN.CurrentRow.Cells["Thu nhập"].Value.ToString();
                //txtMaNV.Text = dgvDanhsachCN.CurrentRow.Cells["Tên đ.nhập"].Value.ToString();

                //txtCMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
                
                //dtpNgaycap.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                //txtNoicap.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                //dtpNgayKH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày kết hôn"].Value.ToString();
                //txtChitiet.Text = dgvDanhsachCN.CurrentRow.Cells["Chi tiết"].Value.ToString();
                //txtGhichu.Text = dgvDanhsachCN.CurrentRow.Cells["Ghi chú"].Value.ToString();

                //cbbTinh.Text = dgvDanhsachCN.CurrentRow.Cells["Tỉnh"].Value.ToString();
                //layDS_Huyen();
                //cbbHuyen.Text = dgvDanhsachCN.CurrentRow.Cells["Huyện"].Value.ToString();
                //layDS_Xa();
                //cbbXa.Text = dgvDanhsachCN.CurrentRow.Cells["Xã"].Value.ToString();
                //cbbLinhvuc.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                //cbbLoaiKHIpcas.Text = dgvDanhsachCN.CurrentRow.Cells["Loại KH"].Value.ToString();

                //Điền thông tin vào tabPage Đóng tài khoản
                //Lấy danh sách số tài khoản tương ứng với mã khách hàng đã chọn gán vào cboxCN_STK
                cboxCN_DONG_TK.DataSource = null;
                cboxCN_DONG_TK.Items.Clear();
                System.Data.DataTable dt_stk = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString());                
                cboxCN_DONG_TK.DataSource = dt_stk;
                cboxCN_DONG_TK.DisplayMember = "SOTK";              
                cboxCN_DONG_TK.Refresh();

                //Điền thông tin vào tabPage Ủy quyền mua bán ngoại tệ               
                //Bên ủy quyền
                txtUQMNT_HOTENKH.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString(); ;
                txtUQMNT_NGAYSINHKH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                txtUQMNT_DTKH.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                txtUQMNT_NGHENGHIEPKH.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                txtUQMNT_CMNDKH.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString(); ;
                txtUQMNT_NGAYCAPCMNDKH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtUQMNT_NOICAPCMNDKH.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                txtUQMNT_DCKH.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                cboxUQMNT_TKKH.DataSource = null;
                cboxUQMNT_TKKH.Items.Clear();
                //System.Data.DataTable dt_stk = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString());
                cboxUQMNT_TKKH.DataSource = dt_stk;
                cboxUQMNT_TKKH.DisplayMember = "SOTK";
                cboxUQMNT_TKKH.Refresh();

                //Bên được ủy quyền
                if (Thongtindangnhap.hs)
                {
                    //Đối với trung tâm các chi nhánh
                    txtUQMNT_CHINHANH.Text = Thongtindangnhap.tencn;
                    txtUQMNT_DIACHINH.Text = Thongtindangnhap.diachicn;
                }
                else
                {
                    //Đối với phòng giao dịch
                    txtUQMNT_CHINHANH.Text = Thongtindangnhap.tencn + " - " + Thongtindangnhap.tenpb;
                    txtUQMNT_DIACHINH.Text = Thongtindangnhap.diachipb;
                }
                System.Data.DataTable nhanvien = nv_bus.NHAN_VIEN_THEO_MANV(cboxLanhdao.SelectedValue.ToString());
                if (Convert.ToBoolean(nhanvien.Rows[0]["GIOITINH"].ToString()) == false)
                {
                    txtUQMNT_DAIDIENNH.Text = "Bà " + cboxLanhdao.Text;
                }
                else
                {
                    txtUQMNT_DAIDIENNH.Text = "Ông " + cboxLanhdao.Text;
                }

                txtUQMNT_CHUCVUDAIDIENNH.Text = nhanvien.Rows[0]["CHUCVU"].ToString();

                txtUQMNT_GUQ.Text = nhanvien.Rows[0]["UYQUYEN"].ToString();          
            }
            catch { }
        }

        private void rdbTTT_UyQuyen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTTT_UyQuyen.Checked == true)
            {
                txtUQ_CMND.Enabled = true;
                txtUQ_HoTen.Enabled = true;
                txtUQ_NgayCapCMND.Enabled = true;
                txtUQ_NoiCapCMND.Enabled = true;
            }
            else
            {
                txtUQ_CMND.Enabled = false;
                txtUQ_HoTen.Enabled = false;
                txtUQ_NgayCapCMND.Enabled = false;
                txtUQ_NoiCapCMND.Enabled = false;

                txtUQ_CMND.Text = ".....................";
                txtUQ_HoTen.Text = ".....................";
                txtUQ_NgayCapCMND.Text = ".....................";
                txtUQ_NoiCapCMND.Text = ".....................";
            }
        }

        private void chkTTT_DKITN_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTTT_DKITN.Checked == true)
            {
                txtInternet_DTDD.Enabled = true;
                txtInternet_HMGD.Enabled = true;

                txtInternet_DTDD.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
            }
            else
            {
                txtInternet_DTDD.Enabled = false;
                txtInternet_HMGD.Enabled = false;

                txtInternet_DTDD.Text = ".....................";
                txtInternet_HMGD.Text = ".....................";
            }
        }

        private void chkTDTT_LoaiKH_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTDTT_LoaiKH.Checked == true)
            {
                txtTDTT_LoaiKH_cu.Enabled = true;
                txtTDTT_LoaiKH_moi.Enabled = true;
            }
            else
            {
                txtTDTT_LoaiKH_cu.Enabled = false;
                //txtTDTT_LoaiKH_cu.Clear();
                txtTDTT_LoaiKH_moi.Enabled = false;
                txtTDTT_LoaiKH_moi.Clear();
            }
        }

        private void chkTDTT_HoTen_Viet_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTDTT_HoTen_Viet.Checked == true)
            {
                txtTDTT_HoTenViet_cu.Enabled = true;
                txtTDTT_HoTenViet_moi.Enabled = true;
            }
            else
            {
                txtTDTT_HoTenViet_cu.Enabled = false;
                //txtTDTT_HoTenViet_cu.Clear();
                txtTDTT_HoTenViet_moi.Enabled = false;
                txtTDTT_HoTenViet_moi.Clear();
            }
        }

        private void chkTDTT_CMND_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTDTT_CMND.Checked == true)
            {
                txtTDTT_CMND_cu.Enabled = true;
                txtTDTT_NgayCapCMND_cu.Enabled = true;
                txtTDTT_NoiCapCMND_cu.Enabled = true;
                txtTDTT_CMND_moi.Enabled = true;
                txtTDTT_NgayCapCMND_moi.Enabled = true;
                txtTDTT_NoiCapCMND_moi.Enabled = true;
            }
            else
            {
                txtTDTT_CMND_cu.Enabled = false;
                //txtTDTT_CMND_cu.Clear();
                txtTDTT_NgayCapCMND_cu.Enabled = false;
                //txtTDTT_NgayCapCMND_cu.Clear();
                txtTDTT_NoiCapCMND_cu.Enabled = false;
                //txtTDTT_NoiCapCMND_cu.Clear();
                txtTDTT_CMND_moi.Enabled = false;
                txtTDTT_NgayCapCMND_moi.Enabled = false;
                txtTDTT_NoiCapCMND_moi.Enabled = false;

                txtTDTT_CMND_moi.Clear();
                txtTDTT_NgayCapCMND_moi.Clear();
                txtTDTT_NoiCapCMND_moi.Clear();
            }
        }

        private void chkTDTT_DiaChi_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTDTT_DiaChi.Checked == true)
            {
                txtTDTT_DiaChi_cu.Enabled = true;
                txtTDTT_DiaChi_moi.Enabled = true;
            }
            else
            {
                txtTDTT_DiaChi_cu.Enabled = false;
                //txtTDTT_DiaChi_cu.Clear();
                txtTDTT_DiaChi_moi.Enabled = false;
                txtTDTT_DiaChi_moi.Clear();
            }
        }

        private void chkTDTT_Dienthoai_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTDTT_Dienthoai.Checked == true)
            {
                txtTDTT_DienThoai_cu.Enabled = true;
                txtTDTT_DienThoai_moi.Enabled = true;
            }
            else
            {
                txtTDTT_DienThoai_cu.Enabled = false;
                //txtTDTT_DienThoai_cu.Clear();
                txtTDTT_DienThoai_moi.Enabled = false;
                txtTDTT_DienThoai_moi.Clear();
            }
        }

        private void chkTDTT_Khac_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTDTT_Khac.Checked == true)
            {
                txtTDTT_Khac.Enabled = true;
            }
            else
            {
                txtTDTT_Khac.Enabled = false;
                txtTDTT_Khac.Clear();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (txtCN_MO_TK.Text == "")
            {
                MessageBox.Show("Chưa nhập số tài khoản mới. Đề nghị kiểm tra lại");
                tctTTKHCN.SelectedTab = tpThongtinchung1;
                txtCN_MO_TK.Focus();
                return;
            }
            if (txtCN_MO_TK.Text.Substring(0,4) != Thongtindangnhap.macn)
            {
                MessageBox.Show("Không thể cập nhật tài khoản khác chi nhánh");
                tctTTKHCN.SelectedTab = tpThongtinchung1;
                txtCN_MO_TK.Focus();
                return;
            }
            //Cập nhật tài khoản mới vào bảng TAIKHOAN
            MO_TAIKHOAN();
        }

        private void cboxLanhdao_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable nhanvien = nv_bus.NHAN_VIEN_THEO_MANV(cboxLanhdao.SelectedValue.ToString());
            if (nhanvien.Rows.Count > 0)
            {
                if (Convert.ToBoolean(nhanvien.Rows[0]["GIOITINH"].ToString()) == false)
                {
                    txtUQMNT_DAIDIENNH.Text = "Bà " + cboxLanhdao.Text;
                }
                else
                {
                    txtUQMNT_DAIDIENNH.Text = "Ông " + cboxLanhdao.Text;
                }

                txtUQMNT_CHUCVUDAIDIENNH.Text = nhanvien.Rows[0]["CHUCVU"].ToString();
                //if (nhanvien.Rows[0]["CHUCVU"].ToString() == "Branch General Manager")
                //{
                //    txtUQMNT_CHUCVUDAIDIENNH.Text = "Giám đốc";
                //}
                //else if (nhanvien.Rows[0]["CHUCVU"].ToString() == "Branch Vice Manager")
                //{
                //    txtUQMNT_CHUCVUDAIDIENNH.Text = "Phó Giám đốc";
                //}
                //else
                //{
                //    txtUQMNT_CHUCVUDAIDIENNH.Text = "Giám đốc";
                //}
                txtUQMNT_GUQ.Text = nhanvien.Rows[0]["UYQUYEN"].ToString();
            }           
        }

        private void txtMaCN_Leave(object sender, EventArgs e)
        {
            System.Data.DataTable chinhanh = cn_bus.CHI_NHANH_THEO_MACN(txtMaCN.Text);
            if (chinhanh.Rows.Count>0)
            {
                txtChinhanhgoc.Text = chinhanh.Rows[0]["TENCN"].ToString();
            }
            else
            {
                txtChinhanhgoc.Text = "Agribank Chi nhánh...";
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
                        Int16 loaikh=1;
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
                        Int16 loaikh =2;
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
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2,Thongtindangnhap.user_id))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

        private void btnLay_TT_BENA_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachCN.SelectedCells.Count > 0)
            {
                txtUQSDTK_KH_MAKH.Text = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtUQSDTK_KH_HOTEN.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtUQSDTK_KH_NGAYSINH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                txtUQSDTK_KH_DTDD.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                txtUQSDTK_KH_NGHENGHIEP.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                txtUQSDTK_KH_CMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
                txtUQSDTK_KH_NGAYCAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtUQSDTK_KH_NOICAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                txtUQSDTK_KH_DIACHI.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();

                //Lấy danh sách số tài khoản tương ứng với mã khách hàng đã chọn gán vào cboxUQSDTK_STK
                cboxUQSDTK_STK.DataSource = null;
                cboxUQSDTK_STK.Items.Clear();
                System.Data.DataTable dt_stk = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString());
                cboxUQSDTK_STK.DataSource = dt_stk;
                cboxUQSDTK_STK.DisplayMember = "SOTK";
                cboxUQSDTK_STK.Refresh();
            }
            else
            {
                MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới.");
            }
        }

        private void btnLay_TT_BENB_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachCN.SelectedCells.Count > 0)
            {
                txtDUQSDTK_KH_HOTEN.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtDUQSDTK_KH_NGAYSINH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                txtDUQSDTK_KH_DTDD.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                txtDUQSDTK_KH_NGHENGHIEP.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                txtDUQSDTK_KH_CMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
                txtDUQSDTK_KH_NGAYCAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtDUQSDTK_KH_NOICAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                txtDUQSDTK_KH_DIACHI.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới để thực hiện chức năng này.");
            }
        }

        private void rdbUQSDTK_HL_Tungay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbUQSDTK_HL_Tungay.Checked == true)
            {
                txtUQSDTK_HL_Tungay.Enabled = true;
            }
            else
            {
                txtUQSDTK_HL_Tungay.Enabled = false;
                txtUQSDTK_HL_Tungay.Clear();                
            }
        }

        private void chkUQGDTKTK_TK1_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkUQGDTKTK_TK1.Checked == true)
            {
                cboxUQGDTKTK_TK1.Enabled = true;
                txtUQGDTKTK_Serial_TK1.Enabled = true;
                txtUQGDTKTK_Sotien_TK1.Enabled = true;
            }
            else
            {
                cboxUQGDTKTK_TK1.Enabled = false;
                txtUQGDTKTK_Serial_TK1.Enabled = false;
                txtUQGDTKTK_Serial_TK1.Clear();
                txtUQGDTKTK_Sotien_TK1.Enabled = false;
                txtUQGDTKTK_Sotien_TK1.Clear();
            }
        }

        private void chkUQGDTKTK_TK2_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkUQGDTKTK_TK2.Checked == true)
            {
                cboxUQGDTKTK_TK2.Enabled = true;
                txtUQGDTKTK_Serial_TK2.Enabled = true;
                txtUQGDTKTK_Sotien_TK2.Enabled = true;
            }
            else
            {
                cboxUQGDTKTK_TK2.Enabled = false;
                txtUQGDTKTK_Serial_TK2.Enabled = false;
                txtUQGDTKTK_Serial_TK2.Clear();
                txtUQGDTKTK_Sotien_TK2.Enabled = false;
                txtUQGDTKTK_Sotien_TK2.Clear();
            }
        }

        private void chkUQGDTKTK_TK3_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkUQGDTKTK_TK3.Checked == true)
            {
                cboxUQGDTKTK_TK3.Enabled = true;
                txtUQGDTKTK_Serial_TK3.Enabled = true;
                txtUQGDTKTK_Sotien_TK3.Enabled = true;
            }
            else
            {
                cboxUQGDTKTK_TK3.Enabled = false;
                txtUQGDTKTK_Serial_TK3.Enabled = false;
                txtUQGDTKTK_Serial_TK3.Clear();
                txtUQGDTKTK_Sotien_TK3.Enabled = false;
                txtUQGDTKTK_Sotien_TK3.Clear();
            }
        }

        private void txtUQGDTKTK_Sotien_TK1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void txtUQGDTKTK_Sotien_TK2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void txtUQGDTKTK_Sotien_TK3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void txtUQGDTKTK_Sotien_TK4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void txtUQGDTKTK_Sotien_TK1_TextChanged(object sender, EventArgs e)
        {
            if (txtUQGDTKTK_Sotien_TK1.Text == "")
            {
                txtUQGDTKTK_Sotien_TK1.Text = null;
            }
            else
            {
                Int64 d = Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK1.Text));
                txtUQGDTKTK_Sotien_TK1.Text = d.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
            }
            txtUQGDTKTK_Sotien_TK1.Select(txtUQGDTKTK_Sotien_TK1.Text.Length, 0);
        }

        private void txtUQGDTKTK_Sotien_TK2_TextChanged(object sender, EventArgs e)
        {
            if (txtUQGDTKTK_Sotien_TK2.Text == "")
            {
                txtUQGDTKTK_Sotien_TK2.Text = null;
            }
            else
            {
                Int64 d = Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK2.Text));
                txtUQGDTKTK_Sotien_TK2.Text = d.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
            }
            txtUQGDTKTK_Sotien_TK2.Select(txtUQGDTKTK_Sotien_TK2.Text.Length, 0);
        }

        private void txtUQGDTKTK_Sotien_TK3_TextChanged(object sender, EventArgs e)
        {
            if (txtUQGDTKTK_Sotien_TK3.Text == "")
            {
                txtUQGDTKTK_Sotien_TK3.Text = null;
            }
            else
            {
                Int64 d = Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK3.Text));
                txtUQGDTKTK_Sotien_TK3.Text = d.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
            }
            txtUQGDTKTK_Sotien_TK3.Select(txtUQGDTKTK_Sotien_TK3.Text.Length, 0);
        }

        private void btnUQGDTKTK_BUQ_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachCN.SelectedCells.Count > 0)
            {
                txtMaKH.Text = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtUQGDTKTK_KH_MAKH.Text = dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtUQGDTKTK_KH_HOTEN.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtUQGDTKTK_KH_NGAYSINH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                txtUQGDTKTK_KH_DTDD.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                txtUQGDTKTK_KH_NGHENGHIEP.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                txtUQGDTKTK_KH_CMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
                txtUQGDTKTK_KH_NGAYCAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtUQGDTKTK_KH_NOICAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                txtUQGDTKTK_KH_DIACHI.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();

                //Lấy danh sách số tài khoản tương ứng với mã khách hàng đã chọn gán vào cboxUQGDTKTK_TK1, cboxUQGDTKTK_TK2, cboxUQGDTKTK_TK3, cboxUQGDTKTK_TK4
                cboxUQGDTKTK_TK1.DataSource = null;
                cboxUQGDTKTK_TK1.Items.Clear();
                System.Data.DataTable dt_stk1 = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString());
                cboxUQGDTKTK_TK1.DataSource = dt_stk1;
                cboxUQGDTKTK_TK1.DisplayMember = "SOTK";
                cboxUQGDTKTK_TK1.Refresh();

                cboxUQGDTKTK_TK2.DataSource = null;
                cboxUQGDTKTK_TK2.Items.Clear();
                System.Data.DataTable dt_stk2 = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString());
                cboxUQGDTKTK_TK2.DataSource = dt_stk2;
                cboxUQGDTKTK_TK2.DisplayMember = "SOTK";
                cboxUQGDTKTK_TK2.Refresh();

                cboxUQGDTKTK_TK3.DataSource = null;
                cboxUQGDTKTK_TK3.Items.Clear();
                System.Data.DataTable dt_stk3 = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString());
                cboxUQGDTKTK_TK3.DataSource = dt_stk3;
                cboxUQGDTKTK_TK3.DisplayMember = "SOTK";
                cboxUQGDTKTK_TK3.Refresh();

            }
            else
            {
                MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới.");
            }
        }

        private void btnUQGDTKTK_BDUQ_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachCN.SelectedCells.Count > 0)
            {
                txtDUQGDTKTK_KH_HOTEN.Text = dgvDanhsachCN.CurrentRow.Cells["Tên KH"].Value.ToString();
                txtDUQGDTKTK_KH_NGAYSINH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                txtDUQGDTKTK_KH_DTDD.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                txtDUQGDTKTK_KH_NGHENGHIEP.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                txtDUQGDTKTK_KH_CMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();
                txtDUQGDTKTK_KH_NGAYCAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                txtDUQGDTKTK_KH_NOICAPCMND.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                txtDUQGDTKTK_KH_DIACHI.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới để thực hiện chức năng này.");
            }
        }

        private void rdbUQGDTKTK_HIEU_LUC_TU_NGAY_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbUQGDTKTK_HIEU_LUC_TU_NGAY.Checked == true)
            {
                txtUQGDTKTK_HIEU_LUC_TU_NGAY.Enabled = true;
                txtUQGDTKTK_HIEU_LUC_DEN_NGAY.Enabled = true;
            }
            else
            {
                txtUQGDTKTK_HIEU_LUC_TU_NGAY.Enabled = false;
                txtUQGDTKTK_HIEU_LUC_TU_NGAY.Clear();
                txtUQGDTKTK_HIEU_LUC_DEN_NGAY.Enabled = false;
                txtUQGDTKTK_HIEU_LUC_DEN_NGAY.Clear();
            }
        }

        private void tctTTKHCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tctTTKHCN.SelectedTab == tpThongtinchung1 || tctTTKHCN.SelectedTab == tpThongtinchung2)
            {
                cboxMaubieu.Text = "Mẫu 01/TKDV.vn - Đề nghị kiêm hợp đồng mở và sử dụng tài khoản thanh toán";
            }
            else if (tctTTKHCN.SelectedTab == tpDongtaikhoan)
            {
                cboxMaubieu.Text = "Mẫu 06/TKDV.vn - Đề nghị đóng tài khoản thanh toán";
            }
            else if (tctTTKHCN.SelectedTab == tpThaydoiTTKH)
            {
                cboxMaubieu.Text = "Mẫu 07/TKDV.vn - Đề nghị chỉnh sửa bổ sung thông tin khách hàng cá nhân";
            }
            else if (tctTTKHCN.SelectedTab == tpUyquyenmuaNT)
            {
                cboxMaubieu.Text = "Mẫu 01/VBAHD - Giấy ủy quyền mua ngoại tệ";
            }
            else if (tctTTKHCN.SelectedTab == tpUyquyensudungTK)
            {
                cboxMaubieu.Text = "Mẫu 02/VBAHD - Giấy ủy quyền sử dụng tài khoản";
            }
            else if (tctTTKHCN.SelectedTab == tpUQGDTKTK)
            {
                cboxMaubieu.Text = "Mẫu 03/VBAHD - Giấy ủy quyền giao dịch tài khoản tiết kiệm";
            }
        }
    }
}