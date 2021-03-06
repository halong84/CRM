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
    public partial class frmCSKH_ChiTiet : Form
    {
        public static string strmakh;
        public frmCSKH_ChiTiet()
        {
            InitializeComponent();
        }

        private void frmCSKH_ChiTiet_Load(object sender, EventArgs e)
        {
            strmakh = CRM.frmCSKH_TraCuuDiem.str_makh;
            layChitietDiem();
            layGiaiThuong();
        }
        private void layChitietDiem()
        {
            DataTable dt = new DataTable();
            decimal tong = 0;
            String strCmd = "select * from lichsudiem where makh='" + CRM.frmCSKH_TraCuuDiem.str_makh + "' order by convert(date,'01/'+thang)";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Tháng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Loại ngoại tệ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Đã phê duyệt", typeof(bool));
            dskh.Columns.Add(col);   

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();

                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["thang"].ToString();
                    row[2] = dt.Rows[i]["ccy"].ToString();
                    row[3] = dt.Rows[i]["diem"].ToString();
                    if (dt.Rows[i]["pheduyet"].ToString() == "True")
                    {
                        row[4] = true;
                        tong = tong + Convert.ToDecimal(row[3].ToString());
                    }
                    else
                        row[4] = false;
                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX11.DataSource = dskh;
            labelX2.Text = "Tổng điểm: " + Convert.ToString(tong);
        }
        private void layGiaiThuong()
        {
            DataTable dt = new DataTable();

            String strCmd = "select * from cauhinhthuong where macn='"+Thongtindangnhap.macn+"' and diem<=" + CRM.frmCSKH_TraCuuDiem.tongdiem + " order by diem";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Tên giải thưởng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(string));
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
                    row[1] = dt.Rows[i]["ten"].ToString();
                    row[2] = dt.Rows[i]["diengiai"].ToString();
                    row[3] = dt.Rows[i]["diem"].ToString();                    
                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX1.DataSource = dskh;
        }

        private void buttonX32_Click(object sender, EventArgs e)
        {
            int dem = 0;
            int i;
            decimal temp_diem = 0;            
            String tengiaithuong = "",diengiai="";
            SqlTransaction trans;
            for (i = 0; i < dataGridViewX1.RowCount; i++)
            {

                if (dataGridViewX1.Rows[i].Cells[4].Value.ToString() == "True")
                {
                    dem++;
                    temp_diem = Convert.ToInt64(dataGridViewX1.Rows[i].Cells[3].Value.ToString());                    
                    tengiaithuong = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                    diengiai = dataGridViewX1.Rows[i].Cells[2].Value.ToString();
                    
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
                
                String ngay;
                String sCommand ="";
                ngay = DateTime.Now.ToString().Substring(3, 2) + "/" + DateTime.Now.ToString().Substring(0, 2) + "/" + DateTime.Now.ToString().Substring(6, 4);
                sCommand = "INSERT INTO lichsunhanqua VALUES('" + CRM.frmCSKH_TraCuuDiem.str_makh + "','" + ngay + "',N'" + tengiaithuong + "'," + temp_diem + ")";                
                //frmMain.myCommand.Transaction = trans;
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn,trans);
                frmMain.myCommand.ExecuteNonQuery(); 
             
                sCommand = "UPDATE diem_cn SET diem =" + (frmCSKH_TraCuuDiem.tongdiem - temp_diem) + " where makh='" + CRM.frmCSKH_TraCuuDiem.str_makh + "'";                
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn,trans);
                frmMain.myCommand.ExecuteNonQuery();                
                frmCSKH_TraCuuDiem.tongdiem = frmCSKH_TraCuuDiem.tongdiem - temp_diem;
                frmMain.myCommand.Transaction.Commit();
            }
            catch
            {
                frmMain.myCommand.Transaction.Rollback();
                MessageBox.Show("Lỗi dữ liệu, kiểm tra lại ngày hệ thống!");
            }
            DataAccess.conn.Close();
            MessageBox.Show("Đã lưu dữ liệu");
            layGiaiThuong();
            

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 39;
            strmakh = CRM.frmCSKH_TraCuuDiem.str_makh; 
            @In form_in = new @In();
            form_in.Show();

        }
    }
}