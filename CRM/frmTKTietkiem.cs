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
using Novacode;

namespace CRM
{
    public partial class frmTKTietkiem : Form
    {
        TinhBUS t_bus = new TinhBUS();
        HuyenBUS h_bus = new HuyenBUS();
        XaBUS x_bus = new XaBUS();
        MAUBIEUBUS mb_bus = new MAUBIEUBUS();
        ChinhanhBUS cn_bus = new ChinhanhBUS();
        NHANVIENBUS nv_bus = new NHANVIENBUS();
        TAIKHOANBUS tk_bus = new TAIKHOANBUS();
        KHACHHANGBUS khachhang_bus = new KHACHHANGBUS();
        TAIKHOANBUS taikhoan_bus = new TAIKHOANBUS();

        String strCmd = "";
        private System.Data.DataTable dtResult = new System.Data.DataTable();
        private System.Data.DataTable dtDanhsach = new System.Data.DataTable();
        private System.Data.DataTable dtDanhsachDN = new System.Data.DataTable();

        private string chucvu_lanhdao = "";
             

        public static string makh = "";

        //Khai báo danh sách các dữ liệu đầu vào phục vụ tạo mẫu biểu
        private List<string> nguon_TTKH = new List<string>();
        private List<string> dich_TTKH = new List<string>();

        //Đường dẫn chứa file hồ sơ xuất ra từ chương trình
        public string output_file_path = @"C:\CRM1";

        public frmTKTietkiem()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
         
            dgvDanhsachTK.RowHeadersVisible = false;
            dgvDanhsachTK.AllowUserToAddRows = false;
            dgvDanhsachTK.ReadOnly = true;
            dgvDanhsachTK.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsachTK.MultiSelect = false;

        }

        private void frmMauBieuKT_Load(object sender, EventArgs e)
        {

            //Gán danh sách mẫu biểu vào cboxMaubieu
            System.Data.DataTable dt_mb = mb_bus.DANH_SACH_MAU_BIEU("Kế toán", "KT02");
            cboxMaubieu.DataSource = dt_mb;
            cboxMaubieu.DisplayMember = "TEN_MAUBIEU";
            cboxMaubieu.ValueMember = "TEN_FILEMAUBIEU";

            //Gán mã chi nhánh vào txtMaCN
            txtMaCN.Text = Thongtindangnhap.macn;

            System.Data.DataTable chinhanh = cn_bus.CHI_NHANH_THEO_MACN(txtMaCN.Text);
            //if (chinhanh.Rows.Count > 0)
            //{
            //    txtChinhanhgoc.Text = chinhanh.Rows[0]["TENCN"].ToString();
            //}

            //Gán danh sách kiểm soát, lãnh đạo vào cboxKiemsoat, cboxLanhdao
            if (Thongtindangnhap.hs)
            {
                //Đối với phòng thuộc trung tâm
                System.Data.DataTable dt_kiemsoat = nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Trưởng phòng");
                dt_kiemsoat.Merge(nv_bus.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Phó phòng"));
                cboxKiemsoat.DataSource = dt_kiemsoat;
                cboxKiemsoat.DisplayMember = "HOTEN";
                cboxKiemsoat.ValueMember = "MANV";

                System.Data.DataTable dt_lanhdao = nv_bus.DANH_SACH_NV_THEO_CN_PB(Thongtindangnhap.macn, Thongtindangnhap.macn + "-01");
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


            //Thiết lập tabpage hiển thị đầu tiên
            tctTT_Taikhoan.SelectedTab = tpXacnhansodutk;
            btnNhapTKTK.Enabled = true;
            btnNhapTKTT.Enabled = false;
            cboxMaubieu.Text = "Mẫu 04/VBAHD - Xác nhận số dư tiết kiệm";

            //Lấy chức vụ lãnh đạo
            System.Data.DataTable nhanvien = nv_bus.NHAN_VIEN_THEO_MANV(cboxLanhdao.SelectedValue.ToString());
            if (nhanvien.Rows.Count > 0)
            {
                chucvu_lanhdao = nhanvien.Rows[0]["CHUCVU"].ToString();
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
        //Lấy thông tin khách hàng từ file excel gán vào các ô dữ liệu trong tab Xác nhận số dư
        internal void FILL_TAB_XNSD(string makh)
        {
            //Xóa thông tin sổ tiết kiệm cũ
            btnXNSD_Them1.Text = "Thêm";
            txtXNSD_STK1.Clear();
            txtXNSD_Soso1.Clear();
            txtXNSD_Loaitien1.Clear();
            txtXNSD_Ngaygui1.Clear();
            txtXNSD_Kyhan1.Clear();
            txtXNSD_Sodu1.Clear();

            btnXNSD_Them2.Text = "Thêm";
            txtXNSD_STK2.Clear();
            txtXNSD_Soso2.Clear();
            txtXNSD_Loaitien2.Clear();
            txtXNSD_Ngaygui2.Clear();
            txtXNSD_Kyhan2.Clear();
            txtXNSD_Sodu2.Clear();

            btnXNSD_Them3.Text = "Thêm";
            txtXNSD_STK3.Clear();
            txtXNSD_Soso3.Clear();
            txtXNSD_Loaitien3.Clear();
            txtXNSD_Ngaygui3.Clear();
            txtXNSD_Kyhan3.Clear();
            txtXNSD_Sodu3.Clear();

            btnXNSD_Them4.Text = "Thêm";
            txtXNSD_STK4.Clear();
            txtXNSD_Soso4.Clear();
            txtXNSD_Loaitien4.Clear();
            txtXNSD_Ngaygui4.Clear();
            txtXNSD_Kyhan4.Clear();
            txtXNSD_Sodu4.Clear();

            btnXNSD_Them5.Text = "Thêm";
            txtXNSD_STK5.Clear();
            txtXNSD_Soso5.Clear();
            txtXNSD_Loaitien5.Clear();
            txtXNSD_Ngaygui5.Clear();
            txtXNSD_Kyhan5.Clear();
            txtXNSD_Sodu5.Clear();

            txtXNSD_Loaitien.Clear();
            txtXNSD_Tongsodu.Clear();
            txtXNSD_Soban.Text = "3";

            //Điền thông tin khách hàng mới
            System.Data.DataTable dt_kh = new System.Data.DataTable();
            dt_kh = khachhang_bus.KH_THEO_MAKH(makh);
            if (dt_kh.Rows.Count > 0)
            {
                //Lấy thông tin KH tiếng Việt
                txtXNSD_MaKH.Text = dt_kh.Rows[0]["MAKH"].ToString();
                if (Boolean.Parse(dt_kh.Rows[0]["GIOITINH"].ToString()) == true)
                {
                    txtXNSD_KH_HOTEN.Text = "ÔNG " + dt_kh.Rows[0]["HOTEN"].ToString().ToUpper();
                }
                else
                {
                    txtXNSD_KH_HOTEN.Text = "BÀ " + dt_kh.Rows[0]["HOTEN"].ToString().ToUpper();
                }
                txtXNSD_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                txtXNSD_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6,4);
                txtXNSD_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                txtXNSD_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();

                //Lấy thông tin KH tiếng Anh
                txtXNSD_MaKH_EN.Text = dt_kh.Rows[0]["MAKH"].ToString();
                if (Boolean.Parse(dt_kh.Rows[0]["GIOITINH"].ToString()) == true)
                {
                    txtXNSD_KH_HOTEN_EN.Text = "MR " + CommonMethod.convertToUnSign(dt_kh.Rows[0]["HOTEN"].ToString()).ToUpper();
                }
                else
                {
                    txtXNSD_KH_HOTEN_EN.Text = "MS " + CommonMethod.convertToUnSign(dt_kh.Rows[0]["HOTEN"].ToString()).ToUpper();
                }
                txtXNSD_KH_CMND_EN.Text = dt_kh.Rows[0]["CMND"].ToString();
                txtXNSD_KH_NGAYCAPCMND_EN.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                txtXNSD_KH_NOICAPCMND_EN.Text = dt_kh.Rows[0]["NOICAP_EN"].ToString();
                txtXNSD_KH_DIACHI_EN.Clear();
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                return;
            }
        }

        internal void FILL_TAB_XNSDTT(string makh)
        {
            //Xóa thông tin sổ tiết kiệm cũ
            btnXNSDTT_Them1.Text = "Thêm";
            txtXNSDTT_STK1.Clear();
            txtXNSDTT_Loaitien1.Clear();
            txtXNSDTT_Ngaymo1.Clear();
            txtXNSDTT_Sodu1.Clear();

            btnXNSDTT_Them2.Text = "Thêm";
            txtXNSDTT_STK2.Clear();
            txtXNSDTT_Loaitien2.Clear();
            txtXNSDTT_Ngaymo2.Clear();
            txtXNSDTT_Sodu2.Clear();

            btnXNSDTT_Them3.Text = "Thêm";
            txtXNSDTT_STK3.Clear();
            txtXNSDTT_Loaitien3.Clear();
            txtXNSDTT_Ngaymo3.Clear();
            txtXNSDTT_Sodu3.Clear();

            btnXNSDTT_Them4.Text = "Thêm";
            txtXNSDTT_STK4.Clear();
            txtXNSDTT_Loaitien4.Clear();
            txtXNSDTT_Ngaymo4.Clear();
            txtXNSDTT_Sodu4.Clear();

            btnXNSDTT_Them5.Text = "Thêm";
            txtXNSDTT_STK5.Clear();
            txtXNSDTT_Loaitien5.Clear();
            txtXNSDTT_Ngaymo5.Clear();
            txtXNSDTT_Sodu5.Clear();

            txtXNSDTT_Loaitien.Clear();
            txtXNSDTT_Tongsodu.Clear();
            txtXNSDTT_Soban.Text = "3";

            //Điền thông tin khách hàng mới
            System.Data.DataTable dt_kh = new System.Data.DataTable();
            dt_kh = khachhang_bus.KH_THEO_MAKH(makh);
            if (dt_kh.Rows.Count > 0)
            {
                //Lấy thông tin KH tiếng Việt
                txtXNSDTT_MaKH.Text = dt_kh.Rows[0]["MAKH"].ToString();
                if (Boolean.Parse(dt_kh.Rows[0]["GIOITINH"].ToString()) == true)
                {
                    txtXNSDTT_KH_HOTEN.Text = "ÔNG " + dt_kh.Rows[0]["HOTEN"].ToString().ToUpper();
                }
                else
                {
                    txtXNSDTT_KH_HOTEN.Text = "BÀ " + dt_kh.Rows[0]["HOTEN"].ToString().ToUpper();
                }
                txtXNSDTT_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                txtXNSDTT_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                txtXNSDTT_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                txtXNSDTT_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();

                //Lấy thông tin KH tiếng Anh
                txtXNSDTT_MaKH_EN.Text = dt_kh.Rows[0]["MAKH"].ToString();
                if (Boolean.Parse(dt_kh.Rows[0]["GIOITINH"].ToString()) == true)
                {
                    txtXNSDTT_KH_HOTEN_EN.Text = "MR " + CommonMethod.convertToUnSign(dt_kh.Rows[0]["HOTEN"].ToString()).ToUpper();
                }
                else
                {
                    txtXNSDTT_KH_HOTEN_EN.Text = "MS " + CommonMethod.convertToUnSign(dt_kh.Rows[0]["HOTEN"].ToString()).ToUpper();
                }
                txtXNSDTT_KH_CMND_EN.Text = dt_kh.Rows[0]["CMND"].ToString();
                txtXNSDTT_KH_NGAYCAPCMND_EN.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                txtXNSDTT_KH_NOICAPCMND_EN.Text = dt_kh.Rows[0]["NOICAP_EN"].ToString();
                txtXNSDTT_KH_DIACHI_EN.Clear();
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (ofdNhapfileSTK.ShowDialog() == DialogResult.OK)
            {
                string import_file_path = ofdNhapfileSTK.FileName;
                System.Data.DataTable dt_temp = CommonMethod.read_excel(import_file_path);
                if (dt_temp.Rows.Count == 0 || dt_temp == null)
                {
                    MessageBox.Show("File không có dữ liệu");
                    return;
                }

                //string makh = Thongtindangnhap.macn + dt_temp.Rows[0][4].ToString();
                string makh = dt_temp.Rows[0][3].ToString().Substring(0, 4) + dt_temp.Rows[0][4].ToString();
                FILL_TAB_XNSD(makh);

                System.Data.DataTable dt_stk = new System.Data.DataTable();
                dt_stk.Columns.AddRange
                (
                    new DataColumn[6] 
                { 
                    new DataColumn("Số TK", typeof(string)),
                    new DataColumn("Loại tiền tệ", typeof(string)),
                    new DataColumn("Ngày gửi", typeof(string)),
                    new DataColumn("Kỳ hạn", typeof(ushort)),
                    new DataColumn("Số dư hiện tại", typeof(string)),
                    new DataColumn("Số dư gốc", typeof(string))
      
                }
                );
                DataRow dr_stk;

                System.Data.DataTable dt_taikhoan = new System.Data.DataTable();
                dt_taikhoan.Columns.AddRange
                (
                    new DataColumn[7] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOTK", typeof(string)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("NGAYMO", typeof(string)),
                    new DataColumn("NGAYDENHAN", typeof(string)),
                    new DataColumn("NGAYDONG", typeof(string)),
                    new DataColumn("HOATDONG", typeof(bool)),   
                }
                );
                DataRow dr_taikhoan;

                int iRows = dt_temp.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    //Chỉ chọn tài khoản tiết kiệm và đang hoạt động để gán vào datagridview
                    if (dt_temp.Rows[i][1].ToString() == "Savings Deposit" && dt_temp.Rows[i][2].ToString() == "Active")
                    {
                        dr_stk = dt_stk.NewRow();
                        dr_stk["Số TK"] = dt_temp.Rows[i][3].ToString();
                        dr_stk["Loại tiền tệ"] = dt_temp.Rows[i][6].ToString();
                        dr_stk["Ngày gửi"] = dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);
                        dr_stk["Kỳ hạn"] = Convert.ToUInt16(dt_temp.Rows[i][14].ToString());
                        dr_stk["Số dư hiện tại"] = Convert.ToUInt64(dt_temp.Rows[i][7].ToString()).ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                        dr_stk["Số dư gốc"] = Convert.ToUInt64(dt_temp.Rows[i][16].ToString()).ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                        dt_stk.Rows.Add(dr_stk);
                    }
                    
                    //Chọn các tài khoản đang hoạt động để nhập vào bảng TAIKHOAN
                    if (dt_temp.Rows[i][2].ToString() == "Active")
                    {
                        dr_taikhoan = dt_taikhoan.NewRow();
                        dr_taikhoan["MAKH"] = makh;
                        dr_taikhoan["SOTK"] = dt_temp.Rows[i][3].ToString();
                        dr_taikhoan["CCY"] = dt_temp.Rows[i][6].ToString();
                        dr_taikhoan["NGAYMO"] = dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);
                        if (dt_temp.Rows[i][1].ToString() == "Savings Deposit")
                        {
                            dr_taikhoan["NGAYDENHAN"] = dt_temp.Rows[i][11].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][11].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][11].ToString().Substring(6, 4);
                        }
                        else
                        {
                            dr_taikhoan["NGAYDENHAN"] = "01/01/1900";
                        }
                        
                        dr_taikhoan["NGAYDONG"] = "01/01/1900";
                        dr_taikhoan["HOATDONG"] = true;
                        dt_taikhoan.Rows.Add(dr_taikhoan);
                    }
                }

