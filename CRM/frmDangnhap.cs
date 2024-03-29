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
using System.Diagnostics;
using System.Xml;
using System.Net;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmDangnhap : Form
    {
        UserBUS userbus = new UserBUS();
        HethongBUS htbus = new HethongBUS();
        ChinhanhBUS cnbus = new ChinhanhBUS();
        INOUTLOGBUS inoutlog_bus = new INOUTLOGBUS();
        public static string matkhau, UserID , hoten;
        public static string macn , line;

        // get the running version  
        public Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        //public static SqlConnection conn;
        //mahoachuoi.Mahoa mahoa;
        //SqlCommand myCommand = new SqlCommand();
        //SqlDataReader reader;
        //public static string str_update = "";
        //public static string phienban = "2.23";

        public frmDangnhap()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private bool Kiemtracapnhat()
        {
            bool has_update = false;
            // in newVersion variable we will store the  
            // version info from xml file  
            Version newVersion = null;
            // and in this variable we will put the url we  
            // would like to open so that the user can  
            // download the new version  
            // it can be a homepage or a direct  
            // link to zip/exe file  
            string url = "";
            XmlTextReader reader = null;
            try
            {
                // provide the XmlTextReader with the URL of  
                // our xml document  
                //string xmlURL = "http://192.168.1.150/update_crm.xml";
                string xmlURL = "http://10.14.0.30/update_crm.xml";
                reader = new XmlTextReader(xmlURL);
                // simply (and easily) skip the junk at the beginning  
                reader.MoveToContent();
                // internal - as the XmlTextReader moves only  
                // forward, we save current xml element name  
                // in elementName variable. When we parse a  
                // text node, we refer to elementName to check  
                // what was the node name  
                string elementName = "";
                // we check if the xml starts with a proper  
                // "ourfancyapp" element node  
                if ((reader.NodeType == XmlNodeType.Element) &&
                    (reader.Name == "crm"))
                {
                    while (reader.Read())
                    {
                        // when we find an element node,  
                        // we remember its name  
                        if (reader.NodeType == XmlNodeType.Element)
                            elementName = reader.Name;
                        else
                        {
                            // for text nodes...  
                            if ((reader.NodeType == XmlNodeType.Text) &&
                                (reader.HasValue))
                            {
                                // we check what the name of the node was  
                                switch (elementName)
                                {
                                    case "version":
                                        // thats why we keep the version info  
                                        // in xxx.xxx.xxx.xxx format  
                                        // the Version class does the  
                                        // parsing for us  
                                        newVersion = new Version(reader.Value);
                                        break;
                                    case "url":
                                        url = reader.Value;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            if (curVersion.CompareTo(newVersion) < 0)
            {
                // ask the user if he would like  
                // to download the new version  
                string title = "Thông báo cập nhật";
                string question = "Phiên bản bạn đang dùng " + curVersion.ToString() + ". Đã có phiên bản mới " + newVersion.ToString() + ". Bạn có muốn tải về?";
                if (DialogResult.Yes ==
                 MessageBox.Show(this, question, title,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question))
                {
                    // navigate the default web  
                    // browser to our app  
                    // homepage (the url  
                    // comes from the xml content)  
                    
                    //System.Diagnostics.Process.Start(url);
                    Process.Start(Application.StartupPath.ToString() + @"\CRM_UPDATE.exe");
                    has_update = true;
                }
            }
            return has_update;
        }
        private void frmDangnhap_Load(object sender, EventArgs e)
        {
            this.Text = "CRM " + Convert.ToString(curVersion) + " - AGRIBANK TỈNH HẢI DƯƠNG";
            //MessageBox.Show(Application.StartupPath.ToString());
            //Kiemtracapnhat();
            
            if (Kiemtracapnhat() == true)
            {
                Application.Exit();
            }
            //this.Text = "Dang nhap CRM [phien ban " + phienban + "]";

            //WebClient client = new WebClient();
            
            //try
            //{
            //    client.DownloadFile("ftp://10.131.0.19/CRM.txt", "c:\\CRM.txt");
            //}
            //catch { }

            //try
            //{
            //    StreamReader reader3 = new StreamReader("c:\\CRM.txt");
            //    str_update = reader3.ReadLine();
            //    reader3.Close();

            //    if (phienban != str_update)
            //    {
            //        frmCapnhat capnhat = new frmCapnhat();
            //        capnhat.ShowDialog();
            //        this.Close();
            //    }
            //}
            //catch { }

            //try
            //{
            //    StreamReader reader1 = new StreamReader("database.txt");
            //    line = reader1.ReadLine();
            //    reader1.Close();
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file database.txt");
            //    this.Close();
            //}

            //try
            //{
                //conn = new SqlConnection("user id=sa;" +
                //                          "password=qaz@123;server=" + line + ";" +
                //                          "Trusted_Connection=no;" +
                //                          "database=CRM; " +
                //                          "connection timeout=0;"+
                //                          "Max Pool Size=30;"+
                //                          "Pooling=True");
            //    conn = new SqlConnection(@"Server=2300-WS0242\SQLEXPRESS;Database=CRM;User Id=sa;Password=123456a@;");

            //}
            //catch
            //{
            //    MessageBox.Show("Không kết nối được máy chủ");
            //    this.Close();
            //}

            //mahoa = new Mahoa(key);
        }

        //bool checkUser(string uid, string pass, ref string group_list)
        //{
        //    string strCmd = "";
        //    string _pass = mahoa.mahoa(pass);

        //    bool b = false;

        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    strCmd = "select group_list,tennv from _User where user_id='" + uid + "' and user_pass='" + _pass + "'";
        //    conn.Open();
        //    myCommand = new SqlCommand(strCmd, conn);
        //    //myCommand.CommandText = "select group_list from _User where user_id='" + uid + "' and user_pass='" + _pass + "'";
        //    reader = myCommand.ExecuteReader();
        //    //adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
        //    //reader = adapter.SelectCommand.ExecuteReader();


        //    //olecom.CommandText= "select group_list from _User where user_id='"+uid+"' and user_pass='"+_pass+"'";
        //    //reader=olecom.ExecuteReader();

        //    if (reader.Read())
        //    {
        //        group_list = reader.GetString(0);
        //        b = true;
        //        hoten = reader.GetString(1);
        //    }

        //    reader.Close();
        //    conn.Close();
        //    return b;
        //}
        public string GetIPAddress()
        {
            string IPAdd = "" ;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAdd = Convert.ToString(IP);
                }
            }
            return IPAdd;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string uid = this.txtUsername.Text.Trim();
            string pass = this.txtPassword.Text.Trim();
            //string pass_mahoa = mahoa.mahoa(pass);
            //string group_list = "";

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

            //DataTable user_dt = userbus.XAC_THUC_USER(uid, pass_mahoa);
            DataTable user_dt = userbus.XAC_THUC_USER(uid, pass);

            if (user_dt.Rows.Count > 0)
            {
                Thongtindangnhap.user_id = uid;
                Thongtindangnhap.user_pass = pass;
                Thongtindangnhap.group_list = user_dt.Rows[0]["GROUP_LIST"].ToString();
                Thongtindangnhap.manv = user_dt.Rows[0]["MANV"].ToString();
                Thongtindangnhap.tennv = user_dt.Rows[0]["TENNV"].ToString();
                Thongtindangnhap.chucvu = user_dt.Rows[0]["CHUCVU"].ToString();
                Thongtindangnhap.macn = user_dt.Rows[0]["MACN"].ToString();
                Thongtindangnhap.ghichu = user_dt.Rows[0]["GHICHU"].ToString();
                Thongtindangnhap.mapb = user_dt.Rows[0]["MAPB"].ToString();
                Thongtindangnhap.tencn = user_dt.Rows[0]["TENCN"].ToString();
                Thongtindangnhap.tencn_en = user_dt.Rows[0]["TENCN_EN"].ToString();
                Thongtindangnhap.sdt_hs = user_dt.Rows[0]["SDT_HS"].ToString();
                Thongtindangnhap.fax_hs = user_dt.Rows[0]["FAX_HS"].ToString();
                Thongtindangnhap.diachicn = user_dt.Rows[0]["DIACHICN"].ToString();
                Thongtindangnhap.diachicn_en = user_dt.Rows[0]["DIACHICN_EN"].ToString();
                Thongtindangnhap.tenpb = user_dt.Rows[0]["TENPB"].ToString();
                Thongtindangnhap.tenpb_en = user_dt.Rows[0]["TENPB_EN"].ToString();
                Thongtindangnhap.sdt_pb = user_dt.Rows[0]["SDT_PB"].ToString();
                Thongtindangnhap.fax_pb = user_dt.Rows[0]["FAX_PB"].ToString();
                Thongtindangnhap.diachipb = user_dt.Rows[0]["DIACHIPB"].ToString();
                Thongtindangnhap.diachipb_en = user_dt.Rows[0]["DIACHIPB_EN"].ToString();
                Thongtindangnhap.hs = Convert.ToBoolean(user_dt.Rows[0]["HS"].ToString());

                DataTable ht_dt = htbus.HE_THONG();
                Thongtindangnhap.ma_hoi_so = ht_dt.Rows[0]["MA_HOI_SO"].ToString();
                Thongtindangnhap.ddimport = ht_dt.Rows[0]["DDIMPORT"].ToString();
                Thongtindangnhap.ma_tinh_hien_tai = ht_dt.Rows[0]["MA_TINH_HIEN_TAI"].ToString();

                //DataTable cn_dt = cnbus.CHI_NHANH_THEO_MACN(Thongtindangnhap.macn);
                //Thongtindangnhap.tencn = CommonMethod.FirstCharToUpper(cn_dt.Rows[0]["TENCN"].ToString().Substring(9));
                Thongtindangnhap.ip_address = GetIPAddress();

                if(inoutlog_bus.INSERT_INOUTLOG(Thongtindangnhap.user_id, Thongtindangnhap.ip_address, "Đăng nhập"))
                {
                    //
                }

                this.DialogResult = DialogResult.OK;
                this.Dispose();
                //UserID = uid;
                //matkhau = pass;

                //this.Hide();

                //group_list = user_dt.Rows[0]["GROUP_LIST"].ToString();
                //macn = user_dt.Rows[0]["MACN"].ToString();

                //frmMain obj = new frmMain();
                
                //obj._uid = uid;
                //obj._group_list = group_list.Split(',');
                //obj._sqlcon = conn;
                //obj.Show();
            }
            //if (uid != "" && pass != "")
            //{
            //    if (checkUser(uid, pass, ref group_list))
            //    {
            //        UserID = uid;
            //        matkhau = pass; 

            //        this.Hide();

            //        conn.Open();
            //        DataTable dt = new DataTable();
            //        String strCmd = "Select Macn from _USER where USER_ID='" + uid + "'";
            //        new SqlDataAdapter(strCmd, conn).Fill(dt);
            //        int i = dt.Rows.Count;
            //        if (i != 0)
            //        {
            //            macn = dt.Rows[i - 1]["MACN"].ToString();
            //        }
            //        conn.Close();

            //        frmMain obj = new frmMain();
            //        obj._uid = uid;
            //        obj._group_list = group_list.Split(',');
            //        obj._sqlcon = conn;
            //        obj.Show();

                                       
            //    }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng hoặc người sử dụng chưa được kích hoạt");
                txtUsername.Focus();
                return;
            }
            //else
            //{
            //    if (uid == "")
            //    {
            //        MessageBox.Show("Chưa nhập tên đăng nhập.");
            //        txtUsername.Focus();
            //        return;
            //    }
            //    else if (pass == "")
            //    {
            //        MessageBox.Show("Chưa nhập mật khẩu.");
            //        txtPassword.Focus();
            //        return;
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        //}

        //private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyData == Keys.Enter)
        //        {
        //            string uid = this.txtUsername.Text.Trim();
        //            string pass = this.txtPassword.Text.Trim();
        //            string group_list = "";

        //            if (uid != "" && pass != "")
        //            {
        //                if (checkUser(uid, pass, ref group_list))
        //                {
        //                    UserID = uid;
        //                    matkhau = pass;

        //                    this.Hide();

        //                    conn.Open();
        //                    DataTable dt = new DataTable();
        //                    String strCmd = "Select Macn from _USER where USER_ID='" + uid + "'";
        //                    new SqlDataAdapter(strCmd, conn).Fill(dt);
        //                    int i = dt.Rows.Count;
        //                    if (i != 0)
        //                    {
        //                        macn = dt.Rows[i - 1]["MACN"].ToString();
        //                    }
        //                    conn.Close();

        //                    frmMain obj = new frmMain();
        //                    obj._uid = uid;
        //                    obj._group_list = group_list.Split(',');
        //                    obj._sqlcon = conn;
        //                    obj.Show();

                                                        
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
        //                    txtUsername.Focus();
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                if (uid == "")
        //                {
        //                    MessageBox.Show("Chưa nhập tên đăng nhập.");
        //                    txtUsername.Focus();
        //                    return;
        //                }
        //                else if (pass == "")
        //                {
        //                    MessageBox.Show("Chưa nhập mật khẩu.");
        //                    txtPassword.Focus();
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        //private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyData == Keys.Enter)
        //        {
        //            string uid = this.txtUsername.Text.Trim();
        //            string pass = this.txtPassword.Text.Trim();
        //            string group_list = "";

        //            if (uid != "" && pass != "")
        //            {
        //                if (checkUser(uid, pass, ref group_list))
        //                {
        //                    UserID = uid;
        //                    matkhau = pass;

        //                    this.Hide();

        //                    conn.Open();
        //                    DataTable dt = new DataTable();
        //                    String strCmd = "Select Macn from _USER where USER_ID='" + uid + "'";
        //                    new SqlDataAdapter(strCmd, conn).Fill(dt);
        //                    int i = dt.Rows.Count;
        //                    if (i != 0)
        //                    {
        //                        macn = dt.Rows[i - 1]["MACN"].ToString();
        //                    }
        //                    conn.Close();

        //                    frmMain obj = new frmMain();
        //                    obj._uid = uid;
        //                    obj._group_list = group_list.Split(',');
        //                    obj._sqlcon = conn;
        //                    obj.Show();

                            
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
        //                    txtUsername.Focus();
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                if (uid == "")
        //                {
        //                    MessageBox.Show("Chưa nhập tên đăng nhập.");
        //                    txtUsername.Focus();
        //                    return;
        //                }
        //                else if (pass == "")
        //                {
        //                    MessageBox.Show("Chưa nhập mật khẩu.");
        //                    txtPassword.Focus();
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //    //try
        //    //{
        //    //    if (e.KeyData == Keys.Enter)
        //    //    {
        //    //        txtPassword.Focus();                            
        //    //    }
        //    //}
        //    //catch { }
        //}        
    }
}