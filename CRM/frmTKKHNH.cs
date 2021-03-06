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
    public partial class frmTKKHNH : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string tungay = "", denngay = "", cn = "",loaibc="";
        public frmTKKHNH()
        {
            InitializeComponent();

            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Tất cả";
            dt.Rows.InsertAt(first_row, 0);

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "MACN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin =38;
            tungay = "01/" + dtpFrom.Text;
            denngay = "01/" + dtpTo.Text;
            cn = cbCN.Text;
            if (cn == "Tất cả")
                cn = "9999";
            if (optTatca.Checked == true)
                loaibc = "1";
            if (optWU.Checked == true)
                loaibc = "2";
            if (optKhac.Checked == true)
                loaibc = "3";
            @In form_in = new @In();
            form_in.Show();

        }

        private void frmTKKHNH_Load(object sender, EventArgs e)
        {
            cbCN.Text = Thongtindangnhap.macn;
        }
    }
}