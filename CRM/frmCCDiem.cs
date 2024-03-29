using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmCCDiem : Form
    {
        public frmCCDiem()
        {
            InitializeComponent();
        }

        private void txtSotien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSotien_KeyDown(object sender, KeyEventArgs e)
        {
            string sDummy = txtSotien.Text;
            try
            {
                int iKeep = txtSotien.SelectionStart-1 ;
                for (int i = iKeep; i >= 0; i--)
                    if (txtSotien.Text[i] == ',')
                        iKeep -= 1;

                sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                for (int i = 0; i <= iKeep; i++)
                    if (sDummy[i] == ',')
                        iKeep += 1;

                txtSotien.Text = sDummy;
                txtSotien.SelectionStart = iKeep+1;
            }
            catch
            {
            }
        }

        private void txtSotien_Leave(object sender, EventArgs e)
        {
            string sDummy = txtSotien.Text;
            try
            {
                int iKeep = txtSotien.SelectionStart -1;
                for (int i = iKeep; i >= 0; i--)
                    if (txtSotien.Text[i] == ',')
                        iKeep -= 1;

                sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                for (int i = 0; i <= iKeep; i++)
                    if (sDummy[i] == ',')
                        iKeep += 1;

                txtSotien.Text = sDummy;
                txtSotien.SelectionStart = iKeep + 1;
            }
            catch
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                String sCommand,ngayhieuluc;
                decimal diem = 0, tien = 0;
                diem = Convert.ToDecimal(txtSoDiem.Text.Replace(",", ""));
                tien = Convert.ToDecimal(txtSotien.Text.Replace(",", ""));
                ngayhieuluc = dtpNgayHL.Text.Substring(3, 2) + "/" + dtpNgayHL.Text.Substring(0, 2) + "/" + dtpNgayHL.Text.Substring(6, 4);
                sCommand = "INSERT INTO CAUHINHDIEM VALUES('"+ngayhieuluc+"','" + cbLoaitien.Text + "'," + tien + "," + diem + ")";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                cbLoaitien.Text = "";
                txtSotien.Text = "0";
                txtSoDiem.Text = "0";
                dtpNgayHL.Text = "";
                layCCDiem();
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
        private void layCCDiem()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            sCommand = "SELECT * from CauHinhDiem";
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
            col = new DataColumn("Loại tiền", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số tiền", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Số điểm", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày hiệu lực", typeof(string));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["Loaitien"].ToString();
                    row[2] = dt.Rows[i]["Sotien"].ToString();
                    row[3] = dt.Rows[i]["Sodiem"].ToString();
                    //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                    row[4] = dt.Rows[i]["ThoiGian"].ToString().Substring(0,10);
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;
            dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void frmCCDiem_Load(object sender, EventArgs e)
        {
            layCCDiem();
        }

        private void txtSotien_TextChanged(object sender, EventArgs e)
        {
            if (txtSotien.Text != "")
            {
                string sDummy = txtSotien.Text;
                try
                {
                    int iKeep = txtSotien.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                        if (txtSotien.Text[i] == ',')
                            iKeep -= 1;

                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                        if (sDummy[i] == ',')
                            iKeep += 1;

                    txtSotien.Text = sDummy;
                    txtSotien.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbLoaitien.Text = dgvDanhsach.CurrentRow.Cells[1].Value.ToString();
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            btnModify.Enabled = true;
            try
            {
                txtSotien.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[2].Value.ToString();
                txtSoDiem.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[3].Value.ToString();
                dtpNgayHL.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[4].Value.ToString();

            }
            catch { }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {               
                if (DateTime.Now < dtpNgayHL.Value)
                {
                    String sCommand, ngayhieuluc;
                    decimal diem = 0, tien = 0;
                    diem = Convert.ToDecimal(txtSoDiem.Text.Replace(",", ""));
                    tien = Convert.ToDecimal(txtSotien.Text.Replace(",", ""));
                    ngayhieuluc = dtpNgayHL.Text.Substring(3, 2) + "/" + dtpNgayHL.Text.Substring(0, 2) + "/" + dtpNgayHL.Text.Substring(6, 4);
                    sCommand = "UPDATE cauhinhdiem SET sotien=" + Convert.ToDecimal(txtSotien.Text.Replace(",", "")) + ",sodiem=" + Convert.ToDecimal(txtSoDiem.Text.Replace(",", "")) + " where Loaitien='" + cbLoaitien.Text + "' and Thoigian='" + ngayhieuluc + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    txtSotien.Text = "0";
                    cbLoaitien.Text = "";
                    txtSoDiem.Text = "0";
                    dtpNgayHL.Text = "";
                    layCCDiem();
                    MessageBox.Show("Đã thay đổi!");
                }
                else
                {
                    MessageBox.Show("Cơ cấu điểm này đã được áp dụng!");
                }

            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                MessageBox.Show("Có lỗi xảy ra!");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now < dtpNgayHL.Value)
                {
                    String sCommand, ngayhieuluc;                    
                    ngayhieuluc = dtpNgayHL.Text.Substring(3, 2) + "/" + dtpNgayHL.Text.Substring(0, 2) + "/" + dtpNgayHL.Text.Substring(6, 4);
                    sCommand = "delete cauhinhdiem where Loaitien='" + cbLoaitien.Text + "' and Thoigian='" + ngayhieuluc + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    txtSotien.Text = "0";
                    cbLoaitien.Text = "";
                    txtSoDiem.Text = "0";
                    dtpNgayHL.Text = "";
                    layCCDiem();
                    MessageBox.Show("Đã xóa!");
                }
                else
                {
                    MessageBox.Show("Cơ cấu điểm này đã được áp dụng!");
                }

            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                MessageBox.Show("Có lỗi xảy ra!");
            }
        }


      
       

       

       
    }
}