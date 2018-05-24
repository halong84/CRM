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
    public partial class frmKH_TKKHHHCN : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string loaikh = "",cn= "";
        public static int tungay, tuthang, denngay, denthang;

        public frmKH_TKKHHHCN()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmKH_TKKHHHCN_Load(object sender, EventArgs e)
        {
            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Toàn tỉnh";
            dt.Rows.InsertAt(first_row, 0);

            //cbChiNhanh.DataSource = dt;
            cbChiNhanh.DisplayMember = "MACN";
            cbChiNhanh.ValueMember = "MACN";
            cbChiNhanh.DataSource = dt;
            cbChiNhanh.SelectedValue = Thongtindangnhap.macn;

            layLoaiKH();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 2;
            cn = cbChiNhanh.Text;
            loaikh = cbLoaiKH.Text;
            
            if (txtTungay.Text == "")
            {
                tungay = 1;
                tuthang = 1;
            }
            else
            {
                tungay = Convert.ToInt16(txtTungay.Text.Substring(0, 2));
                tuthang = Convert.ToInt16(txtTungay.Text.Substring(3, 2));
            }
            if (txtDenngay.Text == "")
            {
                denngay = 31;
                denthang = 12;
            }
            else
            {
                denngay = Convert.ToInt16(txtDenngay.Text.Substring(0, 2));
                denthang = Convert.ToInt16(txtDenngay.Text.Substring(3, 2));
            }
            @In form_in = new @In();
            form_in.Show();
        }
    }
}