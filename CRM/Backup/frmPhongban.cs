using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace CRM
{
    public partial class frmPhongban : Form
    {
        private DataTable dtResult = new DataTable();
        //ArrayList arrTenPB;
        string strCmd = "";

        public frmPhongban()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbMaCN.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;
        }

        private void frmPhongban_Load(object sender, EventArgs e)
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
            layDS_MaPB();
            layDanhsach();
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

        private void layDS_MaPB()
        {
            DataTable dt = new DataTable();
            strCmd = "SELECT * FROM PHONGBAN WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbMaPB.DataSource = dt;
            cbbMaPB.DisplayMember = "mapb";
            cbbMaPB.ValueMember = "mapb";
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
            col = new DataColumn("Mã PB", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên PB", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT * FROM PHONGBAN WHERE MACN='" + cbbMaCN.SelectedValue.ToString() + "' ";

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
                    row[1] = dtResult.Rows[i]["MACN"].ToString();
                    row[2] = dtResult.Rows[i]["MAPB"].ToString();
                    row[3] = dtResult.Rows[i]["TENPB"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 100;
            dgvDanhsach.Columns[2].Width = 100;
            dgvDanhsach.Columns[3].Width = 300;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbMaPB.Text == "")
            {
                MessageBox.Show("Chưa nhập mã phòng.", "Thông báo");
                cbbMaPB.Focus();
                return;
            }
            else if (txtTenPB.Text == "")
            {
                MessageBox.Show("Chưa nhập tên phòng.", "Thông báo");
                txtTenPB.Focus();
                return;
            }

            strCmd = "SELECT * FROM Phongban WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' and MaPB='" + cbbMaPB.SelectedValue.ToString() + "' ";

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
                strCmd = "Insert into Phongban(MaPB, MaCN, TenPB) ";
                strCmd += " Values('" + cbbMaPB.SelectedValue.ToString() + "','" + cbbMaCN.SelectedValue.ToString() + "',N'" + txtTenPB.Text.Trim() + "')";

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
                    layDS_MaPB();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbMaPB.Focus();
                cbbMaPB.Text = "";
                txtTenPB.Text = "";

            }
            else
            {
                MessageBox.Show("Mã phòng này đã tồn tại.", "Cảnh báo");
                cbbMaPB.Focus();
                //txtTennhom.Text = "";
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbMaPB.Text == "")
            {
                MessageBox.Show("Chưa nhập mã phòng.", "Thông báo");
                cbbMaPB.Focus();
                return;
            }
            else if (txtTenPB.Text == "")
            {
                MessageBox.Show("Chưa nhập tên phòng.", "Thông báo");
                txtTenPB.Focus();
                return;
            }

            strCmd = "SELECT * FROM Phongban WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' and MaPB='" + cbbMaPB.SelectedValue.ToString() + "' ";

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
                strCmd = "Update Phongban ";
                strCmd += "SET TenPB=N'" + txtTenPB.Text.Trim() + "' ";
                strCmd += "WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' and MaPB='" + cbbMaPB.SelectedValue.ToString() + "' ";

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
                cbbMaPB.Focus();
                cbbMaPB.Text = "";
                txtTenPB.Text = "";
            }
            else
            {
                MessageBox.Show("Mã phòng này chưa tồn tại.", "Cảnh báo");
                cbbMaPB.Focus();
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
                    strCmd = "Delete from Phongban Where MaCN='" + dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString() + "' and MaPB='" + dgvDanhsach.CurrentRow.Cells["Mã PB"].Value.ToString() + "' ";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    layDS_MaPB();
                    MessageBox.Show("Đã xóa", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbMaPB.Text = "";
                txtTenPB.Text = "";
            }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMaCN.SelectedValue = dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString();
                cbbMaPB.Text = dgvDanhsach.CurrentRow.Cells["Mã PB"].Value.ToString();
                txtTenPB.Text = dgvDanhsach.CurrentRow.Cells["Tên PB"].Value.ToString();
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbMaPB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbMaCN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layDS_MaPB();
            layDanhsach();
        }        
    }
}