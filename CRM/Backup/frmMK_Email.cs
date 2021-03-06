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

namespace CRM
{
    public partial class frmMK_Email : Form
    {
        String sCommand = "";

        public frmMK_Email()
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

        private void frmMK_Email_Load(object sender, EventArgs e)
        {
            //layDSKH();
            layNhomKH();
        }

        private void layNhomKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT manhom,Diengiai from nhomkhachhang Where MaCN='" + frmMain.cn + "' ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
            col = new DataColumn("Email", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "Select * from KhachHang Where MACN='" + frmMain.cn + "' and EMAIL<>''";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + frmMain.cn + "' and EMAIL<>''";
            }

            DataTable dt = new DataTable();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
                    row[5] = dt.Rows[i]["Email"].ToString();
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
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_MaKH()
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
            col = new DataColumn("Email", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "Select * from KhachHang Where MACN='" + frmMain.cn + "' and EMAIL<>'' and MaKH like '%" + txtSMaKH.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + frmMain.cn + "' and EMAIL<>'' and MaKH like '%" + txtSMaKH.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
                    row[5] = dt.Rows[i]["Email"].ToString();
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
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_TenKH()
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
            col = new DataColumn("Email", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "Select * from KhachHang Where MACN='" + frmMain.cn + "' and EMAIL<>'' and Hoten like N'%" + txtSTenKH.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + frmMain.cn + "' and EMAIL<>'' and Hoten like N'%" + txtSTenKH.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
                    row[5] = dt.Rows[i]["Email"].ToString();
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
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_CMND()
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
            col = new DataColumn("Email", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "Select * from KhachHang Where MACN='" + frmMain.cn + "' and EMAIL<>'' and CMND like '%" + txtSCMND.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + frmMain.cn + "' and EMAIL<>'' and CMND like '%" + txtSCMND.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
                    row[5] = dt.Rows[i]["Email"].ToString();
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
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_Email()
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
            col = new DataColumn("Email", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "Select * from KhachHang Where MACN='" + frmMain.cn + "' and EMAIL<>'' and Email like '%" + txtSEmail.Text + "%'";
            }
            else
            {
                sCommand = "select * from khachhangtiemnang where macn='" + frmMain.cn + "' and EMAIL<>'' and Email like '%" + txtSEmail.Text + "%'";
            }

            DataTable dt = new DataTable();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
                    row[5] = dt.Rows[i]["Email"].ToString();
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
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_NhomKH()
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
            col = new DataColumn("Email", typeof(string));
            dskh.Columns.Add(col);

            if (rdbHienhuu.Checked == true)
            {
                sCommand = "select khachhang.* from khachhang,nhomkhachhang,kh_nhomkh ";
                sCommand += " where kh_nhomkh.manhom=nhomkhachhang.manhom and kh_nhomkh.makh=khachhang.makh and khachhang.macn='" + CRM.frmMain.cn + "' and Khachhang.Email<>'' and nhomkhachhang.manhom = '" + cbbSNhomKH.SelectedValue.ToString() + "'";
            }
            else
            {
                sCommand = "select khachhangtiemnang.* from khachhangtiemnang,nhomkhachhang,kh_nhomkh ";
                sCommand += " where kh_nhomkh.manhom=nhomkhachhang.manhom and kh_nhomkh.makh=khachhangtiemnang.makh and khachhangtiemnang.macn='" + CRM.frmMain.cn + "' and Khachhang.Email<>'' and nhomkhachhang.manhom = '" + cbbSNhomKH.SelectedValue.ToString() + "'";
            }

            DataTable dt = new DataTable();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
                    row[5] = dt.Rows[i]["Email"].ToString();
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
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void rdbHienhuu_CheckedChanged(object sender, EventArgs e)
        {
            //layDSKH();
        }

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            layDS_MaKH();
        }

        private void btnSTenKH_Click(object sender, EventArgs e)
        {
            layDS_TenKH();
        }

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            layDS_CMND();
        }

        private void btnSEmail_Click(object sender, EventArgs e)
        {
            layDS_Email();
        }

        private void btnSNhomKH_Click(object sender, EventArgs e)
        {
            layDS_NhomKH();
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
                    sCommand = "Delete from EMAIL where LEFT(makh,4) = '" + frmMain.cn + "' ";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
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

                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[1].Value.ToString() == "True")
                    {
                        try
                        {
                            //insert vao table Email                    
                            sCommand = "insert into Email(makh,tenkh,Email,Chude,noidung) ";
                            sCommand += " values('" + dgvDanhsach.Rows[i].Cells[2].Value.ToString() + "',N'" + dgvDanhsach.Rows[i].Cells[4].Value.ToString() + "','" + dgvDanhsach.Rows[i].Cells[5].Value.ToString() + "',N'" + txtChude.Text.Trim() + "',N'" + txtNoidung.Text.Trim() + "')";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
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