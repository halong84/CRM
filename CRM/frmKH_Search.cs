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
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmKH_Search : Form
    {
        public frmKH_Search()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonX19_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            DataTable dt = new DataTable();
            //sCommand = "select khachhang.* from khachhang where khachhang.makh='" + textBox1.Text + "'";
            if (frmMain.flagSearch == 1)
            {
                if (CRM.frmKH_GiaoDich.loaikh == "HH")
                {
                    if (CRM.frmKH_GiaoDich.loaikhcn_dn == 1)
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.makh like '%" + textBox1.Text + "%'and loaikh=1 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                    else
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.makh like '%" + textBox1.Text + "%' and loaikh=2 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.makh like '%" + textBox1.Text + "%' and khachhangtiemnang.macn='" + Thongtindangnhap.macn + "' ";
                }
            }
            else if (frmMain.flagSearch == 2)
            {
                if (frmKH_TKGD.loaikh == "HH")
                {
                    sCommand = "select khachhang.* from khachhang where khachhang.makh like '%" + textBox1.Text + "%' and khachhang.macn='" + frmKH_TKGD.maCN + "' ";
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.makh like '%" + textBox1.Text + "%' and khachhangtiemnang.macn='" + frmKH_TKGD.maCN + "' ";
                }
            }
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
            col = new DataColumn("Ngày sinh", typeof(string));
            dskh.Columns.Add(col);
            
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["makh"].ToString();

                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["diachi1"].ToString();
                    row[4] = dt.Rows[i]["dienthoai1"].ToString();
                    row[5] = dt.Rows[i]["cmnd"].ToString();
                    row[6] = dt.Rows[i]["ngaysinh"].ToString();                    
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Cursor.Current = Cursors.Default;
        }

        private void buttonX44_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            DataTable dt = new DataTable();
            //sCommand = "select khachhang.* from khachhang where khachhang.makh='" + textBox1.Text + "'";
            if (frmMain.flagSearch == 1)
            {
                if (CRM.frmKH_GiaoDich.loaikh == "HH")
                {
                    if (CRM.frmKH_GiaoDich.loaikhcn_dn == 1)
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.cmnd like '%" + textBox10.Text + "%'and loaikh=1 and khachhang.macn='" + Thongtindangnhap.macn + "' ";                        
                    }
                    else
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.cmnd like '%" + textBox10.Text + "%' and loaikh=2 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.cmnd like '%" + textBox10.Text + "%' and khachhangtiemnang.macn='" + Thongtindangnhap.macn + "' ";
                }
            }
            else if (frmMain.flagSearch == 2)
            {
                if (frmKH_TKGD.loaikh == "HH")
                {
                    sCommand = "select khachhang.* from khachhang where khachhang.cmnd like '%" + textBox10.Text + "%' and khachhang.macn='" + frmKH_TKGD.maCN + "' ";
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.cmnd like '%" + textBox10.Text + "%' and khachhangtiemnang.macn='" + frmKH_TKGD.maCN + "' ";
                }
            }
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
            col = new DataColumn("Ngày sinh", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["makh"].ToString();

                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["diachi1"].ToString();
                    row[4] = dt.Rows[i]["dienthoai1"].ToString();
                    row[5] = dt.Rows[i]["cmnd"].ToString();
                    row[6] = dt.Rows[i]["ngaysinh"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Cursor.Current = Cursors.Default;
        }

        private void dataGridViewX2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (frmMain.flagSearch == 1)
            {
                CRM.frmKH_GiaoDich.makh = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[1].Value.ToString();
                CRM.frmKH_GiaoDich.hoten = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[2].Value.ToString();
            }
            else if (frmMain.flagSearch == 2)
            {
                frmKH_TKGD.makh = dataGridViewX2.CurrentRow.Cells[1].Value.ToString();
            }
            this.Close();
        }

        private void KH_Search_Load(object sender, EventArgs e)
        {

        }

        private void buttonX28_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            DataTable dt = new DataTable();

            if (frmMain.flagSearch == 1)
            {
                if (CRM.frmKH_GiaoDich.loaikh == "HH")
                {
                    if (CRM.frmKH_GiaoDich.loaikhcn_dn == 1)
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.hoten like N'%" + txt_timkh.Text + "%'and loaikh=1 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                    else
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.hoten like N'%" + txt_timkh.Text + "%' and loaikh=2 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.hoten like N'%" + txt_timkh.Text + "%' and khachhangtiemnang.macn='" + Thongtindangnhap.macn + "' ";
                }
            }
            else if (frmMain.flagSearch == 2)
            {
                if (frmKH_TKGD.loaikh == "HH")
                {
                    sCommand = "select khachhang.* from khachhang where khachhang.hoten like N'%" + txt_timkh.Text + "%' and khachhang.macn='" + frmKH_TKGD.maCN + "' ";
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.hoten like N'%" + txt_timkh.Text + "%' and khachhangtiemnang.macn='" + frmKH_TKGD.maCN + "' ";
                }
            }
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
            col = new DataColumn("Ngày sinh", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["makh"].ToString();

                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["diachi1"].ToString();
                    row[4] = dt.Rows[i]["dienthoai1"].ToString();
                    row[5] = dt.Rows[i]["cmnd"].ToString();
                    row[6] = dt.Rows[i]["ngaysinh"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Cursor.Current = Cursors.Default;
        }

        private void buttonX46_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            DataTable dt = new DataTable();

            if (frmMain.flagSearch == 1)
            {
                if (CRM.frmKH_GiaoDich.loaikh == "HH")
                {
                    if (CRM.frmKH_GiaoDich.loaikhcn_dn == 1)
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.dienthoai1 like '%" + txtSTel.Text + "%'and loaikh=1 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                    else
                    {
                        sCommand = "select khachhang.* from khachhang where khachhang.dienthoai1 like '%" + txtSTel.Text + "%' and loaikh=2 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.dienthoai1 like '%" + txtSTel.Text + "%' and khachhangtiemnang.macn='" + Thongtindangnhap.macn + "' ";
                }
            }
            else if (frmMain.flagSearch == 2)
            {
                if (frmKH_TKGD.loaikh == "HH")
                {
                    sCommand = "select khachhang.* from khachhang where khachhang.dienthoai1 like '%" + txtSTel.Text + "%' and khachhang.macn='" + frmKH_TKGD.maCN + "' ";
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.dienthoai1 like '%" + txtSTel.Text + "%' and khachhangtiemnang.macn='" + frmKH_TKGD.maCN + "' ";
                }
            }
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
            col = new DataColumn("Ngày sinh", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["makh"].ToString();

                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["diachi1"].ToString();
                    row[4] = dt.Rows[i]["dienthoai1"].ToString();
                    row[5] = dt.Rows[i]["cmnd"].ToString();
                    row[6] = dt.Rows[i]["ngaysinh"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Cursor.Current = Cursors.Default;
        }

        private void buttonX18_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            DataTable dt = new DataTable();

            if (frmMain.flagSearch == 1)
            {
                if (CRM.frmKH_GiaoDich.loaikh == "HH")
                {
                    if (CRM.frmKH_GiaoDich.loaikhcn_dn == 1)
                    {
                        sCommand = "select khachhang.* from khachhang where (Day(khachhang.Ngaysinh) between '" + dtpSFrom.Text.Substring(0, 2) + "' and '" + dtpSTo.Text.Substring(0, 2) + "') ";
                        sCommand += " and (Month(khachhang.Ngaysinh) between '" + dtpSFrom.Text.Substring(3, 2) + "' and '" + dtpSTo.Text.Substring(3, 2) + "') ";
                        sCommand += " and loaikh=1 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                    else
                    {
                        sCommand = "select khachhang.* from khachhang where (Day(khachhang.Ngaysinh) between '" + dtpSFrom.Text.Substring(0, 2) + "' and '" + dtpSTo.Text.Substring(0, 2) + "') ";
                        sCommand += " and (Month(khachhang.Ngaysinh) between '" + dtpSFrom.Text.Substring(3, 2) + "' and '" + dtpSTo.Text.Substring(3, 2) + "') ";
                        sCommand += " and loaikh=2 and khachhang.macn='" + Thongtindangnhap.macn + "' ";
                    }
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where (Day(khachhangtiemnang.Ngaysinh) between '" + dtpSFrom.Text.Substring(0, 2) + "' and '" + dtpSTo.Text.Substring(0, 2) + "') ";
                    sCommand += " and (Month(khachhangtiemnang.Ngaysinh) between '" + dtpSFrom.Text.Substring(3, 2) + "' and '" + dtpSTo.Text.Substring(3, 2) + "') ";
                    sCommand += " and khachhangtiemnang.macn='" + Thongtindangnhap.macn + "' ";
                }
            }
            else if (frmMain.flagSearch == 2)
            {
                if (frmKH_TKGD.loaikh == "HH")
                {
                    sCommand = "select khachhang.* from khachhang where (Day(khachhang.Ngaysinh) between '" + dtpSFrom.Text.Substring(0, 2) + "' and '" + dtpSTo.Text.Substring(0, 2) + "') ";
                    sCommand += " and (Month(khachhang.Ngaysinh) between '" + dtpSFrom.Text.Substring(3, 2) + "' and '" + dtpSTo.Text.Substring(3, 2) + "') ";
                    sCommand += " and khachhang.macn='" + frmKH_TKGD.maCN + "' ";
                }
                else
                {
                    sCommand = "select khachhangtiemnang.* from khachhangtiemnang where (Day(khachhangtiemnang.Ngaysinh) between '" + dtpSFrom.Text.Substring(0, 2) + "' and '" + dtpSTo.Text.Substring(0, 2) + "') ";
                    sCommand += " and (Month(khachhangtiemnang.Ngaysinh) between '" + dtpSFrom.Text.Substring(3, 2) + "' and '" + dtpSTo.Text.Substring(3, 2) + "') ";
                    sCommand += " and khachhangtiemnang.macn='" + frmKH_TKGD.maCN + "' ";
                }
            }
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
            col = new DataColumn("Ngày sinh", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["makh"].ToString();

                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["diachi1"].ToString();
                    row[4] = dt.Rows[i]["dienthoai1"].ToString();
                    row[5] = dt.Rows[i]["cmnd"].ToString();
                    row[6] = dt.Rows[i]["ngaysinh"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Cursor.Current = Cursors.Default;
        }

        private void buttonX134_Click(object sender, EventArgs e)
        {
            
        }
    }
}