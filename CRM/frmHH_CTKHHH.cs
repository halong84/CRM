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
    public partial class frmHH_CTKHHH : Form
    {
        string strCmd = "";

        public frmHH_CTKHHH()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 
        }

       
        private void laysodubinhquan()
        {
            DataTable dt = new DataTable();

            strCmd = "select top 1 * from sdbqct where makh='" + CRM.frmKhachhangHH.makh + "'  order by thang desc";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable sdbq = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("SDBQ", typeof(string));
            sdbq.Columns.Add(col);
            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            sdbq.Columns.Add(col);

            col = new DataColumn("Điểm", typeof(string));
            sdbq.Columns.Add(col);

            col = new DataColumn("Tỷ trọng(%)", typeof(string));
            sdbq.Columns.Add(col);

            col = new DataColumn("Thực điểm", typeof(string));
            sdbq.Columns.Add(col);
            

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = sdbq.NewRow();
                    
                    row[0] = dt.Rows[i]["SDBQ"].ToString();
                    row[1] = dt.Rows[i]["mact"].ToString();
                    row[2] = dt.Rows[i]["diem"].ToString();
                    row[3] = dt.Rows[i]["tytrong"].ToString();
                    row[4] = dt.Rows[i]["thucdiem"].ToString();                    
                    sdbq.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX1.DataSource = sdbq;
        }
        private void layspdv()
        {
            DataTable dt = new DataTable();

            strCmd = "select top 1 * from spdvct where makh='" + CRM.frmKhachhangHH.makh + "' order by thang desc";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable spdv = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("SPDV", typeof(string));
            spdv.Columns.Add(col);
            col = new DataColumn("Số lượng SPDV", typeof(string));
            spdv.Columns.Add(col);

            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            spdv.Columns.Add(col);

            col = new DataColumn("Điểm", typeof(string));
            spdv.Columns.Add(col);

            col = new DataColumn("Tỷ trọng(%)", typeof(string));
            spdv.Columns.Add(col);

            col = new DataColumn("Thực điểm", typeof(string));
            spdv.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = spdv.NewRow();

                    row[0] = dt.Rows[i]["SPDV"].ToString();
                    row[1] = dt.Rows[i]["SLSPDV"].ToString();
                    row[2] = dt.Rows[i]["mact"].ToString();
                    row[3] = dt.Rows[i]["diem"].ToString();
                    row[4] = dt.Rows[i]["tytrong"].ToString();
                    row[5] = dt.Rows[i]["thucdiem"].ToString();
                    spdv.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = spdv;
        }

        private void laythoigiangui()
        {
            DataTable dt = new DataTable();

            strCmd = "select * from tggtct where makh='" + CRM.frmKhachhangHH.makh + "' and thang=(select top 1  thang from tggtct order by thang desc)";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable tggt = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("Số tài khoản", typeof(string));
            tggt.Columns.Add(col);
            col = new DataColumn("Ngày mở", typeof(string));
            tggt.Columns.Add(col);
            col = new DataColumn("Ngày đến hạn", typeof(string));
            tggt.Columns.Add(col);
            col = new DataColumn("Kỳ hạn", typeof(string));
            tggt.Columns.Add(col);
            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            tggt.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(string));
            tggt.Columns.Add(col);

            col = new DataColumn("Tỷ trọng(%)", typeof(string));
            tggt.Columns.Add(col);

            col = new DataColumn("Thực điểm", typeof(string));
            tggt.Columns.Add(col);

            decimal tytrong = 1;
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = tggt.NewRow();

                    row[0] = dt.Rows[i]["Sotk"].ToString();
                    row[1] = dt.Rows[i]["ngaymo"].ToString().Substring(0,10);
                    row[2] = dt.Rows[i]["ngaydenhan"].ToString().Substring(0,10);
                    row[3] = dt.Rows[i]["thoigian"].ToString();
                    row[4] = dt.Rows[i]["mact"].ToString();
                    row[5] = dt.Rows[i]["diem"].ToString();
                    row[6] = dt.Rows[i]["tytrong"].ToString();
                    tytrong = Convert.ToDecimal(dt.Rows[i]["tytrong"].ToString());
                    row[7] = dt.Rows[i]["thucdiem"].ToString();
                    tggt.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX4.DataSource = tggt;
            //labelX6.Text = Convert.ToString(CRM.frmHH_XeploaiKy.diemtggt*tytrong/100) ;
        }
        private void layprofit()
        {
            DataTable dt = new DataTable();

            strCmd = "select top 1 * from profitct where makh='" + CRM.frmKhachhangHH.makh + "' order by thang desc";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable tsln = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("Tỷ suất lợi nhuận", typeof(string));
            tsln.Columns.Add(col);            

            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            tsln.Columns.Add(col);

            col = new DataColumn("Điểm", typeof(string));
            tsln.Columns.Add(col);

            col = new DataColumn("Tỷ trọng(%)", typeof(string));
            tsln.Columns.Add(col);

            col = new DataColumn("Thực điểm", typeof(string));
            tsln.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = tsln.NewRow();

                    row[0] = dt.Rows[i]["profit"].ToString();                   
                    row[1] = dt.Rows[i]["mact"].ToString();
                    row[2] = dt.Rows[i]["diem"].ToString();
                    row[3] = dt.Rows[i]["tytrong"].ToString();
                    row[4] = dt.Rows[i]["thucdiem"].ToString();
                    tsln.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX3.DataSource = tsln;
        }

        

        private void frmHH_CTKHHH_Load_1(object sender, EventArgs e)
        {
            laysodubinhquan();
            layspdv();            
            laythoigiangui();           
            layprofit();
            layxeploai();
            //labelX7.Text = CRM.frmHH_XeploaiKy.tdiem.ToString();
        }

        private void layxeploai()
        {
            DataTable dt = new DataTable();

            strCmd = "select top 1 * from diemkh where makh='" + CRM.frmKhachhangHH.makh + "'  order by convert(date,'01/'+thang) desc";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable xeploai= new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("Tháng", typeof(string));
            xeploai.Columns.Add(col);
            col = new DataColumn("Điểm_SDBQ", typeof(string));
            xeploai.Columns.Add(col);

            col = new DataColumn("Điểm_TGGT", typeof(string));
            xeploai.Columns.Add(col);

            col = new DataColumn("Điểm SPDV", typeof(string));
            xeploai.Columns.Add(col);

            col = new DataColumn("SPDV", typeof(string));
            xeploai.Columns.Add(col);

            col = new DataColumn("Điểm_Profit", typeof(string));
            xeploai.Columns.Add(col);

            col = new DataColumn("Tổng điểm", typeof(string));
            xeploai.Columns.Add(col);

            col = new DataColumn("Xếp loại", typeof(string));
            xeploai.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = xeploai.NewRow();

                    row[0] = dt.Rows[i]["thang"].ToString();
                    row[1] = dt.Rows[i]["diem_sdbq"].ToString();
                    row[2] = dt.Rows[i]["diem_tgg"].ToString();
                    row[3] = dt.Rows[i]["diem_spdv"].ToString();
                    row[4] = dt.Rows[i]["spdv"].ToString();
                    row[5] = dt.Rows[i]["diem_profit"].ToString();
                    row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                    row[7] = dt.Rows[i]["xeploai"].ToString();
                    xeploai.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX5.DataSource = xeploai;
        }
        
    }
}