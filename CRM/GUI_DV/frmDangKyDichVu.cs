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
        string tenFileIB03 = "IB_03_BIEN_BAN_BAN_GIAO_NHAN_THIET_BI_XAC_THUC";
        string tenFileIB04 = "IB_04_DANG_KY_KICH_HOAT_PHUONG_THUC_BAO_MAT";

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

            //Get KSV
            if (Thong_tin_dang_nhap.hs)
                try
                {
                    DataTable dtTP = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb, "Trưởng phòng");
                    DataTable dtPP = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb, "Phó phòng");

                    dtTP.Merge(dtPP);

                    cbKSV.DataSource = dtTP;
                    cbKSV.DisplayMember = "HOTEN";
                    cbKSV.ValueMember = "MANV";

                    if (cbKSV.Items.Count > 0)
                        cbKSV.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    ErrorMessageDAL.DataAccessError(ex);
                }
            else
                try
                {
                    DataTable dtTP = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb, "Giám đốc");
                    DataTable dtPP = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb, "Phó Giám đốc");
                    dtTP.Merge(dtPP);

                    cbKSV.DataSource = dtTP;
                    cbKSV.DisplayMember = "HOTEN";
                    cbKSV.ValueMember = "MANV";

                    if (cbKSV.Items.Count > 0)
                        cbKSV.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    ErrorMessageDAL.DataAccessError(ex);
                }

            //Get Leader
            try
            {
                if (!Thong_tin_dang_nhap.hs)
                {
                    DataTable dtGD = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb, "Giám đốc");
                    DataTable dtPGD = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb, "Phó Giám đốc");
                    dtGD.Merge(dtPGD);
                    cbLanhDao.DataSource = dtGD;
                    cbLanhDao.DisplayMember = "HOTEN";
                    cbLanhDao.ValueMember = "MANV";

                    if (cbLanhDao.Items.Count > 0)
                        cbLanhDao.SelectedIndex = 0;
                }
                else
                {
                    DataTable dtGD = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb.Substring(0,4)+"-01", "Giám đốc");
                    DataTable dtPGD = PhatHanhTheGhiNoDAL.DANH_SACH_NV_THEO_PB_CV(Thong_tin_dang_nhap.ma_pb.Substring(0,4)+"-01", "Phó Giám đốc");
                    dtGD.Merge(dtPGD);
                    cbLanhDao.DataSource = dtGD;
                    cbLanhDao.DisplayMember = "HOTEN";
                    cbLanhDao.ValueMember = "MANV";

                    if (cbLanhDao.Items.Count > 0)
                        cbLanhDao.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
            }
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
                catch (Exception ex)
                {
                    ErrorMessageDAL.DataAccessError(ex);
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
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
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
            listDich.Add("<NGUOIDAIDIEN_EMB_2>");
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

            listDich.Add("<CHUCVU_EMB_3>");
            listNguon.Add(txtChucVu_EMB_3.Text);

            listDich.Add("<NOIDUNG_EMB_3>");
            listNguon.Add(txtNoiDung_EMB_3.Text);

            listDich.Add("<NGAYGIOGD_EMB_3>");
            listNguon.Add(string.Format("{0} giờ {1} phút, ngày {2} tháng {3} năm {4}.",
                dtpNgayGioGD_EMB_3.Value.Hour,
                dtpNgayGioGD_EMB_3.Value.Minute,
                dtpNgayGioGD_EMB_3.Value.Day,
                dtpNgayGioGD_EMB_3.Value.Month,
                dtpNgayGioGD_EMB_3.Value.Year));

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

            listDich.Add("<HUY_SMS_2>");
            if (ckbHuy_SMS_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<THAYDOI_SMS_2>");
            if (ckbThayDoi_SMS_2.Checked) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());
        }


        void KhoiTaoIB01()
        {
            listDich.Add("<MST_IB_1>");
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

            listDich.Add("<SDT_OTP_IB_1>");
            listNguon.Add(txtSDTNhanOTP_IB_1.Text);

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

        void KhoiTaoIB03()
        {
            listDich.Add("<IB_03_SERIAL>");
            listNguon.Add(txtIB_03_Serial.Text);

            listDich.Add("<IB_03_TEN_DANG_NHAP>");
            listNguon.Add(txtIB_03_TenDangNhap.Text);

            listDich.Add("<IB_03_DV_DANG_KY>");
            listNguon.Add(txtIB_03_DichVuDangKy.Text);
        }

        void KhoiTaoChung()
        {
            //Thong tin chung
            string cn = Thong_tin_dang_nhap.ten_cn;
            if (!Thong_tin_dang_nhap.hs){
                cn = Thong_tin_dang_nhap.tenPb;
                listDich.Add("<CHINHANH0>");
                listNguon.Add((Thong_tin_dang_nhap.ten_cn+"\n"+cn).ToUpper());
            }
            else
            {
                listDich.Add("<CHINHANH0>");
                listNguon.Add(cn.ToUpper());
            }

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

            listDich.Add("<DIA_BAN>");
            if (Thong_tin_dang_nhap.ma_cn == "2300" || Thong_tin_dang_nhap.ma_cn == "2301" || Thong_tin_dang_nhap.ma_cn == "2313")
            {
                listNguon.Add("Hải Dương");
            }
            else
            {
                listNguon.Add(Thong_tin_dang_nhap.ten_cn.Substring(25));
            }

            listDich.Add("<DIA_CHI_PB>");
            listNguon.Add(Thong_tin_dang_nhap.diaChiPb);

            listDich.Add("<GDV>");
            listNguon.Add(Thong_tin_dang_nhap.ho_ten);

            listDich.Add("<KSV>");
            listNguon.Add(cbKSV.SelectedItem.ToString());

            listDich.Add("<LANH_DAO>");
            listNguon.Add(cbLanhDao.Text);

            listDich.Add("<CMND_LD>");
            cbLanhDao.ValueMember = "CMND";
            listNguon.Add(cbLanhDao.SelectedValue.ToString());

            listDich.Add("<NOICAP_LD>");
            cbLanhDao.ValueMember = "NOICAP";
            listNguon.Add(PhatHanhTheGhiNoDAL.DV_GET_NOICAPCMND(cbLanhDao.SelectedValue.ToString()));

            listDich.Add("<NGAYCAP_LD>");
            cbLanhDao.ValueMember = "NGAYCAP";
            DateTime dt = Convert.ToDateTime(cbLanhDao.SelectedValue.ToString());
            listNguon.Add(dt.ToString("dd/MM/yyyy"));

            listDich.Add("<CHUCVU_LD>");
            cbLanhDao.ValueMember = "CHUCVU";
            listNguon.Add(cbLanhDao.SelectedValue.ToString());
        }

        void KhoiTao(){

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
                case 8: 
                    KhoiTaoIB03();
                    break;
                default: break;
            }
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
                case 8:
                    fileName = tenFileIB03;
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


        void PutStringIntoTable(Microsoft.Office.Interop.Word.Document doc)
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Table tb = doc.Tables[2];

            int index = 0;

        }

        #endregion

        #region Other Procedures
        private void layTTKH(System.Data.DataTable dt_temp)
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
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thong_tin_dang_nhap.ma_cn)
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
            dt_temp2 = CommonMethods.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (PhatHanhTheGhiNoDAL.UPDATE_KHACHHANG(dt_temp2, Thong_tin_dang_nhap.ten_dang_nhap))
            {
                MessageBox.Show("Hoàn thành nhập thông tin khách hàng.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng.");
            }
        }

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
            if (((sender as ComboBox).Text.IndexOf('.') > -1))
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

        private void btnLayTTKH_Click(object sender, EventArgs e)
        {
            if (openFileTTKH.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileTTKH.FileName;
                DataTable dt = CommonMethods.read_excel(fileName);
                if (dt.Rows.Count == 0 || dt == null)
                {
                    MessageBox.Show("File không có dữ liệu");
                    return;
                }
                if (dt.Rows[0][7].ToString() == "Cá nhân")
                {
                    layTTKH(dt);
                }
                txtTimKiem.Focus();
            }
        }

        private void btnTaoMauBieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Chưa có thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            KhoiTao();
            CreateFile();
        }
        #endregion

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void label72_Click(object sender, EventArgs e)
        {

        }

        

       
       
    }
}
