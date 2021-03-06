using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmNganhang : Form
    {
        public frmNganhang()
        {
            InitializeComponent();
        }

        private void txtSoKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoKH_Leave(object sender, EventArgs e)
        {
            string sDummy = txtSoKH.Text;
            try
            {
                int iKeep = txtSoKH.SelectionStart - 1;
                for (int i = iKeep; i >= 0; i--)
                    if (txtSoKH.Text[i] == ',')
                        iKeep -= 1;

                sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                for (int i = 0; i <= iKeep; i++)
                    if (sDummy[i] == ',')
                        iKeep += 1;

                txtSoKH.Text = sDummy;
                txtSoKH.SelectionStart = iKeep + 1;
            }
            catch
            {
            }
        }

        private void txtSoKH_KeyDown(object sender, KeyEventArgs e)
        {
            string sDummy = txtSoKH.Text;
            try
            {
                int iKeep = txtSoKH.SelectionStart - 1;
                for (int i = iKeep; i >= 0; i--)
                    if (txtSoKH.Text[i] == ',')
                        iKeep -= 1;

                sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                for (int i = 0; i <= iKeep; i++)
                    if (sDummy[i] == ',')
                        iKeep += 1;

                txtSoKH.Text = sDummy;
                txtSoKH.SelectionStart = iKeep + 1;
            }
            catch
            {
            }
        }

        private void txtSoKH_TextChanged(object sender, EventArgs e)
        {
            string sDummy = txtSoKH.Text;
            try
            {
                int iKeep = txtSoKH.SelectionStart - 1;
                for (int i = iKeep; i >= 0; i--)
                    if (txtSoKH.Text[i] == ',')
                        iKeep -= 1;

                sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                for (int i = 0; i <= iKeep; i++)
                    if (sDummy[i] == ',')
                        iKeep += 1;

                txtSoKH.Text = sDummy;
                txtSoKH.SelectionStart = iKeep + 1;
            }
            catch
            {
            }
        }

        private void frmNganhang_Load(object sender, EventArgs e)
        {
            layNH();
        }

        private void layNH()
        {
            DataTable dt = new DataTable();
            String sCommand = "", loaikh = "";
            sCommand = "SELECT * from nganhang";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngân hàng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Loại khách hàng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số khách hàng", typeof(string));
            dskh.Columns.Add(col);
            

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["nganhang"].ToString();
                    if (dt.Rows[i]["loaikh"].ToString() == "1")
                        loaikh = "Cá nhân";
                    else
                        loaikh = "Doanh nghiệp";
                    row[2] = loaikh;
                    row[3] = dt.Rows[i]["sokh"].ToString();                    
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;

            dgvDanhsach.Columns[0].FillWeight = 20;
            dgvDanhsach.Columns[0].Width = 20;
            dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;          
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                String sCommand;
                int loaikh;
                if (cbLoaiKH.Text == "Cá nhân")
                    loaikh = 1;
                else
                    loaikh = 2;
                sCommand = "INSERT INTO nganhang VALUES(N'" + cbMaNH.Text + "',"+loaikh+"," + Convert.ToInt16(txtSoKH.Text.Replace(",","")) + ")";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                cbMaNH.Text = "";                    
                cbLoaiKH.Text = "";
                txtSoKH.Text = "";
                layNH();
                MessageBox.Show("Đã thêm");
                //btnAdd.Enabled = false;
                //btnDel.Enabled = false;
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                MessageBox.Show("Ngân hàng này đã có!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            try
            {
                String sCommand;
                int loaikh;
                if (cbLoaiKH.Text == "Cá nhân")
                    loaikh = 1;
                else
                    loaikh = 2;
                sCommand = "UPDATE nganhang SET sokh=" + Convert.ToInt16(txtSoKH.Text.Replace(",","")) + " where nganhang=N'" + cbMaNH.Text + "' and loaikh="+loaikh+"";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                txtSoKH.Text = "";
                cbMaNH.Text = "";
                cbLoaiKH.Text = "";
                layNH();
                MessageBox.Show("Đã thay đổi!");
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                MessageBox.Show("Có lỗi xảy ra!");
            }
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            Cursor.Current = Cursors.Default;
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbMaNH.Text = dgvDanhsach.CurrentRow.Cells[1].Value.ToString();
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            btnModify.Enabled = true;
            try
            {
                cbLoaiKH.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[2].Value.ToString();
                txtSoKH.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[3].Value.ToString();                

            }
            catch { }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            int loaikh;
            if (dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[2].Value.ToString() == "Cá nhân")
                loaikh = 1;
            else
                loaikh = 2;
            try
            {
                sCommand = "Delete nganhang where nganhang =N'" + dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[1].Value.ToString() + "' and loaikh="+loaikh+"";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();

                layNH();
                cbMaNH.Text = "";
                cbLoaiKH.Text = "";
                txtSoKH.Text = "";
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã xóa!");
            }
            catch { MessageBox.Show("Không xóa được!"); }
        }    

       
    }
}