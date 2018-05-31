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

namespace CRM.GUI_DV
{
    public partial class frmDangKyDichVu : Form
    {
        KhachHangDV kh;

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

        void KhoiTao()
        {
            listNguon.Clear();
            listDich.Clear();

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

            listDich.Add("<SODIENTHOAI>");
            listNguon.Add(txtSoDienThoai.Text);

            listDich.Add("<EMAIL>");
            listNguon.Add(txtEmail.Text);

            listDich.Add("<QUOCTICH>");
            listNguon.Add(txtQuocTich.Text);

            listDich.Add("<DIACHI>");
            listNguon.Add(txtDiaChi.Text);

            //E-MB 01
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

            listDich.Add("<KHCN_EMB_1>");
            if(kh.loaiKH == 1) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            listDich.Add("<KHDN_EMB_1>");
            if (kh.loaiKH == 2) listNguon.Add(((char)0x2611).ToString());
            else listNguon.Add(((char)0x2610).ToString());

            //E-MB 02

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

        


    }
}
