using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRM
{
    public partial class frmKHChuyen : Form
    {
        public static string makhhh = "";

        public frmKHChuyen()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                //Dua du lieu vao bang GiaoDich
                String ngaychuyen, sCommand;
                makhhh = txtMaKH.Text;
                ngaychuyen = dtpNgaychuyen.Text.Substring(3, 2) + "/" + dtpNgaychuyen.Text.Substring(0, 2) + "/" + dtpNgaychuyen.Text.Substring(6, 4);
                sCommand = "insert into KH_KHTN(makh,makhtn,ngaychuyen) values('" + txtMaKH.Text + "','" + CRM.frmKhachhangTN.makh + "','" + ngaychuyen + "')";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                this.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
        }       
    }
}