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

namespace CRM
{
    public partial class frmTT_KetquathamdoTC : Form
    {
        String sCommand = "", sophieu = "", makh = "", user = "";
        public frmTT_KetquathamdoTC()
        {
            InitializeComponent();
        }

        private void frmTT_KetquathamdoTC_Load(object sender, EventArgs e)
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
                String sCommand = "SELECT * from chitietphieuthamdotc where mahm ='" + hanmuc + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
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
            cbHanmuc.Text = "Cho vay";
        }
        private void layhanmuc()
        {

            DataTable dt = new DataTable();
            String sCommand = "SELECT mahm,hanmuc from phieuthamdotc ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbHanmuc.DataSource = dt;
            cbHanmuc.DisplayMember = "hanmuc";
            cbHanmuc.ValueMember = "Mahm";
            cbHanmuc.SelectedItem = 0;

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
                    col = new DataColumn("Giảm lãi", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Giảm phí dịch vụ", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Phục vụ tại tổ chức doanh nghiệp", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Miễn phí kết nối thanh toán", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Có chế độ chăm sóc khách VIP", typeof(bool));
                    dshm.Columns.Add(col);
                    col = new DataColumn("Khác", typeof(bool));
                    dshm.Columns.Add(col);
                }


                DataTable dt = new DataTable();
                String sCommand = "SELECT * from chitietphieuthamdotc where mahm ='" + hanmuc + "'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            cbHinhthuc.Text = "";
            cbHinhthuc.SelectedText = "Phiếu thu thập";
            txtHoten.Text = "";
            txtEmail.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";       
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
            user = CRM.frmDangnhap.UserID;
            if (makh == "")
                makh = CRM.frmMain.cn + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString(); 
            String ngay;
            ngay = dtpNgaytt.Text.Substring(3, 2) + "/" + dtpNgaytt.Text.Substring(0, 2) + "/" + dtpNgaytt.Text.Substring(6, 4);
            
           
            int loaikh = 0 ;            
            
            byte hinhthuc = 5;
            if (cbHinhthuc.Text == "Trực tiếp")
                hinhthuc = 1;
            if (cbHinhthuc.Text == "Điện thoại")
                hinhthuc = 2;
            if (cbHinhthuc.Text == "Thư,fax")
                hinhthuc = 3;
            if (cbHinhthuc.Text == "Internet")
                hinhthuc = 4;
            if (cbHinhthuc.Text == "Phiếu thu thập")
                hinhthuc = 5;
            try
            {
                //Dua du lieu vao bang khachhangphanhoi
                sCommand = "insert into khachhangphanhoi(makh,hoten,email,dienthoai,diachi,macn,tochuc) values('" + makh + "',N'" + txtHoten.Text + "','" + txtEmail.Text + "','" + txtDienthoai.Text + "',N'" + txtDiachi.Text + "','" + CRM.frmMain.cn + "','1')";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
            }
            try
            {
                // Dua du lieu vao bang ketquathamdo
                sCommand = "insert into ketquathamdo(sophieu,makh,gdvien,thoigian,hinhthuc,khachhang) values('" + sophieu + "','" + makh + "','" + user + "','" + ngay + "'," + hinhthuc + "," + loaikh + ")";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                                sCommand = "insert into ketquathamdoct(sophieu,mact,luachon) values('" + sophieu + "','" + dataGridViewX1.Rows[i].Cells[1].Value.ToString() + "',N'" + luachon + "')";
                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }
                                frmMain.conn.Open();
                                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
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
                                    sCommand = "insert into ketquathamdoct(sophieu,mact,luachon) values('" + sophieu + "','" + dataGridViewX1.Rows[i].Cells[1].Value.ToString() + "',N'" + luachon + "')";
                                    if (frmMain.conn.State == ConnectionState.Open)
                                    {
                                        frmMain.conn.Close();
                                    }
                                    frmMain.conn.Open();
                                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                                    frmMain.myCommand.ExecuteNonQuery();
                                    frmMain.conn.Close();
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
                                    sCommand = "insert into ketquathamdoct(sophieu,mact,luachon) values('" + sophieu + "','" + dataGridViewX1.Rows[i].Cells[1].Value.ToString() + "',N'" + luachon + "')";
                                    if (frmMain.conn.State == ConnectionState.Open)
                                    {
                                        frmMain.conn.Close();
                                    }
                                    frmMain.conn.Open();
                                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                                    frmMain.myCommand.ExecuteNonQuery();
                                    frmMain.conn.Close();
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
                    }
                }
            }
            btnAdd.Enabled = true;
            dtpNgaytt.Enabled = false;
            cbHinhthuc.Enabled = false;
            panel19.Enabled = false;
            MessageBox.Show("Đã lưu!");
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
    }
}