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
    public partial class frmTKSMS : Form
    {
        public static string cn="";
        public static int loaikh,dthoai;
        SqlCommand myCommand;
        public frmTKSMS()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 32;           
            cn = cbCN.Text;
            if ((cn == "Tất cả")||(cn==""))
                cn = "9999";
            if (optCN.Checked == true)
                loaikh = 1;
            else
                loaikh = 2;
            if (optDT.Checked == true)
                dthoai= 1;
            else
                dthoai = 0;
            @In form_in = new @In();
            form_in.Show();
        }

        private void frmTKSMS_Load(object sender, EventArgs e)
        {
            optCN.Checked = true;
            if (CRM.frmDangnhap.macn != "4800")
            {
                cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
            else
                cbCN.Text = "4800";
        }

      

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            String strCmd = "", cn = "";
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                if (optCN.Checked == true)
                {
                    if (optDT.Checked == true)
                        strCmd = "select * from KetquaSMS where mabc='9999' and loaikh=1 and dienthoai=1";
                    else
                        strCmd = "select * from KetquaSMS where mabc='9999' and loaikh=1 and dienthoai=0";
                }
                else
                {
                    if (optDT.Checked == true)
                        strCmd = "select * from KetquaSMS where mabc='9999' and loaikh=2 and dienthoai=1";
                    else
                        strCmd = "select * from KetquaSMS where mabc='9999' and loaikh=2 and dienthoai=0";
                }
                cn = "9999";


            }
            else
            {
                if (optCN.Checked == true)
                {
                    if (optDT.Checked == true)
                        strCmd = "select * from KetquaSMS where macn='" + cbCN.Text + "' and loaikh=1 and dienthoai=1";
                    else
                        strCmd = "select * from KetquaSMS where macn='" + cbCN.Text + "' and loaikh=1 and dienthoai=0";
                }
                else
                {
                    if (optDT.Checked == true)
                        strCmd = "select * from KetquaSMS where macn='" + cbCN.Text + "' and loaikh=2 and dienthoai=1";
                    else
                        strCmd = "select * from KetquaSMS where macn='" + cbCN.Text + "' and loaikh=2 and dienthoai=0";
                }
                cn = cbCN.Text;
            }
            if (optCN.Checked == true)
                loaikh = 1;
            else
                loaikh = 2;
            if (optDT.Checked == true)
                dthoai = 1;
            else
                dthoai = 0;
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            if (dt.Rows.Count > 0)
            {
                if (MessageBox.Show("Bao cao nay da duoc tao! Tao lai khong? ", "Tao bao cao ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                else
                //Xoa du lieu cu
                {
                    if (optCN.Checked == true)
                    {
                        if (optDT.Checked == true)
                            strCmd = "delete KetquaSMS where mabc='" + cn + "' and loaikh=1 and dienthoai=1";
                        else
                            strCmd = "delete KetquaSMS where mabc='" + cn + "' and loaikh=1 and dienthoai=0";
                    }
                    else
                    {
                        if (optDT.Checked == true)
                            strCmd = "delete KetquaSMS where mabc='" + cn + "' and loaikh=2 and dienthoai=1";
                        else
                            strCmd = "delete KetquaSMS where mabc='" + cn + "' and loaikh=2 and dienthoai=0";
                    }
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    myCommand = new SqlCommand(strCmd, frmMain.conn);
                    myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
            }
            SqlCommand cmd = new SqlCommand("rptTKSMS", frmMain.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cn", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cn));
            cmd.Parameters.Add(new SqlParameter("@loaikh", SqlDbType.TinyInt, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, loaikh));
            cmd.Parameters.Add(new SqlParameter("@dt", SqlDbType.TinyInt, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, dthoai));
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Da tao xong bao cao!");
        }

        private void optCN_CheckedChanged(object sender, EventArgs e)
        {
            if (optCN.Checked == true)
                loaikh = 1;
            else
                loaikh = 2;
        }

        private void optDN_CheckedChanged(object sender, EventArgs e)
        {
            if (optCN.Checked == true)
                loaikh = 1;
            else
                loaikh = 2;
        }

        private void optDT_CheckedChanged(object sender, EventArgs e)
        {
            if (optDT.Checked == true)
                dthoai = 1;
            else
                dthoai = 0;
        }

        private void optCDT_CheckedChanged(object sender, EventArgs e)
        {
            if (optDT.Checked == true)
                dthoai = 1;
            else
                dthoai = 0;
        }

       
    }
}