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
    public partial class frmKH_TKGD : Form
    {
        public static string cn = "", canbo = "", maKH = "", makh = "", loaikh = "HH", maCN = "";
        
        public frmKH_TKGD()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbCN.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbCB.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmKH_TKGD_Load(object sender, EventArgs e)
        {
            if (frmMain.cn == "4800")
            {
                cbbCN.Enabled = true;
            }
            else
            {
                cbbCN.Enabled = false;
            }

            cbbCB.Visible = false;
            grbLoai.Visible = true;
            txtMaKH.Visible = true;
            btnKH.Visible = true;
            layCN();
            layCanbo();
            maCN = cbbCN.SelectedValue.ToString();
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
            cbbCN.DataSource = dt;
            cbbCN.DisplayMember = "tencn";
            cbbCN.ValueMember = "macn";

            cbbCN.SelectedValue = frmMain.cn;
        }

        private void layCanbo()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            if (cbbCN.SelectedValue.ToString() == "9999")
            {
                sCommand = "SELECT User_ID,TenNV from _User";
            }
            else
            {
                sCommand = "SELECT User_ID,TenNV from _User Where MaCN='" + cbbCN.SelectedValue.ToString() + "' or MaCN='9999'";
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbCB.DataSource = dt;
            cbbCB.DisplayMember = "TenNV";
            cbbCB.ValueMember = "User_ID";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (cbbCN.SelectedValue.ToString() == "9999")
            {
                cn = "%%";
            }
            else
            {
                cn = cbbCN.SelectedValue.ToString();
            }
            if (rdbHH.Checked == true)
            {
                if (rdbKH.Checked == true)
                {
                    frmMain.manhinhin = 11;
                }
                else if (rdbCB.Checked == true)
                {
                    frmMain.manhinhin = 12;
                }
            }
            else if (rdbTN.Checked == true)
            {
                if (rdbKH.Checked == true)
                {
                    frmMain.manhinhin = 13;
                }
                else if (rdbCB.Checked == true)
                {
                    frmMain.manhinhin = 14;
                }
            }
            if (rdbKH.Checked == true)
            {
                if (txtMaKH.Text == "")
                {
                    maKH = "%%";
                }
                else
                {
                    maKH = txtMaKH.Text;
                }
                canbo = "%%";                    
            }
            else if (rdbCB.Checked == true)
            {
                if (cbbCB.SelectedValue.ToString() == "9999")
                {
                    canbo = "%%";
                }
                else
                {
                    canbo = cbbCB.SelectedValue.ToString();
                }
                maKH = "%%";
            }

            @In form_in = new @In();
            form_in.Show();
        }

        private void cbbCN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layCanbo();
            maCN = cbbCN.SelectedValue.ToString();
        }

        private void rdbHH_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbHH.Checked == true)
            {
                loaikh = "HH";
            }
            else if (rdbTN.Checked == true)
            {
                loaikh = "TN";
            }
        }

        private void rdbKH_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbKH.Checked == true)
            {
                cbbCB.Visible = false;
                txtMaKH.Visible = true;
                btnKH.Visible = true;
            }
            else if (rdbCB.Checked == true)
            {
                cbbCB.Visible = true; ;
                txtMaKH.Visible = false;
                btnKH.Visible = false;
            }
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            frmMain.flagSearch = 2;
            frmKH_Search frmSearch = new frmKH_Search();
            frmSearch.ShowDialog();
            txtMaKH.Text = makh;
        }
    }
}