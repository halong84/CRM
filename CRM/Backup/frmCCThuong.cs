using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRM
{
    public partial class frmCCThuong : Form
    {
        public frmCCThuong()
        {
            InitializeComponent();
        }

        private void txtSoDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoDiem_TextChanged(object sender, EventArgs e)
        {
            if (txtSoDiem.Text != "")
            {
                string sDummy = txtSoDiem.Text;
                try
                {
                    int iKeep = txtSoDiem.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                        if (txtSoDiem.Text[i] == ',')
                            iKeep -= 1;

                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                        if (sDummy[i] == ',')
                            iKeep += 1;

                    txtSoDiem.Text = sDummy;
                    txtSoDiem.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtSoDiem_KeyDown(object sender, KeyEventArgs e)
        {
            string sDummy = txtSoDiem.Text;
            try
            {
                int iKeep = txtSoDiem.SelectionStart - 1;
                for (int i = iKeep; i >= 0; i--)
                    if (txtSoDiem.Text[i] == ',')
                        iKeep -= 1;

                sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                for (int i = 0; i <= iKeep; i++)
                    if (sDummy[i] == ',')
                        iKeep += 1;

                txtSoDiem.Text = sDummy;
                txtSoDiem.SelectionStart = iKeep + 1;
            }
            catch
            {
            }
        }

        private void txtSoDiem_Leave(object sender, EventArgs e)
        {
            string sDummy = txtSoDiem.Text;
            try
            {
                int iKeep = txtSoDiem.SelectionStart - 1;
                for (int i = iKeep; i >= 0; i--)
                    if (txtSoDiem.Text[i] == ',')
                        iKeep -= 1;

                sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                for (int i = 0; i <= iKeep; i++)
                    if (sDummy[i] == ',')
                        iKeep += 1;

                txtSoDiem.Text = sDummy;
                txtSoDiem.SelectionStart = iKeep + 1;
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
                String sCommand,manhom;
                decimal diem = 0;
                diem = Convert.ToDecimal(txtSoDiem.Text.Replace(",", ""));
                manhom = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString(); 
                sCommand = "INSERT INTO CAUHINHTHUONG VALUES('" + manhom + "','"+cbCN.Text+"',N'" + txtTen.Text + "',N'" + txtDG.Text + "'," + diem + ")";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtTen.Text = "";
                txtDG.Text = "";
                txtSoDiem.Text = "0";                
                layCCThuong();
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
                MessageBox.Show("Cơ cấu này đã có!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            btnModify.Enabled = true;
            try
            {
                txtMa.Text = dgvDanhsach.CurrentRow.Cells[1].Value.ToString();
                txtTen.Text = dgvDanhsach.CurrentRow.Cells[2].Value.ToString();
                txtDG.Text = dgvDanhsach.CurrentRow.Cells[3].Value.ToString();
                txtSoDiem.Text = dgvDanhsach.CurrentRow.Cells[4].Value.ToString();            

            }
            catch { }
        }

        private void frmCCThuong_Load(object sender, EventArgs e)
        {
            if (CRM.frmDangnhap.macn != "4800")
            {
                cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
            else
                cbCN.Text = "4800";

            layCCThuong();
        }
        private void layCCThuong()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            if ((cbCN.Text == "Tất cả") || (cbCN.Text == ""))
                sCommand = "SELECT * from CauHinhThuong";
            else
                sCommand = "SELECT * from CauHinhThuong where macn='"+cbCN.Text+"'";
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
            col = new DataColumn("Mã", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Tên giải thưởng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số điểm", typeof(decimal));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["ma"].ToString();
                    row[2] = dt.Rows[i]["ten"].ToString();
                    row[3] = dt.Rows[i]["diengiai"].ToString();
                    //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                    row[4] = dt.Rows[i]["diem"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;
            dgvDanhsach.Columns[1].Visible = false;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                
                String sCommand;
                decimal diem = 0;
                diem = Convert.ToDecimal(txtSoDiem.Text.Replace(",", ""));                                
                sCommand = "UPDATE cauhinhthuong SET diem=" + Convert.ToDecimal(txtSoDiem.Text.Replace(",", "")) + ",ten=N'"+txtTen.Text+"',Diengiai=N'"+txtDG.Text+"',macn='"+cbCN.Text+"' where ma='" + txtMa.Text + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtTen.Text = "";                
                txtDG.Text = "";
                txtSoDiem.Text = "0";
                layCCThuong();
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
            try
            {

                String sCommand;
                decimal diem = 0;
                diem = Convert.ToDecimal(txtSoDiem.Text.Replace(",", ""));
                sCommand = "Delete cauhinhthuong where ma='" + txtMa.Text + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtTen.Text = "";
                txtDG.Text = "";
                txtSoDiem.Text = "0";
                layCCThuong();
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

        private void cbCN_TextChanged(object sender, EventArgs e)
        {
            layCCThuong();
        }

    }
}