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
using System.Windows.Forms.DataVisualization.Charting;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmTK_CLSPDV : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string tuthang = "", denthang = "", mahm = "", chitietHM = "";
        String sCommand = "";

        public frmTK_CLSPDV()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            dataGridViewX2.RowHeadersVisible = false;
            dataGridViewX2.AllowUserToAddRows = false;
            dataGridViewX2.ReadOnly = true;
            dataGridViewX2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewX2.MultiSelect = false;

            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Tất cả";
            dt.Rows.InsertAt(first_row, 0);

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "MACN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;
        }

        private void TK_CLSPDV_Load(object sender, EventArgs e)
        {
            tuthang = "01/01/1990";
            denthang = "12/31/9998";
            //tuthang = txtTuThang.Text = DateTime.Now.ToString().Substring(3, 7);
            //denthang = txtDenThang.Text = DateTime.Now.ToString().Substring(3, 7);
            DataTable dt = new DataTable();
            //Tinh toan ket qua tham do chi tieu su dung san pham dich vu
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                if (optCN.Checked == true)
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDO.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdotc.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
            }
            else
            {
                if (optCN.Checked == true)
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDO.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and  khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdotc.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //Luoi du lieu
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Hoàn toàn phản đối", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Nói chung là phản đối", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Không ý kiến", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Đồng ý", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Hoàn toàn đồng ý", typeof(string));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["mact"].ToString();
                    row[2] = dt.Rows[i]["chitiethanmuc"].ToString();
                    row[3] = dt.Rows[i]["htpd"].ToString();
                    row[4] = dt.Rows[i]["ncpd"].ToString();
                    row[5] = dt.Rows[i]["kyk"].ToString();
                    row[6] = dt.Rows[i]["dy"].ToString();
                    row[7] = dt.Rows[i]["htdy"].ToString();
                    //row[3] = Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[i]["luachon"].ToString()) * 100 / soluot, 2)) + "%";
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX2.Columns[0].Width = 50;
            dataGridViewX2.Columns[1].Width = 50;
            dataGridViewX2.Columns[2].Width = 300;
            ////Bieu do            
            //Chart1.Series.Clear();
            ////int point = 0;
            //for (int pointIndex = 0; pointIndex < dt.Rows.Count; pointIndex++)
            //{
            //    double sum = 0;
            //    for (int j = 1; j < dt.Columns.Count; j++)
            //        sum = sum + Convert.ToDouble(dt.Rows[pointIndex][j].ToString());
            //    for (int i = 1; i < dt.Columns.Count; i++)
            //    {
            //        if(pointIndex==0)
            //        Chart1.Series.Add("Series" + i);                    
            //        Chart1.Series["Series" + i].Points.AddXY(dt.Rows[pointIndex][0],dt.Rows[pointIndex][i]);
            //        //Chart1.Series["Series" +i].Label = "#VALY";
            //        if (Convert.ToDouble(dt.Rows[pointIndex][i].ToString()) == 0)
            //            Chart1.Series["Series" + i].Points[pointIndex].IsValueShownAsLabel = false;
            //        else
            //        {
            //            Chart1.Series["Series" + i].Points[pointIndex].Label = Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[pointIndex][i].ToString()) / sum * 100, 0)) + "%";
            //            Chart1.Series["Series" + i].IsValueShownAsLabel = true;
            //        }
            //        Chart1.Series["Series" + i].IsValueShownAsLabel = true;
            //        Chart1.Series["Series" + i].LegendText = dataGridViewX2.Columns[i+1].Name.ToString() ;
            //        Chart1.Series["Series" + i].ChartType = SeriesChartType.StackedBar100;// Set the Pie width
            //        Chart1.Series["Series" + i]["PointWidth"] = "0.5";// Show data points labels
            //        // Set data points label style            
            //        Chart1.Series["Series" + i]["BarLabelStyle"] = "Center";// Show chart as 3D
            //        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;// Draw chart as 3D
            //        Chart1.Series["Series" + i]["DrawingStyle"] = "Cylinder";
            //        Chart1.Series["Series" + i].Font = new Font("Arial", 9, FontStyle.Bold);
            //       // point = point + 1;
            //    }

            //  }        



        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            //tuthang = txtTuThang.Text.Substring(0, 2) + "/01/" + txtTuThang.Text.Substring(3, 4);
            //if (txtDenThang.Text.Substring(0, 2) == "02")
            //    denthang = txtDenThang.Text.Substring(0, 2) + "/28/" + txtDenThang.Text.Substring(3, 4);
            //else
            //    denthang = txtDenThang.Text.Substring(0, 2) + "/30/" + txtDenThang.Text.Substring(3, 4);
            tuthang = dtpFrom.Text.Substring(3, 2) + "/" + dtpFrom.Text.Substring(0, 2) + "/" + dtpFrom.Text.Substring(6, 4);
            denthang = dtpTo.Text.Substring(3, 2) + "/" + dtpTo.Text.Substring(0, 2) + "/" + dtpTo.Text.Substring(6, 4);
            DataTable dt = new DataTable();
            //Tinh toan ket qua tham do chi tieu su dung san pham dich vu
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                if (optCN.Checked == true)
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDO.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdotc.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
            }
            else
            {
                if (optCN.Checked == true)
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDO.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and  khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [HTPD],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [NCPD],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [KYK],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [DY],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [HTDY]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdotc.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='B' order by KETQUATHAMDOCT.MACT";
                }
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //Luoi du lieu
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Hoàn toàn phản đối", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Nói chung là phản đối", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Không ý kiến", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Đồng ý", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Hoàn toàn đồng ý", typeof(string));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["mact"].ToString();
                    row[2] = dt.Rows[i]["chitiethanmuc"].ToString();
                    row[3] = dt.Rows[i]["htpd"].ToString();
                    row[4] = dt.Rows[i]["ncpd"].ToString();
                    row[5] = dt.Rows[i]["kyk"].ToString();
                    row[6] = dt.Rows[i]["dy"].ToString();
                    row[7] = dt.Rows[i]["htdy"].ToString();
                    //row[3] = Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[i]["luachon"].ToString()) * 100 / soluot, 2)) + "%";
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX2.Columns[0].Width = 50;
            dataGridViewX2.Columns[1].Width = 50;
            dataGridViewX2.Columns[2].Width = 300;
        }

        private void dataGridViewX2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //CRM.frmMain.manhinhin = 8;
            //chitietHM = dataGridViewX2.CurrentRow.Cells[1].Value.ToString();
            //@In form_in = new @In();
            //form_in.Show();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            String temp = "";
            temp = "TK_CLSPDV.xls";

            saveFileDialog1.FileName = temp.Replace("/", "-");
            saveFileDialog1.Filter = " Excel (*.xls)|*.xls|Tất cả (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            string path = "";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Cursor.Current = Cursors.WaitCursor;
                path = saveFileDialog1.FileName;
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 30;
                for (int i = 0; i < dataGridViewX2.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridViewX2.Rows[i];
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
        }
    }
}