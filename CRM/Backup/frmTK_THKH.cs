using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CRM
{
    public partial class frmTK_THKH : Form
    {
        public static string cn = "", thang = "", xeploai = "", sukien = "", kehoach = "",nhanqua="";
        public static int loaikh;

        public frmTK_THKH()
        {
            InitializeComponent();
        }

        private void frmTK_THKH_Load(object sender, EventArgs e)
        {
            optCN.Checked = true;            
            if (CRM.frmDangnhap.macn != "4800")
            {
                cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
            else
                cbCN.Text = "4800";
            layDS_Tieuchi();
            layLoaiKH();
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
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "TenLoai";
            cbLoaiKH.ValueMember = "MaLoai";
            cbLoaiKH.SelectedIndex = 0;
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
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbTieuchi.DataSource = dt;
            cbTieuchi.DisplayMember = "TIEUCHI";
            cbTieuchi.ValueMember = "MATC";
            cbTieuchi.SelectedIndex = 0;
        }

        private void optCN_Click(object sender, EventArgs e)
        {
            optCN.Checked = true;
            optDN.Checked = false;
            optLDDN.Checked = false;
            layLoaiKH();
            layDS_Tieuchi();
            
        }

        private void optDN_Click(object sender, EventArgs e)
        {
            optDN.Checked = true;
            optCN.Checked = false;
            optLDDN.Checked = false;
            layLoaiKH();
            layDS_Tieuchi();
            
        }
        private void layKehoach()
        {
            DataTable dt = new DataTable();
            string quy = cbQuy.Text + "/" + dtpThang.Text;
            String sCommand = "SELECT * FROM kehoachchamsoc where thang='" + quy + "' and pheduyet=1";
            if ((cbCN.Text != "") && (cbCN.Text != "Tất cả"))
                sCommand = sCommand + " and macn='" + cbCN.Text + "'";
            if ((cbTieuchi.Text != "") && (cbTieuchi.Text != "Tất cả"))
                sCommand = sCommand + " and matc='" + cbTieuchi.SelectedValue.ToString() + "'";
            if ((cbLoaiKH.Text != "") && (cbLoaiKH.Text != "Tất cả"))
               sCommand = sCommand + " and xeploaikh='" + cbLoaiKH.SelectedValue.ToString() + "'";
            if (optCN.Checked == true)
                sCommand = sCommand + " and loaikh=1";
            if (optDN.Checked == true)
                sCommand = sCommand + " and loaikh=2";
            if (optLDDN.Checked == true)
                sCommand = sCommand + " and loaikh=3";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbKeHoach.DataSource = dt;
            cbKeHoach.DisplayMember = "NoiDung";
            cbKeHoach.ValueMember = "Ma";
            cbKeHoach.Text = "";

        }

        private void cbCN_TextChanged(object sender, EventArgs e)
        {
           layKehoach();
        }

        private void cbTieuchi_TextChanged(object sender, EventArgs e)
        {
            layKehoach();
        }

        private void cbLoaiKH_TextChanged(object sender, EventArgs e)
        {
            layKehoach();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 21;
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                cn = "9999";
            }
            else
                cn = cbCN.Text;
            thang = cbQuy.Text + "/" + dtpThang.Text; 
            if ((cbLoaiKH.Text == "") || (cbLoaiKH.Text == "Tất cả"))
                xeploai = "%%";
            else
                xeploai = cbLoaiKH.SelectedValue.ToString();

            if ((cbTieuchi.Text == "") || (cbTieuchi.Text == "Tất cả"))
                sukien = "%%";
            else
                sukien = cbTieuchi.SelectedValue.ToString();
            if ((cbKeHoach.Text == "") || (cbKeHoach.Text == "Tất cả"))
                kehoach = "%%";
            else
                kehoach = cbKeHoach.SelectedValue.ToString();
            if (optNhan.Checked == true)
                nhanqua = "1";
            if (optChuanhan.Checked == true)
                nhanqua = "0";
            if (optTatca.Checked == true)
                nhanqua = "%%";
            if (optCN.Checked == true)
                loaikh = 1;
            if (optDN.Checked == true)
                loaikh = 2;
            if (optLDDN.Checked == true)
                loaikh = 3;
            @In form_in = new @In();
            form_in.Show();    
        }

        private void optDN_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optLDDN_Click(object sender, EventArgs e)
        {
            optLDDN.Checked = true;
            optCN.Checked = false;
            optDN.Checked = false;
            layLoaiKH();
            layDS_Tieuchi();
            
        }

    }
}