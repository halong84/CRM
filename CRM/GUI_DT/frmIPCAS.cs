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
                    cboxChucNangHienTai.Items.Add(cn.Rows[i][0].ToString());
                    cboxChucNangThayDoi.Items.Add(cn.Rows[i][0].ToString());
                }

                for (int i = 0; i < menu.Rows.Count; i++)
                {
                    cboxMenuHienTai.Items.Add(menu.Rows[i][0].ToString());
                    cboxMenuThayDoi.Items.Add(menu.Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                CRM.DAL.DV.ErrorMessageDAL.DataAccessError(ex);
            }
            if (cboxChucNangHienTai.Items.Count > 0)
                cboxChucNangHienTai.SelectedIndex = 0;
            if (cboxChucNangThayDoi.Items.Count > 0)
                cboxChucNangThayDoi.SelectedIndex = 0;
            if (cboxMenuThayDoi.Items.Count > 0)
                cboxMenuThayDoi.SelectedIndex = 0;
            if (cboxMenuHienTai.Items.Count > 0)
                cboxMenuHienTai.SelectedIndex = 0;
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
                    cboxNoiLamViecHienTai.Items.Add(pb.Rows[i]["TENPB"].ToString());
                    cboxNoiLamViecThayDoi.Items.Add(pb.Rows[i]["TENPB"].ToString());
                }
            }
            catch (Exception ex)
            {
                CRM.DAL.DV.ErrorMessageDAL.DataAccessError(ex);
            }

            if (cboxNoiLamViecHienTai.Items.Count > 0) cboxNoiLamViecHienTai.SelectedIndex = 0;
            if (cboxNoiLamViecThayDoi.Items.Count > 0) cboxNoiLamViecThayDoi.SelectedIndex = 0;
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
            txtMACHienTai.Enabled = ckbDiaChiMAC.Checked;
            txtMACThayDoi.Enabled = ckbDiaChiMAC.Checked;
        }

        private void ckbThayDoiNoiLamViec_CheckedChanged(object sender, EventArgs e)
        {
            cboxNoiLamViecHienTai.Enabled = ckbThayDoiNoiLamViec.Checked;
            cboxNoiLamViecThayDoi.Enabled = ckbThayDoiNoiLamViec.Checked;
        }

        private void ckbThayDoiChucNang_CheckedChanged(object sender, EventArgs e)
        {
            cboxChucNangHienTai.Enabled = ckbThayDoiChucNang.Checked;
            cboxChucNangThayDoi.Enabled = ckbThayDoiChucNang.Checked;
        }

        private void ckbThayDoiMenu_CheckedChanged(object sender, EventArgs e)
        {
            cboxMenuHienTai.Enabled = ckbThayDoiMenu.Checked;
            cboxMenuThayDoi.Enabled = ckbThayDoiMenu.Checked;
        }

        private void ckbThoiGian_CheckedChanged(object sender, EventArgs e)
        {
            dtpThoiGianTuNgay.Enabled = ckbThoiGian.Checked;
            dtpThoiGianDenNgay.Enabled = ckbThoiGian.Checked;
        }

        private void ckbDangNhapKhongDungThe_CheckedChanged(object sender, EventArgs e)
        {
            dtpDangNhapTuNgay.Enabled = ckbDangNhapKhongDungThe.Checked;
            dtpDangNhapDenNgay.Enabled = ckbDangNhapKhongDungThe.Checked;
        }

        private void ckbThayDoiKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtThayDoiKhac.Enabled = ckbThayDoiKhac.Checked;
        }



    }
}
