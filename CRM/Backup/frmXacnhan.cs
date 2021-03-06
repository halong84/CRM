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

namespace CRM
{
    public partial class frmXacnhan : Form
    {
        String strCmd = "";
        public static decimal diemtggt = 0, tdiem = 0;
        public static byte loaikh;
        string tuthang = "", denthang = "";
        string ngaytinhdiem = "";

        public frmXacnhan()
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

        private void frmXacnhan_Load(object sender, EventArgs e)
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
                strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + frmMain.cn + "'";

                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + frmMain.cn + "' ";

                if (rdbNotconfirm.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=0";
                    if (rdbChuaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                    }
                    if (rdbDaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
                    } 
                }
                else if (rdbConfirmed.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1";
                    if (rdbChuaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                    }
                    if (rdbDaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
                    } 
                }
                DataTable dt = new DataTable();
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
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
                col = new DataColumn("Phê duyệt", typeof(string));
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

                        string xacnhan = "";
                        if (Boolean.Parse(dt.Rows[i]["xacnhan"].ToString()) == true)
                        {
                            xacnhan = "Đã xác nhận";
                        }
                        else
                        {
                            xacnhan = "Chưa xác nhận";
                        }
                        row[13] = xacnhan;

                        string pheduyet = "";
                        if (Boolean.Parse(dt.Rows[i]["Pheduyet"].ToString()) == true)
                        {
                            pheduyet = "Đã phê duyệt";
                        }
                        else
                        {
                            pheduyet = "Chưa phê duyệt";
                        }
                        row[14] = pheduyet;

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
                if (rdbNotconfirm.Checked == true)
                {
                    dgvDanhsach.Columns[7].ReadOnly = false;
                    dgvDanhsach.Columns[8].ReadOnly = false;
                }
                else if (rdbConfirmed.Checked == true)
                {
                    dgvDanhsach.Columns[7].ReadOnly = true;
                    dgvDanhsach.Columns[8].ReadOnly = true;
                }
                //dgvDanhsach.Columns[7].ReadOnly = false;
                //dgvDanhsach.Columns[8].ReadOnly = false;
                dgvDanhsach.Columns[9].ReadOnly = true;
                dgvDanhsach.Columns[10].ReadOnly = true;
                dgvDanhsach.Columns[11].ReadOnly = true;
                dgvDanhsach.Columns[13].ReadOnly = true;
                dgvDanhsach.Columns[14].ReadOnly = true;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                
            }
            else if (optDN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten ";
                strCmd += " from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + frmMain.cn + "'";

                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + frmMain.cn + "'";

                if (rdbNotconfirm.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=0";
                    if (rdbChuaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                    }
                    if (rdbDaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
                    }
                }
                else if (rdbConfirmed.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1";
                    if (rdbChuaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                    }
                    if (rdbDaPD.Checked == true)
                    {
                        strCmd += " and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
                    }
                }
                DataTable dt = new DataTable();
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
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
                col = new DataColumn("Phê duyệt", typeof(string));
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

                        string xacnhan = "";
                        if (Boolean.Parse(dt.Rows[i]["xacnhan"].ToString()) == true)
                        {
                            xacnhan = "Đã xác nhận";
                        }
                        else
                        {
                            xacnhan = "Chưa xác nhận";
                        }
                        row[13] = xacnhan;

                        string pheduyet = "";
                        if (Boolean.Parse(dt.Rows[i]["Pheduyet"].ToString()) == true)
                        {
                            pheduyet = "Đã phê duyệt";
                        }
                        else
                        {
                            pheduyet = "Chưa phê duyệt";
                        }
                        row[14] = pheduyet;

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
                if (rdbNotconfirm.Checked == true)
                {
                    dgvDanhsach.Columns[7].ReadOnly = false;
                    dgvDanhsach.Columns[8].ReadOnly = false;
                }
                else if (rdbConfirmed.Checked == true)
                {
                    dgvDanhsach.Columns[7].ReadOnly = true;
                    dgvDanhsach.Columns[8].ReadOnly = true;
                }
                //dgvDanhsach.Columns[7].ReadOnly = false;
                //dgvDanhsach.Columns[8].ReadOnly = false;
                dgvDanhsach.Columns[9].ReadOnly = true;
                dgvDanhsach.Columns[10].ReadOnly = true;
                dgvDanhsach.Columns[11].ReadOnly = true;
                dgvDanhsach.Columns[13].ReadOnly = true;
                dgvDanhsach.Columns[14].ReadOnly = true;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            layDS();
            if (dgvDanhsach.RowCount > 0)
            {
                if (rdbChuaPD.Checked == true)
                {
                    if (rdbNotconfirm.Checked == true)
                    {
                        btnConfirm.Enabled = true;
                        btnCancel.Enabled = false;
                    }
                    else if (rdbConfirmed.Checked == true)
                    {
                        btnConfirm.Enabled = false;
                        btnCancel.Enabled = true;
                    }
                    dgvDanhsach.Columns[12].ReadOnly = false;
                    btnSelectall.Enabled = true;
                    btnDeselectall.Enabled = true;
                }
                else if (rdbDaPD.Checked == true)
                {
                    btnConfirm.Enabled = false;
                    btnCancel.Enabled = false;
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
            ngaytinhdiem = dtpFrom.Text.Substring(0, 2) + "/01/" + dtpFrom.Text.Substring(3, 4);
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

                String strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + tongdiem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt1);
                frmMain.conn.Close();
                if (dt1.Rows.Count > 0)
                {

                    dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[10].Value = dt1.Rows[0]["tenloai"].ToString();
                    dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[11].Value = dt1.Rows[0]["maloai"].ToString();
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }

            if (dem > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                    {
                        int iloaiKH = 1;
                        if (optCN.Checked == true)
                        {
                            iloaiKH = 1;
                        }
                        else
                        {
                            iloaiKH = 2;
                        }

                        int xacnhan = 0;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[12].Value.ToString()) == true)
                        {
                            xacnhan = 1;
                        }

                        string ngayxacnhan = "";
                        string nam, thang, ngay, gio, phut, giay, miligiay;
                        nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        ngayxacnhan = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        try
                        {
                            strCmd = "Update Ketquaxeploai set xacnhan='" + xacnhan + "',Ngayxacnhan='" + ngayxacnhan + "',Nguoixacnhan='" + frmDangnhap.UserID + "',dinhtinh=" + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[7].Value.ToString()) + ",diengiai=N'" + dgvDanhsach.Rows[i].Cells[8].Value.ToString() + "',tongdiem= " + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[9].Value.ToString()) + ",xeploai='" + dgvDanhsach.Rows[i].Cells[11].Value.ToString() + "' ";
                            strCmd += " where makh ='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and loaikh='" + iloaiKH + "' and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + frmMain.cn + "' and pheduyet='0'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        catch
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                        }
                    }
                }
                layDS();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã xác nhận!");
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }

            if (dem > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                    {
                        int iloaiKH = 1;
                        if (optCN.Checked == true)
                        {
                            iloaiKH = 1;
                        }
                        else
                        {
                            iloaiKH = 2;
                        }

                        int xacnhan = 1;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[12].Value.ToString()) == true)
                        {
                            xacnhan = 0;
                        }

                        string ngayxacnhan = "";
                        string nam, thang, ngay, gio, phut, giay, miligiay;
                        nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        ngayxacnhan = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        try
                        {
                            strCmd = "Update Ketquaxeploai set xacnhan='" + xacnhan + "',Ngayxacnhan='" + ngayxacnhan + "',Nguoixacnhan='" + frmDangnhap.UserID + "',dinhtinh=" + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[7].Value.ToString()) + ",diengiai=N'" + dgvDanhsach.Rows[i].Cells[8].Value.ToString() + "',tongdiem= " + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[9].Value.ToString()) + ",xeploai='" + dgvDanhsach.Rows[i].Cells[11].Value.ToString() + "' ";
                            strCmd += " where makh ='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and loaikh='" + iloaiKH + "' and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + frmMain.cn + "' and pheduyet='0'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        catch
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                        }
                    }
                }
                layDS();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã hủy xác nhận!");
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

        private void rdbConfirmed_Click(object sender, EventArgs e)
        {

        }

        private void rdbNotconfirm_Click(object sender, EventArgs e)
        {

        }        
    }
}