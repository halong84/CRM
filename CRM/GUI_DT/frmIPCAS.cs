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
using CRM.Utilities.DV;

namespace CRM.GUI_DT
{
    public partial class frmIPCAS : Form
    {

        string tenFile02 = "CSUS02";
        string tenFile03 = "CSUS03";
        string tenFile07 = "CSUS07";
        string tenFile08 = "CSUS08";
        string tenFile09 = "CSUS09";

        List<string> listNguon, listDich;

        public frmIPCAS()
        {
            InitializeComponent();
            GetUserInfo();
            GetKiemSoat();
            GetNoiLamViec();
            GetChucNang_Menu();
            cboxMauBieu.SelectedIndex = 0;
            cboxKieuUser_02.SelectedIndex = 0;
            cboxMenu_02.SelectedIndex = 0;
            cboxNoiLamViecHienTai_03.SelectedIndex = 0;
            cboxNoiLamViecThayDoi_03.SelectedIndex = 0;
            cboxChucNangHienTai_03.SelectedIndex = 0;
            cboxChucNangThayDoi_03.SelectedIndex = 0;
            cboxMenuHienTai_03.SelectedIndex = 0;
            cboxMenuThayDoi_03.SelectedIndex = 0;


            listNguon = new List<string>();
            listDich = new List<string>();
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
                    cboxMenu_02.Items.Add(menu.Rows[i][0].ToString());
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

        #region Tao mau bieu
        void KhoiTaoChung()
        {
            string cn = Thongtindangnhap.tencn;
            if (!Thongtindangnhap.hs)
            {
                cn = Thongtindangnhap.tenpb;
                listDich.Add("<CHINHANH0>");
                listNguon.Add((Thongtindangnhap.tencn + "\n" + cn).ToUpper());
            }
            else
            {
                listDich.Add("<CHINHANH0>");
                listNguon.Add(cn.ToUpper());
            }

            listDich.Add("<CHINHANH>");
            listNguon.Add(cn);

            listDich.Add("<MACN>");
            listNguon.Add(Thongtindangnhap.macn);

            listDich.Add("<DONVI>");
            if (Thongtindangnhap.hs)
                listNguon.Add(Thongtindangnhap.tenpb);
            else listNguon.Add(Thongtindangnhap.tencn);

            listDich.Add("<NGAY>");
            listNguon.Add(DateTime.Today.ToString("dd/MM/yyyy"));

            listDich.Add("<DIABAN>");
            if (Thongtindangnhap.macn == "2300" || Thongtindangnhap.macn == "2301" || Thongtindangnhap.macn == "2313")
            {
                listNguon.Add("Hải Dương");
            }
            else
            {
                listNguon.Add(Thongtindangnhap.tencn.Substring(25));
            }

            listDich.Add("<NGAY_THANG_NAM>");
            listNguon.Add(string.Format("ngày {0} tháng {1} năm {2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year));

            listDich.Add("<GDV>");
            listNguon.Add(Thongtindangnhap.tennv);

            listDich.Add("<KSV>");
            listNguon.Add(cboxKiemSoat.Text);

            listDich.Add("<HOTEN>");
            listNguon.Add(txtHoTen.Text);

            listDich.Add("<USERID>");
            listNguon.Add(txtUserID.Text);

            listDich.Add("<MANV>");
            listNguon.Add(txtMaNV.Text);

            listDich.Add("<CHUCVU>");
            listNguon.Add(txtChucVu.Text);

            listDich.Add("<SDT>");
            listNguon.Add(txtSDT.Text);
        }

        void KhoiTao02()
        {
            listDich.Add("<HOTEN_02>");
            listNguon.Add(txtHoTen_02.Text);

            listDich.Add("<PHONGBAN_02>");
            listNguon.Add(txtPhongBan_02.Text);

            listDich.Add("<CHUCVU_02>");
            listNguon.Add(txtChucVu_02.Text);

            listDich.Add("<MANV_02>");
            listNguon.Add(txtMaNV_02.Text);

            listDich.Add("<EMAIL_02>");
            listNguon.Add(txtEmail_02.Text);

            listDich.Add("<SDT_02>");
            listNguon.Add(txtSDT_02.Text);

            listDich.Add("<KIEU_USER_02>");
            listNguon.Add(cboxKieuUser_02.Text);

            listDich.Add("<MENU_02>");
            listNguon.Add(cboxMenu_02.Text);

            listDich.Add("<THONGTINTHEM_02>");
            string thongTinThem = "";
            if (ckbMAC_02.Checked) thongTinThem += "Gán địa chỉ MAC: " + txtMAC_02.Text;
            if (ckbDNKDT_02.Checked)
            {
                if (ckbMAC_02.Checked) thongTinThem += ", ";
                thongTinThem += "Đăng nhập không dùng thẻ PKI từ " + dtpTuNgay_02.Value.ToString("dd/MM/yyyy") + "đến " + dtpDenNgay_02.Value.ToString("dd/MM/yyyy");
            }
            listNguon.Add(thongTinThem);
        }

        void KhoiTao03()
        {
            listDich.Add("<THAYDOI_03>");
            string thayDoi = "";
            if (ckbMAC_03.Checked) thayDoi += "-Đổi địa chỉ MAC: " + txtMACHienTai_03.Text + " => " + txtMACThayDoi_03.Text;
            if (ckbNoiLamViec_03.Checked)
            {
                if (ckbMAC_03.Checked)
                    thayDoi += "\n";
                thayDoi += "-Thay đổi phòng ban:" + cboxNoiLamViecHienTai_03.Text + " => " + cboxNoiLamViecThayDoi_03.Text;
            }
            if (ckbChucNang_03.Checked)
            {
                if (ckbMAC_03.Checked || ckbNoiLamViec_03.Checked)
                    thayDoi += "\n";
                thayDoi += "-Thay đổi chức năng: " + cboxChucNangHienTai_03.Text + " => " + cboxChucNangThayDoi_03.Text;
            }

            if (ckbDNKDT_03.Checked)
            {
                if (ckbMAC_03.Checked || ckbNoiLamViec_03.Checked || ckbChucNang_03.Checked)
                    thayDoi += "\n";
                thayDoi += "-Đăng nhập không dùng thẻ PKI đến: " + dtpDNKDTDenNgay_03.Text;
            }
            listNguon.Add(thayDoi);

            listDich.Add("<THOIGIAN_03>");
            var thoiGian = "-Thực hiện từ: " + dtpThoiGianTuNgay_03.Value.ToString("HH:mm") + " ngày " + dtpThoiGianTuNgay_03.Value.ToString("dd/MM/yyyy");
            if (ckbDenNgay_03.Checked)
                thoiGian += "\n-Đến: " + dtpThoiGianDenNgay_03.Value.ToString("HH:mm") + " ngày " + dtpThoiGianDenNgay_03.Value.ToString("dd/MM/yyyy");
            listNguon.Add(thoiGian);

            listDich.Add("<MENU_03>");
            if (ckbMenu_03.Checked)
                listNguon.Add(cboxMenuHienTai_03.Text + " => " + cboxMenuThayDoi_03.Text);
            else 
                listNguon.Add("");

            listDich.Add("<YEUCAUTHEM_03>");
            if (ckbYeuCauThem_03.Checked)
                listNguon.Add(txtYeuCauThem_03.Text);
            else
                listNguon.Add("");
        }

        void KhoiTao07() { }
        void KhoiTao08() { }
        void KhoiTao09() { }


        void KhoiTao()
        {
            listNguon.Clear();
            listDich.Clear();

            KhoiTaoChung();

            switch(cboxMauBieu.SelectedIndex)
            {
                case 0:
                    KhoiTao02();
                    break;
                case 1:
                    KhoiTao03();
                    break;
                case 2:
                    KhoiTao07();
                    break;
                case 3:
                    KhoiTao08();
                    break;
                case 4:
                    KhoiTao09();
                    break;
                default: break;
            }
        }

        void CreateFile()
        {
            string fileName = "";
            switch (cboxMauBieu.SelectedIndex)
            {
                case 0:
                    fileName = tenFile02;
                    break;
                case 1:
                    fileName = tenFile03;
                    break;
                case 2:
                    fileName = tenFile07;
                    break;
                case 3:
                    fileName = tenFile08;
                    break;
                case 4:
                    fileName = tenFile09;
                    break;
                default: break;
            }
            saveFileDialog1.Filter = "Word Documents|*.docx";

            string subFolder = @"DT\";
            if (!CommonMethods.SubFolderExist(subFolder))
                CommonMethods.CreateSubFolder(subFolder);

            string TemplateFileLocation = CommonMethods.TemplateFileLocation(fileName + ".docx", "DT");
            string saveFileLocation = CommonMethods.SaveFileLocation(subFolder + fileName + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".docx");


            if (CommonMethods.CreateWordDocument(TemplateFileLocation, saveFileLocation, listDich, listNguon))
            {
                MessageBox.Show("File đã được tạo tại đường dẫn: " + saveFileLocation, "Tạo file thành công");
                OpenFileWord(saveFileLocation);
            }

        }

        void OpenFileWord(string fileLocation)
        {
            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(fileLocation);
            //if (cboxMauBieu.SelectedIndex == 9)
            //{
            //    PutStringIntoTable(document);
            //}
            ap.Visible = true;
        }
        #endregion

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
            txtMACHienTai_03.Enabled = ckbMAC_03.Checked;
            txtMACThayDoi_03.Enabled = ckbMAC_03.Checked;
        }

        private void ckbThayDoiNoiLamViec_CheckedChanged(object sender, EventArgs e)
        {
            cboxNoiLamViecHienTai_03.Enabled = ckbNoiLamViec_03.Checked;
            cboxNoiLamViecThayDoi_03.Enabled = ckbNoiLamViec_03.Checked;
        }

        private void ckbThayDoiChucNang_CheckedChanged(object sender, EventArgs e)
        {
            cboxChucNangHienTai_03.Enabled = ckbChucNang_03.Checked;
            cboxChucNangThayDoi_03.Enabled = ckbChucNang_03.Checked;
        }

        private void ckbThayDoiMenu_CheckedChanged(object sender, EventArgs e)
        {
            cboxMenuHienTai_03.Enabled = ckbMenu_03.Checked;
            cboxMenuThayDoi_03.Enabled = ckbMenu_03.Checked;
        }


        private void ckbDangNhapKhongDungThe_CheckedChanged(object sender, EventArgs e)
        {
            dtpDNKDTDenNgay_03.Enabled = ckbDNKDT_03.Checked;
        }

        private void ckbThayDoiKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtYeuCauThem_03.Enabled = ckbYeuCauThem_03.Checked;
        }

        private void tPageCapMoi_Click(object sender, EventArgs e)
        {

        }

        private void ckbMAC_02_CheckedChanged(object sender, EventArgs e)
        {
            txtMAC_02.Enabled = ckbMAC_02.Checked;
        }

        private void ckbDangNhap_02_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay_02.Enabled = ckbDNKDT_02.Checked;
            dtpDenNgay_02.Enabled = ckbDNKDT_02.Checked;
        }

