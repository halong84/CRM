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
using System.Windows;

namespace CRM
{
    public partial class frmGroup : Form
    {
        private DataTable dtResult = new DataTable();
        ArrayList arrListTen;
        string strCmd = "";

        public frmGroup()
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

        public void frmGroup_Load(object sender, EventArgs e)
        {            
            layDS_Manhom();
            layDanhsach();
        }

        private void layDS_Manhom()
        {
            arrListTen = new ArrayList();
            
            cbbManhom.Items.Clear();
            cbbManhom.Refresh();
            strCmd = "SELECT * FROM _Group ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    cbbManhom.Items.Add(dtResult.Rows[i]["Group_ID"].ToString());
                    arrListTen.Add(dtResult.Rows[i]["Group_Name"].ToString());

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
            col = new DataColumn("Mã nhóm", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên nhóm", typeof(string));
            dtDanhsach.Columns.Add(col);
            
            strCmd = "SELECT * FROM _GROUP";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    row[1] = dtResult.Rows[i]["Group_ID"].ToString();
                    row[2] = dtResult.Rows[i]["Group_name"].ToString();
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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbManhom.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhóm.", "Thông báo");
                cbbManhom.Focus();
                return;
            }
            else if (txtTennhom.Text == "")
            {
                MessageBox.Show("Chưa nhập tên nhóm.", "Thông báo");
                txtTennhom.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM _Group WHERE Group_ID='" + cbbManhom.Text.Trim() + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into _Group(Group_ID, Group_Name) ";
                strCmd += " Values('" + cbbManhom.Text.Trim() + "',N'" + txtTennhom.Text.Trim() + "')";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    layDS_Manhom();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbManhom.Focus();
                cbbManhom.Text = "";
                txtTennhom.Text = "";
                
            }
            else
            {
                MessageBox.Show("Mã nhóm này đã tồn tại.", "Cảnh báo");
                cbbManhom.Focus();
                //txtTennhom.Text = "";
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbManhom.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhóm.", "Thông báo");
                cbbManhom.Focus();
                return;
            }
            else if (txtTennhom.Text == "")
            {
                MessageBox.Show("Chưa nhập tên nhóm.", "Thông báo");
                txtTennhom.Focus();
                return;
            }

            strCmd = "SELECT * FROM _Group WHERE Group_ID='" + cbbManhom.Text.Trim() + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update _Group ";
                strCmd += "SET Group_Name=N'" + txtTennhom.Text.Trim() + "' ";
                strCmd += "WHERE (Group_ID='" + cbbManhom.Text.Trim() + "')";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbManhom.Focus();
                cbbManhom.Text = "";
                txtTennhom.Text = "";
            }
            else
            {
                MessageBox.Show("Mã nhóm này chưa tồn tại.", "Cảnh báo");
                cbbManhom.Focus();
                //txtTennhom.Text = "";
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {            
            if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                try
                {
                    strCmd = "Delete from _Group Where Group_ID='" + dgvDanhsach.CurrentRow.Cells["Mã nhóm"].Value.ToString() + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    layDS_Manhom();
                    MessageBox.Show("Đã xóa", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbManhom.Text = "";
                txtTennhom.Text = "";                
            }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbManhom.Text = dgvDanhsach.CurrentRow.Cells["Mã nhóm"].Value.ToString();
                txtTennhom.Text = dgvDanhsach.CurrentRow.Cells["Tên nhóm"].Value.ToString();
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbManhom_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }        
    }
}