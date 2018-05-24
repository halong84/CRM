using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmDMDiemXL_Copy : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";

        public frmDMDiemXL_Copy()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            //dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;

            cbbMaCN.Enabled = false;
        }

        private void frmDMDiemXL_Copy_Load(object sender, EventArgs e)
        {
            layDS_CN();
            layDanhsach();
        }

        private void layDS_CN()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT macn,tencn from Chinhanh Where MaCN='4800'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbMaCN.DataSource = dt;
            cbbMaCN.DisplayMember = "tencn";
            cbbMaCN.ValueMember = "macn";
            cbbMaCN.DataSource = dt;
            //cbbMaCN.SelectedValue = Thongtindangnhap.macn;
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CN", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã tiêu chí", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên tiêu chí", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày hiệu lực", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT diemxl.*, xlkh.TenLoai FROM DMDIEMXL diemxl INNER JOIN DMXEPLOAIKH xlkh ON diemxl.MALOAI = xlkh.MALOAI ";
            strCmd += " Where diemxl.MaCN='" + cbbMaCN.SelectedValue.ToString() + "' ";
            if (rdbCN.Checked == true)
            {
                strCmd += " and diemxl.loaikh='1' ORDER BY diemxl.MaCN, diemxl.LoaiKH, diemxl.MaLoai, diemxl.NgayBD";
            }
            else if (rdbDN.Checked == true)
            {
                strCmd += " and diemxl.loaikh='2' ORDER BY diemxl.MaCN, diemxl.LoaiKH, diemxl.MaLoai, diemxl.NgayBD";
            }

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
                    string loaiKH = "";
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaCN"].ToString();

                    if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    {
                        loaiKH = "Cá nhân";
                    }
                    else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    {
                        loaiKH = "Doanh nghiệp";
                    }

                    row[2] = loaiKH;
                    row[3] = dtResult.Rows[i]["MaLoai"].ToString();
                    row[4] = dtResult.Rows[i]["TenLoai"].ToString();
                    row[5] = dtResult.Rows[i]["Diem"].ToString();

                    string ngayBDHL, ngayHetHL;
                    ngayBDHL = dtResult.Rows[i]["NgayBD"].ToString();
                    ngayHetHL = dtResult.Rows[i]["NgayKT"].ToString();

                    string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
                    ngayBD = ngayBDHL.Substring(0, 2);
                    thangBD = ngayBDHL.Substring(3, 2);
                    namBD = ngayBDHL.Substring(6, 4);
                    ngayKT = ngayHetHL.Substring(0, 2);
                    thangKT = ngayHetHL.Substring(3, 2);
                    namKT = ngayHetHL.Substring(6, 4);

                    row[6] = ngayBD + "/" + thangBD + "/" + namBD;
                    row[7] = ngayKT + "/" + thangKT + "/" + namKT;
                    row[8] = true;
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = true;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.Columns[6].ReadOnly = true;
            dgvDanhsach.Columns[7].ReadOnly = true;
            dgvDanhsach.Columns[8].ReadOnly = false;

            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[8].Width = 60;

            dgvDanhsach.Columns[1].Visible = false;
            dgvDanhsach.Columns[6].Visible = false;
            dgvDanhsach.Columns[7].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            layDanhsach();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[8].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[8].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[8].Value.ToString() == "True")
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
                    if (dgvDanhsach.Rows[i].Cells[8].Value.ToString() == "True")
                    {
                        int loaiKH = 1;
                        if (dgvDanhsach.Rows[i].Cells[2].Value.ToString() == "Cá nhân")
                        {
                            loaiKH = 1;
                        }
                        else
                        {
                            loaiKH = 2;
                        }

                        string ngayBD, ngayKT;
                        ngayBD = "01/01/2012";
                        ngayKT = "12/31/9998";

                        try
                        {
                            strCmd = "Insert into DMDIEMXL(MaCN, LoaiKH, MaLoai, Diem, NgayBD, NgayKT) ";
                            strCmd += "Values('" + frmDM_DiemXL.strMaCN + "','" + loaiKH + "','" + dgvDanhsach.Rows[i].Cells[3].Value.ToString() + "','" + dgvDanhsach.Rows[i].Cells[5].Value.ToString();
                            strCmd += "','" + ngayBD + "','" + ngayKT + "')";
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
                Cursor.Current = Cursors.Default;
                this.Close();
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }
    }
}