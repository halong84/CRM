using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmKH_NV : Form
    {
        public static string maNhomKHTN = "", tenNhomKHTN = "";
        string strCmd = "";
        DataTable dtResult = new DataTable();

        public frmKH_NV()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbNhomKHTN.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbNV.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;            
        }

        private void frmKH_NV_Load(object sender, System.EventArgs e)
        {
            layDS_NhomKH();
            layDS_NV();
            layDanhsach();

            try
            {
                maNhomKHTN = dgvDanhsach.CurrentRow.Cells["Mã nhóm"].Value.ToString();
                tenNhomKHTN = dgvDanhsach.CurrentRow.Cells["Tên nhóm"].Value.ToString();
            }
            catch { }
        }

        private void layDS_NhomKH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT manhom,Tennhom from nhomkhachhangTN Where MaCN='" + Thongtindangnhap.macn + "' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbNhomKHTN.DataSource = dt;
            cbbNhomKHTN.DisplayMember = "Tennhom";
            cbbNhomKHTN.ValueMember = "Manhom";
            cbbNhomKHTN.DataSource = dt;
        }

        private void layDS_NV()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * FROM _USER WHERE MACN='" + Thongtindangnhap.macn + "' ORDER BY TENNV ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbbNV.DataSource = dt;
            cbbNV.DisplayMember = "TENNV";
            cbbNV.ValueMember = "USER_ID";
            cbbNV.DataSource = dt;
            cbbNV.SelectedIndex = 0;
        }        

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã nhóm", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên nhóm", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CB", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CB quản lý", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "Select kh_nv.*, nhomkhtn.DIENGIAI as TenNhom, nv.TENNV as TenNV from KH_NV as kh_nv, NHOMKHACHHANGTN as nhomkhtn, _USER as nv ";
            strCmd += " Where kh_nv.MANHOM=nhomkhtn.MANHOM and kh_nv.MANV=nv.USER_ID and kh_nv.MACN='" + Thongtindangnhap.macn + "' ";
            strCmd += " ORDER BY kh_nv.MANHOM ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaNhom"].ToString();
                    row[2] = dtResult.Rows[i]["TenNhom"].ToString();
                    row[3] = dtResult.Rows[i]["MANV"].ToString();
                    row[4] = dtResult.Rows[i]["TenNV"].ToString();

                    string ngaybatdau, ngayketthuc;
                    ngaybatdau = dtResult.Rows[i]["Ngaybatdau"].ToString();
                    ngayketthuc = dtResult.Rows[i]["Ngayketthuc"].ToString();

                    string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
                    ngayBD = ngaybatdau.Substring(0, 2);
                    thangBD = ngaybatdau.Substring(3, 2);
                    namBD = ngaybatdau.Substring(6, 4);
                    ngayKT = ngayketthuc.Substring(0, 2);
                    thangKT = ngayketthuc.Substring(3, 2);
                    namKT = ngayketthuc.Substring(6, 4);

                    row[5] = ngayBD + "/" + thangBD + "/" + namBD;
                    row[6] = ngayKT + "/" + thangKT + "/" + namKT;
                    row[7] = dtResult.Rows[i]["Ghichu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 150;
            dgvDanhsach.Columns[2].Width = 200;
            dgvDanhsach.Columns[3].Width = 100;
            dgvDanhsach.Columns[4].Width = 200;
            dgvDanhsach.Columns[5].Width = 130;
            dgvDanhsach.Columns[6].Width = 130;
            dgvDanhsach.Columns[7].Width = 200;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (txtKH.Text == "")
            //{
            //    MessageBox.Show("Chưa chọn mã KH.", "Thông báo");
            //    txtKH.Focus();
            //    return;
            //}

            string ngaybatdau, ngayketthuc;
            ngaybatdau = dtpNgayBD.Text;
            ngayketthuc = dtpNgayKT.Text;

            string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
            ngayBD = ngaybatdau.Substring(0, 2);
            thangBD = ngaybatdau.Substring(3, 2);
            namBD = ngaybatdau.Substring(6, 4);
            ngayKT = ngayketthuc.Substring(0, 2);
            thangKT = ngayketthuc.Substring(3, 2);
            namKT = ngayketthuc.Substring(6, 4);

            string ngaycapnhat = "";
            string nam, thang, ngay, gio, phut, giay, miligiay;
            nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
            thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
            ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
            gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
            phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
            giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
            miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
            ngaycapnhat = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;
            
            strCmd = "SELECT * FROM KH_NV WHERE MaNHOM='" + cbbNhomKHTN.SelectedValue.ToString() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into KH_NV(MaNhom,MaNV,Ngaybatdau,Ngayketthuc,Ghichu,Nguoicapnhat,Ngaycapnhat,MaCN) ";
                strCmd += " Values('" + cbbNhomKHTN.SelectedValue.ToString() + "','" + cbbNV.SelectedValue.ToString() + "','" + thangBD + "/" + ngayBD + "/" + namBD + "','" + thangKT + "/" + ngayKT + "/" + namKT;
                strCmd += "',N'" + txtGhichu.Text.Trim() + "','" + Thongtindangnhap.user_id + "','" + ngaycapnhat + "','" + Thongtindangnhap.macn + "')";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layDanhsach();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbNhomKHTN.Focus();
                txtGhichu.Text = "";
            }
            else
            {
                MessageBox.Show("Nhóm khách hàng này đã tồn tại.", "Cảnh báo");
                cbbNhomKHTN.Focus();
                return;                       
            }
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            //frmThamkhao_KHTN frmKH = new frmThamkhao_KHTN();
            //frmKH.ShowDialog();

            //txtKH.Text = frmThamkhao_KHTN.maKH;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            //if (txtKH.Text == "")
            //{
            //    MessageBox.Show("Chưa chọn mã KH.", "Thông báo");
            //    txtKH.Focus();
            //    return;
            //}

            string ngaybatdau, ngayketthuc;
            ngaybatdau = dtpNgayBD.Text;
            ngayketthuc = dtpNgayKT.Text;

            string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
            ngayBD = ngaybatdau.Substring(0, 2);
            thangBD = ngaybatdau.Substring(3, 2);
            namBD = ngaybatdau.Substring(6, 4);
            ngayKT = ngayketthuc.Substring(0, 2);
            thangKT = ngayketthuc.Substring(3, 2);
            namKT = ngayketthuc.Substring(6, 4);

            string ngaycapnhat = "";
            string nam, thang, ngay, gio, phut, giay, miligiay;
            nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
            thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
            ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
            gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
            phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
            giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
            miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
            ngaycapnhat = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

            strCmd = "SELECT * FROM KH_NV WHERE MaNhom='" + cbbNhomKHTN.SelectedValue.ToString() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count > 0)
            {
                strCmd = "Update KH_NV ";
                strCmd += " Set MaNV='" + cbbNV.SelectedValue.ToString() + "',Ngaybatdau='" + thangBD + "/" + ngayBD + "/" + namBD + "',Ngayketthuc='" + thangKT + "/" + ngayKT + "/" + namKT;
                strCmd += "',Ghichu=N'" + txtGhichu.Text.Trim() + "',Nguoicapnhat='" + Thongtindangnhap.user_id + "',Ngaycapnhat='" + ngaycapnhat + "' ";
                strCmd += " Where MaNhom='" + cbbNhomKHTN.SelectedValue.ToString() + "' ";

                try
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layDanhsach();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                cbbNhomKHTN.Focus();
                txtGhichu.Text = "";
            }
            else
            {
                MessageBox.Show("Nhóm khách hàng này không tồn tại.", "Cảnh báo");
                cbbNhomKHTN.Focus();
                return;                         
            }
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbNhomKHTN.Text = dgvDanhsach.CurrentRow.Cells["Tên nhóm"].Value.ToString();
                cbbNV.Text = dgvDanhsach.CurrentRow.Cells["CB quản lý"].Value.ToString();
                txtGhichu.Text = dgvDanhsach.CurrentRow.Cells["Ghi chú"].Value.ToString();
                dtpNgayBD.Text = dgvDanhsach.CurrentRow.Cells["Ngày bắt đầu"].Value.ToString();
                dtpNgayKT.Text = dgvDanhsach.CurrentRow.Cells["Ngày kết thúc"].Value.ToString();

                maNhomKHTN = dgvDanhsach.CurrentRow.Cells["Mã nhóm"].Value.ToString();
                tenNhomKHTN = dgvDanhsach.CurrentRow.Cells["Tên nhóm"].Value.ToString();
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvDanhsach.RowCount > 0)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa thông tin khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string maNhom = dgvDanhsach.CurrentRow.Cells["Mã nhóm"].Value.ToString();
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        strCmd = "Delete from KH_NV Where MaNhom='" + maNhom + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();

                        layDanhsach();

                        MessageBox.Show("Đã xóa", "Thông báo");
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }

                    txtGhichu.Text = "";                    
                }
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            frmKH_NV_Detail frmKH = new frmKH_NV_Detail();
            frmKH.ShowDialog();

            //txtKH.Text = frmThamkhao_KHTN.maKH;
        }

        private void cbbNhomKHTN_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}