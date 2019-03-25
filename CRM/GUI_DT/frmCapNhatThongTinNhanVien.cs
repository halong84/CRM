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

namespace CRM.GUI_DT
{
    public partial class frmCapNhatThongTinNhanVien : Form
    {
        public frmCapNhatThongTinNhanVien()
        {
            InitializeComponent();
            try
            {
                DataTable tbInfo = DungChungDAL.GET_THONGTIN_NHANVIEN(Thongtindangnhap.manv);
                DataRow rInfo = tbInfo.Rows[0];
                dtpNgaySinh.Value = (DateTime)rInfo["NGAYSINH"];
                txtNoiSinh.Text = rInfo["NOISINH"].ToString();
                txtDiaChi.Text = rInfo["DIACHI"].ToString();
                txtCMND.Text = rInfo["CMND"].ToString();
                dtpNgayCap.Value = (DateTime)rInfo["NGAYCAP"];

                cboxNoiCap.DataSource = DungChungDAL.GET_ALL_NOICAP_CMND();
                cboxNoiCap.DisplayMember = "NOICAP";
                cboxNoiCap.ValueMember = "MA_NOICAP";
                cboxNoiCap.SelectedValue = rInfo["NOICAP"];
                txtSDTNhaRieng.Text = rInfo["SDTNHARIENG"].ToString();
                txtDiDong.Text = rInfo["DIDONG"].ToString();
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
                dtpNgaySinh.Value,
                txtNoiSinh.Text,
                txtDiaChi.Text,
                txtCMND.Text,
                dtpNgayCap.Value,
                cboxNoiCap.SelectedValue.ToString(),
                txtSDTNhaRieng.Text,
                txtDiDong.Text
                );
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK);
                Close();
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng ngày/tháng/năm (dd/MM/yyyy)", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
