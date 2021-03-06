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
    public partial class frmDM_Diem : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrList = new ArrayList();
        ArrayList arrDiem, arrNgayBD, arrNgayKT;
        public static string strMaCN = "";
        
        public frmDM_Diem()
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

            cbbMaCN.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbTenCT.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmDM_Diem_Load(object sender, EventArgs e)
        {
            layDS_CN();
            strMaCN = cbbMaCN.SelectedValue.ToString();

            if (frmMain.cn == "4800")
            {
                cbbMaCN.Enabled = true;
                llbCopy.Visible = false;                
            }
            else
            {
                cbbMaCN.Enabled = false;
                llbCopy.Visible = true;
            }
            
            layDanhsach();            
            layDS_Chitieu();
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
            col = new DataColumn("Mã CT", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên chỉ tiêu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày hiệu lực", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT diem.*, ct.DienGiai AS TenCT FROM DMDIEM diem INNER JOIN DMCHITIEU ct ON diem.MACT = ct.MACT ";
            strCmd += " Where diem.MaCN='" + cbbMaCN.SelectedValue.ToString() + "' ";
            if (rdbCN.Checked == true)
            {
                strCmd += " and diem.loaikh='1' ORDER BY diem.MaCN, diem.LoaiKH, diem.MaCT, diem.NgayBDHL";
            }
            else if (rdbDN.Checked == true)
            {
                strCmd += " and diem.loaikh='2' ORDER BY diem.MaCN, diem.LoaiKH, diem.MaCT, diem.NgayBDHL";
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
                    row[3] = dtResult.Rows[i]["MaCT"].ToString();
                    row[4] = dtResult.Rows[i]["TenCT"].ToString();
                    row[5] = dtResult.Rows[i]["Diem"].ToString();

                    string ngayBDHL, ngayHetHL;
                    ngayBDHL = dtResult.Rows[i]["NgayBDHL"].ToString();
                    ngayHetHL = dtResult.Rows[i]["NgayHetHL"].ToString();

                    string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
                    ngayBD = ngayBDHL.Substring(0, 2);
                    thangBD = ngayBDHL.Substring(3, 2);
                    namBD = ngayBDHL.Substring(6, 4);
                    ngayKT = ngayHetHL.Substring(0, 2);
                    thangKT = ngayHetHL.Substring(3, 2);
                    namKT = ngayHetHL.Substring(6, 4);

                    row[6] = ngayBD + "/" + thangBD + "/" + namBD;
                    row[7] = ngayKT + "/" + thangKT + "/" + namKT;
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 80;
            dgvDanhsach.Columns[2].Width = 120;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].Width = 260;
            dgvDanhsach.Columns[5].Width = 70;
            dgvDanhsach.Columns[6].Width = 130;
            dgvDanhsach.Columns[7].Width = 130;
        }

        private void layDS_Chitieu()
        {
            arrDiem = new ArrayList();
            arrNgayBD = new ArrayList();
            arrNgayKT = new ArrayList();

            arrList.Clear();
            cbbTenCT.Items.Clear();
            cbbTenCT.Refresh();

            strCmd = "select ct.*, d.* from dmchitieu ct left join dmdiem d on ct.mact=d.mact ";
            //strCmd += " where d.macn='" + cbbMaCN.Text.Trim() + "' ";
            //strCmd = "select * from dmchitieu ";
            
            if (rdbCN.Checked == true)
            {
                strCmd += " Where ct.loaikh='1' and d.MaCN='" + cbbMaCN.SelectedValue.ToString() + "' Order by ct.MaCT ";
            }
            else if (rdbDN.Checked == true)
            {
                strCmd += " Where ct.loaikh='2' and d.MaCN='" + cbbMaCN.SelectedValue.ToString() + "' Order by ct.MaCT ";
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
                    cbbTenCT.Items.Add(dtResult.Rows[i]["DienGiai"].ToString());
                    arrList.Add(dtResult.Rows[i]["MaCT"].ToString());
                    arrDiem.Add(dtResult.Rows[i]["Diem"].ToString());
                    arrNgayBD.Add(dtResult.Rows[i]["NgayBDHL"].ToString());
                    arrNgayKT.Add(dtResult.Rows[i]["NgayHetHL"].ToString());
                }
                catch { }
            }

            if (iRows > 0)
            {
                cbbTenCT.SelectedIndex = 0;
            }
        }

        private void cbbTenCT_TextChanged(object sender, EventArgs e)
        {
            txtMaCT.Text = "";
            txtMaCT.Text = arrList[cbbTenCT.Items.IndexOf(cbbTenCT.Text.Trim())].ToString();            
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMaCN.SelectedValue = dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString();

                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Cá nhân")
                {
                    rdbCN.Checked = true;
                }
                else if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Doanh nghiệp")
                {
                    rdbDN.Checked = true;
                }
                
                cbbTenCT.Text = dgvDanhsach.CurrentRow.Cells["Tên chỉ tiêu"].Value.ToString();
                txtDiem.Text = dgvDanhsach.CurrentRow.Cells["Điểm"].Value.ToString();
                dtpNgayBD.Text = dgvDanhsach.CurrentRow.Cells["Ngày hiệu lực"].Value.ToString();
                dtpNgayKT.Text = dgvDanhsach.CurrentRow.Cells["Ngày kết thúc"].Value.ToString();
                
            }
            catch
            {
                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbMaCN.Text == "")
            {
                MessageBox.Show("Chưa chọn mã chi nhánh.", "Thong bao");
                cbbMaCN.Focus();
                return;
            }
            else if (cbbTenCT.Text == "")
            {
                MessageBox.Show("Chưa chọn chỉ tiêu.", "Thong bao");
                cbbTenCT.Focus();
                return;
            }            
            
            int loaiKH = 0;
            if (rdbCN.Checked == true)
            {
                loaiKH = 1;
            }
            else if (rdbDN.Checked == true)
            {
                loaiKH = 2;
            }

            string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
            ngayBD = dtpNgayBD.Text.Substring(0, 2);
            thangBD = dtpNgayBD.Text.Substring(3, 2);
            namBD = dtpNgayBD.Text.Substring(6, 4);
            ngayKT = dtpNgayKT.Text.Substring(0, 2);
            thangKT = dtpNgayKT.Text.Substring(3, 2);
            namKT = dtpNgayKT.Text.Substring(6, 4);

            strCmd = "SELECT * FROM DMDiem ";
            strCmd += " WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' AND LoaiKH='" + loaiKH + "' AND MaCT='" + txtMaCT.Text.Trim() + "' AND NgayBDHL='" + thangBD + "/" + ngayBD + "/" + namBD + "' ";

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
                strCmd = "Insert into DMDiem(MaCN, LoaiKH, MACT, Diem, NgayBDHL, NgayHetHL) ";
                strCmd += "Values('" + cbbMaCN.SelectedValue.ToString() + "','" + loaiKH + "','" + txtMaCT.Text.Trim() + "','" + txtDiem.Text.Trim();
                strCmd += "','" + thangBD + "/" + ngayBD + "/" + namBD + "','" + thangKT + "/" + ngayKT + "/" + namKT + "')";

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
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                cbbTenCT.Focus();
                txtDiem.Text = "";                
            }
            else
            {
                MessageBox.Show("Chỉ tiêu này đã tồn tại.", "Canh bao");
                cbbTenCT.Focus();
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbMaCN.Text == "")
            {
                MessageBox.Show("Chưa chọn mã chi nhánh.", "Thông báo");
                cbbMaCN.Focus();
                return;
            }
            else if (cbbTenCT.Text == "")
            {
                MessageBox.Show("Chưa chọn chỉ tiêu.", "Thông báo");
                cbbTenCT.Focus();
                return;
            }
            
            int loaiKH = 0;
            if (rdbCN.Checked == true)
            {
                loaiKH = 1;
            }
            else if (rdbDN.Checked == true)
            {
                loaiKH = 2;
            }

            string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
            ngayBD = dtpNgayBD.Text.Substring(0, 2);
            thangBD = dtpNgayBD.Text.Substring(3, 2);
            namBD = dtpNgayBD.Text.Substring(6, 4);
            ngayKT = dtpNgayKT.Text.Substring(0, 2);
            thangKT = dtpNgayKT.Text.Substring(3, 2);
            namKT = dtpNgayKT.Text.Substring(6, 4);

            strCmd = "SELECT * FROM DMDiem WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' AND LoaiKH='" + loaiKH + "' AND MaCT='" + txtMaCT.Text.Trim() + "' ";

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
                if (DateTime.Now < dtpNgayBD.Value)
                {
                    strCmd = "UPDATE DMDiem ";
                    strCmd += "SET Diem='" + txtDiem.Text.Trim() + "',NgayBDHL='" + thangBD + "/" + ngayBD + "/" + namBD + "',NgayHetHL='" + thangKT + "/" + ngayKT + "/" + namKT + "' ";
                    strCmd += "WHERE (MaCN='" + cbbMaCN.SelectedValue.ToString() + "' AND LoaiKH='" + loaiKH + "' AND MaCT='" + txtMaCT.Text.Trim() + "') ";
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
                }
                else
                {
                    MessageBox.Show("Điểm của chỉ tiêu này đã có hiệu lực. Không cho phép thay đổi.", "Cảnh báo");
                    cbbTenCT.Focus();
                    return;
                }
                
                cbbTenCT.Focus();
                txtDiem.Text = "";
            }
            else
            {
                MessageBox.Show("Chỉ tiêu này chưa tồn tại.", "Cảnh báo");
                cbbTenCT.Focus();
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DateTime today = DateTime.Now;
                dtpNgayBD.Text = dgvDanhsach.CurrentRow.Cells["Ngày hiệu lực"].Value.ToString();

                if (today < dtpNgayBD.Value)
                {
                    int loaiKH = 0;
                    if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Cá nhân")
                    {
                        loaiKH = 1;
                    }
                    else if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Doanh nghiệp")
                    {
                        loaiKH = 2;
                    }

                    string ngayBDHL;
                    ngayBDHL = dgvDanhsach.CurrentRow.Cells["Ngày hiệu lực"].Value.ToString();
                    string ngayBD, thangBD, namBD;
                    ngayBD = ngayBDHL.Substring(0, 2);
                    thangBD = ngayBDHL.Substring(3, 2);
                    namBD = ngayBDHL.Substring(6, 4);

                    try
                    {
                        strCmd = "Delete from DMDIEM Where MACN='" + dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString() + "' AND LoaiKH='" + loaiKH + "' ";
                        strCmd += "AND MaCT='" + dgvDanhsach.CurrentRow.Cells["Mã CT"].Value.ToString() + "' AND NgayBDHL='" + thangBD + "/" + ngayBD + "/" + namBD + "'";
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                        layDanhsach();
                        layDS_Chitieu();
                        MessageBox.Show("Đã xóa", "Thông báo");
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }
                    txtDiem.Text = "";
                }
                else
                {
                    MessageBox.Show("Điểm của chỉ tiêu này đã có hiệu lực. Không cho phép xóa", "Cảnh báo");
                    return;
                }
            }
        }

        private void rdbCN_CheckedChanged(object sender, EventArgs e)
        {
            layDanhsach();
            layDS_Chitieu();
        }

        private void cbbTenCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbbTenCT_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtMaCT.Text = "";
            txtMaCT.Text = arrList[cbbTenCT.Items.IndexOf(cbbTenCT.Text.Trim())].ToString();
            txtDiem.Text = "";
            txtDiem.Text = arrDiem[cbbTenCT.Items.IndexOf(cbbTenCT.Text.Trim())].ToString();
            dtpNgayBD.Value = DateTime.Now;
            dtpNgayBD.Text = arrNgayBD[cbbTenCT.Items.IndexOf(cbbTenCT.Text.Trim())].ToString();
            dtpNgayKT.Value = dtpNgayKT.MaxDate;
            dtpNgayKT.Text = arrNgayKT[cbbTenCT.Items.IndexOf(cbbTenCT.Text.Trim())].ToString();
        }

        private void cbbMaCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtMaCT.Text = "";
            //txtDiem.Text = "";
            //layDanhsach();
            //layDS_Chitieu();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbMaCN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            strMaCN = cbbMaCN.SelectedValue.ToString();
            if (cbbMaCN.SelectedValue.ToString() == "4800")
            {
                llbCopy.Visible = false;
            }
            else
            {
                llbCopy.Visible = true;
            }
            txtMaCT.Text = "";
            txtDiem.Text = "";
            layDanhsach();
            layDS_Chitieu();
        }

        private void llbCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDMDiem_Copy frmCopy = new frmDMDiem_Copy();
            frmCopy.ShowDialog();
            layDanhsach();
        }

        private void txtDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiem_TextChanged(object sender, EventArgs e)
        {
            if (txtDiem.Text != "")
            {
                string sDummy = txtDiem.Text;
                try
                {
                    int iKeep = txtDiem.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtDiem.Text[i] == ',')
                        {
                            iKeep -= 1;
                        }
                    }
                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                    {
                        if (sDummy[i] == ',')
                        {
                            iKeep += 1;
                        }
                    }
                    txtDiem.Text = sDummy;
                    txtDiem.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }        
    }
}