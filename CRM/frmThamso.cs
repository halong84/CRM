using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Quan_he_khach_hang
{
    public partial class frmThamso : Form
    {
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        SqlConnection conn;
        string strConn = "";

        public frmThamso()
        {
            InitializeComponent();
        }        

        private void frmThamso_Load(object sender, EventArgs e)
        {
            this.Top = 55;
            this.Left = 0;

            string line = "";
            try
            {
                StreamReader reader = new StreamReader("Path.dll");
                line = reader.ReadLine();
                reader.Close();
            }
            catch
            {
                MessageBox.Show("Không đọc được file Path.dll");
                this.Close();
            }

            try
            {
                strConn = "SERVER=" + line + ";uid=sa;pwd=qaz123;Database=CRM";
                conn = new SqlConnection(strConn);
            }
            catch
            {
                MessageBox.Show("Không kết nối được dữ liệu!");
                this.Close();
                return;
            }

            layDS_CN();
        }

        private void layDS_CN()
        {
            strCmd = "SELECT * FROM HETHONG ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, conn);
                adapter.SelectCommand.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            if (iRows > 0)
            {
                txtMaCN.Text = dtResult.Rows[0]["MaCN"].ToString();
                txtTenCN.Text = dtResult.Rows[0]["TenCN"].ToString();                
            }            
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMaCN.Text == "")
            {
                MessageBox.Show("Chưa nhập mã chi nhánh.", "Thông báo");
                txtMaCN.Focus();
                return;
            }

            strCmd = "SELECT * FROM HETHONG ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, conn);
                adapter.SelectCommand.ExecuteNonQuery();
                conn.Close();
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
                strCmd = "Insert into HETHONG(MaCN, TenCN) ";
                strCmd += "Values('" + txtMaCN.Text.Trim() + "',N'" + txtTenCN.Text.Trim() + "')";

                try
                {
                    conn.Open();
                    adapter.InsertCommand = new SqlCommand(strCmd, conn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Đã thêm.", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                strCmd = "Update HETHONG ";
                strCmd += "SET MaCN='" + txtMaCN.Text.Trim() + "', TenCN=N'" + txtTenCN.Text.Trim() + "' ";
                
                try
                {
                    conn.Open();
                    adapter.UpdateCommand = new SqlCommand(strCmd, conn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Đã thay đổi.", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}