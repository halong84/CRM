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
    public partial class frmKHWU : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();

        String strCmd = "";
        public static string hoten = "", cmnd = "", diachi = "";
        public static string cn = "", tuthang = "", denthang = "";
        public static DataTable dtKHWU = new DataTable();
        DataTable dskh;
        
        public frmKHWU()
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
            dgvDanhsach.Font = new Font("Arial", 9F);

            cbbMaCN.DropDownStyle = ComboBoxStyle.DropDownList;
            
            DateTime dtCurrent = DateTime.Now;
            dtpFrom.CustomFormat = "MM/yyyy";
            dtpTo.CustomFormat = "MM/yyyy";
            if (dtCurrent.Month == 1)
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
            }
            else
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
            }            
        }

        private void frmKHWU_Load(object sender, EventArgs e)
        {
            if (Thongtindangnhap.macn == Thongtindangnhap.ma_hoi_so)
            {
                cbbMaCN.Enabled = true;
            }
            else
            {
                cbbMaCN.Enabled = false;
            }

            layDS_CN();

            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
        }

        private void layDS_CN()
        {
            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();

            //cbbMaCN.DataSource = dt;
            cbbMaCN.DisplayMember = "MACN";
            cbbMaCN.ValueMember = "MACN";
            cbbMaCN.DataSource = dt;
            cbbMaCN.SelectedValue = Thongtindangnhap.macn;
        }

        private void layDS()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cbbMaCN.SelectedValue.ToString() == "9999")
            {
                strCmd = "SELECT * FROM WU ";
		        strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
		        strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) ";
		        strCmd += " Order by Macn, THANG";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) ";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;
            }
            else
            {
                strCmd = "SELECT * FROM WU ";
                strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "'";
                strCmd += " Order by Macn, CMND";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();                

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "'";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {                    
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;
            }
            Cursor.Current = Cursors.Default;
        }

        private void layDS_TenKH()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cbbMaCN.SelectedValue.ToString() == "9999")
            {
                strCmd = "SELECT * FROM WU ";
                strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND HOTEN like '%" + txtSTenKH.Text.Trim() + "%' ";
                strCmd += " Order by Macn, THANG";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) ";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;
            }
            else
            {
                strCmd = "SELECT * FROM WU ";
                strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "' AND HOTEN like '%" + txtSTenKH.Text.Trim() + "%' ";
                strCmd += " Order by Macn, THANG";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "'";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;               
            }
            Cursor.Current = Cursors.Default;
        }

        private void layDS_CMND()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cbbMaCN.SelectedValue.ToString() == "9999")
            {
                strCmd = "SELECT * FROM WU ";
                strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND CMND like '%" + txtSCMND.Text.Trim() + "%' ";
                strCmd += " Order by Macn, THANG";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) ";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;
            }
            else
            {
                strCmd = "SELECT * FROM WU ";
                strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "' AND CMND like '%" + txtSCMND.Text.Trim() + "%' ";
                strCmd += " Order by Macn, THANG";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "'";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;
            }
            Cursor.Current = Cursors.Default;            
        }

        private void layDS_Diachi()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cbbMaCN.SelectedValue.ToString() == "9999")
            {
                strCmd = "SELECT * FROM WU ";
                strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND DIACHI like '%" + txtSDiachi.Text.Trim() + "%' ";
                strCmd += " Order by Macn, THANG";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) ";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;
            }
            else
            {
                strCmd = "SELECT * FROM WU ";
                strCmd += " where CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "' AND DIACHI like '%" + txtSDiachi.Text.Trim() + "%' ";
                strCmd += " Order by Macn, THANG";

                dtKHWU = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dtKHWU);
                DataAccess.conn.Close();

                dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("CN", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tháng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Số lần nhận", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dtKHWU.Rows.Count;
                //DataRow row;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dtKHWU.Rows[i]["MACN"].ToString();
                        row[2] = dtKHWU.Rows[i]["THANG"].ToString();
                        row[3] = dtKHWU.Rows[i]["HOTEN"].ToString();
                        row[4] = dtKHWU.Rows[i]["CMND"].ToString();
                        row[5] = dtKHWU.Rows[i]["DIACHI"].ToString();
                        row[6] = "";
                        dskh.Rows.Add(row);
                    }
                    catch { };
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = i + 1; j < dskh.Rows.Count; j++)
                    {
                        if (dskh.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                        {
                            dskh.Rows[j].Delete();
                            j--;                            
                        }
                    }
                }
                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    dskh.Rows[i]["STT"] = i + 1;
                }

                strCmd = "SELECT THANG,CMND, count(CMND) as solan FROM WU group by MACN,THANG,CMND ";
                strCmd += " Having CONVERT(date,LEFT(THANG,2)+'/01/'+RIGHT(THANG,4)) BETWEEN (LEFT('" + dtpFrom.Text + "',2)+'/01/'+RIGHT('" + dtpFrom.Text + "',4)) ";
                strCmd += " AND (LEFT('" + dtpTo.Text + "',2)+'/01/'+RIGHT('" + dtpTo.Text + "',4)) AND MACN='" + cbbMaCN.SelectedValue.ToString() + "'";
                strCmd += " Order by Macn, Solan desc";

                DataTable dt = new DataTable();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                for (int i = 0; i < dskh.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        try
                        {
                            if (dt.Rows[j]["CMND"].ToString() == dskh.Rows[i]["CMND"].ToString())
                            {
                                dskh.Rows[i]["Số lần nhận"] += dt.Rows[j]["THANG"].ToString() + ": " + dt.Rows[j]["solan"].ToString() + "; ";
                            }
                        }
                        catch { };
                    }
                }

                dgvDanhsach.DataSource = dskh;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Width = 40;
                dgvDanhsach.Columns[2].Width = 60;
                dgvDanhsach.Columns[2].Visible = false;
                dgvDanhsach.Columns[3].Width = 200;
                dgvDanhsach.Columns[4].Width = 100;
                dgvDanhsach.Columns[5].Width = 250;
                dgvDanhsach.Columns[6].Width = 200;
            }
            Cursor.Current = Cursors.Default;            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            layDS();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            layDS_CMND();
        }

        private void btnSTenKH_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            layDS_TenKH();
        }

        private void btnSDiachi_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            layDS_Diachi();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmMain.manhinhin = 19;
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            if (cbbMaCN.SelectedValue.ToString() == "9999")
            {
                cn = "9999";
            }
            else
            {
                cn = cbbMaCN.SelectedValue.ToString();
            }
            
            @In form_in = new @In();
            form_in.Show();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            String temp = "Khach hang WU";

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
                ExcelApp.Columns.ColumnWidth = 10;

                //Xuat tieu de cot
                for (int i = 0; i < dtKHWU.Columns.Count; i++)
                {
                    ExcelApp.Cells[1, i + 1] = dtKHWU.Columns[i].ColumnName;
                }
                //Xuat du lieu
                for (int j = 0; j < dtKHWU.Rows.Count; j++)
                {
                    for (int i = 0; i < dtKHWU.Columns.Count; i++)
                    {
                        ExcelApp.Cells[j + 2, i + 1] = dtKHWU.Rows[j][i].ToString();
                    }
                }

                ExcelApp.ActiveWorkbook.SaveCopyAs(path);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Đã xuất ra file excel.");
            }
            Cursor.Current = Cursors.Default;
        }

        private void dgvDanhsach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            hoten = dgvDanhsach.CurrentRow.Cells["Họ tên"].Value.ToString();
            cmnd = dgvDanhsach.CurrentRow.Cells["CMND"].Value.ToString();
            diachi = dgvDanhsach.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            cn = dgvDanhsach.CurrentRow.Cells["CN"].Value.ToString();

            frmKHWU_Detail frmKH = new frmKHWU_Detail();
            frmKH.ShowDialog();
        }
    }
}