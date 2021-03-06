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
    public partial class frmHT_KCCSKH : Form
    {
        public frmHT_KCCSKH()
        {
            InitializeComponent();
        }

        private void btnKetchuyen_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT Makh,diem from diem_cn";
            
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                
                //Dua du lieu vao bang lich su diem
                try
                {
                    string ngaypheduyet;
                    ngaypheduyet = DateTime.Now.ToString().Substring(3, 2) + "/" + DateTime.Now.ToString().Substring(0, 2) + "/" + DateTime.Now.ToString().Substring(6, 4);
                    sCommand = "INSERT INTO Lichsudiem(MAKH,THANG,CCY,DIEM,PHEDUYET,NGAYPHEDUYET,NGUOIPHEDUYET) Values ('" + dt.Rows[i]["MAKH"].ToString()+ "','" + dtpThang.Text + "','000'," + Convert.ToDecimal(dt.Rows[i]["Diem"].ToString()) + ",1,'"+ngaypheduyet+"','"+Thongtindangnhap.user_id+"')";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }

                    sCommand = "Update Lichsudiem set diem =" + Convert.ToDecimal(dt.Rows[i]["Diem"].ToString()) + ",thang='" + dtpThang.Text + "' where makh='" + dt.Rows[i]["MAKH"].ToString() + "' and CCY='000'";
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
            }
            MessageBox.Show("Đã kết chuyển xong!");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * from a";

            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                //Dua du lieu vao bang lich su diem
                try
                {
                    
                    sCommand = "update diem_cn set diem=" + dt.Rows[i]["diem"].ToString() + " where makh='" + dt.Rows[i]["makh"].ToString() + "'"; 
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch
                {
                   
                }
            }
            MessageBox.Show("Đã cap nhat xong!");
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT a.makh,a.diem,kh.hoten as tenkh,kh.dienthoai1 as sdt,kh.ngaysinh,kh.cmnd as cmt from a,khachhang kh where a.makh=kh.makh and a.diem>0";

            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            dt.Clear();
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                //Dua du lieu vao bang lich su diem
                try
                {
                    String ngaysinh = "";
                    if(dt.Rows[i]["ngaysinh"].ToString()!="")
                    ngaysinh = dt.Rows[i]["ngaysinh"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(6, 4);

                    sCommand = "insert diem_cn(makh,tenkh,sdt,ngaysinh,cmt,diem,ngaycapnhat,gui,ngaygui,ma) values('" + dt.Rows[i]["makh"].ToString() + "','"+CRM.frmCSKH_PheDuyet.VietNamese2English(dt.Rows[i]["tenkh"].ToString())+"','"+dt.Rows[i]["sdt"].ToString()+"','"+ngaysinh+"','"+dt.Rows[i]["cmt"].ToString()+"',"+Convert.ToDecimal(dt.Rows[i]["diem"].ToString())+",'01/2013','F','','"+dt.Rows[i]["makh"].ToString().Substring(0,4)+"')";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch
                {

                }
            }
            MessageBox.Show("Đã insert xong!");
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * from A";

            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            dt.Clear();
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                //Dua du lieu vao bang lich su diem
                try
                {

                    sCommand = "insert khachhang(makh,hoten,loaikh,macn) values('" + dt.Rows[i]["makh"].ToString() + "',N'" + dt.Rows[i]["ten"].ToString() + "',1,'4813')";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch
                {

                }
            }
            MessageBox.Show("Đã insert xong!");
        }

      
    }
}