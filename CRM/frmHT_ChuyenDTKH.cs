
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmHT_ChuyenDTKH : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public frmHT_ChuyenDTKH()
        {
            InitializeComponent();
        }

        private void frmHT_ChuyenDTKH_Load(object sender, EventArgs e)
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //Chuyen cac khach hang co tai khoan tien gui tuong ung voi doi tuong khach hang
            String sCmd = "";
            DataTable dt = new DataTable();
            dt.Clear();
            
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
               sCmd="select * from khachhang";
            }
            else
              sCmd="select * from khachhang where macn='"+cbCN.Text+"'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Dua du lieu vao bang lich su diem
                try
                {
                    DataTable dt1 = new DataTable();
                    
                    if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
                    {
                        sCmd = "select * from sktiengui where makh='" + dt.Rows[i]["makh"].ToString() + "'";
                    }
                    else
                        sCmd = "select * from sktiengui where makh='" + dt.Rows[i]["makh"].ToString() + "' and left(makh,4)='"+cbCN.Text+"'";
                    
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    dt1.Clear();
                    new SqlDataAdapter(sCmd, DataAccess.conn).Fill(dt1);
                    DataAccess.conn.Close();
                    if (dt1.Rows.Count > 0)
                    {
                        if (dt.Rows[i]["loaikh"].ToString() == "1")
                        {
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                //Doi tuong huong luong tu ngan sach nha nuoc
                                if (dt1.Rows[j]["sotk"].ToString().Substring(4, 3) == "215")
                                {
                                    sCmd = "update khachhang set doituongkh='11' where makh='" + dt1.Rows[j]["makh"].ToString() + "'";
                                    continue;
                                }
                                //Doi tuong huong luong tu bao hiem xa hoi
                                if (dt1.Rows[j]["sotk"].ToString().Substring(4, 3) == "208")
                                {
                                    sCmd = "update khachhang set doituongkh='12' where makh='" + dt1.Rows[j]["makh"].ToString() + "'";
                                    continue;
                                }
                                //Doi tuong la hoc sinh sinh vien
                                if (dt1.Rows[j]["sotk"].ToString().Substring(4, 3) == "220")
                                {
                                    sCmd = "update khachhang set doituongkh='15' where makh='" + dt1.Rows[j]["makh"].ToString() + "'";
                                    continue;
                                }
                                sCmd = "update khachhang set doituongkh='16' where makh='" + dt1.Rows[j]["makh"].ToString() + "'";
                               
                            }
                        }
                        else
                        {
                            sCmd = "update khachhang set doituongkh='25' where makh='" + dt.Rows[i]["makh"].ToString() + "'";
                        }

                    }
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCmd, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    
                }
                catch
                {

                }
            }
            MessageBox.Show("Đã chuyển xong!");

        }

    }
}