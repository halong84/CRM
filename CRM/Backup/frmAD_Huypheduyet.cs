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
    public partial class frmAD_Huypheduyet : Form
    {
        String strCmd = "";
        public static decimal diemtggt = 0, tdiem = 0;
        public static byte loaikh;
        string tuthang = "", denthang = "";

        public frmAD_Huypheduyet()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbMaCN.DropDownStyle = ComboBoxStyle.DropDownList;
            dgvDanhsach.Font = new Font("Arial", 9F);

            DateTime dtCurrent = DateTime.Now;
            dtpFrom.CustomFormat = "MM/yyyy";            
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
            btnCancel.Enabled = false;
        }

        private void frmAD_Huypheduyet_Load(object sender, EventArgs e)
        {
            if (frmMain.cn == "4800")
            {
                cbbMaCN.Enabled = true;
            }
            else
            {
                cbbMaCN.Enabled = false;
            }

            layDS_CN();

            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
        }

        private void layDS_CN()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT macn,tencn from Chinhanh Where MaCN<>'9999' order by macn";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbMaCN.DataSource = dt;
            cbbMaCN.DisplayMember = "tencn";
            cbbMaCN.ValueMember = "macn";

            cbbMaCN.SelectedValue = frmMain.cn;
        }

        private void layDS()
        {
            if (optCN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten ";
                strCmd += " from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + cbbMaCN.SelectedValue.ToString() + "'";

                if (rdbNotconfirm.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                }
                else if (rdbConfirmed.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
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
                        row[12] = false;

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
            }
            else if (optDN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten ";
                strCmd += " from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + cbbMaCN.SelectedValue.ToString() + "'";

                if (rdbNotconfirm.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=0 Order by ketquaxeploai.Tongdiem desc";
                }
                else if (rdbConfirmed.Checked == true)
                {
                    strCmd += " and ketquaxeploai.xacnhan=1 and ketquaxeploai.pheduyet=1 Order by ketquaxeploai.Tongdiem desc";
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
                        row[12] = false;

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
                    btnCancel.Enabled = false;
                    //btnConfirm.Enabled = true;
                    dgvDanhsach.Columns[12].Visible = false;
                    btnSelectall.Enabled = false;
                    btnDeselectall.Enabled = false;
                }
                else if (rdbConfirmed.Checked == true)
                {
                    btnCancel.Enabled = true;
                    //btnConfirm.Enabled = false;
                    dgvDanhsach.Columns[12].Visible = true;
                    dgvDanhsach.Columns[12].ReadOnly = false;
                    btnSelectall.Enabled = true;
                    btnDeselectall.Enabled = true;
                }

            }
            else
            {
                btnConfirm.Enabled = false;
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

                        int pheduyet = 1;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[12].Value.ToString()) == true)
                        {
                            pheduyet = 0;
                        }

                        string ngaypheduyet = "";
                        string nam, thang, ngay, gio, phut, giay, miligiay;
                        nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        ngaypheduyet = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        try
                        {
                            strCmd = "Update Ketquaxeploai set pheduyet='" + pheduyet + "',Ngaypheduyet='01/01/1990',Nguoipheduyet=''";
                            strCmd += " where makh ='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and loaikh='" + iloaiKH + "' and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + cbbMaCN.SelectedValue.ToString() + "'";
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
                AddKHvaoNhomKH();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã hủy phê duyệt!");
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
            String strCmd = "delete KH_NHOMKH where MANHOM in (select distinct manhom from KETQUAXEPLOAI,nhom_loaikh where  XEPLOAI is not null and KETQUAXEPLOAI.XEPLOAI=nhom_loaikh.maloai and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + cbbMaCN.SelectedValue.ToString() + "')";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
            //Dua khach hang da xep loai vao cac nhom hien tai tuong ung
            strCmd = "Insert into  KH_NHOMKH select makh,manhom from KETQUAXEPLOAI,nhom_loaikh where  XEPLOAI is not null and KETQUAXEPLOAI.XEPLOAI=nhom_loaikh.maloai and tuthang='" + tuthang + "' and denthang='" + denthang + "' and left(ketquaxeploai.makh,4)='" + cbbMaCN.SelectedValue.ToString() + "' and PHEDUYET = 1";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
        }        
    }
}