                //Gán tabel dt_stk vào datagridview
                dgvDanhsachTK.DataSource = dt_stk;
                dgvDanhsachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].Width = 150;
                dgvDanhsachTK.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[1].Width = 50;
                dgvDanhsachTK.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[2].Width = 90;
                dgvDanhsachTK.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[3].Width = 50;
                dgvDanhsachTK.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[4].Width = 150;
                dgvDanhsachTK.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[5].Width = 150;
                Cursor.Current = Cursors.Default;

                //Cập nhật bảng TAIKHOAN từ table dt_taikhoan
                if (dt_taikhoan.Rows.Count > 0)
                {
                    bool update_tk = taikhoan_bus.UPDATE_TAIKHOAN_TUFILE(dt_taikhoan);
                }
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

        

        //private void btnSMaKH_Click(object sender, EventArgs e)
        //{
        //    layDS_MaKH();
        //}

        //private void btnSTen_Click(object sender, EventArgs e)
        //{
        //    layDS_TenKH();
        //}

        //private void btnSTel_Click(object sender, EventArgs e)
        //{
        //    layDS_Dienthoai();
        //}

        //private void btnSCMND_Click(object sender, EventArgs e)
        //{
        //    layDS_CMND();
        //}

        

        //private void btnSNgaysinh_Click(object sender, EventArgs e)
        //{
        //    layDS_Ngaysinh();
        //}

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

        //Hàm tạo mẫu biểu kế toán không tạo bảng
        internal void TAO_MAU_BIEU_KE_TOAN(string file_mau_bieu)
        {
            //Lấy đường dẫn file template
            string TemplateFileLocation = CommonMethod.TemplateFileLocation(file_mau_bieu);

            //Xác định đường dẫn file xuất ra từ chương trình
            string output_file_name = output_file_path + @"\" + Path.GetFileNameWithoutExtension(TemplateFileLocation) + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".docx";
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

        //Hàm tạo mẫu biểu có chèn bảng
        internal void TAO_MAU_BIEU_KE_TOAN_1(string file_mau_bieu, System.Data.DataTable data, List<string> list_title, string font_family, double font_size, bool last_row_bold, int merge_row_index = -1, int start_index = -1, int end_index = -1, string p_footer = "")
        {
            //Lấy đường dẫn file template
            string TemplateFileLocation = CommonMethod.TemplateFileLocation(file_mau_bieu);

            //Xác định đường dẫn file xuất ra từ chương trình
            string output_file_name = output_file_path + @"\" + Path.GetFileNameWithoutExtension(TemplateFileLocation) + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".docx";
            //MessageBox.Show(output_file_name);

            //Tạo mẫu biểu
            CommonMethod.CreateWordDocumentWithTable(TemplateFileLocation, output_file_name, this.nguon_TTKH, this.dich_TTKH, data, list_title, font_family, font_size, last_row_bold, merge_row_index, start_index, end_index, p_footer);
            
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

            if (Thongtindangnhap.macn == "2300")
            {
                //Gán tên chi nhánh ở phần tiêu đề
                nguon_TTKH.Add("<CHI_NHANH_1>");
                dich_TTKH.Add("CHI NHÁNH/BRANCH "+Thongtindangnhap.tencn.Substring(24).ToUpper());
            }
            else if (Thongtindangnhap.macn == "2301" || Thongtindangnhap.macn == "2313")
            {
                //Gán tên chi nhánh ở phần tiêu đề
                nguon_TTKH.Add("<CHI_NHANH_1>");
                dich_TTKH.Add("CHI NHÁNH/BRANCH " + Thongtindangnhap.tencn.Substring(19).ToUpper());
            }
            else
            {
                //Gán tên chi nhánh ở phần tiêu đề
                nguon_TTKH.Add("<CHI_NHANH_1>");
                dich_TTKH.Add("CHI NHÁNH/BRANCH " + Thongtindangnhap.tencn.Substring(25).ToUpper());
            }
            
            if (Thongtindangnhap.hs)
            {
                //Đối với trung tâm các chi nhánh
                //VI
                nguon_TTKH.Add("<CHI_NHANH>");
                dich_TTKH.Add(Thongtindangnhap.tencn);

                //EN
                nguon_TTKH.Add("<CHI_NHANH_EN>");
                dich_TTKH.Add(Thongtindangnhap.tencn_en);

                nguon_TTKH.Add("<PHONG_GIAO_DICH_1>");
                dich_TTKH.Add("");
            }
            else
            {
                //Đối với phòng giao dịch
                //VI
                nguon_TTKH.Add("<CHI_NHANH>");
                dich_TTKH.Add(Thongtindangnhap.tencn + " - " + Thongtindangnhap.tenpb);

                //EN
                nguon_TTKH.Add("<CHI_NHANH_EN>");
                dich_TTKH.Add(Thongtindangnhap.tencn_en + "-" + Thongtindangnhap.tenpb_en);

                nguon_TTKH.Add("<PHONG_GIAO_DICH_1>");
                dich_TTKH.Add("PGD/OFFICE " + Thongtindangnhap.tenpb.Substring(4).ToUpper());
            }

            nguon_TTKH.Add("<DC_CHI_NHANH>");
            dich_TTKH.Add(Thongtindangnhap.diachipb);

            //nguon_TTKH.Add("<KH_MAKH>");
            //dich_TTKH.Add(txtXNSD_MaKH.Text);

            //nguon_TTKH.Add("<KH_HOTEN>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString());

            //nguon_TTKH.Add("<KH_NGAYSINH>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["Ngày sinh"].Value.ToString());

            //nguon_TTKH.Add("<KH_NGHENGHIEP>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["Nghề nghiệp"].Value.ToString());

            //nguon_TTKH.Add("<KH_CHUCVU>");
            //dich_TTKH.Add("                            ");

            //nguon_TTKH.Add("<KH_MST>");
            //dich_TTKH.Add("                            ");

            //if (dgvDanhsachTK.CurrentRow.Cells["Giới tính"].Value.ToString() == "Nam")
            //{
            //    nguon_TTKH.Add("<KH_GT_NAM>");
            //    dich_TTKH.Add(((char)0x2611).ToString());

            //    nguon_TTKH.Add("<KH_GT_NU>");
            //    dich_TTKH.Add(((char)0x2610).ToString());
            //}
            //else
            //{
            //    nguon_TTKH.Add("<KH_GT_NAM>");
            //    dich_TTKH.Add(((char)0x2610).ToString());

            //    nguon_TTKH.Add("<KH_GT_NU>");
            //    dich_TTKH.Add(((char)0x2611).ToString());
            //}

            //nguon_TTKH.Add("<KH_QUOCTICH>");
            //dich_TTKH.Add(txtQuoctich.Text);

            //nguon_TTKH.Add("<KH_DANTOC>");
            //dich_TTKH.Add(txtDantoc.Text);

            //nguon_TTKH.Add("<KH_TONGIAO>");
            //dich_TTKH.Add(txtTongiao.Text);

            //if (rdbNCT_CO.Checked == true)
            //{
            //    nguon_TTKH.Add("<KH_NCT_CO>");
            //    dich_TTKH.Add(((char)0x2611).ToString());

            //    nguon_TTKH.Add("<KH_NCT_KHONG>");
            //    dich_TTKH.Add(((char)0x2610).ToString());
            //}
            //else
            //{
            //    nguon_TTKH.Add("<KH_NCT_CO>");
            //    dich_TTKH.Add(((char)0x2610).ToString());

            //    nguon_TTKH.Add("<KH_NCT_KHONG>");
            //    dich_TTKH.Add(((char)0x2611).ToString());
            //}

            //nguon_TTKH.Add("<KH_CMND>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["CMND"].Value.ToString());

            //nguon_TTKH.Add("<KH_NGAYCAPCMND>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["Ngày cấp"].Value.ToString());

            //nguon_TTKH.Add("<KH_NOICAPCMND>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["Nơi cấp"].Value.ToString());

            //nguon_TTKH.Add("<KH_DTDD1>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString());

            //nguon_TTKH.Add("<KH_EMAIL>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["Email"].Value.ToString());

            //nguon_TTKH.Add("<KH_DIACHI>");
            //dich_TTKH.Add(dgvDanhsachTK.CurrentRow.Cells["Địa chỉ"].Value.ToString());

            //nguon_TTKH.Add("<KH_DKKD>");
            //dich_TTKH.Add(".....");

            //nguon_TTKH.Add("<KH_NGAYCAPDKKD>");
            //dich_TTKH.Add(".../.../......");

            //nguon_TTKH.Add("<KH_NOICAPDKKD>");
            //dich_TTKH.Add(".....");

            //nguon_TTKH.Add("<KH_SOQDTL>");
            //dich_TTKH.Add(".....");

            //nguon_TTKH.Add("<KH_MST>");
            //dich_TTKH.Add(".....");

            //nguon_TTKH.Add("<KH_NGAYCAPMST>");
            //dich_TTKH.Add(".../.../......");

            //nguon_TTKH.Add("<KH_NOICAPMST>");
            //dich_TTKH.Add(".....");

            if (Thongtindangnhap.macn == "2300" || Thongtindangnhap.macn == "2301" || Thongtindangnhap.macn == "2313")
            {
                //VI
                nguon_TTKH.Add("<DIA_BAN>");
                dich_TTKH.Add("Hải Dương");

                //EN
                nguon_TTKH.Add("<DIA_BAN_EN>");
                dich_TTKH.Add("Hai Duong");
            }
            else
            {
                //VI
                nguon_TTKH.Add("<DIA_BAN>");
                dich_TTKH.Add(Thongtindangnhap.tencn.Substring(25));

                //EN
                nguon_TTKH.Add("<DIA_BAN>");
                dich_TTKH.Add(CommonMethod.convertToUnSign(Thongtindangnhap.tencn.Substring(25)));
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
                dich_TTKH.Add("GIÁM ĐỐC/DIRECTOR");

                nguon_TTKH.Add("<CHUC_DANH2>");
                dich_TTKH.Add("");
            }
            else if (chucvu_lanhdao == "Phó Giám đốc")
            {
                nguon_TTKH.Add("<CHUC_DANH1>");
                dich_TTKH.Add("KT. GIÁM ĐỐC/PP. DIRECTOR");

                nguon_TTKH.Add("<CHUC_DANH2>");
                dich_TTKH.Add("PHÓ GIÁM ĐỐC/V. DIRECTOR");
            }

            nguon_TTKH.Add("<SDT_AGRIBANK>");
            dich_TTKH.Add(Thongtindangnhap.sdt_pb);

            nguon_TTKH.Add("<FAX_AGRIBANK>");
            dich_TTKH.Add(Thongtindangnhap.fax_pb);

            //VN
            nguon_TTKH.Add("<NTN_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"));

            //EN
            nguon_TTKH.Add("<NTN_HIENTAI_EN>");
            dich_TTKH.Add(DateTime.Now.ToString("dd MMMM yyyy"));

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

        //Lấy thông tin cho mẫu xác nhận số dư tiết kiệm
        internal void LAY_TT_XNSD()
        {
            //VI
            nguon_TTKH.Add("<XNSD_KH_HOTEN>");
            dich_TTKH.Add(txtXNSD_KH_HOTEN.Text);

            //EN
            nguon_TTKH.Add("<XNSD_KH_HOTEN_EN>");
            dich_TTKH.Add(txtXNSD_KH_HOTEN_EN.Text);

            nguon_TTKH.Add("<XNSD_KH_CMND>");
            dich_TTKH.Add(txtXNSD_KH_CMND.Text);

            nguon_TTKH.Add("<XNSD_KH_NGAYCAPCMND>");
            dich_TTKH.Add(txtXNSD_KH_NGAYCAPCMND.Text);

            //VI
            nguon_TTKH.Add("<XNSD_KH_NOICAPCMND>");
            dich_TTKH.Add(txtXNSD_KH_NOICAPCMND.Text);

            //EN
            nguon_TTKH.Add("<XNSD_KH_NOICAPCMND_EN>");
            dich_TTKH.Add(txtXNSD_KH_NOICAPCMND_EN.Text);

            //VI
            nguon_TTKH.Add("<XNSD_KH_DIACHI>");
            dich_TTKH.Add(txtXNSD_KH_DIACHI.Text);

            //EN
            nguon_TTKH.Add("<XNSD_KH_DIACHI_EN>");
            dich_TTKH.Add(txtXNSD_KH_DIACHI_EN.Text);

            //VI
            nguon_TTKH.Add("<XNSD_THOIDIEM>");
            dich_TTKH.Add(DateTime.Now.ToString("HH") + " giờ " + DateTime.Now.ToString("mm") + " phút ngày " + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"));

            //EN
            nguon_TTKH.Add("<XNSD_THOIDIEM_EN>");
            dich_TTKH.Add(DateTime.Now.ToString("dd MMMM yyyy hh:mm tt"));

            nguon_TTKH.Add("<XNSD_TSODU>");
            dich_TTKH.Add(ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Tongsodu.Text))));

            nguon_TTKH.Add("<XNSD_LOAITT>");
            dich_TTKH.Add(txtXNSD_Loaitien.Text);

            if (txtXNSD_Loaitien.Text == "VND")
            {
                //VI
                nguon_TTKH.Add("<XNSD_TSODU_BANGCHU>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtXNSD_Tongsodu.Text))) + "đồng");

                //EN
                nguon_TTKH.Add("<XNSD_TSODU_BANGCHU_EN>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChuEN(ControlFormat.skipComma(txtXNSD_Tongsodu.Text), "Vietnamese Dongs")));
            }
            else if (txtXNSD_Loaitien.Text == "USD")
            {
                //VI
                nguon_TTKH.Add("<XNSD_TSODU_BANGCHU>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtXNSD_Tongsodu.Text))) + "đô la Mỹ");

                //EN
                nguon_TTKH.Add("<XNSD_TSODU_BANGCHU_EN>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChuEN(ControlFormat.skipComma(txtXNSD_Tongsodu.Text), "US dollars")));
            }
            else if (txtXNSD_Loaitien.Text == "EUR")
            {
                //VI
                nguon_TTKH.Add("<XNSD_TSODU_BANGCHU>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtXNSD_Tongsodu.Text))) + "euro");

                //EN
                nguon_TTKH.Add("<XNSD_TSODU_BANGCHU_EN>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChuEN(ControlFormat.skipComma(txtXNSD_Tongsodu.Text), "EURs")));
            }

            int xnsd_soban = Convert.ToInt16(txtXNSD_Soban.Text);
            int xnsd_soban_kh = xnsd_soban - 1;
            if (xnsd_soban < 10)
            {                
                nguon_TTKH.Add("<XNSD_SOBAN>");
                dich_TTKH.Add("0"+Convert.ToString(xnsd_soban));

                if (xnsd_soban_kh <10)
                {
                    nguon_TTKH.Add("<XNSD_SOBAN_KH>");
                    dich_TTKH.Add("0"+Convert.ToString(xnsd_soban_kh));
                }
                else
                {
                    nguon_TTKH.Add("<XNSD_SOBAN_KH>");
                    dich_TTKH.Add(Convert.ToString(xnsd_soban_kh));
                } 
            }
            else
            {
                nguon_TTKH.Add("<XNSD_SOBAN>");
                dich_TTKH.Add(Convert.ToString(xnsd_soban));

                nguon_TTKH.Add("<XNSD_SOBAN_KH>");
                dich_TTKH.Add(Convert.ToString(xnsd_soban_kh));
            }
        }

        //Lấy thông tin cho mẫu xác nhận số dư tài khoản thanh toán
        internal void LAY_TT_XNSDTT()
        {
            //VI
            nguon_TTKH.Add("<XNSDTT_KH_HOTEN>");
            dich_TTKH.Add(txtXNSDTT_KH_HOTEN.Text);

            //EN
            nguon_TTKH.Add("<XNSDTT_KH_HOTEN_EN>");
            dich_TTKH.Add(txtXNSDTT_KH_HOTEN_EN.Text);

            nguon_TTKH.Add("<XNSDTT_KH_CMND>");
            dich_TTKH.Add(txtXNSDTT_KH_CMND.Text);

            nguon_TTKH.Add("<XNSDTT_KH_NGAYCAPCMND>");
            dich_TTKH.Add(txtXNSDTT_KH_NGAYCAPCMND.Text);

            //VI
            nguon_TTKH.Add("<XNSDTT_KH_NOICAPCMND>");
            dich_TTKH.Add(txtXNSDTT_KH_NOICAPCMND.Text);

            //EN
            nguon_TTKH.Add("<XNSDTT_KH_NOICAPCMND_EN>");
            dich_TTKH.Add(txtXNSDTT_KH_NOICAPCMND_EN.Text);

            //VI
            nguon_TTKH.Add("<XNSDTT_KH_DIACHI>");
            dich_TTKH.Add(txtXNSDTT_KH_DIACHI.Text);

            //EN
            nguon_TTKH.Add("<XNSDTT_KH_DIACHI_EN>");
            dich_TTKH.Add(txtXNSDTT_KH_DIACHI_EN.Text);

            //VI
            nguon_TTKH.Add("<XNSDTT_THOIDIEM>");
            dich_TTKH.Add(DateTime.Now.ToString("HH") + " giờ " + DateTime.Now.ToString("mm") + " phút ngày " + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"));

            //EN
            nguon_TTKH.Add("<XNSDTT_THOIDIEM_EN>");
            dich_TTKH.Add(DateTime.Now.ToString("dd MMMM yyyy hh:mm tt"));

            nguon_TTKH.Add("<XNSDTT_TSODU>");
            dich_TTKH.Add(ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text))));

            nguon_TTKH.Add("<XNSDTT_LOAITT>");
            dich_TTKH.Add(txtXNSDTT_Loaitien.Text);

            if (txtXNSDTT_Loaitien.Text == "VND")
            {
                //VI
                nguon_TTKH.Add("<XNSDTT_TSODU_BANGCHU>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text))) + "đồng");

                //EN
                nguon_TTKH.Add("<XNSDTT_TSODU_BANGCHU_EN>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChuEN(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text), "Vietnamese Dongs")));
            }
            else if (txtXNSDTT_Loaitien.Text == "USD")
            {
                //VI
                nguon_TTKH.Add("<XNSDTT_TSODU_BANGCHU>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text))) + "đô la Mỹ");

                //EN
                nguon_TTKH.Add("<XNSDTT_TSODU_BANGCHU_EN>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChuEN(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text), "US dollars")));
            }
            else if (txtXNSDTT_Loaitien.Text == "EUR")
            {
                //VI
                nguon_TTKH.Add("<XNSDTT_TSODU_BANGCHU>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text))) + "euro");

                //EN
                nguon_TTKH.Add("<XNSDTT_TSODU_BANGCHU_EN>");
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChuEN(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text), "EURs")));
            }

            if (chucvu_lanhdao == "Giám đốc")
            {
                nguon_TTKH.Add("<CHUC_DANH1>");
                dich_TTKH.Add("GIÁM ĐỐC/DIRECTOR");

                nguon_TTKH.Add("<CHUC_DANH2>");
                dich_TTKH.Add("");
            }
            else if (chucvu_lanhdao == "Phó Giám đốc")
            {
                nguon_TTKH.Add("<CHUC_DANH1>");
                dich_TTKH.Add("KT. GIÁM ĐỐC/PP. DIRECTOR");

                nguon_TTKH.Add("<CHUC_DANH2>");
                dich_TTKH.Add("PHÓ GIÁM ĐỐC/V. DIRECTOR");
            }

            int xnsd_soban = Convert.ToInt16(txtXNSDTT_Soban.Text);
            int xnsd_soban_kh = xnsd_soban - 1;
            if (xnsd_soban < 10)
            {
                nguon_TTKH.Add("<XNSDTT_SOBAN>");
                dich_TTKH.Add("0" + Convert.ToString(xnsd_soban));

                if (xnsd_soban_kh < 10)
                {
                    nguon_TTKH.Add("<XNSDTT_SOBAN_KH>");
                    dich_TTKH.Add("0" + Convert.ToString(xnsd_soban_kh));
                }
                else
                {
                    nguon_TTKH.Add("<XNSDTT_SOBAN_KH>");
                    dich_TTKH.Add(Convert.ToString(xnsd_soban_kh));
                }
            }
            else
            {
                nguon_TTKH.Add("<XNSDTT_SOBAN>");
                dich_TTKH.Add(Convert.ToString(xnsd_soban));

                nguon_TTKH.Add("<XNSDTT_SOBAN_KH>");
                dich_TTKH.Add(Convert.ToString(xnsd_soban_kh));
            }
        }

        //Lấy thông tin mở tài khoản dùng cho mẫu 01TKDVVN - MO TAI KHOAN CA NHAN)
        //internal void LAY_TT_MOTK()
        //{
        //    if (chkVND.Checked == true)
        //    {
        //        nguon_TTKH.Add("<LTT_VND>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<LTT_VND>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkUSD.Checked == true)
        //    {
        //        nguon_TTKH.Add("<LTT_USD>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<LTT_USD>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkEURO.Checked == true)
        //    {
        //        nguon_TTKH.Add("<LTT_EUR>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<LTT_EUR>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkKhac.Checked == true)
        //    {
        //        nguon_TTKH.Add("<LTT_KHAC>");
        //        dich_TTKH.Add(((char)0x2611).ToString());

        //        nguon_TTKH.Add("<LOAITIENTE_KHAC>");
        //        dich_TTKH.Add(txtLTT_KHAC.Text);
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<LTT_KHAC>");
        //        dich_TTKH.Add(((char)0x2610).ToString());

        //        nguon_TTKH.Add("<LOAITIENTE_KHAC>");
        //        dich_TTKH.Add("");
        //    }

        //    if (chkMB_SMS.Checked == true)
        //    {
        //        nguon_TTKH.Add("<MB_SMS>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<MB_SMS>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkMB_EMB.Checked == true)
        //    {
        //        nguon_TTKH.Add("<MB_EMB>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<MB_EMB>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkMB_BP.Checked == true)
        //    {
        //        nguon_TTKH.Add("<MB_BP>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<MB_BP>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkMB_MP.Checked == true)
        //    {
        //        nguon_TTKH.Add("<MB_MP>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<MB_MP>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    nguon_TTKH.Add("<CN_DTDD1>");
        //    dich_TTKH.Add(txtCN_DTDD1.Text);

        //    nguon_TTKH.Add("<CN_DTDD2>");
        //    dich_TTKH.Add(txtCN_DTDD2.Text);

        //    nguon_TTKH.Add("<CN_DTDD3>");
        //    dich_TTKH.Add(txtCN_DTDD3.Text);

        //    nguon_TTKH.Add("<CN_DTDD4>");
        //    dich_TTKH.Add(txtCN_DTDD4.Text);

        //    nguon_TTKH.Add("<CN_DTDD5>");
        //    dich_TTKH.Add(txtCN_DTDD5.Text);

        //    if (chkIB_TAICHINH.Checked == true)
        //    {
        //        nguon_TTKH.Add("<IB_TAICHINH>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<IB_TAICHINH>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkIB_THANHTOAN.Checked == true)
        //    {
        //        nguon_TTKH.Add("<IB_THANHTOAN>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<IB_THANHTOAN>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkIB_PHITAICHINH.Checked == true)
        //    {
        //        nguon_TTKH.Add("<IB_PHITAICHINH>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<IB_PHITAICHINH>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (rdbOTPSoftToken.Checked == true)
        //    {
        //        nguon_TTKH.Add("<OTP_SOFT>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<OTP_SOFT>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (rdbOTPHardToken.Checked == true)
        //    {
        //        nguon_TTKH.Add("<OTP_HARD>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<OTP_HARD>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (rdbOTPSMSToken.Checked == true)
        //    {
        //        nguon_TTKH.Add("<OTP_SMS>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<OTP_SMS>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkNTTD_Nuoc.Checked == true)
        //    {
        //        nguon_TTKH.Add("<NTTD_NUOC>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<NTTD_NUOC>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkNTTD_Dien.Checked == true)
        //    {
        //        nguon_TTKH.Add("<NTTD_DIEN>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<NTTD_DIEN>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkNTTD_Vienthong.Checked == true)
        //    {
        //        nguon_TTKH.Add("<NTTD_VIENTHONG>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<NTTD_VIENTHONG>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkNTTD_Hocphi.Checked == true)
        //    {
        //        nguon_TTKH.Add("<NTTD_HOCPHI>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<NTTD_HOCPHI>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkNTTD_Khac.Checked == true)
        //    {
        //        nguon_TTKH.Add("<NTTD_KHAC>");
        //        dich_TTKH.Add(((char)0x2611).ToString());

        //        nguon_TTKH.Add("<NTTD_NOIDUNGKHAC>");
        //        dich_TTKH.Add(txtNTTD_Noidungkhac.Text);
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<NTTD_KHAC>");
        //        dich_TTKH.Add(((char)0x2610).ToString());

        //        nguon_TTKH.Add("<NTTD_NOIDUNGKHAC>");
        //        dich_TTKH.Add(txtNTTD_Noidungkhac.Text);
        //    }

        //    if (chkThe_tra_truoc.Checked == true)
        //    {
        //        nguon_TTKH.Add("<THE_TRA_TRUOC>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<THE_TRA_TRUOC>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTTT_Ghinonoidia.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_GHINONOIDIA>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_GHINONOIDIA>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTTT_Lapnghiep.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_LAPNGHIEP>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_LAPNGHIEP>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTTT_VisaDebit.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_VISADEBIT>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_VISADEBIT>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTTT_MasterCardDebit.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_MASTERCARDDEBIT>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_MASTERCARDDEBIT>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTTT_Lienketthuonghieu.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_LIENKETTHUONGHIEU>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_LIENKETTHUONGHIEU>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkHangthechuan.Checked == true)
        //    {
        //        nguon_TTKH.Add("<HANG_THE_CHUAN>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<HANG_THE_CHUAN>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkHangthevang.Checked == true)
        //    {
        //        nguon_TTKH.Add("<HANG_THE_VANG>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<HANG_THE_VANG>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkPhatHanhThuong.Checked == true)
        //    {
        //        nguon_TTKH.Add("<PHAT_HANH_THUONG>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<PHAT_HANH_THUONG>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkPhatHanhNhanh.Checked == true)
        //    {
        //        nguon_TTKH.Add("<PHAT_HANH_NHANH>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<PHAT_HANH_NHANH>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (rdbTTT_Nhantructiep.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_NHANTRUCTIEP>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_NHANTRUCTIEP>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (rdbTTT_UyQuyen.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_UYQUYEN>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_UYQUYEN>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    nguon_TTKH.Add("<UQ_HOTEN>");
        //    dich_TTKH.Add(txtUQ_HoTen.Text);

        //    nguon_TTKH.Add("<UQ_CMND>");
        //    dich_TTKH.Add(txtUQ_CMND.Text);

        //    nguon_TTKH.Add("<UQ_NGAYCAPCMND>");
        //    dich_TTKH.Add(txtUQ_NgayCapCMND.Text);

        //    nguon_TTKH.Add("<UQ_NOICAPCMND>");
        //    dich_TTKH.Add(txtUQ_NoiCapCMND.Text);

        //    if (chkTTT_BHCT.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_BHCT>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_BHCT>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTTT_DKITN.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TTT_DKITN>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TTT_DKITN>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    nguon_TTKH.Add("<INTERNET_HMGD>");
        //    dich_TTKH.Add(txtInternet_HMGD.Text);

        //    nguon_TTKH.Add("<INTERNET_DTDD>");
        //    dich_TTKH.Add(txtInternet_DTDD.Text);

        //    nguon_TTKH.Add("<TBSD_DINHKYGUI>");
        //    dich_TTKH.Add(txtTBSD_DinhKyGui.Text);

        //    if (chkTBSD_Quay.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TBSD_QUAY>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TBSD_QUAY>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTBSD_Thu.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TBSD_THU>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TBSD_THU>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTBSD_Fax.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TBSD_FAX>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TBSD_FAX>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkTBSD_Khac.Checked == true)
        //    {
        //        nguon_TTKH.Add("<TBSD_KHAC>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<TBSD_KHAC>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    nguon_TTKH.Add("<TBSD_HINHTHUCKHAC>");
        //    dich_TTKH.Add(txtTBSD_HinhThucKhac.Text);

        //    nguon_TTKH.Add("<NGAY_HIEU_LUC>");
        //    dich_TTKH.Add(DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"));

        //    string ccy = "VND";
        //    if (chkVND.Checked == true)
        //    {
        //        ccy = "VND";
        //    }

        //    if (chkUSD.Checked == true)
        //    {
        //        ccy = "USD";
        //    }

        //    if (chkEURO.Checked == true)
        //    {
        //        ccy = "EUR";
        //    }

        //    if (chkKhac.Checked == true)
        //    {
        //        ccy = txtLTT_KHAC.Text;
        //    }

        //    nguon_TTKH.Add("<KH_LTT>");
        //    dich_TTKH.Add(ccy);

        //    if (chkCongDanMy.Checked == true)
        //    {
        //        nguon_TTKH.Add("<CONG_DAN_MY>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<CONG_DAN_MY>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkDauHieuMy.Checked == true)
        //    {
        //        nguon_TTKH.Add("<DAU_HIEU_MY>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<DAU_HIEU_MY>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    if (chkNo_CongDanMy.Checked == true)
        //    {
        //        nguon_TTKH.Add("<NO_CONG_DAN_MY>");
        //        dich_TTKH.Add(((char)0x2611).ToString());
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<NO_CONG_DAN_MY>");
        //        dich_TTKH.Add(((char)0x2610).ToString());
        //    }

        //    nguon_TTKH.Add("<NGAY_MOTK>");
        //    dich_TTKH.Add(DateTime.Now.ToString("dd"));

        //    nguon_TTKH.Add("<THANG_MOTK>");
        //    dich_TTKH.Add(DateTime.Now.ToString("MM"));

        //    nguon_TTKH.Add("<NAM_MOTK>");
        //    dich_TTKH.Add(DateTime.Now.ToString("yyyy"));
        //}

        //Lấy thông tin đóng tài khoản dùng cho mẫu 06TKDVVN - DONG TAI KHOAN THANH TOAN.docx
        //internal void LAY_TT_DONGTK()
        //{
        //    nguon_TTKH.Add("<KH_DONG_STK>");
        //    dich_TTKH.Add(cboxCN_DONG_TK.Text);

        //    nguon_TTKH.Add("<KH_DONG_LOAITK>");
        //    dich_TTKH.Add(cboxDong_LoaiTK.Text);

        //    nguon_TTKH.Add("<KH_DONG_LTT>");
        //    dich_TTKH.Add(tk_bus.TAI_KHOAN_THEO_STK(cboxCN_DONG_TK.Text).Rows[0]["CCY"].ToString());

        //    string ngaydongtk = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
        //    nguon_TTKH.Add("<KH_NGAYDONGTK>");
        //    dich_TTKH.Add(ngaydongtk);

        //    nguon_TTKH.Add("<NGAY_DONGTK>");
        //    dich_TTKH.Add(DateTime.Now.ToString("dd"));

        //    nguon_TTKH.Add("<THANG_DONGTK>");
        //    dich_TTKH.Add(DateTime.Now.ToString("MM"));

        //    nguon_TTKH.Add("<NAM_DONGTK>");
        //    dich_TTKH.Add(DateTime.Now.ToString("yyyy"));

        //    nguon_TTKH.Add("<LYDODONGTK>");
        //    dich_TTKH.Add(txtLyDoDongTK.Text);

        //    nguon_TTKH.Add("<XULYSODU>");
        //    dich_TTKH.Add(txtXuLySoDu.Text);
        //}


        //Lấy thông tin thay đổi thông tin dùng cho mẫu 07TKDVVN - CHINH SUA BO SUNG THONG TIN KHACH HANG CA NHAN 
        //và 09TKDVVN - CHINH SUA BO SUNG THONG TIN KHACH HANG DANH CHO NGAN HANG
        
        
        //Lấy thông tin ủy quyền mua ngoại tệ dùng cho mẫu 01VBAHD - UY QUYEN MUA NGOAI TE.docx
        //internal void LAY_TT_UQMNT()
        //{
        //    nguon_TTKH.Add("<KH_UQMNT_STK>");
        //    dich_TTKH.Add(cboxUQMNT_TKKH.Text);

        //    nguon_TTKH.Add("<UQMNT_DAIDIENNH>");
        //    dich_TTKH.Add(txtUQMNT_DAIDIENNH.Text);

        //    nguon_TTKH.Add("<UQMNT_CHUCVUDAIDIENNH>");
        //    dich_TTKH.Add(txtUQMNT_CHUCVUDAIDIENNH.Text);

        //    if (txtUQMNT_GUQ.Text == "")
        //    {
        //        nguon_TTKH.Add("<UQMNT_GUQ>");
        //        dich_TTKH.Add("");
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<UQMNT_GUQ>");
        //        dich_TTKH.Add(txtUQMNT_GUQ.Text);
        //    }

        //    nguon_TTKH.Add("<UQMNT_DAIDIENNH_CHUKY>");
        //    dich_TTKH.Add(cboxLanhdao.Text);
        //}

        ////Lấy thông tin ủy quyền sử dụng tài khoản dùng cho mẫu 02/VBAHD - UY QUYEN SU DUNG TAI KHOAN.docx
        //internal void LAY_TT_UQSDTK()
        //{
        //    nguon_TTKH.Add("<DUQSDTK_KH_HOTEN>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_HOTEN.Text);

        //    nguon_TTKH.Add("<DUQSDTK_KH_NGAYSINH>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_NGAYSINH.Text);

        //    nguon_TTKH.Add("<DUQSDTK_KH_DTDD>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_DTDD.Text);

        //    nguon_TTKH.Add("<DUQSDTK_KH_NGHENGHIEP>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_NGHENGHIEP.Text);

        //    nguon_TTKH.Add("<DUQSDTK_KH_CMND>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_CMND.Text);

        //    nguon_TTKH.Add("<DUQSDTK_KH_NGAYCAPCMND>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_NGAYCAPCMND.Text);

        //    nguon_TTKH.Add("<DUQSDTK_KH_NOICAPCMND>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_NOICAPCMND.Text);

        //    nguon_TTKH.Add("<DUQSDTK_KH_DIACHI>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_DIACHI.Text);

        //    nguon_TTKH.Add("<KH_UQSDTK_STK>");
        //    dich_TTKH.Add(cboxUQSDTK_STK.Text);

        //    if (rdbUQSDTK_HL_Macdinh.Checked == true)
        //    {
        //        string hlmd = "Từ ngày ";
        //        hlmd += DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
        //        hlmd += " cho đến khi ";
        //        hlmd += Thongtindangnhap.tencn;
        //        hlmd += " nhận được, xác nhận hợp lệ đối với văn bản về việc hủy/chấm dứt/thay đổi việc ủy quyền và không bị giới hạn hiệu lực bởi thời hạn (01) năm theo quy định tại Điều 582 Bộ luật dân sự.";
        //        nguon_TTKH.Add("<UQSDTK_HIEU_LUC_MAC_DINH>");
        //        dich_TTKH.Add(hlmd);

        //        nguon_TTKH.Add("<UQSDTK_HIEU_LUC_TU_NGAY>");
        //        dich_TTKH.Add("");
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<UQSDTK_HIEU_LUC_MAC_DINH>");
        //        dich_TTKH.Add("");

        //        nguon_TTKH.Add("<UQSDTK_HIEU_LUC_TU_NGAY>");
        //        dich_TTKH.Add("Từ ngày: " + txtUQSDTK_HL_Tungay.Text + ".");
        //    }
        //}

        ////Lấy thông tin ủy quyền giao dịch tài khoản tiết kiệm dùng cho mẫu 03/VBAHD - 03VBAHD - UY QUYEN GIAO DICH TAI KHOAN TIET KIEM.docx
        //internal void LAY_TT_UQGDTKTK()
        //{
        //    nguon_TTKH.Add("<DUQGDTKTK_KH_HOTEN>");
        //    dich_TTKH.Add(txtDUQGDTKTK_KH_HOTEN.Text);

        //    nguon_TTKH.Add("<DUQGDTKTK_KH_NGAYSINH>");
        //    dich_TTKH.Add(txtDUQGDTKTK_KH_NGAYSINH.Text);

        //    nguon_TTKH.Add("<DUQGDTKTK_KH_DTDD>");
        //    dich_TTKH.Add(txtDUQGDTKTK_KH_DTDD.Text);

        //    nguon_TTKH.Add("<DUQGDTKTK_KH_NGHENGHIEP>");
        //    dich_TTKH.Add(txtDUQGDTKTK_KH_NGHENGHIEP.Text);

        //    nguon_TTKH.Add("<DUQGDTKTK_KH_CMND>");
        //    dich_TTKH.Add(txtDUQSDTK_KH_CMND.Text);

        //    nguon_TTKH.Add("<DUQGDTKTK_KH_NGAYCAPCMND>");
        //    dich_TTKH.Add(txtDUQGDTKTK_KH_NGAYCAPCMND.Text);

        //    nguon_TTKH.Add("<DUQGDTKTK_KH_NOICAPCMND>");
        //    dich_TTKH.Add(txtDUQGDTKTK_KH_NOICAPCMND.Text);

        //    nguon_TTKH.Add("<DUQGDTKTK_KH_DIACHI>");
        //    dich_TTKH.Add(txtDUQGDTKTK_KH_DIACHI.Text);

        //    if (chkUQGDTKTK_TK1.Checked == true)
        //    {
        //        string sotk1 = cboxUQGDTKTK_TK1.Text;
        //        string serial1 = txtUQGDTKTK_Serial_TK1.Text;
        //        string sotien1 = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK1.Text)));
        //        string sotienbangchu1 = CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK1.Text)));
        //        nguon_TTKH.Add("<UQGDTKTK_TK1>");
        //        dich_TTKH.Add("1. Số TK: " + sotk1 + "   Số Sêri: " + serial1 + "   Số tiền: " + sotien1 + " đồng.");

        //        nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU1>");
        //        dich_TTKH.Add("(Bằng chữ: " + sotienbangchu1 + "đồng).");

        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<UQGDTKTK_TK1>");
        //        dich_TTKH.Add("");

        //        nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU1>");
        //        dich_TTKH.Add("");
        //    }

        //    if (chkUQGDTKTK_TK2.Checked == true)
        //    {
        //        string sotk2 = cboxUQGDTKTK_TK2.Text;
        //        string serial2 = txtUQGDTKTK_Serial_TK2.Text;
        //        string sotien2 = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK2.Text)));
        //        string sotienbangchu2 = CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK2.Text)));
        //        nguon_TTKH.Add("<UQGDTKTK_TK2>");
        //        dich_TTKH.Add("2. Số TK: " + sotk2 + "   Số Sêri: " + serial2 + "   Số tiền: " + sotien2 + " đồng.");

        //        nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU2>");
        //        dich_TTKH.Add("(Bằng chữ: " + sotienbangchu2 + "đồng).");

        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<UQGDTKTK_TK2>");
        //        dich_TTKH.Add("");

        //        nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU2>");
        //        dich_TTKH.Add("");
        //    }

        //    if (chkUQGDTKTK_TK3.Checked == true)
        //    {
        //        string sotk3 = cboxUQGDTKTK_TK3.Text;
        //        string serial3 = txtUQGDTKTK_Serial_TK3.Text;
        //        string sotien3 = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK3.Text)));
        //        string sotienbangchu3 = CommonMethod.FirstCharToUpper(CommonMethod.ChuyenSoSangChu(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK3.Text)));
        //        nguon_TTKH.Add("<UQGDTKTK_TK3>");
        //        dich_TTKH.Add("3. Số TK: " + sotk3 + "   Số Sêri: " + serial3 + "   Số tiền: " + sotien3 + " đồng.");

        //        nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU3>");
        //        dich_TTKH.Add("(Bằng chữ: " + sotienbangchu3 + "đồng).");

        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<UQGDTKTK_TK3>");
        //        dich_TTKH.Add("");

        //        nguon_TTKH.Add("<UQGDTKTK_ST_BANGCHU3>");
        //        dich_TTKH.Add("");
        //    }

        //    nguon_TTKH.Add("<UQGDTKTK_LY_DO>");
        //    dich_TTKH.Add(txtUQGDTKTK_LY_DO.Text);

        //    if (rdbUQGDTKTK_HIEU_LUC_MAC_DINH.Checked == true)
        //    {
        //        string hlmd = "Từ ngày ";
        //        hlmd += DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
        //        hlmd += " cho đến khi ";
        //        hlmd += Thongtindangnhap.tencn;
        //        hlmd += " nhận được, xác nhận hợp lệ đối với văn bản về việc hủy/chấm dứt/thay đổi việc ủy quyền và không bị giới hạn hiệu lực bởi thời hạn (01) năm theo quy định tại Điều 582 Bộ luật dân sự.";
        //        nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_MAC_DINH>");
        //        dich_TTKH.Add(hlmd);

        //        nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_TU_NGAY>");
        //        dich_TTKH.Add("");
        //    }
        //    else
        //    {
        //        nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_MAC_DINH>");
        //        dich_TTKH.Add("");

        //        nguon_TTKH.Add("<UQGDTKTK_HIEU_LUC_TU_NGAY>");
        //        dich_TTKH.Add("Từ ngày: " + txtUQGDTKTK_HIEU_LUC_TU_NGAY.Text + " đến ngày " + txtUQGDTKTK_HIEU_LUC_TU_NGAY.Text + ".");
        //    }
        //}
        //internal void MO_TAIKHOAN()
        //{
        //    string ccy = "VND";
        //    if (chkVND.Checked == true)
        //    {
        //        ccy = "VND";
        //    }

        //    if (chkUSD.Checked == true)
        //    {
        //        ccy = "USD";
        //    }

        //    if (chkEURO.Checked == true)
        //    {
        //        ccy = "EUR";
        //    }

        //    if (chkKhac.Checked == true)
        //    {
        //        ccy = txtLTT_KHAC.Text;
        //    }

        //    string ngaymo = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //    if (tk_bus.MO_TAIKHOAN(dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString(), txtCN_MO_TK.Text, ccy, ngaymo) == true)
        //    {
        //        MessageBox.Show("Đã thêm tài khoản mới");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi thêm tài khoản mới");
        //    }
        //}

        //internal void DONG_TAIKHOAN()
        //{
        //    string ngaydong = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //    bool dongtk = tk_bus.DONG_TAIKHOAN(cboxCN_DONG_TK.Text, ngaydong);
        //}
        private void btnTaomaubieu_Click(object sender, EventArgs e)
        {
            string file_mau_bieu = cboxMaubieu.SelectedValue.ToString();
            if (cboxMaubieu.Text.Contains(@"04/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpXacnhansodutk;
                if (txtXNSD_MaKH.Text == "")
                {
                    MessageBox.Show("Chưa có dữ liệu. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtXNSD_KH_DIACHI_EN.Text == "")
                {
                    MessageBox.Show("Chưa nhập địa chỉ khách hàng bằng tiếng Anh. Đề nghị kiểm tra lại!");
                    txtXNSD_KH_DIACHI_EN.Focus();
                    return;
                }

                if (txtXNSD_STK1.Text == "" && txtXNSD_STK2.Text == "" && txtXNSD_STK3.Text == "" && txtXNSD_STK4.Text == "" && txtXNSD_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtXNSD_STK1.Text != "" && txtXNSD_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtXNSD_Soso1.Focus();
                    return;
                }

                if (txtXNSD_STK2.Text != "" && txtXNSD_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtXNSD_Soso2.Focus();
                    return;
                }

                if (txtXNSD_STK3.Text != "" && txtXNSD_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtXNSD_Soso3.Focus();
                    return;
                }

                if (txtXNSD_STK4.Text != "" && txtXNSD_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtXNSD_Soso4.Focus();
                    return;
                }

                if (txtXNSD_STK5.Text != "" && txtXNSD_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtXNSD_Soso5.Focus();
                    return;
                }

                if (txtXNSD_Soban.Text == "")
                {
                    MessageBox.Show("Chưa nhập số bản xác nhận cần tạo. Đề nghị kiểm tra lại!");
                    txtXNSD_Soban.Focus();
                    return;
                }
                else if (Convert.ToByte(txtXNSD_Soban.Text) <3)
                {
                    MessageBox.Show("Số bản tối thiểu là 3. Đề nghị kiểm tra lại!");
                    txtXNSD_Soban.Focus();
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin xác nhận số dư
                LAY_TT_XNSD();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT/\nItem");
                list_title.Add("Số sổ/\nPassbook No.");
                list_title.Add("Số tài khoản/\nAccount No.");
                list_title.Add("Loại tiền/\nType of Currency");
                list_title.Add("Ngày gửi/\nIssued date");
                list_title.Add("Kỳ hạn (tháng)/\nTerm (months)");
                list_title.Add("Số dư/\nCurrent Balance");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[7] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("SO_SO", typeof(string)),
                        new DataColumn("STK", typeof(string)),
                        new DataColumn("LOAI_TIEN", typeof(string)),
                        new DataColumn("NGAY_GUI", typeof(string)),
                        new DataColumn("KY_HAN", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                    }
                );
                DataRow stk_dr;
                int i = 0;
                if (txtXNSD_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["SO_SO"] = txtXNSD_Soso1.Text;
                    stk_dr["STK"] = txtXNSD_STK1.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSD_Loaitien1.Text;
                    stk_dr["NGAY_GUI"] = txtXNSD_Ngaygui1.Text;
                    stk_dr["KY_HAN"] = txtXNSD_Kyhan1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSD_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["SO_SO"] = txtXNSD_Soso1.Text;
                    stk_dr["STK"] = txtXNSD_STK2.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSD_Loaitien2.Text;
                    stk_dr["NGAY_GUI"] = txtXNSD_Ngaygui2.Text;
                    stk_dr["KY_HAN"] = txtXNSD_Kyhan2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSD_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["SO_SO"] = txtXNSD_Soso3.Text;
                    stk_dr["STK"] = txtXNSD_STK3.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSD_Loaitien3.Text;
                    stk_dr["NGAY_GUI"] = txtXNSD_Ngaygui3.Text;
                    stk_dr["KY_HAN"] = txtXNSD_Kyhan3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSD_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["SO_SO"] = txtXNSD_Soso4.Text;
                    stk_dr["STK"] = txtXNSD_STK4.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSD_Loaitien4.Text;
                    stk_dr["NGAY_GUI"] = txtXNSD_Ngaygui4.Text;
                    stk_dr["KY_HAN"] = txtXNSD_Kyhan4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSD_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["SO_SO"] = txtXNSD_Soso5.Text;
                    stk_dr["STK"] = txtXNSD_STK5.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSD_Loaitien5.Text;
                    stk_dr["NGAY_GUI"] = txtXNSD_Ngaygui5.Text;
                    stk_dr["KY_HAN"] = txtXNSD_Kyhan5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                stk_dr = stk_dt.NewRow();
                stk_dr["STT"] = "";
                stk_dr["SO_SO"] = "Tổng cộng (Total)";
                stk_dr["STK"] = "";
                stk_dr["LOAI_TIEN"] = txtXNSD_Loaitien.Text;
                stk_dr["NGAY_GUI"] = "";
                stk_dr["KY_HAN"] = "";
                stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Tongsodu.Text)));
                stk_dt.Rows.Add(stk_dr);
                i = i + 1;
                
                string font_family = "Times New Roman";
                double font_size = 11;
                bool last_row_bold = true;
                int merge_row_index = stk_dt.Rows.Count;
                int start_index = 1;
                int end_index = 2;
                string p_footer = "";
                if (Thongtindangnhap.hs)
                {
                    p_footer = Thongtindangnhap.diachicn_en + "\n" + "Tel: " + Thongtindangnhap.sdt_pb + ".   Fax: " + Thongtindangnhap.fax_pb + ".";
                }
                else
                {
                    p_footer = Thongtindangnhap.diachipb_en +"\n"+ "Tel: " + Thongtindangnhap.sdt_pb + ".   Fax: " + Thongtindangnhap.fax_pb + ".";
                }

                //Lấy file mẫu biểu tương ứng với chi nhánh
                //file_mau_bieu = file_mau_bieu.Substring(0, file_mau_bieu.Length - 5) + " - " + Thongtindangnhap.mapb +".docx";
         
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, merge_row_index, start_index, end_index, p_footer);         
            }
            else  if (cboxMaubieu.Text.Contains(@"05/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpXacnhansodutt;

                if (txtXNSDTT_KH_DIACHI_EN.Text == "")
                {
                    MessageBox.Show("Chưa nhập địa chỉ khách hàng bằng tiếng Anh. Đề nghị kiểm tra lại!");
                    txtXNSDTT_KH_DIACHI_EN.Focus();
                    return;
                }
                else if (Convert.ToByte(txtXNSDTT_Soban.Text) < 3)
                {
                    MessageBox.Show("Số bản tối thiểu là 3. Đề nghị kiểm tra lại!");
                    txtXNSDTT_Soban.Focus();
                    return;
                }
                else if (Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text)) < 0 )
                {
                    MessageBox.Show("Chương trình không hỗ trợ xác nhận số dư âm. Đề nghị kiểm tra lại!");
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin xác nhận số dư
                LAY_TT_XNSDTT();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT/\nItem");
                list_title.Add("Số tài khoản/\nAccount No.");
                list_title.Add("Loại tiền/\nType of Currency");
                list_title.Add("Ngày mở/\nOpen date");
                list_title.Add("Kỳ hạn (tháng)/\nTerm");
                list_title.Add("Số dư/\nCurrent Balance");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[6] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("STK", typeof(string)),
                        new DataColumn("LOAI_TIEN", typeof(string)),
                        new DataColumn("NGAY_MO", typeof(string)),
                        new DataColumn("KY_HAN", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                    }
                );
                DataRow stk_dr;
                int i = 0;
                if (txtXNSDTT_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["STK"] = txtXNSDTT_STK1.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSDTT_Loaitien1.Text;
                    stk_dr["NGAY_MO"] = txtXNSDTT_Ngaymo1.Text;
                    stk_dr["KY_HAN"] = "Không kỳ hạn/None";
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSDTT_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["STK"] = txtXNSDTT_STK2.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSDTT_Loaitien2.Text;
                    stk_dr["NGAY_MO"] = txtXNSDTT_Ngaymo2.Text;
                    stk_dr["KY_HAN"] = "Không kỳ hạn/None";
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSDTT_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["STK"] = txtXNSDTT_STK3.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSDTT_Loaitien3.Text;
                    stk_dr["NGAY_MO"] = txtXNSDTT_Ngaymo3.Text;
                    stk_dr["KY_HAN"] = "Không kỳ hạn/None";
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSDTT_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["STK"] = txtXNSDTT_STK4.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSDTT_Loaitien4.Text;
                    stk_dr["NGAY_MO"] = txtXNSDTT_Ngaymo4.Text;
                    stk_dr["KY_HAN"] = "Không kỳ hạn/None";
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtXNSDTT_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["STK"] = txtXNSDTT_STK5.Text;
                    stk_dr["LOAI_TIEN"] = txtXNSDTT_Loaitien5.Text;
                    stk_dr["NGAY_MO"] = txtXNSDTT_Ngaymo5.Text;
                    stk_dr["KY_HAN"] = "Không kỳ hạn/None";
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                stk_dr = stk_dt.NewRow();
                stk_dr["STT"] = "";
                stk_dr["STK"] = "Tổng cộng (Total)";
                stk_dr["LOAI_TIEN"] = txtXNSDTT_Loaitien.Text;
                stk_dr["NGAY_MO"] = "";
                stk_dr["KY_HAN"] = "";
                stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text)));
                stk_dt.Rows.Add(stk_dr);
                i = i + 1;

                string font_family = "Times New Roman";
                double font_size = 11;
                bool last_row_bold = true;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                if (Thongtindangnhap.hs)
                {
                    p_footer = Thongtindangnhap.diachicn_en + "\n" + "Tel: " + Thongtindangnhap.sdt_pb + ".   Fax: " + Thongtindangnhap.fax_pb + ".";
                }
                else
                {
                    p_footer = Thongtindangnhap.diachipb_en + "\n" + "Tel: " + Thongtindangnhap.sdt_pb + ".   Fax: " + Thongtindangnhap.fax_pb + ".";
                }

                //Lấy file mẫu biểu tương ứng với chi nhánh
                //file_mau_bieu = file_mau_bieu.Substring(0, file_mau_bieu.Length - 5) + " - " + Thongtindangnhap.mapb +".docx";

                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, merge_row_index, start_index, end_index, p_footer);
            }
        }

        private void dgvDanhsachCN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////Điền thông tin vào tabPage Thông tin chung 1
                //txtXNSD_MaKH.Text = dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString();
                //txtTDTT_MAKH.Text = dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString();
                //txtTDTT_KH_HOTEN.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString();
                //txtXNSD_KH_HOTEN.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString();
                //txtCN_DTDD1.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();

                ////Điền thông tin vào tabPage Thay đổi thông tin
                //txtTDTT_LoaiKH_cu.Text = dgvDanhsachTK.CurrentRow.Cells["Loại KH"].Value.ToString();
                //txtTDTT_HoTenViet_cu.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString();
                //txtTDTT_CMND_cu.Text = dgvDanhsachTK.CurrentRow.Cells["CMND"].Value.ToString();
                //txtTDTT_NgayCapCMND_cu.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                //txtTDTT_NoiCapCMND_cu.Text = dgvDanhsachTK.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                //txtTDTT_DiaChi_cu.Text = dgvDanhsachTK.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                //txtTDTT_DienThoai_cu.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();
                ////txtMobile.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT di động"].Value.ToString();
                ////txtTel.Text = dgvDanhsachCN.CurrentRow.Cells["ĐT nhà"].Value.ToString();
                ////dtpNgaysinh.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                ////cbbGioitinh.Text = dgvDanhsachCN.CurrentRow.Cells["Giới tính"].Value.ToString();
                ////cbKH2890.Text = dgvDanhsachCN.CurrentRow.Cells["Đối tượng KH"].Value.ToString();
                ////txtAddress.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                ////txtAddress2.Text = dgvDanhsachCN.CurrentRow.Cells["Địa chỉ khác"].Value.ToString();
                ////txtEmail.Text = dgvDanhsachCN.CurrentRow.Cells["Email"].Value.ToString();

                ////txtWebsite.Text = dgvDanhsachCN.CurrentRow.Cells["Website"].Value.ToString();
                ////txtNHGD.Text = dgvDanhsachCN.CurrentRow.Cells["NH giao dịch"].Value.ToString();
                ////txtSothich.Text = dgvDanhsachCN.CurrentRow.Cells["Sở thích"].Value.ToString();
                ////cbbTinhtrang.Text = dgvDanhsachCN.CurrentRow.Cells["Tình trạng"].Value.ToString();
                ////txtThunhap.Text = dgvDanhsachCN.CurrentRow.Cells["Thu nhập"].Value.ToString();
                ////txtMaNV.Text = dgvDanhsachCN.CurrentRow.Cells["Tên đ.nhập"].Value.ToString();

                ////txtCMND.Text = dgvDanhsachCN.CurrentRow.Cells["CMND"].Value.ToString();

                ////dtpNgaycap.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                ////txtNoicap.Text = dgvDanhsachCN.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                ////dtpNgayKH.Text = dgvDanhsachCN.CurrentRow.Cells["Ngày kết hôn"].Value.ToString();
                ////txtChitiet.Text = dgvDanhsachCN.CurrentRow.Cells["Chi tiết"].Value.ToString();
                ////txtGhichu.Text = dgvDanhsachCN.CurrentRow.Cells["Ghi chú"].Value.ToString();

                ////cbbTinh.Text = dgvDanhsachCN.CurrentRow.Cells["Tỉnh"].Value.ToString();
                ////layDS_Huyen();
                ////cbbHuyen.Text = dgvDanhsachCN.CurrentRow.Cells["Huyện"].Value.ToString();
                ////layDS_Xa();
                ////cbbXa.Text = dgvDanhsachCN.CurrentRow.Cells["Xã"].Value.ToString();
                ////cbbLinhvuc.Text = dgvDanhsachCN.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                ////cbbLoaiKHIpcas.Text = dgvDanhsachCN.CurrentRow.Cells["Loại KH"].Value.ToString();

                ////Điền thông tin vào tabPage Đóng tài khoản
                ////Lấy danh sách số tài khoản tương ứng với mã khách hàng đã chọn gán vào cboxCN_STK
                //cboxCN_DONG_TK.DataSource = null;
                //cboxCN_DONG_TK.Items.Clear();
                //System.Data.DataTable dt_stk = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString());
                //cboxCN_DONG_TK.DataSource = dt_stk;
                //cboxCN_DONG_TK.DisplayMember = "SOTK";
                //cboxCN_DONG_TK.Refresh();

                ////Điền thông tin vào tabPage Ủy quyền mua bán ngoại tệ               
                ////Bên ủy quyền
                //txtUQMNT_HOTENKH.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString(); ;
                //txtUQMNT_NGAYSINHKH.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                //txtUQMNT_DTKH.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();
                //txtUQMNT_NGHENGHIEPKH.Text = dgvDanhsachTK.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
                //txtUQMNT_CMNDKH.Text = dgvDanhsachTK.CurrentRow.Cells["CMND"].Value.ToString(); ;
                //txtUQMNT_NGAYCAPCMNDKH.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp"].Value.ToString();
                //txtUQMNT_NOICAPCMNDKH.Text = dgvDanhsachTK.CurrentRow.Cells["Nơi cấp"].Value.ToString();
                //txtUQMNT_DCKH.Text = dgvDanhsachTK.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                //cboxUQMNT_TKKH.DataSource = null;
                //cboxUQMNT_TKKH.Items.Clear();
                ////System.Data.DataTable dt_stk = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachCN.CurrentRow.Cells["Mã KH"].Value.ToString());
                //cboxUQMNT_TKKH.DataSource = dt_stk;
                //cboxUQMNT_TKKH.DisplayMember = "SOTK";
                //cboxUQMNT_TKKH.Refresh();

                ////Bên được ủy quyền
                //if (Thongtindangnhap.hs)
                //{
                //    //Đối với trung tâm các chi nhánh
                //    txtUQMNT_CHINHANH.Text = Thongtindangnhap.tencn;
                //    txtUQMNT_DIACHINH.Text = Thongtindangnhap.diachicn;
                //}
                //else
                //{
                //    //Đối với phòng giao dịch
                //    txtUQMNT_CHINHANH.Text = Thongtindangnhap.tencn + " - " + Thongtindangnhap.tenpb;
                //    txtUQMNT_DIACHINH.Text = Thongtindangnhap.diachipb;
                //}
                //System.Data.DataTable nhanvien = nv_bus.NHAN_VIEN_THEO_MANV(cboxLanhdao.SelectedValue.ToString());
                //if (Convert.ToBoolean(nhanvien.Rows[0]["GIOITINH"].ToString()) == false)
                //{
                //    txtUQMNT_DAIDIENNH.Text = "Bà " + cboxLanhdao.Text;
                //}
                //else
                //{
                //    txtUQMNT_DAIDIENNH.Text = "Ông " + cboxLanhdao.Text;
                //}

                //txtUQMNT_CHUCVUDAIDIENNH.Text = nhanvien.Rows[0]["CHUCVU"].ToString();

                //txtUQMNT_GUQ.Text = nhanvien.Rows[0]["UYQUYEN"].ToString();
            }
            catch { }
        }

        //private void rdbTTT_UyQuyen_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbTTT_UyQuyen.Checked == true)
        //    {
        //        txtUQ_CMND.Enabled = true;
        //        txtUQ_HoTen.Enabled = true;
        //        txtUQ_NgayCapCMND.Enabled = true;
        //        txtUQ_NoiCapCMND.Enabled = true;
        //    }
        //    else
        //    {
        //        txtUQ_CMND.Enabled = false;
        //        txtUQ_HoTen.Enabled = false;
        //        txtUQ_NgayCapCMND.Enabled = false;
        //        txtUQ_NoiCapCMND.Enabled = false;

        //        txtUQ_CMND.Text = ".....................";
        //        txtUQ_HoTen.Text = ".....................";
        //        txtUQ_NgayCapCMND.Text = ".....................";
        //        txtUQ_NoiCapCMND.Text = ".....................";
        //    }
        //}

        //private void chkTTT_DKITN_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkTTT_DKITN.Checked == true)
        //    {
        //        txtInternet_DTDD.Enabled = true;
        //        txtInternet_HMGD.Enabled = true;

        //        txtInternet_DTDD.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();
        //    }
        //    else
        //    {
        //        txtInternet_DTDD.Enabled = false;
        //        txtInternet_HMGD.Enabled = false;

        //        txtInternet_DTDD.Text = ".....................";
        //        txtInternet_HMGD.Text = ".....................";
        //    }
        //}

        //private void chkTDTT_LoaiKH_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkTDTT_LoaiKH.Checked == true)
        //    {
        //        txtTDTT_LoaiKH_cu.Enabled = true;
        //        txtTDTT_LoaiKH_moi.Enabled = true;
        //    }
        //    else
        //    {
        //        txtTDTT_LoaiKH_cu.Enabled = false;
        //        //txtTDTT_LoaiKH_cu.Clear();
        //        txtTDTT_LoaiKH_moi.Enabled = false;
        //        txtTDTT_LoaiKH_moi.Clear();
        //    }
        //}

        //private void chkTDTT_HoTen_Viet_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkTDTT_HoTen_Viet.Checked == true)
        //    {
        //        txtTDTT_HoTenViet_cu.Enabled = true;
        //        txtTDTT_HoTenViet_moi.Enabled = true;
        //    }
        //    else
        //    {
        //        txtTDTT_HoTenViet_cu.Enabled = false;
        //        //txtTDTT_HoTenViet_cu.Clear();
        //        txtTDTT_HoTenViet_moi.Enabled = false;
        //        txtTDTT_HoTenViet_moi.Clear();
        //    }
        //}

        //private void chkTDTT_CMND_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkTDTT_CMND.Checked == true)
        //    {
        //        txtTDTT_CMND_cu.Enabled = true;
        //        txtTDTT_NgayCapCMND_cu.Enabled = true;
        //        txtTDTT_NoiCapCMND_cu.Enabled = true;
        //        txtTDTT_CMND_moi.Enabled = true;
        //        txtTDTT_NgayCapCMND_moi.Enabled = true;
        //        txtTDTT_NoiCapCMND_moi.Enabled = true;
        //    }
        //    else
        //    {
        //        txtTDTT_CMND_cu.Enabled = false;
        //        //txtTDTT_CMND_cu.Clear();
        //        txtTDTT_NgayCapCMND_cu.Enabled = false;
        //        //txtTDTT_NgayCapCMND_cu.Clear();
        //        txtTDTT_NoiCapCMND_cu.Enabled = false;
        //        //txtTDTT_NoiCapCMND_cu.Clear();
        //        txtTDTT_CMND_moi.Enabled = false;
        //        txtTDTT_NgayCapCMND_moi.Enabled = false;
        //        txtTDTT_NoiCapCMND_moi.Enabled = false;

        //        txtTDTT_CMND_moi.Clear();
        //        txtTDTT_NgayCapCMND_moi.Clear();
        //        txtTDTT_NoiCapCMND_moi.Clear();
        //    }
        //}

        //private void chkTDTT_DiaChi_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkTDTT_DiaChi.Checked == true)
        //    {
        //        txtTDTT_DiaChi_cu.Enabled = true;
        //        txtTDTT_DiaChi_moi.Enabled = true;
        //    }
        //    else
        //    {
        //        txtTDTT_DiaChi_cu.Enabled = false;
        //        //txtTDTT_DiaChi_cu.Clear();
        //        txtTDTT_DiaChi_moi.Enabled = false;
        //        txtTDTT_DiaChi_moi.Clear();
        //    }
        //}

        //private void chkTDTT_Dienthoai_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkTDTT_Dienthoai.Checked == true)
        //    {
        //        txtTDTT_DienThoai_cu.Enabled = true;
        //        txtTDTT_DienThoai_moi.Enabled = true;
        //    }
        //    else
        //    {
        //        txtTDTT_DienThoai_cu.Enabled = false;
        //        //txtTDTT_DienThoai_cu.Clear();
        //        txtTDTT_DienThoai_moi.Enabled = false;
        //        txtTDTT_DienThoai_moi.Clear();
        //    }
        //}

        //private void chkTDTT_Khac_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkTDTT_Khac.Checked == true)
        //    {
        //        txtTDTT_Khac.Enabled = true;
        //    }
        //    else
        //    {
        //        txtTDTT_Khac.Enabled = false;
        //        txtTDTT_Khac.Clear();
        //    }
        //}

        //private void buttonX2_Click(object sender, EventArgs e)
        //{
        //    if (txtCN_MO_TK.Text == "")
        //    {
        //        MessageBox.Show("Chưa nhập số tài khoản mới. Đề nghị kiểm tra lại");
        //        tctTTKHCN.SelectedTab = tpXacnhansodu;
        //        txtCN_MO_TK.Focus();
        //        return;
        //    }
        //    if (txtCN_MO_TK.Text.Substring(0, 4) != Thongtindangnhap.macn)
        //    {
        //        MessageBox.Show("Không thể cập nhật tài khoản khác chi nhánh");
        //        tctTTKHCN.SelectedTab = tpXacnhansodu;
        //        txtCN_MO_TK.Focus();
        //        return;
        //    }
        //    //Cập nhật tài khoản mới vào bảng TAIKHOAN
        //    MO_TAIKHOAN();
        //}

        private void cboxLanhdao_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable nhanvien = nv_bus.NHAN_VIEN_THEO_MANV(cboxLanhdao.SelectedValue.ToString());
            if (nhanvien.Rows.Count > 0)
            {
                chucvu_lanhdao = nhanvien.Rows[0]["CHUCVU"].ToString();
                //if (Convert.ToBoolean(nhanvien.Rows[0]["GIOITINH"].ToString()) == false)
                //{
                //    txtUQMNT_DAIDIENNH.Text = "Bà " + cboxLanhdao.Text;
                //}
                //else
                //{
                //    txtUQMNT_DAIDIENNH.Text = "Ông " + cboxLanhdao.Text;
                //}


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
                //txtUQMNT_GUQ.Text = nhanvien.Rows[0]["UYQUYEN"].ToString();
            }
        }

        //private void txtMaCN_Leave(object sender, EventArgs e)
        //{
        //    System.Data.DataTable chinhanh = cn_bus.CHI_NHANH_THEO_MACN(txtMaCN.Text);
        //    if (chinhanh.Rows.Count > 0)
        //    {
        //        txtChinhanhgoc.Text = chinhanh.Rows[0]["TENCN"].ToString();
        //    }
        //    else
        //    {
        //        txtChinhanhgoc.Text = "Agribank Chi nhánh...";
        //    }
        //}

        //Các hàm lấy thông tin khách hàng từ file

        //Nhập thông tin khách hàng cá nhân
        //private void lay_KHCN(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    //Định dạng ngày tháng theo dạng en-US cho hàm convert.todatetie
        //    IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {

        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    //định dạng mm/dd/yyy
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    //định dạng mm/dd/yyy
        //                    ngaysinh = "01/01/1900";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 1;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");

        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                if (dt_temp.Rows[i][14].ToString() != "")
        //                {
        //                    //Khách hàng sử dụng chứng minh nhân dân
        //                    dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                    ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                    if (ngaycap != "")
        //                    {
        //                        //định dạng mm/dd/yyy
        //                        ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                    }
        //                    else
        //                    {
        //                        //định dạng mm/dd/yyy
        //                        ngaycap = "01/01/1900";
        //                    }
        //                    dr["NGAYCAP"] = ngaycap;
        //                    dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                }
        //                else if (dt_temp.Rows[i][15].ToString() != "")
        //                {
        //                    //Khách hàng sử dụng hộ chiếu
        //                    dr["CMND"] = dt_temp.Rows[i][15].ToString();
        //                    ngaycap = dt_temp.Rows[i][36].ToString().Trim();
        //                    if (ngaycap != "")
        //                    {
        //                        //định dạng mm/dd/yyy
        //                        ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                    }
        //                    else
        //                    {
        //                        //định dạng mm/dd/yyy
        //                        ngaycap = "01/01/1900";
        //                    }
        //                    dr["NGAYCAP"] = ngaycap;
        //                    dr["NOICAP"] = dt_temp.Rows[i][35].ToString();
        //                }
        //                else
        //                {
        //                    dr["CMND"] = "";
        //                    dr["NGAYCAP"] = "01/01/1900";
        //                    dr["NOICAP"] = "";
        //                }
        //                //dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                //dr["NGAYCAP"] = ngaycap;
        //                //dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Cá nhân"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "14";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Doanh nghiệp tư nhân
        //private void lay_KHDNTN(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {
        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Doanh nghiệp tư nhân"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "23";
        //                dr["DOITUONGDN"] = "2";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Hộ gia đình
        //private void lay_KHHGD(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {

        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Hộ gia đình"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "13";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Hợp tác xã
        //private void lay_KHHTX(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {
        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Hợp tác xã"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "24";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Công ty cổ phần
        //private void lay_KHCTCP(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {
        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty cổ phần"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "21";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Công ty TNHH
        //private void lay_KHCTTNHH(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {
        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty TNHH"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "21";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng công ty liên doanh
        //private void lay_KHCTLD(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {
        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty liên doanh"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "21";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Doanh nghiệp có vốn đầu tư nước ngoài
        //private void lay_KHDNDTNN(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {
        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Doanh nghiệp có vốn ĐT nước ngoài"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "14";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Doanh nghiệp nhà nước
        //private void lay_KHDNNN(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {

        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Doanh nghiệp Nhà nước"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "41";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng tổ chức tài chính
        //private void lay_KHTCTC(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {

        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức Tài chính"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "41";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng Tổ chức xã hội
        //private void lay_KHTCXH(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {

        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức XH TƯ & Địa phương"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "34";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        ////Nhập thông tin khách hàng tổ chức
        //private void lay_KHTC(System.Data.DataTable dt_temp)
        //{
        //    System.Data.DataTable dt_temp2 = new System.Data.DataTable();
        //    dt_temp2.Columns.AddRange
        //    (
        //        new DataColumn[39] 
        //        { 
        //            new DataColumn("MAKH", typeof(string)),
        //            new DataColumn("HOTEN", typeof(string)),
        //            new DataColumn("DIACHI1", typeof(string)),
        //            new DataColumn("DIACHI2", typeof(string)),
        //            new DataColumn("DIENTHOAI1", typeof(string)),
        //            new DataColumn("DIENTHOAI2", typeof(string)),
        //            new DataColumn("EMAIL", typeof(string)),
        //            new DataColumn("CMND", typeof(string)),
        //            new DataColumn("NGAYCAP", typeof(string)),
        //            new DataColumn("NOICAP", typeof(string)),
        //            new DataColumn("NGAYSINH", typeof(string)),
        //            new DataColumn("GIOITINH", typeof(bool)),
        //            new DataColumn("LINHVUC", typeof(string)),
        //            new DataColumn("WEBSITE", typeof(string)),
        //            new DataColumn("GPDK", typeof(string)),
        //            new DataColumn("QDTL", typeof(string)),
        //            new DataColumn("MST", typeof(string)),
        //            new DataColumn("LOAIKH", typeof(int)),
        //            new DataColumn("THUNHAP", typeof(decimal)),
        //            new DataColumn("SOTHICH", typeof(string)),
        //            new DataColumn("MANV", typeof(string)),
        //            new DataColumn("NHGIAODICH", typeof(string)),
        //            new DataColumn("GHICHU", typeof(string)),
        //            new DataColumn("MACN", typeof(string)),
        //            new DataColumn("TINHTRANG", typeof(bool)),
        //            new DataColumn("CTLOAIKH", typeof(string)),
        //            new DataColumn("TINH", typeof(string)),
        //            new DataColumn("HUYEN", typeof(string)),
        //            new DataColumn("XA", typeof(string)),
        //            new DataColumn("LOAIKH_IPCAS", typeof(string)),
        //            new DataColumn("NGAYKETHON", typeof(string)),
        //            new DataColumn("NGAYTHANHLAP", typeof(string)),
        //            new DataColumn("NGAYTAO", typeof(string)),
        //            new DataColumn("DOITUONGKH", typeof(string)),
        //            new DataColumn("DOITUONGDN", typeof(string)),
        //            new DataColumn("VONDAUTU", typeof(decimal)),
        //            new DataColumn("SOLAODONG", typeof(decimal)),
        //            new DataColumn("DSXNK", typeof(decimal)),
        //            new DataColumn("NGAYTLNGANH", typeof(string))
        //        }
        //    );
        //    DataRow dr;

        //    for (int i = 0; i < dt_temp.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
        //            {
        //                String ngaycap, ngaysinh, didong;
        //                String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
        //                String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
        //                didong = dt_temp.Rows[i][9].ToString();
        //                ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
        //                if (ngaysinh != "")
        //                {
        //                    ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaysinh = "01/01/1990";
        //                }
        //                ngaycap = dt_temp.Rows[i][34].ToString().Trim();
        //                if (ngaycap != "")
        //                {
        //                    ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
        //                }
        //                else
        //                {
        //                    ngaycap = "01/01/1990";
        //                }
        //                String gt = dt_temp.Rows[i][10].ToString();
        //                Int16 gioitinh;
        //                if (gt == "Nam" || gt == "Male" || gt == "nam")
        //                {
        //                    gioitinh = 1;
        //                }
        //                else
        //                {
        //                    gioitinh = 0;
        //                }
        //                Int16 loaikh = 2;
        //                String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
        //                dr = dt_temp2.NewRow();
        //                dr["MAKH"] = dt_temp.Rows[i][0].ToString();
        //                dr["HOTEN"] = hoten;
        //                dr["DIACHI1"] = diachi1;
        //                dr["DIACHI2"] = diachi2;
        //                dr["DIENTHOAI1"] = didong;
        //                dr["DIENTHOAI2"] = "";
        //                dr["EMAIL"] = "";
        //                dr["CMND"] = dt_temp.Rows[i][14].ToString();
        //                dr["NGAYCAP"] = ngaycap;
        //                dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
        //                dr["NGAYSINH"] = ngaysinh;
        //                dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
        //                dr["LINHVUC"] = "";
        //                dr["WEBSITE"] = "";
        //                dr["GPDK"] = dt_temp.Rows[i][31].ToString();
        //                dr["QDTL"] = dt_temp.Rows[i][30].ToString();
        //                dr["MST"] = dt_temp.Rows[i][45].ToString();
        //                dr["LOAIKH"] = loaikh;
        //                dr["THUNHAP"] = 0;
        //                dr["SOTHICH"] = "";
        //                dr["MANV"] = "";
        //                dr["NHGIAODICH"] = "";
        //                dr["GHICHU"] = "";
        //                dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
        //                dr["TINHTRANG"] = true;
        //                dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
        //                dr["TINH"] = dt_temp.Rows[i][46].ToString();
        //                dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
        //                dr["XA"] = dt_temp.Rows[i][48].ToString();
        //                dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức"
        //                dr["NGAYKETHON"] = "01/01/1900";
        //                dr["NGAYTHANHLAP"] = "01/01/1900";
        //                dr["NGAYTAO"] = ngaytao;
        //                dr["DOITUONGKH"] = "35";
        //                dr["DOITUONGDN"] = "";
        //                dr["VONDAUTU"] = 0;
        //                dr["SOLAODONG"] = 0;
        //                dr["DSXNK"] = 0;
        //                dr["NGAYTLNGANH"] = "01/01/1900";
        //                dt_temp2.Rows.Add(dr);
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    //Xóa các dòng có cùng mã khách hàng
        //    dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

        //    //Nhập thông tin vào bảng KHACHHANG
        //    if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
        //    {
        //        MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
        //    }
        //}

        //private void btnLay_TT_BENA_Click(object sender, EventArgs e)
        //{
        //    if (dgvDanhsachTK.SelectedCells.Count > 0)
        //    {
        //        txtXNSD_MaKH.Text = dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString();
        //        txtUQSDTK_KH_HOTEN.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString();
        //        txtUQSDTK_KH_NGAYSINH.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày sinh"].Value.ToString();
        //        txtUQSDTK_KH_DTDD.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();
        //        txtUQSDTK_KH_NGHENGHIEP.Text = dgvDanhsachTK.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
        //        txtUQSDTK_KH_CMND.Text = dgvDanhsachTK.CurrentRow.Cells["CMND"].Value.ToString();
        //        txtUQSDTK_KH_NGAYCAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp"].Value.ToString();
        //        txtUQSDTK_KH_NOICAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Nơi cấp"].Value.ToString();
        //        txtUQSDTK_KH_DIACHI.Text = dgvDanhsachTK.CurrentRow.Cells["Địa chỉ"].Value.ToString();

        //        //Điền thông tin vào tabPage Đóng tài khoản
        //        //Lấy danh sách số tài khoản tương ứng với mã khách hàng đã chọn gán vào cboxCN_STK
        //        cboxUQSDTK_STK.DataSource = null;
        //        cboxUQSDTK_STK.Items.Clear();
        //        System.Data.DataTable dt_stk = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString());
        //        cboxUQSDTK_STK.DataSource = dt_stk;
        //        cboxUQSDTK_STK.DisplayMember = "SOTK";
        //        cboxUQSDTK_STK.Refresh();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới.");
        //    }
        //}

        //private void btnLay_TT_BENB_Click(object sender, EventArgs e)
        //{
        //    if (dgvDanhsachTK.SelectedCells.Count > 0)
        //    {
        //        txtDUQSDTK_KH_HOTEN.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString();
        //        txtDUQSDTK_KH_NGAYSINH.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày sinh"].Value.ToString();
        //        txtDUQSDTK_KH_DTDD.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();
        //        txtDUQSDTK_KH_NGHENGHIEP.Text = dgvDanhsachTK.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
        //        txtDUQSDTK_KH_CMND.Text = dgvDanhsachTK.CurrentRow.Cells["CMND"].Value.ToString();
        //        txtDUQSDTK_KH_NGAYCAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp"].Value.ToString();
        //        txtDUQSDTK_KH_NOICAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Nơi cấp"].Value.ToString();
        //        txtDUQSDTK_KH_DIACHI.Text = dgvDanhsachTK.CurrentRow.Cells["Địa chỉ"].Value.ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới để thực hiện chức năng này.");
        //    }
        //}

        //private void rdbUQSDTK_HL_Tungay_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbUQSDTK_HL_Tungay.Checked == true)
        //    {
        //        txtUQSDTK_HL_Tungay.Enabled = true;
        //    }
        //    else
        //    {
        //        txtUQSDTK_HL_Tungay.Enabled = false;
        //        txtUQSDTK_HL_Tungay.Clear();
        //    }
        //}

        //private void chkUQGDTKTK_TK1_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkUQGDTKTK_TK1.Checked == true)
        //    {
        //        cboxUQGDTKTK_TK1.Enabled = true;
        //        txtUQGDTKTK_Serial_TK1.Enabled = true;
        //        txtUQGDTKTK_Sotien_TK1.Enabled = true;
        //    }
        //    else
        //    {
        //        cboxUQGDTKTK_TK1.Enabled = false;
        //        txtUQGDTKTK_Serial_TK1.Enabled = false;
        //        txtUQGDTKTK_Serial_TK1.Clear();
        //        txtUQGDTKTK_Sotien_TK1.Enabled = false;
        //        txtUQGDTKTK_Sotien_TK1.Clear();
        //    }
        //}

        //private void chkUQGDTKTK_TK2_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkUQGDTKTK_TK2.Checked == true)
        //    {
        //        cboxUQGDTKTK_TK2.Enabled = true;
        //        txtUQGDTKTK_Serial_TK2.Enabled = true;
        //        txtUQGDTKTK_Sotien_TK2.Enabled = true;
        //    }
        //    else
        //    {
        //        cboxUQGDTKTK_TK2.Enabled = false;
        //        txtUQGDTKTK_Serial_TK2.Enabled = false;
        //        txtUQGDTKTK_Serial_TK2.Clear();
        //        txtUQGDTKTK_Sotien_TK2.Enabled = false;
        //        txtUQGDTKTK_Sotien_TK2.Clear();
        //    }
        //}

        //private void chkUQGDTKTK_TK3_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (chkUQGDTKTK_TK3.Checked == true)
        //    {
        //        cboxUQGDTKTK_TK3.Enabled = true;
        //        txtUQGDTKTK_Serial_TK3.Enabled = true;
        //        txtUQGDTKTK_Sotien_TK3.Enabled = true;
        //    }
        //    else
        //    {
        //        cboxUQGDTKTK_TK3.Enabled = false;
        //        txtUQGDTKTK_Serial_TK3.Enabled = false;
        //        txtUQGDTKTK_Serial_TK3.Clear();
        //        txtUQGDTKTK_Sotien_TK3.Enabled = false;
        //        txtUQGDTKTK_Sotien_TK3.Clear();
        //    }
        //}

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

        //private void txtUQGDTKTK_Sotien_TK1_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtUQGDTKTK_Sotien_TK1.Text == "")
        //    {
        //        txtUQGDTKTK_Sotien_TK1.Text = null;
        //    }
        //    else
        //    {
        //        Int64 d = Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK1.Text));
        //        txtUQGDTKTK_Sotien_TK1.Text = d.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        //    }
        //    txtUQGDTKTK_Sotien_TK1.Select(txtUQGDTKTK_Sotien_TK1.Text.Length, 0);
        //}

        //private void txtUQGDTKTK_Sotien_TK2_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtUQGDTKTK_Sotien_TK2.Text == "")
        //    {
        //        txtUQGDTKTK_Sotien_TK2.Text = null;
        //    }
        //    else
        //    {
        //        Int64 d = Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK2.Text));
        //        txtUQGDTKTK_Sotien_TK2.Text = d.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        //    }
        //    txtUQGDTKTK_Sotien_TK2.Select(txtUQGDTKTK_Sotien_TK2.Text.Length, 0);
        //}

        //private void txtUQGDTKTK_Sotien_TK3_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtUQGDTKTK_Sotien_TK3.Text == "")
        //    {
        //        txtUQGDTKTK_Sotien_TK3.Text = null;
        //    }
        //    else
        //    {
        //        Int64 d = Convert.ToInt64(ControlFormat.skipComma(txtUQGDTKTK_Sotien_TK3.Text));
        //        txtUQGDTKTK_Sotien_TK3.Text = d.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        //    }
        //    txtUQGDTKTK_Sotien_TK3.Select(txtUQGDTKTK_Sotien_TK3.Text.Length, 0);
        //}

        //private void btnUQGDTKTK_BUQ_Click(object sender, EventArgs e)
        //{
        //    if (dgvDanhsachTK.SelectedCells.Count > 0)
        //    {
        //        txtXNSD_MaKH.Text = dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString();
        //        txtUQGDTKTK_KH_HOTEN.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString();
        //        txtUQGDTKTK_KH_NGAYSINH.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày sinh"].Value.ToString();
        //        txtUQGDTKTK_KH_DTDD.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();
        //        txtUQGDTKTK_KH_NGHENGHIEP.Text = dgvDanhsachTK.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
        //        txtUQGDTKTK_KH_CMND.Text = dgvDanhsachTK.CurrentRow.Cells["CMND"].Value.ToString();
        //        txtUQGDTKTK_KH_NGAYCAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp"].Value.ToString();
        //        txtUQGDTKTK_KH_NOICAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Nơi cấp"].Value.ToString();
        //        txtUQGDTKTK_KH_DIACHI.Text = dgvDanhsachTK.CurrentRow.Cells["Địa chỉ"].Value.ToString();

        //        //Lấy danh sách số tài khoản tương ứng với mã khách hàng đã chọn gán vào cboxUQGDTKTK_TK1, cboxUQGDTKTK_TK2, cboxUQGDTKTK_TK3, cboxUQGDTKTK_TK4
        //        cboxUQGDTKTK_TK1.DataSource = null;
        //        cboxUQGDTKTK_TK1.Items.Clear();
        //        System.Data.DataTable dt_stk1 = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString());
        //        cboxUQGDTKTK_TK1.DataSource = dt_stk1;
        //        cboxUQGDTKTK_TK1.DisplayMember = "SOTK";
        //        cboxUQGDTKTK_TK1.Refresh();

        //        cboxUQGDTKTK_TK2.DataSource = null;
        //        cboxUQGDTKTK_TK2.Items.Clear();
        //        System.Data.DataTable dt_stk2 = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString());
        //        cboxUQGDTKTK_TK2.DataSource = dt_stk2;
        //        cboxUQGDTKTK_TK2.DisplayMember = "SOTK";
        //        cboxUQGDTKTK_TK2.Refresh();

        //        cboxUQGDTKTK_TK3.DataSource = null;
        //        cboxUQGDTKTK_TK3.Items.Clear();
        //        System.Data.DataTable dt_stk3 = tk_bus.TAI_KHOAN_THEO_MAKH(dgvDanhsachTK.CurrentRow.Cells["Mã KH"].Value.ToString());
        //        cboxUQGDTKTK_TK3.DataSource = dt_stk3;
        //        cboxUQGDTKTK_TK3.DisplayMember = "SOTK";
        //        cboxUQGDTKTK_TK3.Refresh();

        //    }
        //    else
        //    {
        //        MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới.");
        //    }
        //}

        //private void btnUQGDTKTK_BDUQ_Click(object sender, EventArgs e)
        //{
        //    if (dgvDanhsachTK.SelectedCells.Count > 0)
        //    {
        //        txtDUQGDTKTK_KH_HOTEN.Text = dgvDanhsachTK.CurrentRow.Cells["Tên KH"].Value.ToString();
        //        txtDUQGDTKTK_KH_NGAYSINH.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày sinh"].Value.ToString();
        //        txtDUQGDTKTK_KH_DTDD.Text = dgvDanhsachTK.CurrentRow.Cells["ĐT di động"].Value.ToString();
        //        txtDUQGDTKTK_KH_NGHENGHIEP.Text = dgvDanhsachTK.CurrentRow.Cells["Nghề nghiệp"].Value.ToString();
        //        txtDUQGDTKTK_KH_CMND.Text = dgvDanhsachTK.CurrentRow.Cells["CMND"].Value.ToString();
        //        txtDUQGDTKTK_KH_NGAYCAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp"].Value.ToString();
        //        txtDUQGDTKTK_KH_NOICAPCMND.Text = dgvDanhsachTK.CurrentRow.Cells["Nơi cấp"].Value.ToString();
        //        txtDUQGDTKTK_KH_DIACHI.Text = dgvDanhsachTK.CurrentRow.Cells["Địa chỉ"].Value.ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Bạn cần chọn khách hàng trong danh sách bên dưới để thực hiện chức năng này.");
        //    }
        //}

        //private void rdbUQGDTKTK_HIEU_LUC_TU_NGAY_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbUQGDTKTK_HIEU_LUC_TU_NGAY.Checked == true)
        //    {
        //        txtUQGDTKTK_HIEU_LUC_TU_NGAY.Enabled = true;
        //        txtUQGDTKTK_HIEU_LUC_DEN_NGAY.Enabled = true;
        //    }
        //    else
        //    {
        //        txtUQGDTKTK_HIEU_LUC_TU_NGAY.Enabled = false;
        //        txtUQGDTKTK_HIEU_LUC_TU_NGAY.Clear();
        //        txtUQGDTKTK_HIEU_LUC_DEN_NGAY.Enabled = false;
        //        txtUQGDTKTK_HIEU_LUC_DEN_NGAY.Clear();
        //    }
        //}

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXNSD_Them1_Click(object sender, EventArgs e)
        {
            if (txtXNSD_STK1.Text == "")
            {
                btnXNSD_Them1.Text = "Xóa";
                txtXNSD_STK1.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSD_Loaitien1.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Ngaygui1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày gửi"].Value.ToString();
                txtXNSD_Kyhan1.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                txtXNSD_Sodu1.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSD_Them1.Text = "Thêm";
                txtXNSD_STK1.Clear();
                txtXNSD_Soso1.Clear();
                txtXNSD_Loaitien1.Clear();
                txtXNSD_Ngaygui1.Clear();
                txtXNSD_Kyhan1.Clear();
                txtXNSD_Sodu1.Clear();
            }
            Tong_So_du();
        }

        private void btnXNSD_Them2_Click(object sender, EventArgs e)
        {
            if (txtXNSD_STK2.Text == "")
            {
                btnXNSD_Them2.Text = "Xóa";
                txtXNSD_STK2.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSD_Loaitien2.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Ngaygui2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày gửi"].Value.ToString();
                txtXNSD_Kyhan2.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                txtXNSD_Sodu2.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSD_Them2.Text = "Thêm";
                txtXNSD_STK2.Clear();
                txtXNSD_Soso2.Clear();
                txtXNSD_Loaitien2.Clear();
                txtXNSD_Ngaygui2.Clear();
                txtXNSD_Kyhan2.Clear();
                txtXNSD_Sodu2.Clear();
            }
            Tong_So_du();
        }

        private void btnXNSD_Them3_Click(object sender, EventArgs e)
        {
            if (txtXNSD_STK3.Text == "")
            {
                btnXNSD_Them3.Text = "Xóa";
                txtXNSD_STK3.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSD_Loaitien3.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Ngaygui3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày gửi"].Value.ToString();
                txtXNSD_Kyhan3.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                txtXNSD_Sodu3.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSD_Them3.Text = "Thêm";
                txtXNSD_STK3.Clear();
                txtXNSD_Soso3.Clear();
                txtXNSD_Loaitien3.Clear();
                txtXNSD_Ngaygui3.Clear();
                txtXNSD_Kyhan3.Clear();
                txtXNSD_Sodu3.Clear();
            }
            Tong_So_du();
        }

        private void btnXNSD_Them4_Click(object sender, EventArgs e)
        {
            if (txtXNSD_STK4.Text == "")
            {
                btnXNSD_Them4.Text = "Xóa";
                txtXNSD_STK4.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSD_Loaitien4.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Ngaygui4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày gửi"].Value.ToString();
                txtXNSD_Kyhan4.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                txtXNSD_Sodu4.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSD_Them4.Text = "Thêm";
                txtXNSD_STK4.Clear();
                txtXNSD_Soso4.Clear();
                txtXNSD_Loaitien4.Clear();
                txtXNSD_Ngaygui4.Clear();
                txtXNSD_Kyhan4.Clear();
                txtXNSD_Sodu4.Clear();
            }
            Tong_So_du();
        }

        private void btnXNSD_Them5_Click(object sender, EventArgs e)
        {
            if (txtXNSD_STK5.Text == "")
            {
                btnXNSD_Them5.Text = "Xóa";
                txtXNSD_STK5.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSD_Loaitien5.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSD_Ngaygui5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày gửi"].Value.ToString();
                txtXNSD_Kyhan5.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                txtXNSD_Sodu5.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSD_Them5.Text = "Thêm";
                txtXNSD_STK5.Clear();
                txtXNSD_Soso5.Clear();
                txtXNSD_Loaitien5.Clear();
                txtXNSD_Ngaygui5.Clear();
                txtXNSD_Kyhan5.Clear();
                txtXNSD_Sodu5.Clear();
            }
            Tong_So_du();
        }

        private void Tong_So_du()
        {
            Int64 sodu1 = 0;
            if (txtXNSD_Sodu1.Text != "")
            {
                sodu1 = Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu1.Text));
            }

            Int64 sodu2 = 0;
            if (txtXNSD_Sodu2.Text != "")
            {
                sodu2 = Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu2.Text));
            }

            Int64 sodu3 = 0;
            if (txtXNSD_Sodu3.Text != "")
            {
                sodu3 = Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu3.Text));
            }

            Int64 sodu4 = 0;
            if (txtXNSD_Sodu4.Text != "")
            {
                sodu4 = Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu4.Text));
            }

            Int64 sodu5 = 0;
            if (txtXNSD_Sodu5.Text != "")
            {
                sodu5 = Convert.ToInt64(ControlFormat.skipComma(txtXNSD_Sodu5.Text));
            }

            Int64 tong_so_du = sodu1 + sodu2 + sodu3 + sodu4 + sodu5;
            txtXNSD_Tongsodu.Text = tong_so_du.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture); 
        }

        private void Tong_So_du_TT()
        {
            Int64 sodu1 = 0;
            if (txtXNSDTT_Sodu1.Text != "")
            {
                sodu1 = Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu1.Text));
            }

            Int64 sodu2 = 0;
            if (txtXNSDTT_Sodu2.Text != "")
            {
                sodu2 = Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu2.Text));
            }

            Int64 sodu3 = 0;
            if (txtXNSDTT_Sodu3.Text != "")
            {
                sodu3 = Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu3.Text));
            }

            Int64 sodu4 = 0;
            if (txtXNSDTT_Sodu4.Text != "")
            {
                sodu4 = Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu4.Text));
            }

            Int64 sodu5 = 0;
            if (txtXNSDTT_Sodu5.Text != "")
            {
                sodu5 = Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Sodu5.Text));
            }

            Int64 tong_so_du_tt = sodu1 + sodu2 + sodu3 + sodu4 + sodu5;
            txtXNSDTT_Tongsodu.Text = tong_so_du_tt.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        }

        private void txtXNSD_Tongsodu_TextChanged(object sender, EventArgs e)
        {
            if (txtXNSD_Tongsodu.Text == "")
            {
                txtXNSD_Loaitien.Clear();
            }
        }

        private void txtXNSD_Soban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void btnSTen_Click(object sender, EventArgs e)
        {
            layDS_TenKH();
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

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            layDS_MaKH();
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

        private void btnSTel_Click(object sender, EventArgs e)
        {
            layDS_Dienthoai();
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

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            layDS_CMND();
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

        private void btnXNSDTT_Them1_Click(object sender, EventArgs e)
        {
            if (txtXNSDTT_STK1.Text == "")
            {
                btnXNSDTT_Them1.Text = "Xóa";
                txtXNSDTT_STK1.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSDTT_Loaitien1.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Ngaymo1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                txtXNSDTT_Sodu1.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSDTT_Them1.Text = "Thêm";
                txtXNSDTT_STK1.Clear();
                txtXNSDTT_Loaitien1.Clear();
                txtXNSDTT_Ngaymo1.Clear();
                txtXNSDTT_Sodu1.Clear();
            }
            Tong_So_du_TT();
        }

        private void txtXNSDTT_Tongsodu_TextChanged(object sender, EventArgs e)
        {
            if (txtXNSDTT_Tongsodu.Text == "")
            {
                txtXNSDTT_Loaitien.Clear();
            }
        }

        private void btnXNSDTT_Them2_Click(object sender, EventArgs e)
        {
            if (txtXNSDTT_STK2.Text == "")
            {
                btnXNSDTT_Them2.Text = "Xóa";
                txtXNSDTT_STK2.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSDTT_Loaitien2.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Ngaymo2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                txtXNSDTT_Sodu2.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSDTT_Them2.Text = "Thêm";
                txtXNSDTT_STK2.Clear();
                txtXNSDTT_Loaitien2.Clear();
                txtXNSDTT_Ngaymo2.Clear();
                txtXNSDTT_Sodu2.Clear();
            }
            Tong_So_du_TT();
        }

        private void btnXNSDTT_Them3_Click(object sender, EventArgs e)
        {
            if (txtXNSDTT_STK3.Text == "")
            {
                btnXNSDTT_Them3.Text = "Xóa";
                txtXNSDTT_STK3.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSDTT_Loaitien3.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Ngaymo3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                txtXNSDTT_Sodu3.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSDTT_Them3.Text = "Thêm";
                txtXNSDTT_STK3.Clear();
                txtXNSDTT_Loaitien3.Clear();
                txtXNSDTT_Ngaymo3.Clear();
                txtXNSDTT_Sodu3.Clear();
            }
            Tong_So_du_TT();
        }

        private void btnXNSDTT_Them4_Click(object sender, EventArgs e)
        {
            if (txtXNSDTT_STK4.Text == "")
            {
                btnXNSDTT_Them4.Text = "Xóa";
                txtXNSDTT_STK4.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSDTT_Loaitien4.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Ngaymo4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                txtXNSDTT_Sodu4.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSDTT_Them4.Text = "Thêm";
                txtXNSDTT_STK4.Clear();
                txtXNSDTT_Loaitien4.Clear();
                txtXNSDTT_Ngaymo4.Clear();
                txtXNSDTT_Sodu4.Clear();
            }
            Tong_So_du_TT();
        }

        private void btnXNSDTT_Them5_Click(object sender, EventArgs e)
        {
            if (txtXNSDTT_STK5.Text == "")
            {
                btnXNSDTT_Them5.Text = "Xóa";
                txtXNSDTT_STK5.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                txtXNSDTT_Loaitien5.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                txtXNSDTT_Ngaymo5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                txtXNSDTT_Sodu5.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư hiện tại"].Value.ToString();
            }
            else
            {
                btnXNSDTT_Them5.Text = "Thêm";
                txtXNSDTT_STK5.Clear();
                txtXNSDTT_Loaitien5.Clear();
                txtXNSDTT_Ngaymo5.Clear();
                txtXNSDTT_Sodu5.Clear();
            }
            Tong_So_du_TT();
        }

        private void btnNhapTKTT_Click(object sender, EventArgs e)
        {
            if (ofdNhapfileSTKTT.ShowDialog() == DialogResult.OK)
            {
                string import_file_path = ofdNhapfileSTKTT.FileName;
                System.Data.DataTable dt_temp = CommonMethod.read_excel(import_file_path);
                if (dt_temp.Rows.Count == 0 || dt_temp == null)
                {
                    MessageBox.Show("File không có dữ liệu");
                    return;
                }

                tctTT_Taikhoan.SelectedTab = tpXacnhansodutt;
                //string makh = Thongtindangnhap.macn + dt_temp.Rows[0][4].ToString();
                string makh = dt_temp.Rows[0][3].ToString().Substring(0, 4) + dt_temp.Rows[0][4].ToString();
                FILL_TAB_XNSDTT(makh);

                System.Data.DataTable dt_stk = new System.Data.DataTable();
                dt_stk.Columns.AddRange
                (
                    new DataColumn[4] 
                { 
                    new DataColumn("Số TK", typeof(string)),
                    new DataColumn("Loại tiền tệ", typeof(string)),
                    new DataColumn("Ngày mở", typeof(string)),
                    new DataColumn("Số dư hiện tại", typeof(string))
      
                }
                );
                DataRow dr_stk;

                System.Data.DataTable dt_taikhoan = new System.Data.DataTable();
                dt_taikhoan.Columns.AddRange
                (
                    new DataColumn[7] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOTK", typeof(string)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("NGAYMO", typeof(string)),
                    new DataColumn("NGAYDENHAN", typeof(string)),
                    new DataColumn("NGAYDONG", typeof(string)),
                    new DataColumn("HOATDONG", typeof(bool)),   
                }
                );
                DataRow dr_taikhoan;

                int iRows = dt_temp.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    //Chỉ chọn tài khoản tiết kiệm và đang hoạt động để gán vào datagridview
                    if (dt_temp.Rows[i][1].ToString() == "Demand Deposit" && dt_temp.Rows[i][2].ToString() == "Active")
                    {
                        dr_stk = dt_stk.NewRow();
                        dr_stk["Số TK"] = dt_temp.Rows[i][3].ToString();
                        dr_stk["Loại tiền tệ"] = dt_temp.Rows[i][6].ToString();
                        dr_stk["Ngày mở"] = dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);                       
                        dr_stk["Số dư hiện tại"] = Convert.ToInt64(dt_temp.Rows[i][7].ToString()).ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                        dt_stk.Rows.Add(dr_stk);
                    }

                    //Chọn các tài khoản đang hoạt động để nhập vào bảng TAIKHOAN
                    if (dt_temp.Rows[i][2].ToString() == "Active")
                    {
                        dr_taikhoan = dt_taikhoan.NewRow();
                        dr_taikhoan["MAKH"] = makh;
                        dr_taikhoan["SOTK"] = dt_temp.Rows[i][3].ToString();
                        dr_taikhoan["CCY"] = dt_temp.Rows[i][6].ToString();
                        dr_taikhoan["NGAYMO"] = dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);
                        if (dt_temp.Rows[i][1].ToString() == "Savings Deposit")
                        {
                            dr_taikhoan["NGAYDENHAN"] = dt_temp.Rows[i][11].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][11].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][11].ToString().Substring(6, 4);
                        }
                        else
                        {
                            dr_taikhoan["NGAYDENHAN"] = "01/01/1900";
                        }

                        dr_taikhoan["NGAYDONG"] = "01/01/1900";
                        dr_taikhoan["HOATDONG"] = true;
                        dt_taikhoan.Rows.Add(dr_taikhoan);
                    }
                }

                //Gán tabel dt_stk vào datagridview
                dgvDanhsachTK.DataSource = dt_stk;
                dgvDanhsachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].Width = 150;
                dgvDanhsachTK.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[1].Width = 136;
                dgvDanhsachTK.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[2].Width = 90;
                dgvDanhsachTK.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[3].Width = 150;
                Cursor.Current = Cursors.Default;

                //Cập nhật bảng TAIKHOAN từ table dt_taikhoan
                if (dt_taikhoan.Rows.Count > 0)
                {
                    bool update_tk = taikhoan_bus.UPDATE_TAIKHOAN_TUFILE(dt_taikhoan);
                }
                tctTT_Taikhoan.SelectedTab = tpXacnhansodutt;
                cboxMaubieu.Text = "Mẫu 05/VBAHD - Xác nhận số dư tài khoản thanh toán";
            }
        }

        private void txtXNSDTT_Soban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void tctTT_Taikhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tctTT_Taikhoan.SelectedTab == tpXacnhansodutk)
            {
                btnNhapTKTK.Enabled = true;
                btnNhapTKTT.Enabled = false;
                cboxMaubieu.Text = "Mẫu 04/VBAHD - Xác nhận số dư tiết kiệm";
            }
            else if (tctTT_Taikhoan.SelectedTab == tpXacnhansodutt)
            {
                btnNhapTKTK.Enabled = false;
                btnNhapTKTT.Enabled = true;
                cboxMaubieu.Text = "Mẫu 05/VBAHD - Xác nhận số dư tài khoản thanh toán";
            }
        }
    }
}