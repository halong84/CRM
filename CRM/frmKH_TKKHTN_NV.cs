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
    public partial class frmKH_TKKHTN_NV : Form
    {
        TinhBUS t_bus = new TinhBUS();
        HuyenBUS h_bus = new HuyenBUS();
        XaBUS x_bus = new XaBUS();

        ChinhanhBUS cnbus = new ChinhanhBUS();
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
            if (Thongtindangnhap.macn == Thongtindangnhap.ma_hoi_so)
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
            DataTable dt = cnbus.DANH_SACH_CHI_NHANH();

            //cbbCN.DataSource = dt;
            cbbCN.DisplayMember = "TENCN";
            cbbCN.ValueMember = "MACN";
            cbbCN.DataSource = dt;
            cbbCN.SelectedValue = Thongtindangnhap.macn;
        }

        private void layLoaiKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT maloai,tenloai from dmloaikhachhang";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "Tenloai";
            cbLoaiKH.ValueMember = "Maloai";
            cbLoaiKH.DataSource = dt;
        }
        
        private void layTinh()
        {
            DataTable dt = t_bus.DANH_SACH_TINH();
            //cbTinh.DataSource = dt;
            cbTinh.DisplayMember = "TENTINH";
            cbTinh.ValueMember = "MATINH";
            cbTinh.DataSource = dt;
            cbTinh.SelectedValue = Thongtindangnhap.ma_tinh_hien_tai;
        }
        
        private void layHuyen()
        {
            DataTable dt = new DataTable();

            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                dt = h_bus.DANH_SACH_HUYEN_ALL();
            }
            else
            {
                dt = h_bus.DANH_SACH_HUYEN(cbTinh.SelectedValue.ToString());
            }
            //cbHuyen.DataSource = dt;
            cbHuyen.DisplayMember = "TENHUYEN";
            cbHuyen.ValueMember = "MAHUYEN";
            cbHuyen.DataSource = dt;
            //cbHuyen.SelectedValue = "9999";
        }
        
        private void layXa()
        {
            DataTable dt = new DataTable();
            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                if (cbHuyen.SelectedValue.ToString() == "9999")
                {
                    dt = x_bus.DANH_SACH_XA_ALL();
                }
                else
                {
                    dt = x_bus.DANH_SACH_XA(cbHuyen.SelectedValue.ToString());
                }
            }
            else
            {
                if (cbHuyen.SelectedValue.ToString() == "9999")
                {
                    dt = x_bus.DANH_SACH_XA_THEO_TINH(cbTinh.SelectedValue.ToString());
                }
                else
                {
                    dt = x_bus.DANH_SACH_XA(cbHuyen.SelectedValue.ToString());
                }
            }
            //cbXa.DataSource = dt;
            cbXa.DisplayMember = "TENXA";
            cbXa.ValueMember = "MAXA";
            cbXa.DataSource = dt;
            //cbXa.SelectedValue = "9999";

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
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbCB.DataSource = dt;
            cbbCB.DisplayMember = "TenNV";
            cbbCB.ValueMember = "User_ID";
            cbbCB.DataSource = dt;
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