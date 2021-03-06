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
using Excel = Microsoft.Office.Interop.Excel;
using N_MicrosoftExcelClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmLichchamsoc : Form
    {
        public static string maTieuchi = "", tuthang="",denthang="",xeploaikh="";
        public static int loaikh;
        public static string strma = "";
        string strCmd = "";
        public static decimal kinhphi = 0, tongkinhphi = 0;
        private DataTable dtResult = new DataTable();

        public frmLichchamsoc()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbTieuchi.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbNV.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;
        }

        private void frmLichchamsoc_Load(object sender, EventArgs e)
        {
            
            optCN.Checked = true;
            layDS_Tieuchi();
            layLoaiKH();
            layDanhsach();
            //dtpThang.Enabled = false;
            grbLoaiKH.Enabled = true;
            cbTieuchi.Enabled = false;
            cbLoaiKH.Enabled = false;
            txtChiphi.Enabled = false;
            dtpNgayBD.Enabled = false;
            dtpNgayKT.Enabled = false;
            txtNoiDung.Enabled = false;
            
            btnModify.Enabled = false;
            btnDel.Enabled = false;
            btnAdd.Enabled = true;
            
        }

        private void layLoaiKH()
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                sCommand = "SELECT * from DMXEPLOAIKH where loaikh=1 or MALOAI='9999' order by MALOAI desc";
            }
            else
            {
                sCommand = "SELECT * from DMXEPLOAIKH where loaikh=2 or MALOAI='9999' order by MALOAI desc";
            }
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbLoaiKH.DataSource = dt;
            cbLoaiKH.DisplayMember = "TenLoai";
            cbLoaiKH.ValueMember = "MaLoai";
            cbLoaiKH.DataSource = dt;
            if(optCN.Checked==true)
                cbLoaiKH.Text="VIP Cá nhân";
            if (optDN.Checked == true)
                cbLoaiKH.Text = "VIP Doanh nghiệp";
            cbLoaiKH.Enabled = false;
            
            //lblTenloai.Text = dt.Rows[0]["Tenloai"].ToString();
        }        


        private void layDS_Tieuchi()
        {
            DataTable dt = new DataTable();
            String sCommand="";
            if(optDN.Checked==true)
                sCommand= "SELECT * FROM LICHCHAMSOC where ghichu like '%DN%' ";
            else
                sCommand = "SELECT * FROM LICHCHAMSOC where ghichu like '%CN%' ";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            //cbTieuchi.DataSource = dt;
            cbTieuchi.DisplayMember = "TIEUCHI";
            cbTieuchi.ValueMember = "MATC";
            cbTieuchi.DataSource = dt;
            cbTieuchi.SelectedIndex = 0;
        }

        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dt = new DataTable();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã sự kiện", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên sự kiện", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Đối tượng KH", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Kinh phí", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tổng kinh phí", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nội dung", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);                            
            col = new DataColumn("Phê duyệt", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Quý", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));
            dtDanhsach.Columns.Add(col);
            String quy = cbQuy.Text + "/" + dtpThang.Text;
            if (optCN.Checked == true)
            {
                strCmd = "Select cs.*, xl.TENLOAI, li.Tieuchi as tieuchi  from KEHOACHCHAMSOC cs, DMXEPLOAIKH xl, Lichchamsoc li ";
                strCmd += " Where cs.thang='" + quy + "' and xl.maloai=cs.xeploaikh and cs.matc=li.MaTC and cs.MACN='" + Thongtindangnhap.macn + "' and cs.loaikh=1 ";
            }
            if (optDN.Checked == true)
            {
                strCmd = "Select cs.*, xl.TENLOAI, li.Tieuchi as tieuchi  from KEHOACHCHAMSOC cs, DMXEPLOAIKH xl, Lichchamsoc li ";
                strCmd += " Where cs.thang='" + quy + "' and xl.maloai=cs.xeploaikh and cs.matc=li.MaTC and cs.MACN='" + Thongtindangnhap.macn + "' and cs.loaikh=2 ";
            }
            if (optLDDN.Checked == true)
            {
                strCmd = "Select cs.*, xl.TENLOAI, li.Tieuchi as tieuchi  from KEHOACHCHAMSOC cs, DMXEPLOAIKH xl, Lichchamsoc li ";
                strCmd += " Where cs.thang='" + quy + "' and xl.maloai=cs.xeploaikh and cs.matc=li.MaTC and cs.MACN='" + Thongtindangnhap.macn + "' and cs.loaikh=3 ";
            }

            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();           

            int iRows = dt.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["Ma"].ToString();
                    row[2] = dt.Rows[i]["MATC"].ToString();
                    row[3] = dt.Rows[i]["tieuchi"].ToString();
                    row[4] = dt.Rows[i]["tenloai"].ToString();                  
                    row[5] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["Kinhphi"].ToString()));
                    row[6] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["tongKinhphi"].ToString()));
                    row[7] = dt.Rows[i]["noidung"].ToString();
                   
                    string ngaybatdau, ngayketthuc;
                    ngaybatdau = dt.Rows[i]["thoigianbd"].ToString();
                    ngayketthuc = dt.Rows[i]["thoigiankt"].ToString();

                    string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
                    ngayBD = ngaybatdau.Substring(0, 2);
                    thangBD = ngaybatdau.Substring(3, 2);
                    namBD = ngaybatdau.Substring(6, 4);
                    ngayKT = ngayketthuc.Substring(0, 2);
                    thangKT = ngayketthuc.Substring(3, 2);
                    namKT = ngayketthuc.Substring(6, 4);

                    row[8] = ngayBD + "/" + thangBD + "/" + namBD;
                    row[9] = ngayKT + "/" + thangKT + "/" + namKT;                    

                    string pheduyet = "";
                    if (Boolean.Parse(dt.Rows[i]["pheduyet"].ToString()) == true)
                    {
                        pheduyet = "Đã phê duyệt";
                    }
                    else
                    {
                        pheduyet = "Chưa phê duyệt";
                    }
                    row[10] = pheduyet;
                    row[11] = dt.Rows[i]["Thang"].ToString();
                    row[12] = dt.Rows[i]["loaikh"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;            
            dgvDanhsach.Columns[1].Visible = false;
            dgvDanhsach.Columns[2].Visible = false;
            dgvDanhsach.Columns[11].Visible = false;
            dgvDanhsach.Columns[12].Visible = false;
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            maTieuchi = cbTieuchi.SelectedValue.ToString();
            if (cbQuy.Text == "01")
            {
                tuthang = "01/" + dtpThang.Text;
                denthang = "03/" + dtpThang.Text;
            }
            if (cbQuy.Text == "02")
            {
                tuthang = "04/" + dtpThang.Text;
                denthang = "06/" + dtpThang.Text;
            }
            if (cbQuy.Text == "03")
            {
                tuthang = "07/" + dtpThang.Text;
                denthang = "09/" + dtpThang.Text;
            }
            if (cbQuy.Text == "04")
            {
                tuthang = "10/" + dtpThang.Text;
                denthang = "12/" + dtpThang.Text;
            }                
            if (optCN.Checked == true)
                loaikh = 1;
            if (optDN.Checked == true)
                loaikh = 2;
            if (optLDDN.Checked == true)
                loaikh = 3;
            if(cbLoaiKH.Text=="Tất cả")
                xeploaikh = "9999";
            else
                xeploaikh = cbLoaiKH.SelectedValue.ToString();
            frmKHChamsoc frmKH = new frmKHChamsoc();
            frmKH.ShowDialog();

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strma = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
            txtMa.Text =strma;
            dtpThang.Enabled = true;
            grbLoaiKH.Enabled = true;
            cbTieuchi.Enabled = true;
            cbLoaiKH.Enabled = true;
            txtChiphi.Enabled = true;
            dtpNgayBD.Enabled = true;
            dtpNgayKT.Enabled = true;
            txtNoiDung.Enabled = true;
           
            btnModify.Enabled = true;
            btnDel.Enabled = false;
            btnAdd.Enabled = false;
            cbTieuchi.Text = "";
            cbLoaiKH.Text="";
            txtChiphi.Text = "0";
            txtNoiDung.Text = "";
            layLoaiKH();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string sCmd = "";
            DataTable dt = new DataTable();
            SqlTransaction trans;
            
            if (txtChiphi.Text == "")
            {
                kinhphi = 0;
                tongkinhphi = 0;
            }
            else
            {
                kinhphi = Convert.ToDecimal(txtChiphi.Text.Replace(",", ""));
            }
            if (frmKHChamsoc.arrMaKH.Count <= 0)
            {
                MessageBox.Show("Chưa chọn khách hàng.", "Thông báo");
                btnKH.Focus();
                return;
            }
            sCmd = "select * from kehoachchamsoc where ma='" + txtMa.Text + "'";
            
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            dt.Clear();
            new SqlDataAdapter(sCmd, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            string ngaybd = "", ngaykt = "", ngaycapnhat = "";
            ngaybd = dtpNgayBD.Text.Substring(3, 2) + "/" + dtpNgayBD.Text.Substring(0, 2) + "/" + dtpNgayBD.Text.Substring(6, 4);
            ngaykt = dtpNgayKT.Text.Substring(3, 2) + "/" + dtpNgayKT.Text.Substring(0, 2) + "/" + dtpNgayKT.Text.Substring(6, 4);
            ngaycapnhat = DateTime.Now.ToShortDateString().Substring(3, 2) + "/" + DateTime.Now.ToShortDateString().Substring(0, 2) + "/" + DateTime.Now.ToShortDateString().Substring(6, 4);
            //Them moi ke hoach cham soc
            if (dt.Rows.Count == 0)
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                trans = DataAccess.conn.BeginTransaction();
                try
                {
                                      
                    //frmMain.myCommand.Transaction = trans;
                    //Insert chi tiet khcs

                    tongkinhphi = 0;
                    for (int i = 0; i < frmKHChamsoc.arrMaKH.Count; i++)
                    {
                        sCmd = "insert into chitietKHCS(ma,makh) values('" + txtMa.Text + "','" + frmKHChamsoc.arrMaKH[i].ToString() + "')";
                        frmMain.myCommand = new SqlCommand(sCmd, DataAccess.conn, trans);
                        frmMain.myCommand.ExecuteNonQuery();
                        tongkinhphi = tongkinhphi + kinhphi;
                    }
                    
                    //Insert ke hoach cham soc
                    string quy = cbQuy.Text + "/" + dtpThang.Text;
                    sCmd = "insert into kehoachchamsoc(ma,thang,matc,xeploaikh,kinhphi,tongkinhphi,noidung,loaikh,thoigianbd,thoigiankt,ngaycapnhat,macn)";
                    sCmd = sCmd + " values('" + txtMa.Text + "','" + quy + "','" + cbTieuchi.SelectedValue.ToString() + "','" + cbLoaiKH.SelectedValue.ToString() + "'," + kinhphi + "," + tongkinhphi + ",N'" + txtNoiDung.Text + "'," + loaikh + ",'" + ngaybd + "','" + ngaykt + "','" + ngaycapnhat + "','" + Thongtindangnhap.macn + "')";
                    frmMain.myCommand = new SqlCommand(sCmd, DataAccess.conn,trans);                   
                    frmMain.myCommand.ExecuteNonQuery();                  
                                       
                    frmMain.myCommand.Transaction.Commit();
                    lblTongKP.Text = Convert.ToString(tongkinhphi);
                    MessageBox.Show("Kế hoạch đã được lưu");
                }
                catch
                {
                    frmMain.myCommand.Transaction.Rollback();
                    MessageBox.Show("Insert không thành công, kiểm tra lại ngày hệ thống!");
                }
                DataAccess.conn.Close();
            }
            //Cap nhat ke hoach cham soc
            else
            {
                sCmd = "delete chitietKHCS where ma='" + txtMa.Text + "'";

                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCmd, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                tongkinhphi = 0;
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                trans = DataAccess.conn.BeginTransaction();
                try
                {
                    for (int i = 0; i < frmKHChamsoc.arrMaKH.Count; i++)
                    {
                        sCmd = "insert into chitietKHCS(ma,makh) values('" + txtMa.Text + "','" + frmKHChamsoc.arrMaKH[i].ToString() + "')";
                        frmMain.myCommand = new SqlCommand(sCmd, DataAccess.conn, trans);
                        try
                        { frmMain.myCommand.ExecuteNonQuery(); }
                        catch { }
                        tongkinhphi = tongkinhphi + kinhphi;
                    }
                    //Update ke hoach cham soc
                    sCmd = "update kehoachchamsoc set kinhphi="+kinhphi+",tongkinhphi="+tongkinhphi+",noidung=N'"+txtNoiDung.Text+"',thoigianbd='"+ngaybd+"',thoigiankt='"+ngaykt+"',ngaycapnhat='"+ngaycapnhat+"' where ma='"+txtMa.Text+"'";                    
                    frmMain.myCommand = new SqlCommand(sCmd, DataAccess.conn, trans);
                    frmMain.myCommand.ExecuteNonQuery();

                    frmMain.myCommand.Transaction.Commit();
                    lblTongKP.Text = Convert.ToString(tongkinhphi);
                    MessageBox.Show("Kế hoạch đã được lưu");
                }
                catch { }
            }
            layDanhsach();
            btnAdd.Enabled = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            String sCommand = "delete kehoachchamsoc where ma='" + txtMa.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();
            layDanhsach();
            //dtpThang.Enabled = false;
            grbLoaiKH.Enabled = false;
            cbTieuchi.Enabled = false;
            cbLoaiKH.Enabled = false;
            txtChiphi.Enabled = false;
            dtpNgayBD.Enabled = false;
            dtpNgayKT.Enabled = false;
            txtNoiDung.Enabled = false;
            lblTongKP.Text = "0";
            btnModify.Enabled = false;
            btnDel.Enabled = false;
            btnAdd.Enabled = true;

        }

        private void dgvDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dtpThang.Enabled = true;
            grbLoaiKH.Enabled = true;
            cbTieuchi.Enabled = true;
            cbLoaiKH.Enabled = true;
            txtChiphi.Enabled = true;
            dtpNgayBD.Enabled = true;
            dtpNgayKT.Enabled = true;
            txtNoiDung.Enabled = true;
            
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            try
            {
                dtpNgayBD.Text = dgvDanhsach.CurrentRow.Cells["Ngày bắt đầu"].Value.ToString();
                dtpNgayKT.Text = dgvDanhsach.CurrentRow.Cells["Ngày kết thúc"].Value.ToString();
                //dtpThang.Text = dgvDanhsach.CurrentRow.Cells["Tháng"].Value.ToString();
                cbTieuchi.Text = dgvDanhsach.CurrentRow.Cells["Tên sự kiện"].Value.ToString();
                
                txtMa.Text = dgvDanhsach.CurrentRow.Cells["Mã"].Value.ToString();
                strma = txtMa.Text;
                txtChiphi.Text = dgvDanhsach.CurrentRow.Cells["Kinh phí"].Value.ToString();
                txtNoiDung.Text = dgvDanhsach.CurrentRow.Cells["Nội dung"].Value.ToString();
                dtpThang.Text = "01/01/" + dgvDanhsach.CurrentRow.Cells["Quý"].Value.ToString().Substring(3, 4);
                cbQuy.Text = dgvDanhsach.CurrentRow.Cells["Quý"].Value.ToString().Substring(0, 2);
                lblTongKP.Text = "Tổng kinh phí: " +dgvDanhsach.CurrentRow.Cells["Tổng kinh phí"].Value.ToString();
                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "1")                    
                    optCN.Checked = true;
                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "2")  
                    optDN.Checked = true;
                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "3")
                    optLDDN.Checked = true;
                layLoaiKH();
                cbLoaiKH.Text = dgvDanhsach.CurrentRow.Cells["Đối tượng KH"].Value.ToString();
                
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void btnExcel_Click(object sender, EventArgs e)
        {
            String temp = "Ke hoach cham soc KH";
            
            saveFileDialog1.FileName = temp.Replace("/", "-");
            saveFileDialog1.Filter = " Excel (*.xls)|*.xls|Tất cả (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            string path = "";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                path = saveFileDialog1.FileName;
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 10;

                for (int i = 0; i < dgvDanhsach.Columns.Count; i++)
                {
                    ExcelApp.Cells[1, i+1] = dgvDanhsach.Columns[i].Name;
                }
                
                for (int i = 1; i <= dgvDanhsach.Rows.Count; i++)
                {
                    DataGridViewRow row = dgvDanhsach.Rows[i-1];
                    for (int j = 1; j <= row.Cells.Count; j++)
                    {
                        ExcelApp.Cells[i + 1, j] = row.Cells[j-1].Value.ToString();
                    }
                }

                ExcelApp.ActiveWorkbook.SaveCopyAs(path);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Đã xuất ra file excel.");
            }
            Cursor.Current = Cursors.Default;
        }

        private void txtChiphi_TextChanged(object sender, EventArgs e)
        {
            if (txtChiphi.Text != "")
            {
                string sDummy = txtChiphi.Text;
                try
                {
                    int iKeep = txtChiphi.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtChiphi.Text[i] == ',')
                        {
                            iKeep -= 1;
                        }
                    }
                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                    {
                        if (sDummy[i] == ',')
                        {
                            iKeep += 1;
                        }
                    }
                    txtChiphi.Text = sDummy;
                    txtChiphi.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtChiphi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void optCN_Click(object sender, EventArgs e)
        {
            optCN.Checked = true;
            optDN.Checked = false;
            layDS_Tieuchi();
            layLoaiKH();
        }

        private void optDN_Click(object sender, EventArgs e)
        {
            optDN.Checked = true;
            optCN.Checked = false;
            layDS_Tieuchi();
            layLoaiKH();
        }

        private void dtpThang_DropDown(object sender, EventArgs e)
        {
            layDanhsach();
        }

        private void dtpThang_Leave(object sender, EventArgs e)
        {
            layDanhsach();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            layDanhsach();
        }

        private void optLDDN_Click(object sender, EventArgs e)
        {
            optDN.Checked = false;
            optCN.Checked = false;
            optLDDN.Checked = true;
            layDS_Tieuchi();
            layLoaiKH();
        }

        private void dgvDanhsach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtpThang.Enabled = true;
            grbLoaiKH.Enabled = true;
            cbTieuchi.Enabled = true;
            cbLoaiKH.Enabled = true;
            txtChiphi.Enabled = true;
            dtpNgayBD.Enabled = true;
            dtpNgayKT.Enabled = true;
            txtNoiDung.Enabled = true;

            btnModify.Enabled = true;
            btnDel.Enabled = true;
            try
            {
                dtpNgayBD.Text = dgvDanhsach.CurrentRow.Cells["Ngày bắt đầu"].Value.ToString();
                dtpNgayKT.Text = dgvDanhsach.CurrentRow.Cells["Ngày kết thúc"].Value.ToString();
                //dtpThang.Text = dgvDanhsach.CurrentRow.Cells["Tháng"].Value.ToString();
                cbTieuchi.Text = dgvDanhsach.CurrentRow.Cells["Tên sự kiện"].Value.ToString();

                txtMa.Text = dgvDanhsach.CurrentRow.Cells["Mã"].Value.ToString();
                strma = txtMa.Text;
                txtChiphi.Text = dgvDanhsach.CurrentRow.Cells["Kinh phí"].Value.ToString();
                txtNoiDung.Text = dgvDanhsach.CurrentRow.Cells["Nội dung"].Value.ToString();
                dtpThang.Text = "01/01/"+dgvDanhsach.CurrentRow.Cells["Quý"].Value.ToString().Substring(3,4);
                cbQuy.Text = dgvDanhsach.CurrentRow.Cells["Quý"].Value.ToString().Substring(0,2);
                lblTongKP.Text = "Tổng kinh phí: " + dgvDanhsach.CurrentRow.Cells["Tổng kinh phí"].Value.ToString();
                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "1")
                    optCN.Checked = true;
                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "2")
                    optDN.Checked = true;
                if (dgvDanhsach.CurrentRow.Cells["Loại KH"].Value.ToString() == "3")
                    optLDDN.Checked = true;
                layLoaiKH();
                cbLoaiKH.Text = dgvDanhsach.CurrentRow.Cells["Đối tượng KH"].Value.ToString();

            }
            catch { }
        }

     

       

       
      

             
    }
}