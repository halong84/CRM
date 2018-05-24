using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CRM
{
    public partial class frmCapnhat : Form
    {
        public frmCapnhat()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmCapnhat_Load(object sender, EventArgs e)
        {
            lblOldVer.Text = frmDangnhap.phienban;
            lblNewVer.Text = frmDangnhap.str_update;            
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            Process.Start("ftp://10.131.0.19/CRM/");
            this.Close();
        }
    }
}