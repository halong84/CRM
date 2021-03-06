using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace CRM
{
    public partial class frmImport_Lichsu : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        
        public frmImport_Lichsu()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;

            DateTime dtCurrent = DateTime.Now;
            dtpThang.CustomFormat = "MM/yyyy";
            //dtpThang.Value = dtCurrent.AddMonths(-1);
            if (dtCurrent.Month == 1)
            {
                dtpThang.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);                
            }
            else
            {
                dtpThang.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);                
            }
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CN", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tháng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tiền tệ", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "SELECT c.*, im.Ten FROM CAPNHAT c inner join DMIMPORT im ON c.Loai=im.MaDM ";
            strCmd += " WHERE c.MaCN='" + frmMain.cn + "' and c.Thang='" + dtpThang.Text + "' ORDER BY c.Thang, c.CCY ";

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
                    row[1] = dtResult.Rows[i]["MaCN"].ToString();
                    row[2] = dtResult.Rows[i]["Thang"].ToString();
                    row[3] = dtResult.Rows[i]["Ten"].ToString();
                    row[4] = dtResult.Rows[i]["ccy"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 60;
            dgvDanhsach.Columns[1].Width = 80;
            dgvDanhsach.Columns[2].Width = 100;
            dgvDanhsach.Columns[3].Width = 250;
            dgvDanhsach.Columns[4].Width = 80;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            layDanhsach();
        }
    }
}