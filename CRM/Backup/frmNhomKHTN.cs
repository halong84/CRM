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
    public partial class frmNhomKHTN : Form
    {
        private string manhom="";
        private string tenNhom = "";

        public frmNhomKHTN()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 
        }

        private void frmNhomKHTN_Load(object sender, EventArgs e)
        {
            laydanhsachnhomkhachhang();
            //tenNhom = dataGridViewX1.Rows[0].Cells[1].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                String sCommand,manhom="";
                manhom = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                txtManhom.Text = manhom;
                sCommand = "INSERT INTO NhomkhachhangTN VALUES('" + manhom + "',N'" + txtTennhom.Text + "',N'" + txtDiengiai.Text + "',N'" + txtTaichinh.Text + "',N'" + txtLinhvuc.Text + "'," + Convert.ToInt16(cbDoUuTien.Text) + ",'" + frmMain.cn + "'," + cbTinhtrang.SelectedIndex + ")";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtManhom.Text = "";
                txtTennhom.Text = "";
                txtDiengiai.Text = "";
                txtTaichinh.Text = "";
                txtLinhvuc.Text = "";
                cbDoUuTien.Text = "";
                cbTinhtrang.Text = "";                
                laydanhsachnhomkhachhang();
                MessageBox.Show("Đã thêm nhóm khách hàng mới!");
                btnLuu.Enabled = false;
                btnXoa.Enabled = false;
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                MessageBox.Show("Nhóm khách hàng này đã có!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void laydanhsachnhomkhachhang()
        {
            DataTable dt = new DataTable();
            String sCommand = "",tinhtrang="";            
            sCommand = "SELECT * from NhomkhachhangTN where macn='"+CRM.frmMain.cn+"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Tên nhóm", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Diễn giải", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Lĩnh vực hoạt động", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Đánh giá tài chính", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Độ ưu tiên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã nhóm", typeof(string));
            dskh.Columns.Add(col);            

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;                    
                    row[1] = dt.Rows[i]["tennhom"].ToString();
                    row[2] = dt.Rows[i]["diengiai"].ToString();
                    row[3] = dt.Rows[i]["linhvuchoatdong"].ToString();
                    row[4] = dt.Rows[i]["danhgiataichinh"].ToString();                   
                    row[5] = dt.Rows[i]["douutien"].ToString();
                    if(dt.Rows[i]["tinhtrang"].ToString()=="True")
                        tinhtrang="Hoạt động";
                    else
                        tinhtrang="Không hoạt động";
                    row[6] = tinhtrang;
                    row[7] = dt.Rows[i]["manhom"].ToString();                    
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX1.DataSource = dskh;

            dataGridViewX1.Columns[0].FillWeight = 20;
            dataGridViewX1.Columns[0].Width = 40;
            dataGridViewX1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX1.Columns[7].Visible = false;
            dataGridViewX1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }        

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tenNhom = dataGridViewX1.CurrentRow.Cells[1].Value.ToString();
            btnLuu.Enabled = true;
            btnXoa.Enabled = true;
            btnLoaiKH.Enabled = true;
            try
            {
                txtTennhom.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[1].Value.ToString();
                txtDiengiai.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[2].Value.ToString();
                cbDoUuTien.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[5].Value.ToString();
                cbTinhtrang.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[6].Value.ToString();
                txtLinhvuc.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[3].Value.ToString();
                txtTaichinh.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[4].Value.ToString();
                manhom=txtManhom.Text = dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[7].Value.ToString();
                timkiemkhachhangtheonhom(manhom);

            }
            catch { }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                String sCommand;
                sCommand = "UPDATE NhomkhachhangTN SET tennhom=N'" + txtTennhom.Text + "',linhvuchoatdong=N'" + txtLinhvuc.Text + "',danhgiataichinh=N'" + txtTaichinh.Text + "',tinhtrang=" + cbTinhtrang.SelectedIndex + ",douutien=" + cbDoUuTien.Text + ",diengiai=N'" + txtDiengiai.Text + "' where manhom='"+txtManhom.Text+"'";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                txtManhom.Text = "";
                txtTennhom.Text = "";
                txtDiengiai.Text = "";
                txtLinhvuc.Text = "";
                txtTaichinh.Text = "";
                cbDoUuTien.Text = "";
                cbTinhtrang.Text = "";
                txtTennhom.Focus();
                laydanhsachnhomkhachhang();
                MessageBox.Show("Đã thay đổi!");
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                MessageBox.Show("Có lỗi xảy ra!");
            }
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            Cursor.Current = Cursors.Default;
        }

        private void timkiemkhachhangtheonhom(string strManhom)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dshd = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dshd.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string));
            dshd.Columns.Add(col);           
            col = new DataColumn("CMND", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dshd.Columns.Add(col);

            DataTable dt = new DataTable();
            String sCommand = "SELECT khachhangtiemnang.* from khachhangtiemnang,khtn_nhomkhtn where khachhangtiemnang.makh=khtn_nhomkhtn.makh and khtn_nhomkhtn.manhom='" + strManhom + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();
            
            int iRows = dt.Rows.Count;
            String gioitinh = "",ngaysinh="";
            for (int i = 0; i < iRows; i++)
            {               
                DataRow row = dshd.NewRow();
                row[0] = i + 1;

                row[1] = dt.Rows[i]["hoten"].ToString();

                row[2] = dt.Rows[i]["diachi1"].ToString();
                row[3] = dt.Rows[i]["dienthoai1"].ToString();               
                row[4] = dt.Rows[i]["cmnd"].ToString();
                row[5] = dt.Rows[i]["makh"].ToString();
                if (dt.Rows[i]["ngaysinh"].ToString() != "")
                    ngaysinh = dt.Rows[i]["ngaysinh"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(6, 4);
                row[6] = ngaysinh;
                if (dt.Rows[i]["gioitinh"].ToString() == "True")
                    gioitinh = "Nam";
                else
                    gioitinh = "Nữ";
                row[7] = gioitinh;
                row[8] = dt.Rows[i]["ghichu"].ToString();
                row[9] = true;

                dshd.Rows.Add(row);
                       
            }
            dataGridViewX2.DataSource = dshd;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dataGridViewX2.Columns[4].Visible = false;
            //dataGridViewX1.Columns[6].Visible = false;            
            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewX2.Columns[2].Visible = false;
            dataGridViewX2.Columns[3].Visible = false;
            dataGridViewX2.Columns[6].Visible = false;

            Cursor.Current = Cursors.Default;
        }

        private void buttonX121_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dshd = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dshd.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dshd.Columns.Add(col);

            DataTable dt = new DataTable();
            String sCommand = "";
            sCommand = "SELECT khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.cmnd like '%" + textBox34.Text + "%' and macn='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();

            int iRows = dt.Rows.Count;
            String gioitinh = "", ngaysinh = "";
            for (int i = 0; i < iRows; i++)
            {
                DataRow row = dshd.NewRow();
                row[0] = i + 1;

                row[1] = dt.Rows[i]["hoten"].ToString();

                row[2] = dt.Rows[i]["diachi1"].ToString();
                row[3] = dt.Rows[i]["dienthoai1"].ToString();
                row[4] = dt.Rows[i]["cmnd"].ToString();
                row[5] = dt.Rows[i]["makh"].ToString();
                if (dt.Rows[i]["ngaysinh"].ToString() != "")
                    ngaysinh = dt.Rows[i]["ngaysinh"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(6, 4);
                row[6] = ngaysinh;
                if (dt.Rows[i]["gioitinh"].ToString() == "True")
                    gioitinh = "Nam";
                else
                    gioitinh = "Nữ";
                row[7] = gioitinh;
                row[8] = dt.Rows[i]["ghichu"].ToString();
                row[9] = true;

                dshd.Rows.Add(row);

            }
            dataGridViewX2.DataSource = dshd;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dataGridViewX2.Columns[4].Visible = false;
            //dataGridViewX1.Columns[6].Visible = false;            
            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewX2.Columns[2].Visible = false;
            dataGridViewX2.Columns[3].Visible = false;
            dataGridViewX2.Columns[6].Visible = false;

            Cursor.Current = Cursors.Default;
        }

        private void buttonX128_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dataGridViewX2.RowCount; i++)
            {
                dataGridViewX2.Rows[i].Cells[9].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonX116_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dataGridViewX2.RowCount; i++)
            {
                dataGridViewX2.Rows[i].Cells[9].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonX126_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dshd = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dshd.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dshd.Columns.Add(col);

            DataTable dt = new DataTable();
            String sCommand = "";
            sCommand = "SELECT khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.hoten like N'%" + textBox36.Text + "%' and macn='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();

            int iRows = dt.Rows.Count;
            String gioitinh = "", ngaysinh = "";
            for (int i = 0; i < iRows; i++)
            {
                DataRow row = dshd.NewRow();
                row[0] = i + 1;

                row[1] = dt.Rows[i]["hoten"].ToString();

                row[2] = dt.Rows[i]["diachi1"].ToString();
                row[3] = dt.Rows[i]["dienthoai1"].ToString();
                row[4] = dt.Rows[i]["cmnd"].ToString();
                row[5] = dt.Rows[i]["makh"].ToString();
                if (dt.Rows[i]["ngaysinh"].ToString() != "")
                    ngaysinh = dt.Rows[i]["ngaysinh"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(6, 4);
                row[6] = ngaysinh;
                if (dt.Rows[i]["gioitinh"].ToString() == "True")
                    gioitinh = "Nam";
                else
                    gioitinh = "Nữ";
                row[7] = gioitinh;
                row[8] = dt.Rows[i]["ghichu"].ToString();
                row[9] = true;

                dshd.Rows.Add(row);

            }
            dataGridViewX2.DataSource = dshd;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dataGridViewX2.Columns[4].Visible = false;
            //dataGridViewX1.Columns[6].Visible = false;            
            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewX2.Columns[2].Visible = false;
            dataGridViewX2.Columns[3].Visible = false;
            dataGridViewX2.Columns[6].Visible = false;

            Cursor.Current = Cursors.Default;
        }

        private void buttonX125_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dshd = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dshd.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Điện thoại", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Chọn", typeof(bool));
            dshd.Columns.Add(col);

            DataTable dt = new DataTable();
            String sCommand = "";
            sCommand = "SELECT khachhangtiemnang.* from khachhangtiemnang where khachhangtiemnang.makh like '%" + textBox35.Text + "%' and macn='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();

            int iRows = dt.Rows.Count;
            String gioitinh = "", ngaysinh = "";
            for (int i = 0; i < iRows; i++)
            {
                DataRow row = dshd.NewRow();
                row[0] = i + 1;

                row[1] = dt.Rows[i]["hoten"].ToString();

                row[2] = dt.Rows[i]["diachi1"].ToString();
                row[3] = dt.Rows[i]["dienthoai1"].ToString();
                row[4] = dt.Rows[i]["cmnd"].ToString();
                row[5] = dt.Rows[i]["makh"].ToString();
                if (dt.Rows[i]["ngaysinh"].ToString() != "")
                    ngaysinh = dt.Rows[i]["ngaysinh"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["ngaysinh"].ToString().Substring(6, 4);
                row[6] = ngaysinh;
                if (dt.Rows[i]["gioitinh"].ToString() == "True")
                    gioitinh = "Nam";
                else
                    gioitinh = "Nữ";
                row[7] = gioitinh;
                row[8] = dt.Rows[i]["ghichu"].ToString();
                row[9] = true;

                dshd.Rows.Add(row);

            }
            dataGridViewX2.DataSource = dshd;

            dataGridViewX2.Columns[0].FillWeight = 30;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dataGridViewX2.Columns[4].Visible = false;
            //dataGridViewX1.Columns[6].Visible = false;            
            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewX2.Columns[2].Visible = false;
            dataGridViewX2.Columns[3].Visible = false;
            dataGridViewX2.Columns[6].Visible = false;

            Cursor.Current = Cursors.Default;
        }

        private void buttonX120_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String sCommand = "";
            for (int i = 0; i < dataGridViewX2.RowCount; i++)
            {
                if (dataGridViewX2.Rows[i].Cells[9].Value.ToString() == "True")
                {
                    try
                    {
                        sCommand = "INSERT INTO khtn_nhomkhtn VALUES('" + dataGridViewX2.Rows[i].Cells[5].Value.ToString() + "','" + dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[7].Value.ToString() + "')";
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
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Đã thêm!");
        }

        private void buttonX114_Click(object sender, EventArgs e)
        {
            int dem = 0;

            for (int i = 0; i < dataGridViewX2.RowCount; i++)
            {
                if (dataGridViewX2.Rows[i].Cells[9].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }

            if (dem > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                String sCommand = "";
                for (int i = 0; i < dataGridViewX2.RowCount; i++)
                {
                    if (dataGridViewX2.Rows[i].Cells[9].Value.ToString() == "True")
                    {
                        try
                        {
                            sCommand = "Delete khtn_nhomkhtn  where makh ='" + dataGridViewX2.Rows[i].Cells[5].Value.ToString() + "' and manhom ='" + dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[7].Value.ToString() + "'";
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
                timkiemkhachhangtheonhom(manhom);
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã xóa!");
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                String sCommand = "";
                try
                {
                    sCommand = "Delete NhomkhachhangTN  where manhom ='" + dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[7].Value.ToString() + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();

                    laydanhsachnhomkhachhang();

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Đã xóa!");
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    MessageBox.Show("Có nhiều khách hàng trong nhóm này, không xóa được!");                    
                }
            }
            else
            {
                MessageBox.Show("Không có nhóm khách hàng nào.");
            } 
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //CRM.frmNhom_LoaiKH form_NL = new frmNhom_LoaiKH();
            //form_NL.ShowDialog();
        }
    }
}