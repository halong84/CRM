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
    public partial class frmTygia : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrTygia = new ArrayList();

        public frmTygia()
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

            cbbThang.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbThang.SelectedIndex = 0;
        }

        private void frmTygia_Load(object sender, EventArgs e)
        {
            layDanhsach();
        }

        private void layTygia()
        {
            arrTygia.Clear();

            strCmd = "SELECT Top 1 * FROM TYGIA WHERE Thang='" + cbbThang.Text.Trim() + "' ";

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
            if (iRows > 0)
            {
                try
                {
                    arrTygia.Add(String.Format("{0:0}", Decimal.Parse(dtResult.Rows[0]["Tygia"].ToString())));
                }
                catch { }
            }
            else
            {
                arrTygia.Add("");
            }            
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tháng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỷ giá", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            
            strCmd = "SELECT * FROM TYGIA ";
            
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
                    if (dtResult.Rows[i]["Thang"].ToString().Length == 1)
                    {
                        row[1] = "0" + dtResult.Rows[i]["Thang"].ToString();
                    }
                    else
                    {
                        row[1] = dtResult.Rows[i]["Thang"].ToString();
                    }
                    row[2] = String.Format("{0:0}", Decimal.Parse(dtResult.Rows[i]["Tygia"].ToString()));

                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 60;
            dgvDanhsach.Columns[2].Width = 140;
            dgvDanhsach.Columns[2].DefaultCellStyle.Format = "N0";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT * FROM Tygia WHERE Thang='" + cbbThang.Text.Trim() + "' ";

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
                strCmd = "Insert into TYGIA(Thang, Tygia) ";
                strCmd += "Values('" + cbbThang.Text.Trim() + "','" + Convert.ToDecimal(txtTygia.Text.Trim()) + "')";

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
                cbbThang.Focus();
                txtTygia.Text = "";
                
            }
            else
            {
                MessageBox.Show("Tỷ giá tháng này đã tồn tại.", "Cảnh báo");
                cbbThang.Focus();
                return;
            }            
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    layDS_Tygia();
        //    if (arrTygia.Count > 0)
        //    {
        //        txtTygia.Text = arrTygia[cbbThang.Items.IndexOf(cbbThang.Text.Trim())].ToString();
        //    }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT * FROM TYGIA WHERE Thang='" + cbbThang.Text.Trim() + "' ";

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
                strCmd = "UPDATE TYGIA ";
                strCmd += "SET Tygia='" + Convert.ToDecimal(txtTygia.Text.Trim()) + "' ";
                strCmd += "WHERE Thang='" + cbbThang.Text.Trim() + "'";

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
                cbbThang.Focus();
                txtTygia.Text = "";
                
            }
            else
            {
                MessageBox.Show("Tỷ giá tháng này chưa tồn tại.", "Cảnh báo");
                cbbThang.Focus();
                return;
            }            
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbThang.Text = dgvDanhsach.CurrentRow.Cells["Tháng"].Value.ToString();
                txtTygia.Text = dgvDanhsach.CurrentRow.Cells["Tỷ giá"].Value.ToString();
            }
            catch { }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT Thang ";
            strCmd += "FROM TYGIA ";
            strCmd += "WHERE (Thang = '" + dgvDanhsach.CurrentRow.Cells["Tháng"].Value.ToString() + "')";

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
                    strCmd = "Delete from TYGIA Where Thang='" + dgvDanhsach.CurrentRow.Cells["Tháng"].Value.ToString() + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    MessageBox.Show("Đã xóa", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtTygia.Text = "";                
            }            
        }

        private void cbbThang_TextChanged(object sender, EventArgs e)
        {
            layTygia();
            txtTygia.Text = arrTygia[0].ToString();            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTygia_TextChanged(object sender, EventArgs e)
        {
            if (txtTygia.Text != "")
            {
                string sDummy = txtTygia.Text;
                try
                {
                    int iKeep = txtTygia.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtTygia.Text[i] == ',')
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

                    txtTygia.Text = sDummy;
                    txtTygia.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtTygia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}