using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CRM
{
    public partial class frmDanso : Form
    {
        public frmDanso()
        {
            InitializeComponent();
        }

        private void frmDanSo_Load(object sender, EventArgs e)
        {
            layHuyen();
            layXa();
            layDS();           
            cbHuyen.Text = "";
            cbXa.Text = "";
            txtSoKH.Text = "";
        }
        private void layHuyen()
        {
            String sCommand = "";
            DataTable dt = new DataTable();            
            sCommand = "SELECT mahuyen,tenhuyen from dmhuyen where left(mahuyen,3)='470' or mahuyen='9999' order by mahuyen ";
         
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbHuyen.DataSource = dt;
            cbHuyen.DisplayMember = "tenhuyen";
            cbHuyen.ValueMember = "mahuyen";
            //cbHuyen.SelectedValue = "47001";
        }

        private void layXa()
        {
            String sCommand = "";
            DataTable dt = new DataTable();                               
            sCommand = "SELECT maxa,tenxa from dmxaphuong where left(maxa,5)='" + cbHuyen.SelectedValue.ToString() + "' or maxa='9999' order by maxa ";                
           
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbXa.DataSource = dt;
            cbXa.DisplayMember = "tenxa";
            cbXa.ValueMember = "maxa";
            //cbXa.SelectedValue = "4700101";
        }

        private void cbHuyen_TextChanged(object sender, EventArgs e)
        {
            layXa();
        }

        private void txtSoKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
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
        private void layDS()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            sCommand = "SELECT * from Danso";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Phường xã", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số dân", typeof(string));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    DataTable dt1 = new DataTable();
                    sCommand = "SELECT * from DMHuyen where MaHuyen='"+cbHuyen.SelectedValue.ToString()+"'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                    frmMain.conn.Close();
                    row[1] = dt1.Rows[i]["TenHuyen"].ToString();
                    dt1.Clear();
                    sCommand = "SELECT * from DMXaPhuong where MaXa='" + cbXa.SelectedValue.ToString() + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                    row[2] = dt1.Rows[i]["TenXa"].ToString();
                    dt1.Clear();
                    row[3] = dt.Rows[i]["Danso"].ToString();
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
                
                sCommand = "INSERT INTO Danso VALUES('" + cbHuyen.SelectedValue.ToString() + "','" + cbXa.SelectedValue.ToString() + "'," + Convert.ToInt16(txtSoKH.Text.Replace(",", "")) + ")";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                cbHuyen.Text = "";
                cbXa.Text = "";
                txtSoKH.Text = "";
                layDS();
                MessageBox.Show("Đã thêm");
                //btnAdd.Enabled = false;
                //btnDel.Enabled = false;
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                MessageBox.Show("Xã này đã có!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            try
            {
                String sCommand;                
                sCommand = "UPDATE Danso SET danso=" + Convert.ToInt16(txtSoKH.Text.Replace(",", "")) + " where MaHuyen='" + cbHuyen.SelectedValue.ToString() + "' and Maxa='" + cbXa.SelectedValue.ToString() + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtSoKH.Text = "";
                cbHuyen.Text = "";
                cbXa.Text = "";
                layDS();
                MessageBox.Show("Đã thay đổi!");
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                MessageBox.Show("Có lỗi xảy ra!");
            }
           
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            
            try
            {
                sCommand = "Delete danso where mahuyen ='" + cbHuyen.SelectedValue.ToString() + "' and maxa='" + cbXa.SelectedValue.ToString() + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();

                layDS();
                cbHuyen.Text = "";
                cbXa.Text = "";
                txtSoKH.Text = "";
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã xóa!");
            }
            catch { MessageBox.Show("Không xóa được!"); }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            btnModify.Enabled = true;
            try
            {
                cbHuyen.Text = dgvDanhsach.CurrentRow.Cells[1].Value.ToString();
                cbXa.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[2].Value.ToString();
                txtSoKH.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[3].Value.ToString();

            }
            catch { }
        }

    }
}