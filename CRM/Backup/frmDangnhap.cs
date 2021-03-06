using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Collections;
using mahoachuoi;
using System.Net;

namespace CRM
{
    public partial class frmDangnhap : Form
    {
        char key = 'P';
        public static string matkhau = "1111", UserID = "", hoten="";
        public static string macn = "", line = "";
        public static SqlConnection conn;
        mahoachuoi.Mahoa mahoa;
        SqlCommand myCommand = new SqlCommand();
        SqlDataReader reader;
        public static string str_update = "";
        public static string phienban = "2.23";

        public frmDangnhap()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmDangnhap_Load(object sender, EventArgs e)
        {
            this.Text = "Dang nhap CRM [phien ban " + phienban + "]";

            WebClient client = new WebClient();
            
            try
            {
                client.DownloadFile("ftp://10.131.0.19/CRM.txt", "c:\\CRM.txt");
            }
            catch { }

            try
            {
                StreamReader reader3 = new StreamReader("c:\\CRM.txt");
                str_update = reader3.ReadLine();
                reader3.Close();

                if (phienban != str_update)
                {
                    frmCapnhat capnhat = new frmCapnhat();
                    capnhat.ShowDialog();
                    this.Close();
                }
            }
            catch { }

            try
            {
                StreamReader reader1 = new StreamReader("database.txt");
                line = reader1.ReadLine();
                reader1.Close();
            }
            catch
            {
                MessageBox.Show("Không đọc được file database.txt");
                this.Close();
            }

            try
            {
                conn = new SqlConnection("user id=sa;" +
                                          "password=qaz@123;server=" + line + ";" +
                                          "Trusted_Connection=no;" +
                                          "database=CRM; " +
                                          "connection timeout=0;"+
                                          "Max Pool Size=30;"+
                                          "Pooling=True");                
            }
            catch
            {
                MessageBox.Show("Không kết nối được máy chủ");
                this.Close();
            }

            mahoa = new Mahoa(key);
        }

        bool checkUser(string uid, string pass, ref string group_list)
        {
            string strCmd = "";
            string _pass = mahoa.mahoa(pass);

            bool b = false;

            SqlDataAdapter adapter = new SqlDataAdapter();
            strCmd = "select group_list,tennv from _User where user_id='" + uid + "' and user_pass='" + _pass + "'";
            conn.Open();
            myCommand = new SqlCommand(strCmd, conn);
            //myCommand.CommandText = "select group_list from _User where user_id='" + uid + "' and user_pass='" + _pass + "'";
            reader = myCommand.ExecuteReader();
            //adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
            //reader = adapter.SelectCommand.ExecuteReader();


            //olecom.CommandText= "select group_list from _User where user_id='"+uid+"' and user_pass='"+_pass+"'";
            //reader=olecom.ExecuteReader();

            if (reader.Read())
            {
                group_list = reader.GetString(0);
                b = true;
                hoten = reader.GetString(1);
            }

            reader.Close();
            conn.Close();
            return b;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string uid = this.txtUsername.Text.Trim();
            string pass = this.txtPassword.Text.Trim();
            string group_list = "";

            if (uid != "" && pass != "")
            {
                if (checkUser(uid, pass, ref group_list))
                {
                    UserID = uid;
                    matkhau = pass; 

                    this.Hide();

                    conn.Open();
                    DataTable dt = new DataTable();
                    String strCmd = "Select Macn from _USER where USER_ID='" + uid + "'";
                    new SqlDataAdapter(strCmd, conn).Fill(dt);
                    int i = dt.Rows.Count;
                    if (i != 0)
                    {
                        macn = dt.Rows[i - 1]["MACN"].ToString();
                    }
                    conn.Close();

                    frmMain obj = new frmMain();
                    obj._uid = uid;
                    obj._group_list = group_list.Split(',');
                    obj._sqlcon = conn;
                    obj.Show();

                                       
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                    txtUsername.Focus();
                    return;
                }
            }
            else
            {
                if (uid == "")
                {
                    MessageBox.Show("Chưa nhập tên đăng nhập.");
                    txtUsername.Focus();
                    return;
                }
                else if (pass == "")
                {
                    MessageBox.Show("Chưa nhập mật khẩu.");
                    txtPassword.Focus();
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    string uid = this.txtUsername.Text.Trim();
                    string pass = this.txtPassword.Text.Trim();
                    string group_list = "";

                    if (uid != "" && pass != "")
                    {
                        if (checkUser(uid, pass, ref group_list))
                        {
                            UserID = uid;
                            matkhau = pass;

                            this.Hide();

                            conn.Open();
                            DataTable dt = new DataTable();
                            String strCmd = "Select Macn from _USER where USER_ID='" + uid + "'";
                            new SqlDataAdapter(strCmd, conn).Fill(dt);
                            int i = dt.Rows.Count;
                            if (i != 0)
                            {
                                macn = dt.Rows[i - 1]["MACN"].ToString();
                            }
                            conn.Close();

                            frmMain obj = new frmMain();
                            obj._uid = uid;
                            obj._group_list = group_list.Split(',');
                            obj._sqlcon = conn;
                            obj.Show();

                                                        
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                            txtUsername.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (uid == "")
                        {
                            MessageBox.Show("Chưa nhập tên đăng nhập.");
                            txtUsername.Focus();
                            return;
                        }
                        else if (pass == "")
                        {
                            MessageBox.Show("Chưa nhập mật khẩu.");
                            txtPassword.Focus();
                            return;
                        }
                    }
                }
            }
            catch { }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    string uid = this.txtUsername.Text.Trim();
                    string pass = this.txtPassword.Text.Trim();
                    string group_list = "";

                    if (uid != "" && pass != "")
                    {
                        if (checkUser(uid, pass, ref group_list))
                        {
                            UserID = uid;
                            matkhau = pass;

                            this.Hide();

                            conn.Open();
                            DataTable dt = new DataTable();
                            String strCmd = "Select Macn from _USER where USER_ID='" + uid + "'";
                            new SqlDataAdapter(strCmd, conn).Fill(dt);
                            int i = dt.Rows.Count;
                            if (i != 0)
                            {
                                macn = dt.Rows[i - 1]["MACN"].ToString();
                            }
                            conn.Close();

                            frmMain obj = new frmMain();
                            obj._uid = uid;
                            obj._group_list = group_list.Split(',');
                            obj._sqlcon = conn;
                            obj.Show();

                            
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                            txtUsername.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (uid == "")
                        {
                            MessageBox.Show("Chưa nhập tên đăng nhập.");
                            txtUsername.Focus();
                            return;
                        }
                        else if (pass == "")
                        {
                            MessageBox.Show("Chưa nhập mật khẩu.");
                            txtPassword.Focus();
                            return;
                        }
                    }
                }
            }
            catch { }
            //try
            //{
            //    if (e.KeyData == Keys.Enter)
            //    {
            //        txtPassword.Focus();                            
            //    }
            //}
            //catch { }
        }        
    }
}