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
    public partial class frmCSKH_TKQT : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string tungay = "", denngay = "", macn = "", loaibc = "",quatang="";
        
        public frmCSKH_TKQT()
        {
            InitializeComponent();
        }

        private void frmCSKH_TKQT_Load(object sender, EventArgs e)
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

            layQT();
        }
        private void layQT()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * from cauhinhthuong where macn='"+Thongtindangnhap.macn+"'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbQT.DataSource = dt;
            cbQT.DisplayMember = "ten";
            cbQT.ValueMember = "ma";
            cbQT.DataSource = dt;
        }

        private void optTH_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optTH_Click(object sender, EventArgs e)
        {
            if (optTH.Checked == true)
                loaibc = "1";
            else
                loaibc = "2";
        }

        private void optCT_Click(object sender, EventArgs e)
        {
            if (optCT.Checked == true)
                loaibc = "2";
            else
                loaibc = "1";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            frmMain.manhinhin = 31;
            if((cbCN.Text=="")||(cbCN.Text=="Tất cả"))
                macn="9999";
            else
                macn=cbCN.Text;
            if (optTH.Checked == true)
                loaibc = "1";
            else
                loaibc = "2";
            if (cbQT.Text == "")
                quatang = "";
            else
                quatang = cbQT.Text;
            tungay = dtpFrom.Text.Substring(3, 2) + "/" + dtpFrom.Text.Substring(0, 2) + "/" + dtpFrom.Text.Substring(6, 4);
            denngay = dtpTo.Text.Substring(3, 2) + "/" + dtpTo.Text.Substring(0, 2) + "/" + dtpTo.Text.Substring(6, 4);
            @In form_in = new @In();
            form_in.Show();
        }

    }
}