using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using N_MicrosoftExcelClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmPheduyet : Form
    {
        KETQUAXEPLOAIBUS kqxl_bus = new KETQUAXEPLOAIBUS();
        String strCmd = "";
        public static decimal diemtggt = 0, tdiem = 0;
        public static byte loaikh;
        string tuthang = "", denthang = "";

        public frmPheduyet()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDanhsach.Font = new Font("Arial", 9F);

            dtpFrom.CustomFormat = "MM/yyyy";
            DateTime dtCurrent = DateTime.Now;
            //dtpFrom.Value = dtCurrent.AddMonths(-1);
            dtpTo.CustomFormat = "MM/yyyy";
            //dtpTo.Value = dtCurrent.AddMonths(-1);
            if (dtCurrent.Month == 1)
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
            }
            else
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
            }

            btnSelectall.Enabled = false;
            btnDeselectall.Enabled = false;
            btnConfirm.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void frmPheduyet_Load(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
        }

        private void layDS()
        {
            if (optCN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten ";
                strCmd += " from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";

                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' ";

                if (rdbNotconfirm.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                }
                else if (rdbConfirmed.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
                }
                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Chọn", typeof(bool));
                dskh.Columns.Add(col);
                col = new DataColumn("Tình trạng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("PDTT", typeof(int));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGGT"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        row[12] = true;

                        string pheduyet = "";
                        if (Boolean.Parse(dt.Rows[i]["pheduyet"].ToString()) == true)
                        {
                            pheduyet = "Đã phê duyệt";
                        }
                        else
                        {
                            pheduyet = "Chưa phê duyệt";
                        }
                        row[13] = pheduyet;
                        int pheduyetTT = 0;
                        if (Boolean.Parse(dt.Rows[i]["PDTT"].ToString()) == true)
                        {
                            pheduyetTT = 1;
                        }
                        else
                        {
                            pheduyetTT = 0;
                        }
                        row[14] = pheduyetTT;

                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.Columns[0].ReadOnly = true;
                dgvDanhsach.Columns[1].ReadOnly = true;
                dgvDanhsach.Columns[2].ReadOnly = true;
                dgvDanhsach.Columns[3].ReadOnly = true;
                dgvDanhsach.Columns[4].ReadOnly = true;
                dgvDanhsach.Columns[5].ReadOnly = true;
                dgvDanhsach.Columns[6].ReadOnly = true;
                //if (rdbNotconfirm.Checked == true)
                //{
                //    dgvDanhsach.Columns[7].ReadOnly = false;
                //    dgvDanhsach.Columns[8].ReadOnly = false;
                //}
                //else if (rdbConfirmed.Checked == true)
                //{
                //    dgvDanhsach.Columns[7].ReadOnly = true;
                //    dgvDanhsach.Columns[8].ReadOnly = true;
                //}
                dgvDanhsach.Columns[7].ReadOnly = true;
                dgvDanhsach.Columns[8].ReadOnly = true;
                dgvDanhsach.Columns[9].ReadOnly = true;
                dgvDanhsach.Columns[10].ReadOnly = true;
                dgvDanhsach.Columns[11].ReadOnly = true;
                dgvDanhsach.Columns[13].ReadOnly = true;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDanhsach.Columns[14].Visible = false;
            }
            else if (optDN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten ";
                strCmd += " from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";

                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";

                if (rdbNotconfirm.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                }
                else if (rdbConfirmed.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
                }
                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Chọn", typeof(bool));
                dskh.Columns.Add(col);
                col = new DataColumn("Tình trạng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("PDTT", typeof(int));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        row[12] = true;

                        string pheduyet = "";
                        if (Boolean.Parse(dt.Rows[i]["pheduyet"].ToString()) == true)
                        {
                            pheduyet = "Đã phê duyệt";
                        }
                        else
                        {
                            pheduyet = "Chưa phê duyệt";
                        }
                        row[13] = pheduyet;
                        int pheduyetTT = 0;
                        if (Boolean.Parse(dt.Rows[i]["PDTT"].ToString()) == true)
                        {
                            pheduyetTT = 1;
                        }
                        else
                        {
                            pheduyetTT = 0;
                        }
                        row[14] = pheduyetTT;

                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.Columns[0].ReadOnly = true;
                dgvDanhsach.Columns[1].ReadOnly = true;
                dgvDanhsach.Columns[2].ReadOnly = true;
                dgvDanhsach.Columns[3].ReadOnly = true;
                dgvDanhsach.Columns[4].ReadOnly = true;
                dgvDanhsach.Columns[5].ReadOnly = true;
                dgvDanhsach.Columns[6].ReadOnly = true;
                //if (rdbNotconfirm.Checked == true)
                //{
                //    dgvDanhsach.Columns[7].ReadOnly = false;
                //    dgvDanhsach.Columns[8].ReadOnly = false;
                //}
                //else if (rdbConfirmed.Checked == true)
                //{
                //    dgvDanhsach.Columns[7].ReadOnly = true;
                //    dgvDanhsach.Columns[8].ReadOnly = true;
                //}
                dgvDanhsach.Columns[7].ReadOnly = true;
                dgvDanhsach.Columns[8].ReadOnly = true;
                dgvDanhsach.Columns[9].ReadOnly = true;
                dgvDanhsach.Columns[10].ReadOnly = true;
                dgvDanhsach.Columns[11].ReadOnly = true;
                dgvDanhsach.Columns[13].ReadOnly = true;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDanhsach.Columns[14].Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            layDS();
            if (dgvDanhsach.RowCount > 0)
            {
                if (rdbNotconfirm.Checked == true)
                {
                    btnConfirm.Enabled = true;
                    btnCancel.Enabled = false;
                    dgvDanhsach.Columns[12].ReadOnly = false;
                    btnSelectall.Enabled = true;
                    btnDeselectall.Enabled = true;
                }
                else if (rdbConfirmed.Checked == true)
                {
                    btnConfirm.Enabled = false;
                    btnCancel.Enabled = true;
                    dgvDanhsach.Columns[12].ReadOnly = true;
                    btnSelectall.Enabled = false;
                    btnDeselectall.Enabled = false;
                }                
            }
            else
            {
                btnConfirm.Enabled = false;
                btnCancel.Enabled = false;
                btnSelectall.Enabled = false;
                btnDeselectall.Enabled = false;
            }
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[12].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[12].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void dgvDanhsach_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Kiem tra du lieu nhap vao phai la so
            DataGridViewCell cuCell = dgvDanhsach.CurrentCell;
            string mainStr = dgvDanhsach.CurrentCell.Value.ToString();
            if (cuCell.ColumnIndex == 7)
            {
                for (int scan = 0; scan < mainStr.Length; scan++)
                {
                    if (Char.IsDigit(mainStr[scan])) { }
                    else
                    {
                        dgvDanhsach.CurrentCell.Value = 0;
                        dgvDanhsach.ClearSelection();
                        dgvDanhsach.CurrentCell = cuCell;
                        dgvDanhsach.CurrentCell.Selected = true;
                        break;
                    }
                }
            }

            // Tinh tong diem tren moi dong du lieu            
            if (Convert.ToInt32(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[7].Value.ToString()) >= 0)
            {
                dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[9].Value = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[6].Value.ToString()) + Convert.ToInt32(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[7].Value.ToString());
                if (Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[9].Value) > 100)
                {
                    MessageBox.Show("Tong diem khong duoc > 100!");
                    dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[7].Value = 0;
                    dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[9].Value = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[6].Value;
                    return;
                }
                decimal tongdiem = 0;
                tongdiem = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[9].Value);
                DataTable dt = new DataTable();
                if (optCN.Checked == true)
                    loaikh = 1;
                else
                    loaikh = 2;
                DataTable dt1 = new DataTable();
                String strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + tongdiem + " and dmdiemxl.loaikh=" + loaikh + " order by diem desc";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt1);
                DataAccess.conn.Close();
                if (dt1.Rows.Count > 0)
                {

                    dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[10].Value = dt1.Rows[0]["tenloai"].ToString();
                    dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[11].Value = dt1.Rows[0]["maloai"].ToString();
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[22] 
                { 
                    new DataColumn("TUTHANG", typeof(string)),
                    new DataColumn("DENTHANG", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("DIEM_SDBQ", typeof(byte)),
                    new DataColumn("DIEM_TGGT", typeof(byte)),
                    new DataColumn("DIEM_SPDV", typeof(byte)),
                    new DataColumn("DIEM_PROFIT", typeof(byte)),
                    new DataColumn("TONGDIEMDL", typeof(byte)),
                    new DataColumn("DINHTINH", typeof(int)),
                    new DataColumn("DIENGIAI", typeof(string)),
                    new DataColumn("TONGDIEM", typeof(byte)),
                    new DataColumn("XEPLOAI", typeof(string)),
                    new DataColumn("LOAIKH", typeof(byte)),
                    new DataColumn("XACNHAN", typeof(bool)),
                    new DataColumn("NGAYXACNHAN", typeof(string)),
                    new DataColumn("NGUOIXACNHAN", typeof(string)),
                    new DataColumn("PHEDUYET", typeof(bool)),
                    new DataColumn("NGAYPHEDUYET", typeof(string)),
                    new DataColumn("NGUOIPHEDUYET", typeof(string)),
                    new DataColumn("PDTT", typeof(bool)),
                    new DataColumn("NGAYPDTT", typeof(string)),
                    new DataColumn("NGUOIPDTT", typeof(string))
                }
            );
            DataRow dr2;

            int dem = 0, demTT = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[14].Value.ToString() == "1")
                {
                    demTT++;
                    break;
                }
            }
            
            if (dem > 0 && demTT == 0)
            {
                dt_temp2.Clear();
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True" && dgvDanhsach.Rows[i].Cells[14].Value.ToString() == "0")
                    {

                        //int iloaiKH = 1;
                        //if (optCN.Checked == true)
                        //{
                        //    iloaiKH = 1;
                        //}
                        //else
                        //{
                        //    iloaiKH = 2;
                        //}

                        //int pheduyet = 0;
                        //if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[12].Value.ToString()) == true)
                        //{
                        //    pheduyet = 1;
                        //}

                        //string ngaypheduyet = "";
                        //string nam, thang, ngay, gio, phut, giay, miligiay;
                        //nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        //thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        //ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        //gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        //phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        //giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        //miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        //ngaypheduyet = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        dr2 = dt_temp2.NewRow();
                        dr2["TUTHANG"] = dtpFrom.Text; ;
                        dr2["DENTHANG"] = dtpTo.Text;
                        dr2["MAKH"] = dgvDanhsach.Rows[i].Cells[1].Value.ToString();
                        dr2["DIEM_SDBQ"] = 0;
                        dr2["DIEM_TGGT"] = 0;
                        dr2["DIEM_SPDV"] = 0;
                        dr2["DIEM_PROFIT"] = 0;
                        dr2["TONGDIEMDL"] = 0;
                        dr2["DINHTINH"] = 0;
                        dr2["DIENGIAI"] = "";
                        dr2["TONGDIEM"] = 0;
                        dr2["XEPLOAI"] = "";
                        dr2["LOAIKH"] = 1;
                        dr2["XACNHAN"] = true;
                        dr2["NGAYXACNHAN"] = "01/01/1900";
                        dr2["NGUOIXACNHAN"] = "";
                        dr2["PHEDUYET"] = true;
                        dr2["NGAYPHEDUYET"] = "01/01/1900";
                        dr2["NGUOIPHEDUYET"] = Thongtindangnhap.user_id;
                        dr2["PDTT"] = false;
                        dr2["NGAYPDTT"] = "01/01/1900";
                        dr2["NGUOIPDTT"] = "";
                        dt_temp2.Rows.Add(dr2);

                        //try
                        //{
                        //    strCmd = "Update Ketquaxeploai set pheduyet='" + pheduyet + "',Ngaypheduyet='" + ngaypheduyet + "',Nguoipheduyet='" + Thongtindangnhap.user_id + "',dinhtinh=" + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[7].Value.ToString()) + ",diengiai=N'" + dgvDanhsach.Rows[i].Cells[8].Value.ToString() + "',tongdiem= " + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[9].Value.ToString()) + ",xeploai='" + dgvDanhsach.Rows[i].Cells[11].Value.ToString() + "' ";
                        //    strCmd += " where makh ='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and loaikh='" + iloaiKH + "' and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and xacnhan='1'";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}
                    }
                }
                if (kqxl_bus.UPDATE_KETQUAXEPLOAI_PHEDUYET(dt_temp2))
                {
                    MessageBox.Show("Phê duyệt xếp loại khách hàng thành công.");
                    layDS();
                    AddKHvaoNhomKH();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình phê duyệt xếp loại khách hàng.");
                }                
                Cursor.Current = Cursors.Default;
                //MessageBox.Show("Đã phê duyệt!");
            }
            else if (demTT > 0)
            {
                MessageBox.Show("Kỳ xếp loại này đã được phê duyệt toàn tỉnh.\nKhông cho phép phê duyệt lại.");
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[22] 
                { 
                    new DataColumn("TUTHANG", typeof(string)),
                    new DataColumn("DENTHANG", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("DIEM_SDBQ", typeof(byte)),
                    new DataColumn("DIEM_TGGT", typeof(byte)),
                    new DataColumn("DIEM_SPDV", typeof(byte)),
                    new DataColumn("DIEM_PROFIT", typeof(byte)),
                    new DataColumn("TONGDIEMDL", typeof(byte)),
                    new DataColumn("DINHTINH", typeof(int)),
                    new DataColumn("DIENGIAI", typeof(string)),
                    new DataColumn("TONGDIEM", typeof(byte)),
                    new DataColumn("XEPLOAI", typeof(string)),
                    new DataColumn("LOAIKH", typeof(byte)),
                    new DataColumn("XACNHAN", typeof(bool)),
                    new DataColumn("NGAYXACNHAN", typeof(string)),
                    new DataColumn("NGUOIXACNHAN", typeof(string)),
                    new DataColumn("PHEDUYET", typeof(bool)),
                    new DataColumn("NGAYPHEDUYET", typeof(string)),
                    new DataColumn("NGUOIPHEDUYET", typeof(string)),
                    new DataColumn("PDTT", typeof(bool)),
                    new DataColumn("NGAYPDTT", typeof(string)),
                    new DataColumn("NGUOIPDTT", typeof(string))
                }
            );
            DataRow dr2;

            int dem = 0, demTT = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[14].Value.ToString() == "1")
                {
                    demTT++;
                    break;
                }
            }

            if (dem > 0 && demTT == 0)
            {
                dt_temp2.Clear();
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    //Nếu cột hủy phê duyệt có tích và chưa phê duyệt toàn tỉnh
                    if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True" && dgvDanhsach.Rows[i].Cells[14].Value.ToString() == "0")
                    {
                        //int iloaiKH = 1;
                        //if (optCN.Checked == true)
                        //{
                        //    iloaiKH = 1;
                        //}
                        //else
                        //{
                        //    iloaiKH = 2;
                        //}

                        //int pheduyet = 1;
                        //if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[12].Value.ToString()) == true)
                        //{
                        //    pheduyet = 0;
                        //}

                        //string ngaypheduyet = "";
                        //string nam, thang, ngay, gio, phut, giay, miligiay;
                        //nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        //thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        //ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        //gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        //phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        //giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        //miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        //ngaypheduyet = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        dr2 = dt_temp2.NewRow();
                        dr2["TUTHANG"] = dtpFrom.Text; ;
                        dr2["DENTHANG"] = dtpTo.Text;
                        dr2["MAKH"] = dgvDanhsach.Rows[i].Cells[1].Value.ToString();
                        dr2["DIEM_SDBQ"] = 0;
                        dr2["DIEM_TGGT"] = 0;
                        dr2["DIEM_SPDV"] = 0;
                        dr2["DIEM_PROFIT"] = 0;
                        dr2["TONGDIEMDL"] = 0;
                        dr2["DINHTINH"] = 0;
                        dr2["DIENGIAI"] = "";
                        dr2["TONGDIEM"] = 0;
                        dr2["XEPLOAI"] = "";
                        dr2["LOAIKH"] = 1;
                        dr2["XACNHAN"] = true;
                        dr2["NGAYXACNHAN"] = "01/01/1900";
                        dr2["NGUOIXACNHAN"] = "";
                        dr2["PHEDUYET"] = false;
                        dr2["NGAYPHEDUYET"] = "01/01/1900";
                        dr2["NGUOIPHEDUYET"] = Thongtindangnhap.user_id;
                        dr2["PDTT"] = false;
                        dr2["NGAYPDTT"] = "01/01/1900";
                        dr2["NGUOIPDTT"] = "";
                        dt_temp2.Rows.Add(dr2);

                        //try
                        //{
                        //    strCmd = "Update Ketquaxeploai set pheduyet='" + pheduyet + "',Ngaypheduyet='" + ngaypheduyet + "',Nguoipheduyet='" + Thongtindangnhap.user_id + "',dinhtinh=" + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[7].Value.ToString()) + ",diengiai=N'" + dgvDanhsach.Rows[i].Cells[8].Value.ToString() + "',tongdiem= " + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[9].Value.ToString()) + ",xeploai='" + dgvDanhsach.Rows[i].Cells[11].Value.ToString() + "' ";
                        //    strCmd += " where makh ='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and loaikh='" + iloaiKH + "' and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}
                    }
                }
                if (kqxl_bus.UPDATE_KETQUAXEPLOAI_PHEDUYET(dt_temp2))
                {
                    MessageBox.Show("Hủy phê duyệt xếp loại khách hàng thành công.");
                    layDS();

                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình hủy phê duyệt xếp loại khách hàng.");
                }
                Cursor.Current = Cursors.Default;
            }
            else if (demTT > 0)
            {
                MessageBox.Show("Kỳ xếp loại này đã được phê duyệt toàn tỉnh.\nKhông cho phép hủy phê duyệt.");
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddKHvaoNhomKH()
        { 
            
            DataTable dt = new DataTable();            
            //Xoa khach hang thuoc cac nhom khach hang dang xep loai da co truoc do
            String strCmd = "delete KH_NHOMKH where MANHOM in (select distinct manhom from KETQUAXEPLOAI,nhom_loaikh where  XEPLOAI is not null and KETQUAXEPLOAI.XEPLOAI=nhom_loaikh.maloai and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "')";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();
            //Dua khach hang da xep loai vao cac nhom hien tai tuong ung
            strCmd = "Insert into  KH_NHOMKH select makh,manhom from KETQUAXEPLOAI,nhom_loaikh where  XEPLOAI is not null and KETQUAXEPLOAI.XEPLOAI=nhom_loaikh.maloai and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and PHEDUYET = 1";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();
        }        
    }
}