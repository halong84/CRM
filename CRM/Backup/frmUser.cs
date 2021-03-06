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
using mahoachuoi;

namespace CRM
{
    public partial class frmUser : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrGroup = new ArrayList();
        ArrayList arrTo = new ArrayList();
        ArrayList arrPhong = new ArrayList();
        //bool flag = false;

        char key = 'P';
        //string filename="";
        
        mahoachuoi.Mahoa mahoa;
        //public static frmLogin me;

        public frmUser()
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

        private void frmUser_Load(object sender, EventArgs e)
        {
            //tao doi tuong de ma hoa
            mahoa = new Mahoa(key);
            //me = this;
            
            if (frmMain.cn == "4800")
            {
                cbbMaCN.Enabled = true;
            }
            else
            {
                cbbMaCN.Enabled = false;
            }

            layDS_CN();
            layDS_Phong();
            layDS_Chucvu();
            layDS_Nhom();
            layDanhsach();
        }

        private void layDS_Nhom()
        {
            //arrGroup.Clear();
            //arrGroupName = new ArrayList();
            lstFrom.Items.Clear();
            strCmd = "select * from _group Order by Group_Name ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    lstFrom.Items.Add(dtResult.Rows[i]["group_Name"].ToString());
                    arrGroup.Add(dtResult.Rows[i]["group_ID"].ToString());
                }
                catch { }
            }                      
        }

        private void layDS_CN()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT macn,tencn from Chinhanh Where MaCN<>'9999' order by macn";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbMaCN.DataSource = dt;
            cbbMaCN.DisplayMember = "tencn";
            cbbMaCN.ValueMember = "macn";

            cbbMaCN.SelectedValue = frmMain.cn;
        }        

        private void layDS_Phong()
        {
            DataTable dt = new DataTable();
            strCmd = "SELECT * FROM PHONGBAN WHERE MaCN='" + cbbMaCN.SelectedValue.ToString() + "' ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbPhong.DataSource = dt;
            cbbPhong.DisplayMember = "tenpb";
            cbbPhong.ValueMember = "mapb";            
        }        

        private void layDS_Chucvu()
        {
            cbbChucvu.Items.Clear();
            cbbChucvu.Refresh();
            strCmd = "SELECT DISTINCT Chucvu FROM _USER ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    cbbChucvu.Items.Add(dtResult.Rows[i]["Chucvu"].ToString());
                }
                catch { }
            }
        }

        private void layDS_TenND()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CN", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đăng nhập", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mật khẩu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nhóm người dùng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Phòng ban", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chức vụ", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "Select nv.*, pb.TenPB from _USER as nv left join PHONGBAN as pb on (nv.MACN=pb.MACN and nv.MAPB=pb.MAPB) ";
            strCmd += " Where  nv.MACN='" + cbbMaCN.SelectedValue.ToString() + "' and TenNV like N'%" + txtSTenND.Text.Trim() + "%' ";
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    row[1] = dtResult.Rows[i]["MaCN"].ToString();
                    row[2] = dtResult.Rows[i]["User_ID"].ToString();
                    row[3] = dtResult.Rows[i]["User_Pass"].ToString();
                    row[4] = dtResult.Rows[i]["TenNV"].ToString();

                    String groupList = dtResult.Rows[i]["group_list"].ToString();
                    string[] groupID = groupList.Split(',');
                    string groupName = "";
                    foreach (string id in groupID)
                    {
                        string str = "select * from _group Where Group_ID='" + id + "' ";

                        SqlDataAdapter adapter1 = new SqlDataAdapter();
                        try
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            adapter1.SelectCommand = new SqlCommand(str, frmMain.conn);
                            adapter1.SelectCommand.ExecuteReader();
                            frmMain.conn.Close();
                        }
                        catch
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                        }

                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        DataTable dtResult1 = ds1.Tables[0];

                        try
                        {
                            if (groupName == "")
                            {
                                groupName = dtResult1.Rows[0]["group_Name"].ToString();
                            }
                            else
                            {
                                groupName += "," + dtResult1.Rows[0]["group_Name"].ToString();
                            }
                        }
                        catch { }
                    }
                    row[5] = groupName;
                    row[6] = dtResult.Rows[i]["TenPB"].ToString();
                    row[7] = dtResult.Rows[i]["Chucvu"].ToString();
                    row[8] = dtResult.Rows[i]["Ghichu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 80;
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[3].Visible = false;
            dgvDanhsach.Columns[4].Width = 200;
            dgvDanhsach.Columns[5].Width = 300;
            dgvDanhsach.Columns[6].Width = 200;
            dgvDanhsach.Columns[7].Width = 150;
            dgvDanhsach.Columns[8].Width = 200;
        }

        private void layDS_TenDN()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CN", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đăng nhập", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mật khẩu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nhóm người dùng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Phòng ban", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chức vụ", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "Select nv.*, pb.TenPB from _USER as nv left join PHONGBAN as pb on (nv.MACN=pb.MACN and nv.MAPB=pb.MAPB) ";
            strCmd += " Where nv.MACN='" + cbbMaCN.SelectedValue.ToString() + "' and nv.USER_ID like '%" + txtSTenDN.Text.Trim() + "%' ";
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    row[1] = dtResult.Rows[i]["MaCN"].ToString();
                    row[2] = dtResult.Rows[i]["User_ID"].ToString();
                    row[3] = dtResult.Rows[i]["User_Pass"].ToString();
                    row[4] = dtResult.Rows[i]["TenNV"].ToString();

                    String groupList = dtResult.Rows[i]["group_list"].ToString();
                    string[] groupID = groupList.Split(',');
                    string groupName = "";
                    foreach (string id in groupID)
                    {
                        string str = "select * from _group Where Group_ID='" + id + "' ";

                        SqlDataAdapter adapter1 = new SqlDataAdapter();
                        try
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            adapter1.SelectCommand = new SqlCommand(str, frmMain.conn);
                            adapter1.SelectCommand.ExecuteReader();
                            frmMain.conn.Close();
                        }
                        catch
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                        }
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        DataTable dtResult1 = ds1.Tables[0];

                        try
                        {
                            if (groupName == "")
                            {
                                groupName = dtResult1.Rows[0]["group_Name"].ToString();
                            }
                            else
                            {
                                groupName += "," + dtResult1.Rows[0]["group_Name"].ToString();
                            }
                        }
                        catch { }
                    }
                    row[5] = groupName;
                    row[6] = dtResult.Rows[i]["TenPB"].ToString();
                    row[7] = dtResult.Rows[i]["Chucvu"].ToString();
                    row[8] = dtResult.Rows[i]["Ghichu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 80;
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[3].Visible = false;
            dgvDanhsach.Columns[4].Width = 200;
            dgvDanhsach.Columns[5].Width = 300;
            dgvDanhsach.Columns[6].Width = 200;
            dgvDanhsach.Columns[7].Width = 150;
            dgvDanhsach.Columns[8].Width = 200;
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã CN", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên đăng nhập", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mật khẩu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dtDanhsach.Columns.Add(col);            
            col = new DataColumn("Nhóm người dùng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Phòng ban", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chức vụ", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);

            //strCmd = "Select nv.*, pb.TenPB from _USER as nv left join PHONGBAN as pb on (nv.MACN=pb.MACN and nv.MAPB=pb.MAPB) ";
            //strCmd += " Where nv.MACN='" + cbbMaCN.SelectedValue.ToString() + "' and nv.maPB='" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "' ";
            strCmd = "Select nv.*, pb.TenPB from _USER as nv left join PHONGBAN as pb on (nv.MACN=pb.MACN and nv.MAPB=pb.MAPB) ";
            strCmd += " Where nv.MACN='" + cbbMaCN.SelectedValue.ToString() + "' and nv.maPB='" + cbbPhong.SelectedValue.ToString() + "' ";
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    row[1] = dtResult.Rows[i]["MaCN"].ToString();
                    row[2] = dtResult.Rows[i]["User_ID"].ToString();

                    string pass = dtResult.Rows[i]["User_Pass"].ToString();
                    pass = mahoa.mahoa(pass);
                    row[3] = pass;

                    row[4] = dtResult.Rows[i]["TenNV"].ToString();

                    String groupList = dtResult.Rows[i]["group_list"].ToString();
                    string[] groupID = groupList.Split(',');
                    string groupName = "";
                    foreach (string id in groupID)
                    {
                        string str = "select * from _group Where Group_ID='" + id + "' ";

                        SqlDataAdapter adapter1 = new SqlDataAdapter();
                        try
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            adapter1.SelectCommand = new SqlCommand(str, frmMain.conn);
                            adapter1.SelectCommand.ExecuteReader();
                            frmMain.conn.Close();
                        }
                        catch
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                        }
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        DataTable dtResult1 = ds1.Tables[0];

                        try
                        {
                            if (groupName == "")
                            {
                                groupName = dtResult1.Rows[0]["group_Name"].ToString();
                            }
                            else
                            {
                                groupName += "," + dtResult1.Rows[0]["group_Name"].ToString();
                            }
                        }
                        catch { }
                    }
                    row[5] = groupName;
                    row[6] = dtResult.Rows[i]["TenPB"].ToString();
                    row[7] = dtResult.Rows[i]["Chucvu"].ToString();
                    row[8] = dtResult.Rows[i]["Ghichu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 50;
            dgvDanhsach.Columns[1].Width = 80;
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[3].Visible = false;
            dgvDanhsach.Columns[4].Width = 200;            
            dgvDanhsach.Columns[5].Width = 300;
            dgvDanhsach.Columns[6].Width = 200;
            dgvDanhsach.Columns[7].Width = 150;
            dgvDanhsach.Columns[8].Width = 200;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void btnTo_Click(object sender, EventArgs e)
        {
            string item = "";
            int numberSelected = lstFrom.SelectedItems.Count;
            for (int i = 0; i < numberSelected; i++)
            {
                item = lstFrom.SelectedItems[0].ToString();
                if (item != "")
                {
                    lstTo.Items.Add(item);
                    //lstTo.Sorted = true;
                    lstFrom.Items.Remove(item);
                }
            }
        }

        private void btnToAll_Click(object sender, EventArgs e)
        {
            lstTo.Items.AddRange(lstFrom.Items);
            //lstTo.Sorted = true;
            lstFrom.Items.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            string item = "";
            int numberSelected = lstTo.SelectedItems.Count;
            for (int i = 0; i < numberSelected; i++)
            {
                item = lstTo.SelectedItems[0].ToString();
                if (item != "")
                {
                    lstFrom.Items.Add(item);
                    //lstFrom.Sorted = true;
                    lstTo.Items.Remove(item);
                }
            }
        }

        private void btnBackAll_Click(object sender, EventArgs e)
        {
            lstFrom.Items.AddRange(lstTo.Items);
            //lstFrom.Sorted = true;
            lstTo.Items.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                MessageBox.Show("Chưa nhập tên đăng nhập.", "Thông báo");
                txtUser.Focus();
                return;
            }
            else if (txtPass.Text == "")
            {
                MessageBox.Show("Chưa nhập mật khẩu.", "Thông báo");
                txtPass.Focus();
                return;
            }
            else if (lstTo.Items.Count == 0)
            {
                MessageBox.Show("Chưa chọn nhóm người dùng.", "Thông báo");
                lstFrom.Focus();
                return;
            }

            string user = this.txtUser.Text.Trim();
            string pass = this.txtPass.Text.Trim();
            
            strCmd = "SELECT * FROM _User WHERE User_ID='" + user + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                string groupID = "";
                foreach (object item in lstTo.Items)
                {
                    string str = "select * from _group Where Group_Name=N'" + item.ToString() + "' ";

                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    try
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        adapter1.SelectCommand = new SqlCommand(str, frmMain.conn);
                        adapter1.SelectCommand.ExecuteReader();
                        frmMain.conn.Close();
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);
                    DataTable dtResult1 = ds1.Tables[0];

                    if (groupID == "")
                    {
                        groupID = dtResult1.Rows[0]["group_ID"].ToString();
                    }
                    else
                    {
                        groupID += "," + dtResult1.Rows[0]["group_ID"].ToString();
                    }
                }
                
                pass = mahoa.mahoa(pass);

                strCmd = "Insert into _User(User_ID,User_Pass,Group_List,Ghichu,TenNV,MaCN,Chucvu,MaPB) ";
                //strCmd += " Values('" + user + "','" + pass + "','" + groupID + "',N'" + txtGhichu.Text.Trim() + "',N'" + txtHoten.Text.Trim() + "','" + cbbMaCN.Text.Trim() + "',N'" + cbbChucvu.Text.Trim() + "','" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "')";
                strCmd += " Values('" + user + "','" + pass + "','" + groupID + "',N'" + txtGhichu.Text.Trim() + "',N'" + txtHoten.Text.Trim() + "','" + cbbMaCN.SelectedValue.ToString() + "',N'" + cbbChucvu.Text.Trim() + "','" + cbbPhong.SelectedValue.ToString() + "')";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtUser.Focus();
                txtUser.Text = "";
                txtPass.Text = "";
                txtHoten.Text = "";
                txtGhichu.Text = "";
                cbbChucvu.Text = "";
                if (lstTo.Items.Count > 0)
                {
                    lstFrom.Items.AddRange(lstTo.Items);
                    lstFrom.Sorted = true;
                    lstTo.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại.", "Cảnh báo");
                txtUser.Focus();
                return;
            }            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                MessageBox.Show("Chưa nhập tên đăng nhập.", "Thông báo");
                txtUser.Focus();
                return;
            }
            else if (txtPass.Text == "")
            {
                MessageBox.Show("Chưa nhập mật khẩu.", "Thông báo");
                txtPass.Focus();
                return;
            }
            else if (lstTo.Items.Count == 0)
            {
                MessageBox.Show("Chưa chọn nhóm người dùng.", "Thông báo");
                lstFrom.Focus();
                return;
            }

            string user = this.txtUser.Text.Trim();
            string pass = this.txtPass.Text.Trim();

            strCmd = "SELECT * FROM _User WHERE User_ID='" + user + "' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            if (dtResult.Rows.Count > 0)
            {
                //String groupList = lstTo.;
                //string[] groupID = groupList.Split(',');
                string groupID = "";
                foreach (object item in lstTo.Items)
                {
                    string str = "select * from _group Where Group_Name=N'" + item.ToString() + "' ";

                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    try
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        adapter1.SelectCommand = new SqlCommand(str, frmMain.conn);
                        adapter1.SelectCommand.ExecuteReader();
                        frmMain.conn.Close();
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);
                    DataTable dtResult1 = ds1.Tables[0];

                    if (groupID == "")
                    {
                        groupID = dtResult1.Rows[0]["group_ID"].ToString();
                    }
                    else
                    {
                        groupID += "," + dtResult1.Rows[0]["group_ID"].ToString();
                    }
                }

                pass = mahoa.mahoa(pass);

                strCmd = "Update _User ";
                //strCmd += " Set User_pass='" + pass + "',Group_List='" + groupID + "',Ghichu=N'" + txtGhichu.Text.Trim() + "',TenNV=N'" + txtHoten.Text.Trim() + "',MaCN='" + cbbMaCN.Text.Trim();
                //strCmd += "',Chucvu=N'" + cbbChucvu.Text.Trim() + "',MaPB='" + arrPhong[cbbPhong.Items.IndexOf(cbbPhong.Text.Trim())].ToString() + "'";
                strCmd += " Set User_pass='" + pass + "',Group_List='" + groupID + "',Ghichu=N'" + txtGhichu.Text.Trim() + "',TenNV=N'" + txtHoten.Text.Trim() + "',MaCN='" + cbbMaCN.SelectedValue.ToString();
                strCmd += "',Chucvu=N'" + cbbChucvu.Text.Trim() + "',MaPB='" + cbbPhong.SelectedValue.ToString() + "'";
                
                strCmd += " Where User_ID='" + user + "'";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtUser.Focus();
                txtUser.Text = "";
                txtPass.Text = "";
                txtHoten.Text = "";
                txtGhichu.Text = "";
                cbbChucvu.Text = "";
                if (lstTo.Items.Count > 0)
                {
                    lstFrom.Items.AddRange(lstTo.Items);
                    lstFrom.Sorted = true;
                    lstTo.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập này không tồn tại.", "Cảnh báo");
                txtUser.Focus();
                return;
            }            
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //flag = true;
            layDS_Nhom();
            try
            {
                cbbMaCN.SelectedValue = dgvDanhsach.CurrentRow.Cells["Mã CN"].Value.ToString();

                txtUser.Text = dgvDanhsach.CurrentRow.Cells["Tên đăng nhập"].Value.ToString();
                txtPass.Text = dgvDanhsach.CurrentRow.Cells["Mật khẩu"].Value.ToString();
                txtHoten.Text = dgvDanhsach.CurrentRow.Cells["Họ tên"].Value.ToString();
                txtGhichu.Text = dgvDanhsach.CurrentRow.Cells["Ghi chú"].Value.ToString();
                cbbChucvu.Text = dgvDanhsach.CurrentRow.Cells["Chức vụ"].Value.ToString();
                cbbPhong.Text = dgvDanhsach.CurrentRow.Cells["Phòng ban"].Value.ToString();
                //lstFrom.Items.Clear();
                lstTo.Items.Clear();
                String groupList = dgvDanhsach.CurrentRow.Cells["Nhóm người dùng"].Value.ToString();
                string[] groupName = groupList.Split(',');
                foreach (string item in groupName)
                {
                    lstTo.Items.Add(item);
                    lstFrom.Items.Remove(item);
                }
            }
            catch { }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa dòng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                try
                {
                    strCmd = "Delete from _User Where User_ID='" + dgvDanhsach.CurrentRow.Cells["Tên đăng nhập"].Value.ToString() + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.DeleteCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    layDanhsach();
                    layDS_Nhom();
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                txtUser.Text = "";
                txtPass.Text = "";
                txtHoten.Text = "";
                txtGhichu.Text = "";
                cbbChucvu.Text = "";
                lstTo.Items.Clear();
            }
        }

        private void cbbMaCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbbChucvu.Text = "";
            //txtUser.Text = "";
            //txtPass.Text = "";
            //txtHoten.Text = "";
            //txtGhichu.Text = "";
            //layDS_Phong();
            //lstFrom.Items.AddRange(lstTo.Items);
            //lstFrom.Sorted = true;
            //lstTo.Items.Clear();
            //layDanhsach();
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void btnSTenDN_Click(object sender, EventArgs e)
        {
            layDS_TenDN();
        }

        private void btnSTenND_Click(object sender, EventArgs e)
        {
            layDS_TenND();
        }

        private void cbbPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbbChucvu.Text = "";
            //layDanhsach();
        }

        private void cbbPhong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbChucvu.Text = "";
            layDanhsach();
        }

        private void cbbMaCN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbChucvu.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            txtHoten.Text = "";
            txtGhichu.Text = "";
            layDS_Phong();
            lstFrom.Items.AddRange(lstTo.Items);
            lstFrom.Sorted = true;
            lstTo.Items.Clear();
            layDanhsach();
        }        
    }
}