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
        THONGBAOSTKBUS tbtk_bus = new THONGBAOSTKBUS();

        private System.Data.DataTable dtResult = new System.Data.DataTable();
        private System.Data.DataTable dtDanhsach = new System.Data.DataTable();
        private System.Data.DataTable dtDanhsachDN = new System.Data.DataTable();

        private string chucvu_lanhdao = "";
             

        private static string makh = "";
        private static string donvicap = "";
        private static string chinhanhcap = "";

        private static string tbtk_kh_makh = "";
        private static string tbtk_kh_hoten = "";
        private static string tbtk_kh_cmnd = "";
        private static string tbtk_kh_ngaycapcmnd = "";
        private static string tbtk_kh_noicapcmnd = "";
        private static string tbtk_kh_diachi = "";

        private static string TBTK_NGAY_KH_BAO = "";
        private static string TBTK_NGAY_TIM_THAY = "";
        private static string TBTK_SO_TB_MAT_CN_LOAI2 = "";
        private static string TBTK_NGAY_BAO_MAT_CN_LOAI2 = "";
        private static string TBTK_SO_TB_THAY_CN_LOAI2 = "";
        private static string TBTK_NGAY_BAO_THAY_CN_LOAI2 = "";
        private static string TBTK_SO_TB_MAT_CN_LOAI1 = "";
        private static string TBTK_NGAY_BAO_MAT_CN_LOAI1 = "";
        private static string TBTK_SO_TB_THAY_CN_LOAI1 = "";
        private static string TBTK_NGAY_BAO_THAY_CN_LOAI1 = "";
        private static string TBTK_CNC = "";
        private static string TBTK_CN_THONG_BAO = "";
        private static string TBTK2_CNC = "";
        
        private static string TBTK_SO_TB_MAT = "";
        private static string TBTK_NGAY_TB_MAT = "";
        private static string TBTK_NGAY_TIM_THAY1 = "";
        private static string TBTK_NGAY_TIM_THAY2 = "";
        private static string TBTK_NGAY_TIM_THAY3 = "";
        private static string TBTK_NGAY_TIM_THAY4 = "";
        private static string TBTK_NGAY_TIM_THAY5 = "";

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

                txtMaCN.Text = Thongtindangnhap.macn;
            }

            txtMaCN.Text = Thongtindangnhap.macn;
            if (Thongtindangnhap.macn == Thongtindangnhap.ma_hoi_so)
            {
                txtMaCN.ReadOnly = false;
            }
            else
            {
                txtMaCN.ReadOnly = true;
            }

            //Khóa chức năng tìm kiếm sổ tiết kiệm báo mất
            txtTimNgaybaomat.Enabled = false;
            btnNgaybaomat.Enabled = false;

            txtTimMAKH.Enabled = false;
            btnTimMAKH.Enabled = false;

            txtTimSoso.Enabled = false;
            btnTimSoso.Enabled = false;

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

                //Replace So dien thoai
                nguon_TTKH.Add("<KH_DIENTHOAI>");
                dich_TTKH.Add(dt_kh.Rows[0]["DIENTHOAI1"].ToString());
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                return;
            }
        }


        //Điền thông tin vào tab Xác nhận số dư tài khoản thanh toán
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

        //Điền thông tin vào tab Thông báo mất/thấy số tiết kiệm
        internal void FILL_TAB_TBTK(string makh)
        {
            //Xóa thông tin sổ tiết kiệm cũ
            btnTBTK_Them1.Text = "Thêm";
            txtTBTK_STK1.Clear();
            txtTBTK_Soso1.Clear();
            txtTBTK_Ngaygui1.Clear();
            txtTBTK_DVC1.Clear();
            txtTBTK_CNC1.Clear();
            txtTBTK_Sodu1.Clear();
            txtTBTK_Ngaybaomat1.Clear();
            TBTK_NGAY_TIM_THAY1 = "";

            btnTBTK_Them2.Text = "Thêm";
            txtTBTK_STK2.Clear();
            txtTBTK_Soso2.Clear();
            txtTBTK_Ngaygui2.Clear();
            txtTBTK_DVC2.Clear();
            txtTBTK_CNC2.Clear();
            txtTBTK_Sodu2.Clear();
            txtTBTK_Ngaybaomat2.Clear();
            TBTK_NGAY_TIM_THAY2 = "";

            btnTBTK_Them3.Text = "Thêm";
            txtTBTK_STK3.Clear();
            txtTBTK_Soso3.Clear();
            txtTBTK_Ngaygui3.Clear();
            txtTBTK_DVC3.Clear();
            txtTBTK_CNC3.Clear();
            txtTBTK_Sodu3.Clear();
            txtTBTK_Ngaybaomat3.Clear();
            TBTK_NGAY_TIM_THAY3 = "";

            btnTBTK_Them4.Text = "Thêm";
            txtTBTK_STK4.Clear();
            txtTBTK_Soso4.Clear();
            txtTBTK_Ngaygui4.Clear();
            txtTBTK_DVC4.Clear();
            txtTBTK_CNC4.Clear();
            txtTBTK_Sodu4.Clear();
            txtTBTK_Ngaybaomat4.Clear();
            TBTK_NGAY_TIM_THAY4 = "";

            btnTBTK_Them5.Text = "Thêm";
            txtTBTK_STK5.Clear();
            txtTBTK_Soso5.Clear();
            txtTBTK_Ngaygui5.Clear();
            txtTBTK_DVC5.Clear();
            txtTBTK_CNC5.Clear();
            txtTBTK_Sodu5.Clear();
            txtTBTK_Ngaybaomat5.Clear();
            TBTK_NGAY_TIM_THAY5 = "";

            txtTBTK_So_tb_mat_cn1.Clear();
            txtTBTK_Ngay_tb_mat_cn1.Clear();
            txtTBTK_So_tb_thay_cn1.Clear();
            txtTBTK_Ngay_tb_thay_cn1.Clear();
            txtTBTK_So_tb_mat_cn2.Clear();
            txtTBTK_Ngay_tb_mat_cn2.Clear();
            txtTBTK_So_tb_thay_cn2.Clear();
            txtTBTK_Ngay_tb_thay_cn2.Clear();

            //Điền thông tin khách hàng mới
            System.Data.DataTable dt_kh = new System.Data.DataTable();
            dt_kh = khachhang_bus.KH_THEO_MAKH(makh);
            if (dt_kh.Rows.Count > 0)
            {
                tbtk_kh_makh = dt_kh.Rows[0]["MAKH"].ToString();
                tbtk_kh_hoten = dt_kh.Rows[0]["HOTEN"].ToString();
                tbtk_kh_cmnd = dt_kh.Rows[0]["CMND"].ToString();
                tbtk_kh_ngaycapcmnd = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                tbtk_kh_noicapcmnd = dt_kh.Rows[0]["NOICAP"].ToString();
                tbtk_kh_diachi = dt_kh.Rows[0]["DIACHI1"].ToString();

                txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                //txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                //txtTBTK_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                //txtTBTK_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                //txtTBTK_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                //txtTBTK_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();           
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                return;
            }
        }

        //Điền thông tin vào tab Thông báo hỏng số tiết kiệm
        internal void FILL_TAB_TBTK2(string makh)
        {
            //Xóa thông tin sổ tiết kiệm cũ
            btnTBTK2_Them1.Text = "Thêm";
            txtTBTK2_STK1.Clear();
            txtTBTK2_Soso1.Clear();
            txtTBTK2_Ngaygui1.Clear();
            txtTBTK2_DVC1.Clear();
            txtTBTK2_CNC1.Clear();
            txtTBTK2_Sodu1.Clear();
            txtTBTK2_Ngaybaohong1.Clear();

            btnTBTK2_Them2.Text = "Thêm";
            txtTBTK2_STK2.Clear();
            txtTBTK2_Soso2.Clear();
            txtTBTK2_Ngaygui2.Clear();
            txtTBTK2_DVC2.Clear();
            txtTBTK2_CNC2.Clear();
            txtTBTK2_Sodu2.Clear();
            txtTBTK2_Ngaybaohong2.Clear();

            btnTBTK2_Them3.Text = "Thêm";
            txtTBTK2_STK3.Clear();
            txtTBTK2_Soso3.Clear();
            txtTBTK2_Ngaygui3.Clear();
            txtTBTK2_DVC3.Clear();
            txtTBTK2_CNC3.Clear();
            txtTBTK2_Sodu3.Clear();
            txtTBTK2_Ngaybaohong3.Clear();

            btnTBTK2_Them4.Text = "Thêm";
            txtTBTK2_STK4.Clear();
            txtTBTK2_Soso4.Clear();
            txtTBTK2_Ngaygui4.Clear();
            txtTBTK2_DVC4.Clear();
            txtTBTK2_CNC4.Clear();
            txtTBTK2_Sodu4.Clear();
            txtTBTK2_Ngaybaohong4.Clear();

            btnTBTK2_Them5.Text = "Thêm";
            txtTBTK2_STK5.Clear();
            txtTBTK2_Soso5.Clear();
            txtTBTK2_Ngaygui5.Clear();
            txtTBTK2_DVC5.Clear();
            txtTBTK2_CNC5.Clear();
            txtTBTK2_Sodu5.Clear();
            txtTBTK2_Ngaybaohong5.Clear();

            //Điền thông tin khách hàng mới
            System.Data.DataTable dt_kh = new System.Data.DataTable();
            dt_kh = khachhang_bus.KH_THEO_MAKH(makh);
            if (dt_kh.Rows.Count > 0)
            {
                //tbtk_kh_makh = dt_kh.Rows[0]["MAKH"].ToString();
                //tbtk_kh_hoten = dt_kh.Rows[0]["HOTEN"].ToString();
                //tbtk_kh_cmnd = dt_kh.Rows[0]["CMND"].ToString();
                //tbtk_kh_ngaycapcmnd = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                //tbtk_kh_noicapcmnd = dt_kh.Rows[0]["NOICAP"].ToString();
                //tbtk_kh_diachi = dt_kh.Rows[0]["DIACHI1"].ToString();

                txtTBTK2_MaKH.Text = dt_kh.Rows[0]["MAKH"].ToString();
                txtTBTK2_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                txtTBTK2_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                txtTBTK2_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                txtTBTK2_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                txtTBTK2_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();           
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
                makh = dt_temp.Rows[0][3].ToString().Substring(0, 4) + dt_temp.Rows[0][4].ToString();

                //Lấy thông tin điểm giao dịch nơi cấp sổ tiết kiệmg
                if (makh.Substring(0, 4) == Thongtindangnhap.macn)
                {
                    if (Thongtindangnhap.hs == true)
                    {
                        donvicap = Thongtindangnhap.tencn;
                        chinhanhcap = Thongtindangnhap.tencn;
                    }
                    else
                    {
                        donvicap = Thongtindangnhap.tenpb;
                        chinhanhcap = Thongtindangnhap.tencn;
                    }
                }
                else
                {
                    string tencn_goc = "";
                    System.Data.DataTable dt_cn_goc = cn_bus.CHI_NHANH_THEO_MACN(makh.Substring(0, 4));
                    if (dt_cn_goc.Rows.Count > 0)
                    {
                        tencn_goc = dt_cn_goc.Rows[0]["TENCN"].ToString();
                    }
                    donvicap = tencn_goc;
                    chinhanhcap = tencn_goc;
                }
                
                System.Data.DataTable dt_stk = new System.Data.DataTable();
                dt_stk.Columns.AddRange
                (
                    new DataColumn[19] 
                { 
                    new DataColumn("Số TK", typeof(string)),
                    new DataColumn("Số sổ", typeof(string)),
                    new DataColumn("Loại tiền tệ", typeof(string)),
                    new DataColumn("Ngày cấp sổ", typeof(string)),
                    new DataColumn("Kỳ hạn", typeof(ushort)),
                    new DataColumn("Số dư", typeof(string)),
                    new DataColumn("Ngày báo mất-hỏng", typeof(string)),
                    new DataColumn("Đơn vị cấp", typeof(string)),
                    new DataColumn("Chi nhánh cấp", typeof(string)),
                    new DataColumn("Số thông báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày tìm thấy", typeof(string)),
                    new DataColumn("Hình thức xử lý", typeof(string))
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
                        dr_stk["Số sổ"] = "";
                        dr_stk["Loại tiền tệ"] = dt_temp.Rows[i][6].ToString();
                        dr_stk["Ngày cấp sổ"] = dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);
                        dr_stk["Kỳ hạn"] = Convert.ToUInt16(dt_temp.Rows[i][14].ToString());
                        dr_stk["Số dư"] = Convert.ToDecimal(dt_temp.Rows[i][7].ToString()).ToString("#,#.##", System.Globalization.CultureInfo.InvariantCulture);
                        dr_stk["Ngày báo mất-hỏng"] = DateTime.Now.ToString("dd") + "/" + CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("yyyy");
                        dr_stk["Đơn vị cấp"] = donvicap;
                        dr_stk["Chi nhánh cấp"] = chinhanhcap;
                        dr_stk["Số thông báo mất CN Loại 2"] = "";
                        dr_stk["Ngày báo mất CN Loại 2"] = "01/01/1900";
                        dr_stk["Số thông báo thấy CN Loại 2"] = "";
                        dr_stk["Ngày báo thấy CN Loại 2"] = "01/01/1900";
                        dr_stk["Số thông báo mất CN Loại 1"] = "";
                        dr_stk["Ngày báo mất CN Loại 1"] = "01/01/1900";
                        dr_stk["Số thông báo thấy CN Loại 1"] = "";
                        dr_stk["Ngày báo thấy CN Loại 1"] = "01/01/1900";
                        dr_stk["Ngày tìm thấy"] = "01/01/1900";
                        dr_stk["Hình thức xử lý"] = "";
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
                dgvDanhsachTK.DataSource = null;
                dgvDanhsachTK.DataSource = dt_stk;
                dgvDanhsachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số TK
                dgvDanhsachTK.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số sổ
                dgvDanhsachTK.Columns[1].Visible = false;
                dgvDanhsachTK.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Loại tiền tệ
                dgvDanhsachTK.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Ngày gửi
                dgvDanhsachTK.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Kỳ hạn
                dgvDanhsachTK.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số dư hiện tại
                dgvDanhsachTK.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất-hỏng
                dgvDanhsachTK.Columns[6].Visible = false;
                dgvDanhsachTK.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Đơn vị cấp
                dgvDanhsachTK.Columns[7].Visible = false;
                dgvDanhsachTK.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Chi nhánh cấp
                dgvDanhsachTK.Columns[8].Visible = false;
                dgvDanhsachTK.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 2
                dgvDanhsachTK.Columns[9].Visible = false;
                dgvDanhsachTK.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 2
                dgvDanhsachTK.Columns[10].Visible = false;
                dgvDanhsachTK.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 2
                dgvDanhsachTK.Columns[11].Visible = false;
                dgvDanhsachTK.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 2
                dgvDanhsachTK.Columns[12].Visible = false;
                dgvDanhsachTK.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 1
                dgvDanhsachTK.Columns[13].Visible = false;
                dgvDanhsachTK.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 1
                dgvDanhsachTK.Columns[14].Visible = false;
                dgvDanhsachTK.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 1
                dgvDanhsachTK.Columns[15].Visible = false;
                dgvDanhsachTK.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 1
                dgvDanhsachTK.Columns[16].Visible = false;
                dgvDanhsachTK.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày tìm thấy
                dgvDanhsachTK.Columns[17].Visible = false;
                dgvDanhsachTK.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Hình thức xử lý
                dgvDanhsachTK.Columns[18].Visible = false;
                Cursor.Current = Cursors.Default;

                //Cập nhật bảng TAIKHOAN từ table dt_taikhoan
                if (dt_taikhoan.Rows.Count > 0)
                {
                    bool update_tk = taikhoan_bus.UPDATE_TAIKHOAN_TUFILE(dt_taikhoan);
                }

                //Điền thông tin vào tab page Xác nhận số dư tiết kiệm
                FILL_TAB_XNSD(makh);
                //Điền thông tin vào tab page Thông báo mất/thấy sổ tiết kiệm
                FILL_TAB_TBTK(makh);
                //Điền thông tin vào tab page Thông báo hỏng sổ tiết kiệm
                FILL_TAB_TBTK2(makh);
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
        internal void TAO_MAU_BIEU_KE_TOAN_1(string file_mau_bieu, System.Data.DataTable data, List<string> list_title, string font_family, double font_size, bool last_row_bold, byte table_index, byte table_row_height, int merge_row_index = -1, int start_index = -1, int end_index = -1, string p_footer = "")
        {
            //Lấy đường dẫn file template
            string TemplateFileLocation = CommonMethod.TemplateFileLocation(file_mau_bieu);

            //Xác định đường dẫn file xuất ra từ chương trình
            string output_file_name = output_file_path + @"\" + Path.GetFileNameWithoutExtension(TemplateFileLocation) + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".docx";
            //MessageBox.Show(output_file_name);

            //Tạo mẫu biểu
            CommonMethod.CreateWordDocumentWithTable(TemplateFileLocation, output_file_name, this.nguon_TTKH, this.dich_TTKH, data, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);
            
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
                //Gán tên chi nhánh phần Kính gửi đối với giấy báo của khách hàng
                nguon_TTKH.Add("<CHI_NHANH_2>");
                dich_TTKH.Add(Thongtindangnhap.tencn); //Ví dụ: Agribank chi nhánh tỉnh Hải Dương

                //Gán tên chi nhánh phần Kính gửi đối với thông báo của ngân hàng
                //nguon_TTKH.Add("<CHI_NHANH_3>");
                //dich_TTKH.Add("Agribank chi nhánh loại I");
            }
            else
            {
                //Gán tên chi nhánh phần Kính gửi đối với giấy báo của khách hàng
                nguon_TTKH.Add("<CHI_NHANH_2>");
                dich_TTKH.Add(Thongtindangnhap.tencn + " - " + Thongtindangnhap.tenpb); //Ví dụ: Agribank chi nhánh huyện Ninh Giang - PGD Tân Quang

                //Gán tên chi nhánh phần Kính gửi đối với thông báo của ngân hàng
                //nguon_TTKH.Add("<CHI_NHANH_3>");
                //dich_TTKH.Add("Agribank chi nhánh tỉnh Hải Dương");
            }

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
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(Thongtindangnhap.tencn.Substring(9))); //Ví dụ: Chi nhánh tỉnh Hải Dương

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
                dich_TTKH.Add(CommonMethod.FirstCharToUpper(Thongtindangnhap.tencn.Substring(9)) + " - " + Thongtindangnhap.tenpb); //Ví dụ: Chi nhánh huyện Ninh Giang - PGD Tân Quang

                //EN
                nguon_TTKH.Add("<CHI_NHANH_EN>");
                dich_TTKH.Add(Thongtindangnhap.tencn_en + "-" + Thongtindangnhap.tenpb_en);

                nguon_TTKH.Add("<PHONG_GIAO_DICH_1>");
                dich_TTKH.Add("PGD/OFFICE " + Thongtindangnhap.tenpb.Substring(4).ToUpper());
            }

            nguon_TTKH.Add("<DC_CHI_NHANH>");
            dich_TTKH.Add(Thongtindangnhap.diachipb);


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
                nguon_TTKH.Add("<DIA_BAN_EN>");
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
            dich_TTKH.Add(DateTime.Now.ToString("dd") + "/" + CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("yyyy"));

            //EN
            nguon_TTKH.Add("<NTN_HIENTAI_EN>");
            dich_TTKH.Add(DateTime.Now.ToString("dd MMMM yyyy"));

            nguon_TTKH.Add("<NGAY_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("dd"));

            nguon_TTKH.Add("<THANG_HIENTAI>");
            dich_TTKH.Add(CommonMethod.Thang(DateTime.Now.ToString("MM")));

            nguon_TTKH.Add("<NAM_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("yyyy"));

            nguon_TTKH.Add("<GIO_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("HH"));

            nguon_TTKH.Add("<PHUT_HIENTAI>");
            dich_TTKH.Add(DateTime.Now.ToString("mm"));

            System.Data.DataTable dt_kh = new System.Data.DataTable();
            dt_kh = khachhang_bus.KH_THEO_MAKH(makh);
            if (dt_kh.Rows.Count > 0)
            {
                nguon_TTKH.Add("<KH_DIENTHOAI>");
                dich_TTKH.Add(dt_kh.Rows[0]["DIENTHOAI1"].ToString());
            }
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
            dich_TTKH.Add(DateTime.Now.ToString("HH") + " giờ " + DateTime.Now.ToString("mm") + " phút ngày " + DateTime.Now.ToString("dd") + "/" + CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("yyyy"));

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
            dich_TTKH.Add(DateTime.Now.ToString("HH") + " giờ " + DateTime.Now.ToString("mm") + " phút ngày " + DateTime.Now.ToString("dd") + "/" + CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("yyyy"));

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

        //Lấy thông tin cho các mẫu thông báo mất/thấy sổ tiết kiệm
        internal void LAY_TT_TBTK()
        {
            nguon_TTKH.Add("<TBTK_KH_HOTEN>");
            dich_TTKH.Add(tbtk_kh_hoten);

            nguon_TTKH.Add("<TBTK_KH_CMND>");
            dich_TTKH.Add(tbtk_kh_cmnd);

            nguon_TTKH.Add("<TBTK_KH_NGAYCAPCMND>");
            dich_TTKH.Add(tbtk_kh_ngaycapcmnd);

            nguon_TTKH.Add("<TBTK_KH_NOICAPCMND>");
            dich_TTKH.Add(tbtk_kh_noicapcmnd);

            nguon_TTKH.Add("<TBTK_KH_DIACHI>");
            dich_TTKH.Add(tbtk_kh_diachi);

            nguon_TTKH.Add("<TBTK_CNC>");
            dich_TTKH.Add(TBTK_CNC);

            if (chucvu_lanhdao == "Giám đốc")
            {
                nguon_TTKH.Add("<TBTK_CHUC_DANH1>");
                dich_TTKH.Add("GIÁM ĐỐC");

                nguon_TTKH.Add("<TBTK_CHUC_DANH2>");
                dich_TTKH.Add("");
            }
            else if (chucvu_lanhdao == "Phó Giám đốc")
            {
                nguon_TTKH.Add("<TBTK_CHUC_DANH1>");
                dich_TTKH.Add("KT. GIÁM ĐỐC");

                nguon_TTKH.Add("<TBTK_CHUC_DANH2>");
                dich_TTKH.Add("PHÓ GIÁM ĐỐC");
            }

            nguon_TTKH.Add("<TBTK_NGAY_KH_BAO>");
            dich_TTKH.Add(TBTK_NGAY_KH_BAO);

            nguon_TTKH.Add("<TBTK_NGAY_TIM_THAY>");
            dich_TTKH.Add(TBTK_NGAY_TIM_THAY);

            nguon_TTKH.Add("<TBTK_SO_TB_MAT_CN_LOAI2>");
            dich_TTKH.Add(TBTK_SO_TB_MAT_CN_LOAI2);

            nguon_TTKH.Add("<TBTK_NGAY_BAO_MAT_CN_LOAI2>");
            dich_TTKH.Add(TBTK_NGAY_BAO_MAT_CN_LOAI2);

            nguon_TTKH.Add("<TBTK_SO_TB_THAY_CN_LOAI2>");
            dich_TTKH.Add(TBTK_SO_TB_THAY_CN_LOAI2);

            nguon_TTKH.Add("<TBTK_NGAY_BAO_THAY_CN_LOAI2>");
            dich_TTKH.Add(TBTK_NGAY_BAO_THAY_CN_LOAI2);

            nguon_TTKH.Add("<TBTK_SO_TB_MAT_CN_LOAI1>");
            dich_TTKH.Add(TBTK_SO_TB_MAT_CN_LOAI1);

            nguon_TTKH.Add("<TBTK_NGAY_BAO_MAT_CN_LOAI1>");
            dich_TTKH.Add(TBTK_NGAY_BAO_MAT_CN_LOAI1);

            nguon_TTKH.Add("<TBTK_SO_TB_THAY_CN_LOAI1>");
            dich_TTKH.Add(TBTK_SO_TB_THAY_CN_LOAI1);

            nguon_TTKH.Add("<TBTK_NGAY_BAO_THAY_CN_LOAI1>");
            dich_TTKH.Add(TBTK_NGAY_BAO_THAY_CN_LOAI1);

            nguon_TTKH.Add("<TBTK_SO_TB_MAT>");
            dich_TTKH.Add(TBTK_SO_TB_MAT);

            nguon_TTKH.Add("<TBTK_NGAY_TB_MAT>");
            dich_TTKH.Add(TBTK_NGAY_TB_MAT);

            if (tbtk_kh_makh.Substring(0,4) == Thongtindangnhap.ma_hoi_so)
            {
                nguon_TTKH.Add("<TBTK_CAN_CU_BAO_MAT>");
                dich_TTKH.Add("Giấy báo mất sổ tiết kiệm của khách hàng lập ngày " + TBTK_NGAY_KH_BAO);
            }
            else
            {
                nguon_TTKH.Add("<TBTK_CAN_CU_BAO_MAT>");
                dich_TTKH.Add("Thông báo mất sổ tiết kiệm của " + TBTK_CN_THONG_BAO + " lập ngày " + TBTK_NGAY_BAO_MAT_CN_LOAI2);
            }

            if (tbtk_kh_makh.Substring(0, 4) == Thongtindangnhap.ma_hoi_so)
            {
                nguon_TTKH.Add("<TBTK_CAN_CU_BAO_THAY>");
                dich_TTKH.Add("Giấy báo tìm thấy sổ tiết kiệm đã báo mất do khách hàng lập ngày " + TBTK_NGAY_TIM_THAY);
            }
            else
            {
                nguon_TTKH.Add("<TBTK_CAN_CU_BAO_THAY>");
                dich_TTKH.Add("Thông báo về việc tìm thấy sổ tiết kiệm báo mất do" + TBTK_CN_THONG_BAO + " lập ngày " + TBTK_NGAY_BAO_THAY_CN_LOAI2);
            }
            
        }

        //Lấy thông tin cho các mẫu thông báo hỏng sổ tiết kiệm
        internal void LAY_TT_TBTK2()
        {
            nguon_TTKH.Add("<TBTK2_KH_HOTEN>");
            dich_TTKH.Add(txtTBTK2_KH_HOTEN.Text);

            nguon_TTKH.Add("<TBTK2_KH_CMND>");
            dich_TTKH.Add(txtTBTK2_KH_CMND.Text);

            nguon_TTKH.Add("<TBTK2_KH_NGAYCAPCMND>");
            dich_TTKH.Add(txtTBTK2_KH_NGAYCAPCMND.Text);

            nguon_TTKH.Add("<TBTK2_KH_NOICAPCMND>");
            dich_TTKH.Add(txtTBTK2_KH_NOICAPCMND.Text);

            nguon_TTKH.Add("<TBTK2_KH_DIACHI>");
            dich_TTKH.Add(txtTBTK2_KH_DIACHI.Text);

            nguon_TTKH.Add("<TBTK2_CNC>");
            dich_TTKH.Add(TBTK2_CNC);

            if (chucvu_lanhdao == "Giám đốc")
            {
                nguon_TTKH.Add("<TBTK2_CHUC_DANH1>");
                dich_TTKH.Add("GIÁM ĐỐC");

                nguon_TTKH.Add("<TBTK2_CHUC_DANH2>");
                dich_TTKH.Add("");
            }
            else if (chucvu_lanhdao == "Phó Giám đốc")
            {
                nguon_TTKH.Add("<TBTK2_CHUC_DANH1>");
                dich_TTKH.Add("KT. GIÁM ĐỐC");

                nguon_TTKH.Add("<TBTK2_CHUC_DANH2>");
                dich_TTKH.Add("PHÓ GIÁM ĐỐC");
            }

            nguon_TTKH.Add("<TBTK2_XU_LY>");
            dich_TTKH.Add(txtTBTK2_XU_LY.Text);
        }

        private void btnTaomaubieu_Click(object sender, EventArgs e)
        {
            //TBTK_NGAY_KH_BAO = txtTBTK_Ngaybaomat1.Text;
            //TBTK_NGAY_TIM_THAY = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
            TBTK_SO_TB_MAT_CN_LOAI2 = txtTBTK_So_tb_mat_cn2.Text;
            TBTK_NGAY_BAO_MAT_CN_LOAI2 = txtTBTK_Ngay_tb_mat_cn2.Text;
            TBTK_SO_TB_THAY_CN_LOAI2 = txtTBTK_So_tb_thay_cn2.Text;
            TBTK_NGAY_BAO_THAY_CN_LOAI2 = txtTBTK_Ngay_tb_thay_cn2.Text;
            TBTK_SO_TB_MAT_CN_LOAI1 = txtTBTK_So_tb_mat_cn1.Text;
            TBTK_NGAY_BAO_MAT_CN_LOAI1 = txtTBTK_Ngay_tb_mat_cn1.Text;
            TBTK_SO_TB_THAY_CN_LOAI1 = txtTBTK_So_tb_thay_cn1.Text;
            TBTK_NGAY_BAO_THAY_CN_LOAI1 = txtTBTK_Ngay_tb_thay_cn1.Text;
            if (txtTBTK_So_tb_mat_cn2.Text == "")
            {
                TBTK_SO_TB_MAT = txtTBTK_So_tb_mat_cn1.Text;
            }
            else
            {
                TBTK_SO_TB_MAT = txtTBTK_So_tb_mat_cn2.Text;
            }
            if (txtTBTK_Ngay_tb_mat_cn2.Text == "")
            {
                TBTK_NGAY_TB_MAT = txtTBTK_Ngay_tb_mat_cn1.Text;
            }
            else
            {
                TBTK_NGAY_TB_MAT = txtTBTK_Ngay_tb_mat_cn2.Text;
            }
            //TBTK_CNC = txtTBTK_CNC5.Text;
            //TBTK_CN_THONG_BAO = txtTBTK_CNC5.Text;
            
            string file_mau_bieu = cboxMaubieu.SelectedValue.ToString();
            if (cboxMaubieu.Text.Contains(@"04/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpXacnhansodutk;
                if (txtXNSD_MaKH.Text == "")
                {
                    MessageBox.Show("Chưa có dữ liệu khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtXNSD_MaKH.Focus();
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

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtXNSD_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtXNSD_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtXNSD_Ngaygui1.Focus();
                    return;
                }

                if (txtXNSD_Ngaygui2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtXNSD_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtXNSD_Ngaygui2.Focus();
                    return;
                }

                if (txtXNSD_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtXNSD_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtXNSD_Ngaygui3.Focus();
                    return;
                }

                if (txtXNSD_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtXNSD_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtXNSD_Ngaygui4.Focus();
                    return;
                }

                if (txtXNSD_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtXNSD_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtXNSD_Ngaygui5.Focus();
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
                double font_size = 12;
                bool last_row_bold = true;
                byte table_index = 1;
                byte table_row_height = 40;
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

                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);         
            }
            else  if (cboxMaubieu.Text.Contains(@"05/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpXacnhansodutt;

                if (txtXNSDTT_MaKH.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập dữ liệu khách hàng!");
                    txtXNSDTT_MaKH.Focus();
                    return;
                }

                if (txtXNSDTT_KH_DIACHI_EN.Text == "")
                {
                    MessageBox.Show("Chưa nhập địa chỉ khách hàng bằng tiếng Anh. Đề nghị kiểm tra lại!");
                    txtXNSDTT_KH_DIACHI_EN.Focus();
                    return;
                }

                if (Convert.ToByte(txtXNSDTT_Soban.Text) < 3)
                {
                    MessageBox.Show("Số bản tối thiểu là 3. Đề nghị kiểm tra lại!");
                    txtXNSDTT_Soban.Focus();
                    return;
                }

                if (Convert.ToInt64(ControlFormat.skipComma(txtXNSDTT_Tongsodu.Text)) < 0 )
                {
                    MessageBox.Show("Chương trình không hỗ trợ xác nhận số dư âm. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtXNSDTT_STK1.Text == "" && txtXNSDTT_STK2.Text == "" && txtXNSDTT_STK3.Text == "" && txtXNSDTT_STK4.Text == "" && txtXNSDTT_STK5.Text == "")
                {
                    MessageBox.Show("Chưa có tài khoản thanh toán nào được chọn để xác nhận số dư. Đề nghị kiểm tra lại!");
                    txtXNSDTT_STK1.Focus();
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
                double font_size = 12;
                bool last_row_bold = true;
                byte table_index = 1;
                byte table_row_height = 40;
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

                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);
            }
            else if (cboxMaubieu.Text.Contains(@"06/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpThongbaoTK;

                if (txtTBTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtTBTK_KH_HOTEN.Focus();
                    return;
                }

                if (txtTBTK_STK1.Text == "" && txtTBTK_STK2.Text == "" && txtTBTK_STK3.Text == "" && txtTBTK_STK4.Text == "" && txtTBTK_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtTBTK_STK1.Text != "" && txtTBTK_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso1.Focus();
                    return;
                }

                if (txtTBTK_STK2.Text != "" && txtTBTK_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso2.Focus();
                    return;
                }

                if (txtTBTK_STK3.Text != "" && txtTBTK_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso3.Focus();
                    return;
                }

                if (txtTBTK_STK4.Text != "" && txtTBTK_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso4.Focus();
                    return;
                }

                if (txtTBTK_STK5.Text != "" && txtTBTK_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso5.Focus();
                    return;
                }

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtTBTK_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui1.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui2.Text != "" &&  !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui2.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui3.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui4.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui5.Focus();
                    return;
                }

                //Kiểm tra chi nhánh loại 1 đã tạo thông báo mất chưa
                if (TBTK_SO_TB_MAT_CN_LOAI1 != "")
                {
                    MessageBox.Show("Không thể tạo lại báo mất cho sổ này.");
                    return;
                }

                //Kiểm tra khách hàng đã báo thấy chưa
                if (TBTK_NGAY_TIM_THAY != "01/01/1900")
                {
                    MessageBox.Show("Không thể tạo lại báo mất cho sổ này. Khách hàng đã báo thấy.");
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin sổ tiết kiệm
                LAY_TT_TBTK();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT");
                list_title.Add("Agribank nơi mở thẻ TK");
                list_title.Add("Số seri Thẻ TK");
                list_title.Add("Ngày cấp thẻ TK");
                list_title.Add("Số dư khi mở Thẻ tiết kiệm");
                list_title.Add("Ghi chú");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[6] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("DVC", typeof(string)),
                        //new DataColumn("CNC", typeof(string)),
                        new DataColumn("SO_SERIAL", typeof(string)),
                        new DataColumn("NGAY_CAP", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                        new DataColumn("GHI_CHU", typeof(string))
                    }
                );
                DataRow stk_dr;
                int i = 1;

                if (txtTBTK_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC1.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso1.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC2.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC2.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso2.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC3.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC3.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso3.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC4.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC4.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso4.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC5.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC5.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso5.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                string font_family = "Times New Roman";
                double font_size = 12;
                bool last_row_bold = false;
                byte table_index = 1;
                byte table_row_height = 60;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                //Lấy file mẫu biểu tương ứng với chi nhánh
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);

                //Cập nhật sổ tiết kiệm báo mất trong bảng THONGBAOSTK
                CAP_NHAT_STK_MAT_KH();
            }
            else if (cboxMaubieu.Text.Contains(@"07/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpThongbaoTK;

                if (txtTBTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtTBTK_KH_HOTEN.Focus();
                    return;
                }

                if (txtTBTK_STK1.Text == "" && txtTBTK_STK2.Text == "" && txtTBTK_STK3.Text == "" && txtTBTK_STK4.Text == "" && txtTBTK_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtTBTK_STK1.Text != "" && txtTBTK_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso1.Focus();
                    return;
                }

                if (txtTBTK_STK2.Text != "" && txtTBTK_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso2.Focus();
                    return;
                }

                if (txtTBTK_STK3.Text != "" && txtTBTK_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso3.Focus();
                    return;
                }

                if (txtTBTK_STK4.Text != "" && txtTBTK_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso4.Focus();
                    return;
                }

                if (txtTBTK_STK5.Text != "" && txtTBTK_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso5.Focus();
                    return;
                }

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtTBTK_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui1.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui2.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui3.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui4.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui5.Focus();
                    return;
                }

                //Kiểm tra đã có số thông báo của chi nhánh loại 2 chưa
                if (txtTBTK_So_tb_mat_cn2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số thông báo mất. Đề nghị kiểm tra lại!");
                    txtTBTK_So_tb_mat_cn2.Focus();
                    return;
                }

                //Kiểm tra chi nhánh loại 1 đã tạo thông báo mất chưa
                if (TBTK_SO_TB_MAT_CN_LOAI1 != "")
                {
                    MessageBox.Show("Không thể tạo lại báo mất cho sổ này.");
                    return;
                }

                //Kiểm tra khách hàng đã báo thấy chưa
                if (TBTK_NGAY_TIM_THAY != "01/01/1900")
                {
                    MessageBox.Show("Không thể tạo lại báo mất cho sổ này. Khách hàng đã báo thấy.");
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin sổ tiết kiệm
                LAY_TT_TBTK();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT");
                list_title.Add("Agribank nơi mở thẻ TK");
                list_title.Add("Số tài khoản");
                list_title.Add("Số seri Thẻ TK");
                list_title.Add("Ngày cấp thẻ TK");
                list_title.Add("Số dư khi mở Thẻ tiết kiệm");
                list_title.Add("Ghi chú");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[7] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("DVC", typeof(string)),
                        new DataColumn("STK", typeof(string)),
                        new DataColumn("SO_SERIAL", typeof(string)),
                        new DataColumn("NGAY_CAP", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                        new DataColumn("GHI_CHU", typeof(string))
                    }
                );
                DataRow stk_dr;
                int i = 0;

                if (txtTBTK_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC1.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso1.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC2.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso2.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC3.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso3.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC4.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso4.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC5.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso5.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                string font_family = "Times New Roman";
                double font_size = 12;
                bool last_row_bold = false;
                byte table_index = 1;
                byte table_row_height = 60;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                //Lấy file mẫu biểu tương ứng với chi nhánh
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);

                //Cập nhật sổ tiết kiệm báo mất trong bảng THONGBAOSTK
                CAP_NHAT_STK_MAT_CN2();
            }
            else if (cboxMaubieu.Text.Contains(@"08/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpThongbaoTK;

                if (txtTBTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtTBTK_KH_HOTEN.Focus();
                    return;
                }

                if (txtTBTK_STK1.Text == "" && txtTBTK_STK2.Text == "" && txtTBTK_STK3.Text == "" && txtTBTK_STK4.Text == "" && txtTBTK_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtTBTK_STK1.Text != "" && txtTBTK_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso1.Focus();
                    return;
                }

                if (txtTBTK_STK2.Text != "" && txtTBTK_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso2.Focus();
                    return;
                }

                if (txtTBTK_STK3.Text != "" && txtTBTK_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso3.Focus();
                    return;
                }

                if (txtTBTK_STK4.Text != "" && txtTBTK_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso4.Focus();
                    return;
                }

                if (txtTBTK_STK5.Text != "" && txtTBTK_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso5.Focus();
                    return;
                }

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtTBTK_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui1.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui2.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui3.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui4.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui5.Focus();
                    return;
                }

                if (tbtk_kh_makh.Substring(0,4) != Thongtindangnhap.ma_hoi_so)
                {
                    //Kiểm tra đã có số thông báo mất của chi nhánh loại 2 chưa
                    if (txtTBTK_So_tb_mat_cn2.Text == "")
                    {
                        MessageBox.Show("Chưa nhập số thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                        txtTBTK_So_tb_mat_cn2.Focus();
                        return;
                    }
                    //Kiểm tra đã có ngày thông báo mất của chi nhánh loại 2 chưa
                    if (txtTBTK_Ngay_tb_mat_cn2.Text == "")
                    {
                        MessageBox.Show("Chưa nhập ngày thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                        txtTBTK_Ngay_tb_mat_cn2.Focus();
                        return;
                    }
                    else if (!CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn2.Text))
                    {
                        MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                        txtTBTK_Ngay_tb_mat_cn2.Focus();
                        return;
                    }
                }
                
                //Kiểm tra đã có số thông báo mất của chi nhánh loại 1 chưa
                if (txtTBTK_So_tb_mat_cn1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số thông báo mất của chi nhánh loại 1. Đề nghị kiểm tra lại!");
                    txtTBTK_So_tb_mat_cn1.Focus();
                    return;
                }

                //Kiểm tra khách hàng đã báo thấy chưa
                if (TBTK_NGAY_TIM_THAY != "01/01/1900")
                {
                    MessageBox.Show("Không thể tạo lại báo mất cho sổ này. Khách hàng đã báo thấy.");
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin sổ tiết kiệm
                LAY_TT_TBTK();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT");
                list_title.Add("Đơn vị giao dịch nơi cấp sổ TK");
                list_title.Add("Số tài khoản");
                list_title.Add("Số seri Thẻ TK");
                list_title.Add("Ngày cấp thẻ TK");
                list_title.Add("Số dư khi mở Thẻ tiết kiệm");
                list_title.Add("Ghi chú");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[7] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("DVC", typeof(string)),
                        new DataColumn("STK", typeof(string)),
                        new DataColumn("SO_SERIAL", typeof(string)),
                        new DataColumn("NGAY_CAP", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                        new DataColumn("GHI_CHU", typeof(string))
                    }
                );
                DataRow stk_dr;
                int i = 0;

                if (txtTBTK_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC1.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso1.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC2.Text;
                    stk_dr["STK"] = txtTBTK_STK2.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso2.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC3.Text;
                    stk_dr["STK"] = txtTBTK_STK3.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso3.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC4.Text;
                    stk_dr["STK"] = txtTBTK_STK4.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso4.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC5.Text;
                    stk_dr["STK"] = txtTBTK_STK5.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso5.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                string font_family = "Times New Roman";
                double font_size = 12;
                bool last_row_bold = false;
                byte table_index = 1;
                byte table_row_height = 60;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                //Lấy file mẫu biểu tương ứng với chi nhánh
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);

                //Cập nhật sổ tiết kiệm báo mất trong bảng THONGBAOSTK
                CAP_NHAT_STK_MAT_CN1();
            }
            else if (cboxMaubieu.Text.Contains(@"09/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpThongbaoTK;

                if (txtTBTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtTBTK_KH_HOTEN.Focus();
                    return;
                }

                if (txtTBTK_STK1.Text == "" && txtTBTK_STK2.Text == "" && txtTBTK_STK3.Text == "" && txtTBTK_STK4.Text == "" && txtTBTK_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtTBTK_STK1.Text != "" && txtTBTK_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso1.Focus();
                    return;
                }

                if (txtTBTK_STK2.Text != "" && txtTBTK_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso2.Focus();
                    return;
                }

                if (txtTBTK_STK3.Text != "" && txtTBTK_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso3.Focus();
                    return;
                }

                if (txtTBTK_STK4.Text != "" && txtTBTK_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso4.Focus();
                    return;
                }

                if (txtTBTK_STK5.Text != "" && txtTBTK_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso5.Focus();
                    return;
                }

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtTBTK_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui1.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui2.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui3.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui4.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui5.Focus();
                    return;
                }

                if (txtTBTK_Ngay_tb_mat_cn2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn2.Focus();
                    return;
                }

                if (txtTBTK_Ngay_tb_mat_cn1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 1 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn1.Focus();
                    return;
                }

                if (tbtk_kh_makh.Substring(0, 4) != Thongtindangnhap.ma_hoi_so)
                {
                    //Kiểm tra đã có số thông báo mất của chi nhánh loại 2 chưa (nếu khách hàng thuộc chi nhánh loại 2)
                    if (txtTBTK_So_tb_mat_cn2.Text == "")
                    {
                        MessageBox.Show("Chưa nhập số thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                        txtTBTK_So_tb_mat_cn2.Focus();
                        return;
                    }
                    //Kiểm tra đã có ngày thông báo mất của chi nhánh loại 2 chưa
                    if (txtTBTK_Ngay_tb_mat_cn2.Text == "")
                    {
                        MessageBox.Show("Chưa nhập ngày thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                        txtTBTK_Ngay_tb_mat_cn2.Focus();
                        return;
                    }
                    else if (!CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn2.Text))
                    {
                        MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                        txtTBTK_Ngay_tb_mat_cn2.Focus();
                        return;
                    }
                }

                //Kiểm tra đã có số thông báo mất của chi nhánh loại 1 chưa
                if (txtTBTK_So_tb_mat_cn1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số thông báo mất của chi nhánh loại 1. Đề nghị kiểm tra lại!");
                    txtTBTK_So_tb_mat_cn1.Focus();
                    return;
                }
                //Kiểm tra đã có ngày thông báo mất của chi nhánh loại 1 chưa
                if (txtTBTK_Ngay_tb_mat_cn1.Text == "")
                {
                    MessageBox.Show("Chưa nhập ngày thông báo mất của chi nhánh loại 1. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn1.Focus();
                    return;
                }
                else if (!CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 1 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn1.Focus();
                    return;
                }

                //Kiểm tra chi nhánh loại 1 đã tạo báo thấy chưa
                if (TBTK_SO_TB_THAY_CN_LOAI1 != "")
                {
                    MessageBox.Show("Không thể tạo lại báo thấy cho sổ này.");
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin sổ tiết kiệm
                LAY_TT_TBTK();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT");
                list_title.Add("Agribank nơi mở thẻ TK");
                list_title.Add("Số seri Thẻ TK");
                list_title.Add("Ngày cấp thẻ TK");
                list_title.Add("Số dư khi mở Thẻ tiết kiệm");
                list_title.Add("Ghi chú");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[6] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("DVC", typeof(string)),
                        //new DataColumn("CNC", typeof(string)),
                        new DataColumn("SO_SERIAL", typeof(string)),
                        new DataColumn("NGAY_CAP", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                        new DataColumn("GHI_CHU", typeof(string))
                    }
                );
                DataRow stk_dr;
                int i = 0;

                if (txtTBTK_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC1.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso1.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC2.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC2.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso2.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC3.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC3.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso3.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC4.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC4.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso4.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC5.Text;
                    //stk_dr["CNC"] = txtTBTK_CNC5.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso5.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                string font_family = "Times New Roman";
                double font_size = 12;
                bool last_row_bold = false;
                byte table_index = 0;
                byte table_row_height = 60;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                //Lấy file mẫu biểu tương ứng với chi nhánh
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);

                //Cập nhật sổ tiết kiệm báo mất trong bảng THONGBAOSTK
                CAP_NHAT_STK_THAY_KH();
            }
            else if (cboxMaubieu.Text.Contains(@"10/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpThongbaoTK;

                if (txtTBTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtTBTK_KH_HOTEN.Focus();
                    return;
                }

                if (txtTBTK_STK1.Text == "" && txtTBTK_STK2.Text == "" && txtTBTK_STK3.Text == "" && txtTBTK_STK4.Text == "" && txtTBTK_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtTBTK_STK1.Text != "" && txtTBTK_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso1.Focus();
                    return;
                }

                if (txtTBTK_STK2.Text != "" && txtTBTK_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso2.Focus();
                    return;
                }

                if (txtTBTK_STK3.Text != "" && txtTBTK_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso3.Focus();
                    return;
                }

                if (txtTBTK_STK4.Text != "" && txtTBTK_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso4.Focus();
                    return;
                }

                if (txtTBTK_STK5.Text != "" && txtTBTK_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso5.Focus();
                    return;
                }

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtTBTK_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui1.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui2.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui3.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui4.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui5.Focus();
                    return;
                }

                if (txtTBTK_Ngay_tb_mat_cn2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn2.Focus();
                    return;
                }

                if (txtTBTK_Ngay_tb_mat_cn1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 1 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn1.Focus();
                    return;
                }

                //Kiểm tra đã có số thông báo mất của chi nhánh loại 2 chưa
                if (txtTBTK_So_tb_mat_cn2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                    txtTBTK_So_tb_mat_cn2.Focus();
                    return;
                }
                //Kiểm tra đã có ngày thông báo mất của chi nhánh loại 2 chưa
                if (txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    MessageBox.Show("Chưa nhập ngày thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn2.Focus();
                    return;
                }
                else if (!CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn2.Focus();
                    return;
                }

                //Kiểm tra đã nhập số thông báo thấy của chi nhánh loại 2 chưa
                if (txtTBTK_So_tb_thay_cn2.Text == "")
                {
                    MessageBox.Show("Chưa nhập ngày thông báo thấy của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                    txtTBTK_So_tb_thay_cn2.Focus();
                    return;
                }

                //Kiểm tra chi nhánh loại 1 đã tạo báo thấy chưa
                if (TBTK_SO_TB_THAY_CN_LOAI1 != "")
                {
                    MessageBox.Show("Không thể tạo lại báo thấy cho sổ này.");
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin sổ tiết kiệm
                LAY_TT_TBTK();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT");
                list_title.Add("Agribank nơi mở thẻ TK");
                list_title.Add("Số tài khoản");
                list_title.Add("Số seri Thẻ TK");
                list_title.Add("Ngày cấp thẻ TK");
                list_title.Add("Số dư khi mở Thẻ tiết kiệm");
                list_title.Add("Ghi chú");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[7] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("DVC", typeof(string)),
                        new DataColumn("STK", typeof(string)),
                        new DataColumn("SO_SERIAL", typeof(string)),
                        new DataColumn("NGAY_CAP", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                        new DataColumn("GHI_CHU", typeof(string))
                    }
                );
                DataRow stk_dr;
                int i = 0;

                if (txtTBTK_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC1.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso1.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC2.Text;
                    stk_dr["STK"] = txtTBTK_STK2.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso2.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC3.Text;
                    stk_dr["STK"] = txtTBTK_STK3.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso3.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC4.Text;
                    stk_dr["STK"] = txtTBTK_STK4.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso4.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC5.Text;
                    stk_dr["STK"] = txtTBTK_STK5.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso5.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                string font_family = "Times New Roman";
                double font_size = 12;
                bool last_row_bold = false;
                byte table_index = 1;
                byte table_row_height = 60;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                //Lấy file mẫu biểu tương ứng với chi nhánh
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);

                //Cập nhật sổ tiết kiệm báo mất trong bảng THONGBAOSTK
                CAP_NHAT_STK_THAY_CN2();
            }
            else if (cboxMaubieu.Text.Contains(@"11/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpThongbaoTK;

                if (txtTBTK_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtTBTK_KH_HOTEN.Focus();
                    return;
                }

                if (txtTBTK_STK1.Text == "" && txtTBTK_STK2.Text == "" && txtTBTK_STK3.Text == "" && txtTBTK_STK4.Text == "" && txtTBTK_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtTBTK_STK1.Text != "" && txtTBTK_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso1.Focus();
                    return;
                }

                if (txtTBTK_STK2.Text != "" && txtTBTK_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso2.Focus();
                    return;
                }

                if (txtTBTK_STK3.Text != "" && txtTBTK_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso3.Focus();
                    return;
                }

                if (txtTBTK_STK4.Text != "" && txtTBTK_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso4.Focus();
                    return;
                }

                if (txtTBTK_STK5.Text != "" && txtTBTK_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK_Soso5.Focus();
                    return;
                }

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtTBTK_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui1.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui2.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui3.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui4.Focus();
                    return;
                }

                if (txtTBTK_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK_Ngaygui5.Focus();
                    return;
                }

                if (txtTBTK_Ngay_tb_mat_cn2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn2.Focus();
                    return;
                }

                if (txtTBTK_Ngay_tb_mat_cn1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 1 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn1.Focus();
                    return;
                }

                
                if (tbtk_kh_makh.Substring(0,4) != Thongtindangnhap.ma_hoi_so)
                {
                    //Kiểm tra đã có số thông báo mất của chi nhánh loại 2 chưa
                    //if (txtTBTK_So_tb_mat_cn2.Text == "")
                    //{
                    //    MessageBox.Show("Chưa nhập số thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                    //    txtTBTK_So_tb_mat_cn2.Focus();
                    //    return;
                    //}
                    ////Kiểm tra đã có ngày thông báo mất của chi nhánh loại 2 chưa
                    //if (txtTBTK_Ngay_tb_mat_cn2.Text == "")
                    //{
                    //    MessageBox.Show("Chưa nhập ngày thông báo mất của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                    //    txtTBTK_Ngay_tb_mat_cn2.Focus();
                    //    return;
                    //}
                    //else if (!CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn2.Text))
                    //{
                    //    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                    //    txtTBTK_Ngay_tb_mat_cn2.Focus();
                    //    return;
                    //}

                    //Kiểm tra đã nhập số thông báo thấy của chi nhánh loại 2 chưa
                    if (txtTBTK_So_tb_thay_cn2.Text == "")
                    {
                        MessageBox.Show("Chưa nhập ngày thông báo thấy của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                        txtTBTK_So_tb_thay_cn2.Focus();
                        return;
                    }
                    //Kiểm tra đã có ngày thông báo thấy của chi nhánh loại 2 chưa
                    if (txtTBTK_Ngay_tb_thay_cn2.Text == "")
                    {
                        MessageBox.Show("Chưa nhập ngày thông báo thấy của chi nhánh loại 2. Đề nghị kiểm tra lại!");
                        txtTBTK_Ngay_tb_thay_cn2.Focus();
                        return;
                    }
                    else if (!CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_thay_cn2.Text))
                    {
                        MessageBox.Show("Dữ liệu ngày báo thấy của chi nhánh loại 2 chưa đúng. Đề nghị kiểm tra lại!");
                        txtTBTK_Ngay_tb_thay_cn2.Focus();
                        return;
                    }
                }
                //Kiểm tra đã nhập số thông báo mất của chi nhánh loại 1 chưa
                if (txtTBTK_So_tb_mat_cn1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số thông báo mất của chi nhánh loại 1. Đề nghị kiểm tra lại!");
                    txtTBTK_So_tb_mat_cn1.Focus();
                    return;
                }
                //Kiểm tra đã nhập ngày thông báo mất của chi nhánh loại 1 chưa
                if (txtTBTK_Ngay_tb_mat_cn1.Text == "")
                {
                    MessageBox.Show("Chưa nhập ngày thông báo mất của chi nhánh loại 1. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn1.Focus();
                    return;
                }
                else if (!CommonMethod.KiemTraNhapNgay(txtTBTK_Ngay_tb_mat_cn1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày báo mất của chi nhánh loại 1 chưa đúng. Đề nghị kiểm tra lại!");
                    txtTBTK_Ngay_tb_mat_cn1.Focus();
                    return;
                }

                //Kiểm tra đã nhập số thông báo thấy của chi nhánh loại 1 chưa
                if (txtTBTK_So_tb_thay_cn1.Text == "")
                {
                    MessageBox.Show("Chưa nhập ngày thông báo thấy của chi nhánh loại 1. Đề nghị kiểm tra lại");
                    txtTBTK_So_tb_thay_cn1.Focus();
                    return;
                }
                

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin sổ tiết kiệm
                LAY_TT_TBTK();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT");
                list_title.Add("Agribank nơi mở thẻ TK");
                list_title.Add("Số tài khoản");
                list_title.Add("Số seri Thẻ TK");
                list_title.Add("Ngày cấp thẻ TK");
                list_title.Add("Số dư khi mở Thẻ tiết kiệm");
                list_title.Add("Ghi chú");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[7] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("DVC", typeof(string)),
                        new DataColumn("STK", typeof(string)),
                        new DataColumn("SO_SERIAL", typeof(string)),
                        new DataColumn("NGAY_CAP", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                        new DataColumn("GHI_CHU", typeof(string))
                    }
                );
                DataRow stk_dr;
                int i = 0;

                if (txtTBTK_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC1.Text;
                    stk_dr["STK"] = txtTBTK_STK1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso1.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC2.Text;
                    stk_dr["STK"] = txtTBTK_STK2.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso2.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC3.Text;
                    stk_dr["STK"] = txtTBTK_STK3.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso3.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC4.Text;
                    stk_dr["STK"] = txtTBTK_STK4.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso4.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK_DVC5.Text;
                    stk_dr["STK"] = txtTBTK_STK5.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK_Soso5.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK_Ngaygui5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                string font_family = "Times New Roman";
                double font_size = 12;
                bool last_row_bold = false;
                byte table_index = 1;
                byte table_row_height = 60;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                //Lấy file mẫu biểu tương ứng với chi nhánh
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);

                //Cập nhật sổ tiết kiệm báo mất trong bảng THONGBAOSTK
                CAP_NHAT_STK_THAY_CN1();
            }
            else if (cboxMaubieu.Text.Contains(@"12/VBAHD"))
            {
                tctTT_Taikhoan.SelectedTab = tpThongbaoTK2;

                if (txtTBTK2_KH_HOTEN.Text == "")
                {
                    MessageBox.Show("Chưa có thông tin khách hàng. Đề nghị nhập lại thông tin khách hàng!");
                    txtTBTK_KH_HOTEN.Focus();
                    return;
                }

                if (txtTBTK2_STK1.Text == "" && txtTBTK2_STK2.Text == "" && txtTBTK2_STK3.Text == "" && txtTBTK2_STK4.Text == "" && txtTBTK2_STK5.Text == "")
                {
                    MessageBox.Show("Chưa chọn sổ tiết kiệm nào. Đề nghị kiểm tra lại!");
                    return;
                }

                if (txtTBTK2_STK1.Text != "" && txtTBTK2_Soso1.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK2_Soso1.Focus();
                    return;
                }

                if (txtTBTK2_STK2.Text != "" && txtTBTK2_Soso2.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK2_Soso2.Focus();
                    return;
                }

                if (txtTBTK2_STK3.Text != "" && txtTBTK2_Soso3.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK2_Soso3.Focus();
                    return;
                }

                if (txtTBTK2_STK4.Text != "" && txtTBTK2_Soso4.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK2_Soso4.Focus();
                    return;
                }

                if (txtTBTK2_STK5.Text != "" && txtTBTK2_Soso5.Text == "")
                {
                    MessageBox.Show("Chưa nhập số sổ tiết kiệm. Đề nghị kiểm tra lại!");
                    txtTBTK2_Soso5.Focus();
                    return;
                }

                //Kiểm tra dữ liệu ngày cấp sổ tiết kiệm đã đúng định dạng hay chưa
                if (txtTBTK2_Ngaygui1.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK2_Ngaygui1.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK2_Ngaygui1.Focus();
                    return;
                }

                if (txtTBTK2_Ngaygui2.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK2_Ngaygui2.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK2_Ngaygui2.Focus();
                    return;
                }

                if (txtTBTK2_Ngaygui3.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK2_Ngaygui3.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK2_Ngaygui3.Focus();
                    return;
                }

                if (txtTBTK2_Ngaygui4.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK2_Ngaygui4.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK2_Ngaygui4.Focus();
                    return;
                }

                if (txtTBTK2_Ngaygui5.Text != "" && !CommonMethod.KiemTraNhapNgay(txtTBTK2_Ngaygui5.Text))
                {
                    MessageBox.Show("Dữ liệu ngày chưa đúng. Đề nghị kiểm tra lại");
                    txtTBTK2_Ngaygui5.Focus();
                    return;
                }

                //Lấy thông tin chung
                LAY_TT_CHUNG();

                //Lấy thông tin sổ tiết kiệm
                LAY_TT_TBTK2();

                //Tiêu đề bảng
                List<string> list_title = new List<string>();
                list_title.Add("STT");
                list_title.Add("Agribank nơi mở thẻ TK");
                list_title.Add("Số seri Thẻ TK");
                list_title.Add("Ngày cấp thẻ TK");
                list_title.Add("Số dư khi mở Thẻ tiết kiệm");
                list_title.Add("Ghi chú");

                System.Data.DataTable stk_dt = new System.Data.DataTable();
                stk_dt.Columns.AddRange
                (
                    new DataColumn[6] 
                    { 
                        new DataColumn("STT", typeof(string)),
                        new DataColumn("DVC", typeof(string)),
                        //new DataColumn("CNC", typeof(string)),
                        new DataColumn("SO_SERIAL", typeof(string)),
                        new DataColumn("NGAY_CAP", typeof(string)),
                        new DataColumn("SO_DU", typeof(string)),
                        new DataColumn("GHI_CHU", typeof(string))
                    }
                );
                DataRow stk_dr;
                int i = 0;

                if (txtTBTK2_STK1.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK2_DVC1.Text;
                    //stk_dr["CNC"] = txtTBTK2_CNC1.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK2_Soso1.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK2_Ngaygui1.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK2_Sodu1.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK2_STK2.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK2_DVC2.Text;
                    //stk_dr["CNC"] = txtTBTK2_CNC2.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK2_Soso2.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK2_Ngaygui2.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK2_Sodu2.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK2_STK3.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK2_DVC3.Text;
                    //stk_dr["CNC"] = txtTBTK2_CNC3.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK2_Soso3.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK2_Ngaygui3.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK2_Sodu3.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK2_STK4.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK2_DVC4.Text;
                    //stk_dr["CNC"] = txtTBTK2_CNC4.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK2_Soso4.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK2_Ngaygui4.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK2_Sodu4.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                if (txtTBTK2_STK5.Text != "")
                {
                    stk_dr = stk_dt.NewRow();
                    stk_dr["STT"] = Convert.ToString(i + 1);
                    stk_dr["DVC"] = txtTBTK2_DVC5.Text;
                    //stk_dr["CNC"] = txtTBTK2_CNC5.Text;
                    stk_dr["SO_SERIAL"] = txtTBTK2_Soso5.Text;
                    stk_dr["NGAY_CAP"] = txtTBTK2_Ngaygui5.Text;
                    stk_dr["SO_DU"] = ControlFormat.ToFormatPrice(Convert.ToInt64(ControlFormat.skipComma(txtTBTK2_Sodu5.Text)));
                    stk_dt.Rows.Add(stk_dr);
                    i = i + 1;
                }

                string font_family = "Times New Roman";
                double font_size = 12;
                bool last_row_bold = false;
                byte table_index = 0;
                byte table_row_height = 60;
                int merge_row_index = -1;
                int start_index = -1;
                int end_index = -1;
                string p_footer = "";
                //Lấy file mẫu biểu tương ứng với chi nhánh
                this.TAO_MAU_BIEU_KE_TOAN_1(file_mau_bieu, stk_dt, list_title, font_family, font_size, last_row_bold, table_index, table_row_height, merge_row_index, start_index, end_index, p_footer);

                //Cập nhật sổ tiết kiệm báo hỏng trong bảng THONGBAOSTK
                CAP_NHAT_STK_HONG_KH();
            }
        }

        private void dgvDanhsachCN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
            }
            catch { }
        }

        private void cboxLanhdao_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable nhanvien = nv_bus.NHAN_VIEN_THEO_MANV(cboxLanhdao.SelectedValue.ToString());
            if (nhanvien.Rows.Count > 0)
            {
                chucvu_lanhdao = nhanvien.Rows[0]["CHUCVU"].ToString();
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXNSD_Them1_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSD_STK1.Text == "")
                {
                    btnXNSD_Them1.Text = "Xóa";
                    txtXNSD_STK1.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSD_Loaitien1.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Ngaygui1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtXNSD_Kyhan1.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                    txtXNSD_Sodu1.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        }

        private void btnXNSD_Them2_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSD_STK2.Text == "")
                {
                    btnXNSD_Them2.Text = "Xóa";
                    txtXNSD_STK2.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSD_Loaitien2.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Ngaygui2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtXNSD_Kyhan2.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                    txtXNSD_Sodu2.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        }

        private void btnXNSD_Them3_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSD_STK3.Text == "")
                {
                    btnXNSD_Them3.Text = "Xóa";
                    txtXNSD_STK3.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSD_Loaitien3.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Ngaygui3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtXNSD_Kyhan3.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                    txtXNSD_Sodu3.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        }

        private void btnXNSD_Them4_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSD_STK4.Text == "")
                {
                    btnXNSD_Them4.Text = "Xóa";
                    txtXNSD_STK4.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSD_Loaitien4.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Ngaygui4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtXNSD_Kyhan4.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                    txtXNSD_Sodu4.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        }

        private void btnXNSD_Them5_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSD_STK5.Text == "")
                {
                    btnXNSD_Them5.Text = "Xóa";
                    txtXNSD_STK5.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSD_Loaitien5.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSD_Ngaygui5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtXNSD_Kyhan5.Text = dgvDanhsachTK.CurrentRow.Cells["Kỳ hạn"].Value.ToString();
                    txtXNSD_Sodu5.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        private void btnXNSDTT_Them1_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSDTT_STK1.Text == "")
                {
                    btnXNSDTT_Them1.Text = "Xóa";
                    txtXNSDTT_STK1.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSDTT_Loaitien1.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Ngaymo1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                    txtXNSDTT_Sodu1.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSDTT_STK2.Text == "")
                {
                    btnXNSDTT_Them2.Text = "Xóa";
                    txtXNSDTT_STK2.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSDTT_Loaitien2.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Ngaymo2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                    txtXNSDTT_Sodu2.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        }

        private void btnXNSDTT_Them3_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSDTT_STK3.Text == "")
                {
                    btnXNSDTT_Them3.Text = "Xóa";
                    txtXNSDTT_STK3.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSDTT_Loaitien3.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Ngaymo3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                    txtXNSDTT_Sodu3.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        }

        private void btnXNSDTT_Them4_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSDTT_STK4.Text == "")
                {
                    btnXNSDTT_Them4.Text = "Xóa";
                    txtXNSDTT_STK4.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSDTT_Loaitien4.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Ngaymo4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                    txtXNSDTT_Sodu4.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
        }

        private void btnXNSDTT_Them5_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (txtXNSDTT_STK5.Text == "")
                {
                    btnXNSDTT_Them5.Text = "Xóa";
                    txtXNSDTT_STK5.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtXNSDTT_Loaitien5.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Loaitien.Text = dgvDanhsachTK.CurrentRow.Cells["Loại tiền tệ"].Value.ToString();
                    txtXNSDTT_Ngaymo5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày mở"].Value.ToString();
                    txtXNSDTT_Sodu5.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
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
                makh = dt_temp.Rows[0][3].ToString().Substring(0, 4) + dt_temp.Rows[0][4].ToString();               

                System.Data.DataTable dt_stk = new System.Data.DataTable();
                dt_stk.Columns.AddRange
                (
                    new DataColumn[4] 
                { 
                    new DataColumn("Số TK", typeof(string)),
                    new DataColumn("Loại tiền tệ", typeof(string)),
                    new DataColumn("Ngày mở", typeof(string)),
                    new DataColumn("Số dư", typeof(string))
      
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
                        dr_stk["Số dư"] = Convert.ToDecimal(dt_temp.Rows[i][7].ToString()).ToString("#,#.##", System.Globalization.CultureInfo.InvariantCulture);
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
                dgvDanhsachTK.DataSource = null;
                dgvDanhsachTK.DataSource = dt_stk;
                dgvDanhsachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Cursor.Current = Cursors.Default;

                //Cập nhật bảng TAIKHOAN từ table dt_taikhoan
                if (dt_taikhoan.Rows.Count > 0)
                {
                    bool update_tk = taikhoan_bus.UPDATE_TAIKHOAN_TUFILE(dt_taikhoan);
                }
                tctTT_Taikhoan.SelectedTab = tpXacnhansodutt;
                cboxMaubieu.Text = "Mẫu 05/VBAHD - Xác nhận số dư tài khoản thanh toán";

                //Điền thông tin vào tab xác nhận số dư thanh toán
                FILL_TAB_XNSDTT(makh);
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
                cboxMaubieu.Enabled = false;

                //Khóa chức năng tìm kiếm sổ tiết kiệm báo mất
                txtTimNgaybaomat.Enabled = false;
                btnNgaybaomat.Enabled = false;

                txtTimMAKH.Enabled = false;
                btnTimMAKH.Enabled = false;

                txtTimSoso.Enabled = false;
                btnTimSoso.Enabled = false;
            }
            else if (tctTT_Taikhoan.SelectedTab == tpXacnhansodutt)
            {
                btnNhapTKTK.Enabled = false;
                btnNhapTKTT.Enabled = true;
                cboxMaubieu.Text = "Mẫu 05/VBAHD - Xác nhận số dư tài khoản thanh toán";
                cboxMaubieu.Enabled = false;

                //Khóa chức năng tìm kiếm sổ tiết kiệm báo mất
                txtTimNgaybaomat.Enabled = false;
                btnNgaybaomat.Enabled = false;

                txtTimMAKH.Enabled = false;
                btnTimMAKH.Enabled = false;

                txtTimSoso.Enabled = false;
                btnTimSoso.Enabled = false;
            }
            else if (tctTT_Taikhoan.SelectedTab == tpThongbaoTK)
            {
                btnNhapTKTK.Enabled = true;
                btnNhapTKTT.Enabled = false;
                cboxMaubieu.Text = "Mẫu 06/VBAHD - Giấy báo mất sổ tiết kiệm - áp dụng cho khách hàng";
                cboxMaubieu.Enabled = true;

                //Bật chức năng tìm kiếm sổ tiết kiệm báo mất
                txtTimNgaybaomat.Enabled = true;
                btnNgaybaomat.Enabled = true;

                txtTimMAKH.Enabled = true;
                btnTimMAKH.Enabled = true;

                txtTimSoso.Enabled = true;
                btnTimSoso.Enabled = true;
            }
            else if (tctTT_Taikhoan.SelectedTab == tpThongbaoTK2)
            {
                btnNhapTKTK.Enabled = true;
                btnNhapTKTT.Enabled = false;
                cboxMaubieu.Text = "Mẫu 12/VBAHD - Giấy báo hỏng sổ tiết kiệm - áp dụng cho khách hàng";
                cboxMaubieu.Enabled = false;

                //Khóa chức năng tìm kiếm sổ tiết kiệm báo mất
                txtTimNgaybaomat.Enabled = false;
                btnNgaybaomat.Enabled = false;

                txtTimMAKH.Enabled = false;
                btnTimMAKH.Enabled = false;

                txtTimSoso.Enabled = false;
                btnTimSoso.Enabled = false;
            }
        }

        private void btnTBTK_Them1_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK_Them1.Text == "Thêm")
                {
                    btnTBTK_Them1.Text = "Xóa";
                    txtTBTK_STK1.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK_Soso1.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK_Sodu1.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK_Ngaygui1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK_DVC1.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK_CNC1.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK_Ngaybaomat1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();
                    txtTBTK_So_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 1"].Value.ToString(); 
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString();
                    }

                    txtTBTK_So_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 2"].Value.ToString(); 
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString();
                    }

                    //Lấy thông tin khách hàng khi sử dụng chức năng tìm kiếm
                    if (dgvDanhsachTK.Columns.Contains("Tình trạng"))
                    {
                        System.Data.DataTable dt_kh = tk_bus.TAI_KHOAN_THEO_STK(dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString());
                        if (dt_kh.Rows.Count > 0)
                        {
                            tbtk_kh_makh = dt_kh.Rows[0]["MAKH"].ToString();
                            tbtk_kh_hoten = dt_kh.Rows[0]["HOTEN"].ToString();
                            tbtk_kh_cmnd = dt_kh.Rows[0]["CMND"].ToString();
                            tbtk_kh_ngaycapcmnd = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            tbtk_kh_noicapcmnd = dt_kh.Rows[0]["NOICAP"].ToString();
                            tbtk_kh_diachi = dt_kh.Rows[0]["DIACHI1"].ToString();

                            txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                            //txtTBTK_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            //txtTBTK_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                            //txtTBTK_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();           
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                            return;
                        }
                    }
                
                    //Lấy thông tin khác
                    TBTK_NGAY_KH_BAO = txtTBTK_Ngaybaomat1.Text;
                    TBTK_NGAY_TIM_THAY = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                    //TBTK_SO_TB_MAT_CN_LOAI2 = txtTBTK_So_tb_mat_cn2.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI2 = txtTBTK_Ngay_tb_mat_cn2.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI2 = txtTBTK_So_tb_thay_cn2.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI2 = txtTBTK_Ngay_tb_thay_cn2.Text;
                    //TBTK_SO_TB_MAT_CN_LOAI1 = txtTBTK_So_tb_mat_cn1.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI1 = txtTBTK_Ngay_tb_mat_cn1.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI1 = txtTBTK_So_tb_thay_cn1.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI1 = txtTBTK_Ngay_tb_thay_cn1.Text;
                    TBTK_CNC = txtTBTK_CNC1.Text;
                    TBTK_CN_THONG_BAO = txtTBTK_CNC1.Text;
                    TBTK_NGAY_TIM_THAY1 = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                    
                }
                else
                {
                    btnTBTK_Them1.Text = "Thêm";
                    txtTBTK_STK1.Clear();
                    txtTBTK_Soso1.Clear();
                    txtTBTK_Ngaygui1.Clear();
                    txtTBTK_DVC1.Clear();
                    txtTBTK_CNC1.Clear();
                    txtTBTK_Sodu1.Clear();
                    txtTBTK_Ngaybaomat1.Clear();
                    TBTK_NGAY_TIM_THAY1 = "";
                }
            }            
        }

        //Cập nhật dữ liệu sổ tiết kiệm báo mất khi tạo giấy thông báo mất của chi nhánh loại 1 (Mẫu 08/VBAHD - Thông báo mất sổ tiết kiệm - áp dụng cho chi nhánh loại 1)
        internal void CAP_NHAT_STK_MAT_CN1()
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    //new DataColumn("ID", typeof(bigint)),
                    new DataColumn("TK", typeof(string)),
                    new DataColumn("SERIAL", typeof(string)),
                    new DataColumn("DV_CAP_STK", typeof(string)),
                    new DataColumn("CN_CAP_STK", typeof(string)),
                    new DataColumn("SO_DU", typeof(decimal)),
                    new DataColumn("NGAY_KH_BAO", typeof(string)),
                    new DataColumn("NGAY_TIM_THAY", typeof(string)),
                    new DataColumn("TINH_TRANG", typeof(string)),
                    new DataColumn("CN_LOAI2_GUI", typeof(bool)),
                    new DataColumn("CN_LOAI1_GUI", typeof(bool)),
                    new DataColumn("NGAY_CAP_SO", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("XU_LY", typeof(string))
                }
            );

            DataRow dr_stk;
            //Thêm dữ liệu vào bảng dt_stk           
            if (txtTBTK_STK1.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK1.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso1.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC1.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC1.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu1.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }              
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK2.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK2.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso2.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC2.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC2.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu2.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK3.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK3.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso3.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC3.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC3.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu3.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK4.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK4.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso4.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC4.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC4.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu4.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK5.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK5.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso5.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC5.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC5.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu5.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3,2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0,2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6,4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            try
            {
                bool cap_nhat_stk_mat_cn1 = tbtk_bus.UPDATE_THONGBAOSTK_CN1(dt_stk);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        //Cập nhật dữ liệu sổ tiết kiệm báo mất khi tạo thông báo thấy của chi nhánh loại 1 (Mẫu 11/VBAHD - Thông báo thấy sổ tiết kiệm - áp dụng cho chi nhánh loại 1)
        internal void CAP_NHAT_STK_THAY_CN1()
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    //new DataColumn("ID", typeof(bigint)),
                    new DataColumn("TK", typeof(string)),
                    new DataColumn("SERIAL", typeof(string)),
                    new DataColumn("DV_CAP_STK", typeof(string)),
                    new DataColumn("CN_CAP_STK", typeof(string)),
                    new DataColumn("SO_DU", typeof(decimal)),
                    new DataColumn("NGAY_KH_BAO", typeof(string)),
                    new DataColumn("NGAY_TIM_THAY", typeof(string)),
                    new DataColumn("TINH_TRANG", typeof(string)),
                    new DataColumn("CN_LOAI2_GUI", typeof(bool)),
                    new DataColumn("CN_LOAI1_GUI", typeof(bool)),
                    new DataColumn("NGAY_CAP_SO", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("XU_LY", typeof(string))
                }
            );

            DataRow dr_stk;
            //Thêm dữ liệu vào bảng dt_stk
            if (txtTBTK_STK1.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK1.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso1.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC1.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC1.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu1.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY1.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY1.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY1.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                if (txtTBTK_Ngay_tb_thay_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = txtTBTK_Ngay_tb_thay_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = txtTBTK_So_tb_thay_cn1.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK2.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK2.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso2.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC2.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC2.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu2.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY2.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY2.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY2.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                if (txtTBTK_Ngay_tb_thay_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = txtTBTK_Ngay_tb_thay_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = txtTBTK_So_tb_thay_cn1.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK3.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK3.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso3.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC3.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC3.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu3.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY3.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY3.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY3.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                if (txtTBTK_Ngay_tb_thay_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = txtTBTK_Ngay_tb_thay_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = txtTBTK_So_tb_thay_cn1.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK4.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK4.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso4.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC4.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC4.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu4.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY4.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY4.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY4.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                if (txtTBTK_Ngay_tb_thay_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = txtTBTK_Ngay_tb_thay_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = txtTBTK_So_tb_thay_cn1.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK5.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK5.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso5.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC5.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC5.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu5.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY5.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY5.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY5.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                if (txtTBTK_Ngay_tb_thay_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = txtTBTK_Ngay_tb_thay_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_thay_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = txtTBTK_So_tb_thay_cn1.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            try
            {
                bool cap_nhat_stk_mat_kh = tbtk_bus.UPDATE_THONGBAOSTK_CN1(dt_stk);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        //Cập nhật dữ liệu sổ tiết kiệm báo mất khi tạo giấy thông báo mất của chi nhánh loại 2 (Mẫu 07/VBAHD - Thông báo mất sổ tiết kiệm - áp dụng cho chi nhánh loại 2)
        internal void CAP_NHAT_STK_MAT_CN2()
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    //new DataColumn("ID", typeof(bigint)),
                    new DataColumn("TK", typeof(string)),
                    new DataColumn("SERIAL", typeof(string)),
                    new DataColumn("DV_CAP_STK", typeof(string)),
                    new DataColumn("CN_CAP_STK", typeof(string)),
                    new DataColumn("SO_DU", typeof(decimal)),
                    new DataColumn("NGAY_KH_BAO", typeof(string)),
                    new DataColumn("NGAY_TIM_THAY", typeof(string)),
                    new DataColumn("TINH_TRANG", typeof(string)),
                    new DataColumn("CN_LOAI2_GUI", typeof(bool)),
                    new DataColumn("CN_LOAI1_GUI", typeof(bool)),
                    new DataColumn("NGAY_CAP_SO", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("XU_LY", typeof(string))
                }
            );

            DataRow dr_stk;
            //Thêm dữ liệu vào bảng dt_stk
            if (txtTBTK_STK1.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK1.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso1.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC1.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC1.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu1.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = true;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK2.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK2.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso2.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC2.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC2.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu2.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat2.Text.Substring(3,2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(0,2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(6,4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = true;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK3.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK3.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso3.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC3.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC3.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu3.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = true;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK4.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK4.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso4.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC4.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC4.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu4.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK5.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK5.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso5.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC5.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC5.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu5.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            try
            {
                bool cap_nhat_stk_mat_cn2 = tbtk_bus.UPDATE_THONGBAOSTK_CN2(dt_stk);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        //Cập nhật dữ liệu sổ tiết kiệm báo mất khi tạo thông báo thấy của chi nhánh loại 2 (Mẫu 10/VBAHD - Thông báo thấy sổ tiết kiệm - áp dụng cho chi nhánh loại 2)
        internal void CAP_NHAT_STK_THAY_CN2()
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    //new DataColumn("ID", typeof(bigint)),
                    new DataColumn("TK", typeof(string)),
                    new DataColumn("SERIAL", typeof(string)),
                    new DataColumn("DV_CAP_STK", typeof(string)),
                    new DataColumn("CN_CAP_STK", typeof(string)),
                    new DataColumn("SO_DU", typeof(decimal)),
                    new DataColumn("NGAY_KH_BAO", typeof(string)),
                    new DataColumn("NGAY_TIM_THAY", typeof(string)),
                    new DataColumn("TINH_TRANG", typeof(string)),
                    new DataColumn("CN_LOAI2_GUI", typeof(bool)),
                    new DataColumn("CN_LOAI1_GUI", typeof(bool)),
                    new DataColumn("NGAY_CAP_SO", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("XU_LY", typeof(string))
                }
            );

            DataRow dr_stk;
            //Thêm dữ liệu vào bảng dt_stk
            if (txtTBTK_STK1.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK1.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso1.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC1.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC1.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu1.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY1.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY1.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY1.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK2.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK2.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso2.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC2.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC2.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu2.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY2.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY2.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY2.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK3.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK3.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso3.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC3.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC3.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu3.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY3.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY3.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY3.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK4.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK4.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso4.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC4.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC4.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu4.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY4.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY4.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY4.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK5.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK5.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso5.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC5.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC5.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu5.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = TBTK_NGAY_TIM_THAY5.Substring(3, 2) + "/" + TBTK_NGAY_TIM_THAY5.Substring(0, 2) + "/" + TBTK_NGAY_TIM_THAY5.Substring(6, 4);
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = txtTBTK_So_tb_thay_cn2.Text;
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = DateTime.Now.ToString("MM/dd/yyyy");
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            try
            {
                bool cap_nhat_stk_mat_kh = tbtk_bus.UPDATE_THONGBAOSTK_CN2(dt_stk);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        //Cập nhật dữ liệu sổ tiết kiệm báo mất khi tạo giấy báo mất của khách hàng (Mẫu 06/VBAHD - Giấy báo mất sổ tiết kiệm - áp dụng cho khách hàng)
        internal void CAP_NHAT_STK_MAT_KH()
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    //new DataColumn("ID", typeof(bigint)),
                    new DataColumn("TK", typeof(string)),
                    new DataColumn("SERIAL", typeof(string)),
                    new DataColumn("DV_CAP_STK", typeof(string)),
                    new DataColumn("CN_CAP_STK", typeof(string)),
                    new DataColumn("SO_DU", typeof(decimal)),
                    new DataColumn("NGAY_KH_BAO", typeof(string)),
                    new DataColumn("NGAY_TIM_THAY", typeof(string)),
                    new DataColumn("TINH_TRANG", typeof(string)),
                    new DataColumn("CN_LOAI2_GUI", typeof(bool)),
                    new DataColumn("CN_LOAI1_GUI", typeof(bool)),
                    new DataColumn("NGAY_CAP_SO", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("XU_LY", typeof(string))
                }
            );

            DataRow dr_stk;
            //Thêm dữ liệu vào bảng dt_stk
            if (txtTBTK_STK1.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK1.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso1.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC1.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC1.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu1.Text));
                //dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900"; 
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK2.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK2.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso2.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC2.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC2.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu2.Text));
                //dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK3.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK3.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso3.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC3.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC3.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu3.Text));
                //dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK4.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK4.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso4.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC4.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC4.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu4.Text));
                //dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK5.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK5.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso5.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC5.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC5.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu5.Text));
                //dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["NGAY_KH_BAO"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Mất";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            try
            {
                bool cap_nhat_stk_mat_kh = tbtk_bus.UPDATE_THONGBAOSTK_KH(dt_stk);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        //Cập nhật dữ liệu sổ tiết kiệm báo mất khi tạo giấy báo thấy của khách hàng (Mẫu 09/VBAHD - Giấy báo thấy sổ tiết kiệm - áp dụng cho khách hàng)
        internal void CAP_NHAT_STK_THAY_KH()
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    //new DataColumn("ID", typeof(bigint)),
                    new DataColumn("TK", typeof(string)),
                    new DataColumn("SERIAL", typeof(string)),
                    new DataColumn("DV_CAP_STK", typeof(string)),
                    new DataColumn("CN_CAP_STK", typeof(string)),
                    new DataColumn("SO_DU", typeof(decimal)),
                    new DataColumn("NGAY_KH_BAO", typeof(string)),
                    new DataColumn("NGAY_TIM_THAY", typeof(string)),
                    new DataColumn("TINH_TRANG", typeof(string)),
                    new DataColumn("CN_LOAI2_GUI", typeof(bool)),
                    new DataColumn("CN_LOAI1_GUI", typeof(bool)),
                    new DataColumn("NGAY_CAP_SO", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("XU_LY", typeof(string))
                }
            );

            DataRow dr_stk;
            //Thêm dữ liệu vào bảng dt_stk
            if (txtTBTK_STK1.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK1.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso1.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC1.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC1.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu1.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat1.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui1.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui1.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }               
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK2.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK2.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso2.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC2.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC2.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu2.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat2.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui2.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui2.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK3.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK3.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso3.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC3.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC3.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu3.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat3.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui3.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui3.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK4.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK4.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso4.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC4.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC4.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu4.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat4.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui4.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui4.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK_STK5.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK_STK5.Text;
                dr_stk["SERIAL"] = txtTBTK_Soso5.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK_DVC5.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK_CNC5.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK_Sodu5.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK_Ngaybaomat5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaybaomat5.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = CommonMethod.Thang(DateTime.Now.ToString("MM")) + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                dr_stk["TINH_TRANG"] = "Thấy";
                if (txtTBTK_So_tb_mat_cn2.Text == "" && txtTBTK_Ngay_tb_mat_cn2.Text == "")
                {
                    dr_stk["CN_LOAI2_GUI"] = false;
                }
                else
                {
                    dr_stk["CN_LOAI2_GUI"] = true;
                }
                dr_stk["CN_LOAI1_GUI"] = true;
                dr_stk["NGAY_CAP_SO"] = txtTBTK_Ngaygui5.Text.Substring(3, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(0, 2) + "/" + txtTBTK_Ngaygui5.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = txtTBTK_So_tb_mat_cn2.Text;
                if (txtTBTK_Ngay_tb_mat_cn2.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = txtTBTK_Ngay_tb_mat_cn2.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn2.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = txtTBTK_So_tb_mat_cn1.Text;
                if (txtTBTK_Ngay_tb_mat_cn1.Text != "")
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = txtTBTK_Ngay_tb_mat_cn1.Text.Substring(3, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(0, 2) + "/" + txtTBTK_Ngay_tb_mat_cn1.Text.Substring(6, 4);
                }
                else
                {
                    dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                }
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = "";
                dt_stk.Rows.Add(dr_stk);
            }

            try
            {
                bool cap_nhat_stk_mat_kh = tbtk_bus.UPDATE_THONGBAOSTK_KH(dt_stk);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        //Cập nhật tông tin sổ tiết kiệm hỏng vào cơ sở dữ liệu
        internal void CAP_NHAT_STK_HONG_KH()
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    //new DataColumn("ID", typeof(bigint)),
                    new DataColumn("TK", typeof(string)),
                    new DataColumn("SERIAL", typeof(string)),
                    new DataColumn("DV_CAP_STK", typeof(string)),
                    new DataColumn("CN_CAP_STK", typeof(string)),
                    new DataColumn("SO_DU", typeof(decimal)),
                    new DataColumn("NGAY_KH_BAO", typeof(string)),
                    new DataColumn("NGAY_TIM_THAY", typeof(string)),
                    new DataColumn("TINH_TRANG", typeof(string)),
                    new DataColumn("CN_LOAI2_GUI", typeof(bool)),
                    new DataColumn("CN_LOAI1_GUI", typeof(bool)),
                    new DataColumn("NGAY_CAP_SO", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI2", typeof(string)),
                    new DataColumn("SO_TB_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_MAT_CN_LOAI1", typeof(string)),
                    new DataColumn("SO_TB_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("NGAY_BAO_THAY_CN_LOAI1", typeof(string)),
                    new DataColumn("XU_LY", typeof(string))
                }
            );

            DataRow dr_stk;
            //Thêm dữ liệu vào bảng dt_stk
            if (txtTBTK2_STK1.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK2_STK1.Text;
                dr_stk["SERIAL"] = txtTBTK2_Soso1.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK2_DVC1.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK2_CNC1.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK2_Sodu1.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK2_Ngaybaohong1.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaybaohong1.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaybaohong1.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Hỏng";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK2_Ngaygui1.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaygui1.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaygui1.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = txtTBTK2_XU_LY.Text;
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK2_STK2.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK2_STK2.Text;
                dr_stk["SERIAL"] = txtTBTK2_Soso2.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK2_DVC2.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK2_CNC2.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK2_Sodu2.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK2_Ngaybaohong2.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaybaohong2.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaybaohong2.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Hỏng";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK2_Ngaygui2.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaygui2.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaygui2.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = txtTBTK2_XU_LY.Text;
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK2_STK3.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK2_STK3.Text;
                dr_stk["SERIAL"] = txtTBTK2_Soso3.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK2_DVC3.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK2_CNC3.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK2_Sodu3.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK2_Ngaybaohong3.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaybaohong3.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaybaohong3.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Hỏng";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK2_Ngaygui3.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaygui3.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaygui3.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = txtTBTK2_XU_LY.Text;
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK2_STK4.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK2_STK4.Text;
                dr_stk["SERIAL"] = txtTBTK2_Soso4.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK2_DVC4.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK2_CNC4.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK2_Sodu4.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK2_Ngaybaohong4.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaybaohong4.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaybaohong4.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Hỏng";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK2_Ngaygui4.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaygui4.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaygui4.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = txtTBTK2_XU_LY.Text;
                dt_stk.Rows.Add(dr_stk);
            }

            if (txtTBTK2_STK5.Text != "")
            {
                dr_stk = dt_stk.NewRow();
                dr_stk["TK"] = txtTBTK2_STK5.Text;
                dr_stk["SERIAL"] = txtTBTK2_Soso5.Text;
                dr_stk["DV_CAP_STK"] = txtTBTK2_DVC5.Text;
                dr_stk["CN_CAP_STK"] = txtTBTK2_CNC5.Text;
                dr_stk["SO_DU"] = Convert.ToDecimal(ControlFormat.skipComma(txtTBTK2_Sodu5.Text));
                dr_stk["NGAY_KH_BAO"] = txtTBTK2_Ngaybaohong5.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaybaohong5.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaybaohong5.Text.Substring(6, 4);
                dr_stk["NGAY_TIM_THAY"] = "01/01/1900";
                dr_stk["TINH_TRANG"] = "Hỏng";
                dr_stk["CN_LOAI2_GUI"] = false;
                dr_stk["CN_LOAI1_GUI"] = false;
                dr_stk["NGAY_CAP_SO"] = txtTBTK2_Ngaygui5.Text.Substring(3, 2) + "/" + txtTBTK2_Ngaygui5.Text.Substring(0, 2) + "/" + txtTBTK2_Ngaygui5.Text.Substring(6, 4);
                dr_stk["SO_TB_MAT_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI2"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI2"] = "01/01/1900";
                dr_stk["SO_TB_MAT_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_MAT_CN_LOAI1"] = "01/01/1900";
                dr_stk["SO_TB_THAY_CN_LOAI1"] = "";
                dr_stk["NGAY_BAO_THAY_CN_LOAI1"] = "01/01/1900";
                dr_stk["XU_LY"] = txtTBTK2_XU_LY.Text;
                dt_stk.Rows.Add(dr_stk);
            }

            try
            {
                bool cap_nhat_stk_hong_kh = tbtk_bus.UPDATE_THONGBAOSTK_KH(dt_stk);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnTBTK_Them2_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK_Them2.Text == "Thêm")
                {
                    btnTBTK_Them2.Text = "Xóa";
                    txtTBTK_STK2.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK_Soso2.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK_Sodu2.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK_Ngaygui2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK_DVC2.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK_CNC2.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK_Ngaybaomat2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();
                    
                    txtTBTK_So_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString();
                    }

                    txtTBTK_So_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString();
                    }

                    //Lấy thông tin khách hàng khi sử dụng chức năng tìm kiếm
                    if (dgvDanhsachTK.Columns.Contains("Tình trạng"))
                    {
                        System.Data.DataTable dt_kh = tk_bus.TAI_KHOAN_THEO_STK(dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString());
                        if (dt_kh.Rows.Count > 0)
                        {
                            tbtk_kh_makh = dt_kh.Rows[0]["MAKH"].ToString();
                            tbtk_kh_hoten = dt_kh.Rows[0]["HOTEN"].ToString();
                            tbtk_kh_cmnd = dt_kh.Rows[0]["CMND"].ToString();
                            tbtk_kh_ngaycapcmnd = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            tbtk_kh_noicapcmnd = dt_kh.Rows[0]["NOICAP"].ToString();
                            tbtk_kh_diachi = dt_kh.Rows[0]["DIACHI1"].ToString();

                            txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                            //txtTBTK_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            //txtTBTK_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                            //txtTBTK_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();           
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                            return;
                        }
                    }

                    //Lấy thông tin khác
                    TBTK_NGAY_KH_BAO = txtTBTK_Ngaybaomat2.Text;
                    TBTK_NGAY_TIM_THAY = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                    //TBTK_SO_TB_MAT_CN_LOAI2 = txtTBTK_So_tb_mat_cn2.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI2 = txtTBTK_Ngay_tb_mat_cn2.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI2 = txtTBTK_So_tb_thay_cn2.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI2 = txtTBTK_Ngay_tb_thay_cn2.Text;
                    //TBTK_SO_TB_MAT_CN_LOAI1 = txtTBTK_So_tb_mat_cn1.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI1 = txtTBTK_Ngay_tb_mat_cn1.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI1 = txtTBTK_So_tb_thay_cn1.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI1 = txtTBTK_Ngay_tb_thay_cn1.Text;
                    TBTK_CNC = txtTBTK_CNC2.Text;
                    TBTK_CN_THONG_BAO = txtTBTK_CNC2.Text;
                    TBTK_NGAY_TIM_THAY2 = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                }
                else
                {
                    btnTBTK_Them2.Text = "Thêm";
                    txtTBTK_STK2.Clear();
                    txtTBTK_Soso2.Clear();
                    txtTBTK_Ngaygui2.Clear();
                    txtTBTK_DVC2.Clear();
                    txtTBTK_CNC2.Clear();
                    txtTBTK_Sodu2.Clear();
                    txtTBTK_Ngaybaomat2.Clear();
                    TBTK_NGAY_TIM_THAY2 = "";
                }
            }            
        }

        private void btnTBTK_Them3_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK_Them3.Text == "Thêm")
                {
                    btnTBTK_Them3.Text = "Xóa";
                    txtTBTK_STK3.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK_Soso3.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK_Sodu3.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK_Ngaygui3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK_DVC3.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK_CNC3.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK_Ngaybaomat3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();

                    txtTBTK_So_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString();
                    }

                    txtTBTK_So_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString();
                    }

                    //Lấy thông tin khách hàng khi sử dụng chức năng tìm kiếm
                    if (dgvDanhsachTK.Columns.Contains("Tình trạng"))
                    {
                        System.Data.DataTable dt_kh = tk_bus.TAI_KHOAN_THEO_STK(dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString());
                        if (dt_kh.Rows.Count > 0)
                        {
                            tbtk_kh_makh = dt_kh.Rows[0]["MAKH"].ToString();
                            tbtk_kh_hoten = dt_kh.Rows[0]["HOTEN"].ToString();
                            tbtk_kh_cmnd = dt_kh.Rows[0]["CMND"].ToString();
                            tbtk_kh_ngaycapcmnd = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            tbtk_kh_noicapcmnd = dt_kh.Rows[0]["NOICAP"].ToString();
                            tbtk_kh_diachi = dt_kh.Rows[0]["DIACHI1"].ToString();

                            txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                            //txtTBTK_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            //txtTBTK_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                            //txtTBTK_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();           
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                            return;
                        }
                    }

                    //Lấy thông tin khác
                    TBTK_NGAY_KH_BAO = txtTBTK_Ngaybaomat3.Text;
                    TBTK_NGAY_TIM_THAY = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                    //TBTK_SO_TB_MAT_CN_LOAI2 = txtTBTK_So_tb_mat_cn2.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI2 = txtTBTK_Ngay_tb_mat_cn2.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI2 = txtTBTK_So_tb_thay_cn2.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI2 = txtTBTK_Ngay_tb_thay_cn2.Text;
                    //TBTK_SO_TB_MAT_CN_LOAI1 = txtTBTK_So_tb_mat_cn1.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI1 = txtTBTK_Ngay_tb_mat_cn1.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI1 = txtTBTK_So_tb_thay_cn1.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI1 = txtTBTK_Ngay_tb_thay_cn1.Text;
                    TBTK_CNC = txtTBTK_CNC3.Text;
                    TBTK_CN_THONG_BAO = txtTBTK_CNC3.Text;
                    TBTK_NGAY_TIM_THAY3 = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                }
                else
                {
                    btnTBTK_Them3.Text = "Thêm";
                    txtTBTK_STK3.Clear();
                    txtTBTK_Soso3.Clear();
                    txtTBTK_Ngaygui3.Clear();
                    txtTBTK_DVC3.Clear();
                    txtTBTK_CNC3.Clear();
                    txtTBTK_Sodu3.Clear();
                    txtTBTK_Ngaybaomat3.Clear();
                    TBTK_NGAY_TIM_THAY3 = "";
                }
            }       
        }

        private void btnTBTK_Them4_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK_Them4.Text == "Thêm")
                {
                    btnTBTK_Them4.Text = "Xóa";
                    txtTBTK_STK4.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK_Soso4.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK_Sodu4.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK_Ngaygui4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK_DVC4.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK_CNC4.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK_Ngaybaomat4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();

                    txtTBTK_So_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString();
                    }

                    txtTBTK_So_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString();
                    }

                    //Lấy thông tin khách hàng khi sử dụng chức năng tìm kiếm
                    if (dgvDanhsachTK.Columns.Contains("Tình trạng"))
                    {
                        System.Data.DataTable dt_kh = tk_bus.TAI_KHOAN_THEO_STK(dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString());
                        if (dt_kh.Rows.Count > 0)
                        {
                            tbtk_kh_makh = dt_kh.Rows[0]["MAKH"].ToString();
                            tbtk_kh_hoten = dt_kh.Rows[0]["HOTEN"].ToString();
                            tbtk_kh_cmnd = dt_kh.Rows[0]["CMND"].ToString();
                            tbtk_kh_ngaycapcmnd = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            tbtk_kh_noicapcmnd = dt_kh.Rows[0]["NOICAP"].ToString();
                            tbtk_kh_diachi = dt_kh.Rows[0]["DIACHI1"].ToString();

                            txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                            //txtTBTK_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            //txtTBTK_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                            //txtTBTK_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();           
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                            return;
                        }
                    }

                    //Lấy thông tin khác
                    TBTK_NGAY_KH_BAO = txtTBTK_Ngaybaomat4.Text;
                    TBTK_NGAY_TIM_THAY = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                    //TBTK_SO_TB_MAT_CN_LOAI2 = txtTBTK_So_tb_mat_cn2.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI2 = txtTBTK_Ngay_tb_mat_cn2.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI2 = txtTBTK_So_tb_thay_cn2.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI2 = txtTBTK_Ngay_tb_thay_cn2.Text;
                    //TBTK_SO_TB_MAT_CN_LOAI1 = txtTBTK_So_tb_mat_cn1.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI1 = txtTBTK_Ngay_tb_mat_cn1.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI1 = txtTBTK_So_tb_thay_cn1.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI1 = txtTBTK_Ngay_tb_thay_cn1.Text;
                    TBTK_CNC = txtTBTK_CNC4.Text;
                    TBTK_CN_THONG_BAO = txtTBTK_CNC4.Text;
                    TBTK_NGAY_TIM_THAY4 = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                }
                else
                {
                    btnTBTK_Them4.Text = "Thêm";
                    txtTBTK_STK4.Clear();
                    txtTBTK_Soso4.Clear();
                    txtTBTK_Ngaygui4.Clear();
                    txtTBTK_DVC4.Clear();
                    txtTBTK_CNC4.Clear();
                    txtTBTK_Sodu4.Clear();
                    txtTBTK_Ngaybaomat4.Clear();
                    TBTK_NGAY_TIM_THAY4 = "";
                }
            }
            
        }

        private void btnTBTK_Them5_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK_Them5.Text == "Thêm")
                {
                    btnTBTK_Them5.Text = "Xóa";
                    txtTBTK_STK5.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK_Soso5.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK_Sodu5.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK_Ngaygui5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK_DVC5.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK_CNC5.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK_Ngaybaomat5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();

                    txtTBTK_So_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 1"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 1"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 1"].Value.ToString();
                    }

                    txtTBTK_So_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo mất CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_mat_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất CN Loại 2"].Value.ToString();
                    }
                    txtTBTK_So_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Số thông báo thấy CN Loại 2"].Value.ToString();
                    if (dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString() == "01/01/1900")
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = "";
                    }
                    else
                    {
                        txtTBTK_Ngay_tb_thay_cn2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo thấy CN Loại 2"].Value.ToString();
                    }

                    //Lấy thông tin khách hàng khi sử dụng chức năng tìm kiếm
                    if (dgvDanhsachTK.Columns.Contains("Tình trạng"))
                    {
                        System.Data.DataTable dt_kh = tk_bus.TAI_KHOAN_THEO_STK(dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString());
                        if (dt_kh.Rows.Count > 0)
                        {
                            tbtk_kh_makh = dt_kh.Rows[0]["MAKH"].ToString();
                            tbtk_kh_hoten = dt_kh.Rows[0]["HOTEN"].ToString();
                            tbtk_kh_cmnd = dt_kh.Rows[0]["CMND"].ToString();
                            tbtk_kh_ngaycapcmnd = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            tbtk_kh_noicapcmnd = dt_kh.Rows[0]["NOICAP"].ToString();
                            tbtk_kh_diachi = dt_kh.Rows[0]["DIACHI1"].ToString();

                            txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_HOTEN.Text = dt_kh.Rows[0]["HOTEN"].ToString();
                            //txtTBTK_KH_CMND.Text = dt_kh.Rows[0]["CMND"].ToString();
                            //txtTBTK_KH_NGAYCAPCMND.Text = dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(0, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(3, 2) + "/" + dt_kh.Rows[0]["NGAYCAP"].ToString().Substring(6, 4);
                            //txtTBTK_KH_NOICAPCMND.Text = dt_kh.Rows[0]["NOICAP"].ToString();
                            //txtTBTK_KH_DIACHI.Text = dt_kh.Rows[0]["DIACHI1"].ToString();           
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng. Đề nghị nhập lại thông tin khách hàng từ IPCAS!");
                            return;
                        }
                    }

                    //Lấy thông tin khác
                    TBTK_NGAY_KH_BAO = txtTBTK_Ngaybaomat5.Text;
                    TBTK_NGAY_TIM_THAY = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                    //TBTK_SO_TB_MAT_CN_LOAI2 = txtTBTK_So_tb_mat_cn2.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI2 = txtTBTK_Ngay_tb_mat_cn2.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI2 = txtTBTK_So_tb_thay_cn2.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI2 = txtTBTK_Ngay_tb_thay_cn2.Text;
                    //TBTK_SO_TB_MAT_CN_LOAI1 = txtTBTK_So_tb_mat_cn1.Text;
                    //TBTK_NGAY_BAO_MAT_CN_LOAI1 = txtTBTK_Ngay_tb_mat_cn1.Text;
                    //TBTK_SO_TB_THAY_CN_LOAI1 = txtTBTK_So_tb_thay_cn1.Text;
                    //TBTK_NGAY_BAO_THAY_CN_LOAI1 = txtTBTK_Ngay_tb_thay_cn1.Text;
                    TBTK_CNC = txtTBTK_CNC5.Text;
                    TBTK_CN_THONG_BAO = txtTBTK_CNC5.Text;
                    TBTK_NGAY_TIM_THAY5 = dgvDanhsachTK.CurrentRow.Cells["Ngày tìm thấy"].Value.ToString();
                }
                else
                {
                    btnTBTK_Them5.Text = "Thêm";
                    txtTBTK_STK5.Clear();
                    txtTBTK_Soso5.Clear();
                    txtTBTK_Ngaygui5.Clear();
                    txtTBTK_DVC5.Clear();
                    txtTBTK_CNC5.Clear();
                    txtTBTK_Sodu5.Clear();
                    txtTBTK_Ngaybaomat5.Clear();
                    TBTK_NGAY_TIM_THAY5 = "";
                }
            }           
        }

        private void btnNgaybaomat_Click(object sender, EventArgs e)
        {
            if (!CommonMethod.KiemTraNhapNgay(txtTimNgaybaomat.Text))
            {
                MessageBox.Show("Dữ liệu ngày nhập chưa đúng định dạng dd/MM/yyyy");
                txtTimNgaybaomat.Focus();
                return;
            }
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    new DataColumn("Số TK", typeof(string)),
                    new DataColumn("Số sổ", typeof(string)),
                    new DataColumn("Số dư", typeof(string)),
                    new DataColumn("Ngày báo mất-hỏng", typeof(string)),
                    new DataColumn("Đơn vị cấp", typeof(string)),
                    new DataColumn("Chi nhánh cấp", typeof(string)),
                    new DataColumn("Tình trạng", typeof(string)),
                    new DataColumn("Ngày cấp sổ", typeof(string)), 
                    new DataColumn("Loại tiền tệ", typeof(string)),
                    new DataColumn("Kỳ hạn", typeof(ushort)),
                    new DataColumn("Số thông báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày tìm thấy", typeof(string)),
                    new DataColumn("Hình thức xử lý", typeof(string))
                }
            );
            DataRow dr_stk;
            System.Data.DataTable dt_temp = tbtk_bus.STK_MAT_THEO_NGAY_KH_BAO_MACN(txtTimNgaybaomat.Text.Substring(3, 2) + "/" + txtTimNgaybaomat.Text.Substring(0, 2) + "/" + txtTimNgaybaomat.Text.Substring(6, 4), txtMaCN.Text);
            if (dt_temp.Rows.Count > 0)
            {              
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    dr_stk = dt_stk.NewRow();
                    dr_stk["Số TK"] = dt_temp.Rows[i]["TK"].ToString();
                    dr_stk["Số sổ"] = dt_temp.Rows[i]["SERIAL"].ToString();
                    dr_stk["Số dư"] = Convert.ToDecimal(dt_temp.Rows[i]["SO_DU"].ToString()).ToString("#,#.##", System.Globalization.CultureInfo.InvariantCulture);
                    dr_stk["Ngày báo mất-hỏng"] = dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(6, 4);
                    dr_stk["Đơn vị cấp"] = dt_temp.Rows[i]["DV_CAP_STK"].ToString();
                    dr_stk["Chi nhánh cấp"] = dt_temp.Rows[i]["CN_CAP_STK"].ToString();
                    dr_stk["Tình trạng"] = dt_temp.Rows[i]["TINH_TRANG"].ToString();
                    dr_stk["Ngày cấp sổ"] = dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(6, 4);
                    dr_stk["Loại tiền tệ"] = "";
                    dr_stk["Kỳ hạn"] = 0;
                    dr_stk["Số thông báo mất CN Loại 2"] = dt_temp.Rows[i]["SO_TB_MAT_CN_LOAI2"].ToString();
                    dr_stk["Ngày báo mất CN Loại 2"] = dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(0,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(3,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(6,4);
                    dr_stk["Số thông báo thấy CN Loại 2"] = dt_temp.Rows[i]["SO_TB_THAY_CN_LOAI2"].ToString();
                    dr_stk["Ngày báo thấy CN Loại 2"] = dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(0,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(3,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(6,4);
                    dr_stk["Số thông báo mất CN Loại 1"] = dt_temp.Rows[i]["SO_TB_MAT_CN_LOAI1"].ToString();
                    dr_stk["Ngày báo mất CN Loại 1"] = dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(0,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(3,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(6,4);
                    dr_stk["Số thông báo thấy CN Loại 1"] = dt_temp.Rows[i]["SO_TB_THAY_CN_LOAI1"].ToString();
                    dr_stk["Ngày báo thấy CN Loại 1"] = dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(0,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(3,2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(6,4);
                    dr_stk["Ngày tìm thấy"] = dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(6, 4);
                    dr_stk["Hình thức xử lý"] = dt_temp.Rows[i]["XU_LY"].ToString();
                    dt_stk.Rows.Add(dr_stk);
                }

                //Gán table dt_stk vào datagridview
                dgvDanhsachTK.DataSource = null;
                dgvDanhsachTK.DataSource = dt_stk;
                dgvDanhsachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số TK
                dgvDanhsachTK.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số sổ
                dgvDanhsachTK.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số dư
                dgvDanhsachTK.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Ngày báo mất-hỏng
                dgvDanhsachTK.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Đơn vị cấp
                dgvDanhsachTK.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Chi nhánh cấp
                dgvDanhsachTK.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Tình trạng
                dgvDanhsachTK.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày cấp sổ
                dgvDanhsachTK.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Loại tiền tệ
                dgvDanhsachTK.Columns[8].Visible = false;
                dgvDanhsachTK.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Kỳ hạn
                dgvDanhsachTK.Columns[9].Visible = false;
                dgvDanhsachTK.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 2
                dgvDanhsachTK.Columns[10].Visible = false;
                dgvDanhsachTK.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 2
                dgvDanhsachTK.Columns[11].Visible = false;
                dgvDanhsachTK.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 2
                dgvDanhsachTK.Columns[12].Visible = false;
                dgvDanhsachTK.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 2
                dgvDanhsachTK.Columns[13].Visible = false;
                dgvDanhsachTK.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 1
                dgvDanhsachTK.Columns[14].Visible = false;
                dgvDanhsachTK.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 1
                dgvDanhsachTK.Columns[15].Visible = false;
                dgvDanhsachTK.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 1
                dgvDanhsachTK.Columns[16].Visible = false;
                dgvDanhsachTK.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 1
                dgvDanhsachTK.Columns[17].Visible = false;
                dgvDanhsachTK.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày tìm thấy
                dgvDanhsachTK.Columns[18].Visible = false;
                dgvDanhsachTK.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Hình thức xử lý
                dgvDanhsachTK.Columns[19].Visible = false;
                Cursor.Current = Cursors.Default;

                //Xóa thông tin sổ tiết kiệm cũ
                btnTBTK_Them1.Text = "Thêm";
                txtTBTK_STK1.Clear();
                txtTBTK_Soso1.Clear();
                txtTBTK_Ngaygui1.Clear();
                txtTBTK_DVC1.Clear();
                txtTBTK_CNC1.Clear();
                txtTBTK_Sodu1.Clear();
                txtTBTK_Ngaybaomat1.Clear();
                TBTK_NGAY_TIM_THAY1 = "";

                btnTBTK_Them2.Text = "Thêm";
                txtTBTK_STK2.Clear();
                txtTBTK_Soso2.Clear();
                txtTBTK_Ngaygui2.Clear();
                txtTBTK_DVC2.Clear();
                txtTBTK_CNC2.Clear();
                txtTBTK_Sodu2.Clear();
                txtTBTK_Ngaybaomat2.Clear();
                TBTK_NGAY_TIM_THAY2 = "";

                btnTBTK_Them3.Text = "Thêm";
                txtTBTK_STK3.Clear();
                txtTBTK_Soso3.Clear();
                txtTBTK_Ngaygui3.Clear();
                txtTBTK_DVC3.Clear();
                txtTBTK_CNC3.Clear();
                txtTBTK_Sodu3.Clear();
                txtTBTK_Ngaybaomat3.Clear();
                TBTK_NGAY_TIM_THAY3 = "";

                btnTBTK_Them4.Text = "Thêm";
                txtTBTK_STK4.Clear();
                txtTBTK_Soso4.Clear();
                txtTBTK_Ngaygui4.Clear();
                txtTBTK_DVC4.Clear();
                txtTBTK_CNC4.Clear();
                txtTBTK_Sodu4.Clear();
                txtTBTK_Ngaybaomat4.Clear();
                TBTK_NGAY_TIM_THAY4 = "";

                btnTBTK_Them5.Text = "Thêm";
                txtTBTK_STK5.Clear();
                txtTBTK_Soso5.Clear();
                txtTBTK_Ngaygui5.Clear();
                txtTBTK_DVC5.Clear();
                txtTBTK_CNC5.Clear();
                txtTBTK_Sodu5.Clear();
                txtTBTK_Ngaybaomat5.Clear();
                TBTK_NGAY_TIM_THAY5 = "";

                txtTBTK_So_tb_mat_cn1.Clear();
                txtTBTK_Ngay_tb_mat_cn1.Clear();
                txtTBTK_So_tb_thay_cn1.Clear();
                txtTBTK_Ngay_tb_thay_cn1.Clear();
                txtTBTK_So_tb_mat_cn2.Clear();
                txtTBTK_Ngay_tb_mat_cn2.Clear();
                txtTBTK_So_tb_thay_cn2.Clear();
                txtTBTK_Ngay_tb_thay_cn2.Clear();
            }
            else
            {
                MessageBox.Show("Không tìm thấy sổ tiết kiệm đã báo mất nào trong hệ thống vào ngày " + txtTimNgaybaomat.Text + ". Có thể sử dụng chức năng NHẬP TK TIẾT KIỆM TỪ FILE để tạo thông báo.");
                dgvDanhsachTK.DataSource = null;
                return;
            }      
        }

        private void btnTimMAKH_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    new DataColumn("Số TK", typeof(string)),
                    new DataColumn("Số sổ", typeof(string)),
                    new DataColumn("Số dư", typeof(string)),
                    new DataColumn("Ngày báo mất-hỏng", typeof(string)),
                    new DataColumn("Đơn vị cấp", typeof(string)),
                    new DataColumn("Chi nhánh cấp", typeof(string)),
                    new DataColumn("Tình trạng", typeof(string)),
                    new DataColumn("Ngày cấp sổ", typeof(string)), 
                    new DataColumn("Loại tiền tệ", typeof(string)),
                    new DataColumn("Kỳ hạn", typeof(ushort)),
                    new DataColumn("Số thông báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày tìm thấy", typeof(string)),
                    new DataColumn("Hình thức xử lý", typeof(string))
                }
            );
            DataRow dr_stk;
            System.Data.DataTable dt_temp = tbtk_bus.STK_MAT_THEO_MA_KH(txtTimMAKH.Text);
            if (dt_temp.Rows.Count > 0)
            {

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    dr_stk = dt_stk.NewRow();
                    dr_stk["Số TK"] = dt_temp.Rows[i]["TK"].ToString();
                    dr_stk["Số sổ"] = dt_temp.Rows[i]["SERIAL"].ToString();
                    dr_stk["Số dư"] = Convert.ToDecimal(dt_temp.Rows[i]["SO_DU"].ToString()).ToString("#,#.##", System.Globalization.CultureInfo.InvariantCulture);
                    dr_stk["Ngày báo mất-hỏng"] = dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(6, 4);
                    dr_stk["Đơn vị cấp"] = dt_temp.Rows[i]["DV_CAP_STK"].ToString();
                    dr_stk["Chi nhánh cấp"] = dt_temp.Rows[i]["CN_CAP_STK"].ToString();
                    dr_stk["Tình trạng"] = dt_temp.Rows[i]["TINH_TRANG"].ToString();
                    dr_stk["Ngày cấp sổ"] = dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(6, 4);
                    dr_stk["Loại tiền tệ"] = "";
                    dr_stk["Kỳ hạn"] = 0;
                    dr_stk["Số thông báo mất CN Loại 2"] = dt_temp.Rows[i]["SO_TB_MAT_CN_LOAI2"].ToString();
                    dr_stk["Ngày báo mất CN Loại 2"] = dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(6, 4);
                    dr_stk["Số thông báo thấy CN Loại 2"] = dt_temp.Rows[i]["SO_TB_THAY_CN_LOAI2"].ToString();
                    dr_stk["Ngày báo thấy CN Loại 2"] = dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(6, 4);
                    dr_stk["Số thông báo mất CN Loại 1"] = dt_temp.Rows[i]["SO_TB_MAT_CN_LOAI1"].ToString();
                    dr_stk["Ngày báo mất CN Loại 1"] = dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(6, 4);
                    dr_stk["Số thông báo thấy CN Loại 1"] = dt_temp.Rows[i]["SO_TB_THAY_CN_LOAI1"].ToString();
                    dr_stk["Ngày báo thấy CN Loại 1"] = dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(6, 4);
                    dr_stk["Ngày tìm thấy"] = dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(6, 4);
                    dr_stk["Hình thức xử lý"] = dt_temp.Rows[i]["XU_LY"].ToString();
                    dt_stk.Rows.Add(dr_stk);
                }

                //Gán table dt_stk vào datagridview
                dgvDanhsachTK.DataSource = null;
                dgvDanhsachTK.DataSource = dt_stk;
                dgvDanhsachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số TK
                dgvDanhsachTK.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số sổ
                dgvDanhsachTK.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số dư
                dgvDanhsachTK.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Ngày báo mất-hỏng
                dgvDanhsachTK.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Đơn vị cấp
                dgvDanhsachTK.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Chi nhánh cấp
                dgvDanhsachTK.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Tình trạng
                dgvDanhsachTK.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày cấp sổ
                dgvDanhsachTK.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Loại tiền tệ
                dgvDanhsachTK.Columns[8].Visible = false;
                dgvDanhsachTK.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Kỳ hạn
                dgvDanhsachTK.Columns[9].Visible = false;
                dgvDanhsachTK.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 2
                dgvDanhsachTK.Columns[10].Visible = false;
                dgvDanhsachTK.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 2
                dgvDanhsachTK.Columns[11].Visible = false;
                dgvDanhsachTK.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 2
                dgvDanhsachTK.Columns[12].Visible = false;
                dgvDanhsachTK.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 2
                dgvDanhsachTK.Columns[13].Visible = false;
                dgvDanhsachTK.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 1
                dgvDanhsachTK.Columns[14].Visible = false;
                dgvDanhsachTK.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 1
                dgvDanhsachTK.Columns[15].Visible = false;
                dgvDanhsachTK.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 1
                dgvDanhsachTK.Columns[16].Visible = false;
                dgvDanhsachTK.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 1
                dgvDanhsachTK.Columns[17].Visible = false;
                dgvDanhsachTK.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày tìm thấy
                dgvDanhsachTK.Columns[18].Visible = false;
                dgvDanhsachTK.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Hình thức xử lý
                dgvDanhsachTK.Columns[19].Visible = false; 
                Cursor.Current = Cursors.Default;

                //Xóa thông tin sổ tiết kiệm cũ
                btnTBTK_Them1.Text = "Thêm";
                txtTBTK_STK1.Clear();
                txtTBTK_Soso1.Clear();
                txtTBTK_Ngaygui1.Clear();
                txtTBTK_DVC1.Clear();
                txtTBTK_CNC1.Clear();
                txtTBTK_Sodu1.Clear();
                txtTBTK_Ngaybaomat1.Clear();
                TBTK_NGAY_TIM_THAY1 = "";

                btnTBTK_Them2.Text = "Thêm";
                txtTBTK_STK2.Clear();
                txtTBTK_Soso2.Clear();
                txtTBTK_Ngaygui2.Clear();
                txtTBTK_DVC2.Clear();
                txtTBTK_CNC2.Clear();
                txtTBTK_Sodu2.Clear();
                txtTBTK_Ngaybaomat2.Clear();
                TBTK_NGAY_TIM_THAY2 = "";

                btnTBTK_Them3.Text = "Thêm";
                txtTBTK_STK3.Clear();
                txtTBTK_Soso3.Clear();
                txtTBTK_Ngaygui3.Clear();
                txtTBTK_DVC3.Clear();
                txtTBTK_CNC3.Clear();
                txtTBTK_Sodu3.Clear();
                txtTBTK_Ngaybaomat3.Clear();
                TBTK_NGAY_TIM_THAY3 = "";

                btnTBTK_Them4.Text = "Thêm";
                txtTBTK_STK4.Clear();
                txtTBTK_Soso4.Clear();
                txtTBTK_Ngaygui4.Clear();
                txtTBTK_DVC4.Clear();
                txtTBTK_CNC4.Clear();
                txtTBTK_Sodu4.Clear();
                txtTBTK_Ngaybaomat4.Clear();
                TBTK_NGAY_TIM_THAY4 = "";

                btnTBTK_Them5.Text = "Thêm";
                txtTBTK_STK5.Clear();
                txtTBTK_Soso5.Clear();
                txtTBTK_Ngaygui5.Clear();
                txtTBTK_DVC5.Clear();
                txtTBTK_CNC5.Clear();
                txtTBTK_Sodu5.Clear();
                txtTBTK_Ngaybaomat5.Clear();
                TBTK_NGAY_TIM_THAY5 = "";

                txtTBTK_So_tb_mat_cn1.Clear();
                txtTBTK_Ngay_tb_mat_cn1.Clear();
                txtTBTK_So_tb_thay_cn1.Clear();
                txtTBTK_Ngay_tb_thay_cn1.Clear();
                txtTBTK_So_tb_mat_cn2.Clear();
                txtTBTK_Ngay_tb_mat_cn2.Clear();
                txtTBTK_So_tb_thay_cn2.Clear();
                txtTBTK_Ngay_tb_thay_cn2.Clear();
            }
            else
            {
                MessageBox.Show("Không tìm thấy sổ tiết kiệm đã báo mất của khách hàng có mã khách hàng" + txtTimMAKH.Text + ". Có thể sử dụng chức năng NHẬP TK TIẾT KIỆM TỪ FILE để tạo thông báo.");
                dgvDanhsachTK.DataSource = null;
                return;
            }
        }

        private void btnTimSTK_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt_stk = new System.Data.DataTable();
            dt_stk.Columns.AddRange
            (
                new DataColumn[20] 
                { 
                    new DataColumn("Số TK", typeof(string)),
                    new DataColumn("Số sổ", typeof(string)),
                    new DataColumn("Số dư", typeof(string)),
                    new DataColumn("Ngày báo mất-hỏng", typeof(string)),
                    new DataColumn("Đơn vị cấp", typeof(string)),
                    new DataColumn("Chi nhánh cấp", typeof(string)),
                    new DataColumn("Tình trạng", typeof(string)),
                    new DataColumn("Ngày cấp sổ", typeof(string)), 
                    new DataColumn("Loại tiền tệ", typeof(string)),
                    new DataColumn("Kỳ hạn", typeof(ushort)),
                    new DataColumn("Số thông báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 2", typeof(string)),
                    new DataColumn("Số thông báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo mất CN Loại 1", typeof(string)),
                    new DataColumn("Số thông báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày báo thấy CN Loại 1", typeof(string)),
                    new DataColumn("Ngày tìm thấy", typeof(string)),
                    new DataColumn("Hình thức xử lý", typeof(string))
                }
            );
            DataRow dr_stk;
            System.Data.DataTable dt_temp = tbtk_bus.STK_MAT_THEO_SO_SO(txtTimSoso.Text);
            if (dt_temp.Rows.Count > 0)
            {

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    dr_stk = dt_stk.NewRow();
                    dr_stk["Số TK"] = dt_temp.Rows[i]["TK"].ToString();
                    dr_stk["Số sổ"] = dt_temp.Rows[i]["SERIAL"].ToString();
                    dr_stk["Số dư"] = Convert.ToDecimal(dt_temp.Rows[i]["SO_DU"].ToString()).ToString("#,#.##", System.Globalization.CultureInfo.InvariantCulture);
                    dr_stk["Ngày báo mất-hỏng"] = dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_KH_BAO"].ToString().Substring(6, 4);
                    dr_stk["Đơn vị cấp"] = dt_temp.Rows[i]["DV_CAP_STK"].ToString();
                    dr_stk["Chi nhánh cấp"] = dt_temp.Rows[i]["CN_CAP_STK"].ToString();
                    dr_stk["Tình trạng"] = dt_temp.Rows[i]["TINH_TRANG"].ToString();
                    dr_stk["Ngày cấp sổ"] = dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_CAP_SO"].ToString().Substring(6, 4);
                    dr_stk["Loại tiền tệ"] = "";
                    dr_stk["Kỳ hạn"] = 0;
                    dr_stk["Số thông báo mất CN Loại 2"] = dt_temp.Rows[i]["SO_TB_MAT_CN_LOAI2"].ToString();
                    dr_stk["Ngày báo mất CN Loại 2"] = dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI2"].ToString().Substring(6, 4);
                    dr_stk["Số thông báo thấy CN Loại 2"] = dt_temp.Rows[i]["SO_TB_THAY_CN_LOAI2"].ToString();
                    dr_stk["Ngày báo thấy CN Loại 2"] = dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI2"].ToString().Substring(6, 4);
                    dr_stk["Số thông báo mất CN Loại 1"] = dt_temp.Rows[i]["SO_TB_MAT_CN_LOAI1"].ToString();
                    dr_stk["Ngày báo mất CN Loại 1"] = dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_MAT_CN_LOAI1"].ToString().Substring(6, 4);
                    dr_stk["Số thông báo thấy CN Loại 1"] = dt_temp.Rows[i]["SO_TB_THAY_CN_LOAI1"].ToString();
                    dr_stk["Ngày báo thấy CN Loại 1"] = dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_BAO_THAY_CN_LOAI1"].ToString().Substring(6, 4);
                    dr_stk["Ngày tìm thấy"] = dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i]["NGAY_TIM_THAY"].ToString().Substring(6, 4);
                    dr_stk["Hình thức xử lý"] = dt_temp.Rows[i]["XU_LY"].ToString();
                    dt_stk.Rows.Add(dr_stk);
                }

                //Gán table dt_stk vào datagridview
                dgvDanhsachTK.DataSource = null;
                dgvDanhsachTK.DataSource = dt_stk;
                dgvDanhsachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachTK.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số TK
                dgvDanhsachTK.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số sổ
                dgvDanhsachTK.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Số dư
                dgvDanhsachTK.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Ngày báo mất-hỏng
                dgvDanhsachTK.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Đơn vị cấp
                dgvDanhsachTK.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Chi nhánh cấp
                dgvDanhsachTK.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Tình trạng
                dgvDanhsachTK.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày cấp sổ
                dgvDanhsachTK.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Loại tiền tệ
                dgvDanhsachTK.Columns[8].Visible = false;
                dgvDanhsachTK.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Kỳ hạn
                dgvDanhsachTK.Columns[9].Visible = false;
                dgvDanhsachTK.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 2
                dgvDanhsachTK.Columns[10].Visible = false;
                dgvDanhsachTK.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 2
                dgvDanhsachTK.Columns[11].Visible = false;
                dgvDanhsachTK.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 2
                dgvDanhsachTK.Columns[12].Visible = false;
                dgvDanhsachTK.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 2
                dgvDanhsachTK.Columns[13].Visible = false;
                dgvDanhsachTK.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo mất CN Loại 1
                dgvDanhsachTK.Columns[14].Visible = false;
                dgvDanhsachTK.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo mất CN Loại 1
                dgvDanhsachTK.Columns[15].Visible = false;
                dgvDanhsachTK.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Số thông báo thấy CN Loại 1
                dgvDanhsachTK.Columns[16].Visible = false;
                dgvDanhsachTK.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày báo thấy CN Loại 1
                dgvDanhsachTK.Columns[17].Visible = false;
                dgvDanhsachTK.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Ngày tìm thấy
                dgvDanhsachTK.Columns[18].Visible = false;
                dgvDanhsachTK.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//Hình thức xử lý
                dgvDanhsachTK.Columns[19].Visible = false;
                Cursor.Current = Cursors.Default;

                //Xóa thông tin sổ tiết kiệm cũ
                btnTBTK_Them1.Text = "Thêm";
                txtTBTK_STK1.Clear();
                txtTBTK_Soso1.Clear();
                txtTBTK_Ngaygui1.Clear();
                txtTBTK_DVC1.Clear();
                txtTBTK_CNC1.Clear();
                txtTBTK_Sodu1.Clear();
                txtTBTK_Ngaybaomat1.Clear();
                TBTK_NGAY_TIM_THAY1 = "";

                btnTBTK_Them2.Text = "Thêm";
                txtTBTK_STK2.Clear();
                txtTBTK_Soso2.Clear();
                txtTBTK_Ngaygui2.Clear();
                txtTBTK_DVC2.Clear();
                txtTBTK_CNC2.Clear();
                txtTBTK_Sodu2.Clear();
                txtTBTK_Ngaybaomat2.Clear();
                TBTK_NGAY_TIM_THAY2 = "";

                btnTBTK_Them3.Text = "Thêm";
                txtTBTK_STK3.Clear();
                txtTBTK_Soso3.Clear();
                txtTBTK_Ngaygui3.Clear();
                txtTBTK_DVC3.Clear();
                txtTBTK_CNC3.Clear();
                txtTBTK_Sodu3.Clear();
                txtTBTK_Ngaybaomat3.Clear();
                TBTK_NGAY_TIM_THAY3 = "";

                btnTBTK_Them4.Text = "Thêm";
                txtTBTK_STK4.Clear();
                txtTBTK_Soso4.Clear();
                txtTBTK_Ngaygui4.Clear();
                txtTBTK_DVC4.Clear();
                txtTBTK_CNC4.Clear();
                txtTBTK_Sodu4.Clear();
                txtTBTK_Ngaybaomat4.Clear();
                TBTK_NGAY_TIM_THAY4 = "";

                btnTBTK_Them5.Text = "Thêm";
                txtTBTK_STK5.Clear();
                txtTBTK_Soso5.Clear();
                txtTBTK_Ngaygui5.Clear();
                txtTBTK_DVC5.Clear();
                txtTBTK_CNC5.Clear();
                txtTBTK_Sodu5.Clear();
                txtTBTK_Ngaybaomat5.Clear();
                TBTK_NGAY_TIM_THAY5 = "";

                txtTBTK_So_tb_mat_cn1.Clear();
                txtTBTK_Ngay_tb_mat_cn1.Clear();
                txtTBTK_So_tb_thay_cn1.Clear();
                txtTBTK_Ngay_tb_thay_cn1.Clear();
                txtTBTK_So_tb_mat_cn2.Clear();
                txtTBTK_Ngay_tb_mat_cn2.Clear();
                txtTBTK_So_tb_thay_cn2.Clear();
                txtTBTK_Ngay_tb_thay_cn2.Clear();
            }
            else
            {
                MessageBox.Show("Không tìm thấy sổ tiết kiệm đã báo mất có số" + txtTimSoso.Text + ". Có thể sử dụng chức năng NHẬP TK TIẾT KIỆM TỪ FILE để tạo thông báo.");
                dgvDanhsachTK.DataSource = null;
                return;
            }
        }

        private void btnTBTK2_Them1_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK2_Them1.Text == "Thêm")
                {
                    btnTBTK2_Them1.Text = "Xóa";
                    txtTBTK2_STK1.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK2_Soso1.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK2_Sodu1.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK2_Ngaygui1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK2_DVC1.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK2_CNC1.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK2_Ngaybaohong1.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();
                    TBTK2_CNC = txtTBTK2_CNC1.Text;
                }
                else
                {
                    btnTBTK2_Them1.Text = "Thêm";
                    txtTBTK2_STK1.Clear();
                    txtTBTK2_Soso1.Clear();
                    txtTBTK2_Ngaygui1.Clear();
                    txtTBTK2_DVC1.Clear();
                    txtTBTK2_CNC1.Clear();
                    txtTBTK2_Sodu1.Clear();
                    txtTBTK2_Ngaybaohong1.Clear();
                }
            }     
        }

        private void btnTBTK2_Them2_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK2_Them2.Text == "Thêm")
                {
                    btnTBTK2_Them2.Text = "Xóa";
                    txtTBTK2_STK2.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK2_Soso2.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK2_Sodu2.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK2_Ngaygui2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK2_DVC2.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK2_CNC2.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK2_Ngaybaohong2.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();
                    TBTK2_CNC = txtTBTK2_CNC2.Text;
                }
                else
                {
                    btnTBTK2_Them2.Text = "Thêm";
                    txtTBTK2_STK2.Clear();
                    txtTBTK2_Soso2.Clear();
                    txtTBTK2_Ngaygui2.Clear();
                    txtTBTK2_DVC2.Clear();
                    txtTBTK2_CNC2.Clear();
                    txtTBTK2_Sodu2.Clear();
                    txtTBTK2_Ngaybaohong2.Clear();
                }
            }  
        }

        private void btnTBTK2_Them3_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK2_Them3.Text == "Thêm")
                {
                    btnTBTK2_Them3.Text = "Xóa";
                    txtTBTK2_STK3.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK2_Soso3.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK2_Sodu3.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK2_Ngaygui3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK2_DVC3.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK2_CNC3.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK2_Ngaybaohong3.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();
                    TBTK2_CNC = txtTBTK2_CNC3.Text;
                }
                else
                {
                    btnTBTK2_Them3.Text = "Thêm";
                    txtTBTK2_STK3.Clear();
                    txtTBTK2_Soso3.Clear();
                    txtTBTK2_Ngaygui3.Clear();
                    txtTBTK2_DVC3.Clear();
                    txtTBTK2_CNC3.Clear();
                    txtTBTK2_Sodu3.Clear();
                    txtTBTK2_Ngaybaohong3.Clear();
                }
            }  
        }

        private void btnTBTK2_Them4_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK2_Them4.Text == "Thêm")
                {
                    btnTBTK2_Them4.Text = "Xóa";
                    txtTBTK2_STK4.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK2_Soso4.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK2_Sodu4.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK2_Ngaygui4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK2_DVC4.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK2_CNC4.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK2_Ngaybaohong4.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();
                    TBTK2_CNC = txtTBTK2_CNC4.Text;
                }
                else
                {
                    btnTBTK2_Them4.Text = "Thêm";
                    txtTBTK2_STK4.Clear();
                    txtTBTK2_Soso4.Clear();
                    txtTBTK2_Ngaygui4.Clear();
                    txtTBTK2_DVC4.Clear();
                    txtTBTK2_CNC4.Clear();
                    txtTBTK2_Sodu4.Clear();
                    txtTBTK2_Ngaybaohong4.Clear();
                }
            }  
        }

        private void btnTBTK2_Them5_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachTK.SelectedCells.Count > 0)
            {
                if (btnTBTK2_Them5.Text == "Thêm")
                {
                    btnTBTK2_Them5.Text = "Xóa";
                    txtTBTK2_STK5.Text = dgvDanhsachTK.CurrentRow.Cells["Số TK"].Value.ToString();
                    txtTBTK2_Soso5.Text = dgvDanhsachTK.CurrentRow.Cells["Số sổ"].Value.ToString();
                    txtTBTK2_Sodu5.Text = dgvDanhsachTK.CurrentRow.Cells["Số dư"].Value.ToString();
                    txtTBTK2_Ngaygui5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày cấp sổ"].Value.ToString();
                    txtTBTK2_DVC5.Text = dgvDanhsachTK.CurrentRow.Cells["Đơn vị cấp"].Value.ToString();
                    txtTBTK2_CNC5.Text = dgvDanhsachTK.CurrentRow.Cells["Chi nhánh cấp"].Value.ToString();
                    txtTBTK2_Ngaybaohong5.Text = dgvDanhsachTK.CurrentRow.Cells["Ngày báo mất-hỏng"].Value.ToString();
                    TBTK2_CNC = txtTBTK2_CNC5.Text;
                }
                else
                {
                    btnTBTK2_Them5.Text = "Thêm";
                    txtTBTK2_STK5.Clear();
                    txtTBTK2_Soso5.Clear();
                    txtTBTK2_Ngaygui5.Clear();
                    txtTBTK2_DVC5.Clear();
                    txtTBTK2_CNC5.Clear();
                    txtTBTK2_Sodu5.Clear();
                    txtTBTK2_Ngaybaohong5.Clear();
                }
            }  
        }
    }
}