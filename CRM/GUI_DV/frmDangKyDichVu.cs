using CRM.DAL.DV;
using CRM.Entities.DV;
using CRM.Utilities.DV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop;

namespace CRM.GUI_DV
{
    public partial class frmDangKyDichVu : Form
    {
        KhachHangDV kh;

        string tenFileEMB01 = "EMB_01_DANG_KY";
        string tenFileEMB02 = "EMB_02_THAY_DOI";
        string tenFileEMB03 = "EMB_03_XAC_MINH_GD";
        string tenFileEMB04 = "EMB_04_MAT_KHAU";
        string tenFileSMS01 = "SMS_01_DANG_KY";
        string tenFileSMS02 = "SMS_02_THAY_DOI";
        string tenFileIB01 = "IB_01_DANG_KY";
        string tenFileIB02 = "IB_02_THAY_DOI";

        List<string> listNguon, listDich;

        public frmDangKyDichVu()
        {
            InitializeComponent();
            listDich = new List<string>();
            listNguon = new List<string>();
            cbChonMauBieu.SelectedIndex = 0;
            btnTimKiem.Focus();
            //Place holder Textbox 
            txtTimKiem.Text = "CMND/CCCD/MAKH/SDT/GPKD";
            txtTimKiem.ForeColor = Color.Gray;
            txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Italic);
        }


