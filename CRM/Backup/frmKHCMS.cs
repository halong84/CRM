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
    public partial class frmKHCMS : Form
    {
        public frmKHCMS()
        {
            InitializeComponent();
        }

        private void frmKHCMS_Load(object sender, EventArgs e)
        {
            layKHCMS();
        }
        private void layKHCMS()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            sCommand = "SELECT khachhang.hoten,CMSPOS.* from CMSPOS,khachhang where khachhang.makh=CMSPOS.makh and left(khachhang.makh,4)='" + frmMain.cn + "'";
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
            col = new DataColumn("Mã khách hàng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Dịch vụ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày đăng ký", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["makh"].ToString();
                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["dichvu"].ToString();
                    string ngaydk = dt.Rows[i]["ngaydk"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["ngaydk"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["ngaydk"].ToString().Substring(6, 4);
                    row[4] = ngaydk;
                    //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                    if (dt.Rows[i]["hientrang"].ToString()=="True")
                        row[5] = "Active";
                    else
                        row[5]="Stop";
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width =30;
            dgvDanhsach.Columns[1].Width =50;
            dgvDanhsach.Columns[2].Width =300;
            dgvDanhsach.Columns[3].Width = 50;
            dgvDanhsach.Columns[4].Width = 100;
            dgvDanhsach.Columns[5].Width = 50;

            dgvDanhsach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                String sCommand, ngayhieuluc;
                int tinhtrang;
                if (cbTinhTrang.Text == "Active")
                    tinhtrang = 1;
                else
                    tinhtrang = 0;                      
                ngayhieuluc = dtpNgayHL.Text.Substring(3, 2) + "/" + dtpNgayHL.Text.Substring(0, 2) + "/" + dtpNgayHL.Text.Substring(6, 4);
                sCommand = "INSERT INTO CMSPOS VALUES('"+txtMaKH.Text+"','" + cbDichVu.Text + "','" + ngayhieuluc + "'," + tinhtrang + ")";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                cbDichVu.Text = "";
                txtMaKH.Text = "";
                cbTinhTrang.Text = "";
                dtpNgayHL.Text = "";
                layKHCMS();
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
                MessageBox.Show("Khách hàng này đã có!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
               
                String sCommand, ngayhieuluc;
                int tinhtrang;
                if (cbTinhTrang.Text == "Active")
                    tinhtrang = 1;
                else
                    tinhtrang = 0;
                ngayhieuluc = dtpNgayHL.Text.Substring(3, 2) + "/" + dtpNgayHL.Text.Substring(0, 2) + "/" + dtpNgayHL.Text.Substring(6, 4);
                sCommand = "UPDATE CMSPOS SET hientrang=" + tinhtrang + ",ngaydk='" + ngayhieuluc + "' where makh='" + txtMaKH.Text + "' and Dichvu='" + cbDichVu.Text + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtMaKH.Text = "";
                cbDichVu.Text = "";
                cbTinhTrang.Text = "";
                dtpNgayHL.Text = "";
                layKHCMS();
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
            try
            {
                String sCommand;
                
                sCommand = "delete CMSPOS where makh='" + txtMaKH.Text + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtMaKH.Text = "";
                cbDichVu.Text = "";
                cbTinhTrang.Text= "";
                dtpNgayHL.Text = "";
                layKHCMS();
                MessageBox.Show("Đã xóa!");
               

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

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbTinhTrang.Text = dgvDanhsach.CurrentRow.Cells[5].Value.ToString();
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            btnModify.Enabled = true;
            try
            {
                txtMaKH.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[1].Value.ToString();
                cbDichVu.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[3].Value.ToString();
                dtpNgayHL.Text = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[4].Value.ToString();

            }
            catch { }
        }


    }
}