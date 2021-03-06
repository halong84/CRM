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
    public partial class frmDM_Tytrong : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrList, arrTytrong, arrTieuchi, arrNgayBD, arrNgayKT;
        public static string strMaCN = "";
        
        public frmDM_Tytrong()
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
            cbbTenNhomCT.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmDM_Tytrong_Load(object sender, EventArgs e)
        {
            layDS_CN();
            strMaCN = cbbMaCN.SelectedValue.ToString();

            if (Thongtindangnhap.macn == Thongtindangnhap.ma_hoi_so)
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
            layDS_NhomCT();
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
            col = new DataColumn("Mã nhóm CT", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tiêu chí", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỷ trọng (%)", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày hiệu lực", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên nhóm CT", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT tt.*, nhomct.TenNhom FROM DMTYTRONG tt INNER JOIN DMNHOMCT nhomct ON tt.MaNhom = nhomct.MaNhom ";
            strCmd += " Where tt.MaCN='" + cbbMaCN.SelectedValue.ToString() + "' ";
            if (rdbCN.Checked == true)
            {
                strCmd += " and tt.loaikh='1' ORDER BY tt.MaCN, tt.LoaiKH, tt.MaNhom, tt.NgayBatdauHL";
            }
            else if (rdbDN.Checked == true)
            {
                strCmd += " and tt.loaikh='2' ORDER BY tt.MaCN, tt.LoaiKH, tt.MaNhom, tt.NgayBatdauHL";
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
                    row[3] = dtResult.Rows[i]["MaNhom"].ToString();
                    row[4] = dtResult.Rows[i]["DienGiai"].ToString();
                    row[5] = dtResult.Rows[i]["Tytrong"].ToString();

                    string ngayBDHL, ngayHetHL;
                    ngayBDHL = dtResult.Rows[i]["NgaybatdauHL"].ToString().Substring(0, 10);
                    ngayHetHL = dtResult.Rows[i]["NgayHetHL"].ToString().Substring(0, 10);

                    row[6] = ngayBDHL; ;
                    row[7] = ngayHetHL;
                    row[8] = dtResult.Rows[i]["TenNhom"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 90;
            dgvDanhsach.Columns[2].Width = 120;
            dgvDanhsach.Columns[3].Width = 130;
            dgvDanhsach.Columns[4].Width = 260;
            dgvDanhsach.Columns[5].Width = 120;
            dgvDanhsach.Columns[6].Width = 130;
            dgvDanhsach.Columns[7].Width = 130;
            dgvDanhsach.Columns[8].Visible = false;
        }

        private void layDS_CN()
        {
            DataTable dt = cnbus.DANH_SACH_CHI_NHANH();

            //cbbMaCN.DataSource = dt;
            cbbMaCN.DisplayMember = "TENCN";
            cbbMaCN.ValueMember = "MACN";
            cbbMaCN.DataSource = dt;
            cbbMaCN.SelectedValue = Thongtindangnhap.macn;
        }

        private void layDS_NhomCT()
        {
            arrList = new ArrayList();
            arrTytrong = new ArrayList();
            arrTieuchi = new ArrayList();
            arrNgayBD = new ArrayList();
            arrNgayKT = new ArrayList();
            
            arrList.Clear();
            cbbTenNhomCT.Items.Clear();
            cbbTenNhomCT.Refresh();

            strCmd = "select distinct n.*, t.* from dmnhomct n inner join dmtytrong t on n.manhom=t.manhom ";
            strCmd += "where t.macn='" + cbbMaCN.SelectedValue.ToString() + "' ";
            //strCmd = "select * from DMTYTRONG ";
            //strCmd += "where macn='" + cbbMaCN.Text.Trim() + "' ";

            if (rdbCN.Checked == true)
            {
                strCmd += " and t.loaikh='1'";
            }
            else if (rdbDN.Checked == true)
            {
                strCmd += " and t.loaikh='2'";
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
                    cbbTenNhomCT.Items.Add(dtResult.Rows[i]["TenNhom"].ToString());
                    arrList.Add(dtResult.Rows[i]["MaNhom"].ToString());
                    //arrTytrong.Add(dtResult.Rows[i]["Tytrong"].ToString());
                    //arrTieuchi.Add(dtResult.Rows[i]["DienGiai"].ToString());
                    //arrNgayBD.Add(dtResult.Rows[i]["NgaybatdauHL"].ToString());
                    //arrNgayKT.Add(dtResult.Rows[i]["NgayhetHL"].ToString());
                }
                catch { }
            }

            if (iRows > 0)
            {
                cbbTenNhomCT.SelectedIndex = 0;
            }
        }

        private void cbbTenNhomCT_TextChanged(object sender, EventArgs e)
        {
            txtMaNhomCT.Text = "";
            txtMaNhomCT.Text = arrList[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();            
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

                cbbTenNhomCT.Text = dgvDanhsach.CurrentRow.Cells["Tên nhóm CT"].Value.ToString();
                txtTytrong.Text = dgvDanhsach.CurrentRow.Cells["Tỷ trọng (%)"].Value.ToString();
                txtTieuchi.Text = dgvDanhsach.CurrentRow.Cells["Tiêu chí"].Value.ToString();
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
                MessageBox.Show("Chưa chọn mã chi nhánh.", "Thông báo");
                cbbMaCN.Focus();
                return;
            }
            else if (cbbTenNhomCT.Text == "")
            {
                MessageBox.Show("Chưa chọn nhóm chỉ tiêu.", "Thông báo");
                cbbTenNhomCT.Focus();
                return;
            }
            else if (txtTytrong.Text == "")
            {
                MessageBox.Show("Chưa nhập tỷ trọng.", "Thông báo");
                txtTytrong.Focus();
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

            strCmd = "SELECT * FROM DMTYTRONG WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' AND LoaiKH='" + loaiKH + "' AND MaNhom='" + txtMaNhomCT.Text.Trim() + "' AND NgaybatdauHL='" + thangBD + "/" + ngayBD + "/" + namBD + "'";

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
                strCmd = "Insert into DMTYTRONG(MaCN, LoaiKH, MaNhom, Tytrong, DienGiai, NgaybatdauHL, NgayHetHL) ";
                strCmd += "Values('" + cbbMaCN.SelectedValue.ToString() + "','" + loaiKH + "','" + txtMaNhomCT.Text.Trim() + "','" + txtTytrong.Text.Trim() + "',N'" + txtTieuchi.Text.Trim();
                strCmd += "','" + thangBD + "/" + ngayBD + "/" + namBD + "','" + thangKT + "/" + ngayKT + "/" + namKT + "')";

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
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbTenNhomCT.Focus();
                txtTytrong.Text = "";
            }
            else
            {
                MessageBox.Show("Tiêu chí này đã tồn tại.", "Cảnh báo");
                cbbTenNhomCT.Focus();
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
            else if (cbbTenNhomCT.Text == "")
            {
                MessageBox.Show("Chưa chọn nhóm chỉ tiêu.", "Thông báo");
                cbbTenNhomCT.Focus();
                return;
            }
            else if (txtTytrong.Text == "")
            {
                MessageBox.Show("Chưa nhập tỷ trọng.", "Thông báo");
                txtTytrong.Focus();
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

            strCmd = "SELECT * FROM DMTYTRONG WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' AND LoaiKH='" + loaiKH + "' AND MaNhom='" + txtMaNhomCT.Text.Trim() + "' ";

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
                if (DateTime.Now < dtpNgayBD.Value)
                {
                    strCmd = "UPDATE DMTYTRONG ";
                    strCmd += "SET Tytrong='" + txtTytrong.Text.Trim() + "',NgaybatdauHL='" + thangBD + "/" + ngayBD + "/" + namBD + "',NgayHetHL='" + thangKT + "/" + ngayKT + "/" + namKT + "',DienGiai=N'" + txtTieuchi.Text.Trim() + "' ";
                    strCmd += "WHERE (MaCN='" + cbbMaCN.SelectedValue.ToString() + "' AND LoaiKH='" + loaiKH + "' AND MaNhom='" + txtMaNhomCT.Text.Trim() + "')";
                }
                else
                {
                    MessageBox.Show("Tỷ trọng của chỉ tiêu này đã có hiệu lực. Không cho phép thay đổi.", "Cảnh báo");
                    cbbTenNhomCT.Focus();
                    return;
                }

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
                cbbTenNhomCT.Focus();
                txtTytrong.Text = "";
            }
            else
            {
                MessageBox.Show("Tiêu chí này chưa tồn tại.", "Cảnh báo");
                cbbTenNhomCT.Focus();
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
                        strCmd = "Delete from DMTYTRONG Where MACN='" + dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString() + "' AND LoaiKH='" + loaiKH + "' ";
                        strCmd += "AND MaNhom='" + dgvDanhsach.CurrentRow.Cells["Mã nhóm CT"].Value.ToString() + "' AND NgaybatdauHL='" + thangBD + "/" + ngayBD + "/" + namBD + "'";
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                        layDanhsach();
                        layDS_NhomCT();
                        MessageBox.Show("Đã xóa", "Thông báo");
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }
                    txtTytrong.Text = "";
                    txtTieuchi.Text = "";
                }
                else
                {
                    MessageBox.Show("Tỷ trọng điểm của tiêu chí này đã có hiệu lực. Không cho phép xóa", "Cảnh báo");
                    return;
                }
            }
        }

        private void cbbTenNhomCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtMaNhomCT.Text = "";
            //txtMaNhomCT.Text = arrList[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            //txtTytrong.Text = "";
            ////txtTytrong.Text = arrTytrong[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            //txtTieuchi.Text = "";
            ////txtTieuchi.Text = arrTieuchi[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            ////dtpNgayBD.Text = arrNgayBD[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            ////dtpNgayKT.Text = arrNgayKT[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
        }

        private void rdbCN_CheckedChanged(object sender, EventArgs e)
        {
            layDanhsach();
            layDS_NhomCT();
        }

        private void cbbMaCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtMaNhomCT.Text = "";
            //txtTytrong.Text = "";
            //txtTieuchi.Text = "";
            //layDanhsach();
            //layDS_NhomCT();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbMaCN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            strMaCN = cbbMaCN.SelectedValue.ToString();
            if (cbbMaCN.SelectedValue.ToString() == Thongtindangnhap.ma_hoi_so)
            {
                llbCopy.Visible = false;
            }
            else
            {
                llbCopy.Visible = true;
            }
            txtMaNhomCT.Text = "";
            txtTytrong.Text = "";
            txtTieuchi.Text = "";
            layDanhsach();
            layDS_NhomCT();
        }

        private void cbbTenNhomCT_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtMaNhomCT.Text = "";
            txtMaNhomCT.Text = arrList[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            txtTytrong.Text = "";
            //txtTytrong.Text = arrTytrong[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            txtTieuchi.Text = "";
            //txtTieuchi.Text = arrTieuchi[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            //dtpNgayBD.Text = arrNgayBD[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            //dtpNgayKT.Text = arrNgayKT[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
        }

        private void llbCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDMtytrong_Copy frmCopy = new frmDMtytrong_Copy();
            frmCopy.ShowDialog();
            layDanhsach();
        }

        private void txtTytrong_TextChanged(object sender, EventArgs e)
        {
            if (txtTytrong.Text != "")
            {
                string sDummy = txtTytrong.Text;
                try
                {
                    int iKeep = txtTytrong.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtTytrong.Text[i] == ',')
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
                    txtTytrong.Text = sDummy;
                    txtTytrong.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtTytrong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}