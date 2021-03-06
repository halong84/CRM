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
    public partial class frmNhom_LoaiKH : Form
    {
        public frmNhom_LoaiKH()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 
        }

        private void frmNhom_LoaiKH_Load(object sender, EventArgs e)
        {
            layLoaiKH();
            layDSLoaiKH();
        }

        private void layLoaiKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * from DMXEPLOAIKH Where MALOAI<>'9999' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "MaLoai";
            cbLoaiKH.ValueMember = "MaLoai";
            cbLoaiKH.DataSource = dt;
        }

        private void cbLoaiKH_TextChanged(object sender, EventArgs e)
        {
            if (cbLoaiKH.ValueMember != "")
            {
                DataTable dt = new DataTable();
                String sCommand = "SELECT * from DMXEPLOAIKH where maloai='" + cbLoaiKH.Text + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                lblTenloai.Text = dt.Rows[0]["Tenloai"].ToString();
            }
        }

        private void layDSLoaiKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * from NHOM_LoaiKH where manhom='" + CRM.frmNhomKH.manhom + "'";
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

            col = new DataColumn("Mã nhóm KH", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Mã loại KH", typeof(string));
            dskh.Columns.Add(col);
            
            col = new DataColumn("Ngày cập nhật", typeof(string));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["manhom"].ToString();
                    row[2] = dt.Rows[i]["maloai"].ToString();
                    row[3] = dt.Rows[i]["ngaycapnhat"].ToString().Substring(0,10);                   
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX1.DataSource = dskh;
            
            dataGridViewX1.Columns[0].Width = 30;
            dataGridViewX1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[1].Visible = false;
            dataGridViewX1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String ngaycapnhat, sCommand;
            ngaycapnhat = DateTime.Now.ToString().Substring(3, 2) + "/" + DateTime.Now.ToString().Substring(0, 2) + "/" + DateTime.Now.ToString().Substring(6, 4);      
            
            sCommand = "insert into nhom_loaikh(manhom,maloai,ngaycapnhat) values('" + CRM.frmNhomKH.manhom + "','" + cbLoaiKH.Text + "','" + ngaycapnhat + "')";
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch 
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                MessageBox.Show("Đã có!"); 
            }
            
            layDSLoaiKH();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbLoaiKH.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[2].Value.ToString();
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            String sCommand = "delete nhom_loaikh where manhom='" + CRM.frmNhomKH.manhom + "' and maloai='" + cbLoaiKH.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();
            layDSLoaiKH();
        }
    }
}