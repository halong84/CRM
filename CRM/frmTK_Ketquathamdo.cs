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
    public partial class frmTK_Ketquathamdo : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public frmTK_Ketquathamdo()
        {
            InitializeComponent();
            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Tất cả";
            dt.Rows.InsertAt(first_row, 0);

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "MACN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            LoadPhieuthamdo();
        }
        private void LoadPhieuthamdo()
        {
            DataTable dt = new DataTable();

            String strCon = " 1=1", strCmd = "select * from khachhangphanhoi where";
            if (cbCN.Text != "")
            {
                if ((cbCN.Text != "Tất cả"))
                    strCon = strCon + " and macn='" + cbCN.Text + "'";
            }
            if (optCN.Checked == true)
                strCon = strCon + " and tochuc=0";
            else
                strCon = strCon + " and tochuc=1";
            if (txtCMND.Text != "")
            {
                strCon = strCon + " and khachhangphanhoi.cmnd='" + txtCMND.Text + "'";
            }
            if (txtHoten.Text != "")
            {
                strCon = strCon + " and khachhangphanhoi.hoten like N'%" + txtHoten.Text + "%'";
            }
            if (txtDienthoai.Text != "")
            {
                strCon = strCon + " and khachhangphanhoi.dienthoai='" + txtDienthoai.Text + "'";
            }


            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            dt.Clear();
            DataAccess.conn.Open();
            strCmd = strCmd + strCon;
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã khách hàng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));
            dskh.Columns.Add(col);
            if (optCN.Checked == true)
            {
                col = new DataColumn("CMND", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Giới tính", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tuổi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Trình độ", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Thu nhập", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Sở thích", typeof(string));
                dskh.Columns.Add(col);
            }

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();

                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["MAKH"].ToString();
                    row[2] = dt.Rows[i]["hoten"].ToString();
                    row[3] = dt.Rows[i]["diachi"].ToString();
                    row[4] = dt.Rows[i]["dienthoai"].ToString();
                    row[5] = dt.Rows[i]["email"].ToString();
                    if (optCN.Checked == true)
                    {
                        row[6] = dt.Rows[i]["cmnd"].ToString();
                        if (dt.Rows[i]["Gioitinh"].ToString() == "True")
                            row[7] = "Nam";
                        else
                            row[7] = "Nữ";
                        row[8] = dt.Rows[i]["tuoi"].ToString();
                        row[9] = dt.Rows[i]["trinhdo"].ToString();
                        row[10] = dt.Rows[i]["thunhap"].ToString();
                        row[11] = dt.Rows[i]["sothich"].ToString();
                    }
                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX1.DataSource = dskh;
            dataGridViewX1.Columns[1].Visible = false;
        }
        private void frmTK_Ketquathamdo_Load(object sender, EventArgs e)
        {
            if (CRM.frmDangnhap.macn != "4800")
            {
                cbCN.Text = CRM.frmDangnhap.macn;
                cbCN.Enabled = false;
            }
        }

        private void dataGridViewX1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lay_kqthamdochitiet(dataGridViewX1.CurrentRow.Cells["Mã khách hàng"].Value.ToString());
        }
        private void lay_kqthamdochitiet(string s)
        {
            String strCmd = "";
            if(optCN.Checked==true)
                strCmd="select KETQUATHAMDO.*,KETQUATHAMDOCT.MACT,KETQUATHAMDOCT.LUACHON,CHITIETPHIEUTHAMDO.CHITIETHANMUC from KETQUATHAMDO,KETQUATHAMDOCT,KHACHHANGPHANHOI,CHITIETPHIEUTHAMDO where KHACHHANGPHANHOI.MAKH=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and CHITIETPHIEUTHAMDO.MACT=KETQUATHAMDOCT.mact and  KHACHHANGPHANHOI.MAKH='" + s + "'";
            else
                strCmd = "select KETQUATHAMDO.*,KETQUATHAMDOCT.MACT,KETQUATHAMDOCT.LUACHON,CHITIETPHIEUTHAMDOTC.CHITIETHANMUC from KETQUATHAMDO,KETQUATHAMDOCT,KHACHHANGPHANHOI,CHITIETPHIEUTHAMDOTC where KHACHHANGPHANHOI.MAKH=KETQUATHAMDO.MAKH and KETQUATHAMDO.SOPHIEU=KETQUATHAMDOCT.SOPHIEU and CHITIETPHIEUTHAMDOTC.MACT=KETQUATHAMDOCT.mact and  KHACHHANGPHANHOI.MAKH='" + s + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataTable dt1 = new DataTable();
            dt1.Clear();
            DataAccess.conn.Open();            
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt1);
            
            System.Data.DataTable dsct = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dsct.Columns.Add(col);
            col = new DataColumn("Mã chỉ tiêu", typeof(string));
            dsct.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dsct.Columns.Add(col);
            col = new DataColumn("Lựa chọn", typeof(string));
            dsct.Columns.Add(col);
            int iRows = dt1.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dsct.NewRow();

                    row[0] = i + 1;
                    row[1] = dt1.Rows[i]["MACT"].ToString();
                    row[2] = dt1.Rows[i]["chitiethanmuc"].ToString(); ;
                    if (dt1.Rows[i]["MACT"].ToString().Substring(0, 1) == "A")
                    {
                        if (dt1.Rows[i]["luachon"].ToString() == "1")
                            row[3] = "Không";
                        if (dt1.Rows[i]["luachon"].ToString() == "2")
                            row[3] = "Agribank";
                        if (dt1.Rows[i]["luachon"].ToString() == "3")
                            row[3] = "Ngân hàng khác";
                        if (dt1.Rows[i]["luachon"].ToString() == "4")
                            row[3] = "Cả Agribank và NH khác";                       

                    }
                    if (dt1.Rows[i]["MACT"].ToString().Substring(0, 1) == "B")
                    {
                        if (dt1.Rows[i]["luachon"].ToString() == "1")
                            row[3] = "Hoàn toàn phản đối";
                        if (dt1.Rows[i]["luachon"].ToString() == "2")
                            row[3] = "Nói chung phản đối";
                        if (dt1.Rows[i]["luachon"].ToString() == "3")
                            row[3] = "Không ý kiến";
                        if (dt1.Rows[i]["luachon"].ToString() == "4")
                            row[3] = "Đồng ý";
                        if (dt1.Rows[i]["luachon"].ToString() == "5")
                            row[3] = "Hoàn toàn đồng ý";

                    }
                    if ((dt1.Rows[i]["MACT"].ToString().Substring(0, 1) == "C")||(dt1.Rows[i]["MACT"].ToString().Substring(0, 1) == "D"))
                    {
                        if (dt1.Rows[i]["luachon"].ToString() == "1")
                            row[3] = "Hoàn toàn không quan trọng";
                        if (dt1.Rows[i]["luachon"].ToString() == "2")
                            row[3] = "Không quan trọng";
                        if (dt1.Rows[i]["luachon"].ToString() == "3")
                            row[3] = "Bình thường";
                        if (dt1.Rows[i]["luachon"].ToString() == "4")
                            row[3] = "Quan trọng";
                        if (dt1.Rows[i]["luachon"].ToString() == "5")
                            row[3] = "Rất quan trọng";

                    }
                    if (dt1.Rows[i]["MACT"].ToString().Substring(0, 1) == "E")
                    {
                        if (optCN.Checked == true)
                        {
                            if (dt1.Rows[i]["luachon"].ToString() == "1")
                                row[3] = "Dự thưởng";
                            if (dt1.Rows[i]["luachon"].ToString() == "2")
                                row[3] = "Tặng quà";
                            if (dt1.Rows[i]["luachon"].ToString() == "3")
                                row[3] = "Rút thăm trúng thưởng";
                            if (dt1.Rows[i]["luachon"].ToString() == "4")
                                row[3] = "Tặng tiền mặt";
                            if (dt1.Rows[i]["luachon"].ToString() == "5")
                                row[3] = "Chuyến du lịch";
                            if (dt1.Rows[i]["luachon"].ToString() == "6")
                                row[3] = "Khác";
                        }
                        else
                        {
                            if (dt1.Rows[i]["luachon"].ToString() == "1")
                                row[3] = "Giảm lãi";
                            if (dt1.Rows[i]["luachon"].ToString() == "2")
                                row[3] = "Giảm phí dịch vụ";
                            if (dt1.Rows[i]["luachon"].ToString() == "3")
                                row[3] = "Phục vụ tại tổ chức doanh nghiệp";
                            if (dt1.Rows[i]["luachon"].ToString() == "4")
                                row[3] = "Miễn phí kết nối thanh toán";
                            if (dt1.Rows[i]["luachon"].ToString() == "5")
                                row[3] = "Có chế độ chăm sóc khách VIP";
                            if (dt1.Rows[i]["luachon"].ToString() == "6")
                                row[3] = "Khác";
                        }
                    }                   
                   
                    dsct.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX2.DataSource = dsct;
            dataGridViewX2.Columns[2].Width = 300;
            dataGridViewX2.Columns[3].Width = 150;
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lay_kqthamdochitiet(dataGridViewX1.CurrentRow.Cells["Mã khách hàng"].Value.ToString());
            buttonX2.Enabled = true;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string makh = dataGridViewX1.CurrentRow.Cells["Mã khách hàng"].Value.ToString();
            String strCmd = "select * from ketquathamdo where makh='"+makh+"'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            
            dt.Clear();
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    //Dua du lieu vao bang khachhangphanhoi
                    String sCommand = "delete ketquathamdoct where sophieu ='"+dt.Rows[0]["sophieu"].ToString()+"'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    sCommand = "delete ketquathamdo where makh ='" + makh + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    sCommand = "delete khachhangphanhoi where makh ='" + makh + "'";
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
                }
            }
            MessageBox.Show("Đã xóa!");
            LoadPhieuthamdo();
            buttonX2.Enabled = false;
        }
    }
}