        private void ckbLamThay_07_CheckedChanged(object sender, EventArgs e)
        {
            txtHoTen_07.Enabled = ckbLamThay_07.Checked;
            txtChucVu_07.Enabled = ckbLamThay_07.Checked;
            txtPhongBan_07.Enabled = ckbLamThay_07.Checked;
            txtSDT_07.Enabled = ckbLamThay_07.Checked;
            txtUser_07.Enabled = ckbLamThay_07.Checked;
        }

        private void ckbVinhVien_08_CheckedChanged(object sender, EventArgs e)
        {
            dtpDenNgay_08.Enabled = !ckbVinhVien_08.Checked;
        }

        private void ckbLamThay_08_CheckedChanged(object sender, EventArgs e)
        {
            txtHoTen_08.Enabled = ckbLamThay_08.Checked;
            txtChucVu_08.Enabled = ckbLamThay_08.Checked;
            txtPhongBan_08.Enabled = ckbLamThay_08.Checked;
            txtSDT_08.Enabled = ckbLamThay_08.Checked;
            txtUser_08.Enabled = ckbLamThay_08.Checked;
        }

        private void cboxMenu_02_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void txtMAC_02_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dtpTuNgay_02_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDenNgay_02_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cboxKieuUser_02_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KhoiTao();
            CreateFile();
        }

        private void ckbDNKDT_03_CheckedChanged(object sender, EventArgs e)
        {
            dtpDNKDTDenNgay_03.Enabled = ckbDNKDT_03.Checked;
        }

        private void ckbThoiGian_03_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ckbYeuCauThem_03_CheckedChanged(object sender, EventArgs e)
        {
            txtYeuCauThem_03.Enabled = ckbYeuCauThem_03.Checked;
        }

        private void ckbDenNgay_03_CheckedChanged(object sender, EventArgs e)
        {
            dtpThoiGianDenNgay_03.Enabled = ckbDenNgay_03.Checked;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }



    }
}
