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
    public partial class frmCSKH_TKD : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static string cn="";
        public frmCSKH_TKD()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dshd = new System.Data.DataTable();
            DataColumn col = null;
            dataGridViewX32.DataSource = dshd;
            col = new DataColumn("Mã CN", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("< 1,200", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("1,200 - 2,400", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("2,400 - 4,800", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("4,800 - 7,200", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("7,200 - 12,000", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("12,000 - 36,000", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("36,000 - 72,000", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("> 72,000", typeof(Int64));
            dshd.Columns.Add(col);

            DataTable dt_macn = cnbus.DANH_SACH_MA_CHI_NHANH();
            if ((cbCN.Text == "Tất cả")||(cbCN.Text==""))
            {
                //for (int k = 2300; k <= 2313; k++)
                for (int i = 0; i < dt_macn.Rows.Count; i++)
                {
                    //if (k != 4812)
                    //{
                        DataRow row = dshd.NewRow();
                        //row[0] = k.ToString();
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<1200 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[1] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[1] = "0";
                        };
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<2400 and diem>=1200 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[2] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[2] = "0";
                        };
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<4800 and diem>=2400 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[3] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[3] = "0";
                        };
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<7200 and diem>=4800 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[4] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[4] = "0";
                        };
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<12000 and diem>=7200 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[5] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[5] = "0";
                        };
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<36000 and diem>=12000 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[6] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[6] = "0";
                        };
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<72000 and diem>=36000 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[7] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[7] = "0";
                        };
                        dt.Clear();
                        sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem>=72000 and ma='" + dt_macn.Rows[i]["MACN"].ToString() + "' GROUP BY ma";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        try
                        {
                            row[8] = dt.Rows[0]["cou"].ToString();
                        }
                        catch
                        {
                            row[8] = "0";
                        };
                        dshd.Rows.Add(row);
                    //}
                }
            }
            else
            {
                DataRow row = dshd.NewRow();
                int k = Convert.ToInt32(cbCN.Text);
                row[0] = cbCN.Text;
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<1200 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[1] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[1] = "0";
                };
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<2400 and diem>=1200 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[2] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[2] = "0";
                };
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<4800 and diem>=2400 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[3] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[3] = "0";
                };
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<7200 and diem>=4800 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[4] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[4] = "0";
                };
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<12000 and diem>=7200 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[5] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[5] = "0";
                };
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<36000 and diem>=12000 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[6] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[6] = "0";
                };
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem<72000 and diem>=36000 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[7] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[7] = "0";
                };
                dt.Clear();
                sCommand = "SELECT ma,count(ma) as cou from DIEM_CN ,khachhang where diem_cn.makh=khachhang.makh and khachhang.loaikh=1 and diem>=72000 and ma='" + k.ToString() + "' GROUP BY ma";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                try
                {
                    row[8] = dt.Rows[0]["cou"].ToString();
                }
                catch
                {
                    row[8] = "0";
                };
                dshd.Rows.Add(row);   
            }

            dataGridViewX32.DataSource = dshd;


            dataGridViewX32.Columns[0].FillWeight = 50;
            dataGridViewX32.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX32.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX32.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX32.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX32.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX32.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX32.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX32.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewX1.Columns[6].Visible = false;            
            dataGridViewX32.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewX32.Columns[1].DefaultCellStyle.Format = "N0";
            dataGridViewX32.Columns[2].DefaultCellStyle.Format = "N0";
            dataGridViewX32.Columns[3].DefaultCellStyle.Format = "N0";
            dataGridViewX32.Columns[4].DefaultCellStyle.Format = "N0";
            dataGridViewX32.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridViewX32.Columns[6].DefaultCellStyle.Format = "N0";
            dataGridViewX32.Columns[7].DefaultCellStyle.Format = "N0";
            dataGridViewX32.Columns[8].DefaultCellStyle.Format = "N0";
            Cursor.Current = Cursors.Default;
        }

        private void frmCSKH_TKD_Load(object sender, EventArgs e)
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
                //cbCN.Text = Thongtindangnhap.macn;
                cbCN.Enabled = false;
            }
        }

        private void buttonX159_Click(object sender, EventArgs e)
        {
            String temp = "";
            temp = "DiemKhachHang.xls";

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
                for (int i = 0; i < dataGridViewX32.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridViewX32.Rows[i];
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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 24;
            cn = cbCN.Text;
            if ((cn == "Tất cả") || (cn == ""))
                cn = "9999";
            @In form_in = new @In();
            form_in.Show();
        }

       
    }
}