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

namespace CRM
{
    public partial class frmMenu : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrGroup = new ArrayList();
        ArrayList arrTo = new ArrayList();

        public frmMenu()
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
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            layDS_Mamenu();
            layDS_ParentID();
            layDS_Nhom();
            layDanhsach();
        }

        private void layDS_Mamenu()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT distinct Menu_ID from _MENU";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbMamenu.DataSource = dt;
            cbbMamenu.DisplayMember = "Menu_ID";
            cbbMamenu.ValueMember = "Menu_ID";            
        }

        private void layDS_ParentID()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT distinct Parent_ID from _MENU";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
            cbbParentID.DataSource = dt;
            cbbParentID.DisplayMember = "Parent_ID";
            cbbParentID.ValueMember = "Parent_ID";
        }

        private void layDS_Nhom()
        {
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

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Menu_ID", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Menu_title", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Parent_ID", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Deep", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Pos", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Group_list", typeof(string));
            dtDanhsach.Columns.Add(col);            
            col = new DataColumn("Form_name", typeof(string));
            dtDanhsach.Columns.Add(col);

            strCmd = "Select * from _MENU";
            
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
                    row[1] = dtResult.Rows[i]["Menu_ID"].ToString();
                    row[2] = dtResult.Rows[i]["Menu_title"].ToString();
                    row[3] = dtResult.Rows[i]["Parent_ID"].ToString();
                    row[4] = dtResult.Rows[i]["Deep"].ToString();
                    row[5] = dtResult.Rows[i]["Pos"].ToString();

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
                    row[6] = groupName;
                    row[7] = dtResult.Rows[i]["Form_name"].ToString();                    
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
            dgvDanhsach.Columns[1].Width = 120;
            dgvDanhsach.Columns[2].Width = 150;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].Width = 50;
            dgvDanhsach.Columns[5].Width = 50;
            dgvDanhsach.Columns[6].Width = 300;
            dgvDanhsach.Columns[7].Width = 150;            
        }

        private void cbbMamenu_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtTenmenu.Text = "";
            DataTable dt = new DataTable();
            String sCommand = "SELECT Menu_Title from _MENU Where Menu_ID='" + cbbMamenu.SelectedValue.ToString() + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            txtTenmenu.Text = dt.Rows[0][0].ToString();
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
            if (cbbMamenu.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập MENU_ID.", "Thông báo");
                cbbMamenu.Focus();
                return;
            }
            else if (cbbParentID.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập MENU_TITLE.", "Thông báo");
                txtTenmenu.Focus();
                return;
            }
            else if (cbbParentID.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập PARENT_ID.", "Thông báo");
                cbbParentID.Focus();
                return;
            }
            else if (txtDeep.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập DEEP.", "Thông báo");
                txtDeep.Focus();
                return;
            }
            else if (txtPos.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập POS.", "Thông báo");
                txtPos.Focus();
                return;
            }
            else if (lstTo.Items.Count == 0)
            {
                MessageBox.Show("Chưa chọn GROUP_LIST.", "Thông báo");
                lstFrom.Focus();
                return;
            }
            
            strCmd = "Select * from _MENU Where MENU_ID='" + cbbMamenu.Text.Trim() + "' ";

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

                strCmd = "Insert into _Menu(Menu_ID,Menu_title,Parent_ID,Deep,Pos,Group_list,Form_name) ";
                strCmd += " Values('" + cbbMamenu.Text.Trim() + "',N'" + txtTenmenu.Text.Trim() + "','" + cbbParentID.Text.Trim();
                strCmd += "','" + txtDeep.Text.Trim() + "','" + txtPos.Text.Trim() + "','" + groupID + "','" + txtTenform.Text.Trim() + "')";

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
                cbbMamenu.Focus();
                cbbMamenu.Text = "";
                txtTenmenu.Text = "";
                cbbParentID.Text = "";
                txtDeep.Text = "";
                txtPos.Text = "";
                if (lstTo.Items.Count > 0)
                {
                    lstFrom.Items.AddRange(lstTo.Items);
                    lstFrom.Sorted = true;
                    lstTo.Items.Clear();
                }
                txtTenform.Text = "";
            }
            else
            {
                MessageBox.Show("Menu này đã tồn tại.", "Cảnh báo");
                cbbMamenu.Focus();
                return;
            }            
        }        

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbbMamenu.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập MENU_ID.", "Thông báo");
                cbbMamenu.Focus();
                return;
            }
            else if (cbbParentID.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập MENU_TITLE.", "Thông báo");
                txtTenmenu.Focus();
                return;
            }
            else if (cbbParentID.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập PARENT_ID.", "Thông báo");
                cbbParentID.Focus();
                return;
            }
            else if (txtDeep.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập DEEP.", "Thông báo");
                txtDeep.Focus();
                return;
            }
            else if (txtPos.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập POS.", "Thông báo");
                txtPos.Focus();
                return;
            }
            else if (lstTo.Items.Count == 0)
            {
                MessageBox.Show("Chưa chọn GROUP_LIST.", "Thông báo");
                lstFrom.Focus();
                return;
            }
            
            strCmd = "Select * from _MENU Where MENU_ID='" + cbbMamenu.Text.Trim() + "' ";

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

                strCmd = "Update _Menu ";
                strCmd += " Set Menu_title=N'" + txtTenmenu.Text.Trim() + "',Parent_ID='" + cbbParentID.Text.Trim();
                strCmd += " ',Deep='" + txtDeep.Text.Trim() + "',Pos='" + txtPos.Text.Trim() + "',Group_List='" + groupID + "',Form_name='" + txtTenform.Text.Trim() + "' ";
                strCmd += " Where Menu_ID='" + cbbMamenu.Text.Trim() + "'";

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
                cbbMamenu.Focus();
                cbbMamenu.Text = "";
                txtTenmenu.Text = "";
                cbbParentID.Text = "";
                txtDeep.Text = "";
                txtPos.Text = "";
                if (lstTo.Items.Count > 0)
                {
                    lstFrom.Items.AddRange(lstTo.Items);
                    lstFrom.Sorted = true;
                    lstTo.Items.Clear();
                }
                txtTenform.Text = "";
            }
            else
            {
                MessageBox.Show("Menu này không tồn tại.", "Cảnh báo");
                cbbMamenu.Focus();
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
                    strCmd = "Delete from _Menu Where Menu_ID='" + dgvDanhsach.CurrentRow.Cells["Menu_ID"].Value.ToString() + "'";
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
                cbbMamenu.Focus();
                cbbMamenu.Text = "";
                txtTenmenu.Text = "";
                cbbParentID.Text = "";
                txtDeep.Text = "";
                txtPos.Text = "";
                txtTenform.Text = "";
                lstTo.Items.Clear();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            layDS_Nhom();
            try
            {
                cbbMamenu.Text = dgvDanhsach.CurrentRow.Cells["Menu_ID"].Value.ToString();

                txtTenmenu.Text = dgvDanhsach.CurrentRow.Cells["Menu_title"].Value.ToString();
                
                cbbParentID.Text = dgvDanhsach.CurrentRow.Cells["Parent_ID"].Value.ToString();
                
                txtDeep.Text = dgvDanhsach.CurrentRow.Cells["Deep"].Value.ToString();
                txtPos.Text = dgvDanhsach.CurrentRow.Cells["Pos"].Value.ToString();
                
                lstTo.Items.Clear();
                String groupList = dgvDanhsach.CurrentRow.Cells["Group_list"].Value.ToString();
                string[] groupName = groupList.Split(',');
                foreach (string item in groupName)
                {
                    lstTo.Items.Add(item);
                    lstFrom.Items.Remove(item);
                }

                txtTenform.Text = dgvDanhsach.CurrentRow.Cells["Form_name"].Value.ToString();
            }
            catch { }
        }

        private void txtDeep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbbMamenu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cbbParentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }        
    }
}