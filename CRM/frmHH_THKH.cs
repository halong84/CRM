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
    public partial class frmHH_THKH : Form
    {
        public frmHH_THKH()
        {
            InitializeComponent();
        }

        private void frmHH_THKH_Load(object sender, EventArgs e)
        {
            
            dtpThang.Enabled = false;
            cbTieuchi.Enabled = false;
            
            cbLoaiKH.Enabled = false;
            cbKeHoach.Enabled = false;
            txtChiphi.Enabled = false;
            txtNoiDung.Enabled = false;
            dtpNgay.Enabled = false;
            btnSave.Enabled = false;
            
        }
        private void layDS_Tieuchi()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            if ((optCN.Checked == true)||(optLDDN.Checked == true))
                sCommand = "SELECT * FROM LICHCHAMSOC where ghichu like '%CN%' ";
            else
                sCommand = "SELECT * FROM LICHCHAMSOC where ghichu like '%DN%' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbTieuchi.DataSource = dt;
            cbTieuchi.DisplayMember = "TIEUCHI";
            cbTieuchi.ValueMember = "MATC";
            cbTieuchi.DataSource = dt;
            cbTieuchi.SelectedIndex = 0;
        }
        private void layLoaiKH()
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                sCommand = "SELECT * from DMXEPLOAIKH where loaikh=1 or MALOAI='9999' order by MALOAI desc";
            }
            else
            {
                sCommand = "SELECT * from DMXEPLOAIKH where loaikh=2 or MALOAI='9999' order by MALOAI desc";
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "TenLoai";
            cbLoaiKH.ValueMember = "MaLoai";
            cbLoaiKH.DataSource = dt;
            //lblTenloai.Text = dt.Rows[0]["Tenloai"].ToString();
        }
        private void layKehoach()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            string quy = cbQuy.Text + "/" + dtpThang.Text;

            if (optCN.Checked == true)
            {
                if ((cbLoaiKH.Text == "") || (cbLoaiKH.Text == "Tất cả"))
                    sCommand = "SELECT * FROM kehoachchamsoc where thang='" + quy + "' and  matc='" + cbTieuchi.SelectedValue.ToString() + "' and loaikh=1 and pheduyet=1 and macn='" + Thongtindangnhap.macn + "'";
                else
                    sCommand = "SELECT * FROM kehoachchamsoc where thang='" + quy + "' and  matc='" + cbTieuchi.SelectedValue.ToString() + "' and xeploaikh='" + cbLoaiKH.SelectedValue.ToString() + "' and loaikh=1 and pheduyet=1 and macn='" + Thongtindangnhap.macn + "'";
            }
            if (optDN.Checked == true)
            {
                if ((cbLoaiKH.Text == "") || (cbLoaiKH.Text == "Tất cả"))
                    sCommand = "SELECT * FROM kehoachchamsoc where thang='" + quy + "' and  matc='" + cbTieuchi.SelectedValue.ToString() + "' and loaikh=2 and pheduyet=1 and macn='" + Thongtindangnhap.macn + "'";
                else
                    sCommand = "SELECT * FROM kehoachchamsoc where thang='" + quy + "' and  matc='" + cbTieuchi.SelectedValue.ToString() + "' and xeploaikh='" + cbLoaiKH.SelectedValue.ToString() + "' and loaikh=2 and pheduyet=1 and macn='" + Thongtindangnhap.macn + "'";
            }
            if (optLDDN.Checked == true)
            {
                if ((cbLoaiKH.Text == "") || (cbLoaiKH.Text == "Tất cả"))
                    sCommand = "SELECT * FROM kehoachchamsoc where thang='" + quy + "' and  matc='" + cbTieuchi.SelectedValue.ToString() + "' and loaikh=3 and pheduyet=1 and macn='" + Thongtindangnhap.macn + "'";
                else
                    sCommand = "SELECT * FROM kehoachchamsoc where thang='" + quy + "' and  matc='" + cbTieuchi.SelectedValue.ToString() + "' and xeploaikh='" + cbLoaiKH.SelectedValue.ToString() + "' and loaikh=3 and pheduyet=1 and macn='" + Thongtindangnhap.macn + "'";
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbKeHoach.DataSource = dt;
            cbKeHoach.DisplayMember = "NoiDung";
            cbKeHoach.ValueMember = "Ma";
            cbKeHoach.DataSource = dt;
            cbKeHoach.Text = "";
            
        }
       

        private void btnAdd_Click(object sender, EventArgs e)
        {

            dtpThang.Enabled = true;
            cbTieuchi.Enabled = true;
            
            cbLoaiKH.Enabled = true;
            cbKeHoach.Enabled = true;            
            dtpNgay.Enabled = true;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            layDS_Tieuchi();
            layLoaiKH();
            layKehoach();            
            layDanhsach();
        }
        private void layDanhsach()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            String sCommand = "";
            if (cbKeHoach.Text != "")
            {
                if ((optCN.Checked == true) || (optDN.Checked == true))
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and chitietkhcs.ma='" + cbKeHoach.SelectedValue.ToString() + "'";
                else
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from nguoilienhe khachhang,chitietkhcs where khachhang.manlh=chitietkhcs.makh and chitietkhcs.ma='" + cbKeHoach.SelectedValue.ToString() + "'";
            }
            else
            {
                if ((optCN.Checked == true) || (optDN.Checked == true))
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and 1=-0";
                else
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from nguoilienhe khachhang,chitietkhcs where khachhang.manlh=chitietkhcs.makh and 1=0";
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã khách hàng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Nhận quà", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Nhân viên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Edit", typeof(bool));
            dskh.Columns.Add(col);


            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["makh"].ToString();
                    row[2] = dt.Rows[i]["hotenkh"].ToString();
                    if (dt.Rows[i]["nhan"].ToString() == "True")
                        row[3] = true;
                    else
                        row[3] = false;
                    if(dt.Rows[i]["ngay"].ToString()!="")
                        row[4] = dt.Rows[i]["ngay"].ToString().Substring(0,10);
                    row[5] = dt.Rows[i]["manv"].ToString();
                    row[6] = false;
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = true;
            dgvDanhsach.Columns[2].ReadOnly = true;
            //dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.Columns[6].Visible = false;
        }
       

        private void cbTieuchi_TextChanged(object sender, EventArgs e)
        {
            if((dtpThang.Text!="")&&(cbLoaiKH.Text!="")&&(cbQuy.Text!=""))
                layKehoach();
        }

        private void cbLoaiKH_TextChanged(object sender, EventArgs e)
        {
            if ((dtpThang.Text != "") && (cbLoaiKH.Text != "") && (cbQuy.Text != ""))
                layKehoach();
        }

        private void optCN_Click(object sender, EventArgs e)
        {
            layDS_Tieuchi();
            layLoaiKH();
            
        }

        private void optDN_Click(object sender, EventArgs e)
        {
            layDS_Tieuchi();
            layLoaiKH();
        }

        private void cbKeHoach_TextChanged(object sender, EventArgs e)
        {
            layDanhsach();
            //txtNoiDung.Text = cbKeHoach.SelectedText.ToString();
            if (cbKeHoach.Text != "")
            {
                DataTable dt = new DataTable();
                dt.Clear();
                String sCommand = "";
                if (cbKeHoach.Text != "")
                {
                    sCommand = "select * from kehoachchamsoc where ma='" + cbKeHoach.SelectedValue.ToString() + "'";
                }
                else
                    sCommand = "select * from kehoachchamsoc where 1=0";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                txtNoiDung.Text = dt.Rows[0]["noidung"].ToString();
                txtChiphi.Text = dt.Rows[0]["kinhphi"].ToString();                
            }
            else
            {
                txtNoiDung.Text = "";
                txtChiphi.Text = "0";
            }
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                
                dgvDanhsach.Rows[i].Cells[3].Value = true;                
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
               dgvDanhsach.Rows[i].Cells[3].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qyery_temp = "";
            //string ngay = dtpNgay.Text.Substring(3, 2) + "/" + dtpNgay.Text.Substring(0, 2) + "/" + dtpNgay.Text.Substring(6, 4);
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[6].Value.ToString() =="True")
                {
                    
                    if (dgvDanhsach.Rows[i].Cells[3].Value.ToString() == "True")
                    {
                        string ngay = dgvDanhsach.Rows[i].Cells[4].Value.ToString().Substring(3, 2) + "/" + dgvDanhsach.Rows[i].Cells[4].Value.ToString().Substring(0, 2) + "/" + dgvDanhsach.Rows[i].Cells[4].Value.ToString().Substring(6, 4);
                        qyery_temp = "Update chitietkhcs set nhan=1, ngay='" + ngay + "',manv='" + Thongtindangnhap.user_id + "' where makh='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and ma='" + cbKeHoach.SelectedValue.ToString() + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                        //Kiem tra khach hang da nhan qua chua
                        DataTable dt = new DataTable();
                        dt.Clear();
                        qyery_temp = "select * from giaodich where makh='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and ghichu='" + cbKeHoach.SelectedValue.ToString() + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(qyery_temp, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        if (dt.Rows.Count == 0)
                        {
                            //Insert vao giao dich khach hang
                            try
                            {
                                string magd = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                qyery_temp = "Insert into Giaodich (magd,makh,noidung,TGBatdau,TGketthuc,chiphi,maloaigd,user_id,macp,ghichu) values('" + magd + "','" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "',N'Tặng quà theo kế hoạch chăm sóc khách hàng','" + ngay + "','" + ngay + "'," + Convert.ToDecimal(txtChiphi.Text) + ",'HD07','" + Thongtindangnhap.user_id + "','CP01','" + cbKeHoach.SelectedValue.ToString() + "')";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }
                            catch { };
                        }
                    }
                    else
                    {
                        qyery_temp = "Update chitietkhcs set nhan=0, ngay='',manv='" + Thongtindangnhap.user_id + "' where makh='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }

                }
            }
            btnAdd.Enabled = true;
            MessageBox.Show("Đã cập nhật!");
            layDanhsach();
            
        }

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            if (txtSCMND.Text == "")
                layDanhsach();
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();
                String sCommand = "";
                if (cbKeHoach.Text != "")
                {
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and chitietkhcs.ma='" + cbKeHoach.SelectedValue.ToString() + "' and khachhang.cmnd='" + txtSCMND.Text + "'";
                }
                else
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and 1=0";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhận quà", typeof(bool));
                dskh.Columns.Add(col);
                col = new DataColumn("Ngày", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhân viên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Edit", typeof(bool));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hotenkh"].ToString();
                        if (dt.Rows[i]["nhan"].ToString() == "True")
                            row[3] = true;
                        else
                            row[3] = false;
                        if(dt.Rows[i]["ngay"].ToString()!="")
                            row[4] = dt.Rows[i]["ngay"].ToString().Substring(0, 10);
                        row[5] = dt.Rows[i]["manv"].ToString();
                        row[6] = false;
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].ReadOnly = true;
                dgvDanhsach.Columns[1].ReadOnly = true;
                dgvDanhsach.Columns[2].ReadOnly = true;
                dgvDanhsach.Columns[4].ReadOnly = true;
                dgvDanhsach.Columns[5].ReadOnly = true;
                dgvDanhsach.Columns[6].Visible = false;
            }
        }
        private void btnSTel_Click(object sender, EventArgs e)
        {
            if (txtSTel.Text == "")
                layDanhsach();
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();
                String sCommand = "";
                if (cbKeHoach.Text != "")
                {
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and chitietkhcs.ma='" + cbKeHoach.SelectedValue.ToString() + "'and khachhang.dienthoai1='" + txtSTel.Text + "'";
                }
                else
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and 1=0";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhận quà", typeof(bool));
                dskh.Columns.Add(col);
                col = new DataColumn("Ngày", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhân viên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Edit", typeof(bool));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hotenkh"].ToString();
                        if (dt.Rows[i]["nhan"].ToString() == "True")
                            row[3] = true;
                        else
                            row[3] = false;
                        if (dt.Rows[i]["ngay"].ToString() != "")
                            row[4] = dt.Rows[i]["ngay"].ToString().Substring(0, 10);
                        row[5] = dt.Rows[i]["manv"].ToString();
                        row[6] = false;
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].ReadOnly = true;
                dgvDanhsach.Columns[1].ReadOnly = true;
                dgvDanhsach.Columns[2].ReadOnly = true;
                dgvDanhsach.Columns[4].ReadOnly = true;
                dgvDanhsach.Columns[5].ReadOnly = true;
                dgvDanhsach.Columns[6].Visible = false;
            }
        }
        private void btnSTen_Click(object sender, EventArgs e)
        {
            if (txtSTen.Text == "")
                layDanhsach();
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();
                String sCommand = "";
                if (cbKeHoach.Text != "")
                {
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and chitietkhcs.ma='" + cbKeHoach.SelectedValue.ToString() + "' and khachhang.hoten like N'%" + txtSTen.Text + "%'";
                }
                else
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and 1=0";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhận quà", typeof(bool));
                dskh.Columns.Add(col);
                col = new DataColumn("Ngày", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhân viên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Edit", typeof(bool));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hotenkh"].ToString();
                        if (dt.Rows[i]["nhan"].ToString() == "True")
                            row[3] = true;
                        else
                            row[3] = false;
                        if (dt.Rows[i]["ngay"].ToString() != "")
                            row[4] = dt.Rows[i]["ngay"].ToString().Substring(0, 10);
                        row[5] = dt.Rows[i]["manv"].ToString();
                        row[6] = false;
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].ReadOnly = true;
                dgvDanhsach.Columns[1].ReadOnly = true;
                dgvDanhsach.Columns[2].ReadOnly = true;
                dgvDanhsach.Columns[4].ReadOnly = true;
                dgvDanhsach.Columns[5].ReadOnly = true;
                dgvDanhsach.Columns[6].Visible = false;
            }
        }

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            if (txtSMaKH.Text == "")
                layDanhsach();
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();
                String sCommand = "";
                if (cbKeHoach.Text != "")
                {
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and chitietkhcs.ma='" + cbKeHoach.SelectedValue.ToString() + "'and khachhang.makh='"+txtSMaKH.Text+"'";
                }
                else
                    sCommand = "select khachhang.hoten as hotenkh,chitietkhcs.* from khachhang,chitietkhcs where khachhang.makh=chitietkhcs.makh and 1=0";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhận quà", typeof(bool));
                dskh.Columns.Add(col);
                col = new DataColumn("Ngày", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Nhân viên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Edit", typeof(bool));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hotenkh"].ToString();
                        if (dt.Rows[i]["nhan"].ToString() == "True")
                            row[3] = true;
                        else
                            row[3] = false;
                        if (dt.Rows[i]["ngay"].ToString() != "")
                            row[4] = dt.Rows[i]["ngay"].ToString().Substring(0, 10);
                        row[5] = dt.Rows[i]["manv"].ToString();
                        row[6] = false;
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].ReadOnly = true;
                dgvDanhsach.Columns[1].ReadOnly = true;
                dgvDanhsach.Columns[2].ReadOnly = true;
                dgvDanhsach.Columns[4].ReadOnly = true;
                dgvDanhsach.Columns[5].ReadOnly = true;
                dgvDanhsach.Columns[6].Visible = false;
            }
        }

        private void dgvDanhsach_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[6].Value = true;
        }

        private void optLDDN_Click(object sender, EventArgs e)
        {
            layDS_Tieuchi();
            layLoaiKH();
        }


       
    }
}