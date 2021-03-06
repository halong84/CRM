using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmTT_Ketquathamdo : Form
    {
        //SqlConnection myConnection;
        //SqlCommand myCommand;
        //String line = "";
        String sCommand = "",sophieu="",makh="",user="";

        public frmTT_Ketquathamdo()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmTT_Ketquathamdo_Load(object sender, EventArgs e)
        {
                       
            layhanmuc();
            //Lay chi tiet han muc
            System.Data.DataTable dshm = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dshm.Columns.Add(col);
            col = new DataColumn("Mã hạn mục", typeof(string));
            dshm.Columns.Add(col);
            col = new DataColumn("Tên hạn mục", typeof(string));
            dshm.Columns.Add(col);
           
            try
            {
                String hanmuc = (cbHanmuc.SelectedValue.ToString());
                DataTable dt = new DataTable();
                String sCommand = "SELECT * from chitietphieuthamdo where mahm ='" + hanmuc + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                int iRows = dt.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dshm.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["MaCT"].ToString();
                        row[2] = dt.Rows[i]["Chitiethanmuc"].ToString();
                        //row[4] = dt.Rows[i]["Ghichu"].ToString();
                        dshm.Rows.Add(row);

                    }
                    catch { };

                }
                dataGridViewX1.DataSource = dshm;
            }
            catch { }
            dtpNgaytt.Enabled = false;
            cbHinhthuc.Enabled = false;
            panel19.Enabled = false;
            cbHanmuc.Enabled = false;
            dataGridViewX1.Enabled = false;
            btnLuu.Enabled = false;
            cbHanmuc.Text = "";

        }
        private void layhanmuc()
        {
            
            DataTable dt = new DataTable();
            String sCommand = "SELECT mahm,hanmuc from phieuthamdo ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbHanmuc.DataSource = dt;
            cbHanmuc.DisplayMember = "hanmuc";
            cbHanmuc.ValueMember = "Mahm";
            cbHanmuc.DataSource = dt;
            cbHanmuc.SelectedItem = 0;
            
        }

        private void txtCMND_Leave(object sender, EventArgs e)
        {
            String cmnd = "";
            cmnd = txtCMND.Text;
            String sql = "";
            DataTable dt = new DataTable();
            sql = "select makh,Hoten,diachi1,dienthoai1,gioitinh,email from khachhang where cmnd='" + cmnd + "';";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sql, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;
            if (iRows > 0)
            {
                txtHoten.Text = dt.Rows[0]["Hoten"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtDiachi.Text = dt.Rows[0]["Diachi1"].ToString();
                txtDienthoai.Text = dt.Rows[0]["dienthoai1"].ToString();
                makh = dt.Rows[0]["makh"].ToString(); 
                cbLoaikh.Text="Cũ";
                if(dt.Rows[0]["gioitinh"].ToString()=="1")
                    cbGioitinh.Text="Nam";
                else
                    cbGioitinh.Text="Nữ";

            }
            else
            {
                txtHoten.Text = "";
                txtEmail.Text = "";
                txtDiachi.Text = "";
                txtDienthoai.Text ="";
                cbLoaikh.Text="Mới";

            }
           
        }

        private void txtCMND_KeyDown(object sender, KeyEventArgs e)
        {
            String cmnd = "";
            cmnd = txtCMND.Text;
            String sql = "";
            DataTable dt = new DataTable();
            sql = "select makh,Hoten,diachi1,dienthoai1,gioitinh,email from khachhang where cmnd='" + cmnd + "';";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sql, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            int iRows = dt.Rows.Count;
            if (iRows > 0)
            {
                txtHoten.Text = dt.Rows[0]["Hoten"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtDiachi.Text = dt.Rows[0]["Diachi1"].ToString();
                txtDienthoai.Text = dt.Rows[0]["dienthoai1"].ToString();
                cbLoaikh.Text = "Cũ";
                makh = dt.Rows[0]["makh"].ToString(); 
                if (dt.Rows[0]["gioitinh"].ToString() == "1")
                    cbGioitinh.Text = "Nam";
                else
                    cbGioitinh.Text = "Nữ";

            }
            else
            {
                txtHoten.Text = "";
                txtEmail.Text = "";
                txtDiachi.Text = "";
                txtDienthoai.Text = "";
                cbLoaikh.Text = "Mới";
                makh = "";
            }
        }

      
        private void cbHanmuc_TextChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dshm = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dshm.Columns.Add(col);
            col = new DataColumn("Mã hạn mục", typeof(string));
            dshm.Columns.Add(col);
            col = new DataColumn("Tên hạn mục", typeof(string));
            dshm.Columns.Add(col);
           
            
            try
            {
                String hanmuc = (cbHanmuc.SelectedValue.ToString());
                if (hanmuc.Substring(0, 1) == "A")
                {
                    col = new DataColumn("Không", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Agribank", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Ngân hàng khác", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Cả Agribank và NH khác", typeof(bool));
                    dshm.Columns.Add(col);
                      
                }

                if (hanmuc.Substring(0, 1) == "B")
                {
                    col = new DataColumn("Hoàn toàn phản đối", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Nói chung phản đối", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Không ý kiến", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Đồng ý", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Hoàn toàn đồng ý", typeof(bool));
                    dshm.Columns.Add(col);
                }
                if ((hanmuc.Substring(0, 1) == "C") || (hanmuc.Substring(0, 1) == "D"))
                {
                    col = new DataColumn("Hoàn toàn không quan trọng", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Không quan trọng", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Bình thường", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Quan trọng", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Rất quan trọng", typeof(bool));
                    dshm.Columns.Add(col);
                }
                if (hanmuc.Substring(0, 1) == "E")
                {
                    
                    col = new DataColumn("Dự thưởng", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Tặng quà", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Rút thăm trúng thưởng", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Tặng tiền mặt", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Chuyến du lịch", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Khác", typeof(bool));
                    dshm.Columns.Add(col);
                }
                    
                
                DataTable dt = new DataTable();
                String sCommand = "SELECT * from chitietphieuthamdo where mahm ='" + hanmuc + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                int iRows = dt.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dshm.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["MaCT"].ToString();
                        row[2] = dt.Rows[i]["Chitiethanmuc"].ToString();
                        //row[4] = dt.Rows[i]["Ghichu"].ToString();
                        dshm.Rows.Add(row);

                    }
                    catch { };

                }
                dataGridViewX1.DataSource = dshm;
            }
            catch { }
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int vlRow = int.Parse(dataGridViewX1.SelectedCells[0].RowIndex.ToString());
            int vlCol = int.Parse(dataGridViewX1.SelectedCells[0].ColumnIndex.ToString());

            if (dataGridViewX1.SelectedCells[0].EditedFormattedValue.ToString() == "True")
            {
                for (int i = 3; i < dataGridViewX1.Columns.Count; i++)
                {
                    if (i != vlCol)
                    {
                        dataGridViewX1.Rows[vlRow].Cells[i].Value = false;
                    }
                }

            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCMND.Text = "";
            cbHinhthuc.Text = "";
            cbHinhthuc.SelectedText="Phiếu thu thập";
            txtHoten.Text = "";
            txtEmail.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
            cbLoaikh.Text = "";
            cbGioitinh.Text = "";
            ckb18.Checked = false;
            ckb25.Checked = false;
            ckb35.Checked = false;
            ckb50.Checked = false;
            ckb51.Checked = false;
            ckb60.Checked = false;
            ckbCD.Checked = false;
            ckbDH.Checked = false;
            ckbKhac.Checked = false;
            ckbTDH.Checked = false;
            ckbTHPT.Checked = false;
            ckb2tr.Checked = false;
            ckb4tr.Checked = false;

           
            sophieu = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
            makh = "";
            btnAdd.Enabled = false;
            btnLuu.Enabled = true;
            dtpNgaytt.Enabled = true;
            cbHinhthuc.Enabled = true;
            panel19.Enabled = true;
            cbHanmuc.Enabled = true;
            dataGridViewX1.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            user = Thongtindangnhap.user_id;
            if (makh == "")
                makh = Thongtindangnhap.macn + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
            String ngay,tuoi="",trinhdo="",thunhap="";
            ngay = dtpNgaytt.Text.Substring(3, 2) + "/" + dtpNgaytt.Text.Substring(0, 2) + "/" + dtpNgaytt.Text.Substring(6, 4);
            //Kiem tra tuoi
            if (ckb18.Checked == true)
                tuoi = "<20 tuổi";
            if (ckb25.Checked == true)
                tuoi = "20-30 tuổi";
            if (ckb35.Checked == true)
                tuoi = "31-40 tuổi";
            if (ckb50.Checked == true)
                tuoi = "41-50 tuổi";
            if (ckb51.Checked == true)
                tuoi = "50-60 tuổi";
            if (ckb60.Checked == true)
                tuoi = ">60 tuổi";
            //Kiem tra trinh do
            if (ckbTDH.Checked == true)
                trinhdo = "Trên đại học";
            if (ckbDH.Checked == true)
                trinhdo = "Đại học";
            if (ckbCD.Checked == true)
                trinhdo = "Cao đẳng/Trung cấp";
            if (ckbTHPT.Checked == true)
                trinhdo = "THPT";
            if (ckbKhac.Checked == true)
                trinhdo = "Khác";
            //Kiem tra thu nhap
            if (ckb2tr.Checked == true)
                thunhap = "<20 triệu";
            if (ckb4tr.Checked == true)
                thunhap = "20-80 triệu";
            if (ckb6tr.Checked == true)
                thunhap = "80-150 triệu";
            if (ckb7tr.Checked == true)
                thunhap = ">150 triệu";
            int loaikh = 0, gioitinh = 0; ;
            
            if(cbLoaikh.Text=="Cũ")
                loaikh=0;
            else
                loaikh=1;
            if (cbGioitinh.Text == "Nữ")
                gioitinh = 0;
            else
                gioitinh = 1;
            byte hinhthuc =5;
            if(cbHinhthuc.Text=="Trực tiếp")
                hinhthuc=1;
            if(cbHinhthuc.Text=="Điện thoại")
                hinhthuc=2;
            if(cbHinhthuc.Text=="Thư,fax")
                hinhthuc=3;
            if(cbHinhthuc.Text=="Internet")
                hinhthuc=4;
            if(cbHinhthuc.Text=="Phiếu thu thập")
                hinhthuc=5;
            try
            {
                //Dua du lieu vao bang khachhangphanhoi
                sCommand = "insert into khachhangphanhoi(makh,cmnd,hoten,email,dienthoai,diachi,gioitinh,tuoi,trinhdo,thunhap,macn,tochuc) values('" + makh + "','" + txtCMND.Text + "',N'" + txtHoten.Text + "','" + txtEmail.Text + "','" + txtDienthoai.Text + "',N'" + txtDiachi.Text + "'," + gioitinh + ",N'" + tuoi + "',N'" + trinhdo + "',N'" + thunhap + "','" + Thongtindangnhap.macn + "','0')";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }
            try
            {
                // Dua du lieu vao bang ketquathamdo
                sCommand = "insert into ketquathamdo(sophieu,makh,gdvien,thoigian,hinhthuc,khachhang) values('" + sophieu + "','" + makh + "','" + user + "','" + ngay + "'," + hinhthuc + "," + loaikh + ")";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }
            //Dua du lieu vao bang ketquathamdoct
            if ((cbHanmuc.SelectedValue.ToString().Substring(0, 1) == "B") || (cbHanmuc.SelectedValue.ToString().Substring(0, 1) == "C") || (cbHanmuc.SelectedValue.ToString().Substring(0, 1) == "D"))
            {
                String luachon = "";
                for (int i = 0; i < dataGridViewX1.RowCount; i++)
                {
                    for (int j = 3; j < dataGridViewX1.ColumnCount; j++)
                    {
                        if (dataGridViewX1.Rows[i].Cells[j].Value.ToString() == "True")
                        {
                            if (j == 3)
                                luachon = "1";//Hoan toan phan doi
                            if (j == 4)
                                luachon = "2";//Noi chung phan doi
                            if (j == 5)
                                luachon = "3";//Khong y kien
                            if (j == 6)
                                luachon = "4";//Dong y
                            if (j == 7)
                                luachon = "5";//Hoan toan dong y                              

                            try
                            {
                                // Dua du lieu vao bang ketquathamdo
                                sCommand = "insert into ketquathamdoct(sophieu,mact,luachon) values('" + sophieu + "','" + dataGridViewX1.Rows[i].Cells[1].Value.ToString() + "',N'" + luachon + "')";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }
                            catch
                            {
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                if (cbHanmuc.SelectedValue.ToString().Substring(0, 1) == "A")
                {
                    String luachon = "";
                    for (int i = 0; i < dataGridViewX1.RowCount; i++)
                    {
                        for (int j = 3; j < dataGridViewX1.ColumnCount; j++)
                        {
                            if (dataGridViewX1.Rows[i].Cells[j].Value.ToString() == "True")
                            {
                                if (j == 3)
                                    luachon = "1";//Khong
                                if (j == 4)
                                    luachon = "2";//Agribank
                                if (j == 5)
                                    luachon = "3";//Ngan hang khac
                                if (j == 6)
                                    luachon = "4";//Ca Agribank va NH khac                                

                                try
                                {
                                    // Dua du lieu vao bang ketquathamdo
                                    sCommand = "insert into ketquathamdoct(sophieu,mact,luachon) values('" + sophieu + "','" + dataGridViewX1.Rows[i].Cells[1].Value.ToString() + "',N'" + luachon + "')";
                                    if (DataAccess.conn.State == ConnectionState.Open)
                                    {
                                        DataAccess.conn.Close();
                                    }
                                    DataAccess.conn.Open();
                                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                                    frmMain.myCommand.ExecuteNonQuery();
                                    DataAccess.conn.Close();
                                }
                                catch
                                {
                                    if (DataAccess.conn.State == ConnectionState.Open)
                                    {
                                        DataAccess.conn.Close();
                                    }
                                }
                                
                            }
                        }
                    }

                }
                if (cbHanmuc.SelectedValue.ToString().Substring(0, 1) == "E")
                {
                    String luachon = "";
                    for (int i = 0; i < dataGridViewX1.RowCount; i++)
                    {
                        for (int j = 3; j < dataGridViewX1.ColumnCount; j++)
                        {
                            if (dataGridViewX1.Rows[i].Cells[j].Value.ToString() == "True")
                            {
                                if (j == 3)
                                    luachon = "1";//Du thuong
                                if (j == 4)
                                    luachon = "2";//Tang qua
                                if (j == 5)
                                    luachon = "3";//Rut tham trung thuong
                                if (j == 6)
                                    luachon = "4";//Tang tien mat
                                if (j == 7)
                                    luachon = "5";//Chuyen du lich
                                if (j == 8)
                                    luachon = "6";//Khac

                                try
                                {
                                    // Dua du lieu vao bang ketquathamdo
                                    sCommand = "insert into ketquathamdoct(sophieu,mact,luachon) values('" + sophieu + "','" + dataGridViewX1.Rows[i].Cells[1].Value.ToString() + "',N'" + luachon + "')";
                                    if (DataAccess.conn.State == ConnectionState.Open)
                                    {
                                        DataAccess.conn.Close();
                                    }
                                    DataAccess.conn.Open();
                                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                                    frmMain.myCommand.ExecuteNonQuery();
                                    DataAccess.conn.Close();
                                }
                                catch
                                {
                                    if (DataAccess.conn.State == ConnectionState.Open)
                                    {
                                        DataAccess.conn.Close();
                                    }
                                }                               
                            }
                        }
                    }
                }
            }
            btnAdd.Enabled = true;
            dtpNgaytt.Enabled = false;
            cbHinhthuc.Enabled = false;
            panel19.Enabled = false;
            MessageBox.Show("Đã lưu!");
        }

        private void ckb18_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtCMND_TextChanged(object sender, EventArgs e)
        {

        }       
    }
}