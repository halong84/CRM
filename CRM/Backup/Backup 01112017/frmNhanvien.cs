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
    public partial class frmNhanvien : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        PhongBUS pbbus = new PhongBUS();
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrPhong = new ArrayList();
        ArrayList arrChucvu = new ArrayList();
        
        public frmNhanvien()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;

            //cbbMaCN.DataSource = null;
            cbbMaCN.DropDownStyle = ComboBoxStyle.DropDownList;

            //cbbPhong.DataSource = null;
            cbbPhong.DropDownStyle = ComboBoxStyle.DropDownList;

            layDS_CN();
            layDS_Phong();
            layDS_Chucvu();
            layDanhsach(); 
        }

        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            //layDS_CN();
            //layDS_Phong();
            //layDS_Chucvu();
            //layDanhsach();            
        }

        private void layDS_CN()
        {
            //cbbMaCN.DataSource = null;
            //cbbMaCN.Items.Clear();
            //cbbMaCN.Refresh();

            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            //DataRow first_row = dt.NewRow();
            //first_row[0] = "Tất cả";
            //dt.Rows.InsertAt(first_row, 0);

            cbbMaCN.DataSource = dt;
            cbbMaCN.DisplayMember = "MACN";
            cbbMaCN.ValueMember = "MACN";
            //cbbMaCN.DataSource = dt;

            cbbMaCN.SelectedValue = Thongtindangnhap.macn;


            //strCmd = "SELECT * FROM CHINHANH ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    frmMain.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    frmMain.conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        cbbMaCN.Items.Add(dtResult.Rows[i]["MaCN"].ToString());
            //        //arrPhong.Add(dtResult.Rows[i]["MaPB"].ToString());
            //        //arrChucvu.Add(dtResult.Rows[i]["Chucvu"].ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}

            //cbbMaCN.Text = frmMain.cn;
        }

        private void layDS_Phong()
        {
            //cbbPhong.DataSource = null;
            //cbbPhong.Items.Clear();
            //cbbPhong.Refresh();

            DataTable dt = pbbus.DANH_SACH_PHONG_BAN(cbbMaCN.Text);
            //DataRow first_row = dt.NewRow();
            //first_row[0] = "Tất cả";
            //dt.Rows.InsertAt(first_row, 0);

            cbbPhong.DataSource = dt;
            cbbPhong.DisplayMember = "TENPB";
            cbbPhong.ValueMember = "MAPB";
            
            if (dt.Rows.Count > 0)
            {
                cbbPhong.SelectedIndex = 0;
            }
            
            

            //strCmd = "SELECT * FROM PHONGBAN WHERE MACN='" + cbbMaCN.Text.Trim() + "'";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    frmMain.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    frmMain.conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            //dtResult = ds.Tables[0];

            //int iRows = dtResult.Rows.Count;
            //for (int i = 0; i < iRows; i++)
            //{
            //    try
            //    {
            //        cbbPhong.Items.Add(dtResult.Rows[i]["TenPB"].ToString());
            //        arrPhong.Add(dtResult.Rows[i]["MaPB"].ToString());
            //        //arrChucvu.Add(dtResult.Rows[i]["Chucvu"].ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}

            //if (iRows > 0)
            //{
            //    cbbPhong.SelectedIndex = 0;
            //}
        }

        private void layDS_Chucvu()
        {
            //cbbChucvu.DataSource = null;
            cbbChucvu.Items.Clear();
            cbbChucvu.Refresh();
            strCmd = "SELECT DISTINCT Chucvu FROM NHANVIEN ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    cbbChucvu.Items.Add(dtResult.Rows[i]["Chucvu"].ToString());
                    //arrChucvu.Add(dtResult.Rows[i]["MaPB"].ToString());
                    //arrChucvu.Add(dtResult.Rows[i]["Chucvu"].ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //if (iRows > 0)
            //{
            //    cbbChucvu.SelectedIndex = 0;
            //}
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã NV", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chức vụ", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CN", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Phòng ban", typeof(string));
            dtDanhsach.Columns.Add(col);            
            
            strCmd = "SELECT DISTINCT nv.*, pb.TenPB FROM NHANVIEN nv INNER JOIN PHONGBAN pb ON (nv.MAPB = pb.MAPB AND nv.MaCN=pb.maCN) ";
            //strCmd += " Where nv.MaCN='" + cbbMaCN.Text.Trim() + "' and nv.MaPB='" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "' ";
            strCmd += " Where nv.MaCN='" + cbbMaCN.Text.Trim() + "' and nv.MaPB='" + cbbPhong.SelectedValue.ToString() + "' ";
            //strCmd += " and nv.Chucvu='" + cbbChucvu.Text.Trim() + "'";
            strCmd += " ORDER BY nv.MaCN";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                    row[1] = dtResult.Rows[i]["MaNV"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Chucvu"].ToString();
                    row[4] = dtResult.Rows[i]["MaCN"].ToString();
                    row[5] = dtResult.Rows[i]["TenPB"].ToString();                                        
                    dtDanhsach.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Width = 100;
            dgvDanhsach.Columns[2].Width = 220;
            dgvDanhsach.Columns[3].Width = 150;
            dgvDanhsach.Columns[4].Width = 80;
            dgvDanhsach.Columns[5].Width = 250;            
        }

        private void cbbMaCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(cbbMaCN.SelectedValue.ToString());
            cbbChucvu.Text = "";
            cbbPhong.DataSource = null;
            layDS_Phong();
            //layDS_Chucvu();
            layDanhsach();
        }

        private void cbbPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbChucvu.Text = "";
            //layDS_Chucvu();
            layDanhsach();
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMaCN.Text = dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString();
                cbbPhong.Text = dgvDanhsach.CurrentRow.Cells["Phòng ban"].Value.ToString();
                txtMaNV.Text = dgvDanhsach.CurrentRow.Cells["Mã NV"].Value.ToString();
                txtTenNV.Text = dgvDanhsach.CurrentRow.Cells["Họ tên"].Value.ToString();
                cbbChucvu.Text = dgvDanhsach.CurrentRow.Cells["Chức vụ"].Value.ToString();

            }
            catch { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhân viên.", "Thông báo");
                txtMaNV.Focus();
                return;
            }
            
            strCmd = "SELECT * FROM Nhanvien WHERE MaNV='" + txtMaNV.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Insert into Nhanvien(MaNV, HoTen, Chucvu, MaPB, MaCN) ";
                strCmd += " Values('" + txtMaNV.Text.Trim() + "',N'" + txtTenNV.Text.Trim() + "',N'" + cbbChucvu.Text.Trim() + "','" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "','" + cbbMaCN.Text.Trim() + "')";

                try
                {
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    //layDS_MaPB();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                txtMaNV.Focus();
                txtMaNV.Text = "";
                txtTenNV.Text = "";
                cbbChucvu.Text = "";
            }
            else
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại.", "Cảnh báo");
                txtMaNV.Focus();
                //txtTennhom.Text = "";
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhân viên.", "Thông báo");
                txtMaNV.Focus();
                return;
            }

            strCmd = "SELECT * FROM Nhanvien WHERE MaNV='" + txtMaNV.Text.Trim() + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                strCmd = "Update Nhanvien(MaNV, HoTen, Chucvu, MaPB, MaCN) ";
                strCmd += " Set HoTen=N'" + txtTenNV.Text.Trim() + "',Chucvu=N'" + cbbChucvu.Text.Trim() + "',MaPB='" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "',MaCN='" + cbbMaCN.Text.Trim() + "' ";
                strCmd += " Where MaNV='" + txtMaNV.Text.Trim() + "' ";

                try
                {
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    //layDS_MaPB();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                txtMaNV.Focus();
                txtMaNV.Text = "";
                txtTenNV.Text = "";
                cbbChucvu.Text = "";
            }
            else
            {
                MessageBox.Show("Mã nhân viên này chưa tồn tại.", "Cảnh báo");
                txtMaNV.Focus();
                //txtTennhom.Text = "";
                return;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                try
                {
                    strCmd = "Delete from Nhanvien Where MaNV='" + dgvDanhsach.CurrentRow.Cells["Mã NV"].Value.ToString() + "' ";
                    frmMain.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    //layDS_MaPB();
                    MessageBox.Show("Đã xóa", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                txtMaNV.Text = "";
                txtTenNV.Text = "";
                cbbChucvu.Text = "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbMaCN_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            MessageBox.Show(cbbMaCN.SelectedValue.ToString());
            //cbbChucvu.Text = "";
            //cbbPhong.DataSource = null;
            //layDS_Phong();
            //layDS_Chucvu();
            //layDanhsach();
        }

        
    }
}