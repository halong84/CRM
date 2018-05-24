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
    public partial class frmKH_TKKHTN : Form
    {
        TinhBUS t_bus = new TinhBUS();
        HuyenBUS h_bus = new HuyenBUS();
        XaBUS x_bus = new XaBUS();

        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string cn = "", loaikh = "", nhomDT = "", doituongKH = "", tinh = "", huyen = "", xa = "";
        public static int tungay, tuthang, denngay, denthang;
        ArrayList arrListNhomDT, arrListDoituongKH;

        public frmKH_TKKHTN()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbCN.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLoaiKH.DropDownStyle = ComboBoxStyle.DropDownList;

            cbbNhomDT.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbDoituongKH.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbNhomDT.Enabled = false;
            cbbDoituongKH.Enabled = false;

            cbTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbHuyen.DropDownStyle = ComboBoxStyle.DropDownList;
            cbXa.DropDownStyle = ComboBoxStyle.DropDownList;

            chkNgay.Checked = true;

            txtNhomDT.Visible = false;
            txtDoituongKH.Visible = false;
        }

        private void frmKH_TKKHTN_Load(object sender, EventArgs e)
        {
            if (Thongtindangnhap.macn == Thongtindangnhap.ma_hoi_so)
            {
                cbbCN.Enabled = true;
            }
            else
            {
                cbbCN.Enabled = false;
            }

            layCN();
            layLoaiKH();
            layTinh();
            layHuyen();
            layXa();
            //txtTungay.Text = DateTime.Now.ToString().Substring(0, 5);
            //txtDenngay.Text = DateTime.Now.ToString().Substring(0, 5);

            txtNhomDT.Text = "";
            txtDoituongKH.Text = "";
        }

        private void layCN()
        {
            DataTable dt = cnbus.DANH_SACH_CHI_NHANH();

            //cbbCN.DataSource = dt;
            cbbCN.DisplayMember = "TENCN";
            cbbCN.ValueMember = "MACN";
            cbbCN.DataSource = dt;
            cbbCN.SelectedValue = Thongtindangnhap.macn;

        }

        private void layLoaiKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT maloai,tenloai from dmloaikhachhang";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "Tenloai";
            cbLoaiKH.ValueMember = "Maloai";
            cbLoaiKH.DataSource = dt;
        }

        private void layNhomDT()
        {
            arrListNhomDT = new ArrayList();

            cbbNhomDT.Items.Clear();
            cbbNhomDT.Refresh();
            String sCommand = "";
            DataTable dt = new DataTable();
            if (cbLoaiKH.SelectedValue.ToString() == "9999")
            {
                sCommand = "Select distinct NhomDT, TennhomDT from DoiTuongKH";
            }

            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    cbbNhomDT.Items.Add(dt.Rows[i]["TennhomDT"].ToString());
                    arrListNhomDT.Add(dt.Rows[i]["NhomDT"].ToString());
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }

            //cbbNhomDT.DataSource = dt;
            //cbbNhomDT.DisplayMember = "TennhomDT";
            //cbbNhomDT.ValueMember = "NhomDT";
            cbbNhomDT.Items.Add("Tất cả");
            arrListNhomDT.Add("9999");

            cbbNhomDT.SelectedIndex = arrListNhomDT.Count - 1;
            txtNhomDT.Text = "9999";
        }

        private void layDoituongKH()
        {
            arrListDoituongKH = new ArrayList();

            cbbDoituongKH.Items.Clear();
            cbbDoituongKH.Refresh();

            String sCommand = "";
            DataTable dt = new DataTable();
            if (txtNhomDT.Text.Trim() == "9999")
            {
                sCommand = "Select Ma, Ten from DoiTuongKH order by Ma";
            }
            else
            {
                sCommand = "Select Ma, Ten from DoiTuongKH where NhomDT='" + txtNhomDT.Text.Trim() + "' order by Ma";
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    cbbDoituongKH.Items.Add(dt.Rows[i]["Ten"].ToString());
                    arrListDoituongKH.Add(dt.Rows[i]["Ma"].ToString());
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }

            //cbbDoituongKH.DataSource = dt;
            //cbbDoituongKH.DisplayMember = "Ten";
            //cbbDoituongKH.ValueMember = "Ma";
            //cbbDoituongKH.Items.Add("Tất cả");
            cbbDoituongKH.Items.Add("Tất cả");
            arrListDoituongKH.Add("9999");

            cbbDoituongKH.SelectedIndex = arrListDoituongKH.Count - 1;
            txtDoituongKH.Text = "9999";
        }

        private void layTinh()
        {
            DataTable dt = t_bus.DANH_SACH_TINH();
            //cbTinh.DataSource = dt;
            cbTinh.DisplayMember = "TENTINH";
            cbTinh.ValueMember = "MATINH";
            cbTinh.DataSource = dt;
            cbTinh.SelectedValue = Thongtindangnhap.ma_tinh_hien_tai;
        }

        private void layHuyen()
        {
            DataTable dt = new DataTable();

            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                dt = h_bus.DANH_SACH_HUYEN_ALL();
            }
            else
            {
                dt = h_bus.DANH_SACH_HUYEN(cbTinh.SelectedValue.ToString());
            }
            //cbHuyen.DataSource = dt;
            cbHuyen.DisplayMember = "TENHUYEN";
            cbHuyen.ValueMember = "MAHUYEN";
            cbHuyen.DataSource = dt;
            cbHuyen.SelectedValue = "9999";
        }

        private void layXa()
        {
            DataTable dt = new DataTable();
            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                if (cbHuyen.SelectedValue.ToString() == "9999")
                {
                    dt = x_bus.DANH_SACH_XA_ALL();
                }
                else
                {
                    dt = x_bus.DANH_SACH_XA(cbHuyen.SelectedValue.ToString());
                }
            }
            else
            {
                if (cbHuyen.SelectedValue.ToString() == "9999")
                {
                    dt = x_bus.DANH_SACH_XA_THEO_TINH(cbTinh.SelectedValue.ToString());
                }
                else
                {
                    dt = x_bus.DANH_SACH_XA(cbHuyen.SelectedValue.ToString());
                }
            }
            //cbXa.DataSource = dt;
            cbXa.DisplayMember = "TENXA";
            cbXa.ValueMember = "MAXA";
            cbXa.DataSource = dt;
            cbXa.SelectedValue = "9999";
        }

        private void cbTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            //layHuyen();
            //layXa();
        }

        private void cbHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //layXa();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 4;
            if (cbLoaiKH.Text == "Tất cả")
            {
                loaikh = "%%";
            }
            else
            {
                loaikh = cbLoaiKH.Text;
            }

            if (cbbNhomDT.Enabled == false)
            {
                nhomDT = "%%";
                doituongKH = "%%";
            }
            else
            {
                if (txtNhomDT.Text.Trim() == "9999")
                {
                    nhomDT = "%%";
                }
                else
                {
                    nhomDT = txtNhomDT.Text.Trim();
                }

                if (txtDoituongKH.Text.Trim() == "9999")
                {
                    doituongKH = "%%";
                }
                else
                {
                    doituongKH = txtDoituongKH.Text.Trim();
                }
            }

            if (cbbCN.SelectedValue.ToString() == "9999")
            {
                cn = "%%";
            }
            else
            {
                cn = cbbCN.SelectedValue.ToString();
            }
            if (cbTinh.SelectedValue.ToString() == "9999")
            {
                tinh = "%%";
            }
            else
            {
                tinh = cbTinh.SelectedValue.ToString();
            }
            if (cbHuyen.SelectedValue.ToString() == "9999")
            {
                huyen = "%%";
            }
            else
            {
                huyen = cbHuyen.SelectedValue.ToString();
            }
            if (cbXa.SelectedValue.ToString() == "9999")
            {
                xa = "%%";
            }
            else
            {
                xa = cbXa.SelectedValue.ToString();
            }
            if (chkNgay.Checked == true)
            {
                tungay = Convert.ToInt16(dtpFrom.Text.Substring(0, 2));
                tuthang = Convert.ToInt16(dtpFrom.Text.Substring(3, 2));
                denngay = Convert.ToInt16(dtpTo.Text.Substring(0, 2));
                denthang = Convert.ToInt16(dtpTo.Text.Substring(3, 2));
            }
            else
            {
                tungay = 1;
                tuthang = 1;
                denngay = 31;
                denthang = 12;
            }

            @In form_in = new @In();
            form_in.Show();
        }

        private void chkNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNgay.Checked == true)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
        }

        private void cbTinh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layHuyen();
        }

        private void cbHuyen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layXa();
        }

        private void cbLoaiKH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbLoaiKH.SelectedValue.ToString() == "9999")
            {
                cbbNhomDT.Enabled = true;
                cbbDoituongKH.Enabled = true;
                layNhomDT();
                layDoituongKH();
            }
            else
            {
                cbbNhomDT.Enabled = false;
                cbbDoituongKH.Enabled = false;
            }
        }

        private void cbbNhomDT_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtNhomDT.Text = arrListNhomDT[cbbNhomDT.Items.IndexOf(cbbNhomDT.Text.Trim())].ToString();
            cbbDoituongKH.Enabled = true;
            layDoituongKH();
        }

        private void cbbDoituongKH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtDoituongKH.Text = arrListDoituongKH[cbbDoituongKH.Items.IndexOf(cbbDoituongKH.Text.Trim())].ToString();
        }
    }
}