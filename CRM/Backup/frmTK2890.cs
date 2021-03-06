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
    public partial class frmTK2890 : Form
    {
        public static string cn = "",thang;
        SqlCommand myCommand;
        public frmTK2890()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
           // String sCommand = "";

            CRM.frmMain.manhinhin = 26;

            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                cn = "9999";
            }
            else
                cn = cbCN.Text;
            thang = dtpThang.Text;
            @In form_in = new @In();
            form_in.Show();    
        }

        private void frmTK2890_Load(object sender, EventArgs e)
        {
           
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
            String strCmd = "",cn="";
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                strCmd = "select * from Ketqua2890 where macn='9999' and thang ='" + dtpThang.Text + "'";
                cn = "9999";

            }
            else
            {
                strCmd = "select * from Ketqua2890 where macn='" + cbCN.Text + "' and thang = '" + dtpThang.Text + "'";
                cn = cbCN.Text;
            }
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
                    strCmd = "delete Ketqua2890 where macn='" + cbCN.Text + "' and thang = '" + dtpThang.Text + "'";
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
            SqlCommand cmd = new SqlCommand("rpt2890", frmMain.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cn", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,cn));
            cmd.Parameters.Add(new SqlParameter("@thang",SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,dtpThang.Text));
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Da tao xong bao cao!");
        }
    }
}