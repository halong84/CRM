using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CRM
{
    public partial class frmTK19892b : Form
    {
        public static string cn = "";
        public frmTK19892b()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 27;

            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                cn = "9999";
            }
            else
                cn = cbCN.Text;           
            @In form_in = new @In();
            form_in.Show();    
        }
    }
}