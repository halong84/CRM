using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CRM
{
    public partial class frmHH_TKVIPCT : Form
    {
        public static string tungay = "", denngay = "",  cn = "";
        public frmHH_TKVIPCT()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 23;
            tungay = "01/" + dtpFrom.Text;
            denngay = "01/" + dtpTo.Text;
            cn = cbCN.Text;
            if(cn=="Tất cả")
                cn="9999";
            @In form_in = new @In();
            form_in.Show();

        }

        private void frmHH_TKVIPCT_Load(object sender, EventArgs e)
        {
            cbCN.Text = "Tất cả";
        }
    }
}