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
    
    public partial class frmCSKH_TKQuaTang : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string strmakh, tungay, denngay;
        public frmCSKH_TKQuaTang()
        {
            InitializeComponent();
        }

        private void frmCSKH_TKQuaTang_Load(object sender, EventArgs e)
        {
            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Tất cả";
            dt.Rows.InsertAt(first_row, 0);

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "MACN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;

            if (Thongtindangnhap.macn != Thongtindangnhap.ma_hoi_so)
            {
                cbCN.Enabled = false;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            layDSQT();
            if (txtMakh.Text == "")
                btnIn.Enabled = false;
            else
                btnIn.Enabled = true;
            
        }
        private void layDSQT()
        {
            DataTable dt = new DataTable();
            String fromDate, toDate;
            fromDate = dtpFrom.Text.Substring(3, 2) + "/" + dtpFrom.Text.Substring(0, 2) + "/" + dtpFrom.Text.Substring(6, 4);
            toDate = dtpTo.Text.Substring(3, 2) + "/" + dtpTo.Text.Substring(0, 2) + "/" + dtpTo.Text.Substring(6, 4);
            String strCmd = "";
            if((cbCN.Text=="")||(cbCN.Text=="Tất cả"))
            {
                txtMakh.Enabled=false;
                strCmd = "select khachhang.hoten,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.cmnd,khachhang.diachi1,lichsunhanqua.* from lichsunhanqua,khachhang where khachhang.makh=lichsunhanqua.makh and ngay>='" + fromDate + "' and ngay<='" + toDate + "'";
            }
            else
            {
                if (txtMakh.Text == "")
                    strCmd = "select khachhang.hoten,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.cmnd,khachhang.diachi1,lichsunhanqua.* from lichsunhanqua,khachhang where khachhang.makh=lichsunhanqua.makh and left(lichsunhanqua.makh,4)='" + cbCN.Text + "' and ngay>='" + fromDate + "' and ngay<='" + toDate + "'";
                else
                    strCmd = "select khachhang.hoten,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.cmnd,khachhang.diachi1,lichsunhanqua.* from lichsunhanqua,khachhang where khachhang.makh=lichsunhanqua.makh and khachhang.makh='" + txtMakh.Text + "' and ngay>='" + fromDate + "' and ngay<='" + toDate + "'";
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            dt.Clear();
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã khách hàng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();

                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["MAKH"].ToString();
                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["diachi1"].ToString();
                    row[4] = dt.Rows[i]["dienthoai1"].ToString();
                    row[5] = dt.Rows[i]["cmnd"].ToString();
                    row[6] = dt.Rows[i]["NGAY"].ToString().Substring(0, 10);
                    row[7] = dt.Rows[i]["NoiDung"].ToString();
                    row[8] = dt.Rows[i]["Diem"].ToString();

                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX1.DataSource = dskh;
        }

        private void buttonX159_Click(object sender, EventArgs e)
        {
            String temp = "";
            temp = "KhachHangQuaTang.xls";

            saveFileDialog1.FileName = temp.Replace("/", "-");
            saveFileDialog1.Filter = " Excel (*.xls)|*.xls|Tất cả (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            string path = "";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                path = saveFileDialog1.FileName;
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 30;
                for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridViewX1.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        ExcelApp.Cells[i + 1, j + 1] = row.Cells[j].Value.ToString();
                    }
                }

                ExcelApp.ActiveWorkbook.SaveCopyAs(path);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Đã Lưu");
            }
            Cursor.Current = Cursors.Default;
        }

        private void txtMakh_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void labelX11_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            int dem = 0;
            int i;
            decimal temp_diem = 0;
            String makh = "",ngay="";
            SqlTransaction trans;
            for (i = 0; i < dataGridViewX1.RowCount; i++)
            {

                if (dataGridViewX1.Rows[i].Cells[9].Value.ToString() == "True")
                {
                    dem++;
                    temp_diem = Convert.ToInt64(dataGridViewX1.Rows[i].Cells[8].Value.ToString());
                    makh = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                    ngay = dataGridViewX1.Rows[i].Cells[6].Value.ToString().Substring(3, 2) + "/" + dataGridViewX1.Rows[i].Cells[6].Value.ToString().Substring(0, 2) + "/" + dataGridViewX1.Rows[i].Cells[6].Value.ToString().Substring(6,4);
                }
            }
            if (dem > 1)
            {
                MessageBox.Show("Chỉ chọn 1 sản phẩm !");
                return;
            }
            if (dem == 0)
            {
                MessageBox.Show("Chưa chọn sản phẩm !");
                return;
            }
            //Cap nhat nhan qua vao file lichsunhanqua
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            trans = DataAccess.conn.BeginTransaction();
            try
            {

                
                String sCommand = "";                
                sCommand = "delete lichsunhanqua where makh='"+makh+"' and diem ="+temp_diem+" and ngay= '"+ngay+"'";
                //frmMain.myCommand.Transaction = trans;
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn, trans);
                frmMain.myCommand.ExecuteNonQuery();

                sCommand = "UPDATE diem_cn SET diem =diem+ "+temp_diem+" where makh='" + makh + "'";
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn, trans);
                frmMain.myCommand.ExecuteNonQuery();                
                frmMain.myCommand.Transaction.Commit();
            }
            catch
            {
                frmMain.myCommand.Transaction.Rollback();
                MessageBox.Show("Lỗi dữ liệu, kiểm tra lại ngày hệ thống!");
            }
            DataAccess.conn.Close();
            MessageBox.Show("Đã reset!");
            layDSQT();

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (txtMakh.Text == "")
                btnIn.Enabled = false;
            else
            {
                btnIn.Enabled = true;
                strmakh = txtMakh.Text;
                tungay = dtpFrom.Text.Substring(3,2)+"/"+dtpFrom.Text.Substring(0,2)+"/"+dtpFrom.Text.Substring(6,4);
                denngay = dtpTo.Text.Substring(3, 2) + "/" + dtpTo.Text.Substring(0, 2) + "/" + dtpTo.Text.Substring(6, 4);
                CRM.frmMain.manhinhin = 40;                
                @In form_in = new @In();
                form_in.Show();
            }
        }
    }
}