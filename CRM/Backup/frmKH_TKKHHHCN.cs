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
    public partial class frmKH_TKKHHHCN : Form
    {
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
            layLoaiKH();
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