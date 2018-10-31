using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRM.Utilities;
using CRM.DAL.DT;

namespace CRM.GUI_DT
{
    public partial class frmIPCAS : Form
    {
        public frmIPCAS()
        {
            InitializeComponent();
            GetUserInfo();
            GetKiemSoat();
            GetNoiLamViec();
            GetChucNang_Menu();
            cboxMauBieu.SelectedIndex = 0;


        }

        void GetUserInfo()
        {
            txtHoTen.Text = Thongtindangnhap.tennv;
            txtMaNV.Text = Thongtindangnhap.manv;
            txtChucVu.Text = Thongtindangnhap.chucvu;
            txtDonVi.Text = Thongtindangnhap.tencn;
            txtUserID.Text = Thongtindangnhap.user_id;
        }

        void GetKiemSoat()
        {
            try
            {
                DataTable ks = new DataTable();
                if (Thongtindangnhap.hs)
                {
                    var tp = IPCASDAL.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Trưởng phòng");
                    var pp = IPCASDAL.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Phó phòng");
                    tp.Merge(pp);
                    ks = tp;
                }
                else
                {
                    var gd = IPCASDAL.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Giám đốc");
                    var pgd = IPCASDAL.DANH_SACH_NV_THEO_PB_CV(Thongtindangnhap.mapb, "Phó Giám đốc");
                    gd.Merge(pgd);
                    ks = gd;
                }

                for (int i = 0; i < ks.Rows.Count; i++)
                {
                    cboxKiemSoat.Items.Add(ks.Rows[i]["HOTEN"].ToString());
                }
            }
            catch (Exception ex)
            {
                CRM.DAL.DV.ErrorMessageDAL.DataAccessError(ex);
            }

            if (cboxKiemSoat.Items.Count > 0)
                cboxKiemSoat.SelectedIndex = 0;
        }

        void GetChucNang_Menu()
        {
            try
            {
                var cn = IPCASDAL.DT_GET_CHUCNANG_IPCAS();
                var menu = IPCASDAL.DT_GET_MENU_IPCAS();
                for (int i = 0; i < cn.Rows.Count; i++)
                {
                    cboxChucNangHienTai_03.Items.Add(cn.Rows[i][0].ToString());
                    cboxChucNangThayDoi_03.Items.Add(cn.Rows[i][0].ToString());
                }

                for (int i = 0; i < menu.Rows.Count; i++)
                {
                    cboxMenuHienTai_03.Items.Add(menu.Rows[i][0].ToString());
                    cboxMenuThayDoi_03.Items.Add(menu.Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                CRM.DAL.DV.ErrorMessageDAL.DataAccessError(ex);
            }
            if (cboxChucNangHienTai_03.Items.Count > 0)
                cboxChucNangHienTai_03.SelectedIndex = 0;
            if (cboxChucNangThayDoi_03.Items.Count > 0)
                cboxChucNangThayDoi_03.SelectedIndex = 0;
            if (cboxMenuThayDoi_03.Items.Count > 0)
                cboxMenuThayDoi_03.SelectedIndex = 0;
            if (cboxMenuHienTai_03.Items.Count > 0)
                cboxMenuHienTai_03.SelectedIndex = 0;
        }

        void GetNoiLamViec()
        {
            try
            {
                var pb = IPCASDAL.DANHSACH_PB(Thongtindangnhap.macn);
                //cboxNoiLamViecHienTai.DataSource = pb;
                //cboxNoiLamViecHienTai.DisplayMember = "TENPB";
                //cboxNoiLamViecHienTai.ValueMember = "MAPB";
                //cboxNoiLamViecThayDoi.DataSource = pb;
                //cboxNoiLamViecThayDoi.DisplayMember = "TENPB";
                //cboxNoiLamViecThayDoi.ValueMember = "MAPB";
                for (int i = 0; i < pb.Rows.Count; i++)
                {
                    cboxNoiLamViecHienTai_03.Items.Add(pb.Rows[i]["TENPB"].ToString());
                    cboxNoiLamViecThayDoi_03.Items.Add(pb.Rows[i]["TENPB"].ToString());
                }
            }
            catch (Exception ex)
            {
                CRM.DAL.DV.ErrorMessageDAL.DataAccessError(ex);
            }

            if (cboxNoiLamViecHienTai_03.Items.Count > 0) cboxNoiLamViecHienTai_03.SelectedIndex = 0;
            if (cboxNoiLamViecThayDoi_03.Items.Count > 0) cboxNoiLamViecThayDoi_03.SelectedIndex = 0;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tPageThayDoi_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cboxMauBieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            tCtrThongTin.SelectedIndex = cboxMauBieu.SelectedIndex;
        }

        private void tCtrThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxMauBieu.SelectedIndex = tCtrThongTin.SelectedIndex;
        }

        private void txtMACHienTai_TextChanged(object sender, EventArgs e)
        {
            //txtMACHienTai.Text = txtMACHienTai.Text.ToUpper();
            
        }

        private void txtMACThayDoi_TextChanged(object sender, EventArgs e)
        {
            //txtMACThayDoi.Text = txtMACThayDoi.Text.ToUpper();
        }

        private void cbkDiaChiMAC_CheckedChanged(object sender, EventArgs e)
        {
            txtMACHienTai_03.Enabled = ckbDiaChiMAC_03.Checked;
            txtMACThayDoi_03.Enabled = ckbDiaChiMAC_03.Checked;
        }

        private void ckbThayDoiNoiLamViec_CheckedChanged(object sender, EventArgs e)
        {
            cboxNoiLamViecHienTai_03.Enabled = ckbThayDoiNoiLamViec_03.Checked;
            cboxNoiLamViecThayDoi_03.Enabled = ckbThayDoiNoiLamViec_03.Checked;
        }

        private void ckbThayDoiChucNang_CheckedChanged(object sender, EventArgs e)
        {
            cboxChucNangHienTai_03.Enabled = ckbThayDoiChucNang_03.Checked;
            cboxChucNangThayDoi_03.Enabled = ckbThayDoiChucNang_03.Checked;
        }

        private void ckbThayDoiMenu_CheckedChanged(object sender, EventArgs e)
        {
            cboxMenuHienTai_03.Enabled = ckbThayDoiMenu_03.Checked;
            cboxMenuThayDoi_03.Enabled = ckbThayDoiMenu_03.Checked;
        }

        private void ckbThoiGian_CheckedChanged(object sender, EventArgs e)
        {
            dtpThoiGianTuNgay_03.Enabled = ckbThoiGian_03.Checked;
            dtpThoiGianDenNgay_03.Enabled = ckbThoiGian_03.Checked;
        }

        private void ckbDangNhapKhongDungThe_CheckedChanged(object sender, EventArgs e)
        {
            dtpDangNhapTuNgay_03.Enabled = ckbDangNhapKhongDungThe_03.Checked;
            dtpDangNhapDenNgay_03.Enabled = ckbDangNhapKhongDungThe_03.Checked;
        }

        private void ckbThayDoiKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtThayDoiKhac_03.Enabled = ckbThayDoiKhac_03.Checked;
        }

        private void tPageCapMoi_Click(object sender, EventArgs e)
        {

        }



    }
}
