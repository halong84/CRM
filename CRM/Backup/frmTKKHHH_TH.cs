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
    public partial class frmTKKHHH_TH : Form
    {
        public static string  cn = "";
        
        public frmTKKHHH_TH()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            
            DateTime dtCurrentTime = DateTime.Now;
            
        }

       

        private void layCN()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT macn,tencn from Chinhanh order by macn";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbChiNhanh.DataSource = dt;
            cbChiNhanh.DisplayMember = "tencn";
            cbChiNhanh.ValueMember = "macn";

            cbChiNhanh.SelectedValue = frmMain.cn;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 15;
            cn = cbChiNhanh.SelectedValue.ToString();
           

            @In form_in = new @In();
            form_in.Show();
        }

       

       

        private void frmTKKHHH_TH_Load(object sender, EventArgs e)
        {
            if (frmMain.cn == "4800")
            {
                cbChiNhanh.Enabled = true;
            }
            else
            {
                cbChiNhanh.Enabled = false;
            }

            layCN(); 
        }
    }
}