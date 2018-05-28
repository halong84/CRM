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

        NhanVien nvTT, nvGS1, nvGS2, nvBV, nvLX;
        string maToTruong, maGiamSat1, maGiamSat2, maBaoVe, maLaixe;
        bool isExist = false; //Da ton tai thong tin tren CSDL hay chua

        public frmThanhLapToVanChuyen()
        {
            InitializeComponent();
            listDich = new List<string>();
            listNguon = new List<string>();

            DataTable dt = null;
            try
            {
                dt = ToVanChuyenDAL.DV_TOVANCHUYEN_MAPB(Thong_tin_dang_nhap.ma_pb);
                if (dt.Rows.Count > 0)
                {
                    isExist = true;
                    DataRow r = dt.Rows[0];
                    //To truong
                    maToTruong = r["MATOTRUONG"].ToString();
                    DataRow toTruong = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_MANV(maToTruong).Rows[0];
                    nvTT = new NhanVien(toTruong);
                    txtTenToTruong.Text = toTruong["HOTEN"].ToString();
                    txtChucVuToTruong.Text = toTruong["CHUCVU"].ToString();
                    txtCMNDToTruong.Text = toTruong["CMND"].ToString();
                    txtNgayCapToTruong.Text = toTruong["NGAYCAP"].ToString();
                    txtNoiCapToTruong.Text = toTruong["NOICAP"].ToString();
                    //Giam sat 1
                    maGiamSat1 = r["MAGIAMSAT1"].ToString();
                    DataRow gs1 = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_MANV(maGiamSat1).Rows[0];
                    nvGS1 = new NhanVien(gs1);
                    txtTenGiamSat1.Text = gs1["HOTEN"].ToString();
                    txtChucVuGiamSat1.Text = gs1["CHUCVU"].ToString();
                    txtCMNDGiamSat1.Text = gs1["CMND"].ToString();
                    txtNgayCapGiamSat1.Text = gs1["NGAYCAP"].ToString();
                    txtNoiCapGiamSat1.Text = gs1["NOICAP"].ToString();
                    //Giam sat 2
                    maGiamSat2 = r["MAGIAMSAT2"].ToString();
                    DataRow gs2 = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_MANV(maGiamSat2).Rows[0];
                    nvGS2 = new NhanVien(gs2);
                    txtTenGiamSat2.Text = gs2["HOTEN"].ToString();
                    txtChucVuGiamSat2.Text = gs2["CHUCVU"].ToString();
                    txtCMNDGiamSat2.Text = gs2["CMND"].ToString();
                    txtNgayCapGiamSat2.Text = gs2["NGAYCAP"].ToString();
                    txtNoiCapGiamSat2.Text = gs2["NOICAP"].ToString();
                    //Bao ve
                    maBaoVe = r["MABAOVE"].ToString();
                    DataRow bv = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_MANV(maBaoVe).Rows[0];
                    nvBV = new NhanVien(bv);
                    txtTenBaoVe.Text = bv["HOTEN"].ToString();
                    txtChucVuBaoVe.Text = bv["CHUCVU"].ToString();
                    txtCMNDBaoVe.Text = bv["CMND"].ToString();
                    txtNgayCapBaoVe.Text = bv["NGAYCAP"].ToString();
                    txtNoiCapBaoVe.Text = bv["NOICAP"].ToString();
                    //Lai xe
                    maLaixe = r["MALAIXE"].ToString();
                    DataRow lx = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_MANV(maLaixe).Rows[0];
                    nvLX = new NhanVien(lx);
                    txtTenLaiXe.Text = lx["HOTEN"].ToString();
                    txtChucVuLaiXe.Text = lx["CHUCVU"].ToString();
                    txtCMNDLaiXe.Text = lx["CMND"].ToString();
                    txtNgayCapLaiXe.Text = lx["NGAYCAP"].ToString();
                    txtNoiCapLaiXe.Text = lx["NOICAP"].ToString();
                    //Loai hang
                    txtLoaiHang.Text = r["LOAIHANG"].ToString();
                    //Bang so
                    txtBangSo.Text = r["BANGSO"].ToString();
                    //Phuong tien
                    txtPhuongTien.Text = r["PHUONGTIEN"].ToString();
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
                string gt = "Ông";
                string pb = Thong_tin_dang_nhap.ten_cn;
                if (nvTT != null)
                {
                    if (!nvTT.gioiTinh) gt = "Bà";
                    if (nvTT.maPb.Split('-')[1] != "01")
                    {
                        pb = Thong_tin_dang_nhap.tenPb;
                    }
                }
                toTruong = index + ". " + gt + ": " + txtTenToTruong.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDToTruong.Text + ", Ngày cấp: " + txtNgayCapToTruong.Text +
                    ", Nơi cấp: " + txtNoiCapToTruong.Text + "; Chức vụ: " + txtChucVuToTruong.Text + " " + pb + "; Chức danh: Tổ trưởng;";
                tb.Rows[index].Cells[1].Range.Text = toTruong;
            }

            if (string.IsNullOrEmpty(txtTenGiamSat1.Text))
            {
                tb.Rows.Add(oMissing);
                tb.Rows.Add(tb.Rows[index]);
                index++;
                string gt = "Ông";
                string pb = Thong_tin_dang_nhap.ten_cn;
                if (nvGS1 != null)
                {
                    if (!nvGS1.gioiTinh) gt = "Bà";
                    if (nvGS1.maPb.Split('-')[1] != "01")
                    {
                        pb = Thong_tin_dang_nhap.tenPb;
                    }
                }
                giamSat1 = index + ". " + gt + ": " + txtTenGiamSat1.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat1.Text + " Ngày cấp: " + txtNgayCapGiamSat1.Text +
                    " Nơi cấp: " + txtNoiCapGiamSat1.Text + "; Chức vụ: " + txtChucVuGiamSat1.Text + " " + pb + "; Chức danh: Giám sát;";
                tb.Rows[index].Cells[1].Range.Text = giamSat1;
            }

            if (string.IsNullOrEmpty(txtTenGiamSat2.Text))
            {
                tb.Rows.Add(oMissing);
                tb.Rows.Add(tb.Rows[index]);
                index++;
                string gt = "Ông";
                string pb = Thong_tin_dang_nhap.ten_cn;
                if (nvGS2 != null)
                {
                    if (!nvGS2.gioiTinh) gt = "Bà";
                    if (nvGS2.maPb.Split('-')[1] != "01")
                    {
                        pb = Thong_tin_dang_nhap.tenPb;
                    }
                }
                giamSat2 = index + ". " + gt + ": " + txtTenGiamSat2.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat2.Text + " Ngày cấp: " + txtNgayCapGiamSat2.Text +
                    " Nơi cấp: " + txtNoiCapGiamSat2.Text + "; Chức vụ: " + txtChucVuGiamSat2.Text + " " + pb + "; Chức danh: Giám sát;";
                tb.Rows[index].Cells[1].Range.Text = giamSat2;
            }

            //if (cbGiamSat1.SelectedItem != null)
            //{
            //    tb.Rows.Add(oMissing);
            //    tb.Rows.Add(tb.Rows[index]);
            //    index++;
            //    var u = users[cbGiamSat1.SelectedIndex];
            //    string gt = "Ông";
            //    if (!u.gioiTinh) gt = "Bà";
            //    string pb = Thong_tin_dang_nhap.ten_cn;
            //    if (u.chucvu != "Giám đốc" && u.chucvu != "Phó Giám đốc")
            //        pb = ToVanChuyenDAL.DV_LAYPHONGBAN(u.mapb);
            //    giamSat1 = index + ". " + gt + ": " + u.tennv + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat1.Text + " Ngày cấp: " + txtNgayCapGiamSat1.Text +
            //        " Nơi cấp: " + txtNoiCapGiamSat1.Text + "; Chức vụ: " + u.chucvu + " " + pb + "; Chức danh: Giám sát;";
            //    tb.Rows[index].Cells[1].Range.Text = giamSat1;
            //}

            //listDich.Add("<GIAM_SAT_2>");
            //if (cbGiamSat2.SelectedItem != null)
            //{
            //    tb.Rows.Add(oMissing);
            //    index++;
            //    var u = users[cbGiamSat2.SelectedIndex];
            //    string gt = "Ông";
            //    if (!u.gioiTinh) gt = "Bà";
            //    string pb = Thong_tin_dang_nhap.ten_cn;
            //    if (u.chucvu != "Giám đốc" && u.chucvu != "Phó Giám đốc")
            //        pb = ToVanChuyenDAL.DV_LAYPHONGBAN(u.mapb);
            //    giamSat2 = index + ". " + gt + ": " + u.tennv + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDGiamSat2.Text + " Ngày cấp: " + txtNgayCapGiamSat2.Text +
            //        " Nơi cấp: " + txtNoiCapGiamSat2.Text + "; Chức vụ: " + u.chucvu + " " + pb + "; Chức danh: Giám sát;";
            //    tb.Rows[index].Cells[1].Range.Text = giamSat2;
            //}
            //Lai xe
            if (!string.IsNullOrEmpty(txtTenLaiXe.Text))
            {
                tb.Rows.Add(oMissing);
                index++;
                laiXe = index + ". " + "Ông/Bà: " + txtTenLaiXe.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDLaiXe.Text +
                    " Ngày cấp: " + txtNgayCapLaiXe.Text + ",Nơi cấp: " + txtNoiCapLaiXe.Text + ";Chức danh: Lái xe;";
                tb.Rows[index].Cells[1].Range.Text = laiXe;
            }
            //Bao ve
            if (!string.IsNullOrEmpty(txtTenBaoVe.Text))
            {
                tb.Rows.Add(oMissing);
                index++;
                baoVe = index + ". " + "Ông/Bà: " + txtTenBaoVe.Text + ", Số hộ chiếu/CMND/CCCD: " + txtCMNDBaoVe.Text +
                    " Ngày cấp: " + txtNgayCapBaoVe.Text + ",Nơi cấp: " + txtNoiCapBaoVe.Text + ";Chức danh: Bảo vệ;";
                tb.Rows[index].Cells[1].Range.Text = baoVe;
            }
            doc.Save();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //if (isExist)
                
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

        private void btnThemToTruong_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow tt = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_CMND(txtCMNDTimKiem.Text).Rows[0];
                txtTenToTruong.Text = tt["HOTEN"].ToString();
                txtChucVuToTruong.Text = tt["CHUCVU"].ToString();
                txtCMNDToTruong.Text = tt["CMND"].ToString();
                txtNgayCapToTruong.Text = tt["NGAYCAP"].ToString();
                txtNoiCapToTruong.Text = tt["NOICAP"].ToString();
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        private void btnThemGiamSat1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow tt = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_CMND(txtCMNDTimKiem.Text).Rows[0];
                txtTenGiamSat1.Text = tt["HOTEN"].ToString();
                txtChucVuGiamSat1.Text = tt["CHUCVU"].ToString();
                txtCMNDGiamSat1.Text = tt["CMND"].ToString();
                txtNgayCapGiamSat1.Text = tt["NGAYCAP"].ToString();
                txtNoiCapGiamSat1.Text = tt["NOICAP"].ToString();
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        private void btnThemGiamSat2_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow tt = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_CMND(txtCMNDTimKiem.Text).Rows[0];
                txtTenGiamSat2.Text = tt["HOTEN"].ToString();
                txtChucVuGiamSat2.Text = tt["CHUCVU"].ToString();
                txtCMNDGiamSat2.Text = tt["CMND"].ToString();
                txtNgayCapGiamSat2.Text = tt["NGAYCAP"].ToString();
                txtNoiCapGiamSat2.Text = tt["NOICAP"].ToString();
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        private void btnThemLaiXe_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow tt = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_CMND(txtCMNDTimKiem.Text).Rows[0];
                txtTenLaiXe.Text = tt["HOTEN"].ToString();
                txtChucVuLaiXe.Text = tt["CHUCVU"].ToString();
                txtCMNDLaiXe.Text = tt["CMND"].ToString();
                txtNgayCapLaiXe.Text = tt["NGAYCAP"].ToString();
                txtNoiCapLaiXe.Text = tt["NOICAP"].ToString();
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        private void btnThemBaoVe_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow tt = ToVanChuyenDAL.DV_TOVANCHUYEN_NHANVIEN_CMND(txtCMNDTimKiem.Text).Rows[0];
                txtTenBaoVe.Text = tt["HOTEN"].ToString();
                txtChucVuBaoVe.Text = tt["CHUCVU"].ToString();
                txtCMNDBaoVe.Text = tt["CMND"].ToString();
                txtNgayCapBaoVe.Text = tt["NGAYCAP"].ToString();
                txtNoiCapBaoVe.Text = tt["NOICAP"].ToString();
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
            
        }


    }
}
