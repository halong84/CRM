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
using Excel = Microsoft.Office.Interop.Excel;
using N_MicrosoftExcelClient;

namespace CRM
{
    public partial class frmTK_BATD : Form
    {
        public static string tungay, denngay, chinhanh, cbtd, lhbh;
        public frmTK_BATD()
        {
            InitializeComponent();
        }

        private void frmTK_BATD_Load(object sender, EventArgs e)
        {
            layLoaiHinhBaoHiem();
        }
        private void layLoaiHinhBaoHiem()
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            sCommand = "SELECT * from dmlhbh order by MALOAIHINH desc";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbLHBH.DataSource = dt;
            cbLHBH.DisplayMember = "Ten";
            cbLHBH.ValueMember = "maloaihinh";            

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 35;
            tungay = dtpFrom.Text.Substring(3, 2) + "/" + dtpFrom.Text.Substring(0, 2) + "/" + dtpFrom.Text.Substring(6, 4);
            denngay = dtpTo.Text.Substring(3, 2) + "/" + dtpTo.Text.Substring(0, 2) + "/" + dtpTo.Text.Substring(6, 4);
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
                chinhanh = "9999";
            else
                chinhanh = cbCN.Text;
            if (cbLHBH.Text == "")
                lhbh = "";
            else
                lhbh = cbLHBH.SelectedValue.ToString();
            cbtd = txtCBTD.Text;
            @In form_in = new @In();
            form_in.Show();  
        }
    }
}