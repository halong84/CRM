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
    public partial class frmKH_GiaoDich : Form
    {
        public static string makh = "", hoten = "", loaikh = "HH", magd = "";
        public static int loaikhcn_dn = 1;
        
        public frmKH_GiaoDich()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void labelX11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonX19_Click(object sender, EventArgs e)
        {
            frmMain.flagSearch = 1;
            CRM.frmKH_Search form_ct = new frmKH_Search();
            form_ct.ShowDialog();
            txtMakh.Text = makh;
            lblHotenKH.Text = hoten;
        }

        private void frmKH_Giaodich_Load(object sender, EventArgs e)
        {
            dtpNgaybd.CustomFormat= "dd/MM/yyyy";
            dtpNgaykt.CustomFormat = "dd/MM/yyyy";            
            dtpGioBD.CustomFormat = "HH:mm";
            dtpGioKT.CustomFormat = "HH:mm";
            txtMaNV.Text = Thongtindangnhap.user_id;
            lblTenNV.Text = CRM.frmDangnhap.hoten;
            layLoaiGD();
            layKMCP();
            txtMaGD.Enabled = false;
            txtMaNV.Enabled = false;
            lblTenNV.Enabled = false;
            txtChiPhi.Enabled = false;
            txtMakh.Enabled = true;
            cbNLH.Enabled = false;
            cbHTGD.Enabled = false;
            cbDoUuTien.Enabled = false;
            txtNoiDung.Enabled = false;
            txtDiaDiem.Enabled = false;
            cbDanhgia.Enabled = false;
            txtGhichu.Enabled = false;
            cbKMCP.Enabled = false;
            buttonX19.Enabled = false;
        }

        private void layLoaiGD()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT maloaiGD,tenloai from loaigiaodich ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbLoaiGD.DataSource = dt;
            cbLoaiGD.DisplayMember = "Tenloai";
            cbLoaiGD.ValueMember = "Maloaigd";
            cbLoaiGD.DataSource = dt;
        }

        private void layKMCP()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT macp,diengiai from dmkhoanmuccp ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbKMCP.DataSource = dt;
            cbKMCP.DisplayMember = "diengiai";
            cbKMCP.ValueMember = "macp";
            cbKMCP.DataSource = dt;
        }

        private void layNguoiLH()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT manlh,hoten from nguoilienhe where makh='"+txtMakh.Text+"'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbNLH.DataSource = dt;
            cbNLH.DisplayMember = "hoten";
            cbNLH.ValueMember = "Manlh";
            cbNLH.DataSource = dt;

        }

        private void optHH_Click(object sender, EventArgs e)
        {
            if (optHH.Checked == true)
            {
                loaikh = "HH";
                groupBox1.Visible = true;
            }    
        }

        private void optTN_Click(object sender, EventArgs e)
        {
            if (optTN.Checked == true)
            {
                loaikh = "TN";
                groupBox1.Visible = false;
            }
        }

        private void optCN_Click(object sender, EventArgs e)
        {
            if (optCN.Checked == true)
                loaikhcn_dn = 1;
        }

        private void optDN_Click(object sender, EventArgs e)
        {
            if (optDN.Checked == true)
                loaikhcn_dn = 2;
        }

        private void txtMakh_TextChanged(object sender, EventArgs e)
        {
            if((optHH.Checked==true) &&(optDN.Checked==true))
            {
                layNguoiLH();
            }
            layGiaoDich();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            magd = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
            txtMaGD.Text = magd;
            txtMaNV.Text=Thongtindangnhap.user_id;
            lblTenNV.Text =CRM.frmDangnhap.hoten;
            txtChiPhi.Text = "0";            
            txtMakh.Text = "";
            cbNLH.Text = "";
            cbHTGD.Text = "";
            cbDoUuTien.Text = "";
            txtNoiDung.Text = "";
            txtDiaDiem.Text = "";
            cbDanhgia.Text = "";
            txtGhichu.Text = "";
            txtMaGD.Enabled = true;
            //txtMaNV.Enabled = true;
            lblTenNV.Enabled = true;
            txtChiPhi.Enabled = true;
            txtMakh.Enabled = true;
            cbNLH.Enabled = true;
            cbHTGD.Enabled = true;
            cbDoUuTien.Enabled = true;
            txtNoiDung.Enabled = true;
            txtDiaDiem.Enabled = true;
            cbDanhgia.Enabled = true;
            txtGhichu.Enabled = true;
            cbKMCP.Enabled = true;
            btnAdd.Enabled = false;
            btnLuu.Enabled = true;
            btnXoa.Enabled = false;
            buttonX19.Enabled = true;

        }

        private void layGiaoDich()
        {
            DataTable dt = new DataTable();
            String sCommand="";

            if ((loaikh == "HH") && (loaikhcn_dn == 2))
            {
                if (txtMakh.Text == "")
                    sCommand = "SELECT giaodich.*,khachhang.hoten,nguoilienhe.hoten,loaigiaodich.tenloai,DMKhoanmucCP.Diengiai from khachhang,giaodich,nguoilienhe,loaigiaodich,DMKhoanmucCP where giaodich.maloaigd=loaigiaodich.maloaigd and giaodich.makh=khachhang.makh and giaodich.manlh=nguoilienhe.manlh and giaodich.macp=dmkhoanmuccp.macp and giaodich.user_id='" + txtMaNV.Text+ "'";
                else
                    sCommand = "SELECT giaodich.*,khachhang.hoten,nguoilienhe.hoten,loaigiaodich.tenloai,DMKhoanmucCP.Diengiai from khachhang,giaodich,nguoilienhe,loaigiaodich,DMKhoanmucCP where giaodich.maloaigd=loaigiaodich.maloaigd and giaodich.makh=khachhang.makh and giaodich.manlh=nguoilienhe.manlh and giaodich.macp=dmkhoanmuccp.macp and giaodich.user_id='" + txtMaNV.Text + "' and khachhang.makh='" + txtMakh.Text + "'";
            }
            else
            {
                if (loaikh == "TN")
                {
                    if (txtMakh.Text == "")
                        sCommand = "SELECT giaodich.*,khachhangtiemnang.hoten,loaigiaodich.tenloai,DMKhoanmucCP.Diengiai from khachhangtiemnang,giaodich,loaigiaodich,DMKhoanmucCP where giaodich.maloaigd=loaigiaodich.maloaigd and giaodich.makh=khachhangtiemnang.makh and giaodich.macp=dmkhoanmuccp.macp and giaodich.user_id='" + txtMaNV.Text + "'";
                    else
                        sCommand = "SELECT giaodich.*,khachhangtiemnang.hoten,loaigiaodich.tenloai,DMKhoanmucCP.Diengiai from khachhangtiemnang,giaodich,loaigiaodich,DMKhoanmucCP where giaodich.maloaigd=loaigiaodich.maloaigd and giaodich.makh=khachhangtiemnang.makh and giaodich.macp=dmkhoanmuccp.macp and giaodich.user_id='" + txtMaNV.Text + "' and khachhangtiemnang.makh='" + txtMakh.Text + "'";
                }
                else
                {
                    if(txtMakh.Text=="")
                        sCommand = "SELECT giaodich.*,khachhang.hoten,loaigiaodich.tenloai,DMKhoanmucCP.Diengiai from khachhang,giaodich,loaigiaodich,DMKhoanmucCP where giaodich.maloaigd=loaigiaodich.maloaigd and giaodich.makh=khachhang.makh and giaodich.macp=dmkhoanmuccp.macp and giaodich.user_id='" + txtMaNV.Text + "'";
                    else
                        sCommand = "SELECT giaodich.*,khachhang.hoten,loaigiaodich.tenloai,DMKhoanmucCP.Diengiai from khachhang,giaodich,loaigiaodich,DMKhoanmucCP where giaodich.maloaigd=loaigiaodich.maloaigd and giaodich.makh=khachhang.makh and giaodich.macp=dmkhoanmuccp.macp and giaodich.user_id='" + txtMaNV.Text + "' and khachhang.makh='" + txtMakh.Text + "'";
                }
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Loại giao dịch", typeof(string));
            dskh.Columns.Add(col);           
            col = new DataColumn("Nội dung", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Địa điểm", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Bắt đầu", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Kết thúc", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Đánh giá", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số tiền", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Hình thức GD", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Độ ưu tiên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã giao dịch", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Khoản mục chi phí", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên KH", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    row[0] = i + 1;
                    //DataTable dt = new DataTable();
                   // sCommand = "select * from loaigiaodich where maloaigd='" + dt.Rows[i]["maloaigd"].ToString() + "'";
                    //DataAccess.conn.Open();
                    //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                    //DataAccess.conn.Close();

                    row[1] = dt.Rows[0]["tenloai"].ToString();
                    row[2] = dt.Rows[i]["noidung"].ToString();
                    row[3] = dt.Rows[i]["diadiem"].ToString();
                    row[4] = dt.Rows[i]["tgbatdau"].ToString();
                    row[5] = dt.Rows[i]["tgketthuc"].ToString();
                    row[6] = dt.Rows[i]["danhgia"].ToString();
                    row[7] = dt.Rows[i]["Chiphi"].ToString();
                    row[8] = dt.Rows[i]["Ghichu"].ToString();
                    row[9] = dt.Rows[i]["hinhthucGD"].ToString();
                    row[10] = dt.Rows[i]["Douutien"].ToString();
                    row[11] = dt.Rows[i]["magd"].ToString();
                    row[12] = dt.Rows[i]["diengiai"].ToString();
                    row[13] = dt.Rows[i]["hoten"].ToString();
                    row[14] = dt.Rows[i]["makh"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dataGridViewX2.DataSource = dskh;

            dataGridViewX2.Columns[0].FillWeight = 20;
            dataGridViewX2.Columns[0].Width = 40;
            dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewX2.Columns[11].Visible = false;
            dataGridViewX2.Columns[14].Visible = false;
            dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //Dua du lieu vao bang GiaoDich
                String ngaybd, ngaykt,sCommand,manlh;
                if (cbNLH.SelectedValue == null)
                {
                    manlh = "";
                }
                else
                {
                    manlh = cbNLH.SelectedValue.ToString();
                }
                ngaybd = dtpNgaybd.Text.Substring(3, 2) + "/" + dtpNgaybd.Text.Substring(0, 2) + "/" + dtpNgaybd.Text.Substring(6, 4)+" "+ dtpGioBD.Text ;
                ngaykt = dtpNgaykt.Text.Substring(3, 2) + "/" + dtpNgaykt.Text.Substring(0, 2) + "/" + dtpNgaykt.Text.Substring(6, 4) + " " + dtpGioKT.Text;
                sCommand = "insert into GiaoDich(magd,makh,manlh,noidung,diadiem,TGBatdau,TGketthuc,danhgia,douutien,chiphi,maloaigd,hinhthucgd,ghichu,user_id,macp) values('" + txtMaGD.Text + "','" + txtMakh.Text + "','" + manlh + "',N'" + txtNoiDung.Text + "',N'" + txtDiaDiem.Text + "','" + ngaybd + "','" + ngaykt + "',N'" + cbDanhgia.Text + "',N'" + cbDoUuTien.Text + "'," + Convert.ToDecimal(txtChiPhi.Text.Replace(",", "")) + ",'" + cbLoaiGD.SelectedValue.ToString() + "',N'" + cbHTGD.Text + "',N'" + txtGhichu.Text + "','" + txtMaNV.Text + "','" + cbKMCP.SelectedValue.ToString() + "')";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                layGiaoDich();                
                
            }
            catch 
            //Cap nhat giao dich
            {
                if (cbHTGD.Text != "")
                {
                    String ngaybd, ngaykt, sCommand;
                    ngaybd = dtpNgaybd.Text.Substring(3, 2) + "/" + dtpNgaybd.Text.Substring(0, 2) + "/" + dtpNgaybd.Text.Substring(6, 4) + " " + dtpGioBD.Text;
                    ngaykt = dtpNgaykt.Text.Substring(3, 2) + "/" + dtpNgaykt.Text.Substring(0, 2) + "/" + dtpNgaykt.Text.Substring(6, 4) + " " + dtpGioKT.Text;
                    sCommand = "Update GiaoDich set noidung =N'" + txtNoiDung.Text + "',diadiem=N'" + txtDiaDiem.Text + "',TGBatdau='" + ngaybd + "',TGketthuc='" + ngaykt + "',danhgia=N'" + cbDanhgia.Text + "',douutien=N'" + cbDoUuTien.Text + "',chiphi='" + Convert.ToDecimal(txtChiPhi.Text.Replace(",", "")) + "',maloaigd='" + cbLoaiGD.SelectedValue.ToString() + "',macp='" + cbKMCP.SelectedValue.ToString() + "',hinhthucgd=N'" + cbHTGD.Text + "',ghichu=N'" + txtGhichu.Text + "' where magd='" + txtMaGD.Text + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layGiaoDich();
                }
            }
            btnAdd.Enabled =true;
            
        }

        private void txtChiPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtChiPhi_TextChanged(object sender, EventArgs e)
        {
            if (txtChiPhi.Text != "")    
            {
                string sDummy = txtChiPhi.Text;
                try
                {
                    int iKeep = txtChiPhi.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                        if (txtChiPhi.Text[i] == ',')
                            iKeep -= 1;

                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                        if (sDummy[i] == ',')
                            iKeep += 1;

                    txtChiPhi.Text = sDummy;
                    txtChiPhi.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }

        }

        private void dataGridViewX2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbLoaiGD.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[1].Value.ToString();
                cbHTGD.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[9].Value.ToString();
                cbDoUuTien.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[10].Value.ToString();
                txtChiPhi.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[7].Value.ToString();
                txtNoiDung.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[2].Value.ToString();
                txtDiaDiem.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[3].Value.ToString();
                cbDanhgia.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[6].Value.ToString();
                txtGhichu.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[8].Value.ToString();
                txtMaGD.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[11].Value.ToString();
                cbKMCP.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[12].Value.ToString();
                dtpNgaybd.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[4].Value.ToString().Substring(0,10);
                dtpNgaykt.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[5].Value.ToString().Substring(0,10);
                dtpGioBD.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[4].Value.ToString().Substring(10,5);
                dtpGioKT.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[5].Value.ToString().Substring(10, 5);
                txtMakh.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[14].Value.ToString();
                lblHotenKH.Text = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[13].Value.ToString();
                txtMaGD.Enabled = true;
                txtMaNV.Enabled = true;
                lblTenNV.Enabled = true;
                txtChiPhi.Enabled = true;
                txtMakh.Enabled = true;
                cbNLH.Enabled = true;
                cbHTGD.Enabled = true;
                cbDoUuTien.Enabled = true;
                txtNoiDung.Enabled = true;
                txtDiaDiem.Enabled = true;
                cbDanhgia.Enabled = true;
                txtGhichu.Enabled = true;
                cbKMCP.Enabled = true;
                btnAdd.Enabled = true;
                btnLuu.Enabled = true;
                btnXoa.Enabled = true;

            }
            catch { }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String sCommand = "delete giaodich where magd='" + txtMaGD.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();
            layGiaoDich();
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnAdd.Enabled = true;
            txtChiPhi.Text = "0";
            //txtMakh.Text = "";
            cbNLH.Text = "";
            cbHTGD.Text = "";
            cbDoUuTien.Text = "";
            txtNoiDung.Text = "";
            txtDiaDiem.Text = "";
            cbDanhgia.Text = "";
            txtGhichu.Text = "";
            txtMaGD.Enabled = false;
            txtMaNV.Enabled = false;
            lblTenNV.Enabled = false;
            txtChiPhi.Enabled = false;
            txtMakh.Enabled = false;
            cbNLH.Enabled = false;
            cbHTGD.Enabled = false;
            cbDoUuTien.Enabled = false;
            txtNoiDung.Enabled = false;
            txtDiaDiem.Enabled = false;
            cbDanhgia.Enabled = false;
            txtGhichu.Enabled = false;
            layGiaoDich();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            layGiaoDich();
        }        
    }
}