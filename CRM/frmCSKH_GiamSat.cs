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
    public partial class frmCSKH_GiamSat : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public frmCSKH_GiamSat()
        {
            InitializeComponent();
            dtpFrom.CustomFormat = "MM/yyyy";
            DateTime dtCurrent = DateTime.Now;

            //dtpThang.Value = dtCurrentTime.AddMonths(-1);
            if (dtCurrent.Month == 1)
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
            }
            else
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
            }
            btnCancel.Visible = false; 
        }

        
        private void frmCSKH_GiamSat_Load(object sender, EventArgs e)
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            
            col = new DataColumn("Chi nhánh", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Chấm điểm", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Phê duyệt", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Tháng chấm điểm cuối", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Tháng phê duyệt cuối", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Phê duyệt toàn tỉnh", typeof(bool));
            dskh.Columns.Add(col);

            DataTable dt_macn = cnbus.DANH_SACH_MA_CHI_NHANH();
            if ((cbCN.Text == "") || (cbCN.Text == "Tất cả"))
            {
                //for (int i = 2300; i <= 2313; i++)
                for (int i = 0; i < dt_macn.Rows.Count; i++)
                {
                    String strcn = dt_macn.Rows[i]["MACN"].ToString();
                    dt.Clear();
                    String Strcmd = "select * from lichsudiem where left(makh,4)='" + strcn + "' and thang='" + dtpFrom.Text + "'";
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
                        DataTable dt1 = new DataTable();
                        dt1.Clear();
                        Strcmd = "select * from lichsudiem where left(makh,4)='" + strcn + "' and thang='" + dtpFrom.Text + "' and pheduyet=1";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt1);
                        DataAccess.conn.Close();
                        //Da phe duyet
                        if (dt1.Rows.Count > 0)
                            row[2] = true;
                        else
                            //Chua phe duyet
                            row[2] = false;
                        dt1.Clear();
                        Strcmd = "select * from lichsudiem where left(makh,4)='" + strcn + "' and thang='" + dtpFrom.Text + "' and PDTT=1";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt1);
                        DataAccess.conn.Close();
                        //Da phe duyet
                        if (dt1.Rows.Count > 0)
                        {
                            row[5] = true;
                            //btnConfirm.Enabled = false;
                            //btnCancel.Enabled = true;
                        }
                        else
                        {
                            //Chua phe duyet
                            row[5] = false;
                            //btnConfirm.Enabled = true;
                            //btnCancel.Enabled = false;
                        }
                    }
                    else
                    {
                        row[1] = false;
                        row[2] = false;
                        row[5]=false;
                    }
                    //Lay thang cham diem sau cung cua chi nhanh
                    DataTable dt2 = new DataTable();
                    dt2.Clear();
                    Strcmd = "select top 1 convert(date,'01/'+thang) as ngay,thang from LICHSUDIEM where left(makh,4)='" + strcn + "' order by ngay desc ";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt2);
                    DataAccess.conn.Close();
                    if (dt2.Rows.Count > 0)
                    {
                        row[3] = dt2.Rows[0]["Thang"].ToString();
                    }
                    DataTable dt3 = new DataTable();
                    dt3.Clear();
                    Strcmd = "select top 1 convert(date,'01/'+thang) as ngay,thang from LICHSUDIEM where left(makh,4)='" + strcn + "' and pheduyet=1 order by ngay desc ";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt3);
                    DataAccess.conn.Close();
                    if (dt3.Rows.Count > 0)
                    {
                        row[4] = dt3.Rows[0]["Thang"].ToString();
                    }
                    dskh.Rows.Add(row);
                }
            }
            else
            {
                String strcn = cbCN.Text;
                dt.Clear();
                String Strcmd = "select * from lichsudiem where left(makh,4)='" + strcn + "' and thang='" + dtpFrom.Text + "'";
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
                    DataTable dt1 = new DataTable();
                    dt1.Clear();
                    Strcmd = "select * from lichsudiem where left(makh,4)='" + strcn + "' and thang='" + dtpFrom.Text + "' and pheduyet=1";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt1);
                    DataAccess.conn.Close();
                    //Da phe duyet
                    if (dt1.Rows.Count > 0)
                        row[2] = true;
                    else
                        //Chua phe duyet
                        row[2] = false;
                    dt1.Clear();
                    Strcmd = "select * from lichsudiem where left(makh,4)='" + strcn + "' and thang='" + dtpFrom.Text + "' and PDTT=1";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt1);
                    DataAccess.conn.Close();
                    
                    if (dt1.Rows.Count > 0)
                    {
                        //Da phe duyet
                        row[5] = true;
                        //btnConfirm.Enabled = false;
                        //btnCancel.Enabled = true;
                    }
                        
                    else
                    {
                        //Chua phe duyet
                        row[5] = false;
                        //btnConfirm.Enabled = true;
                        //btnCancel.Enabled = false;
                    }
                        
                }
                else
                {
                    row[1] = false;
                    row[2] = false;
                    row[5] = false;
                }
                //Lay thang cham diem sau cung cua chi nhanh
                DataTable dt2 = new DataTable();
                dt2.Clear();
                Strcmd = "select top 1 convert(date,'01/'+thang) as ngay,thang from LICHSUDIEM where left(makh,4)='" + strcn + "' order by ngay desc ";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt2);
                DataAccess.conn.Close();
                if (dt2.Rows.Count > 0)
                {
                    row[3] = dt2.Rows[0]["Thang"].ToString();
                }
                DataTable dt3 = new DataTable();
                dt3.Clear();
                Strcmd = "select top 1 convert(date,'01/'+thang) as ngay,thang from LICHSUDIEM where left(makh,4)='" + strcn + "' and pheduyet=1 order by ngay desc ";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(Strcmd, DataAccess.conn).Fill(dt3);
                DataAccess.conn.Close();
                if (dt3.Rows.Count > 0)
                {
                    row[4] = dt3.Rows[0]["Thang"].ToString();
                }
                dskh.Rows.Add(row);   
            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = true;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = false;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[5].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[5].Value =true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //int dem = 0;
            //for (int i = 0; i < dgvDanhsach.RowCount; i++)
            //{
            //    if (dgvDanhsach.Rows[i].Cells[5].Value.ToString() == "True")
            //    {
            //        dem++;
            //        break;
            //    }
            //}

            //if (dem > 0)
            //{
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    //if (dgvDanhsach.Rows[i].Cells[5].Value.ToString() == "True")
                    //{
                        int pheduyet = 0;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[5].Value.ToString()) == true)
                        {
                            pheduyet = 1;
                        }
                        //string ngaypheduyet = "";
                        //string nam, thang, ngay, gio, phut, giay, miligiay;
                        //nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        //thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        //ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        //gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        //phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        //giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        //miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        //ngaypheduyet = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        try
                        {
                            String strCmd = "Update lichsudiem set PDTT='" + pheduyet + "',NgayPDTT= GETDATE(),NguoiPDTT='" + Thongtindangnhap.user_id + "' ";
                            strCmd += " Where left(MAKH,4)='" + dgvDanhsach.Rows[i].Cells[0].Value.ToString() + "' and thang='" + dtpFrom.Text + "'";

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
                                MessageBox.Show("Xác nhận phê duyệt/hủy phê duyệt chưa thành công!");
                                DataAccess.conn.Close();
                            }
                        }
                    }
                //}
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã xác nhận phê duyệt/hủy phê duyệt.");

            //}
            //else
            //{
            //    MessageBox.Show("Chưa chọn dòng nào.");
            //}
        }

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    int dem = 0;
        //    for (int i = 0; i < dgvDanhsach.RowCount; i++)
        //    {
        //        if (dgvDanhsach.Rows[i].Cells[5].Value.ToString() == "True")
        //        {
        //            dem++;
        //            break;
        //        }
        //    }

        //    if (dem > 0)
        //    {
        //        Cursor.Current = Cursors.WaitCursor;
        //        for (int i = 0; i < dgvDanhsach.RowCount; i++)
        //        {
        //            if (dgvDanhsach.Rows[i].Cells[5].Value.ToString() == "True")
        //            {
        //                int pheduyet = 1;
        //                if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[5].Value.ToString()) == true)
        //                {
        //                    pheduyet = 0;
        //                }
        //                //string ngaypheduyet = "";
        //                //string nam, thang, ngay, gio, phut, giay, miligiay;
        //                //nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
        //                //thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
        //                //ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
        //                //gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
        //                //phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
        //                //giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
        //                //miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
        //                //ngaypheduyet = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

        //                try
        //                {
        //                    String strCmd = "Update lichsudiem set PDTT='" + pheduyet + "',NgayPDTT= GETDATE(),NguoiPDTT='" + Thongtindangnhap.user_id + "' ";
        //                    strCmd += " Where left(MAKH,4)='" + dgvDanhsach.Rows[i].Cells[0].Value.ToString() + "' and thang='" + dtpFrom.Text + "'";

        //                    if (DataAccess.conn.State == ConnectionState.Open)
        //                    {
        //                        DataAccess.conn.Close();
        //                    }
        //                    DataAccess.conn.Open();
        //                    frmMain.myCommand = new SqlCommand(strCmd, DataAccess.conn);
        //                    frmMain.myCommand.ExecuteNonQuery();
        //                    DataAccess.conn.Close();
        //                }
        //                catch
        //                {
        //                    if (DataAccess.conn.State == ConnectionState.Open)
        //                    {
        //                        MessageBox.Show("Hủy phê duyệt chưa thành công!");
        //                        DataAccess.conn.Close();
        //                    }
        //                }
        //            }
        //        }
        //        Cursor.Current = Cursors.Default;
        //        MessageBox.Show("Đã hủy phê duyệt!");

        //    }
        //    else
        //    {
        //        MessageBox.Show("Chưa chọn dòng nào.");
        //    }
        //}

    }
}