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
    public partial class frmHH_XeploaiThang : Form
    {
        String strCmd = "";
        SqlCommand myCommand;
        public static String makh = "", thang = "";
        public static decimal diemtggt = 0, tdiem = 0;
        public static byte loaikh;

        public frmHH_XeploaiThang()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            DateTime dtCurrent = DateTime.Now;
            dtpThang.CustomFormat = "MM/yyyy";            
            //dtpThang.Value = dtCurrent.AddMonths(-1);
            if (dtCurrent.Month == 1)
            {
                dtpThang.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);                
            }
            else
            {
                dtpThang.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);                
            }
        }       

        private void XEPLOAIT_Load(object sender, EventArgs e)
        {
            thang = dtpThang.Text;            
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            String ngaytinhdiem;
            SqlDataAdapter adapter = new SqlDataAdapter();
            ngaytinhdiem = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
            if (optCN.Checked == true)
            {
                //Kiem tra thang da duoc xep loai chua
                DataTable dt = new DataTable();
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                myCommand = new SqlCommand(strCmd, frmMain.conn);
                myCommand.CommandTimeout = 0;                
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                frmMain.conn.Close();
                if (dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Thang nay da duoc xep loai! Xep loai lai khong? ", "Xep loai khach hang ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Xep loai khach hang

                        decimal diem = 0;
                        thang = dtpThang.Text;
                        dt.Clear();
                        strCmd = "select diemkh.*,khachhang.hoten from diemkh,khachhang where diemkh.diem_sdbq>0 and khachhang.makh=diemkh.makh and diemkh.loaikh=1 and thang='" + thang + "'  and left(diemkh.makh,4)='" + frmMain.cn + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        myCommand = new SqlCommand(strCmd, frmMain.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt);
                        frmMain.conn.Close();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            makh = dt.Rows[i]["MAKH"].ToString();
                            loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                            diem = Convert.ToDecimal(dt.Rows[i]["tongdiemdl"].ToString());

                            DataTable dt1 = new DataTable();
                            strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            myCommand = new SqlCommand(strCmd, frmMain.conn);
                            myCommand.CommandTimeout = 0;
                            adapter.SelectCommand = myCommand;
                            adapter.Fill(dt1);
                            frmMain.conn.Close();
                            if (dt1.Rows.Count > 0)
                            {
                                strCmd = "Update diemkh set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "' and thang ='" + thang + "'";
                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }
                                frmMain.conn.Open();
                                myCommand = new SqlCommand(strCmd, frmMain.conn);
                                myCommand.CommandTimeout = 0;
                                myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
                            }
                        }
                    }
                }
                else //Chua xep loai
                {
                    decimal diem = 0;
                    thang = dtpThang.Text;
                    dt.Clear();
                    strCmd = "select diemkh.*,khachhang.hoten from diemkh,khachhang where diemkh.diem_sdbq>0 and khachhang.makh=diemkh.makh and diemkh.loaikh=1 and thang='" + thang + "'  and left(diemkh.makh,4)='" + frmMain.cn + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    myCommand = new SqlCommand(strCmd, frmMain.conn);
                    myCommand.CommandTimeout = 0;
                    adapter.SelectCommand = myCommand;
                    adapter.Fill(dt);
                    frmMain.conn.Close();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        makh = dt.Rows[i]["MAKH"].ToString();
                        loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                        diem = Convert.ToDecimal(dt.Rows[i]["tongdiemdl"].ToString());

                        DataTable dt1 = new DataTable();
                        strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        myCommand = new SqlCommand(strCmd, frmMain.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt1);
                        frmMain.conn.Close();
                        if (dt1.Rows.Count > 0)
                        {
                            strCmd = "Update diemkh set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "' and thang ='" + thang + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            myCommand = new SqlCommand(strCmd, frmMain.conn);
                            myCommand.CommandTimeout = 0;
                            myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }

                }

                dt.Clear();
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                myCommand = new SqlCommand(strCmd, frmMain.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGG"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {

                //Kiem tra thang da duoc xep loai chua
                DataTable dt = new DataTable();
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                myCommand = new SqlCommand(strCmd, frmMain.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                frmMain.conn.Close();
                if (dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Thang nay da duoc xep loai! Xep loai lai khong? ", "Xep loai khach hang ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Xep loai khach hang
                        decimal diem = 0;
                        dt.Clear();
                        strCmd = "select diemkh.*,khachhang.hoten from diemkh,khachhang where diemkh.diem_sdbq>0 and khachhang.makh=diemkh.makh and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        myCommand = new SqlCommand(strCmd, frmMain.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt);
                        frmMain.conn.Close();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            makh = dt.Rows[i]["MAKH"].ToString();
                            loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                            diem = Convert.ToDecimal(dt.Rows[i]["tongdiemdl"].ToString());

                            DataTable dt1 = new DataTable();
                            strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            myCommand = new SqlCommand(strCmd, frmMain.conn);
                            myCommand.CommandTimeout = 0;
                            adapter.SelectCommand = myCommand;
                            adapter.Fill(dt1);
                            frmMain.conn.Close();
                            if (dt1.Rows.Count > 0)
                            {
                                strCmd = "Update diemkh set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "' and thang ='" + dtpThang.Text + "'";
                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }
                                frmMain.conn.Open();
                                myCommand = new SqlCommand(strCmd, frmMain.conn);
                                myCommand.CommandTimeout = 0;
                                myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
                            }
                        }
                    }
                }
                else
                //Chua xep loai
                {
                    decimal diem = 0;
                    thang = dtpThang.Text;
                    dt.Clear();
                    strCmd = "select diemkh.*,khachhang.hoten from diemkh,khachhang where diemkh.diem_sdbq>0 and khachhang.makh=diemkh.makh and diemkh.loaikh=2 and thang='" + thang + "'  and left(diemkh.makh,4)='" + frmMain.cn + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    myCommand = new SqlCommand(strCmd, frmMain.conn);
                    myCommand.CommandTimeout = 0;
                    adapter.SelectCommand = myCommand;
                    adapter.Fill(dt);
                    frmMain.conn.Close();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        makh = dt.Rows[i]["MAKH"].ToString();
                        loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                        diem = Convert.ToDecimal(dt.Rows[i]["tongdiemdl"].ToString());

                        DataTable dt1 = new DataTable();
                        strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        myCommand = new SqlCommand(strCmd, frmMain.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt1);
                        frmMain.conn.Close();
                        if (dt1.Rows.Count > 0)
                        {
                            strCmd = "Update diemkh set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "' and thang ='" + thang + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            myCommand = new SqlCommand(strCmd, frmMain.conn);
                            myCommand.CommandTimeout = 0;
                            myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
                dt.Clear();
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                myCommand = new SqlCommand(strCmd, frmMain.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }

       
        private void optCN_Click(object sender, EventArgs e)
        {
            if (optCN.Checked == true)
            {
                loaikh = 1;
                dataGridViewX2.Visible = true;
                dataGridViewX3.Visible = false;
            }
            
        }

        private void optDN_Click(object sender, EventArgs e)
        {
            if (optDN.Checked == true)
            {
                loaikh = 2;
                dataGridViewX2.Visible = false;
                dataGridViewX3.Visible = true;
            }     
        }

        

      

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and diemkh.makh like '%" + textBox1.Text + "%' and left(diemkh.makh,4)='" + frmMain.cn + "'";

                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                } frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));                                
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGG"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;                

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and diemkh.makh like '%" + textBox1.Text + "%' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));               
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();                        
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;               

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dataGridViewX3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loaikh = 2;
            makh = dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[1].Value.ToString();
            tdiem = Convert.ToDecimal(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[6].Value.ToString());
            CRM.frmHH_CTDiemKHThang form_ct = new frmHH_CTDiemKHThang();
            form_ct.ShowDialog();
            
        }

        private void dataGridViewX2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loaikh = 1;
            makh = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[1].Value.ToString();
            tdiem = Convert.ToDecimal(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[6].Value.ToString());
            diemtggt = Convert.ToDecimal(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[4].Value.ToString());
            CRM.frmHH_CTDiemKHThang form_ct = new frmHH_CTDiemKHThang();
            form_ct.ShowDialog();
            
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            String temp="";
            if(optCN.Checked==true)
            {   
                temp = "XeploaikhachhangCN.xls";
            }
            else
            { 
                temp = "XeploaikhachhangDN.xls";
            }

            saveFileDialog1.FileName = temp.Replace("/", "-");
            saveFileDialog1.Filter = " Excel (*.xls)|*.xls|Tất cả (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            string path = "";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                path = saveFileDialog1.FileName;
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 30;
                if (optCN.Checked == true)
                {
                    for (int i = 0; i < dataGridViewX2.Rows.Count; i++)
                    {
                        DataGridViewRow row = dataGridViewX2.Rows[i];
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            ExcelApp.Cells[i + 1, j + 1] = row.Cells[j].Value.ToString();
                        }
                    }
                }
                else
                    for (int i = 0; i < dataGridViewX3.Rows.Count; i++)
                    {
                        DataGridViewRow row = dataGridViewX3.Rows[i];
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            ExcelApp.Cells[i + 1, j + 1] = row.Cells[j].Value.ToString();
                        }
                    }
                ExcelApp.ActiveWorkbook.SaveCopyAs(path);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Đã Lưu");
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            if (optCN.Checked == true)
            {
                DataTable dt = new DataTable();
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                myCommand = new SqlCommand(strCmd, frmMain.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGG"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {

                DataTable dt = new DataTable();
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();

                myCommand = new SqlCommand(strCmd, frmMain.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);

                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
 
        }

        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {
            thang = dtpThang.Text;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and khachhang.hoten like N'%" + textBox2.Text + "%' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGG"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and diemkh.makh like N'%" + textBox2.Text + "%' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and dmxeploaikh.tenloai like '%" + textBox3.Text + "%' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGG"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and dmxeploaikh.tenloai like '%" + textBox3.Text + "%' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                if (txtTu.Text.Trim() == "" && txtDen.Text.Trim() == "")
                {
                    strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                }
                else
                {
                    strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=1 and thang='" + dtpThang.Text + "' and (diemkh.tongdiemdl between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "') and left(diemkh.makh,4)='" + frmMain.cn + "'";
                }
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGG"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                if (txtTu.Text.Trim() == "" && txtDen.Text.Trim() == "")
                {
                    strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and left(diemkh.makh,4)='" + frmMain.cn + "'";
                }
                else
                {
                    strCmd = "select diemkh.*,dmxeploaikh.tenloai,khachhang.hoten from diemkh,khachhang,dmxeploaikh where khachhang.makh=diemkh.makh and diemkh.xeploai=dmxeploaikh.maloai and diemkh.loaikh=2 and thang='" + dtpThang.Text + "' and (diemkh.tongdiemdl between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "') and left(diemkh.makh,4)='" + frmMain.cn + "'";
                }
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["tenloai"].ToString();
                        row[8] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }      
    }
}