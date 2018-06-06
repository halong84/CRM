using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRM.Entities.DV;
using CRM.DAL.DV;
using CRM.Utilities.DV;
using System.Threading;
using System.Globalization;
using Word = Microsoft.Office.Interop.Word;

namespace CRM.GUI_DV
{
    public partial class frmKiemQuy : Form
    {
        public static string fileNameKiemQuy = "THE_51_BIEN_BAN_KIEM_QUY";

        List<User> users, usersKiemQuy;
        List<string> listNguon, listDich;

        DateTime tuNgay, denNgay, timeTCO, timeCO, timeCI, timeSC, timeCE;

        string tpKiemQuy = "";

        Int64 qtTienTHT, qtMonCHT, qtTienCHT,
            soDuTCO, soDuCO, soDuCI,
            fimiSC50, fimiSC100, fimiSC200, fimiSC500,
            fimiCE50, fimiCE100, fimiCE200, fimiCE500, fimiCETong,
            demB50, demB100, demB200, demB500,
            demCC50, demCC100, demCC200, demCC500,
            demCL50, demCL100, demCL200, demCL500,
            demTong, thuaThieu;

        public frmKiemQuy()
        {
            InitializeComponent();
            listNguon = new List<string>();
            listDich = new List<string>();
            LayTTCanBo();
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

        void LayTTCanBo()
        {
            users = new List<User>();
            usersKiemQuy = new List<User>();
            DataTable dt = new DataTable();
            try
            {
                dt = KiemQuyDAL.DV_DSNhanVien_MaCN(Thong_tin_dang_nhap.ma_cn);
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var u = new User(dt.Rows[i]);
                users.Add(u);
                cbCanBo.Items.Add(u.tennv + " - " + u.chucvu);
            }
        }

        void XoaCanBoKiemQuy()
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbCanBo.SelectedIndex < 0) return;
            foreach (var c in usersKiemQuy)
                if (c == users[cbCanBo.SelectedIndex]) return;
            usersKiemQuy.Add(users[cbCanBo.SelectedIndex]);
            string chucDanh = " (Trưởng ban)";
            if (usersKiemQuy.Count > 1) chucDanh = " (Thành viên)";
            txtCanBoKiemQuy.Text += users[cbCanBo.SelectedIndex].tennv + chucDanh + "; ";
            tpKiemQuy += users[cbCanBo.SelectedIndex].manv + ",";
        }


