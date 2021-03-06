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
    public partial class frmDM_Chitieu : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrList = new ArrayList();
        ArrayList arrListLoaiKH, arrListGiatri, arrListDonvi, arrListTenCT;
        
        public frmDM_Chitieu()
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

            cbbTenNhomCT.DropDownStyle = ComboBoxStyle.DropDownList;            
        }

        private void frmDM_Chitieu_Load(object sender, EventArgs e)
        {            
            //layDanhsach();
            layDS_NhomCT();
            layDS_Donvi();
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CT", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên chỉ tiêu", typeof(string));
            dtDanhsach.Columns.Add(col);            
            col = new DataColumn("Giá trị", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đơn vị", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã nhóm CT", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên nhóm CT", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT ct.*, nhomct.* FROM DMCHITIEU ct INNER JOIN DMNHOMCT nhomct ON ct.MaNhom = nhomct.MaNhom ";
            strCmd += " Where ct.MaNhom='" + txtMaNhomCT.Text.Trim() + "' ";
            if (rdbCN.Checked == true)
            {
                strCmd += " and ct.loaikh='1' ORDER BY LoaiKH, MaCT";
            }
            else if (rdbDN.Checked == true)
            {
                strCmd += " and ct.loaikh='2' ORDER BY LoaiKH, MaCT";
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
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;

                    string loaiKH = "";
                    if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    {
                        loaiKH = "Cá nhân";
                    }
                    else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    {
                        loaiKH = "Doanh nghiệp";
                    }
                    row[1] = loaiKH;


                    row[2] = dtResult.Rows[i]["MaCT"].ToString();
                    row[3] = dtResult.Rows[i]["DienGiai"].ToString();
                    row[4] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Giatri"].ToString()));
                    row[5] = dtResult.Rows[i]["DonVi"].ToString();
                    row[6] = dtResult.Rows[i]["MaNhom"].ToString();
                    row[7] = dtResult.Rows[i]["TenNhom"].ToString();

                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 120;
            dgvDanhsach.Columns[2].Width = 120;
            dgvDanhsach.Columns[3].Width = 250;
            dgvDanhsach.Columns[4].Width = 120;
            dgvDanhsach.Columns[4].DefaultCellStyle.Format = "N0";
            dgvDanhsach.Columns[5].Width = 100;
            dgvDanhsach.Columns[6].Width = 120;
            dgvDanhsach.Columns[7].Width = 300;
        }

        private void layDS_NhomCT()
        {
            cbbTenNhomCT.Items.Clear();
            cbbTenNhomCT.Refresh();
            strCmd = "SELECT MaNhom, TenNhom FROM DMNHOMCT ";

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
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }

            //if (iRows > 0)
            //{
            //    cbbTenNhomCT.SelectedIndex = 0;
            //}
        }        

        private void layDS_MaCT()
        {
            arrListLoaiKH = new ArrayList();
            arrListGiatri = new ArrayList();
            arrListDonvi = new ArrayList();
            arrListTenCT = new ArrayList();

            cbbMaCT.Items.Clear();
            cbbMaCT.Refresh();
            strCmd = "SELECT ct.* FROM DMCHITIEU ct INNER JOIN DMNHOMCT nhomct ON ct.MaNhom = nhomct.MaNhom ";
            strCmd += "WHERE (ct.MaNhom = '" + txtMaNhomCT.Text.Trim() + "')";

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
                    cbbMaCT.Items.Add(dtResult.Rows[i]["MACT"].ToString());
                    arrListLoaiKH.Add(dtResult.Rows[i]["LoaiKH"].ToString());
                    arrListGiatri.Add(dtResult.Rows[i]["GiaTri"].ToString());
                    arrListDonvi.Add(dtResult.Rows[i]["DonVi"].ToString());
                    arrListTenCT.Add(dtResult.Rows[i]["DienGiai"].ToString());
                }
                catch { }
            }
        }

        private void layDS_Donvi()
        {
            cbbDonvi.Items.Clear();
            cbbDonvi.Refresh();
            strCmd = "SELECT DISTINCT DonVi FROM DMCHITIEU ";
            
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
                    cbbDonvi.Items.Add(dtResult.Rows[i]["DonVi"].ToString());
                }
                catch { }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbTenNhomCT.Text == "")
            {
                MessageBox.Show("Chưa chọn nhóm chỉ tiêu.", "Thông báo");
                cbbTenNhomCT.Focus();
                return;
            }
            else if (cbbMaCT.Text == "")
            {
                MessageBox.Show("Chưa nhập mã chỉ tiêu.", "Thông báo");
                cbbMaCT.Focus();
                return;
            }
            else if (txtGiatri.Text == "")
            {
                MessageBox.Show("Chưa nhập giá trị.", "Thông báo");
                txtGiatri.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMCHITIEU WHERE MACT='" + cbbMaCT.Text.Trim() + "'";

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
                strCmd = "Insert into DMCHITIEU(MaNhom, MACT, GiaTri, DienGiai, DonVi, LoaiKH) ";
                strCmd += "Values('" + txtMaNhomCT.Text.Trim() + "','" + cbbMaCT.Text.Trim() + "','" + txtGiatri.Text.Trim() + "',N'" + txtTenCT.Text.Trim() + "',N'" + cbbDonvi.Text.Trim() + "','" + loaiKH + "')";

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
                    layDS_MaCT();
                    layDS_Donvi();
                    MessageBox.Show("Đã thêm.", "Thong bao");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbMaCT.Focus();
                cbbMaCT.Text = "";
                txtTenCT.Text = "";
                txtGiatri.Text = "";
                cbbDonvi.Text = "";
            }
            else
            {
                MessageBox.Show("Mã chỉ tiêu này đã tồn tại.", "Canh bao");
                cbbMaCT.Focus();
                cbbMaCT.Text = "";
                return;
            }            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbTenNhomCT.Text == "")
            {
                MessageBox.Show("Chưa chọn nhóm chỉ tiêu.", "Thong bao");
                cbbTenNhomCT.Focus();
                return;
            }
            else if (cbbMaCT.Text == "")
            {
                MessageBox.Show("Chưa nhập mã chỉ tiêu.", "Thong bao");
                cbbMaCT.Focus();
                return;
            }
            else if (txtGiatri.Text == "")
            {
                MessageBox.Show("Chưa nhập giá trị.", "Thong bao");
                txtGiatri.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM DMCHITIEU WHERE MACT='" + cbbMaCT.Text.Trim() + "'";

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
                strCmd = "UPDATE DMCHITIEU ";
                strCmd += "SET MaNhom='" + txtMaNhomCT.Text.Trim() + "',GiaTri='" + txtGiatri.Text.Trim() + "',DienGiai=N'" + txtTenCT.Text.Trim() + "',DonVi=N'" + cbbDonvi.Text.Trim() + "',LoaiKH='" + loaiKH + "' ";
                strCmd += "WHERE (MACT='" + cbbMaCT.Text.Trim() + "')";

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
                    layDS_Donvi();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbMaCT.Focus();
                cbbMaCT.Text = "";
                txtTenCT.Text = "";
                txtGiatri.Text = "";
                cbbDonvi.Text = "";
            }
            else
            {
                MessageBox.Show("Mã chỉ tiêu này đã tồn tại.", "Canh bao");
                cbbMaCT.Focus();
                cbbMaCT.Text = "";
                return;
            }            
        }

        private void cbbTenNhomCT_TextChanged(object sender, EventArgs e)
        {
            txtMaNhomCT.Text = "";
            txtMaNhomCT.Text = arrList[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();            
        }
        
        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //flag = true;
            try
            {
                //if (flag == true)
                //{
                cbbTenNhomCT.Text = dgvDanhsach.CurrentRow.Cells["Tên nhóm CT"].Value.ToString();
                //txtMaNhomCT.Text = dgvDanhsach.CurrentRow.Cells["Mã nhóm CT"].Value.ToString();
                cbbMaCT.Text = dgvDanhsach.CurrentRow.Cells["Mã CT"].Value.ToString();
                txtTenCT.Text = dgvDanhsach.CurrentRow.Cells["Tên chỉ tiêu"].Value.ToString();
                txtGiatri.Text = dgvDanhsach.CurrentRow.Cells["Giá trị"].Value.ToString();
                cbbDonvi.Text = dgvDanhsach.CurrentRow.Cells["Đơn vị"].Value.ToString();

                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Cá nhân")
                {
                    rdbCN.Checked = true;
                }
                else if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "Doanh nghiệp")
                {
                    rdbDN.Checked = true;
                }
                //}
            }
            catch { }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT diem.MACT ";
            strCmd += "FROM DMCHITIEU ct INNER JOIN DMDIEM diem ON ct.MACT = diem.MACT ";
            strCmd += "WHERE (diem.MACT = '" + dgvDanhsach.CurrentRow.Cells["Mã CT"].Value.ToString() + "')";

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
                        strCmd = "Delete from DMCHITIEU Where MACT='" + dgvDanhsach.CurrentRow.Cells["Mã CT"].Value.ToString() + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                        layDanhsach();
                        layDS_MaCT();
                        layDS_Donvi();
                        MessageBox.Show("Đã xóa", "Thong bao");
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }
                    txtMaNhomCT.Text = "";
                    cbbMaCT.Text = "";
                    txtTenCT.Text = "";
                    txtGiatri.Text = "";
                    cbbDonvi.Text = "";                    
                }
            }
            else
            {
                MessageBox.Show("Chỉ tiêu này đã cập nhật điểm. Không cho phép xóa.", "Thong bao");
            }
        }

        private void cbbMaCT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbMaCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (arrListLoaiKH[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString() == "1")
                {
                    rdbCN.Checked = true;
                }
                else if (arrListLoaiKH[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString() == "2")
                {
                    rdbDN.Checked = true;
                }
                txtGiatri.Text = arrListGiatri[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString();
                cbbDonvi.Text = arrListDonvi[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString();
                txtTenCT.Text = arrListTenCT[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString();
            }
            catch { }
        }

        private void rdbCN_CheckedChanged(object sender, EventArgs e)
        {
            layDanhsach();
        }

        private void cbbMaCT_TextChanged(object sender, EventArgs e)
        {
            //if (cbbMaCT.Text.Trim() != "")
            //{
            //    if (arrListLoaiKH[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString() == "1")
            //    {
            //        rdbCN.Checked = true;
            //    }
            //    else if (arrListLoaiKH[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString() == "2")
            //    {
            //        rdbDN.Checked = true;
            //    }
            //    txtGiatri.Text = arrListGiatri[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString();
            //    cbbDonvi.Text = arrListDonvi[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString();
            //    txtTenCT.Text = arrListTenCT[cbbMaCT.Items.IndexOf(cbbMaCT.Text.Trim())].ToString();
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbTenNhomCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtMaNhomCT.Text = arrList[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            //cbbMaCT.Text = "";
            //txtGiatri.Text = "";
            //cbbDonvi.Text = "";
            //txtTenCT.Text = "";
            //layDanhsach();
            //layDS_MaCT();
        }

        private void txtGiatri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiatri_TextChanged(object sender, EventArgs e)
        {
            if (txtGiatri.Text != "")
            {
                string sDummy = txtGiatri.Text;
                try
                {
                    int iKeep = txtGiatri.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                        if (txtGiatri.Text[i] == ',')
                            iKeep -= 1;

                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                        if (sDummy[i] == ',')
                            iKeep += 1;

                    txtGiatri.Text = sDummy;
                    txtGiatri.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void cbbTenNhomCT_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtMaNhomCT.Text = arrList[cbbTenNhomCT.Items.IndexOf(cbbTenNhomCT.Text.Trim())].ToString();
            cbbMaCT.Text = "";
            txtGiatri.Text = "";
            cbbDonvi.Text = "";
            txtTenCT.Text = "";
            layDanhsach();
            layDS_MaCT();
        }       
    }
}