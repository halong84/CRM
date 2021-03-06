using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmTKKHSPDV : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string cn = "",thang="",makh="";
        public static byte loaikh;
        public frmTKKHSPDV()
        {
            InitializeComponent();
        }

        private void frmTKKHSPDV_Load(object sender, EventArgs e)
        {

            DataTable dt = cnbus.DANH_SACH_CHI_NHANH();

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "TENCN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;


            if (Thongtindangnhap.macn != Thongtindangnhap.ma_hoi_so)
            {
                //cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 34;

            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                cn = "9999";
            }
            else
                cn = cbCN.Text;
            if (optCN.Checked == true)
                loaikh = 1;
            if (optDN.Checked == true)
                loaikh = 2;
            if (optNV.Checked == true)
                loaikh = 3;
            if (optKHTL.Checked == true)
                loaikh = 4;
            if (optKHTV.Checked == true)
                loaikh = 5;
            if (txtMakh.Text == "")
                makh = "";
            else
                makh = txtMakh.Text;
            thang = dtpThang.Text;
            @In form_in = new @In();
            form_in.Show();    
        }
    }
}