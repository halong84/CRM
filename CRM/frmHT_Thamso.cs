using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmHT_Thamso : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        
        public frmHT_Thamso()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            txtMaCN.Enabled = false;
            txtTenCN.Enabled = false;
        }        

        private void frmThamso_Load(object sender, EventArgs e)
        {            
            layDS_CN();
        }

        private void layDS_CN()
        {
            strCmd = "SELECT * FROM HETHONG Where MaCN='" + frmDangnhap.macn + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            if (iRows > 0)
            {
                txtMaCN.Text = dtResult.Rows[0]["MaCN"].ToString();
                txtTenCN.Text = dtResult.Rows[0]["TenCN"].ToString();
                txtDiaChi.Text = dtResult.Rows[0]["diachi"].ToString();
                txtDT.Text = dtResult.Rows[0]["DT"].ToString();
                txtDDImport.Text = dtResult.Rows[0]["DDImport"].ToString();
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            strCmd = "SELECT * FROM HETHONG Where MaCN='" + frmDangnhap.macn + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into HETHONG(MaCN, TenCN,DDIMPORT,diachi,dt) ";
                strCmd += "Values('" + txtMaCN.Text.Trim() + "',N'" + txtTenCN.Text.Trim() + "','" + txtDDImport.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() + "','" + txtDT.Text.Trim() + "')";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }
            else
            {
                strCmd = "Update HETHONG ";
                strCmd += "SET DDImport='" + txtDDImport.Text.Trim() + "',dt='" + txtDT.Text.Trim() + "',diachi=N'" + txtDiaChi.Text.Trim() + "' Where MaCN='" + frmDangnhap.macn + "'";
                //strCmd += "SET MaCN='" + txtMaCN.Text.Trim() + "', TenCN=N'" + txtTenCN.Text.Trim() + "',DDImport='"+txtDDImport.Text.Trim()+"'";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    MessageBox.Show("Đã lưu.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void frmHT_Thamso_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataTable dt = new DataTable();
            String strCmd = "SELECT * FROM HETHONG Where MaCN='" + frmDangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int i = dt.Rows.Count;
            if (i != 0)
            {
                //Thongtindangnhap.macn = dt.Rows[i - 1]["MACN"].ToString();
                frmMain.ddimport = dt.Rows[i - 1]["DDImport"].ToString();
            }
        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}