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
    public partial class frmBHThu : Form
    {
        string sohd = "";
        decimal tongphi = 0,hoahongtl=0;
        public frmBHThu()
        {
            InitializeComponent();
        }

        private void txtSotien_TextChanged(object sender, EventArgs e)
        {
            if (txtSotien.Text != "")
            {
                string sDummy = txtSotien.Text;
                try
                {
                    int iKeep = txtSotien.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtSotien.Text[i] == ',')
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
                    txtSotien.Text = sDummy;
                    txtSotien.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtSotien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmBHThu_Load(object sender, EventArgs e)
        {
            layDSHD();

        }

        private void layDSHD()
        {
            dgvDanhsach.Refresh();
            DataTable dt = new DataTable();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            String strCmd = "";
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số hợp đồng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số GCN", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã khách hàng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại hình BH", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tổng phí", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉ lệ HH", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tiền Hoa hồng", typeof(decimal));
            dtDanhsach.Columns.Add(col);            
                     
                      
            if (optSHD.Checked==true)
            {
                if (txtTim.Text == "")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + Thongtindangnhap.macn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.sohd like '%" + txtTim.Text + "%' and hdbh.macn='" + Thongtindangnhap.macn + "'";
            }
            if (optSCN.Checked == true)
            {
                if (txtTim.Text == "")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + Thongtindangnhap.macn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.SOGCNBH like '%" + txtTim.Text + "%' and hdbh.macn='" + Thongtindangnhap.macn + "'";
            }
            if (optMAKH.Checked == true)
            {
                if (txtTim.Text == "")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + Thongtindangnhap.macn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.makh like '%" + txtTim.Text + "%' and hdbh.macn='" + Thongtindangnhap.macn + "'";
            }
            if (optTen.Checked== true)
            {
                if (txtTim.Text == "")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + Thongtindangnhap.macn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and khachhang.hoten like N'%" + txtTim.Text + "%' and hdbh.macn='" + Thongtindangnhap.macn + "'";
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
                    row[1] = dt.Rows[i]["SOHD"].ToString();
                    row[2] = dt.Rows[i]["SOGCNBH"].ToString();
                    row[3] = dt.Rows[i]["MAKH"].ToString();
                    row[4] = dt.Rows[i]["HOTEN"].ToString();
                    row[5] = dt.Rows[i]["TEN"].ToString();
                    string ngaybatdau, ngayketthuc;
                    ngaybatdau = dt.Rows[i]["tungay"].ToString();
                    ngayketthuc = dt.Rows[i]["denngay"].ToString();

                    string ngayBD, thangBD, namBD, ngayKT, thangKT, namKT;
                    ngayBD = ngaybatdau.Substring(0, 2);
                    thangBD = ngaybatdau.Substring(3, 2);
                    namBD = ngaybatdau.Substring(6, 4);
                    ngayKT = ngayketthuc.Substring(0, 2);
                    thangKT = ngayketthuc.Substring(3, 2);
                    namKT = ngayketthuc.Substring(6, 4);

                    row[6] = ngayBD + "/" + thangBD + "/" + namBD;
                    row[7] = ngayKT + "/" + thangKT + "/" + namKT;                   
                    row[8] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["TONGPHI"].ToString()));
                    row[9] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["HOAHONGTL"].ToString()));                   
                    row[10] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["HOAHONG"].ToString()));                    
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
        }

        private void layDSThu(string s)
        {
            dgvDSThu.Refresh();
            DataTable dt = new DataTable();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            String strCmd = "";
            col = new DataColumn("STT", typeof(int));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Số hợp đồng", typeof(string));
            dtDanhsach.Columns.Add(col);            
            col = new DataColumn("Ngày thu", typeof(string));
            dtDanhsach.Columns.Add(col);            
            col = new DataColumn("Số tiền", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Hoa hồng", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);
            
            strCmd = "Select thubh.* from thubh,hdbh where thubh.sohd=hdbh.sohd and thubh.sohd='"+s+"'and hdbh.macn='" + Thongtindangnhap.macn + "'";
            
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
                    row[1] = dt.Rows[i]["SOHD"].ToString();                    
                    string ngay;
                    ngay = dt.Rows[i]["ngay"].ToString();
                    string ngaythu, thangthu, namthu;
                    ngaythu = ngay.Substring(0, 2);
                    thangthu = ngay.Substring(3, 2);
                    namthu = ngay.Substring(6, 4);                   

                    row[2] = ngaythu + "/" + thangthu + "/" + namthu;                    
                    row[3] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["SOTIEN"].ToString()));                    
                    row[4] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["HOAHONG"].ToString()));
                    row[5] = dt.Rows[i]["Ghichu"].ToString(); 
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDSThu.DataSource = dtDanhsach;
            dgvDSThu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDSThu.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDSThu.Columns[0].Width = 40;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            layDSHD();
        }

        private void dgvDanhsach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sohd = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[1].Value.ToString();
            tongphi = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[8].Value.ToString());
            hoahongtl = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[9].Value.ToString());

            layDSThu(sohd);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtSoHD.Text = sohd;
            dtpNgay.Enabled = true;
            txtSotien.Enabled = true;
            txtGhichu.Enabled = true;
            txtSotien.Text = "";
            txtHoahong.Text = "";
            txtGhichu.Text = "";
            btnAdd.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                //Dua du lieu vao bang ThuBH
                String ngay, sCommand;
                if (txtSotien.Text == "")
                    txtSotien.Text = "0";
                if (txtHoahong.Text == "")
                    txtHoahong.Text = "0";
                ngay = dtpNgay.Text.Substring(3, 2) + "/" + dtpNgay.Text.Substring(0, 2) + "/" + dtpNgay.Text.Substring(6, 4);
                if (Convert.ToDecimal(txtSotien.Text.Replace(",", "")) > tongphi)
                {
                    MessageBox.Show("So tien thu phi lon hon gia tri hop dong");
                    return;
                }
                else
                {
                    sCommand = "insert into THUBH (SoHD,ngay,sotien,hoahong,ghichu) values('" + txtSoHD.Text + "','" + ngay + "'," + Convert.ToDecimal(txtSotien.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtHoahong.Text.Replace(",", "")) + ",N'" + txtGhichu.Text + "')";

                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                    layDSThu(sohd);
                }
            }
            catch
            //Cap nhat thu bao hiem
            {
                String ngay, sCommand;
                ngay = dtpNgay.Text.Substring(3, 2) + "/" + dtpNgay.Text.Substring(0, 2) + "/" + dtpNgay.Text.Substring(6, 4);
                sCommand = "Update ThuBH set sotien=" + Convert.ToDecimal(txtSotien.Text.Replace(",", "")) + ",hoahong=" + Convert.ToDecimal(txtHoahong.Text.Replace(",", "")) + ",Ghichu=N'" + txtGhichu.Text + "' where sohd='" + txtSoHD.Text + "' and ngay='" + ngay + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                frmMain.myCommand.ExecuteNonQuery();
                DataAccess.conn.Close();
                layDSThu(sohd);
            }
            btnAdd.Enabled = true;
        }

        private void dgvDSThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtpNgay.Enabled = true;
            txtSotien.Enabled = true;
            txtGhichu.Enabled = true;
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            try
            {
                txtSoHD.Text = dgvDSThu.CurrentRow.Cells["Số hợp đồng"].Value.ToString();
                dtpNgay.Text = dgvDSThu.CurrentRow.Cells["Ngày thu"].Value.ToString();
                txtSotien.Text = dgvDSThu.CurrentRow.Cells["Số tiền"].Value.ToString();
                txtHoahong.Text = dgvDSThu.CurrentRow.Cells["Hoa hồng"].Value.ToString();
                txtGhichu.Text = dgvDSThu.CurrentRow.Cells["Ghi chú"].Value.ToString();
            }

            catch { }
        }

        private void dgvDanhsach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            sohd = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[1].Value.ToString();
            tongphi = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[8].Value.ToString());
            hoahongtl = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[9].Value.ToString());
            layDSThu(sohd);
            txtSoHD.Text = sohd;
            dtpNgay.Text = "";
            txtSotien.Text = "0";
            txtHoahong.Text = "0";
            txtGhichu.Text = "";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string strngay = dtpNgay.Text.Substring(3,2) + "/" + dtpNgay.Text.Substring(0, 2) + "/" + dtpNgay.Text.Substring(6, 4);
            String sCommand = "delete thubh where sohd='" + txtSoHD.Text + "' and ngay ='"+strngay+"'and sotien="+ Convert.ToDecimal(txtSotien.Text.Replace(",",""))+"";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();
            layDSThu(sohd);
            txtSoHD.Enabled = false;
            dtpNgay.Enabled = false;
            txtSotien.Text = "";
            txtSotien.Enabled = false;
            txtGhichu.Text = "";
            txtGhichu.Enabled = false;           
            btnDel.Enabled = false;
            btnAdd.Enabled = true;
        }

        private void txtSotien_Leave(object sender, EventArgs e)
        {
            if (txtSotien.Text != "")
            {
                txtHoahong.Text = Convert.ToString(hoahongtl/100 * Convert.ToDecimal(txtSotien.Text.Replace(",", "")));
            }

        }

        private void txtHoahong_TextChanged(object sender, EventArgs e)
        {
            if (txtHoahong.Text != "")
            {
                string sDummy = txtHoahong.Text;
                try
                {
                    int iKeep = txtHoahong.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtHoahong.Text[i] == ',')
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
                    txtHoahong.Text = sDummy;
                    txtHoahong.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }
            
    }
}