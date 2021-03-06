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

namespace CRM
{
    public partial class frmDM_LoaiGD : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrTen;

        public frmDM_LoaiGD()
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

        private void frmDM_LoaiGD_Load(object sender, EventArgs e)
        {
            layDS_LoaiGD();
            layDanhsach();
        }

        private void layDS_LoaiGD()
        {
            arrTen = new ArrayList();

            cbbLoai.Items.Clear();
            cbbLoai.Refresh();
            strCmd = "SELECT * FROM LOAIGIAODICH ";

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
                    cbbLoai.Items.Add(dtResult.Rows[i]["MaLoaiGD"].ToString());
                    arrTen.Add(dtResult.Rows[i]["TenLoai"].ToString());
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
            col = new DataColumn("Mã loại GD", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên loại GD", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT * FROM LOAIGIAODICH ";

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
                    row[1] = dtResult.Rows[i]["MaLoaiGD"].ToString();
                    row[2] = dtResult.Rows[i]["TenLoai"].ToString();
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
            if (cbbLoai.Text == "")
            {
                MessageBox.Show("Chưa nhập mã loại GD.", "Thông báo");
                cbbLoai.Focus();
                return;
            }

            strCmd = "SELECT * FROM LOAIGIAODICH WHERE MaLoaiGD='" + cbbLoai.Text.Trim() + "'";

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
                strCmd = "Insert into LOAIGIAODICH(MaLoaiGD, TenLoai) ";
                strCmd += "Values('" + cbbLoai.Text.Trim() + "',N'" + txtTenloai.Text.Trim() + "')";

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
                    layDS_LoaiGD();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbLoai.Focus();
                cbbLoai.Text = "";
                txtTenloai.Text = "";
            }
            else
            {
                MessageBox.Show("Mã loại GD này đã tồn tại.", "Cảnh báo");
                cbbLoai.Focus();
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbLoai.Text == "")
            {
                MessageBox.Show("Chưa nhập mã loại GD.", "Thông báo");
                cbbLoai.Focus();
                return;
            }

            strCmd = "SELECT * FROM LOAIGIAODICH WHERE MaLoaiGD='" + cbbLoai.Text.Trim() + "'";

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
                strCmd = "Update LOAIGIAODICH ";
                strCmd += "SET TenLoai=N'" + txtTenloai.Text.Trim() + "' ";
                strCmd += "WHERE (MaLoaiGD='" + cbbLoai.Text.Trim() + "')";

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
                cbbLoai.Focus();
                cbbLoai.Text = "";
                txtTenloai.Text = "";

            }
            else
            {
                MessageBox.Show("Mã loại GD này chưa tồn tại.", "Cảnh báo");
                cbbLoai.Focus();
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT MaLoaiGD ";
            strCmd += "FROM LOAIGIAODICH ";
            strCmd += "WHERE (MaLoaiGD = '" + dgvDanhsach.CurrentRow.Cells["Mã loại GD"].Value.ToString() + "')";

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

            if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    strCmd = "Delete from LOAIGIAODICH Where MaLoaiGD='" + dgvDanhsach.CurrentRow.Cells["Mã loại GD"].Value.ToString() + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbLoai.Text = "";
                txtTenloai.Text = "";
                layDanhsach();
                layDS_LoaiGD();
            }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbLoai.Text = dgvDanhsach.CurrentRow.Cells["Mã loại GD"].Value.ToString();
                txtTenloai.Text = dgvDanhsach.CurrentRow.Cells["Tên loại GD"].Value.ToString();
            }
            catch
            {
                
            }
        }

        private void cbbMaloai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbMaloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenloai.Text = arrTen[cbbLoai.Items.IndexOf(cbbLoai.Text.Trim())].ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}