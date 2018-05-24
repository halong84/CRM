using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CRM
{
    public partial class frmTKKHNH : Form
    {
        public static string tungay = "", denngay = "", cn = "",loaibc="";
        public frmTKKHNH()
        {
            InitializeComponent();
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
            cbCN.Text = frmMain.cn;
        }
    }
}