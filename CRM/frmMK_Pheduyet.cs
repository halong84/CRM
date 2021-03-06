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
    public partial class frmMK_Pheduyet : Form
    {
        String strCmd = "";
        private DataTable dtResult = new DataTable();

        public frmMK_Pheduyet()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

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

            btnConfirm.Enabled = true;
            btnCancel.Enabled = false;

            cbbTieuchi.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            //dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;
        }

        private void frmMK_Pheduyet_Load(object sender, EventArgs e)
        {
            layDS_Tieuchi();
        }

        private void layDS_Tieuchi()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * FROM LICHCHAMSOC ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbTieuchi.DataSource = dt;
            cbbTieuchi.DisplayMember = "TIEUCHI";
            cbbTieuchi.ValueMember = "MATC";
            cbbTieuchi.DataSource = dt;
            cbbTieuchi.SelectedIndex = 0;
        }

        private void layDanhsach()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tiêu chí", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CB", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên CB", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi Phí", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xếp loại", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Từ tháng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đến tháng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("TC", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Kỳ xếp loại", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string));
            dtDanhsach.Columns.Add(col);            

            int loaiKH = 1;
            if (rdbCN.Checked == true)
            {
                loaiKH = 1;
            }
            else if (rdbDN.Checked == true)
            {
                loaiKH = 2;
            }

            int confirm = 0;
            if (rdbNotconfirm.Checked == true)
            {
                confirm = 0;
            }
            else if (rdbConfirmed.Checked == true)
            {
                confirm = 1;
            }

            strCmd = "Select cs.*, kh.HOTEN, nv.TENNV, xl.TENLOAI, li.TIEUCHI as NgayCS from KEHOACHCHAMSOC cs, KHACHHANG kh, _USER nv, KETQUAXEPLOAI kq, DMXEPLOAIKH xl, LICHCHAMSOC li ";
            strCmd += " Where cs.MAKH=kh.MAKH and cs.MAKH=kq.MAKH and kq.PHEDUYET='1' and cs.MANV=nv.USER_ID and kq.XEPLOAI=xl.MALOAI and cs.TIEUCHI=li.MATC and cs.MACN='" + Thongtindangnhap.macn + "' ";
            strCmd += " and cs.Tieuchi='" + cbbTieuchi.SelectedValue.ToString() + "' and cs.TUTHANG='" + dtpFrom.Text + "' and cs.DENTHANG='" + dtpTo.Text + "' and kq.LOAIKH='" + loaiKH + "' and cs.PHEDUYET='" + confirm + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["NgayCS"].ToString();
                    row[4] = dtResult.Rows[i]["MANV"].ToString();
                    row[5] = dtResult.Rows[i]["TenNV"].ToString();

                    if (dtResult.Rows[i]["Chiphi"].ToString() == "")
                    {
                        row[6] = 0;
                    }
                    else
                    {
                        row[6] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Chiphi"].ToString()));
                    }
                    //row[6] = dtResult.Rows[i]["Chiphi"].ToString();

                    string ngaybatdau, ngayketthuc;
                    ngaybatdau = dtResult.Rows[i]["Ngaybatdau"].ToString();
                    ngayketthuc = dtResult.Rows[i]["Ngayketthuc"].ToString();

                    string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
                    ngayBD = ngaybatdau.Substring(0, 2);
                    thangBD = ngaybatdau.Substring(3, 2);
                    namBD = ngaybatdau.Substring(6, 4);
                    ngayKT = ngayketthuc.Substring(0, 2);
                    thangKT = ngayketthuc.Substring(3, 2);
                    namKT = ngayketthuc.Substring(6, 4);

                    row[7] = ngayBD + "/" + thangBD + "/" + namBD;
                    row[8] = ngayKT + "/" + thangKT + "/" + namKT;
                    row[9] = dtResult.Rows[i]["Tenloai"].ToString();
                    row[10] = dtResult.Rows[i]["Tuthang"].ToString();
                    row[11] = dtResult.Rows[i]["Denthang"].ToString();
                    row[12] = dtResult.Rows[i]["Ghichu"].ToString();
                    row[13] = dtResult.Rows[i]["Tieuchi"].ToString();
                    row[14] = dtResult.Rows[i]["Tuthang"].ToString() + " -> " + dtResult.Rows[i]["Denthang"].ToString();
                    row[15] = true;

                    string pheduyet = "";
                    if (Boolean.Parse(dtResult.Rows[i]["pheduyet"].ToString()) == true)
                    {
                        pheduyet = "Đã phê duyệt";
                    }
                    else
                    {
                        pheduyet = "Chưa phê duyệt";
                    }
                    row[16] = pheduyet;

                    dtDanhsach.Rows.Add(row);
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = true;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.Columns[6].ReadOnly = true;
            dgvDanhsach.Columns[6].DefaultCellStyle.Format = "N0";
            dgvDanhsach.Columns[7].ReadOnly = true;
            dgvDanhsach.Columns[8].ReadOnly = true;
            dgvDanhsach.Columns[9].ReadOnly = true;
            dgvDanhsach.Columns[10].ReadOnly = true;
            dgvDanhsach.Columns[11].ReadOnly = true;
            dgvDanhsach.Columns[12].ReadOnly = true;
            dgvDanhsach.Columns[13].ReadOnly = true;
            dgvDanhsach.Columns[14].ReadOnly = true;
            dgvDanhsach.Columns[15].ReadOnly = false;
            dgvDanhsach.Columns[16].ReadOnly = true;            
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].FillWeight = 50;
            dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[14].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;            
            dgvDanhsach.Columns[15].FillWeight = 60;
            dgvDanhsach.Columns[16].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDanhsach.Columns[4].Visible = false;
            dgvDanhsach.Columns[10].Visible = false;
            dgvDanhsach.Columns[11].Visible = false;
            dgvDanhsach.Columns[13].Visible = false;
            dgvDanhsach.Columns[14].Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            layDanhsach();
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[15].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[15].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }
        
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[15].Value.ToString() == "True")
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
                    if (dgvDanhsach.Rows[i].Cells[15].Value.ToString() == "True")
                    {
                        int pheduyet = 0;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[15].Value.ToString()) == true)
                        {
                            pheduyet = 1;
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
                            strCmd = "Update KEHOACHCHAMSOC set pheduyet='" + pheduyet + "',Ngaypheduyet='" + ngaypheduyet + "',Nguoipheduyet='" + Thongtindangnhap.user_id + "' ";
                            strCmd += " Where MAKH='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and Tieuchi='" + dgvDanhsach.Rows[i].Cells[13].Value.ToString() + "' ";
                            strCmd += " and TUTHANG='" + dgvDanhsach.Rows[i].Cells[10].Value.ToString() + "' and DENTHANG='" + dgvDanhsach.Rows[i].Cells[11].Value.ToString() + "' and MaCN='" + Thongtindangnhap.macn + "' ";
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                        catch
                        {
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                        }
                    }
                }
                layDanhsach();
                //AddKHvaoNhomKH();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã phê duyệt!");
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
                if (dgvDanhsach.Rows[i].Cells[15].Value.ToString() == "True")
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
                    if (dgvDanhsach.Rows[i].Cells[15].Value.ToString() == "True")
                    {
                        int pheduyet = 1;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[15].Value.ToString()) == true)
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
                            strCmd = "Update Ketquaxeploai set pheduyet='" + pheduyet + "',Ngaypheduyet='" + ngaypheduyet + "',Nguoipheduyet='" + Thongtindangnhap.user_id + "',dinhtinh=" + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[7].Value.ToString()) + ",diengiai=N'" + dgvDanhsach.Rows[i].Cells[8].Value.ToString() + "',tongdiem= " + Convert.ToDecimal(dgvDanhsach.Rows[i].Cells[9].Value.ToString()) + ",xeploai='" + dgvDanhsach.Rows[i].Cells[11].Value.ToString() + "' where makh ='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "'";
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                        catch
                        {
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                        }
                    }
                }
                layDanhsach();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã hủy phê duyệt!");
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }

        private void rdbNotconfirm_Click(object sender, EventArgs e)
        {
            btnConfirm.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void rdbConfirmed_Click(object sender, EventArgs e)
        {
            btnConfirm.Enabled = false;
            btnCancel.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}