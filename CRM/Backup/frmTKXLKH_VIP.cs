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
    public partial class frmTKXLKH_VIP : Form
    {
        public static string tungay = "", denngay = "", cn = "";
        public static int loaikh = 0, dinhtinh = 0, pheduyet = 0;
        
        public frmTKXLKH_VIP()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            
            DateTime dtCurrent = DateTime.Now;
            dtpFrom.CustomFormat = "MM/yyyy";
            //dtpFrom.Value = dtCurrentTime.AddMonths(-1);
            dtpTo.CustomFormat = "MM/yyyy";
            //dtpTo.Value = dtCurrentTime.AddMonths(-1);
            if (dtCurrent.Month == 1)
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
            }
            else
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
            }
        }

        private void frmTKXLKH_VIP_Load(object sender, EventArgs e)
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
            CRM.frmMain.manhinhin = 28;
            cn = cbChiNhanh.SelectedValue.ToString();
            tungay = dtpFrom.Text;
            denngay = dtpTo.Text;
            if (optCN.Checked == true)
            {
                loaikh = 1;
            }
            else
            {
                loaikh = 2;
            }
            //Phan loai theo dinh tinh
            if (optDinhtinh.Checked == true)
            {
                dinhtinh = 1;
            }
            //Phan loai theo dinh luong
            if (optDinhluong.Checked == true)
            {
                dinhtinh = 0;
            }
            //Phan loai theo dinh tinh va dinh luong
            if (optTatca.Checked == true)
            {
                dinhtinh = 3;
            }
            if (optDTDL.Checked == true)
            {
                dinhtinh = 2;
            }
            if (optPheduyet.Checked == true)
            {
                pheduyet = 1;
            }
            else
            {
                pheduyet = 0;
            }
            
            @In form_in = new @In();
            form_in.Show();
        }
    }
}