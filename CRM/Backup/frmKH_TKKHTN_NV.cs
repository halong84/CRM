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
    public partial class frmKH_TKKHTN_NV : Form
    {
        public static string cn = "", loaikh = "", tinh = "", huyen = "", xa = "", canbo = "";
        //public static int tungay, tuthang, denngay, denthang;

        public frmKH_TKKHTN_NV()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbCN.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLoaiKH.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbHuyen.DropDownStyle = ComboBoxStyle.DropDownList;
            cbXa.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbCB.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmKH_TKKHTN_NV_Load(object sender, EventArgs e)
        {
            if (frmMain.cn == "4800")
            {
                cbbCN.Enabled = true;
            }
            else
            {
                cbbCN.Enabled = false;
            }

            layCN();
            //layLoaiKH();
            layTinh();
            layHuyen();
            layXa();
            layCanbo();
            //txtTungay.Text = DateTime.Now.ToString().Substring(0, 5);
            //txtDenngay.Text = DateTime.Now.ToString().Substring(0, 5);

        }

        private void layCN()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT macn,tencn from Chinhanh order by macn";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbCN.DataSource = dt;
            cbbCN.DisplayMember = "tencn";
            cbbCN.ValueMember = "macn";

            cbbCN.SelectedValue = frmMain.cn;
        }

        private void layLoaiKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT maloai,tenloai from dmloaikhachhang";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "Tenloai";
            cbLoaiKH.ValueMember = "Maloai";
        }
        
        private void layTinh()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT matinh,tentinh from dmtinh order by matinh";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbTinh.DataSource = dt;
            cbTinh.DisplayMember = "tentinh";
            cbTinh.ValueMember = "matinh";
            cbTinh.SelectedValue = "470";
        }
        
        private void layHuyen()
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                sCommand = "SELECT mahuyen,tenhuyen from dmhuyen order by mahuyen ";
            }
            else
            {
                sCommand = "SELECT mahuyen,tenhuyen from dmhuyen where left(mahuyen,3)='" + cbTinh.SelectedValue.ToString() + "' or mahuyen='9999' order by mahuyen ";
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbHuyen.DataSource = dt;
            cbHuyen.DisplayMember = "tenhuyen";
            cbHuyen.ValueMember = "mahuyen";
            //cbHuyen.SelectedValue = "47001";
        }
        
        private void layXa()
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                if (cbHuyen.SelectedValue.ToString() == "9999")
                {
                    sCommand = "SELECT maxa,tenxa from dmxaphuong order by maxa ";
                }
                else
                {
                    sCommand = "SELECT maxa,tenxa from dmxaphuong where left(maxa,5)='" + cbHuyen.SelectedValue.ToString() + "' or maxa='9999' order by maxa ";
                }
            }
            else
            {
                if (cbHuyen.SelectedValue.ToString() == "9999")
                {
                    sCommand = "SELECT maxa,tenxa from dmxaphuong where left(maxa,3)='" + cbTinh.SelectedValue.ToString() + "' or maxa='9999' order by maxa ";
                }
                else
                {
                    sCommand = "SELECT maxa,tenxa from dmxaphuong where left(maxa,5)='" + cbHuyen.SelectedValue.ToString() + "' or maxa='9999' order by maxa ";
                }
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbXa.DataSource = dt;
            cbXa.DisplayMember = "tenxa";
            cbXa.ValueMember = "maxa";
            //cbXa.SelectedValue = "4700101";
        }

        private void layCanbo()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            if (cbbCN.SelectedValue.ToString() == "9999")
            {
                sCommand = "SELECT User_ID,TenNV from _User";
            }
            else
            {
                sCommand = "SELECT User_ID,TenNV from _User Where MaCN='" + cbbCN.SelectedValue.ToString() + "' or MaCN='9999'";
            }
            //String sCommand = "SELECT User_ID,TenNV from _User Where MaCN='" + cbbCN.SelectedValue.ToString() + "' or MACN='9999'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbCB.DataSource = dt;
            cbbCB.DisplayMember = "TenNV";
            cbbCB.ValueMember = "User_ID";
        }

        private void cbTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            //layHuyen();
            //layXa();
        }

        private void cbHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //layXa();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 7;
            //loaikh = cbLoaiKH.Text;
            //if (cbLoaiKH.Text == "Tất cả")
            //{
            //    loaikh = "%%";
            //}
            //else
            //{
            //    loaikh = cbLoaiKH.Text;
            //}
            if (cbbCN.SelectedValue.ToString() == "9999")
            {
                cn = "%%";
            }
            else
            {
                cn = cbbCN.SelectedValue.ToString();
            }
            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                tinh = "%%";
            }
            else
            {
                tinh = cbTinh.SelectedValue.ToString();
            }
            if (cbHuyen.SelectedValue.ToString() == "9999")
            {
                huyen = "%%";
            }
            else
            {
                huyen = cbHuyen.SelectedValue.ToString();
            }
            if (cbXa.SelectedValue.ToString() == "9999")
            {
                xa = "%%";
            }
            else
            {
                xa = cbXa.SelectedValue.ToString();
            }
            if (cbbCB.SelectedValue.ToString() == "9999")
            {
                canbo = "%%";
            }
            else
            {
                canbo = cbbCB.SelectedValue.ToString();
            }
            
            @In form_in = new @In();
            form_in.Show();
        }

        private void cbTinh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layHuyen();
        }

        private void cbHuyen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layXa();
        }

        private void cbbCN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layCanbo();
        }
    }
}