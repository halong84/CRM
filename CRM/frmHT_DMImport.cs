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
    public partial class frmHT_DMImport : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrTen, arrLoai;

        public frmHT_DMImport()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;
        }

        private void frmDM_Import_Load(object sender, EventArgs e)
        {
            layDS_MaDM();
            layDanhsach();
        }

        private void layDS_MaDM()
        {
            arrTen = new ArrayList();
            arrLoai = new ArrayList();
            
            cbbMaDM.Items.Clear();
            cbbMaDM.Refresh();
            strCmd = "SELECT * FROM DMIMPORT ORDER BY LoaiDM";

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
                    cbbMaDM.Items.Add(dtResult.Rows[i]["MaDM"].ToString());
                    arrTen.Add(dtResult.Rows[i]["Ten"].ToString());
                    arrLoai.Add(dtResult.Rows[i]["LoaiDM"].ToString());
                }
                catch { }
            }
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã DM", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Danh mục", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại DM", typeof(string));
            dtDanhsach.Columns.Add(col);
            
            strCmd = "SELECT * FROM DMIMPORT ORDER BY LoaiDM";

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
            catch { }

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
                    row[1] = dtResult.Rows[i]["MaDM"].ToString();
                    row[2] = dtResult.Rows[i]["Ten"].ToString();

                    string loaiDM = "";
                    if (dtResult.Rows[i]["LoaiDM"].ToString() == "1")
                    {
                        loaiDM = "Hệ thống";
                    }
                    else if (dtResult.Rows[i]["LoaiDM"].ToString() == "2")
                    {
                        loaiDM = "Dữ liệu";
                    }
                    row[3] = loaiDM;
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 180;
            dgvDanhsach.Columns[2].Width = 350;
            dgvDanhsach.Columns[3].Width = 200;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbMaDM.Text == "")
            {
                MessageBox.Show("Chưa nhập mã danh mục.", "Thông báo");
                cbbMaDM.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMIMPORT WHERE MaDM='" + cbbMaDM.Text.Trim() + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch { }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int loaiDM = 0;
            if (rdbHT.Checked == true)
            {
                loaiDM = 1;
            }
            else if (rdbDL.Checked == true)
            {
                loaiDM = 2;
            }

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into DMIMPORT(MaDM, Ten, LoaiDM) ";
                strCmd += "Values('" + cbbMaDM.Text.Trim() + "',N'" + txtTenDM.Text.Trim() + "','" + loaiDM + "')";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layDanhsach();
                    layDS_MaDM();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch { }           
            }
            else
            {
                MessageBox.Show("Mã danh mục này đã tồn tại.", "Cảnh báo");
                return;
            }
            cbbMaDM.Focus();
            cbbMaDM.Text = "";
            txtTenDM.Text = "";
            rdbHT.Checked = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbMaDM.Text == "")
            {
                MessageBox.Show("Chưa nhập mã danh mục.", "Thông báo");
                cbbMaDM.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMIMPORT WHERE MaDM='" + cbbMaDM.Text.Trim() + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch { }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int loaiDM = 0;
            if (rdbHT.Checked == true)
            {
                loaiDM = 1;
            }
            else if (rdbDL.Checked == true)
            {
                loaiDM = 2;
            }

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update DMIMPORT ";
                strCmd += "SET Ten=N'" + txtTenDM.Text.Trim() + "', LoaiDM='" + loaiDM + "' ";
                strCmd += "WHERE (MaDM='" + cbbMaDM.Text.Trim() + "')";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layDanhsach();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch { }                           
            }
            else
            {
                MessageBox.Show("Mã danh mục này chưa tồn tại.", "Cảnh báo");
                return;
            }
            cbbMaDM.Focus();
            cbbMaDM.Text = "";
            txtTenDM.Text = "";
            rdbHT.Checked = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                try
                {
                    strCmd = "Delete from DMIMPORT Where MaDM='" + dgvDanhsach.CurrentRow.Cells["Mã DM"].Value.ToString() + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch { }
                cbbMaDM.Text = "";
                txtTenDM.Text = "";
                rdbHT.Checked = true;
                layDanhsach();
                layDS_MaDM();
            }            
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMaDM.Text = dgvDanhsach.CurrentRow.Cells["Mã DM"].Value.ToString();
                txtTenDM.Text = dgvDanhsach.CurrentRow.Cells["Danh mục"].Value.ToString();

                if (dgvDanhsach.CurrentRow.Cells["Loại DM"].Value.ToString() == "Hệ thống")
                {
                    rdbHT.Checked = true;
                }
                else if (dgvDanhsach.CurrentRow.Cells["Loại DM"].Value.ToString() == "Dữ liệu")
                {
                    rdbDL.Checked = true;
                }
            }
            catch { }
        }

        private void cbbMaDM_TextChanged(object sender, EventArgs e)
        {
            //txtTenDM.Text = "";
        }

        private void cbbMaDM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbMaDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenDM.Text = arrTen[cbbMaDM.Items.IndexOf(cbbMaDM.Text.Trim())].ToString();
            if (arrLoai[cbbMaDM.Items.IndexOf(cbbMaDM.Text.Trim())].ToString() == "1")
            {
                rdbHT.Checked = true;
            }
            else if (arrLoai[cbbMaDM.Items.IndexOf(cbbMaDM.Text.Trim())].ToString() == "2")
            {
                rdbDL.Checked = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}