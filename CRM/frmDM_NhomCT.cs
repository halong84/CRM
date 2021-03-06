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
    public partial class frmDM_NhomCT : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrListTen, arrListGhichu;
        
        public frmDM_NhomCT()
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

        public void frmDM_NhomCT_Load(object sender, EventArgs e)
        {            
            layDS_ManhomCT();
            layDanhsach();
        }

        private void layDS_ManhomCT()
        {
            arrListTen = new ArrayList();
            arrListGhichu = new ArrayList();

            cbbMaNhomCT.Items.Clear();
            cbbMaNhomCT.Refresh();
            strCmd = "SELECT * FROM DMNHOMCT ";

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
                    cbbMaNhomCT.Items.Add(dtResult.Rows[i]["MaNhom"].ToString());
                    arrListTen.Add(dtResult.Rows[i]["TenNhom"].ToString());
                    arrListGhichu.Add(dtResult.Rows[i]["Ghichu"].ToString());
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
            col = new DataColumn("Mã nhóm CT", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên nhóm CT", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT * FROM DMNHOMCT";

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
                    row[1] = dtResult.Rows[i]["MaNhom"].ToString();
                    row[2] = dtResult.Rows[i]["TenNhom"].ToString();
                    row[3] = dtResult.Rows[i]["GhiChu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 120;
            dgvDanhsach.Columns[2].Width = 300;
            dgvDanhsach.Columns[3].Width = 300;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {   
            if (cbbMaNhomCT.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhóm chỉ tiêu.", "Thông báo");
                cbbMaNhomCT.Focus();
                return;
            }
            else if (txtTenNhomCT.Text == "")
            {
                MessageBox.Show("Chưa nhập tên nhóm chỉ tiêu.", "Thông báo");
                txtTenNhomCT.Focus();
                return;
            }

            strCmd = "SELECT * FROM DMNHOMCT WHERE MaNhom='" + cbbMaNhomCT.Text.Trim() + "'";

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

            if (dtResult.Rows.Count == 0)
            {                
                strCmd = "Insert into DMNHOMCT(MaNhom, TenNhom, GhiChu) ";
                strCmd += "Values('" + cbbMaNhomCT.Text.Trim() + "',N'" + txtTenNhomCT.Text.Trim() + "',N'" + txtGhichu.Text.Trim() + "')";

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
                    layDS_ManhomCT();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbMaNhomCT.Focus();
                cbbMaNhomCT.Text = "";
                txtTenNhomCT.Text = "";
                txtGhichu.Text = "";
            }
            else
            {
                MessageBox.Show("Mã nhóm chỉ tiêu này đã tồn tại.", "Cảnh báo");
                cbbMaNhomCT.Focus();
                cbbMaNhomCT.Text = "";
                return;                
            }            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbMaNhomCT.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhóm chỉ tiêu.", "Thông báo");
                cbbMaNhomCT.Focus();
                return;
            }
            else if (txtTenNhomCT.Text == "")
            {
                MessageBox.Show("Chưa nhập tên nhóm chỉ tiêu.", "Thông báo");
                txtTenNhomCT.Focus();
                return;
            }

            strCmd = "SELECT * FROM DMNHOMCT WHERE MaNhom='" + cbbMaNhomCT.Text.Trim() + "'";

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

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update DMNHOMCT ";
                strCmd += "SET TenNhom=N'" + txtTenNhomCT.Text.Trim() + "', Ghichu=N'" + txtGhichu.Text.Trim() + "' ";
                strCmd += "WHERE (MaNhom='" + cbbMaNhomCT.Text.Trim() + "')";

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
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbMaNhomCT.Focus();
                cbbMaNhomCT.Text = "";
                txtTenNhomCT.Text = "";
                txtGhichu.Text = "";
            }
            else
            {
                MessageBox.Show("Mã nhóm chỉ tiêu này chưa tồn tại.", "Cảnh báo");
                cbbMaNhomCT.Focus();
                cbbMaNhomCT.Text = "";
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT tt.MaNhom ";
            strCmd += "FROM DMNHOMCT nhomct INNER JOIN DMTYTRONG tt ON nhomct.MaNhom = tt.MaNhom ";
            strCmd += "WHERE (tt.MaNhom = '" + dgvDanhsach.CurrentRow.Cells["Mã nhóm CT"].Value.ToString() + "')";

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
            if (dtResult.Rows.Count == 0)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        strCmd = "Delete from DMNHOMCT Where MaNhom='" + dgvDanhsach.CurrentRow.Cells["Mã nhóm CT"].Value.ToString() + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }
                    cbbMaNhomCT.Text = "";
                    txtTenNhomCT.Text = "";
                    txtGhichu.Text = "";
                    layDanhsach();
                    layDS_ManhomCT();
                }
            }
            else
            {
                MessageBox.Show("Nhóm chỉ tiêu này đã cập nhật tỷ trọng điểm. Không cho phép xóa.", "Thông báo");
            }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMaNhomCT.Text = dgvDanhsach.CurrentRow.Cells["Mã nhóm CT"].Value.ToString();
                txtTenNhomCT.Text = dgvDanhsach.CurrentRow.Cells["Tên nhóm CT"].Value.ToString();
                txtGhichu.Text = dgvDanhsach.CurrentRow.Cells["Ghi chú"].Value.ToString();
            }
            catch { }
        }

        private void cbbMaNhomCT_TextChanged(object sender, EventArgs e)
        {
            //txtTenNhomCT.Text = "";
            //txtGhichu.Text = "";
        }

        private void cbbMaNhomCT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbMaNhomCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenNhomCT.Text = arrListTen[cbbMaNhomCT.Items.IndexOf(cbbMaNhomCT.Text.Trim())].ToString();
            txtGhichu.Text = arrListGhichu[cbbMaNhomCT.Items.IndexOf(cbbMaNhomCT.Text.Trim())].ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}