        void KhoiTaoKiemQuy()
        {
            listNguon.Clear();
            listDich.Clear();

            //TT CHUNG
            listDich.Add("<CHI_NHANH>");
            listNguon.Add(Thong_tin_dang_nhap.ten_cn.ToUpper());

            listDich.Add("<NGAY>");
            listNguon.Add(DateTime.Now.Day.ToString());
            listDich.Add("<THANG>");
            listNguon.Add(DateTime.Now.Month.ToString());
            listDich.Add("<NAM>");
            listNguon.Add(DateTime.Now.Year.ToString());

            listDich.Add("<TU_NGAY>");
            listNguon.Add(dtpTuNgay.Value.ToString("hh")+"h"+dtpTuNgay.Value.ToString("mm")+" ngày "+ dtpTuNgay.Value.ToString("dd/MM/yyyy"));
            tuNgay = dtpTuNgay.Value;
            listDich.Add("<DEN_NGAY>");
            listNguon.Add(dtpDenNgay.Value.ToString("hh") + "h" + dtpDenNgay.Value.ToString("mm") + " ngày " + dtpDenNgay.Value.ToString("dd/MM/yyyy"));
            denNgay = dtpDenNgay.Value;

            listDich.Add("<ATM_ID>");
            listNguon.Add(cbATMID.SelectedItem.ToString());
            
            

            //GD Quoc te/NAPAS
            listDich.Add("<TIEN_GDTQT_THT>");
            qtTienTHT = Convert.ToInt64(XoaDauPhay(txtNAPAS1.Text));
            listNguon.Add(txtNAPAS1.Text);
            listDich.Add("<MON_GDTQT_CHT>");
            qtMonCHT = Convert.ToInt64(XoaDauPhay(txtNAPAS2.Text));
            listNguon.Add(txtNAPAS2.Text);
            listDich.Add("<TIEN_GDTQT_CHT>");
            qtTienCHT = Convert.ToInt64(XoaDauPhay(txtNAPAS3.Text));
            listNguon.Add(txtNAPAS3.Text);

            //IPCAS
            listDich.Add("<T_CO>");
            //timeTCO = DateTime.ParseExact(txtTimeIPCAS1.Text, "HH:mm", CultureInfo.InvariantCulture);
            //listNguon.Add(txtTimeIPCAS1.Text);
            timeTCO = dtpTruocCO.Value;
            listNguon.Add(timeTCO.ToString("HH:mm"));
            listDich.Add("<CO>");
            //timeCO = DateTime.ParseExact(txtTimeIPCAS2.Text, "HH:mm", CultureInfo.InvariantCulture);
            //listNguon.Add(txtTimeIPCAS2.Text);
            timeCO = dtpCO.Value;
            listNguon.Add(timeCO.ToString("HH:mm"));
            listDich.Add("<CI>");
            //timeCI = DateTime.ParseExact(txtTimeIPCAS3.Text, "HH:mm", CultureInfo.InvariantCulture);
            //listNguon.Add(txtTimeIPCAS3.Text);
            timeCI = dtpCI.Value;
            listNguon.Add(timeCI.ToString("HH:mm"));
            listDich.Add("<DU_T_CO>");
            soDuTCO = Convert.ToInt64(XoaDauPhay(txtSoDuIPCAS1.Text));
            listNguon.Add(txtSoDuIPCAS1.Text);
            listDich.Add("<DU_CO>");
            soDuCO = Convert.ToInt64(XoaDauPhay(txtSoDuIPCAS2.Text));
            listNguon.Add(txtSoDuIPCAS2.Text);
            listDich.Add("<DU_CI>");
            soDuCI = Convert.ToInt64(XoaDauPhay(txtSoDuIPCAS3.Text));
            listNguon.Add(txtSoDuIPCAS3.Text);
            listDich.Add("<GC1>");
            listNguon.Add(txtGhiChuIPCAS1.Text);
            listDich.Add("<GC2>");
            listNguon.Add(txtGhiChuIPCAS2.Text);
            listDich.Add("<GC3>");
            listNguon.Add(txtGhiChuIPCAS3.Text);

            //FIMI

            //STARTING CASH
            listDich.Add("<TIME_SC>"); //TIME
            //timeSC = DateTime.ParseExact(txtTimeFIMIStart.Text, "HH:mm", CultureInfo.InvariantCulture);
            //listNguon.Add(txtTimeFIMIStart.Text);
            timeSC = dtpFIMISC.Value;
            listNguon.Add(timeSC.ToString("HH:mm"));
            listDich.Add("<FIMI_SC50>"); //SO TO
            fimiSC50 = Convert.ToInt64(XoaDauPhay(txtFIMIStart50.Text));
            listNguon.Add(txtFIMIStart50.Text);
            listDich.Add("<FIMI_SC100>");
            fimiSC100 = Convert.ToInt64(XoaDauPhay(txtFIMIStart100.Text));
            listNguon.Add(txtFIMIStart100.Text);
            listDich.Add("<FIMI_SC200>");
            fimiSC200 = Convert.ToInt64(XoaDauPhay(txtFIMIStart200.Text));
            listNguon.Add(txtFIMIStart200.Text);
            listDich.Add("<FIMI_SC500>");
            fimiSC500 = Convert.ToInt64(XoaDauPhay(txtFIMIStart500.Text));
            listNguon.Add(txtFIMIStart500.Text);
            listDich.Add("<FIMI_SC_TONG>");
            listNguon.Add(CommonMethods.ThemDauPhay(
            (Convert.ToInt64(XoaDauPhay(txtFIMIStart50.Text)) +
            Convert.ToInt64(XoaDauPhay(txtFIMIStart100.Text)) +
            Convert.ToInt64(XoaDauPhay(txtFIMIStart200.Text)) +
            Convert.ToInt64(XoaDauPhay(txtFIMIStart500.Text))
            ).ToString()
            ));

            listDich.Add("<FIMI_SC_TT50>");// THANH TIEN
            listNguon.Add(CommonMethods.ThemDauPhay((Convert.ToInt64(XoaDauPhay(txtFIMIStart50.Text)) * 50000).ToString()));
            listDich.Add("<FIMI_SC_TT100>");
            listNguon.Add(CommonMethods.ThemDauPhay((Convert.ToInt64(XoaDauPhay(txtFIMIStart100.Text)) * 100000).ToString()));
            listDich.Add("<FIMI_SC_TT200>");
            listNguon.Add(CommonMethods.ThemDauPhay((Convert.ToInt64(XoaDauPhay(txtFIMIStart200.Text)) * 200000).ToString()));
            listDich.Add("<FIMI_SC_TT500>");
            listNguon.Add(CommonMethods.ThemDauPhay((Convert.ToInt64(XoaDauPhay(txtFIMIStart500.Text)) * 500000).ToString()));
            listDich.Add("<FIMI_SC_TT_TONG>");
            listNguon.Add(CommonMethods.ThemDauPhay((
            Convert.ToInt64(XoaDauPhay(txtFIMIStart50.Text)) * 50000 +
            Convert.ToInt64(XoaDauPhay(txtFIMIStart100.Text)) * 100000 +
            Convert.ToInt64(XoaDauPhay(txtFIMIStart200.Text)) * 200000 +
            Convert.ToInt64(XoaDauPhay(txtFIMIStart500.Text)) * 500000
            ).ToString()
            ));

            //CASH END
            listDich.Add("<TIME_CE>"); //TIME
            //timeCE = DateTime.ParseExact(txtTimeFIMIEnd.Text, "HH:mm", CultureInfo.InvariantCulture);
            //listNguon.Add(txtTimeFIMIEnd.Text);
            timeCE = dtpFIMICE.Value;
            listNguon.Add(timeCE.ToString("HH:mm"));
            listDich.Add("<FIMI_CE50>");//SO TO
            fimiCE50 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd50.Text));
            listNguon.Add(txtFIMIEnd50.Text);
            listDich.Add("<FIMI_CE100>");
            fimiCE100 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd100.Text));
            listNguon.Add(txtFIMIEnd100.Text);
            listDich.Add("<FIMI_CE200>");
            fimiCE200 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd200.Text));
            listNguon.Add(txtFIMIEnd200.Text);
            listDich.Add("<FIMI_CE500>");
            fimiCE500 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd500.Text));
            listNguon.Add(txtFIMIEnd500.Text);
            listDich.Add("<FIMI_CE_TONG>");
            listNguon.Add(CommonMethods.ThemDauPhay(
            (Convert.ToInt64(XoaDauPhay(txtFIMIEnd50.Text)) +
            Convert.ToInt64(XoaDauPhay(txtFIMIEnd100.Text)) +
            Convert.ToInt64(XoaDauPhay(txtFIMIEnd200.Text)) +
            Convert.ToInt64(XoaDauPhay(txtFIMIEnd500.Text))
            ).ToString()
            ));

            listDich.Add("<FIMI_CE_TT50>");
            listNguon.Add(CommonMethods.ThemDauPhay((fimiCE50 * 50000).ToString()));
            listDich.Add("<FIMI_CE_TT100>");
            listNguon.Add(CommonMethods.ThemDauPhay((fimiCE100 * 100000).ToString()));
            listDich.Add("<FIMI_CE_TT200>");
            listNguon.Add(CommonMethods.ThemDauPhay((fimiCE200 * 200000).ToString()));
            listDich.Add("<FIMI_CE_TT500>");
            listNguon.Add(CommonMethods.ThemDauPhay((fimiCE500 * 500000).ToString()));
            listDich.Add("<FIMI_CE_TT_TONG>");
            long cashEnd = 
            Convert.ToInt64(XoaDauPhay(txtFIMIEnd50.Text)) * 50000 +
            Convert.ToInt64(XoaDauPhay(txtFIMIEnd100.Text)) * 100000 +
            Convert.ToInt64(XoaDauPhay(txtFIMIEnd200.Text)) * 200000 +
            Convert.ToInt64(XoaDauPhay(txtFIMIEnd500.Text)) * 500000;
            listNguon.Add(CommonMethods.ThemDauPhay(cashEnd.ToString()));
            

            //KIEM DEM THUC TE

            //HOP TIEN CHINH
            listDich.Add("<B50>");
            demB50 = Convert.ToInt64(XoaDauPhay(txtDemChinh50.Text));
            listNguon.Add(txtDemChinh50.Text);
            listDich.Add("<B100>");
            demB100 = Convert.ToInt64(XoaDauPhay(txtDemChinh100.Text));
            listNguon.Add(txtDemChinh100.Text);
            listDich.Add("<B200>");
            demB200 = Convert.ToInt64(XoaDauPhay(txtDemChinh200.Text));
            listNguon.Add(txtDemChinh200.Text);
            listDich.Add("<B500>");
            demB500 = Convert.ToInt64(XoaDauPhay(txtDemChinh500.Text));
            listNguon.Add(txtDemChinh500.Text);
            listDich.Add("<BTONG>");
            listNguon.Add(CommonMethods.ThemDauPhay((
                Convert.ToInt64(XoaDauPhay(txtDemChinh50.Text)) +
                Convert.ToInt64(XoaDauPhay(txtDemChinh100.Text)) +
                Convert.ToInt64(XoaDauPhay(txtDemChinh200.Text)) +
                Convert.ToInt64(XoaDauPhay(txtDemChinh500.Text))).ToString()
                ));

            //HOP TIEN LOAI
            listDich.Add("<C_NC50>"); //NGAN CHINH
            demCC50 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh50.Text));
            listNguon.Add(txtDemLoaiChinh50.Text);
            listDich.Add("<C_NC100>");
            demCC100 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh100.Text));
            listNguon.Add(txtDemLoaiChinh100.Text);
            listDich.Add("<C_NC200>");
            demCC200 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh200.Text));
            listNguon.Add(txtDemLoaiChinh200.Text);
            listDich.Add("<C_NC500>");
            demCC500 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh500.Text));
            listNguon.Add(txtDemLoaiChinh500.Text);
            listDich.Add("<C_NCTONG>");
            listNguon.Add(CommonMethods.ThemDauPhay((
               Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh50.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh100.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh200.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh500.Text))).ToString()
               ));

            listDich.Add("<C_TH50>");//NGAN THU HOI
            demCL50 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi50.Text));
            listNguon.Add(txtDemLoaiThuHoi50.Text);
            listDich.Add("<C_TH100>");
            demCL100 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi100.Text));
            listNguon.Add(txtDemLoaiThuHoi100.Text);
            listDich.Add("<C_TH200>");
            demCL200 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi200.Text));
            listNguon.Add(txtDemLoaiThuHoi200.Text);
            listDich.Add("<C_TH500>");
            demCL500 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi500.Text));
            listNguon.Add(txtDemLoaiThuHoi500.Text);
            listDich.Add("<C_THTONG>");
            listNguon.Add(CommonMethods.ThemDauPhay((
               Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi50.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi100.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi200.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi500.Text))).ToString()
               ));

            //TONG TIEN
            listDich.Add("<TONG50>");
            long tong50 =
                (Convert.ToInt64(XoaDauPhay(txtDemChinh50.Text)) +
                Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh50.Text)) +
                Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi50.Text))) * 50000;
            listNguon.Add(CommonMethods.ThemDauPhay(tong50.ToString()));

            listDich.Add("<TONG100>");
            long tong100 = (
                Convert.ToInt64(XoaDauPhay(txtDemChinh100.Text)) +
                Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh100.Text)) +
                Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi100.Text))) * 100000;
            listNguon.Add(CommonMethods.ThemDauPhay(tong100.ToString()));

            listDich.Add("<TONG200>");
            long tong200 = (
               Convert.ToInt64(XoaDauPhay(txtDemChinh200.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh200.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi200.Text))) * 200000;
            listNguon.Add(CommonMethods.ThemDauPhay(tong200.ToString()));

            listDich.Add("<TONG500>");
            long tong500 = (
               Convert.ToInt64(XoaDauPhay(txtDemChinh500.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh500.Text)) +
               Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi500.Text))) * 500000;
            listNguon.Add(CommonMethods.ThemDauPhay(tong500.ToString()));

            listDich.Add("<TONG>");
            long tong = tong50 + tong100 + tong200 + tong500;
            listNguon.Add(CommonMethods.ThemDauPhay(tong.ToString()));

            listDich.Add("<TONG_BANGCHU>");
            listNguon.Add(CommonMethods.FirstCharToUpper(CommonMethods.ChuyenSoSangChu(tong.ToString())));

            //KET LUAN
            thuaThieu = Math.Abs(tong - cashEnd);
            listDich.Add("<TIEN_THUA_THIEU>");
            listNguon.Add(txtThuaThieu.Text);
            listDich.Add("<NGUYEN_NHAN>");
            listNguon.Add(txtNguyenNhan.Text);
            listDich.Add("<HUONG_XL>");
            listNguon.Add(txtKhacPhuc.Text);
        }

        void TaoFileKiemQuy()
        {
            saveFileKiemQuy.Filter = "Word Documents|*.docx";

            string subFolder = @"BienBanKiemQuy\";
            if (!CommonMethods.SubFolderExist(subFolder))
                CommonMethods.CreateSubFolder(subFolder);

            string TemplateFileLocation = CommonMethods.TemplateFileLocation(fileNameKiemQuy + ".docx");
            string saveFileLocation = CommonMethods.SaveFileLocation(subFolder + fileNameKiemQuy + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".docx");


            if (CommonMethods.CreateWordDocument(TemplateFileLocation, saveFileLocation, listDich, listNguon))
            {
                MessageBox.Show("File đã được tạo tại đường dẫn: " + saveFileLocation, "Tạo file thành công");
                //Thread.Sleep(500);
                OpenFileWord(saveFileLocation);
            }
        }

        void PutStringIntoTable(Word.Document doc)
        {
            object oMissing = System.Reflection.Missing.Value;
            Word.Table tb = doc.Tables[2];
            for (int i = 0; i < usersKiemQuy.Count; i++)
            {
                string pb = Thong_tin_dang_nhap.ten_cn;
                string hoTen = "- "+ usersKiemQuy[i].tennv;
                string chucVu = "- "+usersKiemQuy[i].chucvu + " " + Thong_tin_dang_nhap.tenPb;
                if (usersKiemQuy[i].chucvu == "Giám đốc" || usersKiemQuy[i].chucvu == "Phó Giám đốc")
                    chucVu = "- " + usersKiemQuy[i].chucvu.Replace("Phó Giám đốc", "PGĐ").Replace("Giám đốc", "GĐ") + " " + Thong_tin_dang_nhap.ten_cn.Replace("chi nhánh", "CN").Replace("Thành phố", "tp");
                else if (usersKiemQuy[i].chucvu == "Nhân viên") chucVu = "- " + "Cán bộ nghiệp vụ thẻ";
                string chucDanh = "- " + "Thành viên";
                if (usersKiemQuy[i] == usersKiemQuy[0])
                    chucDanh = "- " + "Trưởng ban";

                tb.Rows[i + 1].Cells[1].Range.Text = hoTen;
                tb.Rows[i + 1].Cells[2].Range.Text = chucVu;
                tb.Rows[i + 1].Cells[3].Range.Text = chucDanh;
                if (i != usersKiemQuy.Count - 1)
                    tb.Rows.Add(oMissing);
                doc.Save();
            }
        }

        void OpenFileWord(string fileLocation)
        {
            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(fileLocation);
            PutStringIntoTable(document);
            ap.Visible = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Luu CSDL
            KhoiTaoKiemQuy();
            try
            {
                KiemQuyDAL.DV_INSERT_KIEMQUY(
                    tuNgay,
                    denNgay,
                    cbATMID.SelectedItem.ToString(),
                    tpKiemQuy,
                    qtTienTHT,
                    qtMonCHT,
                    qtTienCHT,
                    timeTCO,
                    timeCO,
                    timeCI,
                    soDuTCO,
                    soDuCO,
                    soDuCI,
                    txtGhiChuIPCAS1.Text,
                    txtGhiChuIPCAS2.Text,
                    txtGhiChuIPCAS3.Text,
                    timeSC,
                    timeCE,
                    fimiSC50,
                    fimiSC100,
                    fimiSC200,
                    fimiSC500,
                    fimiCE50,
                    fimiCE100,
                    fimiCE200,
                    fimiCE500,
                    demB50,
                    demB100,
                    demB200,
                    demB500,
                    demCC50,
                    demCC100,
                    demCC200,
                    demCC500,
                    demCL50,
                    demCL100,
                    demCL200,
                    demCL500,
                    thuaThieu,
                    txtNguyenNhan.Text,
                    txtKhacPhuc.Text
                    );
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
                return;
            }
            //Tao file
            TaoFileKiemQuy();
            //Thread th = new Thread(TaoFileKiemQuy);
            //th.Start();
        }

        private void frmKiemQuy_Load(object sender, EventArgs e)
        {

        }

        void TinhChenhLech()
        {
            if (string.IsNullOrEmpty(txtFIMIEnd50.Text))
                txtFIMIStart50.Text = "0";
            if (string.IsNullOrEmpty(txtFIMIEnd100.Text))
                txtFIMIStart100.Text = "0";
            if (string.IsNullOrEmpty(txtFIMIEnd200.Text))
                txtFIMIStart200.Text = "0";
            if (string.IsNullOrEmpty(txtFIMIEnd500.Text))
                txtFIMIStart500.Text = "0";

            fimiCE50 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd50.Text));
            fimiCE100 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd100.Text));
            fimiCE200 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd200.Text));
            fimiCE500 = Convert.ToInt64(XoaDauPhay(txtFIMIEnd500.Text));

            fimiCETong = (fimiCE50 + fimiCE100 * 2 + fimiCE200 * 4 + fimiCE500 * 10) * 50000;

            if (string.IsNullOrEmpty(txtDemChinh50.Text))
                txtDemChinh50.Text = "0";
            if (string.IsNullOrEmpty(txtDemChinh100.Text))
                txtDemChinh100.Text = "0";
            if (string.IsNullOrEmpty(txtDemChinh200.Text))
                txtDemChinh200.Text = "0";
            if (string.IsNullOrEmpty(txtDemChinh500.Text))
                txtDemChinh500.Text = "0";
            demB50 = Convert.ToInt64(XoaDauPhay(txtDemChinh50.Text));
            demB100 = Convert.ToInt64(XoaDauPhay(txtDemChinh100.Text));
            demB200 = Convert.ToInt64(XoaDauPhay(txtDemChinh200.Text));
            demB500 = Convert.ToInt64(XoaDauPhay(txtDemChinh500.Text));

            if (string.IsNullOrEmpty(txtDemLoaiChinh50.Text))
                txtDemLoaiChinh50.Text = "0";
            if (string.IsNullOrEmpty(txtDemLoaiChinh100.Text))
                txtDemLoaiChinh100.Text = "0";
            if (string.IsNullOrEmpty(txtDemLoaiChinh200.Text))
                txtDemLoaiChinh200.Text = "0";
            if (string.IsNullOrEmpty(txtDemLoaiChinh500.Text))
                txtDemLoaiChinh500.Text = "0";
            demCC50 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh50.Text));
            demCC100 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh100.Text));
            demCC200 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh200.Text));
            demCC500 = Convert.ToInt64(XoaDauPhay(txtDemLoaiChinh500.Text));

            if (string.IsNullOrEmpty(txtDemLoaiThuHoi50.Text))
                txtDemLoaiThuHoi50.Text = "0";
            if (string.IsNullOrEmpty(txtDemLoaiThuHoi100.Text))
                txtDemLoaiThuHoi100.Text = "0";
            if (string.IsNullOrEmpty(txtDemLoaiThuHoi200.Text))
                txtDemLoaiThuHoi200.Text = "0";
            if (string.IsNullOrEmpty(txtDemLoaiThuHoi500.Text))
                txtDemLoaiThuHoi500.Text = "0";
            demCL50 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi50.Text));
            demCL100 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi100.Text));
            demCL200 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi200.Text));
            demCL500 = Convert.ToInt64(XoaDauPhay(txtDemLoaiThuHoi500.Text));

            demTong = 50000 * (
                (demB50 + demCC50 + demCL50) +
                (demB100 + demCC100 + demCL100) * 2 +
                (demB200 + demCC200 + demCL200) * 4 +
                (demB500 + demCC500 + demCL500) * 10
                );
            txtThuaThieu.Text = CommonMethods.ThemDauPhay(Math.Abs(demTong - fimiCETong).ToString());
        }

        private void txtDemLoaiThuHoi500_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtCanBoKiemQuy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtCanBoKiemQuy.Text = "";
            usersKiemQuy.Clear();
            tpKiemQuy = "";
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
            string s2 = s.Replace(",", "");
            return s2;
        }
       

        private void txtNAPAS1_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtNAPAS1);
        }

        private void txtNAPAS2_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtNAPAS2);
        }

        private void txtNAPAS3_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtNAPAS3);
        }

        private void txtSoDuIPCAS1_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtSoDuIPCAS1);
        }

        private void txtSoDuIPCAS2_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtSoDuIPCAS2);
        }

        private void txtSoDuIPCAS3_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtSoDuIPCAS3);
        }

        private void txtFIMIStart50_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIStart50);
        }

        private void txtFIMIStart100_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIStart100);
        }

        private void txtFIMIStart200_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIStart200);
        }

        private void txtFIMIStart500_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIStart500);
        }

        private void txtFIMIEnd50_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIEnd50);
        }

        private void txtFIMIEnd100_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIEnd100);
        }

        private void txtFIMIEnd200_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIEnd200);
        }

        private void txtFIMIEnd500_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtFIMIEnd500);
        }

        private void txtDemChinh50_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemChinh50);
        }

        private void txtDemChinh100_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemChinh100);
        }

        private void txtDemChinh200_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemChinh200);
        }

        private void txtDemChinh500_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemChinh500);
        }

        private void txtDemLoaiChinh50_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiChinh50);
        }

        private void txtDemLoaiChinh100_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiChinh100);
        }

        private void txtDemLoaiChinh200_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiChinh200);
        }

        private void txtDemLoaiChinh500_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiChinh500);
        }

        private void txtDemLoaiThuHoi50_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiThuHoi50);
        }

        private void txtDemLoaiThuHoi100_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiThuHoi100);
        }

        private void txtDemLoaiThuHoi200_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiThuHoi200);
        }

        private void txtDemLoaiThuHoi500_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtDemLoaiThuHoi500);
        }

        private void txtDemChinh50_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemChinh100_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemChinh200_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemChinh500_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemLoaiChinh50_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemLoaiChinh100_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemLoaiChinh200_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemLoaiChinh500_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemLoaiThuHoi50_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemLoaiThuHoi100_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void txtDemLoaiThuHoi200_Leave(object sender, EventArgs e)
        {
            TinhChenhLech();
        }  

    }
}
