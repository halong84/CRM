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
    public partial class frmKSKhachHang : Form
    {
        public frmKSKhachHang()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            layDoiTuongKH();
        }

        private void frmKSKhachHang_Load(object sender, EventArgs e)
        {

        }
        private void layDoiTuongKH()
        {
            System.Data.DataTable dtkh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("Mã", typeof(int));
            dtkh.Columns.Add(col);
            col = new DataColumn("Tên đối tượng khách hàng", typeof(string));
            dtkh.Columns.Add(col);
            col = new DataColumn("Số lượng", typeof(string));
            dtkh.Columns.Add(col);
            col = new DataColumn("Số tiền(Triệu đồng)", typeof(string));
            dtkh.Columns.Add(col);
           
             String sCommand="";    
            DataTable dt = new DataTable();
            if(optCN.Checked==true)
                sCommand = "SELECT doituongkh.ma,doituongkh.ten,kskhachhang.sokh,kskhachhang.sotien from doituongkh left join kskhachhang on doituongkh.ma=kskhachhang.ma and kskhachhang.macn='" + cbCN.Text + "' where TenNhomDT=N'Cá nhân' order by doituongkh.ma";
            else
                sCommand = "SELECT doituongkh.ma,doituongkh.ten,kskhachhang.sokh,kskhachhang.sotien from doituongkh left join kskhachhang on doituongkh.ma=kskhachhang.ma and kskhachhang.macn='" + cbCN.Text + "' where TenNhomDT<>N'Cá nhân'order by doituongkh.ma";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            int iRows = dt.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dtkh.NewRow();
                    if((dt.Rows[i]["Ma"].ToString()=="1") || (dt.Rows[i]["Ma"].ToString()=="2") || (dt.Rows[i]["Ma"].ToString()=="3") || (dt.Rows[i]["Ma"].ToString()=="4") || (dt.Rows[i]["Ma"].ToString()=="32"))
                    continue;
                    row[0] = dt.Rows[i]["Ma"].ToString();
                    row[1] = dt.Rows[i]["ten"].ToString();
                    if(dt.Rows[i]["sokh"].ToString()=="")
                        row[2] =0;
                    else    
                        row[2] = Convert.ToDecimal(dt.Rows[i]["sokh"].ToString());
                    dtkh.Rows.Add(row);
                    if (dt.Rows[i]["sotien"].ToString() == "")
                        row[3] = 0;
                    else
                        row[3] = Convert.ToDecimal(dt.Rows[i]["sotien"].ToString());
                    dtkh.Rows.Add(row);
                    
                 }
                catch { };

              }
              dataGridViewX1.DataSource = dtkh;
              dataGridViewX1.Columns[0].Visible=true;
              dataGridViewX1.Columns[1].Width=300;
              dataGridViewX1.Columns[0].ReadOnly = true;
              dataGridViewX1.Columns[1].ReadOnly = true;
                
            }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sCommand = "";
            for (int i = 0; i < dataGridViewX1.RowCount; i++)
            {
                try
                {
                    sCommand = "insert into KSKhachHang(ma,macn,sokh,sotien) values('" + dataGridViewX1.Rows[i].Cells[0].Value.ToString() + "','" + cbCN.Text + "'," + Convert.ToDecimal(dataGridViewX1.Rows[i].Cells[2].Value.ToString()) + "," + Convert.ToDecimal(dataGridViewX1.Rows[i].Cells[3].Value.ToString()) + ")";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
                catch
                {
                    sCommand = "update KSKhachHang set sokh=" + Convert.ToDecimal(dataGridViewX1.Rows[i].Cells[2].Value.ToString()) + ",sotien=" + Convert.ToDecimal(dataGridViewX1.Rows[i].Cells[3].Value.ToString()) + " where ma='" + dataGridViewX1.Rows[i].Cells[0].Value.ToString() + "' and macn='" + cbCN.Text + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    
                }
            }
            MessageBox.Show("Đã lưu xong!");
        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Kiem tra du lieu nhap vao phai la so
            DataGridViewCell cuCell = dataGridViewX1.CurrentCell;
            string mainStr = dataGridViewX1.CurrentCell.Value.ToString();
            if (cuCell.ColumnIndex == 2)
            {
                for (int scan = 0; scan < mainStr.Length; scan++)
                {
                    if (Char.IsDigit(mainStr[scan])) { }
                    else
                    {
                        dataGridViewX1.CurrentCell.Value = 0;
                        dataGridViewX1.ClearSelection();
                        dataGridViewX1.CurrentCell = cuCell;
                        dataGridViewX1.CurrentCell.Selected = true;
                        break;
                    }
                }
            }
        }

        private void dataGridViewX1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //Kiem tra du lieu nhap vao phai la so
            DataGridViewCell cuCell = dataGridViewX1.CurrentCell;
            string mainStr = dataGridViewX1.CurrentCell.Value.ToString();
            if (cuCell.ColumnIndex == 2)
            {
                for (int scan = 0; scan < mainStr.Length; scan++)
                {
                    if (Char.IsDigit(mainStr[scan])) { }
                    else
                    {
                        dataGridViewX1.CurrentCell.Value = 0;
                        dataGridViewX1.ClearSelection();
                        dataGridViewX1.CurrentCell = cuCell;
                        dataGridViewX1.CurrentCell.Selected = true;
                        break;
                    }
                }
            }
        }

        private void optCN_CheckedChanged(object sender, EventArgs e)
        {
            
        }
           
        }
}
