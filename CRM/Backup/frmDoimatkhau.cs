using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using mahoachuoi;

namespace CRM
{
    public partial class frmDoimatkhau : Form
    {
        DataTable dtResult = new DataTable();
        string strCmd = "";
        char key = 'P';
        mahoachuoi.Mahoa mahoa;

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
            mahoa = new Mahoa(key);
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

            if (txtPass_old.Text != frmDangnhap.matkhau)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.");
                txtPass_old.Focus();
                return;
            }

            if (txtConfirm.Text != txtConfirm.Text)
            {
                MessageBox.Show("Mật khẩu mới không giống nhau.");
                txtPass_new.Text = "";
                txtConfirm.Text = "";
                txtPass_new.Focus();
                return;
            }

            string pass_new = txtPass_new.Text;
            string pass_confirm = txtConfirm.Text;

            strCmd = "SELECT * FROM _User WHERE User_ID='" + frmDangnhap.UserID + "' ";

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
                pass_new = mahoa.mahoa(pass_new);
                pass_confirm = mahoa.mahoa(pass_confirm);

                strCmd = "Update _User Set User_pass='" + pass_new + "' Where User_ID='" + frmDangnhap.UserID + "' ";

                try
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, frmMain.conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                    txtPass_old.Text = "";
                    txtPass_new.Text = "";
                    txtConfirm.Text = "";
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}