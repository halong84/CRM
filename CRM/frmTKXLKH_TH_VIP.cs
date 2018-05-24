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
    public partial class frmTKXLKH_TH_VIP : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string tungay = "", denngay = "", cn = "";

        public frmTKXLKH_TH_VIP()
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

        private void frmTKXLKH_TH_VIP_Load(object sender, EventArgs e)
        {
            if (Thongtindangnhap.macn == Thongtindangnhap.ma_hoi_so)
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
            DataTable dt = cnbus.DANH_SACH_CHI_NHANH();

            //cbChiNhanh.DataSource = dt;
            cbChiNhanh.DisplayMember = "TENCN";
            cbChiNhanh.ValueMember = "MACN";
            cbChiNhanh.DataSource = dt;
            cbChiNhanh.SelectedValue = Thongtindangnhap.macn;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 27;
            cn = cbChiNhanh.SelectedValue.ToString();
            tungay = dtpFrom.Text;
            denngay = dtpTo.Text;

            @In form_in = new @In();
            form_in.Show();
        }

        private void llbXeploai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CRM.frmMain.manhinhin = 29;
            cn = cbChiNhanh.SelectedValue.ToString();
            tungay = dtpFrom.Text;
            denngay = dtpTo.Text;

            @In form_in = new @In();
            form_in.Show();
        }
    }
}