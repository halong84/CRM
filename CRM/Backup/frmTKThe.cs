using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CRM
{
    public partial class frmTKThe : Form
    {
        public static string cn = "";
        public frmTKThe()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 33;

            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                cn = "9999";
            }
            else
                cn = cbCN.Text;
            @In form_in = new @In();
            form_in.Show();    
        }

        private void frmTKThe_Load(object sender, EventArgs e)
        {
           
            if (CRM.frmDangnhap.macn != "4800")
            {
                cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
            else
                cbCN.Text = "4800";
        }
    }
}