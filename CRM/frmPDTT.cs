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
    public partial class frmPDTT : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public frmPDTT()
        {
            InitializeComponent();
        }

        private void frmPDTT_Load(object sender, EventArgs e)
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
            DataTable dt = new DataTable();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;

            col = new DataColumn("Chi nhánh", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Phê duyệt chi nhánh", typeof(bool));
            dskh.Columns.Add(col);           
            col = new DataColumn("Phê duyệt toàn tỉnh", typeof(bool));
            dskh.Columns.Add(col);
            String Strcmd = "";
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                for (int i = 4800; i <= 4815; i++)
                {
                    if (i != 4812)
                    {
                        String strcn = Convert.ToString(i);
                        dt.Clear();
                        Strcmd = "select * from ketquaxeploai where left(makh,4)='" + strcn + "' and tuthang='" + dtpFrom.Text + "' and denthang='"+dtpTo.Text+"' and pheduyet=1";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();

                        DataRow row = dskh.NewRow();
                        row[0] = strcn;
                        if (dt.Rows.Count > 0)
                        {
                            row[1] = true;                           
                        }
                        else
                        {
                            row[1] = false;                           
                        }
                        
                        dt.Clear();
                        Strcmd = "select * from ketquaxeploai where left(makh,4)='" + strcn + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and pdtt=1";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        if (dt.Rows.Count > 0)
                        {
                            row[2] = true;
                        }
                        else
                        {
                            row[2] = false;
                        }
                       
                        dskh.Rows.Add(row);
                    }
                }
            }
            else
            {
                String strcn = cbCN.Text;
                dt.Clear();
                Strcmd = "select * from ketquaxeploai where left(makh,4)='" + strcn + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'  and pheduyet=1";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                DataRow row = dskh.NewRow();
                row[0] = strcn;
                if (dt.Rows.Count > 0)
                {
                    row[1] = true;
                }
                else
                {
                    row[1] = false;
                }
                
                dt.Clear();
                Strcmd = "select * from ketquaxeploai where left(makh,4)='" + strcn + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and pdtt=1";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                if (dt.Rows.Count > 0)
                {
                    row[2] = true;
                }
                else
                {
                    row[2] = false;
                }

                dskh.Rows.Add(row);                
            }
            dgvDanhsach.DataSource = dskh;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 50;
            //dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = true;            
            dgvDanhsach.Columns[2].ReadOnly = false;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[2].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[2].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[2].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }

            if (dem > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[2].Value.ToString() == "True")
                    {
                        int pheduyet = 0;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[2].Value.ToString()) == true)
                        {
                            pheduyet = 1;
                        }

                        string ngaypheduyet = "";
                        string nam, thang, ngay, gio, phut, giay, miligiay;
                        nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        ngaypheduyet = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        try
                        {
                            //String strCmd = "Update ketquaxeploai set PDTT='" + pheduyet + "',NgayPDTT='" + ngaypheduyet + "',NguoiPDTT='" + Thongtindangnhap.user_id + "' ";
                            //strCmd += " Where left(MAKH,4)='" + dgvDanhsach.Rows[i].Cells[0].Value.ToString() + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";

                            String strCmd = "Update ketquaxeploai set PDTT='" + pheduyet + "',NgayPDTT= GETDATE(),NguoiPDTT='" + Thongtindangnhap.user_id + "' ";
                            strCmd += " Where left(MAKH,4)='" + dgvDanhsach.Rows[i].Cells[0].Value.ToString() + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";

                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
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
                }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã phê duyệt!");
                
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }
    }
}