        #region Tim kiem khach hang
        void TimKiemKH()
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text))
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!", "Thông báo", MessageBoxButtons.OK);
            else
            {
                try
                {
                    //Dat ham tim kiem
                    kh = DangKyDichVuDAL.DV_DANGKYDICHVU_KHACHHANG(txtTimKiem.Text);
                    if (kh == null)
                    {
                        KhongTimThayKH();
                    }
                    else
                    {
                        TimThayKH(kh);
                    }
                }
                catch
                {
                    ErrorMessageDAL.DataAccessError();
                }
            }
        }


        void KhongTimThayKH()
        {
            MessageBox.Show(@"Không tìm thấy khách hàng!", "Thông báo", MessageBoxButtons.OK);
            //SetTextBoxStatus_TTKH(true);
            ClearAllTextBox();
        }

        void TimThayKH(KhachHangDV kh)
        {
            cbSoTK.Items.Clear();
            //Thong tin chung
            txtNgayCap.Text = kh.ngay_cap.ToString("dd/MM/yyyy");
            txtCMND.Text = kh.cmt;
            txtNoiCap.Text = PhatHanhTheGhiNoDAL.DV_GET_NOICAPCMND(kh.noi_cap);
            txtMaKH.Text = kh.ma_KH;
            txtHoTen.Text = kh.ho_ten;
            txtNgaySinh.Text = kh.ngay_sinh.ToString("dd/MM/yyyy");
            txtSoDienThoai.Text = kh.dien_thoai;
            txtEmail.Text = kh.email;
            txtDiaChi.Text = kh.dia_chi;
            txtQuocTich.Text = "Việt Nam";
            txtGPKD.Text = kh.gpkd;

            if (kh.gioi_tinh)
            {
                cbGioiTinh.SelectedIndex = 0;
            }
            else
            {
                cbGioiTinh.SelectedIndex = 1;
            }

            //Lay cac so TK cua KH
            try
            {
                DataTable dt = PhatHanhTheGhiNoDAL.TimSoTK(kh.ma_KH);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string soTK = dt.Rows[i]["SOTK"].ToString();
                    char loaiTK = soTK[4];
                    if (loaiTK == '1' || loaiTK == '2')
                        cbSoTK.Items.Add(soTK);
                }
                if (cbSoTK.Items.Count > 0)
                {
                    cbSoTK.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Khách hàng chưa có số tài khoản!\nHãy điền vào số tài khoản!", "Thông báo", MessageBoxButtons.OK);
                    cbSoTK.Focus();
                }
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }

        }

        void ClearAllTextBox()
        {
            txtNgayCap.Text = "";
            txtNoiCap.Text = "";
            txtCMND.Text = "";
            txtHoTen.Text = "";
            txtQuocTich.Text = "";
            cbSoTK.SelectedItem = null;
            cbSoTK.Items.Clear();
            cbSoTK.Text = "";
            txtNgaySinh.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            cbGioiTinh.SelectedItem = null;
            txtSoDienThoai.Text = "";
        }
        #endregion

        #region Tao mau bieu
        void KhoiTaoEMB01()
        {
            listDich.Add("<NGUOIDAIDIEN_EMB_1>");
            listNguon.Add(txtNguoiDaiDien_EMB_1.Text);

            listDich.Add("<CHUCVU_EMB_1>");
            listNguon.Add(txtChucVu_EMB_1.Text);

            listDich.Add("<NGAYCAP_GPKD_EMB_1>");
            listNguon.Add(txtNgayCapGPKD_EMB_1.Text);

            listDich.Add("<NOICAP_GPKD_EMB_1>");
            listNguon.Add(txtNoiCapGPKD_EMB_1.Text);

            listDich.Add("<DTDD_EMB_1>");
            listNguon.Add(txtSDTSDDV_EMB_1.Text);
        }

        void KhoiTaoEMB02()
        {
            listDich.Add("<NGUOI_DAI_DIEN_EMB_2>");
            listNguon.Add(txtNguoiDaiDien_EMB_2.Text);

            listDich.Add("<CHUCVU_EMB_2>");
            listNguon.Add(txtChucVu_EMB_2.Text);

            listDich.Add("<NOICAP_GPKD_EMB_2>");
            listNguon.Add(txtNoiCapGPKD_EMB_2.Text);

            listDich.Add("<NGAYCAP_GPKD_EMB_2>");
            listNguon.Add(txtNgayCapGPKD_EMB_2.Text);

            listDich.Add("<HUY_EMB_2>");
            if (ckbHuy_EMB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<THAYDOI_EMB_2>");
            if (ckbThayDoi_EMB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<NOIDUNG_EMB_2>");
            listNguon.Add(txtNoiDung_EMB_2.Text);
        }

        void KhoiTaoEMB03()
        {
            listDich.Add("<NGUOIDAIDIEN_EMB_3>");
            listNguon.Add(txtNguoiDaiDien_EMB_3.Text);

            listDich.Add("<NOIDUNG_EMB_3>");
            listNguon.Add(txtNoiDung_EMB_3.Text);

            listDich.Add("<NGAYGIOGD_EMB_3>");
            listNguon.Add(dtpNgayGioGD_EMB_3.Value.ToString("hh:mm dd/MM/yyyy"));

            listDich.Add("<GIATRIGD_EMB_3>");
            listNguon.Add(txtGiaTriGD_EMB_3.Text);
        }

        void KhoiTaoEMB04()
        {
            listDich.Add("<DTDD_EMB_4>");
            listNguon.Add(txtDTDD_EMB_4.Text);

            listDich.Add("<LYDO_EMB_4>");
            listNguon.Add(txtLyDo_EMB_4.Text);
        }

        void KhoiTaoSMS01()
        {
            listDich.Add("<NGUOIDAIDIEN_SMS_1>");
            listNguon.Add(txtNguoiDaiDien_SMS_1.Text);

            listDich.Add("<CHUCVU_SMS_1>");
            listNguon.Add(txtChucVu_SMS_1.Text);

            listDich.Add("<NOICAP_GPKD_SMS_1>");
            listNguon.Add(txtNoiCapGPKD_SMS_1.Text);

            listDich.Add("<NGAYCAP_GPKD_SMS_1>");
            listNguon.Add(txtNgayCapGPKD_SMS_1.Text);

            listDich.Add("<DTDD_1_SMS_1>");
            listNguon.Add(txtSDTSDDV_1_SMS_1.Text);

            listDich.Add("<DTDD_2_SMS_1>");
            listNguon.Add(txtSDTSDDV_2_SMS_1.Text);

            listDich.Add("<DTDD_3_SMS_1>");
            listNguon.Add(txtSDTSDDV_3_SMS_1.Text);

            listDich.Add("<DTDD_4_SMS_1>");
            listNguon.Add(txtSDTSDDV_4_SMS_1.Text);

            listDich.Add("<DTDD_5_SMS_1>");
            listNguon.Add(txtSDTSDDV_5_SMS_1.Text);
        }

        void KhoiTaoSMS02()
        {
            listDich.Add("<NGUOIDAIDIEN_SMS_2>");
            listNguon.Add(txtNguoiDaiDien_SMS_2.Text);

            listDich.Add("<CHUCVU_SMS_2>");
            listNguon.Add(txtChucVu_SMS_2.Text);

            listDich.Add("<NOICAP_GPKD_SMS_2>");
            listNguon.Add(txtNoiCapGPKD_SMS_2.Text);

            listDich.Add("<NGAYCAP_GPKD_SMS_2>");
            listNguon.Add(txtNgayCapGPKD_SMS_2.Text);

            listDich.Add("<DTDD_1_SMS_2>");
            listNguon.Add(txtSDTSDDV_1_SMS_2.Text);

            listDich.Add("<DTDD_2_SMS_2>");
            listNguon.Add(txtSDTSDDV_2_SMS_2.Text);

            listDich.Add("<DTDD_3_SMS_2>");
            listNguon.Add(txtSDTSDDV_3_SMS_2.Text);

            listDich.Add("<DTDD_4_SMS_2>");
            listNguon.Add(txtSDTSDDV_4_SMS_2.Text);

            listDich.Add("<DTDD_5_SMS_2>");
            listNguon.Add(txtSDTSDDV_5_SMS_2.Text);

            listDich.Add("<SOTK_BOSUNG_SMS_2>");
            listNguon.Add(txtSoTK_BoSung_SMS_2.Text);

        }

        void KhoiTaoIB01()
        {
            listDich.Add("<MST_IB_01>");
            listNguon.Add(txtMST_IB_1.Text);

            listDich.Add("<DVTC_IB_1>");
            if (ckbDVTaiChinh_IB_1.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<DVTT_IB_1>");
            if (ckbDVThanhToan_IB_1.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<DVPTC_IB_1>");
            if (ckbDVPhiTaiChinh_IB_1.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<SMS_OTP_IB_1>");
            if (ckbOTP_SMS_IB_1.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<SOFT_OTP_IB_1>");
            if (ckbOTP_Soft_IB_1.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HARD_OTP_IB_1>");
            if (ckbOTP_Hard_IB_1.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<SOTK_MD_IB_1>");
            listNguon.Add(txtSTKMacDinh_IB_1.Text);

            listDich.Add("<SOTK_1_IB_1>");
            listNguon.Add(txtSTKSuDung_1_IB_1.Text);

            listDich.Add("<SOTK_2_IB_1>");
            listNguon.Add(txtSTKSuDung_2_IB_1.Text);

            listDich.Add("<SOTK_3_IB_1>");
            listNguon.Add(txtSTKSuDung_3_IB_1.Text);

            listDich.Add("<SOTK_4_IB_1>");
            listNguon.Add(txtSTKSuDung_4_IB_1.Text);

            listDich.Add("<SOTK_5_IB_1>");
            listNguon.Add(txtSTKSuDung_5_IB_1.Text);

            listDich.Add("<SOTK_6_IB_1>");
            listNguon.Add(txtSTKSuDung_6_IB_1.Text);

        }

        void KhoiTaoIB02()
        {
            listDich.Add("<MST_IB_2>");
            listNguon.Add(txtMST_IB_2.Text);

            listDich.Add("<CAPLAI_MK_IB_2>");
            if (ckbCapLaiMK_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<KHOA_TEN_DN_IB_2>");
            if (ckbKhoaTenDangNhap_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<MOKHOA_TEN_DN_IB_2>");
            if (ckbMoTenDangNhap_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<KHOA_THIETBI_IB_2>");
            if (ckbKhoaThietBi_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<MOKHOA_THIETBI_IB_2>");
            if (ckbMoKhoaThietBi_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<CAPLAI_THIETBI_IB_2>");
            if (ckbCapLaiThietBi_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HUY_DK_IB_2>");
            if (ckbHuyDangKy_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<SOTK_1_IB_2>");
            listNguon.Add(txtTaiKhoan_1_IB_2.Text);

            listDich.Add("<SOTK_2_IB_2>");
            listNguon.Add(txtTaiKhoan_2_IB_2.Text);

            listDich.Add("<SOTK_3_IB_2>");
            listNguon.Add(txtTaiKhoan_3_IB_2.Text);

            listDich.Add("<SOTK_4_IB_2>");
            listNguon.Add(txtTaiKhoan_4_IB_2.Text);

            listDich.Add("<BS_1_IB_2>");
            if (ckbBoSung_TK_1_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<BS_2_IB_2>");
            if (ckbBoSung_TK_2_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<BS_3_IB_2>");
            if (ckbBoSung_TK_3_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<BS_4_IB_2>");
            if (ckbBoSung_TK_4_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HB_1_IB_2>");
            if (ckbHuyBo_TK_1_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HB_2_IB_2>");
            if (ckbHuyBo_TK_2_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HB_3_IB_2>");
            if (ckbHuyBo_TK_3_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HB_4_IB_2>");
            if (ckbHuyBo_TK_4_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<BS_DVTC_IB_2>");
            if (ckbBoSung_TaiChinh_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<BS_DVTT_IB_2>");
            if (ckbBoSung_ThanhToan_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HB_DVTC_IB_2>");
            if (ckbHuyBo_TaiChinh_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<HB_DVTT_IB_2>");
            if (ckbHuyBo_ThanhToan_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<OTP_HARD_IB_2>");
            if (ckbOTP_Hard_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<OTP_SOFT_IB_2>");
            if (ckbOTP_Soft_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<OTP_SMS_IB_2>");
            if (ckbOTP_SMS_IB_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<SDT_OTP_IB_2>");
            listNguon.Add(txtSDTNhanOTP_IB_2.Text);
        }

        void KhoiTaoChung()
        {
            //Thong tin chung
            string cn = Thong_tin_dang_nhap.ten_cn;
            if (!Thong_tin_dang_nhap.hs) cn = Thong_tin_dang_nhap.tenPb;
            listDich.Add("<CHINHANH0>");
            listNguon.Add(cn.ToUpper());

            listDich.Add("<CHINHANH>");
            listNguon.Add(cn);

            listDich.Add("<HOTEN>");
            listNguon.Add(txtHoTen.Text);

            listDich.Add("<SOTAIKHOAN>");
            listNguon.Add(cbSoTK.Text);

            listDich.Add("<CMND>");
            listNguon.Add(txtCMND.Text);

            listDich.Add("<NGAYCAP>");
            listNguon.Add(txtNgayCap.Text);

            listDich.Add("<NOICAP>");
            listNguon.Add(txtNoiCap.Text);

            listDich.Add("<GPKD>");
            listNguon.Add(txtGPKD.Text);

            listDich.Add("<NGAYSINH>");
            listNguon.Add(txtNgaySinh.Text);

            listDich.Add("<GIOITINH>");
            if (kh.gioi_tinh) listNguon.Add("Nam");
            else listNguon.Add("Nữ");

            listDich.Add("<DIENTHOAI>");
            listNguon.Add(txtSoDienThoai.Text);

            listDich.Add("<EMAIL>");
            listNguon.Add(txtEmail.Text);

            listDich.Add("<QUOCTICH>");
            listNguon.Add(txtQuocTich.Text);

            listDich.Add("<DIACHI>");
            listNguon.Add(txtDiaChi.Text);

            listDich.Add("<NGAY>");
            listNguon.Add(DateTime.Today.ToString("dd/MM/yyyy"));

            listDich.Add("<KHCN>");
            if (kh.loaiKH == 1) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<KHDN>");
            if (kh.loaiKH == 2) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<NGAY_THANG_NAM>");
            listNguon.Add(string.Format("ngày {0} tháng {1} năm {2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year));
        }

        void KhoiTao()
        {
            listNguon.Clear();
            listDich.Clear();

            KhoiTaoChung();

            switch (cbChonMauBieu.SelectedIndex)
            {
                case 0:
                    KhoiTaoEMB01();
                    break;
                case 1:
                    KhoiTaoEMB02();
                    break;
                case 2:
                    KhoiTaoEMB03();
                    break;
                case 3:
                    KhoiTaoEMB04();
                    break;
                case 4:
                    KhoiTaoSMS01();
                    break;
                case 5:
                    KhoiTaoSMS02();
                    break;
                case 6:
                    KhoiTaoIB01();
                    break;
                case 7:
                    KhoiTaoIB02();
                    break;
                default: break;
            }
        }

        #endregion

        #region Other Procedures

        void OpenFileWord(string fileLocation)
        {
            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(fileLocation);
            ap.Visible = true;
        }
        public void TachSo(TextBox luong)
        {
            string txt, txt1;
            txt1 = luong.Text.Replace(",", "");
            txt = "";
            int n = txt1.Length;
            int dem = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                if (dem == 2 && i != 0)
                {
                    txt = "," + txt1.Substring(i, 1) + txt;
                    dem = 0;
                }
                else
                {
                    txt = txt1.Substring(i, 1) + txt;
                    dem += 1;
                }
            }
            luong.Text = txt;
            luong.SelectionStart = luong.Text.Length;
        }
        string XoaDauPhay(string s)
        {
            return s.Replace(",", "");
        }

        #endregion

        #region Event Handler
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "CMND/CCCD/MAKH/SDT/GPKD")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Regular);
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "CMND/CCCD/MAKH/SDT/GPKD";
                txtTimKiem.ForeColor = Color.Gray;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Italic);
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemKH();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                TimKiemKH();
        }

        private void cbSoTK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cbSoTK_Validated(object sender, EventArgs e)
        {
            foreach (var c in cbSoTK.Items)
            {
                if (c.ToString() == cbSoTK.Text) return;
            }
            cbSoTK.Items.Add(cbSoTK.Text);
            cbSoTK.SelectedIndex = cbSoTK.Items.Count - 1;
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cbChonMauBieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            tCtrlDangKyDV.SelectedIndex = cbChonMauBieu.SelectedIndex;
        }

        private void tCtrlDangKyDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbChonMauBieu.SelectedIndex = tCtrlDangKyDV.SelectedIndex;
        }

        #endregion

        private void btnTaoMauBieu_Click(object sender, EventArgs e)
        {
            KhoiTao();
            CreateFile();
            
        }

        void CreateFile()
        {
            string fileName = "";
            switch (cbChonMauBieu.SelectedIndex)
            {
                case 0:
                    fileName = tenFileEMB01;
                    break;
                case 1:
                    fileName = tenFileEMB02;
                    break;
                case 2:
                    fileName = tenFileEMB03;
                    break;
                case 3:
                    fileName = tenFileEMB04;
                    break;
                case 4:
                    fileName = tenFileSMS01;
                    break;
                case 5:
                    fileName = tenFileSMS02;
                    break;
                case 6:
                    fileName = tenFileIB01;
                    break;
                case 7:
                    fileName = tenFileIB02;
                    break;
                default: break;
            }
            saveFileDialog1.Filter = "Word Documents|*.docx";

            string subFolder = @"DangKyDichVu\";
            if (!CommonMethods.SubFolderExist(subFolder))
                CommonMethods.CreateSubFolder(subFolder);

            string TemplateFileLocation = CommonMethods.TemplateFileLocation(fileName + ".docx");
            string saveFileLocation = CommonMethods.SaveFileLocation(subFolder + fileName + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".docx");


            if (CommonMethods.CreateWordDocument(TemplateFileLocation, saveFileLocation, listDich, listNguon))
            {
                MessageBox.Show("File đã được tạo tại đường dẫn: " + saveFileLocation, "Tạo file thành công");
                OpenFileWord(saveFileLocation);
            }
        }
    }
}
