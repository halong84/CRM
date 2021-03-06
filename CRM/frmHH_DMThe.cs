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
    public partial class frmHH_DMThe : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrTen;

        public frmHH_DMThe()
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

        private void frmHH_DMThe_Load(object sender, EventArgs e)
        {
            layDS_TheKH();
            layDanhsach();
        }

        private void layDS_TheKH()
        {
            arrTen = new ArrayList();
            
            cbbMathe.Items.Clear();
            cbbMathe.Refresh();
            strCmd = "SELECT * FROM DMTHE ";

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
                    cbbMathe.Items.Add(dtResult.Rows[i]["MaThe"].ToString());
                    arrTen.Add(dtResult.Rows[i]["TenThe"].ToString());
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
            col = new DataColumn("Mã thẻ", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên thẻ", typeof(string));
            dtDanhsach.Columns.Add(col);
            
            strCmd = "SELECT * FROM DMTHE ";

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
                    row[1] = dtResult.Rows[i]["MaThe"].ToString();
                    row[2] = dtResult.Rows[i]["TenThe"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 120;
            dgvDanhsach.Columns[2].Width = 250;
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbMathe.Text == "")
            {
                MessageBox.Show("Chưa nhập mã thẻ.", "Thông báo");
                cbbMathe.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMTHE WHERE MaThe='" + cbbMathe.Text.Trim() + "'";

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
                strCmd = "Insert into DMTHE(MaThe, TenThe) ";
                strCmd += "Values('" + cbbMathe.Text.Trim() + "',N'" + txtTenthe.Text.Trim() + "')";

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
                    layDS_TheKH();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbMathe.Focus();
                cbbMathe.Text = "";
                txtTenthe.Text = "";
                
            }
            else
            {
                MessageBox.Show("Mã thẻ này đã tồn tại.", "Cảnh báo");
                cbbMathe.Focus();
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbMathe.Text == "")
            {
                MessageBox.Show("Chưa nhập mã thẻ.", "Thông báo");
                cbbMathe.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMTHE WHERE MaThe='" + cbbMathe.Text.Trim() + "'";

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
                strCmd = "Update DMTHE ";
                strCmd += "SET TenThe=N'" + txtTenthe.Text.Trim() + "' ";
                strCmd += "WHERE (MaThe='" + cbbMathe.Text.Trim() + "')";

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
                cbbMathe.Focus();
                cbbMathe.Text = "";
                txtTenthe.Text = "";
                
            }
            else
            {
                MessageBox.Show("Mã thẻ này chưa tồn tại.", "Cảnh báo");
                cbbMathe.Focus();
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT MaThe ";
            strCmd += "FROM DMTHE ";
            strCmd += "WHERE (MaThe = '" + dgvDanhsach.CurrentRow.Cells["Mã thẻ"].Value.ToString() + "')";

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
            
            if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    strCmd = "Delete from DMTHE Where MaThe='" + dgvDanhsach.CurrentRow.Cells["Mã thẻ"].Value.ToString() + "'";
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
                cbbMathe.Text = "";
                txtTenthe.Text = "";
                layDanhsach();
                layDS_TheKH();
            }            
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMathe.Text = dgvDanhsach.CurrentRow.Cells["Mã thẻ"].Value.ToString();
                txtTenthe.Text = dgvDanhsach.CurrentRow.Cells["Tên thẻ"].Value.ToString();
            }
            catch { }
        }

        private void cbbMathe_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbMathe_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenthe.Text = arrTen[cbbMathe.Items.IndexOf(cbbMathe.Text.Trim())].ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}