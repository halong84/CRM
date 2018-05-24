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
using System.Threading;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace CRM.GUI_DV
{
    public partial class frmThanhLapToVanChuyen : Form
    {
        static string fileNameTLTVC = "QD_THANH_LAP_TO_VAN_CHUYEN_DAC_BIET";
        List<String> listDich, listNguon;
        List<User> users;
        public frmThanhLapToVanChuyen()
        {
            InitializeComponent();
            listDich = new List<string>();
            listNguon = new List<string>();
            users = new List<User>();
            try
            {
                DataTable dt = KiemQuyDAL.DV_DSNhanVien_MaCN(Thong_tin_dang_nhap.ma_cn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    users.Add(new User(dt.Rows[i]));
                    cbToTruong.Items.Add(users[i].tennv);
                    cbGiamSat1.Items.Add(users[i].tennv);
                    cbGiamSat2.Items.Add(users[i].tennv);
                }
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        void KhoiTaoTLTVC()
        {
            listNguon.Clear();
            listDich.Clear();

            listDich.Add("<CHI_NHANH_0>");
            listNguon.Add(Thong_tin_dang_nhap.ten_cn.ToUpper());
            listDich.Add("<CHI_NHANH>");
            listNguon.Add(Thong_tin_dang_nhap.ten_cn);
            listDich.Add("<SO>");
            listNguon.Add(txtSo.Text);
            listDich.Add("<NGAY>");
            listNguon.Add(DateTime.Now.Day.ToString());
            listDich.Add("<THANG>");
            listNguon.Add(DateTime.Now.Month.ToString());
            listDich.Add("<NAM>");
            listNguon.Add(DateTime.Now.Year.ToString());

            listDich.Add("<HANG_DAC_BIET>");
            listNguon.Add(txtLoaiHang.Text);
            listDich.Add("<BANG_SO>");
            listNguon.Add(txtBangSo.Text);
            listDich.Add("<BANG_CHU>");
            listNguon.Add(CommonMethods.FirstCharToUpper(CommonMethods.ChuyenSoSangChu(XoaDauPhay(txtBangSo.Text))));
            listDich.Add("<DIA_CHI_CN>");
            listNguon.Add(Thong_tin_dang_nhap.dia_chi_cn);
            listDich.Add("<NOI_DEN>");
            listNguon.Add(txtNoiDen.Text);
            listDich.Add("<PHUONG_TIEN>");
            listNguon.Add(txtBienSo.Text);
            listDich.Add("<NGAY_THUC_HIEN>");
            listNguon.Add(dtpNgayThucHien.Value.ToString("dd/MM/yyyy"));
        }

        void TaoFileTLTVC()
        {
            saveFileTLTVC.Filter = "Word Documents|*.docx";

            string subFolder = @"ThanhLapToVanChuyenDacBiet\";
            if (!CommonMethods.SubFolderExist(subFolder))
                CommonMethods.CreateSubFolder(subFolder);

            string TemplateFileLocation = CommonMethods.TemplateFileLocation(fileNameTLTVC + ".docx");
            string saveFileLocation = CommonMethods.SaveFileLocation(subFolder + fileNameTLTVC + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".docx");


            if (CommonMethods.CreateWordDocument(TemplateFileLocation, saveFileLocation, listDich, listNguon))
            {
                MessageBox.Show("File đã được tạo tại đường dẫn: " + saveFileLocation, "Tạo file thành công");
                //Thread.Sleep(500);
                OpenFileWord(saveFileLocation);
            }

        }

        void OpenFileWord(string fileLocation)
        {
            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(fileLocation);
            PutStringIntoTable(document);
            ap.Visible = true;
        }

        void PutStringIntoTable(Word.Document doc)
        {
            object oMissing = System.Reflection.Missing.Value;
            Word.Table tb = doc.Tables[2];

            int index = 0;
            string toTruong = "";
            string giamSat1 = "";
            string giamSat2 = "";
            string laiXe = "";
            string baoVe = "";
            if (cbToTruong.SelectedItem != null)
            {
                index++;
                var u = users[cbToTruong.SelectedIndex];
                string gt = "Ông";
                if (!u.gioiTinh) gt = "Bà";
                string pb = Thong_tin_dang_nhap.ten_cn;
                if (u.chucvu != "Giám đốc" && u.chucvu != "Phó Giám đốc")
                    pb = Thong_tin_dang_nhap.tenPb;
                toTruong = index + ". " + gt + ": " + u.tennv + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDToTruong.Text + ", Ngày cấp: " + txtNgayCapToTruong.Text +
                    ", Nơi cấp: " + txtNoiCapToTruong.Text + "; Chức vụ: " + u.chucvu + " " + pb + "; Chức danh: Tổ trưởng;";
                tb.Rows[index].Cells[1].Range.Text = toTruong;
            }

            if (cbGiamSat1.SelectedItem != null)
            {
                tb.Rows.Add(oMissing);
                tb.Rows.Add(tb.Rows[index]);
                index++;
                var u = users[cbGiamSat1.SelectedIndex];
                string gt = "Ông";
                if (!u.gioiTinh) gt = "Bà";
                string pb = Thong_tin_dang_nhap.ten_cn;
                if (u.chucvu != "Giám đốc" && u.chucvu != "Phó Giám đốc")
                    pb = ToVanChuyenDAL.DV_LAYPHONGBAN(u.mapb);
                giamSat1 = index + ". " + gt + ": " + u.tennv + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat1.Text + " Ngày cấp: " + txtNgayCapGiamSat1.Text +
                    " Nơi cấp: " + txtNoiCapGiamSat1.Text + "; Chức vụ: " + u.chucvu + " " + pb + "; Chức danh: Giám sát;";
                tb.Rows[index].Cells[1].Range.Text = giamSat1;
            }

            listDich.Add("<GIAM_SAT_2>");
            if (cbGiamSat2.SelectedItem != null)
            {
                tb.Rows.Add(oMissing);
                index++;
                var u = users[cbGiamSat2.SelectedIndex];
                string gt = "Ông";
                if (!u.gioiTinh) gt = "Bà";
                string pb = Thong_tin_dang_nhap.ten_cn;
                if (u.chucvu != "Giám đốc" && u.chucvu != "Phó Giám đốc")
                    pb = ToVanChuyenDAL.DV_LAYPHONGBAN(u.mapb);
                giamSat2 = index + ". " + gt + ": " + u.tennv + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat2.Text + " Ngày cấp: " + txtNgayCapGiamSat2.Text +
                    " Nơi cấp: " + txtNoiCapGiamSat2.Text + "; Chức vụ: " + u.chucvu + " " + pb + "; Chức danh: Giám sát;";
                tb.Rows[index].Cells[1].Range.Text = giamSat2;
            }
            //Lai xe
            if (!string.IsNullOrEmpty(txtHoTenLaiXe.Text))
            {
                tb.Rows.Add(oMissing);
                index++;
                listDich.Add("<LAI_XE>");
                laiXe = index + ". " + "Ông/Bà: " + txtHoTenLaiXe.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDLaiXe.Text +
                    " Ngày cấp: " + txtNgayCapLaiXe.Text + ",Nơi cấp: " + txtNoiCapLaiXe.Text + ";Chức danh: Lái xe;";
                tb.Rows[index].Cells[1].Range.Text = laiXe;
            }
            //Bao ve
            if (!string.IsNullOrEmpty(txtHoTenBaoVe.Text))
            {
                tb.Rows.Add(oMissing);
                index++;
                listDich.Add("<BAO_VE>");
                baoVe = index + ". " + "Ông/Bà: " + txtHoTenLaiXe.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDLaiXe.Text +
                    " Ngày cấp: " + txtNgayCapLaiXe.Text + ",Nơi cấp: " + txtNoiCapLaiXe.Text + ";Chức danh: Bảo vệ;";
                tb.Rows[index].Cells[1].Range.Text = baoVe;
            }
            doc.Save();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            KhoiTaoTLTVC();
            TaoFileTLTVC();
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

        private void txtBangSo_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtBangSo);
        }

        private void txtNgayCapLaiXe_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void cbToTruong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbGiamSat1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbGiamSat2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
