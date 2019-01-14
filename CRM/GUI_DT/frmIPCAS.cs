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
        bool tabSelectingAllowed = false;

        //ten file .docx
        string tenFileIPCAS02 = "CSUS02";
        string tenFileIPCAS03 = "CSUS03";
        string tenFileIPCAS07 = "CSUS07";
        string tenFileIPCAS08 = "CSUS08";
        string tenFileIPCAS09 = "CSUS09";

        string[] listIPCAS, listAD, listPKI, listIB, listBill, listKieuHoi, listTTSP, listAGRITAX;
        //int indexIPCAS = 0, indexAD = 5, indexPKI = 12, indexIB = 22,
        //    indexBill = 28, indexKH = 31, indexTTSP = 35, indexAGRITAX = 39;
        int[] indexHeThong = { 0, 5, 12, 22, 28, 31, 35, 39 };

        List<string> listNguon, listDich;

        public frmIPCAS()
        {
            InitializeComponent();

            //Khoi tao ten mau bieu
            listIPCAS = new string[]{"CSUS/02 - Cấp mới user",
                                    "CSUS/03 - Thay đổi thông tin",
                                    "CSUS/07 - Xác nhận",
                                    "CSUS/08 - Thu hồi user", 
                                    "CSUS/09 - Đặt lại mật khẩu"};
            listAD = new string[]{"AD/02 - Cấp mới user",
                                  "AD/03 - Phiếu xác nhận",
                                  "AD/06 - Thu hồi user",
                                  "AD/08 - Tạm dừng user",
                                  "AD/10 - Khôi phục user",
                                  "AD/12 - Đổi mật khẩu",
                                  "AD/14 - Thay đổi thông tin"};
            listPKI = new string[] {
                            "1A - Cấp chứng thư số",
                            "2A - Gia hạn chứng thư số",
                            "3A - Tạm dừng chứng thư số",
                            "4A - Khôi phục chứng thư số",
                            "5A - Thu hồi chứng thư số",
                            "9A - Đổi mật khẩu",
                            "11A - Bản xác nhận chứng thư số và thiết bị bảo mật",
                            "12A - Bản xác nhận xử lý lỗi thiết bị bảo mật",
                            "13A - Biên bản xác nhận mất/hỏng thiết bị",
                            "PKI/01 - Đề nghị cấp thiết bị bảo mật"};
            listIB = new string[]{
                    "07/IB-ADMIN - Cấp mới user",
                    "08/IB-ADMIN - Thay đổi thông tin",
                    "09/IB-ADMIN - Thu hồi user",
                    "10/IB-ADMIN - Khôi phục user",
                    "11/IB-ADMIN - Đổi mật khẩu",
                    "12/IB-ADMIN - Biên bản bàn giao user"
                };
            listBill = new string[]{
                    "01/BILLPAYMENT - Cấp mới, thay đổi thông tin",
                    "02/BILLPAYMENT - Đổi mật khẩu",
                    "03/BILLPAYMENT - Bản xác nhận user, mật khẩu"
                };
            listKieuHoi = new string[]{
              "KH02 - Cấp mới user",
              "KH04 - Thu hồi user",
              "KH06 - Khôi phục user",
              "KH08 - Đổi mật khẩu"
            };
            listTTSP = new string[]{
                "03/SPKB - Cấp mới user",
                "04/SPKB - Thay đổi thông tin",
                "05/SPKB - Đổi mật khẩu",
                "06/SPKB - Bản xác nhận"
            };
            listAGRITAX = new string[]{
                "01/AGRITAX - Cấp mới user",
                "02/AGRITAX - Thay đổi thông tin",
                "03/AGRITAX - Đổi mật khẩu",
                "04/AGRITAX - Bản xác nhận"
            };
            
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
            cboxHeThong.SelectedIndex = 0;
          
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

        void ThayDoiHeThong()
        {
            cboxMauBieu.Items.Clear();
            switch (cboxHeThong.SelectedIndex)
            {
                case 0: //IPCAS
                    cboxMauBieu.Items.AddRange(listIPCAS);
                    break;
                case 1: //AD
                    cboxMauBieu.Items.AddRange(listAD);
                    break;
                case 2: //PKI
                    cboxMauBieu.Items.AddRange(listPKI);
                    break;
                case 3: //Internet Banking
                    cboxMauBieu.Items.AddRange(listIB);
                    break;
                case 4: //Billpayment
                    cboxMauBieu.Items.AddRange(listBill);
                    break;
                case 5: //Kieu hoi
                    cboxMauBieu.Items.AddRange(listKieuHoi);
                    break;
                case 6: //TTSP
                    cboxMauBieu.Items.AddRange(listTTSP);
                    break;
                case 7: //AGRITAX
                    cboxMauBieu.Items.AddRange(listAGRITAX);
                    break;
                default: 
                    break;
            }
            cboxMauBieu.SelectedIndex = 0;
        }

        void ThayDoiMauBieu()
        {
            if (cboxHeThong.SelectedIndex < 0) return;
            var index = indexHeThong[cboxHeThong.SelectedIndex] + cboxMauBieu.SelectedIndex;
            if (tCtrThongTin.TabCount < index)
            {
                MessageBox.Show("Mẫu biểu hiện tại chưa được hỗ trợ.\nXin chờ bản cập nhật sau!");
            }
            else
            {
                tabSelectingAllowed = true;
                tCtrThongTin.SelectedIndex = index;
            }
        }

        void ThayDoiMauBieu_Tab()
        {
            if (cboxHeThong.SelectedIndex < 0 || cboxMauBieu.SelectedIndex < 0) return;
            for (int i = 0; i < indexHeThong.Length; i++)
            {
                if (tCtrThongTin.SelectedIndex < indexHeThong[i] && tCtrThongTin.SelectedIndex < indexHeThong[i + 1])
                {
                    cboxHeThong.SelectedIndex = i;
                    cboxMauBieu.SelectedIndex = tCtrThongTin.SelectedIndex - indexHeThong[i];
                    break;
                }
            }
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

            listDich.Add("<PHONGBAN>");
            listNguon.Add(Thongtindangnhap.tenpb);

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

        void CSUS02()
        {
            listDich.Add("<CSUS02_HOTEN>");
            listNguon.Add(txtHoTen_02.Text);

            listDich.Add("<CSUS02_PHONGBAN>");
            listNguon.Add(txtPhongBan_02.Text);

            listDich.Add("<CSUS02_CHUCVU>");
            listNguon.Add(txtChucVu_02.Text);

            listDich.Add("<CSUS02_MANV>");
            listNguon.Add(txtMaNV_02.Text);

            listDich.Add("<CSUS02_EMAIL>");
            listNguon.Add(txtEmail_02.Text);

            listDich.Add("<CSUS02_SDT>");
            listNguon.Add(txtSDT_02.Text);

            listDich.Add("<CSUS02_KIEU_USER>");
            listNguon.Add(cboxKieuUser_02.Text);

            listDich.Add("<CSUS02_MENU>");
            listNguon.Add(cboxMenu_02.Text);

            listDich.Add("<CSUS02_THONGTINTHEM>");
            string thongTinThem = "";
            if (ckbMAC_02.Checked) thongTinThem += "Gán địa chỉ MAC: " + txtMAC_02.Text;
            if (ckbDNKDT_02.Checked)
            {
                if (ckbMAC_02.Checked) thongTinThem += ", ";
                thongTinThem += "Đăng nhập không dùng thẻ PKI từ " + dtpTuNgay_02.Value.ToString("dd/MM/yyyy") + "đến " + dtpDenNgay_02.Value.ToString("dd/MM/yyyy");
            }
            listNguon.Add(thongTinThem);
        }

        void CSUS03()
        {
            listDich.Add("<CSUS03_THAYDOI>");
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

            listDich.Add("<CSUS03_THOIGIAN>");
            var thoiGian = "-Thực hiện từ: " + dtpThoiGianTuNgay_03.Value.ToString("HH:mm") + " ngày " + dtpThoiGianTuNgay_03.Value.ToString("dd/MM/yyyy");
            if (ckbDenNgay_03.Checked)
                thoiGian += "\n-Đến: " + dtpThoiGianDenNgay_03.Value.ToString("HH:mm") + " ngày " + dtpThoiGianDenNgay_03.Value.ToString("dd/MM/yyyy");
            listNguon.Add(thoiGian);

            listDich.Add("<CSUS03_MENU>");
            if (ckbMenu_03.Checked)
                listNguon.Add(cboxMenuHienTai_03.Text + " => " + cboxMenuThayDoi_03.Text);
            else 
                listNguon.Add("");

            listDich.Add("<CSUS03_YEUCAUTHEM>");
            if (ckbYeuCauThem_03.Checked)
                listNguon.Add(txtYeuCauThem_03.Text);
            else
                listNguon.Add("");
        }

        void CSUS07() {
            listDich.Add("<CSUS07_TIME>");
            listNguon.Add(string.Format("{0} giờ {1} phút, ngày {2} tháng {3} năm {4}",
                dtpThoiGian_07.Value.Hour,
                dtpThoiGian_07.Value.Minute,
                dtpThoiGian_07.Value.Day,
                dtpThoiGian_07.Value.Month,
                dtpThoiGian_07.Value.Year));
        }
        void CSUS08() {
            if (!ckbVinhVien_08.Checked)
            {
                listDich.Add("<CSUS08_USERID_R>");
                listNguon.Add(Thongtindangnhap.user_id);

                listDich.Add("<CSUS08_FROM_R>");
                listNguon.Add(dtpTuNgay_08.Text);

                listDich.Add("<CSUS08_TO_R>");
                listNguon.Add(dtpDenNgay_08.Text);

                listDich.Add("<CSUS08_LYDO_R>");
                listNguon.Add(txtLyDo_08.Text);
            }
            else
            {
                listDich.Add("<CSUS08_USERID_D>");
                listNguon.Add(Thongtindangnhap.user_id);

                listDich.Add("<CSUS08_FROM_D>");
                listNguon.Add(dtpTuNgay_08.Text);

                listDich.Add("<CSUS08_LYDO_D>");
                listNguon.Add(txtLyDo_08.Text);
            }
        }
        void CSUS09() {
            listDich.Add("<CSUS09_LYDO>");
            listNguon.Add(txtLyDo_09.Text);
        }


        void KhoiTao()
        {
            listNguon.Clear();
            listDich.Clear();

            KhoiTaoChung();

            switch(cboxMauBieu.SelectedIndex)
            {
                case 0:
                    CSUS02();
                    break;
                case 1:
                    CSUS03();
                    break;
                case 2:
                    CSUS07();
                    break;
                case 3:
                    CSUS08();
                    break;
                case 4:
                    CSUS09();
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
                    fileName = tenFileIPCAS02;
                    break;
                case 1:
                    fileName = tenFileIPCAS03;
                    break;
                case 2:
                    fileName = tenFileIPCAS07;
                    break;
                case 3:
                    fileName = tenFileIPCAS08;
                    break;
                case 4:
                    fileName = tenFileIPCAS09;
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
            ThayDoiMauBieu();
        }

        private void tCtrThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabSelectingAllowed = false;
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

        private void cboxHeThong_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThayDoiHeThong();
        }

        private void tCtrThongTin_Selected(object sender, TabControlEventArgs e)
        {
            tabSelectingAllowed = false;
        }

        private void tCtrThongTin_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!tabSelectingAllowed)
                e.Cancel = true;
        }
    }
}
