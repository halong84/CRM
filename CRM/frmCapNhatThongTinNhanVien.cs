using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRM.DAL;
using CRM.Utilities;
using CRM.Entities;
using CRM.DAL.DV;

namespace CRM
{
    public partial class frmCapNhatThongTinNhanVien : Form
    {
        public frmCapNhatThongTinNhanVien()
        {
            InitializeComponent();
            try
            {
                cboxNoiCap.DataSource = DungChungDAL.GET_ALL_NOICAP_CMND();
                cboxNoiCap.DisplayMember = "NOICAP";
            }
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                DungChungDAL.UPDATE_NHANVIEN_DT(
                Thongtindangnhap.manv,
                DateTime.Parse(txtNgaySinh.Text),
                txtNoiSinh.Text,
                txtDiaChi.Text,
                txtCMND.Text,
                DateTime.Parse(txtNgayCap.Text),
                cboxNoiCap.Text,
                txtSDTNhaRieng.Text,
                txtDiDong.Text
                );
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK);
                Close();
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng ngày/tháng/năm", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
