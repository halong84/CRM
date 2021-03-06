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
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmTKSMS : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
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

            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Tất cả";
            dt.Rows.InsertAt(first_row, 0);

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "MACN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;

            if (Thongtindangnhap.macn != Thongtindangnhap.ma_hoi_so)
            {
                //cbCN.Text = Thongtindangnhap.macn;
                cbCN.Enabled = false;
            }
            optCN.Checked = true;
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
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
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
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
            }
            SqlCommand cmd = new SqlCommand("rptTKSMS", DataAccess.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cn", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cn));
            cmd.Parameters.Add(new SqlParameter("@loaikh", SqlDbType.TinyInt, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, loaikh));
            cmd.Parameters.Add(new SqlParameter("@dt", SqlDbType.TinyInt, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, dthoai));
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
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