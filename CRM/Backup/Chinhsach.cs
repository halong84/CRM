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
    public partial class Chinhsach : Form
    {
        public static int nhomkh = 1;
        string macs = "";

        public Chinhsach()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Chinhsach_Load(object sender, EventArgs e)
        {
            layNhomKH();
            layChinhsach();
        }

        private void layNhomKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT manhom,tennhom from nhomkhachhang where macn='"+CRM.frmMain.cn+"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbNhomKH.DataSource = dt;
            cbNhomKH.DisplayMember = "Tennhom";
            cbNhomKH.ValueMember = "Manhom";
        }
        private void layChinhsach()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT chinhsach.*,nhomkhachhang.tennhom from Chinhsach,nhomkhachhang where chinhsach.manhom=nhomkhachhang.manhom and chinhsach.macn='" + CRM.frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chính sách", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên nhóm KH", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày kết thúc", typeof(string));
            dskh.Columns.Add(col);
            
            col = new DataColumn("Hiệu lực", typeof(string));           
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["macs"].ToString();

                    row[2] = dt.Rows[i]["ten"].ToString();
                    row[3] = dt.Rows[i]["noidung"].ToString();
                    row[4] = dt.Rows[i]["ghichu"].ToString();
                    row[5] = dt.Rows[i]["tennhom"].ToString();
                    row[6] = dt.Rows[i]["batdau"].ToString().Substring(0,10);
                    row[7] = dt.Rows[i]["ketthuc"].ToString().Substring(0,10);
                    if (dt.Rows[i]["Hieuluc"].ToString() == "True")
                    {
                        row[8] = "Hoạt động";
                    }
                    else
                    {
                        row[8] = "Ngưng hoạt động";
                    }
                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX2.DataSource = dskh;
            dataGridViewX2.Columns[0].ReadOnly = true;
            
            dataGridViewX2.Columns[2].ReadOnly = true;
            dataGridViewX2.Columns[3].ReadOnly = true;
            dataGridViewX2.Columns[4].ReadOnly = true;
            dataGridViewX2.Columns[5].ReadOnly = true;
            dataGridViewX2.Columns[6].ReadOnly = true;
            dataGridViewX2.Columns[7].ReadOnly = false;
            dataGridViewX2.Columns[8].ReadOnly = false;           

            dataGridViewX2.Columns[0].Width = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX2.Columns[1].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cbNhomKH.Enabled = true;
            txtTenCS.Enabled = true;
            txtNoiDung.Enabled = true;
            txtGhiChu.Enabled = true;
            dtpNgaybd.Enabled = true;
            dtpNgaykt.Enabled = true;
            cbHieuluc.Enabled = true;
            cbHieuluc.Text = "Hoạt động";
            btnAdd.Enabled = false;
            macs= DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
            txtmacs.Text = macs;
            cbNhomKH.Text = "";
            txtTenCS.Text = "";
            txtNoiDung.Text = "";
            txtGhiChu.Text = "";
            dtpNgaybd.Value = DateTime.Now;
            dtpNgaykt.Text = "31/12/9998";
            cbHieuluc.Text = "Hoạt động";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Dua du lieu vao bang Chinhsach
                String ngaybd, ngaykt, sCommand;
                int hieuluc = 1;
                if (cbHieuluc.Text == "Hoạt động")
                {
                    hieuluc = 1;
                }
                else
                {
                    hieuluc = 0;
                }
                ngaybd = dtpNgaybd.Text.Substring(3, 2) + "/" + dtpNgaybd.Text.Substring(0, 2) + "/" + dtpNgaybd.Text.Substring(6, 4) ;
                ngaykt = dtpNgaykt.Text.Substring(3, 2) + "/" + dtpNgaykt.Text.Substring(0, 2) + "/" + dtpNgaykt.Text.Substring(6, 4) ;
                sCommand = "insert into chinhsach(macs,ten,noidung,ghichu,Batdau,ketthuc,hieuluc,macn,manhom) values('" + txtmacs.Text + "',N'" + txtTenCS.Text + "',N'" + txtNoiDung.Text + "',N'" + txtGhiChu.Text + "','" + ngaybd + "','" + ngaykt + "',"+hieuluc+",'"+CRM.frmMain.cn+"','" + cbNhomKH.SelectedValue.ToString() + "')";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                layChinhsach();
            }
            catch
            //Cap nhat chinhsach
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }

                String ngaybd, ngaykt, sCommand;
                int hieuluc = 1;
                if (cbHieuluc.Text == "Hoạt động")
                {
                    hieuluc = 1;
                }
                else
                {
                    hieuluc = 0;
                }
                ngaybd = dtpNgaybd.Text.Substring(3, 2) + "/" + dtpNgaybd.Text.Substring(0, 2) + "/" + dtpNgaybd.Text.Substring(6, 4) ;
                ngaykt = dtpNgaykt.Text.Substring(3, 2) + "/" + dtpNgaykt.Text.Substring(0, 2) + "/" + dtpNgaykt.Text.Substring(6, 4) ;
                sCommand = "Update chinhsach set ten=N'" + txtTenCS.Text + "',noidung =N'" + txtNoiDung.Text + "',ghichu=N'" + txtGhiChu.Text + "',Batdau='" + ngaybd + "',ketthuc='" + ngaykt + "',hieuluc=" + hieuluc + " where macs='" + txtmacs.Text + "'";
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                layChinhsach();                
            }
            btnAdd.Enabled = true;
        }

        
        private void layChinhsachCT()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT Chinhsachchitiet.* from chinhsach,Chinhsachchitiet where chinhsach.macs=chinhsachchitiet.macs and chinhsachchitiet.macs='" + txtmacs.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);

            col = new DataColumn("Mã chính sách chi tiết", typeof(string));
            dskh.Columns.Add(col);          

            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tỷ lệ(%)", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);

            
            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["macsct"].ToString();                    
                    row[2] = dt.Rows[i]["noidung"].ToString();
                    row[3] = dt.Rows[i]["tyle"].ToString();
                    row[4] = dt.Rows[i]["ghichu"].ToString();                                      
                    dskh.Rows.Add(row);
                }
                catch { }
            }
            dataGridViewX3.DataSource = dskh;
            dataGridViewX3.Columns[0].Width = 30;
            dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX3.Columns[1].Visible = false;
        }

        private void layChamsoc()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT Chamsoc.* from chinhsach,chamsoc where chinhsach.macs=chamsoc.macs and chamsoc.macs='" + txtmacs.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);

            col = new DataColumn("Mã chăm sóc", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);           

            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["macsoc"].ToString();
                    row[2] = dt.Rows[i]["noidung"].ToString();                   
                    row[3] = dt.Rows[i]["ghichu"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { }
            }
            dataGridViewX1.DataSource = dskh;

            dataGridViewX1.Columns[0].Width = 30;
            dataGridViewX1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridViewX1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX1.Columns[1].Visible = false;
        }       

        private void dataGridViewX2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbNhomKH.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[5].Value.ToString();
                txtTenCS.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[2].Value.ToString();
                txtNoiDung.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[3].Value.ToString();
                txtGhiChu.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[4].Value.ToString();
                dtpNgaybd.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[6].Value.ToString();
                dtpNgaykt.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[7].Value.ToString();
                cbHieuluc.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[8].Value.ToString();
                txtmacs.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[1].Value.ToString();
                cbNhomKH.Enabled = true;
                txtTenCS.Enabled = true;
                txtNoiDung.Enabled = true;
                txtGhiChu.Enabled = true;
                dtpNgaybd.Enabled = true;
                dtpNgaykt.Enabled = true;
                cbHieuluc.Enabled = true;
                cbHieuluc.Enabled = true;
                layChinhsachCT();
                layChamsoc();
            }
            catch { }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewX3.RowCount-1; i++)
            {
                String macsct = "";
                if (dataGridViewX3.Rows[i].Cells[1].Value.ToString() == "")
                {
                    macsct = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + i.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                }
                else
                {
                    macsct = dataGridViewX3.Rows[i].Cells[1].Value.ToString();
                }
                if (dataGridViewX3.Rows[i].Cells[3].Value.ToString() == "")
                {
                    dataGridViewX3.Rows[i].Cells[3].Value = 0;
                }
                try
                {
                    String sCommand = "insert into chinhsachchitiet(macs,macsct,noidung,tyle,ghichu) values('" + txtmacs.Text + "','" + macsct + "',N'" + dataGridViewX3.Rows[i].Cells[2].Value.ToString() + "'," + dataGridViewX3.Rows[i].Cells[3].Value + ", N'" + dataGridViewX3.Rows[i].Cells[4].Value.ToString() + "')";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);                    
                    if (macsct != "")
                    {
                        frmMain.myCommand.ExecuteNonQuery();
                    }
                    frmMain.conn.Close();
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }

                    String sCommand = "update chinhsachchitiet set noidung=N'" + dataGridViewX3.Rows[i].Cells[2].Value.ToString() + "',tyle=" + dataGridViewX3.Rows[i].Cells[3].Value + ",ghichu=N'" + dataGridViewX3.Rows[i].Cells[4].Value.ToString() + "' where macsct='" +dataGridViewX3.Rows[i].Cells[1].Value.ToString() +"'";
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    //if (macsct != "")
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }                
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewX1.RowCount-1; i++)
            {
                String macsoc = "";
                if (dataGridViewX1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    macsoc = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + i.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                }
                else
                    macsoc = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                try
                {
                    String sCommand = "insert into chamsoc(macs,macsoc,noidung,ghichu) values('" + txtmacs.Text + "','" + macsoc + "',N'" + dataGridViewX1.Rows[i].Cells[2].Value.ToString() + "', N'" + dataGridViewX1.Rows[i].Cells[3].Value.ToString() + "')";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    if (macsoc != "")
                    {
                        frmMain.myCommand.ExecuteNonQuery();
                    }
                    frmMain.conn.Close();
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }

                    String sCommand = "update chamsoc set noidung=N'" + dataGridViewX1.Rows[i].Cells[2].Value.ToString() + "',ghichu=N'" + dataGridViewX1.Rows[i].Cells[3].Value.ToString() + "' where macsoc='" + dataGridViewX1.Rows[i].Cells[1].Value.ToString() + "'";
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    if (macsoc != "")
                    {
                        frmMain.myCommand.ExecuteNonQuery();
                    }
                    frmMain.conn.Close();
                }                
            }
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            String sCommand="";
            DataTable dt = new DataTable();
            sCommand = "SELECT * from chinhsachchitiet where chinhsachchitiet.macs='" + txtmacs.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            DataTable dt1 = new DataTable();
            sCommand = "SELECT * from chamsoc where chamsoc.macs='" + txtmacs.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
            frmMain.conn.Close();
            if ((dt.Rows.Count == 0) && (dt1.Rows.Count == 0))
            {
                sCommand = "delete chinhsach where macs='" + txtmacs.Text + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                layChinhsach();
            }
            else
            {
                MessageBox.Show("Chính sách này đã hoặc đang áp dụng, không thể xóa!");
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            String sCommand = "delete chinhsachchitiet where macsct='" + dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[1].Value.ToString()+"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
            layChinhsachCT();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            String sCommand = "delete chamsoc where macsoc='" + dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[1].Value.ToString() + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT chinhsach.*,nhomkhachhang.tennhom from Chinhsach,nhomkhachhang where chinhsach.manhom=nhomkhachhang.manhom and chinhsach.macn='" + CRM.frmMain.cn + "' and nhomkhachhang.tennhom like '%" + textBox1.Text + "%' ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chính sách", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên nhóm KH", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày kết thúc", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Hiệu lực", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["macs"].ToString();

                    row[2] = dt.Rows[i]["ten"].ToString();
                    row[3] = dt.Rows[i]["noidung"].ToString();
                    row[4] = dt.Rows[i]["ghichu"].ToString();
                    row[5] = dt.Rows[i]["tennhom"].ToString();
                    row[6] = dt.Rows[i]["batdau"].ToString().Substring(0, 10);
                    row[7] = dt.Rows[i]["ketthuc"].ToString().Substring(0, 10);
                    if (dt.Rows[i]["Hieuluc"].ToString() == "True")
                    {
                        row[8] = "Hoạt động";
                    }
                    else
                    {
                        row[8] = "Ngưng hoạt động";
                    }
                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX2.DataSource = dskh;
            dataGridViewX2.Columns[0].ReadOnly = true;

            dataGridViewX2.Columns[2].ReadOnly = true;
            dataGridViewX2.Columns[3].ReadOnly = true;
            dataGridViewX2.Columns[4].ReadOnly = true;
            dataGridViewX2.Columns[5].ReadOnly = true;
            dataGridViewX2.Columns[6].ReadOnly = true;
            dataGridViewX2.Columns[7].ReadOnly = false;
            dataGridViewX2.Columns[8].ReadOnly = false;
            
            dataGridViewX2.Columns[0].Width = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX2.Columns[1].Visible = false;
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT chinhsach.*,nhomkhachhang.tennhom from Chinhsach,nhomkhachhang where chinhsach.manhom=nhomkhachhang.manhom and chinhsach.macn='" + CRM.frmMain.cn + "' and chinhsach.ten like N'%" + textBox2.Text + "%' ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chính sách", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên nhóm KH", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày kết thúc", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Hiệu lực", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["macs"].ToString();

                    row[2] = dt.Rows[i]["ten"].ToString();
                    row[3] = dt.Rows[i]["noidung"].ToString();
                    row[4] = dt.Rows[i]["ghichu"].ToString();
                    row[5] = dt.Rows[i]["tennhom"].ToString();
                    row[6] = dt.Rows[i]["batdau"].ToString().Substring(0, 10);
                    row[7] = dt.Rows[i]["ketthuc"].ToString().Substring(0, 10);
                    if (dt.Rows[i]["Hieuluc"].ToString() == "True")
                        row[8] = "Hoạt động";
                    else
                        row[8] = "Ngưng hoạt động";
                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX2.DataSource = dskh;
            dataGridViewX2.Columns[0].ReadOnly = true;

            dataGridViewX2.Columns[2].ReadOnly = true;
            dataGridViewX2.Columns[3].ReadOnly = true;
            dataGridViewX2.Columns[4].ReadOnly = true;
            dataGridViewX2.Columns[5].ReadOnly = true;
            dataGridViewX2.Columns[6].ReadOnly = true;
            dataGridViewX2.Columns[7].ReadOnly = false;
            dataGridViewX2.Columns[8].ReadOnly = false;
            
            dataGridViewX2.Columns[0].Width = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX2.Columns[1].Visible = false;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int hluc=0;
            if (cbHieuluc_S.Text == "Hoạt động")
            {
                hluc = 1;
            }
            else
            {
                hluc = 0;
            }
            String sCommand = "SELECT chinhsach.*,nhomkhachhang.tennhom from Chinhsach,nhomkhachhang where chinhsach.manhom=nhomkhachhang.manhom and chinhsach.macn='" + CRM.frmMain.cn + "' and chinhsach.hieuluc =" + hluc + "";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã chính sách", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Tên nhóm KH", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Ngày kết thúc", typeof(string));
            dskh.Columns.Add(col);

            col = new DataColumn("Hiệu lực", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["macs"].ToString();
                    row[2] = dt.Rows[i]["ten"].ToString();
                    row[3] = dt.Rows[i]["noidung"].ToString();
                    row[4] = dt.Rows[i]["ghichu"].ToString();
                    row[5] = dt.Rows[i]["tennhom"].ToString();
                    row[6] = dt.Rows[i]["batdau"].ToString().Substring(0, 10);
                    row[7] = dt.Rows[i]["ketthuc"].ToString().Substring(0, 10);
                    if (dt.Rows[i]["Hieuluc"].ToString() == "True")
                    {
                        row[8] = "Hoạt động";
                    }
                    else
                    {
                        row[8] = "Ngưng hoạt động";
                    }
                    dskh.Rows.Add(row);
                }
                catch { }

            }
            dataGridViewX2.DataSource = dskh;
            dataGridViewX2.Columns[0].ReadOnly = true;

            dataGridViewX2.Columns[2].ReadOnly = true;
            dataGridViewX2.Columns[3].ReadOnly = true;
            dataGridViewX2.Columns[4].ReadOnly = true;
            dataGridViewX2.Columns[5].ReadOnly = true;
            dataGridViewX2.Columns[6].ReadOnly = true;
            dataGridViewX2.Columns[7].ReadOnly = false;
            dataGridViewX2.Columns[8].ReadOnly = false;

            dataGridViewX2.Columns[0].Width = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX2.Columns[1].Visible = false;
        }       
    }
}