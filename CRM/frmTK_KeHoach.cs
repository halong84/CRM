using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmTK_KeHoach : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string cn = "", thang = "",xeploai="",sukien="";
        public static int loaikh;
        
        public frmTK_KeHoach()
        {

            InitializeComponent();
        }

        private void frmTK_KeHoach_Load(object sender, EventArgs e)
        {
            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Tất cả";
            dt.Rows.InsertAt(first_row, 0);

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "MACN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;

            optCN.Checked = true;
            layDS_Tieuchi();
            layLoaiKH();
            if (Thongtindangnhap.macn != Thongtindangnhap.ma_hoi_so)
            {
                //cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
            else
            {
                cbCN.Enabled = true;
            }
        }
        private void layLoaiKH()
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                sCommand = "SELECT * from DMXEPLOAIKH where loaikh=1 or MALOAI='9999' order by MALOAI desc";
            }
            else
            {
                sCommand = "SELECT * from DMXEPLOAIKH where loaikh=2 or MALOAI='9999' order by MALOAI desc";
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "TenLoai";
            cbLoaiKH.ValueMember = "MaLoai";
            cbLoaiKH.DataSource = dt;
            //lblTenloai.Text = dt.Rows[0]["Tenloai"].ToString();
        }


        private void layDS_Tieuchi()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            if ((optCN.Checked == true)||(optLDDN.Checked == true))
                sCommand = "SELECT * FROM LICHCHAMSOC where ghichu like '%CN%' ";
            else
                sCommand = "SELECT * FROM LICHCHAMSOC where ghichu like '%DN%' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbTieuchi.DataSource = dt;
            cbTieuchi.DisplayMember = "TIEUCHI";
            cbTieuchi.ValueMember = "MATC";
            cbTieuchi.DataSource = dt;
            cbTieuchi.SelectedIndex = 0;
        }

        private void optCN_Click(object sender, EventArgs e)
        {
            optCN.Checked = true;
            optDN.Checked = false;
            optLDDN.Checked = false;
            layDS_Tieuchi();
            layLoaiKH();
        }

        private void optDN_Click(object sender, EventArgs e)
        {
            optDN.Checked = true;
            optCN.Checked = false;
            optLDDN.Checked = false;
            layDS_Tieuchi();
            layLoaiKH();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 20;
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                cn = "9999";
            }
            else
                cn = cbCN.Text;
            thang = cbQuy.Text+"/"+ dtpThang.Text;
            if ((cbLoaiKH.Text == "")||(cbLoaiKH.Text=="Tất cả"))
                xeploai = "%%";
            else
                xeploai = cbLoaiKH.SelectedValue.ToString();   
                
            if ((cbTieuchi.Text == "")||(cbTieuchi.Text=="Tất cả"))
                sukien = "%%";
            else
                sukien = cbTieuchi.SelectedValue.ToString();
            if (optCN.Checked == true)
                loaikh = 1;
            if (optDN.Checked == true)
                loaikh = 2;
            if (optLDDN.Checked == true)
                loaikh = 3;
            @In form_in = new @In();
            form_in.Show();    
            
        }

        private void optLDDN_Click(object sender, EventArgs e)
        {
            optLDDN.Checked = true;
            optCN.Checked = false;
            optDN.Checked = false;
            layDS_Tieuchi();
            layLoaiKH();
        }
    }
}