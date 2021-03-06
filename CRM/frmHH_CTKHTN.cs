using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using N_MicrosoftExcelClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmHH_CTKHTN : Form
    {
        //string strCmd = "";

        public frmHH_CTKHTN()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 
        }      

        private void layGiaoDich()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            sCommand = "SELECT giaodich.*,khachhangtiemnang.hoten,loaigiaodich.tenloai from khachhangtiemnang,giaodich,loaigiaodich where giaodich.maloaigd=loaigiaodich.maloaigd and giaodich.makh=khachhangtiemnang.makh and khachhangtiemnang.makh='" + CRM.frmKhachhangTN.makh + "'";
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
            col = new DataColumn("Loại giao dịch", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa điểm", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Bắt đầu", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Kết thúc", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Đánh giá", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Chi phí", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Hình thức GD", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Độ ưu tiên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã giao dịch", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    DataTable dt1 = new DataTable();
                    sCommand = "select * from loaigiaodich where maloaigd='" + dt.Rows[i]["maloaigd"].ToString() + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                    DataAccess.conn.Close();
                    row[1] = dt1.Rows[0]["tenloai"].ToString();
                    row[2] = dt.Rows[i]["noidung"].ToString();
                    row[3] = dt.Rows[i]["diadiem"].ToString();
                    row[4] = dt.Rows[i]["tgbatdau"].ToString();
                    row[5] = dt.Rows[i]["tgketthuc"].ToString();
                    row[6] = dt.Rows[i]["danhgia"].ToString();
                    row[7] = dt.Rows[i]["Chiphi"].ToString();
                    row[8] = dt.Rows[i]["Ghichu"].ToString();
                    row[9] = dt.Rows[i]["hinhthucGD"].ToString();
                    row[10] = dt.Rows[i]["Douutien"].ToString();
                    row[11] = dt.Rows[i]["magd"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX1.DataSource = dskh;

            dataGridViewX1.Columns[0].FillWeight = 20;
            dataGridViewX1.Columns[0].Width = 40;
            dataGridViewX1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[11].Visible = false;
            dataGridViewX1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }       

        private void frmHH_CTKHTN_Load(object sender, EventArgs e)
        {
            layGiaoDich();
            //laykhhh();
        }        
    }
}