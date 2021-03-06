using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
namespace CRM
{
    public partial class frmHH : Form
    {
        SqlConnection myConnection;
        SqlCommand myCommand;
        string line = "",sCommand="";
        public frmHH()
        {
            InitializeComponent();
        }

        private void CTDiemKHT_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader("database.txt");


                line = reader.ReadLine();
                reader.Close();
            }
            catch
            {
                MessageBox.Show("không đọc được file database.txt");
                this.Close();
            }

            try
            {
                myConnection = new SqlConnection("user id=sa;" +
                                          "password=qaz123;server=" + line + ";" +
                                          "Trusted_Connection=no;" +
                                          "database=CRM; " +
                                          "connection timeout=30");

                myConnection.Open();
            }
            catch
            {
                MessageBox.Show("Không kết nối được máy chủ");
                this.Close();
            }
            laysodubinhquan();
            layspdv();
            if (CRM.frmHH_XeploaiThang.loaikh == 1)
                laythoigiangui();
            else
                layprofit();
            labelX7.Text = CRM.frmHH_XeploaiThang.tdiem.ToString();
        }
        private void laysodubinhquan()
        {
            DataTable dt = new DataTable();

            sCommand = "select * from sdbqct where makh='"+CRM.frmHH_XeploaiThang.makh+"' and thang='" + CRM.frmHH_XeploaiThang.thang.ToString()+ "'";
            new SqlDataAdapter(sCommand, myConnection).Fill(dt);
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

            sCommand = "select * from spdvct where makh='" + CRM.frmHH_XeploaiThang.makh + "' and thang='" + CRM.frmHH_XeploaiThang.thang.ToString() + "'";
            new SqlDataAdapter(sCommand, myConnection).Fill(dt);
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

            sCommand = "select * from tggtct where makh='" + CRM.frmHH_XeploaiThang.makh + "' and thang='" + CRM.frmHH_XeploaiThang.thang + "'";
            new SqlDataAdapter(sCommand, myConnection).Fill(dt);
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
            labelX6.Text = Convert.ToString(Convert.ToInt32(CRM.frmHH_XeploaiThang.diemtggt*tytrong/100)) ;
        }
        private void layprofit()
        {
            DataTable dt = new DataTable();

            sCommand = "select * from profitct where makh='" + CRM.frmHH_XeploaiThang.makh + "' and thang='" + CRM.frmHH_XeploaiThang.thang + "'";
            new SqlDataAdapter(sCommand, myConnection).Fill(dt);
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

       
    }
}