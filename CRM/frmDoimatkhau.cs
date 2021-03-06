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
    public partial class frmDoimatkhau : Form
    {
        UserBUS user_bus = new UserBUS();
        DataTable dtResult = new DataTable();
        //string strCmd = "";
        //char key = 'P';
        //mahoachuoi.Mahoa mahoa;

        public frmDoimatkhau()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmDoimatkhau_Load(object sender, EventArgs e)
        {
            //mahoa = new Mahoa(key);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPass_old.Text == "")
            {
                MessageBox.Show("Chưa nhập mật khẩu cũ.");
                txtPass_old.Focus();
                return;
            }
            else if (txtPass_new.Text == "")
            {
                MessageBox.Show("Chưa nhập mật khẩu mới.");
                txtPass_new.Focus();
                return;
            }
            else if (txtConfirm.Text == "")
            {
                MessageBox.Show("Chưa xác nhận mật khẩu mới.");
                txtConfirm.Focus();
                return;
            }

            if (txtPass_old.Text != Thongtindangnhap.user_pass)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.");
                txtPass_old.Focus();
                return;
            }

            if (txtConfirm.Text != txtConfirm.Text)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu mới không trùng nhau. Đề nghị kiểm tra lại!");
                txtPass_new.Text = "";
                txtConfirm.Text = "";
                txtPass_new.Focus();
                return;
            }

            //if (user_bus.DOI_MAT_KHAU(Thongtindangnhap.user_id, mahoa.mahoa(txtPass_new.Text)))
            if (user_bus.DOI_MAT_KHAU(Thongtindangnhap.user_id, txtPass_new.Text))
            {
                MessageBox.Show("Đổi mật khẩu thành công.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra, đổi mật khẩu không thành công.");
            }
            //string pass_new = txtPass_new.Text;
            //string pass_confirm = txtConfirm.Text;

            //strCmd = "SELECT * FROM _User WHERE User_ID='" + Thongtindangnhap.user_id + "' ";

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //try
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
            //    adapter.SelectCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //DataSet ds = new DataSet();
            //adapter.Fill(ds);
            //dtResult = ds.Tables[0];

            //if (dtResult.Rows.Count > 0)
            //{
            //    pass_new = mahoa.mahoa(pass_new);
            //    pass_confirm = mahoa.mahoa(pass_confirm);

            //    strCmd = "Update _User Set User_pass='" + pass_new + "' Where User_ID='" + Thongtindangnhap.user_id + "' ";

            //    try
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //        DataAccess.conn.Open();
            //        adapter.UpdateCommand = new SqlCommand(strCmd, DataAccess.conn);
            //        adapter.UpdateCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();
            //        MessageBox.Show("Đã thay đổi.", "Thông báo");
            //        txtPass_old.Text = "";
            //        txtPass_new.Text = "";
            //        txtConfirm.Text = "";
            //    }
            //    catch
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //    }
            //}          
  
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}