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

namespace CRM.GUI_DV
{
    public partial class frmTiepQuy : Form
    {
        static string fileNameTiepQuy = "THE_49_GIAY_DE_NGHI_TIEP_QUY";

        int soTo50, soTo100, soTo200, soTo500, tt50, tt100, tt200, tt500, tong;
        List<string> listNguon, listDich;

        public frmTiepQuy()
        {
            InitializeComponent();
            listNguon = new List<string>();
            listDich = new List<string>();
            txtTenCanBo.Text = Thong_tin_dang_nhap.ho_ten;
            try
            {
                DataTable dt = KiemQuyDAL.DV_ATM_MACN();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbATMID.Items.Add(dt.Rows[i]["ID"].ToString());
                }
                if (cbATMID.Items.Count > 0)
                    cbATMID.SelectedIndex = 0;
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        private void txt50_Leave(object sender, EventArgs e)
        {
            GetNumber();
        }

        void GetNumber()
        {
            if (string.IsNullOrEmpty(txt50.Text))
                txt50.Text = "0";
            if (string.IsNullOrEmpty(txt100.Text))
                txt100.Text = "0";
            if (string.IsNullOrEmpty(txt200.Text))
                txt200.Text = "0";
            if (string.IsNullOrEmpty(txt500.Text))
                txt500.Text = "0";
            soTo50 = Convert.ToInt32(XoaDauPhay(txt50.Text));
            soTo100 = Convert.ToInt32(XoaDauPhay(txt100.Text));
            soTo200 = Convert.ToInt32(XoaDauPhay(txt200.Text));
            soTo500 = Convert.ToInt32(XoaDauPhay(txt500.Text));
            tt50 = soTo50 * 50000;
            tt100 = soTo100 * 100000;
            tt200 = soTo200 * 200000;
            tt500 = soTo500 * 500000;
            tong = tt50 + tt100 + tt200 + tt500;
            txtTong.Text = CommonMethods.ThemDauPhay(tong.ToString());
        }

        private void txt100_Leave(object sender, EventArgs e)
        {
            GetNumber();
        }

        private void txt200_Leave(object sender, EventArgs e)
        {
            GetNumber();
        }

        private void txt500_Leave(object sender, EventArgs e)
        {
            GetNumber();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                TiepQuyDAL.DV_INSERT_TIEPQUY(
                DateTime.Now,
                Thong_tin_dang_nhap.ma_cn,
                Thong_tin_dang_nhap.maNV,
                cbATMID.SelectedItem.ToString(),
                soTo50,
                soTo100,
                soTo200,
                soTo500
                );
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
                return;
            }
            KhoiTaoTiepQuy();
            Thread th = new Thread(TaoFileTiepQuy);
            th.Start();
        }

        void KhoiTaoTiepQuy()
        {
            listNguon.Clear();
            listDich.Clear();

            //TT CHUNG
            listDich.Add("<CHI_NHANH>");
            listNguon.Add(Thong_tin_dang_nhap.ten_cn.ToUpper());
            listDich.Add("<KINH_GUI>");
            listNguon.Add("Giám đốc " + Thong_tin_dang_nhap.ten_cn);
            listDich.Add("<NGAY>");
            listNguon.Add(DateTime.Now.Day.ToString());
            listDich.Add("<THANG>");
            listNguon.Add(DateTime.Now.Month.ToString());
            listDich.Add("<NAM>");
            listNguon.Add(DateTime.Now.Year.ToString());

            listDich.Add("<ATM_ID>");
            listNguon.Add(cbATMID.SelectedItem.ToString());
            listDich.Add("<DIA_DIEM_ATM>");
            try
            {
                listNguon.Add(TiepQuyDAL.DV_GET_DIADIEM_ATM(cbATMID.SelectedItem.ToString()));
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
                return;
            }

            listDich.Add("<NGUOI_DE_NGHI>");
            listNguon.Add(txtTenCanBo.Text);

            listDich.Add("<PHONG>");
            listNguon.Add(Thong_tin_dang_nhap.tenPb + "" + Thong_tin_dang_nhap.ten_cn);

            //So to tiep quy
            listDich.Add("<SO_TO_50>");
            listNguon.Add(CommonMethods.ThemDauPhay(soTo50.ToString()));
            listDich.Add("<SO_TO_100>");
            listNguon.Add(CommonMethods.ThemDauPhay(soTo100.ToString()));
            listDich.Add("<SO_TO_200>");
            listNguon.Add(CommonMethods.ThemDauPhay(soTo200.ToString()));
            listDich.Add("<SO_TO_500>");
            listNguon.Add(CommonMethods.ThemDauPhay(soTo500.ToString()));

            listDich.Add("<THANH_TIEN_50>");
            listNguon.Add(CommonMethods.ThemDauPhay(tt50.ToString()));
            listDich.Add("<THANH_TIEN_100>");
            listNguon.Add(CommonMethods.ThemDauPhay(tt100.ToString()));
            listDich.Add("<THANH_TIEN_200>");
            listNguon.Add(CommonMethods.ThemDauPhay(tt200.ToString()));
            listDich.Add("<THANH_TIEN_500>");
            listNguon.Add(CommonMethods.ThemDauPhay(tt500.ToString()));

            listDich.Add("<TONG_TIEN_SO>");
            listNguon.Add(CommonMethods.ThemDauPhay(tong.ToString()));

            listDich.Add("<TONG_TIEN_CHU>");
            listNguon.Add(CommonMethods.FirstCharToUpper(CommonMethods.ChuyenSoSangChu(tong.ToString())));
        }

        void TaoFileTiepQuy()
        {
            saveFileTiepQuy.Filter = "Word Documents|*.docx";

            string subFolder = @"BienBanTiepQuy\";
            if (!CommonMethods.SubFolderExist(subFolder))
                CommonMethods.CreateSubFolder(subFolder);

            string TemplateFileLocation = CommonMethods.TemplateFileLocation(fileNameTiepQuy + ".docx");
            string saveFileLocation = CommonMethods.SaveFileLocation(subFolder + fileNameTiepQuy + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".docx");


            if (CommonMethods.CreateWordDocument(TemplateFileLocation, saveFileLocation, listDich, listNguon))
            {
                MessageBox.Show("File đã được tạo tại đường dẫn: " + saveFileLocation, "Tạo file thành công");
                Thread.Sleep(500);
                OpenFileWord(saveFileLocation);
            }

        }

        void OpenFileWord(string fileLocation)
        {
            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(fileLocation);
            ap.Visible = true;
        }

        string XoaDauPhay(string s)
        {
            return s.Replace(",", "");
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


        private void txt50_TextChanged(object sender, EventArgs e)
        {
            TachSo(txt50);
        }

        private void txt100_TextChanged(object sender, EventArgs e)
        {
            TachSo(txt100);
        }

        private void txt200_TextChanged(object sender, EventArgs e)
        {
            TachSo(txt200);
        }

        private void txt500_TextChanged(object sender, EventArgs e)
        {
            TachSo(txt500);
        }
    }
}
