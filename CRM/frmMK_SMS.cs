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
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmMK_SMS : Form
    {
        String sCommand = "";

        public frmMK_SMS()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbSNhomKH.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            //dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;
        }

        private void frmMK_SMS_Load(object sender, EventArgs e)
        {
            //layDSKH();
            layNhomKH();
        }

        private void layNhomKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT manhom,Diengiai from nhomkhachhang Where MaCN='" + Thongtindangnhap.macn + "' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            cbbSNhomKH.DataSource = dt;
            cbbSNhomKH.DisplayMember = "Diengiai";
            cbbSNhomKH.ValueMember = "Manhom";
        }
        
        private void layDSKH()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dskh;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "select * from khachhang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>''";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>''";
            }

            DataTable dt = new DataTable();
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;        
                    row[1] = true;
                    row[2] = dt.Rows[i]["makh"].ToString();
                    row[3] = dt.Rows[i]["cmnd"].ToString();
                    row[4] = dt.Rows[i]["hoten"].ToString();
                    row[5] = dt.Rows[i]["dienthoai1"].ToString();
                    row[6] = dt.Rows[i]["diachi1"].ToString();
                    row[7] = dt.Rows[i]["ngaysinh"].ToString().Substring(0,10);                    
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = false;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 60;
            dgvDanhsach.Columns[2].Width = 130;
            dgvDanhsach.Columns[3].Width = 105;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].Width = 110;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dskh;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "select * from khachhang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and makh like '%" + txtSMaKH.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and makh like '%" + txtSMaKH.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = true;
                    row[2] = dt.Rows[i]["makh"].ToString();
                    row[3] = dt.Rows[i]["cmnd"].ToString();
                    row[4] = dt.Rows[i]["hoten"].ToString();
                    row[5] = dt.Rows[i]["dienthoai1"].ToString();
                    row[6] = dt.Rows[i]["diachi1"].ToString();
                    row[7] = dt.Rows[i]["ngaysinh"].ToString().Substring(0, 10);
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = false;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 60;
            dgvDanhsach.Columns[2].Width = 130;
            dgvDanhsach.Columns[3].Width = 105;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].Width = 110;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void btnSTenKH_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dskh;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "select * from khachhang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and hoten like N'%" + txtSTenKH.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and hoten like N'%" + txtSTenKH.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = true;
                    row[2] = dt.Rows[i]["makh"].ToString();
                    row[3] = dt.Rows[i]["cmnd"].ToString();
                    row[4] = dt.Rows[i]["hoten"].ToString();
                    row[5] = dt.Rows[i]["dienthoai1"].ToString();
                    row[6] = dt.Rows[i]["diachi1"].ToString();
                    row[7] = dt.Rows[i]["ngaysinh"].ToString().Substring(0, 10);
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = false;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 60;
            dgvDanhsach.Columns[2].Width = 130;
            dgvDanhsach.Columns[3].Width = 105;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].Width = 110;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dskh;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "select * from khachhang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and cmnd like '%" + txtSCMND.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and cmnd like '%" + txtSCMND.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = true;
                    row[2] = dt.Rows[i]["makh"].ToString();
                    row[3] = dt.Rows[i]["cmnd"].ToString();
                    row[4] = dt.Rows[i]["hoten"].ToString();
                    row[5] = dt.Rows[i]["dienthoai1"].ToString();
                    row[6] = dt.Rows[i]["diachi1"].ToString();
                    row[7] = dt.Rows[i]["ngaysinh"].ToString().Substring(0, 10);
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = false;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 60;
            dgvDanhsach.Columns[2].Width = 130;
            dgvDanhsach.Columns[3].Width = 105;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].Width = 110;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void btnSDienthoai_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dskh;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "select * from khachhang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and dienthoai1 like '%" + txtSDienthoai.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + Thongtindangnhap.macn + "' and DIENTHOAI1<>'' and dienthoai1 like '%" + txtSDienthoai.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = true;
                    row[2] = dt.Rows[i]["makh"].ToString();
                    row[3] = dt.Rows[i]["cmnd"].ToString();
                    row[4] = dt.Rows[i]["hoten"].ToString();
                    row[5] = dt.Rows[i]["dienthoai1"].ToString();
                    row[6] = dt.Rows[i]["diachi1"].ToString();
                    row[7] = dt.Rows[i]["ngaysinh"].ToString().Substring(0, 10);
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = false;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 60;
            dgvDanhsach.Columns[2].Width = 130;
            dgvDanhsach.Columns[3].Width = 105;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].Width = 110;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void btnSNhomKH_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dskh;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "select khachhang.* from khachhang,nhomkhachhang,kh_nhomkh ";
                sCommand += " where kh_nhomkh.manhom=nhomkhachhang.manhom and kh_nhomkh.makh=khachhang.makh and khachhang.macn='" + Thongtindangnhap.macn + "' and khachhang.DIENTHOAI1<>'' and nhomkhachhang.manhom = '" + cbbSNhomKH.SelectedValue.ToString() + "'";
            }
            else
            {
                sCommand = "select khachhangtiemnang.* from khachhangtiemnang,nhomkhachhang,kh_nhomkh ";
                sCommand += " where kh_nhomkh.manhom=nhomkhachhang.manhom and kh_nhomkh.makh=khachhangtiemnang.makh and khachhangtiemnang.macn='" + Thongtindangnhap.macn + "' and khachhangtiemnang.DIENTHOAI1<>'' and nhomkhachhang.manhom = '" + cbbSNhomKH.SelectedValue.ToString() + "'";
            }

            DataTable dt = new DataTable();
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = true;
                    row[2] = dt.Rows[i]["makh"].ToString();
                    row[3] = dt.Rows[i]["cmnd"].ToString();
                    row[4] = dt.Rows[i]["hoten"].ToString();
                    row[5] = dt.Rows[i]["dienthoai1"].ToString();
                    row[6] = dt.Rows[i]["diachi1"].ToString();
                    row[7] = dt.Rows[i]["ngaysinh"].ToString().Substring(0, 10);
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = false;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 60;
            dgvDanhsach.Columns[2].Width = 130;
            dgvDanhsach.Columns[3].Width = 105;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].Width = 110;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void rdbHienhuu_CheckedChanged(object sender, EventArgs e)
        {
            //layDSKH();
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[1].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[1].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[1].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }

            if (dem > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    sCommand = "Delete from TINNHAN where LEFT(makh,4) = '" + Thongtindangnhap.macn + "' ";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
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

                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[1].Value.ToString() == "True")
                    {
                        try
                        {
                            //insert vao table TinNhan                    
                            sCommand = "insert into TinNhan(makh,tenkh,sdt,diachi,noidung) values('" + dgvDanhsach.Rows[i].Cells[2].Value.ToString() + "',N'" + dgvDanhsach.Rows[i].Cells[4].Value.ToString() + "','" + dgvDanhsach.Rows[i].Cells[5].Value.ToString() + "',N'" + dgvDanhsach.Rows[i].Cells[6].Value.ToString() + "',N'" + txtNoidung.Text + "')";

                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            } 
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
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
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã lưu.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Chưa chọn khách hàng nào.", "Thông báo");
            }
        }
    }
}