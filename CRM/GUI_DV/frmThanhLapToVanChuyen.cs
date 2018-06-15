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
        static string fileNameTLTVC = "MAU_03_LENH_DIEU_CHUYEN";
        List<String> listDich, listNguon;

        public frmThanhLapToVanChuyen()
        {
            InitializeComponent();
            listDich = new List<string>();
            listNguon = new List<string>();

            cbGtTT.SelectedIndex = 0;
            cbGtGs1.SelectedIndex = 0;
            cbGtGs2.SelectedIndex = 0;
            cbGtBv.SelectedIndex = 0;
            cbGtLx.SelectedIndex = 0;


            DataTable dt = null;
            try
            {
                dt = ToVanChuyenDAL.DV_TOVANCHUYEN_MAPB(Thong_tin_dang_nhap.ma_pb);
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtQuyetDinh.Text = r["QUYETDINH"].ToString();

                    if(!Convert.ToBoolean(r["GTTT"])) cbGtTT.SelectedIndex = 1;
                    if(!Convert.ToBoolean(r["GTGS1"])) cbGtGs1.SelectedIndex = 1;
                    if(!Convert.ToBoolean(r["GTGS2"])) cbGtGs2.SelectedIndex = 1;
                    if(!Convert.ToBoolean(r["GTBV"])) cbGtBv.SelectedIndex = 1;
                    if(!Convert.ToBoolean(r["GTLX"])) cbGtLx.SelectedIndex = 1;

                    txtTenToTruong.Text = r["TENTT"].ToString();
                    txtTenGiamSat1.Text = r["TENGS1"].ToString();
                    txtTenGiamSat2.Text = r["TENGS2"].ToString();
                    txtTenBaoVe.Text = r["TENBV"].ToString();
                    txtTenLaiXe.Text = r["TENLX"].ToString();

                    txtChucVuToTruong.Text = r["CHUCVUTT"].ToString();
                    txtChucVuGiamSat1.Text = r["CHUCVUGS1"].ToString();
                    txtChucVuGiamSat2.Text = r["CHUCVUGS2"].ToString();
                    txtChucVuBaoVe.Text = r["CHUCVUBV"].ToString();
                    txtChucVuLaiXe.Text = r["CHUCVULX"].ToString();

                    txtCMNDToTruong.Text = r["CMNDTT"].ToString();
                    txtCMNDGiamSat1.Text = r["CMNDGS1"].ToString();
                    txtCMNDGiamSat2.Text = r["CMNDGS2"].ToString();
                    txtCMNDBaoVe.Text = r["CMNDBV"].ToString();
                    txtCMNDLaiXe.Text = r["CMNDLX"].ToString();

                    txtNgayCapToTruong.Text = r["NGAYCAPTT"].ToString();
                    txtNgayCapGiamSat1.Text = r["NGAYCAPGS1"].ToString();
                    txtNgayCapGiamSat2.Text = r["NGAYCAPGS2"].ToString();
                    txtNgayCapBaoVe.Text = r["NGAYCAPBV"].ToString();
                    txtNgayCapLaiXe.Text = r["NGAYCAPLX"].ToString();

                    txtNoiCapToTruong.Text = r["NOICAPTT"].ToString();
                    txtNoiCapGiamSat1.Text = r["NOICAPGS1"].ToString();
                    txtNoiCapGiamSat2.Text = r["NOICAPGS2"].ToString();
                    txtNoiCapBaoVe.Text = r["NOICAPBV"].ToString();
                    txtNoiCapLaiXe.Text = r["NOICAPLX"].ToString();

                    txtLoaiHang.Text = r["LOAIHANG"].ToString();
                    txtNoiDen.Text = r["NOIDEN"].ToString();
                    txtPhuongTien.Text = r["PHUONGTIEN"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
            }

        }

        void KhoiTaoTLTVC()
        {
            listNguon.Clear();
            listDich.Clear();

            listDich.Add("<QUYET_DINH>");
            listNguon.Add(txtQuyetDinh.Text);
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
            listDich.Add("<NOI_DI>");
            listNguon.Add(Thong_tin_dang_nhap.dia_chi_cn);
            listDich.Add("<NOI_DEN>");
            listNguon.Add(txtNoiDen.Text);
            listDich.Add("<PHUONG_TIEN>");
            listNguon.Add(txtPhuongTien.Text);
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

            if (!string.IsNullOrEmpty(txtTenToTruong.Text))
            {
                index++;
                toTruong = index + ". " + cbGtTT.Text + ": " + txtTenToTruong.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDToTruong.Text + ", Ngày cấp: " + txtNgayCapToTruong.Text +
                    ", Nơi cấp: " + txtNoiCapToTruong.Text;
                if (!string.IsNullOrEmpty(txtChucVuToTruong.Text)) toTruong += " - " + txtChucVuToTruong.Text;
                toTruong += " - Tổ trưởng;";
                tb.Rows[index].Cells[1].Range.Text = toTruong;
                Console.WriteLine(tb.Rows[index].Cells[1].Range.Text);
            }

            if (!string.IsNullOrEmpty(txtTenGiamSat1.Text))
            {
                tb.Rows.Add(oMissing);
                //tb.Rows.Add(tb.Rows[index]);
                index++;
                giamSat1 = index + ". " + cbGtBv.Text + ": " + txtTenGiamSat1.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat1.Text + ", Ngày cấp: " + txtNgayCapGiamSat1.Text +
                    ", Nơi cấp: " + txtNoiCapGiamSat1.Text;
                if (!string.IsNullOrEmpty(txtChucVuGiamSat1.Text)) giamSat1 += " - " + txtChucVuGiamSat1.Text;
                giamSat1 += " - Thành viên;";
                tb.Rows[index].Cells[1].Range.Text = giamSat1;
                Console.WriteLine(tb.Rows[index].Cells[1].Range.Text);

            }

            if (!string.IsNullOrEmpty(txtTenGiamSat2.Text))
            {
                tb.Rows.Add(oMissing);
                //tb.Rows.Add(tb.Rows[index]);
                index++;
                giamSat2 = index + ". " + cbGtGs2.Text + ": " + txtTenGiamSat2.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat2.Text + " Ngày cấp: " + txtNgayCapGiamSat2.Text +
                    ", Nơi cấp: " + txtNoiCapGiamSat2.Text;
                if (!string.IsNullOrEmpty(txtChucVuGiamSat2.Text)) giamSat2 += " - " + txtChucVuGiamSat2.Text;
                giamSat2 += " - Thành viên;";
                tb.Rows[index].Cells[1].Range.Text = giamSat2;
                Console.WriteLine(tb.Rows[index].Cells[1].Range.Text);
            }

            //Lai xe
            if (!string.IsNullOrEmpty(txtTenLaiXe.Text))
            {
                tb.Rows.Add(oMissing);
                index++;
                laiXe = index + ". " + cbGtLx.Text + ": " + txtTenLaiXe.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDLaiXe.Text +
                    ", Ngày cấp: " + txtNgayCapLaiXe.Text + ", Nơi cấp: " + txtNoiCapLaiXe.Text + " - Lái xe;";
                tb.Rows[index].Cells[1].Range.Text = laiXe;
                Console.WriteLine(tb.Rows[index].Cells[1].Range.Text);
            }
            //Bao ve
            if (!string.IsNullOrEmpty(txtTenBaoVe.Text))
            {
                tb.Rows.Add(oMissing);
                index++;
                baoVe = index + ". " + cbGtBv.Text + ": " + txtTenBaoVe.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDBaoVe.Text +
                    ", Ngày cấp: " + txtNgayCapBaoVe.Text + ", Nơi cấp: " + txtNoiCapBaoVe.Text + " - Bảo vệ;";
                tb.Rows[index].Cells[1].Range.Text = baoVe;
                Console.WriteLine(tb.Rows[index].Cells[1].Range.Text);

            }
            doc.Save();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool gtTT = false, gtGs1 = false, gtGs2 =false, gtBv = false, gtLx= false;
            if(cbGtTT.SelectedIndex == 0) gtTT = true;
            if(cbGtGs1.SelectedIndex == 0) gtGs1 = true;
            if(cbGtGs2.SelectedIndex == 0) gtGs2 = true;
            if(cbGtBv.SelectedIndex == 0) gtBv = true;
            if(cbGtLx.SelectedIndex == 0) gtLx = true;

            try
            {
                ToVanChuyenDAL.DV_TOVANCHUYEN_UPDATE(
                    Thong_tin_dang_nhap.ma_pb,
                    txtQuyetDinh.Text,
                    gtTT,
                    gtGs1,
                    gtGs2,
                    gtBv,
                    gtLx,
                    txtTenToTruong.Text,
                    txtTenGiamSat1.Text,
                    txtTenGiamSat2.Text,
                    txtTenBaoVe.Text,
                    txtTenLaiXe.Text,
                    txtChucVuToTruong.Text,
                    txtChucVuGiamSat1.Text,
                    txtChucVuGiamSat2.Text,
                    txtChucVuBaoVe.Text,
                    txtChucVuLaiXe.Text,
                    txtCMNDToTruong.Text,
                    txtCMNDGiamSat1.Text,
                    txtCMNDGiamSat2.Text,
                    txtCMNDBaoVe.Text,
                    txtCMNDLaiXe.Text,
                    txtNgayCapToTruong.Text,
                    txtNgayCapGiamSat1.Text,
                    txtNgayCapGiamSat2.Text,
                    txtNgayCapBaoVe.Text,
                    txtNgayCapLaiXe.Text,
                    txtNoiCapToTruong.Text,
                    txtNoiCapGiamSat1.Text,
                    txtNoiCapGiamSat2.Text,
                    txtNoiCapBaoVe.Text,
                    txtNoiCapLaiXe.Text,
                    txtLoaiHang.Text,
                    txtNoiDen.Text,
                    txtPhuongTien.Text
                    );
            }
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
            }
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
        }

        private void btnXoaToTruong_Click(object sender, EventArgs e)
        {
            cbGtTT.SelectedIndex = 0;
            txtTenToTruong.Text = "";
            txtChucVuToTruong.Text = "";
            txtCMNDToTruong.Text = "";
            txtNgayCapToTruong.Text = "";
            txtNoiCapToTruong.Text = "";
        }

        private void btnXoaGiamSat1_Click(object sender, EventArgs e)
        {
            cbGtGs1.SelectedIndex = 0;
            txtTenGiamSat1.Text = "";
            txtChucVuGiamSat1.Text = "";
            txtCMNDGiamSat1.Text = "";
            txtNgayCapGiamSat1.Text = "";
            txtNoiCapGiamSat1.Text = "";
        }

        private void btnXoaGiamSat2_Click(object sender, EventArgs e)
        {
            cbGtGs2.SelectedIndex = 0;
            txtTenGiamSat2.Text = "";
            txtChucVuGiamSat2.Text = "";
            txtCMNDGiamSat2.Text = "";
            txtNgayCapGiamSat2.Text = "";
            txtNoiCapGiamSat2.Text = "";
        }

        private void btnXoaLaiXe_Click(object sender, EventArgs e)
        {
            cbGtLx.SelectedIndex = 0;
            txtTenLaiXe.Text = "";
            txtChucVuLaiXe.Text = "";
            txtCMNDLaiXe.Text = "";
            txtNgayCapLaiXe.Text = "";
            txtNoiCapLaiXe.Text = "";
        }

        private void btnXoaBaoVe_Click(object sender, EventArgs e)
        {
            cbGtBv.SelectedIndex = 0;
            txtTenBaoVe.Text = "";
            txtChucVuBaoVe.Text = "";
            txtCMNDBaoVe.Text = "";
            txtNgayCapBaoVe.Text = "";
            txtNoiCapBaoVe.Text = "";
        }

    }
}
