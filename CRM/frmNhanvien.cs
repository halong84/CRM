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
        NHANVIENBUS nvbus = new NHANVIENBUS();
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

            cbbMaCN.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbPhong.DropDownStyle = ComboBoxStyle.DropDownList;            
        }

        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            layDS_CN();
            layDS_Phong();
            layDS_Chucvu();
            layDanhsach();
            if (Thongtindangnhap.group_list.Contains("G_AD"))
            {
                btnModify.Enabled = true;
                btnAdd.Enabled = true;
                btnDel.Enabled = true;
            }
            else
            {
                btnModify.Enabled = false;
                btnAdd.Enabled = false;
                btnDel.Enabled = false;
            }
        }

        private void layDS_CN()
        {
            cbbMaCN.Items.Clear();
            cbbMaCN.Refresh();

            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            //DataRow first_row = dt.NewRow();
            //first_row[0] = "Tất cả";
            //dt.Rows.InsertAt(first_row, 0);

            //cbbMaCN.DataSource = dt;
            cbbMaCN.DisplayMember = "MACN";
            cbbMaCN.ValueMember = "MACN";
            cbbMaCN.DataSource = dt;

            cbbMaCN.SelectedValue = Thongtindangnhap.macn;
            //MessageBox.Show(cbbMaCN.SelectedValue.ToString());
            //strCmd = "SELECT * FROM CHINHANH WHERE KICH_HOAT = '1' ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
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
            //        arrPhong.Add(dtResult.Rows[i]["MaPB"].ToString());
            //        //arrChucvu.Add(dtResult.Rows[i]["Chucvu"].ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}

            //cbbMaCN.Text = Thongtindangnhap.macn;
        }

        private void layDS_Phong()
        {
            cbbPhong.DataSource = null;
            cbbPhong.Items.Clear();
            cbbPhong.Refresh();

            DataTable dt = pbbus.DANH_SACH_PHONG_BAN(cbbMaCN.Text);
            //MessageBox.Show(cbbMaCN.SelectedValue.ToString());
            //DataRow first_row = dt.NewRow();
            //first_row[0] = "Tất cả";
            //dt.Rows.InsertAt(first_row, 0);

            //cbbPhong.DataSource = dt;
            //cbbPhong.SelectedIndexChanged -= new EventHandler(cbbPhong_SelectedIndexChanged);
            cbbPhong.DisplayMember = "TENPB";
            cbbPhong.ValueMember = "MAPB";            
            cbbPhong.DataSource = dt;
            cbbPhong.SelectedValue = dt.Rows[0]["MAPB"].ToString();
            //cbbPhong.SelectedIndexChanged += new EventHandler(cbbPhong_SelectedIndexChanged);
            //if (dt.Rows.Count > 0)
            //{
            //    cbbPhong.SelectedIndex = 0;
            //}
            //MessageBox.Show(cbbPhong.SelectedValue.ToString());
            //strCmd = "SELECT * FROM PHONGBAN WHERE MaCN='" + cbbMaCN.Text.Trim() + "' ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
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
            //        //arrPhong.Add(dtResult.Rows[i]["MaPB"].ToString());
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
            cbbChucvu.Items.Clear();
            cbbChucvu.Refresh();
            strCmd = "SELECT DISTINCT Chucvu FROM NHANVIEN ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
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
            dgvDanhsach.DataSource = null;
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            dtDanhsach.Columns.AddRange
                (
                new DataColumn[9] 
                    { 
                        new DataColumn("STT", typeof(int)),
                        new DataColumn("Mã NV", typeof(string)),
                        new DataColumn("Họ tên", typeof(string)),
                        new DataColumn("Chức vụ", typeof(string)),
                        new DataColumn("Mã CN", typeof(string)),
                        new DataColumn("Phòng ban", typeof(string)),
                        new DataColumn("Giấy ủy quyền", typeof(string)),
                        new DataColumn("Ngày sinh", typeof(string)),
                        new DataColumn("Tình trạng công việc", typeof(string))
                    }
                );
           
            
            //strCmd = "SELECT DISTINCT nv.*, pb.TenPB FROM NHANVIEN nv INNER JOIN PHONGBAN pb ON (nv.MAPB = pb.MAPB AND nv.MaCN=pb.maCN) ";
            ////strCmd += " Where nv.MaCN='" + cbbMaCN.Text.Trim() + "' and nv.MaPB='" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "' ";
            //strCmd += " Where nv.MaCN='" + cbbMaCN.Text.Trim() + "' and nv.MaPB='" + cbbPhong.SelectedValue.ToString() + "' ";
            ////strCmd += " and nv.Chucvu='" + cbbChucvu.Text.Trim() + "'";
            //strCmd += " ORDER BY nv.MaCN";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteReader();
            //    DataAccess.conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);

            dtResult = nvbus.DANH_SACH_NV_THEO_CN_PB(cbbMaCN.Text, cbbPhong.SelectedValue.ToString());

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
                    row[6] = dtResult.Rows[i]["UYQUYEN"].ToString();
                    row[7] = dtResult.Rows[i]["NGAYSINH"].ToString().Substring(0, 2) + "/" + dtResult.Rows[i]["NGAYSINH"].ToString().Substring(3, 2) + "/" + dtResult.Rows[i]["NGAYSINH"].ToString().Substring(6, 4);
                    if (Convert.ToBoolean(dtResult.Rows[i]["HOATDONG"]) == true)
                    {
                        row[8] = "Đang làm việc";
                    }
                    else
                    {
                        row[8] = "Đã nghỉ";
                    }
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
            dgvDanhsach.Columns[6].Width = 250;
            dgvDanhsach.Columns[7].Width = 50;
            dgvDanhsach.Columns[8].Width = 50;  
        }

        //private void cbbMaCN_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cbbChucvu.Text = "";
        //    layDS_Phong();
        //    //layDS_Chucvu();
        //    layDanhsach();
        //}

        //private void cbbPhong_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cbbChucvu.Text = "";
        //    //layDS_Chucvu();
        //    layDanhsach();
        //}

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbMaCN.Text = dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString();
                cbbPhong.Text = dgvDanhsach.CurrentRow.Cells["Phòng ban"].Value.ToString();
                txtMaNV.Text = dgvDanhsach.CurrentRow.Cells["Mã NV"].Value.ToString();
                txtTenNV.Text = dgvDanhsach.CurrentRow.Cells["Họ tên"].Value.ToString();
                cbbChucvu.Text = dgvDanhsach.CurrentRow.Cells["Chức vụ"].Value.ToString();
                txtGiayuyquyen.Text = dgvDanhsach.CurrentRow.Cells["Giấy ủy quyền"].Value.ToString();
                txtNgaysinh.Text = dgvDanhsach.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                txtHoatdong.Text = dgvDanhsach.CurrentRow.Cells["Tình trạng công việc"].Value.ToString();
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
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
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
                strCmd = "Insert into Nhanvien(MaNV, HoTen, Chucvu, MaPB, MaCN, UYQUYEN) ";
                //strCmd += " Values('" + txtMaNV.Text.Trim() + "',N'" + txtTenNV.Text.Trim() + "',N'" + cbbChucvu.Text.Trim() + "','" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "','" + cbbMaCN.Text.Trim() + "')";
                strCmd += " Values('" + txtMaNV.Text.Trim() + "',N'" + txtTenNV.Text.Trim() + "',N'" + cbbChucvu.Text.Trim() + "','" + cbbPhong.SelectedValue.ToString() + "','" + cbbMaCN.Text.Trim() + "','" + txtGiayuyquyen.Text.Trim()  + "')";

                try
                {
                    DataAccess.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
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
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count != 0)
            {
                strCmd = "Update Nhanvien";
                //strCmd += " Set HoTen=N'" + txtTenNV.Text.Trim() + "',Chucvu=N'" + cbbChucvu.Text.Trim() + "',MaPB='" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "',MaCN='" + cbbMaCN.Text.Trim() + "' ";
                strCmd += " Set HoTen=N'" + txtTenNV.Text.Trim() + "',Chucvu=N'" + cbbChucvu.Text.Trim() + "',MaPB='" + cbbPhong.SelectedValue.ToString() + "',MaCN='" + cbbMaCN.Text.Trim() + "', UYQUYEN=N'" + txtGiayuyquyen.Text  + "' ";
                strCmd += " Where MaNV='" + txtMaNV.Text.Trim() + "' ";
                try
                {
                    DataAccess.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
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
                    DataAccess.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, DataAccess.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
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

        private void cbbMaCN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbChucvu.Text = "";
            //cbbPhong.DataSource = null;
            //cbbPhong.Items.Clear();
            //cbbPhong.ResetText();
            layDS_Phong();
        }

        
        private void cbbPhong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbChucvu.Text = "";
            //layDS_Chucvu();
            layDanhsach();
        }

        //private void cbbPhong_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    cbbChucvu.Text = "";
        //    //layDS_Chucvu();
        //    layDanhsach();
        //}
 
    }
}