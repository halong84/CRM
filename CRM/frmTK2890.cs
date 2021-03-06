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
    public partial class frmTK2890 : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
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
                //cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
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
                    strCmd = "delete Ketqua2890 where macn='" + cbCN.Text + "' and thang = '" + dtpThang.Text + "'";
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
            SqlCommand cmd = new SqlCommand("rpt2890", DataAccess.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cn", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,cn));
            cmd.Parameters.Add(new SqlParameter("@thang",SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,dtpThang.Text));
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Da tao xong bao cao!");
        }
    }
}