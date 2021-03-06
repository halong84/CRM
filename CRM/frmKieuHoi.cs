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
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmKieuHoi : Form
    {
        string FNgay, TNgay;
        public frmKieuHoi()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FNgay = "01/" + dtpFrom.Text;
            TNgay = "01/" + dtpTo.Text;
            load_KHKieuHoi();
            btnAdd.Enabled = true;
            btnModify.Enabled = false;
            btnDel.Enabled = false;
        }
        private void load_KHKieuHoi()
        {
            dgvDanhsach.Refresh();
            DataTable dt = new DataTable();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            String strCmd = "";
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            
            col = new DataColumn("Mã khách hàng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số điện thoại", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại NT", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số tiền", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số lần", typeof(string));
            dtDanhsach.Columns.Add(col);            
            strCmd = "select makh,hoten,diachi,cmnd,sdt,loaint,SUM(sotien) as sotien,COUNT(hoten) as solan from CHUYENTIENN where left(makh,4)='" + Thongtindangnhap.macn + "' and convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' group by makh,hoten,diachi,cmnd,sdt,loaint order by solan desc";
            //Tim theo CMND
            if(textBox4.Text!="")
                strCmd = "select makh,hoten,diachi,cmnd,sdt,loaint,SUM(sotien) as sotien,COUNT(hoten) as solan from CHUYENTIENN where left(makh,4)='" + Thongtindangnhap.macn + "' and convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and cmnd ='"+textBox4.Text+"' group by makh,hoten,diachi,cmnd,sdt,loaint order by solan desc";
            if(textBox2.Text!="")
                strCmd = "select makh,hoten,diachi,cmnd,sdt,loaint,SUM(sotien) as sotien,COUNT(hoten) as solan from CHUYENTIENN where left(makh,4)='" + Thongtindangnhap.macn + "' and convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and hoten like N'%" + textBox2.Text + "%' group by makh,hoten,diachi,cmnd,sdt,loaint order by solan desc";
            if (txtFrom.Text != "")
            { 
                if(txtTo.Text=="")
                    strCmd="select makh,hoten,diachi,cmnd,sdt,loaint,SUM(sotien) as sotien,COUNT(hoten) as solan from CHUYENTIENN where left(makh,4)='" + Thongtindangnhap.macn + "' and convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and sotien>="+Convert.ToDecimal(txtFrom.Text)+" group by makh,hoten,diachi,cmnd,sdt,loaint order by solan desc";
                else
                    strCmd = "select makh,hoten,diachi,cmnd,sdt,loaint,SUM(sotien) as sotien,COUNT(hoten) as solan from CHUYENTIENN where left(makh,4)='" + Thongtindangnhap.macn + "' and convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and sotien>=" + Convert.ToDecimal(txtFrom.Text) + " and sotien<="+Convert.ToDecimal(txtTo.Text)+" group by makh,hoten,diachi,cmnd,sdt,loaint order by solan desc";
            }           
            
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            int iRows = dt.Rows.Count;
            if (iRows > 0)
            {
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dtDanhsach.NewRow();
                        row[0] = i + 1;                       
                        row[1] = dt.Rows[i]["Makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["cmnd"].ToString();
                        row[4] = dt.Rows[i]["diachi"].ToString();
                        row[5] = dt.Rows[i]["sdt"].ToString();
                        row[6] = dt.Rows[i]["loaint"].ToString();
                        row[7] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["sotien"].ToString()));
                        row[8] = dt.Rows[i]["solan"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
            }
                dgvDanhsach.DataSource = dtDanhsach;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            txtMAKH.Text = Thongtindangnhap.macn.ToString() + "000000000";
            txtHoten.Enabled = true;
            txtCMND.Enabled = true;
            txtDiachi.Enabled = true;
            txtSDT.Enabled = true;
            txtHoten.Text = "";
            txtCMND.Text = "";
            txtDiachi.Text = "";
            txtSDT.Text = "";
        }

        private void dgvDanhsach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMAKH.Enabled = false;
            txtHoten.Enabled = false;
            txtCMND.Enabled = true;
            txtDiachi.Enabled = true;
            txtSDT.Enabled = true;
            btnAdd.Enabled =true;
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            try
            {

                txtMAKH.Text = dgvDanhsach.CurrentRow.Cells["Mã khách hàng"].Value.ToString();
                txtHoten.Text = dgvDanhsach.CurrentRow.Cells["Họ tên"].Value.ToString();
                txtCMND.Text = dgvDanhsach.CurrentRow.Cells["CMND"].Value.ToString();
                txtDiachi.Text = dgvDanhsach.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                txtSDT.Text = dgvDanhsach.CurrentRow.Cells["Số điện thoại"].Value.ToString();
                txtSoTien.Text = dgvDanhsach.CurrentRow.Cells["Số tiền"].Value.ToString();
                txtLoaiNT.Text = dgvDanhsach.CurrentRow.Cells["Loại NT"].Value.ToString();
                txtSolan.Text = dgvDanhsach.CurrentRow.Cells["Số lần"].Value.ToString();               
            }
            catch { }
        }

        private void frmKieuHoi_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnModify.Enabled = false;
            btnDel.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                //Them moi khach hang kieu hoi
               string sCommand = "insert into CHUYENTIENN(THANG,MAKH,HOTEN,CMND,DIACHI,SDT,loaichuyentien,loaint,sotien) values('"+dtpTo.Text+"','" + txtMAKH.Text + "',N'" + txtHoten.Text + "','" + txtCMND.Text + "',N'" + txtDiachi.Text + "','" + txtSDT.Text + "',1,'USD',0)";

                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                MessageBox.Show("Đã lưu!");
                load_KHKieuHoi();

            }
            catch
            //Cap nhat khach hang kieu hoi
            {
                string sCommand = "Update Chuyentienn set cmnd ='" + txtCMND.Text + "',diachi=N'" + txtDiachi.Text +"',sdt='" + txtSDT.Text + "' where hoten =N'" + txtHoten.Text + "' and left(makh,4)='"+Thongtindangnhap.macn+"' and loaint='"+txtLoaiNT.Text+"'";               

                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                MessageBox.Show("Đã cập nhật!");
                load_KHKieuHoi();
            }
            btnAdd.Enabled = true;
            btnModify.Enabled = true;
            btnDel.Enabled = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            String sCommand = "delete chuyentienn where hoten=N'" + txtHoten.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();
            load_KHKieuHoi();
            txtMAKH.Text = "";
            txtMAKH.Enabled = false;
            txtHoten.Text = "";
            txtHoten.Enabled = false;
            txtCMND.Text="";
            txtCMND.Enabled=false;
            txtDiachi.Text = "";
            txtDiachi.Enabled=false;
            txtSDT.Text="";
            txtSDT.Enabled=false;
            btnModify.Enabled = false;
            btnDel.Enabled = false;
            btnAdd.Enabled = true;
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            load_KHKieuHoi();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            load_KHKieuHoi();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            load_KHKieuHoi();
        }

    }
}