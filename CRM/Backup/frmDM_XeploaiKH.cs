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
    public partial class frmDM_XeploaiKH : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrTen, arrThe, arrLoaiKH, arrTenthe;

        public frmDM_XeploaiKH()
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

            cbbTheKH.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmDM_XeploaiKH_Load(object sender, EventArgs e)
        {            
            layDS_Maloai();
            layDS_TheKH();
            layDanhsach();
        }

        private void layDS_Maloai()
        {
            arrTen = new ArrayList();            
            arrLoaiKH = new ArrayList();
            arrTenthe = new ArrayList();
            
            cbbMaloai.Items.Clear();
            cbbMaloai.Refresh();

            int loaiKH = 1;

            if (rdbCN.Checked == true)
            {
                loaiKH = 1;
            }
            else if (rdbDN.Checked == true)
            {
                loaiKH = 2;
            }
            strCmd = "Select xl.*, the.TenThe from DMXEPLOAIKH xl inner join DMTHE the on xl.MaThe=the.MaThe Where xl.LOAIKH='" + loaiKH + "'";

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
                    cbbMaloai.Items.Add(dtResult.Rows[i]["MaLoai"].ToString());
                    arrTen.Add(dtResult.Rows[i]["TenLoai"].ToString());
                    arrTenthe.Add(dtResult.Rows[i]["TenThe"].ToString());
                    arrLoaiKH.Add(dtResult.Rows[i]["LoaiKH"].ToString());
                }
                catch { }
            }
        }

        private void layDS_TheKH()
        {
            arrThe = new ArrayList();

            cbbTheKH.Items.Clear();
            cbbTheKH.Refresh();
            strCmd = "SELECT * FROM DMTHE ";

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
                    cbbTheKH.Items.Add(dtResult.Rows[i]["TenThe"].ToString());
                    arrThe.Add(dtResult.Rows[i]["MaThe"].ToString());
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

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã", typeof(string));
            dtDanhsach.Columns.Add(col);            
            col = new DataColumn("Tiêu chí", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thẻ KH", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));
            dtDanhsach.Columns.Add(col);            

            strCmd = "Select xl.*, the.TenThe from DMXEPLOAIKH xl inner join DMTHE the on xl.MaThe=the.MaThe ";
            if (rdbCN.Checked == true)
            {
                strCmd += " Where xl.loaikh='1'";
            }
            else if (rdbDN.Checked == true)
            {
                strCmd += " Where xl.loaikh='2'";
            }

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
                    string loaiKH = "";
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaLoai"].ToString();
                    row[2] = dtResult.Rows[i]["TenLoai"].ToString();
                    row[3] = dtResult.Rows[i]["TenThe"].ToString();

                    if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    {
                        loaiKH = "Cá nhân";
                    }
                    else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    {
                        loaiKH = "Doanh nghiệp";
                    }
                    row[4] = loaiKH;

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
            dgvDanhsach.Columns[3].Width = 150;
            dgvDanhsach.Columns[4].Width = 120;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbMaloai.Text == "")
            {
                MessageBox.Show("Chưa nhập mã.", "Thông báo");
                cbbMaloai.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMXEPLOAIKH WHERE MaLoai='" + cbbMaloai.Text.Trim() + "'";

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


            int loaiKH = 0;
            if (rdbCN.Checked == true)
            {
                loaiKH = 1;
            }
            else if (rdbDN.Checked == true)
            {
                loaiKH = 2;
            }

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into DMXEPLOAIKH(MaLoai, TenLoai, MaThe, LoaiKH) ";
                strCmd += "Values('" + cbbMaloai.Text.Trim() + "',N'" + txtTieuchi.Text.Trim() + "','" + arrThe[cbbTheKH.Items.IndexOf(cbbTheKH.Text.Trim())].ToString() + "','" + loaiKH + "')";

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
                    layDS_Maloai();
                    layDS_TheKH();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbMaloai.Focus();
                cbbMaloai.Text = "";
                txtTieuchi.Text = "";
                cbbTheKH.Text = "";
            }
            else
            {
                MessageBox.Show("Mã này đã tồn tại.", "Cảnh báo");
                cbbMaloai.Focus();
                cbbMaloai.Text = "";
                return;
            }            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbMaloai.Text == "")
            {
                MessageBox.Show("Chưa nhập mã.", "Thông báo");
                cbbMaloai.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMXEPLOAIKH WHERE MaLoai='" + cbbMaloai.Text.Trim() + "'";

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

            int loaiKH = 0;
            if (rdbCN.Checked == true)
            {
                loaiKH = 1;
            }
            else if (rdbDN.Checked == true)
            {
                loaiKH = 2;
            }

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update DMXEPLOAIKH ";
                strCmd += "SET TenLoai=N'" + txtTieuchi.Text.Trim() + "', MaThe='" + arrThe[cbbTheKH.Items.IndexOf(cbbTheKH.Text.Trim())].ToString() + "', LoaiKH='" + loaiKH + "'";
                strCmd += "WHERE (MaLoai='" + cbbMaloai.Text.Trim() + "')";

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
                    layDS_TheKH();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbMaloai.Focus();
                cbbMaloai.Text = "";
                txtTieuchi.Text = "";
                cbbTheKH.Text = "";
            }
            else
            {
                MessageBox.Show("Mã này chưa tồn tại.", "Cảnh báo");
                cbbMaloai.Focus();
                cbbMaloai.Text = "";
                return;
            }        
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT xlkh.MaLoai ";
            strCmd += "FROM DMXEPLOAIKH xlkh INNER JOIN DMDIEMXL diemxl ON xlkh.MaLoai = diemxl.MaLoai ";
            strCmd += "WHERE (xlkh.MaLoai = '" + dgvDanhsach.CurrentRow.Cells["Mã"].Value.ToString() + "')";

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
                if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        strCmd = "Delete from DMXEPLOAIKH Where MaLoai='" + dgvDanhsach.CurrentRow.Cells["Mã"].Value.ToString() + "'";
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
                    cbbMaloai.Text = "";
                    txtTieuchi.Text = "";
                    cbbTheKH.Text = "";
                    layDanhsach();
                    layDS_Maloai();
                    layDS_TheKH();
                }
            }
            else
            {
                MessageBox.Show("Tiêu chí này đã cập nhật điểm. Không cho phép xóa.", "Thông báo");
            }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMaloai.Text = dgvDanhsach.CurrentRow.Cells["Mã"].Value.ToString();
                txtTieuchi.Text = dgvDanhsach.CurrentRow.Cells["Tiêu chí"].Value.ToString();
                cbbTheKH.Text = dgvDanhsach.CurrentRow.Cells["Thẻ KH"].Value.ToString();

                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Cá nhân")
                {
                    rdbCN.Checked = true;
                }
                else if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Doanh nghiệp")
                {
                    rdbDN.Checked = true;
                }
            }
            catch { }
        }

        private void cbbMaloai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbMaloai_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbbMaloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTieuchi.Text = arrTen[cbbMaloai.Items.IndexOf(cbbMaloai.Text.Trim())].ToString();
            cbbTheKH.Text = arrTenthe[cbbMaloai.Items.IndexOf(cbbMaloai.Text.Trim())].ToString();
            if (arrLoaiKH[cbbMaloai.Items.IndexOf(cbbMaloai.Text.Trim())].ToString() == "1")
            {
                rdbCN.Checked = true;
            }
            else if (arrLoaiKH[cbbMaloai.Items.IndexOf(cbbMaloai.Text.Trim())].ToString() == "2")
            {
                rdbDN.Checked = true;
            }
        }

        private void rdbCN_CheckedChanged(object sender, EventArgs e)
        {            
            cbbMaloai.Text = "";
            txtTieuchi.Text = "";
            layDS_Maloai();
            layDanhsach();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}