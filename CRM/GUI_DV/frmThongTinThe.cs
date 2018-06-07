using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRM.Entities.DV;
using CRM.DAL.DV;
using CRM.Utilities.DV;
using System.Globalization;

namespace CRM.GUI_DV
{
    public partial class frmThongTinThe : Form
    {
        private The the;
        public frmThongTinThe(int ID)
        {
            InitializeComponent();
            this.TopMost = true;
            try
            {
                DataRow row = ThongTinTheDAL.LayThongTinThe(ID);
                if (row != null) the = new The(row);
                txtHoTen.Text = the.hoTen;
                txtSoTK.Text = the.soTK;
                txtSoThe.Text = the.soThe;
                txtLoaiThe.Text = the.loaiThe;
                txtHangThe.Text = the.hangThe;
                txtHTPH.Text = the.hinhThucPhatHanh;
                txtHTNT.Text = the.hinhThucNhanThe;
                txtDTDD.Text = the.dtdd;
                txtUser.Text = the.userPhatHanh;

                DataRow rPb = ThongTinTheDAL.LayPhongBan(the.maPB);
                if (!(bool)rPb["HS"])
                    txtNoiPhatHanh.Text = rPb["TENPB"].ToString();
                else
                    txtNoiPhatHanh.Text = ThongTinTheDAL.LayTenChiNhanh(the.maPB);

                if (the.maPB != Thong_tin_dang_nhap.ma_pb)
                    btnLuu.Enabled = false;

                if (the.hmgd != 0)
                    txtHMGD.Text = the.hmgd.ToString();
                txtNgayDangKy.Text = the.ngayDK.ToString("dd/MM/yyyy");
                if (the.ngayNhan == DateTime.MinValue)
                    txtNgayNhanThe.Text = "";
                else
                    txtNgayNhanThe.Text = the.ngayNhan.ToString("dd/MM/yyyy");
                if (the.ngayGiao == DateTime.MinValue)
                    txtNgayGiaoThe.Text = "";
                else
                    txtNgayGiaoThe.Text = the.ngayGiao.ToString("dd/MM/yyyy");

                if (!string.IsNullOrEmpty(txtDTDD.Text))
                {
                    ckbSMS.Checked = true;
                }

                if (!string.IsNullOrEmpty(txtHMGD.Text))
                {
                    ckbInternet.Checked = true;
                }

                if (the.baoHiem) ckbBaoHiem.Checked = true;

                var txts = GetAll(this, typeof(TextBox));
                var txtms = GetAll(this, typeof(MaskedTextBox));

                foreach (var c in txts)
                {
                    if (!string.IsNullOrEmpty(c.Text))
                        c.Enabled = false;
                }

                foreach (var c in txtms)
                {
                    if (c.Text != "  /  /")
                    {
                        c.Enabled = false;
                    }
                }

                if (the.isPhatHanhMoi) cbPhatHanhMoi.Checked = true;
                else cbPhatHanhLai.Checked = true;
            }
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
                this.Close();
            }

        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void frmThongTinThe_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cRMDataSet.KHACHHANG' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'cRMDataSet.THEODOITHE' table. You can move, or remove it, as needed.

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void gbDichVu_Enter(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoThe.Text))
            {
                MessageBox.Show("Hãy nhập số thẻ!", "Thông báo", MessageBoxButtons.OK);
                txtSoThe.Focus();
                return;
            }
            else the.soThe = txtSoThe.Text;

            //Check ngay nhan
            if (CommonMethods.KiemTraNhapNgay(txtNgayNhanThe.Text))
                the.ngayNhan = DateTime.ParseExact(txtNgayNhanThe.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
            {
                if (txtNgayGiaoThe.Text != "  /  /")
                {
                    MessageBox.Show("Nhập sai định dạng ngày tháng!", "Thông báo", MessageBoxButtons.OK);
                   
                }
                else
                {
                    MessageBox.Show("Hãy nhập ngày nhận thẻ!", "Thông báo", MessageBoxButtons.OK);

                }
                txtNgayNhanThe.Focus();
                return;
               
            }

            //check ngay giao
            if (CommonMethods.KiemTraNhapNgay(txtNgayGiaoThe.Text)){
                the.ngayGiao = DateTime.ParseExact(txtNgayGiaoThe.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                try
                {
                    ThongTinTheDAL.GiaoThe(the);
                    this.Close();
                }
                catch (Exception ex)
                {
                    ErrorMessageDAL.DataAccessError(ex);
                }
            }
            else
            {
                if (txtNgayGiaoThe.Text != "  /  /")
                {
                    MessageBox.Show("Nhập sai định dạng ngày tháng!", "Thông báo", MessageBoxButtons.OK);
                    txtNgayGiaoThe.Focus();
                    return;
                }
                try
                {
                    ThongTinTheDAL.NhanThe(the);
                    this.Close();
                }
                catch (Exception ex)
                {
                    ErrorMessageDAL.DataAccessError(ex);
                }
            }
        }

        private void txtSoThe_TextChanged(object sender, EventArgs e)
        {
        }

        private void gbThongTinThe_Enter(object sender, EventArgs e)
        {

        }


    }
}
