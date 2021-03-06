using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRM
{
    public partial class frmTKTGSPDV : Form

    {
        public static string tuthang = "", denthang = "", luachon = "", chitietHM = "";
        String sCommand = "";

        public frmTKTGSPDV()
        {
            InitializeComponent();
        }

        private void frmTKTGSPDV_Load(object sender, EventArgs e)
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
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdotc.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
            }
            else
            {
                if (optCN.Checked == true)
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDO.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and  khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdotc.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            //Luoi du lieu
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dskh.Columns.Add(col);
            if (optCN.Checked == true)
            {
                col = new DataColumn("Dự thưởng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tặng quà", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Rút thăm trúng thưởng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tặng tiền mặt", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Chuyến du lịch", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Khác", typeof(string));
                dskh.Columns.Add(col);
            }
            else
            {
                col = new DataColumn("Giảm lãi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Giảm phí dịch vụ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Phục vụ tại tổ chức doanh nghiệp", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Miễn phí kết nối thanh toán", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Có chế độ chăm sóc khách VIP", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Khác", typeof(string));
                dskh.Columns.Add(col);
            }

            int iRows = dt.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["mact"].ToString();
                    row[2] = dt.Rows[i]["chitiethanmuc"].ToString();
                    row[3] = dt.Rows[i]["11"].ToString();
                    row[4] = dt.Rows[i]["12"].ToString();
                    row[5] = dt.Rows[i]["13"].ToString();
                    row[6] = dt.Rows[i]["14"].ToString();
                    row[7] = dt.Rows[i]["15"].ToString();
                    row[8] = dt.Rows[i]["16"].ToString();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text.Substring(3, 2) + "/" + dtpFrom.Text.Substring(0, 2) + "/" + dtpFrom.Text.Substring(6, 4);
            denthang = dtpTo.Text.Substring(3, 2) + "/" + dtpTo.Text.Substring(0, 2) + "/" + dtpTo.Text.Substring(6, 4);

            DataTable dt = new DataTable();
            //Tinh toan ket qua tham do chi tieu su dung san pham dich vu
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                if (optCN.Checked == true)
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDO.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdoTC.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
            }
            else
            {
                if (optCN.Checked == true)
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDO.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDO,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=0 and  khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdo.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDO.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
                else
                {
                    sCommand = "select ketquathamdoct.mact,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC,";
                    sCommand = sCommand + "sum(case when luachon= 1 then 1 else 0 end) [11],";
                    sCommand = sCommand + "sum(case when luachon = 2 then 1 else 0 end) [12],";
                    sCommand = sCommand + "sum(case when luachon = 3 then 1 else 0 end) [13],";
                    sCommand = sCommand + "sum(case when luachon = 4 then 1 else 0 end) [14],";
                    sCommand = sCommand + "sum(case when luachon = 5 then 1 else 0 end) [15],";
                    sCommand = sCommand + "sum(case when luachon = 6 then 1 else 0 end) [16]";
                    sCommand = sCommand + " from KETQUATHAMDOCT,CHITIETPHIEUTHAMDOTC,KETQUATHAMDO ,KHACHHANGPHANHOI where  khachhangphanhoi.tochuc=1 and khachhangphanhoi.macn='" + cbCN.Text + "' and khachhangphanhoi.makh=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and chitietphieuthamdotc.mact=ketquathamdoct.mact and ketquathamdo.Thoigian >='" + tuthang + "' and ketquathamdo.Thoigian <='" + denthang + "'";
                    sCommand = sCommand + " group by KETQUATHAMDOCT.mact,CHITIETPHIEUTHAMDOTC.chitiethanmuc having LEFT(KETQUATHAMDOCT.MACT,1)='E' order by KETQUATHAMDOCT.MACT";
                }
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            //Luoi du lieu
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dskh.Columns.Add(col);
            if (optCN.Checked == true)
            {
                col = new DataColumn("Dự thưởng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tặng quà", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Rút thăm trúng thưởng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tặng tiền mặt", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Chuyến du lịch", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Khác", typeof(string));
                dskh.Columns.Add(col);
            }
            else
            {
                col = new DataColumn("Giảm lãi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Giảm phí dịch vụ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Phục vụ tại tổ chức doanh nghiệp", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Miễn phí kết nối thanh toán", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Có chế độ chăm sóc khách VIP", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Khác", typeof(string));
                dskh.Columns.Add(col);
            }

            int iRows = dt.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["mact"].ToString();
                    row[2] = dt.Rows[i]["chitiethanmuc"].ToString();
                    row[3] = dt.Rows[i]["11"].ToString();
                    row[4] = dt.Rows[i]["12"].ToString();
                    row[5] = dt.Rows[i]["13"].ToString();
                    row[6] = dt.Rows[i]["14"].ToString();
                    row[7] = dt.Rows[i]["15"].ToString();
                    row[8] = dt.Rows[i]["16"].ToString();
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

        private void dataGridViewX2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //CRM.frmMain.manhinhin = 23;
            //chitietHM = dataGridViewX2.CurrentRow.Cells[1].Value.ToString();

            //switch (chitietHM)
            //{
            //    case "Dưới 3 T":
            //        luachon = "%M11%";
            //        break;
            //    case "Từ 3-6T":
            //        luachon = "%M12%";
            //        break;
            //    case "Từ 6-9T":
            //        luachon = "%M13%";
            //        break;
            //    case "Từ 9-12T":
            //        luachon = "%M14%";
            //        break;
            //    case "Trên 12T":
            //        luachon = "%M15%";
            //        break;                
            //}

            //@In form_in = new @In();
            //form_in.Show();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            String temp = "";
            temp = "TK_CSKM.xls";

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