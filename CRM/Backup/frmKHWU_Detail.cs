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

namespace CRM
{
    public partial class frmKHWU_Detail : Form
    {
        private string strCmd = "";
        DataTable dtResult = new DataTable();

        public frmKHWU_Detail()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;
        }

        private void frmKHWU_Detail_Load(object sender, EventArgs e)
        {
            txtHoten.Text = frmKHWU.hoten;
            txtCMND.Text = frmKHWU.cmnd;
            txtDiachi.Text = frmKHWU.diachi;
            layDanhsach();
        }

        private void layDanhsach()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã số nhận tiền", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH gửi tiền", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số tiền gửi", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số tiền nhận", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày nhận", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CN", typeof(string));  //6
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT * FROM WU ";
            strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + frmKHWU.tuthang + "',2)+'/01/'+RIGHT('" + frmKHWU.tuthang + "',4)) ";
            strCmd += " AND (LEFT('" + frmKHWU.denthang + "',2)+'/01/'+RIGHT('" + frmKHWU.denthang + "',4)) AND MACN='" + frmKHWU.cn + "' AND CMND like '%" + frmKHWU.cmnd + "%' ";
            strCmd += " Order by Macn, Ngaynhan";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MTCN"].ToString();
                    row[2] = dtResult.Rows[i]["HOTEN_GUI"].ToString();

                    if (dtResult.Rows[i]["SOTIEN_GUI"].ToString() == "")
                    {
                        row[3] = "";
                    }
                    else
                    {
                        row[3] = String.Format("{0:0.00}", Decimal.Parse(dtResult.Rows[i]["SOTIEN_GUI"].ToString())) + " " + dtResult.Rows[i]["CCY_GUI"].ToString();
                    }

                    if (dtResult.Rows[i]["SOTIEN_GUI"].ToString() == "")
                    {
                        row[4] = "";
                    }
                    else
                    {
                        row[4] = String.Format("{0:0.00}", Decimal.Parse(dtResult.Rows[i]["SOTIEN"].ToString())) + " " + dtResult.Rows[i]["CCY"].ToString();
                    }
                    
                    if (dtResult.Rows[i]["NGAYNHAN"].ToString() != "")
                    {
                        string ngayNhan, ngayN, thangN, namN;
                        ngayNhan = dtResult.Rows[i]["NGAYNHAN"].ToString();

                        ngayN = ngayNhan.Substring(0, 2);
                        thangN = ngayNhan.Substring(3, 2);
                        namN = ngayNhan.Substring(6, 4);

                        row[5] = ngayN + "/" + thangN + "/" + namN;
                    }
                    row[6] = dtResult.Rows[i]["MACN"].ToString();

                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 160;
            dgvDanhsach.Columns[2].Width = 200;
            dgvDanhsach.Columns[3].Width = 120;
            //dgvDanhsach.Columns[3].DefaultCellStyle.Format = "N0";
            dgvDanhsach.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[4].Width = 150;
            //dgvDanhsach.Columns[4].DefaultCellStyle.Format = "N0";
            dgvDanhsach.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[5].Width = 110;
            dgvDanhsach.Columns[6].Width = 50;
            Cursor.Current = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}