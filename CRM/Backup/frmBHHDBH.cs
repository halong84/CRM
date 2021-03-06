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
namespace CRM
{
    public partial class frmBHHDBH : Form
    {
        public frmBHHDBH()
        {
            InitializeComponent();
        }

        private void labelX7_Click(object sender, EventArgs e)
        {

        }

        private void txtChiphi_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX11_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtSoXe.Enabled = true;
            txtSoHD.Enabled = true;
            txtSoGCN.Enabled = true;
            txtMAKH.Enabled = true;
            dtpNgayBD.Enabled = true;
            dtpNgayKT.Enabled = true;
            txtTNDS.Enabled = true;
            txtLPX.Enabled = true;
            txtHH.Enabled = true;
            txtVCX.Enabled = true;
            txtVATTL.Enabled = true;
            //txtVATTT.Enabled = true;
            txtTongPhi.Enabled = true;
            txtTLHH.Enabled = true;
            //txtTTHH.Enabled = true;
            txtGhiChu.Enabled = true;
            cbLHBH.Enabled = true;
            txtPhi.Enabled = true;

            txtSoXe.Text = "";
            txtSoHD.Text = "";
            txtSoGCN.Text = "";
            txtMAKH.Text = "";
            dtpNgayBD.Enabled = true;
            dtpNgayKT.Enabled = true;
            txtTNDS.Text = "";
            txtLPX.Text = "";
            txtHH.Text = "";
            txtVCX.Text = "";
            txtVATTL.Text = "";
            //txtVATTT.Enabled = true;
            txtTongPhi.Text = "";
            txtTLHH.Text = "";
            //txtTTHH.Enabled = true;
            txtGhiChu.Text = "";
            cbLHBH.Text = "";
            txtPhi.Text = "";

            btnAdd.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
           
            try
            {
                //Dua du lieu vao bang HDBH
                String ngaybd, ngaykt, sCommand,ngay;
                if (txtTNDS.Text == "")
                    txtTNDS.Text = "0";
                if (txtLPX.Text == "")
                    txtLPX.Text = "0";
                if (txtHH.Text == "")
                    txtHH.Text = "0";
                if (txtVCX.Text == "")
                    txtVCX.Text = "0";
                if (txtPhi.Text == "")
                    txtPhi.Text = "0";
                if (txtVATTL.Text == "")
                    txtVATTL.Text = "0";
                if (txtVATTT.Text == "")
                    txtVATTT.Text = "0";
                if (txtTongPhi.Text == "")
                    txtTongPhi.Text = "0";
                if (txtTLHH.Text == "")
                    txtTLHH.Text = "0";
                if (txtTTHH.Text == "")
                    txtTTHH.Text = "0";

                ngaybd = dtpNgayBD.Text.Substring(3, 2) + "/" + dtpNgayBD.Text.Substring(0, 2) + "/" + dtpNgayBD.Text.Substring(6, 4);
                ngaykt = dtpNgayKT.Text.Substring(3, 2) + "/" + dtpNgayKT.Text.Substring(0, 2) + "/" + dtpNgayKT.Text.Substring(6, 4);
                ngay = ngaybd;
                if ((cbLHBH.Text == "Xe cơ giới")||(cbLHBH.Text == "Tàu"))
                {
                    sCommand = "insert into HDBH(SoHD,SoGCNBH,MAKH,ngay,tungay,denngay,maloaihinh,soxe,tnds,lpx,hh,vcx,phibh,vattl,vat,tongphi,hoahongtl,hoahong,ghichu,cbtd,macn) values('" + txtSoHD.Text + "','" + txtSoGCN.Text + "','" + txtMAKH.Text + "','" + ngay + "','" + ngaybd + "','" + ngaykt + "','" + cbLHBH.SelectedValue.ToString() + "','" + txtSoXe.Text + "'," + Convert.ToDecimal(txtTNDS.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtLPX.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtHH.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtVCX.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtPhi.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtVATTL.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtVATTT.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtTongPhi.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtTLHH.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtTTHH.Text.Replace(",", "")) + ",N'" + txtGhiChu.Text + "','" + frmDangnhap.UserID + "','" + frmMain.cn + "')";
                }
                else
                {
                    sCommand = "insert into HDBH(SoHD,SoGCNBH,MAKH,ngay,tungay,denngay,maloaihinh,phibh,vattl,vat,tongphi,hoahongtl,hoahong,ghichu,cbtd,macn) values('" + txtSoHD.Text + "','" + txtSoGCN.Text + "','" + txtMAKH.Text + "','" + ngay + "','" + ngaybd + "','" + ngaykt + "','" + cbLHBH.SelectedValue.ToString() + "'," + Convert.ToDecimal(txtPhi.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtVATTL.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtVATTT.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtTongPhi.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtTLHH.Text.Replace(",", "")) + "," + Convert.ToDecimal(txtTTHH.Text.Replace(",", "")) + ",N'" + txtGhiChu.Text + "','" + frmDangnhap.UserID + "','" + frmMain.cn + "')";
                }
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                MessageBox.Show("Đã lưu!");
                layDSHDBH();

            }
            catch
            //Cap nhat hop dong bao hiem
            {
                String ngaybd, ngaykt, sCommand;
                ngaybd = dtpNgayBD.Text.Substring(3, 2) + "/" + dtpNgayBD.Text.Substring(0, 2) + "/" + dtpNgayBD.Text.Substring(6, 4);
                ngaykt = dtpNgayKT.Text.Substring(3, 2) + "/" + dtpNgayKT.Text.Substring(0, 2) + "/" + dtpNgayKT.Text.Substring(6, 4);
                if ((cbLHBH.Text == "Xe cơ giới")||(cbLHBH.Text == "Tàu"))
                {
                    sCommand = "Update HDBH set tungay='"+ngaybd+"',denngay='"+ngaykt+"',soxe ='" + txtSoXe.Text + "',TNDS="+ Convert.ToDecimal(txtTNDS.Text.Replace(",","")) + ",LPX="+ Convert.ToDecimal(txtLPX.Text.Replace(",","")) + ",HH="+ Convert.ToDecimal(txtHH.Text.Replace(",","")) + ",VCX="+ Convert.ToDecimal(txtVCX.Text.Replace(",","")) + ",PhiBH="+ Convert.ToDecimal(txtPhi.Text.Replace(",","")) + ",VATTL="+ Convert.ToDecimal(txtVATTL.Text.Replace(",","")) + ",VAT="+ Convert.ToDecimal(txtVATTT.Text.Replace(",","")) + ",Tongphi="+ Convert.ToDecimal(txtTongPhi.Text.Replace(",","")) + ",hoahongtl="+ Convert.ToDecimal(txtTLHH.Text.Replace(",","")) + ",hoahong="+ Convert.ToDecimal(txtTTHH.Text.Replace(",","")) + ",ghichu=N'" + txtGhiChu.Text + "' where SOHD ='"+txtSoHD.Text+"'";

                }
                else
                {
                    sCommand = "Update HDBH set tungay='" + ngaybd + "',denngay='" + ngaykt + "',PhiBH=" + Convert.ToDecimal(txtPhi.Text.Replace(",","")) + ",VATTL=" + Convert.ToDecimal(txtVATTL.Text.Replace(",","")) + ",VAT=" + Convert.ToDecimal(txtVATTT.Text.Replace(",","")) + ",Tongphi=" + Convert.ToDecimal(txtTongPhi.Text.Replace(",","")) + ",hoahongtl=" + Convert.ToDecimal(txtTLHH.Text.Replace(",","")) + ",hoahong=" + Convert.ToDecimal(txtTTHH.Text.Replace(",","")) + ",ghichu=N'" + txtGhiChu.Text + "' where SOHD ='" + txtSoHD.Text + "'";
                }

                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                frmMain.myCommand.ExecuteNonQuery();
                frmMain.conn.Close();
                MessageBox.Show("Đã cập nhật!");
                layDSHDBH();
            }
            btnAdd.Enabled = true;
           
        }

        private void frmBHHDBH_Load(object sender, EventArgs e)
        {
            layLoaiHinhBaoHiem();
            layDSHDBH();
        }
        private void layLoaiHinhBaoHiem()
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            sCommand = "SELECT * from dmlhbh order by MALOAIHINH desc";           
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbLHBH.DataSource = dt;
            cbLHBH.DisplayMember = "Ten";
            cbLHBH.ValueMember = "maloaihinh";           
            cbLHBH.Enabled = false;
           
        }
        private void layDSHDBH()
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
            col = new DataColumn("Số xe", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("TNDS", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("LPX", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Hàng hóa", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("VCX", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Phí BH", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("VAT", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tiền VAT", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tổng phí", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Hoa hồng", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tiền Hoa hồng", typeof(decimal));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày bắt đầu", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày kết thúc", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));
            dtDanhsach.Columns.Add(col);
            if (tabControl1.SelectedTabIndex == 0)
            {
                if(textBox4.Text=="")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + CRM.frmMain.cn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.makh like '%"+textBox4.Text+"%' and hdbh.macn='" + CRM.frmMain.cn + "'";
            }
            if (tabControl1.SelectedTabIndex == 1)
            {
                if (textBox2.Text == "")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + CRM.frmMain.cn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and khachhang.hoten like N'%" + textBox2.Text + "%' and hdbh.macn='" + CRM.frmMain.cn + "'";
            }
            if (tabControl1.SelectedTabIndex == 2)
            {
                if (textBox3.Text == "")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + CRM.frmMain.cn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.sohd ='" + textBox3.Text + "' and hdbh.macn='" + CRM.frmMain.cn + "'";
            }
            if (tabControl1.SelectedTabIndex ==3)
            {
                if (txt_SoGCN.Text == "")
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.macn='" + CRM.frmMain.cn + "'";
                else
                    strCmd = "Select khachhang.hoten,hdbh.*,dmlhbh.ten from khachhang,hdbh,dmlhbh where khachhang.makh=hdbh.makh and hdbh.maloaihinh=dmlhbh.maloaihinh and hdbh.SoGCNBH = '" + txt_SoGCN.Text + "' and hdbh.macn='" + CRM.frmMain.cn + "'";
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

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
                    //Bao hiem xe co gioi
                    if ((dt.Rows[i]["MALOAIHINH"].ToString() == "03")||(dt.Rows[i]["MALOAIHINH"].ToString() == "04"))
                    {
                        //if(dt.Rows[i]["SOXE"].ToString()
                        row[6] = dt.Rows[i]["SOXE"].ToString();
                        row[7] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["TNDS"].ToString()));
                        row[8] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["LPX"].ToString()));
                        row[9] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["HH"].ToString()));
                        row[10] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["VCX"].ToString()));                        
                    }
                    else
                    //Cac loai hinh bao hiem khac
                    {
                        row[6] = "";
                        row[7] = 0;
                        row[8] = 0;
                        row[9] = 0;
                        row[10] = 0;
                    }
                    row[11] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["PHIBH"].ToString()));
                    row[12] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["VATTL"].ToString()));
                    row[13] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["VAT"].ToString()));
                    row[14] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["TONGPHI"].ToString()));
                    row[15] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["HOAHONGTL"].ToString()));
                    row[16] = String.Format("{0:0}", Decimal.Parse(dt.Rows[i]["HOAHONG"].ToString()));


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

                    row[17] = ngayBD + "/" + thangBD + "/" + namBD;
                    row[18] = ngayKT + "/" + thangKT + "/" + namKT;                   
                    
                    row[19] = dt.Rows[i]["Ghichu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            
        }

        private void dgvDanhsach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSoHD.Enabled = true;
            txtSoGCN.Enabled = true;
            txtMAKH.Enabled = true;
            dtpNgayBD.Enabled = true;
            dtpNgayKT.Enabled = true;
            txtTNDS.Enabled = true;
            txtLPX.Enabled = true;
            txtHH.Enabled = true;
            txtVCX.Enabled = true;
            txtVATTL.Enabled = true;
            //txtVATTT.Enabled = true;
            txtTongPhi.Enabled = true;
            txtTLHH.Enabled = true;
            //txtTTHH.Enabled = true;
            txtGhiChu.Enabled = true;
            cbLHBH.Enabled = true;
            txtPhi.Enabled = true;
            btnModify.Enabled = true;
            btnDel.Enabled = true;
            try
            {
                txtSoHD.Text = dgvDanhsach.CurrentRow.Cells["Số hợp đồng"].Value.ToString();
                txtSoGCN.Text = dgvDanhsach.CurrentRow.Cells["Số GCN"].Value.ToString();
                txtMAKH.Text = dgvDanhsach.CurrentRow.Cells["Mã khách hàng"].Value.ToString();
                labelX4.Text = dgvDanhsach.CurrentRow.Cells["Họ tên"].Value.ToString();
                cbLHBH.Text= dgvDanhsach.CurrentRow.Cells["Loại hình BH"].Value.ToString();
                if (cbLHBH.Text == "Xe cơ giới")
                {
                    txtSoXe.Text = dgvDanhsach.CurrentRow.Cells["Số xe"].Value.ToString();
                    txtTNDS.Text = dgvDanhsach.CurrentRow.Cells["TNDS"].Value.ToString();
                    txtLPX.Text = dgvDanhsach.CurrentRow.Cells["LPX"].Value.ToString();
                    txtHH.Text = dgvDanhsach.CurrentRow.Cells["Hàng hóa"].Value.ToString();
                    txtVCX.Text = dgvDanhsach.CurrentRow.Cells["VCX"].Value.ToString();                 

                }
                else
                {
                    txtTNDS.Enabled = false;
                    txtLPX.Enabled = false;
                    txtHH.Enabled=false;
                    txtVCX.Enabled=false;
                }
                txtPhi.Text = dgvDanhsach.CurrentRow.Cells["Phí BH"].Value.ToString();
                txtVATTL.Text = dgvDanhsach.CurrentRow.Cells["VAT"].Value.ToString();
                txtVATTT.Text = dgvDanhsach.CurrentRow.Cells["Tiền VAT"].Value.ToString();
                txtTongPhi.Text = dgvDanhsach.CurrentRow.Cells["Tổng phí"].Value.ToString();
                txtTLHH.Text = dgvDanhsach.CurrentRow.Cells["Hoa hồng"].Value.ToString();
                txtTTHH.Text = dgvDanhsach.CurrentRow.Cells["Tiền Hoa hồng"].Value.ToString();
                dtpNgayBD.Text = dgvDanhsach.CurrentRow.Cells["Ngày bắt đầu"].Value.ToString();
                dtpNgayKT.Text = dgvDanhsach.CurrentRow.Cells["Ngày kết thúc"].Value.ToString();
                txtGhiChu.Text = dgvDanhsach.CurrentRow.Cells["Ghi chú"].Value.ToString();
            }
            catch { }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            String sCommand = "delete hdbh where sohd='" + txtSoHD.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
            layDSHDBH();
            txtSoHD.Enabled = false;
            txtSoGCN.Enabled = false;
            txtMAKH.Enabled = false;
            dtpNgayBD.Enabled = false;
            dtpNgayKT.Enabled = false;
            txtTNDS.Enabled = false;
            txtLPX.Enabled = false;
            txtHH.Enabled = false;
            txtVCX.Enabled = false;
            txtVATTL.Enabled = false;
            txtVATTT.Enabled = false;
            txtTongPhi.Enabled = false;
            txtTLHH.Enabled = false;
            txtTTHH.Enabled = false;
            txtGhiChu.Enabled = false;
            btnDel.Enabled = false;
            btnAdd.Enabled = true;

        }

        private void txtTNDS_TextChanged(object sender, EventArgs e)
        {
            if (txtTNDS.Text != "")
            {
                string sDummy = txtTNDS.Text;
                try
                {
                    int iKeep = txtTNDS.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtTNDS.Text[i] == ',')
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
                    txtTNDS.Text = sDummy;
                    txtTNDS.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtLPX_TextChanged(object sender, EventArgs e)
        {
            if (txtLPX.Text != "")
            {
                string sDummy = txtLPX.Text;
                try
                {
                    int iKeep = txtLPX.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtLPX.Text[i] == ',')
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
                    txtLPX.Text = sDummy;
                    txtLPX.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }
        }

        private void txtHH_TextChanged(object sender, EventArgs e)
        {
            if (txtHH.Text != "")
            {
                string sDummy = txtHH.Text;
                try
                {
                    int iKeep = txtHH.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtHH.Text[i] == ',')
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
                    txtHH.Text = sDummy;
                    txtHH.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            } 
        }

        private void txtVCX_TextChanged(object sender, EventArgs e)
        {
            if (txtVCX.Text != "")
            {
                string sDummy = txtVCX.Text;
                try
                {
                    int iKeep = txtVCX.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtVCX.Text[i] == ',')
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
                    txtVCX.Text = sDummy;
                    txtVCX.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            } 
        }

        private void txtPhi_TextChanged(object sender, EventArgs e)
        {
            if (txtPhi.Text != "")
            {
                string sDummy = txtPhi.Text;
                try
                {
                    int iKeep = txtPhi.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtPhi.Text[i] == ',')
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
                    txtPhi.Text = sDummy;
                    txtPhi.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
                
            } 
        }

        private void txtVATTL_TextChanged(object sender, EventArgs e)
        {
            if (txtVATTL.Text != "")
            {
                string sDummy = txtVATTL.Text;
                try
                {
                    int iKeep = txtVATTL.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtVATTL.Text[i] == ',')
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
                   
                    txtVATTL.Text = sDummy;
                    txtVATTL.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
                
            } 
        }

        private void txtTongPhi_TextChanged(object sender, EventArgs e)
        {
            if (txtTongPhi.Text != "")
            {
                string sDummy = txtTongPhi.Text;
                try
                {
                    int iKeep = txtTongPhi.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtTongPhi.Text[i] == ',')
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
                    txtTongPhi.Text = sDummy;
                    txtTongPhi.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
                
            } 
        }

        private void txtTNDS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLPX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtVCX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtVATTL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtVATTT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTongPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTLHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTTHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            layDSHDBH();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            layDSHDBH();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            layDSHDBH();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            layDSHDBH();
        }

        private void txtVATTT_TextChanged(object sender, EventArgs e)
        {
            if (txtVATTT.Text != "")
            {
                string sDummy = txtVATTT.Text;
                try
                {
                    int iKeep = txtVATTT.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtVATTT.Text[i] == ',')
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
                    txtVATTT.Text = sDummy;
                    txtVATTT.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            } 
        }

        private void txtTLHH_TextChanged(object sender, EventArgs e)
        {
            if (txtTLHH.Text != "")
            {
                string sDummy = txtTLHH.Text;
                try
                {
                    int iKeep = txtTLHH.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtTLHH.Text[i] == ',')
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
                    txtTLHH.Text = sDummy;
                    txtTLHH.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
                
            } 
        }

        private void txtTTHH_TextChanged(object sender, EventArgs e)
        {
            if (txtTTHH.Text != "")
            {
                string sDummy = txtTTHH.Text;
                try
                {
                    int iKeep = txtTTHH.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtTTHH.Text[i] == ',')
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
                    txtTTHH.Text = sDummy;
                    txtTTHH.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            } 
        }

        private void txtMAKH_Leave(object sender, EventArgs e)
        {
            String sCommand = "";
            DataTable dt = new DataTable();
            sCommand = "SELECT * from khachhang where makh='"+txtMAKH.Text+"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            if (dt.Rows.Count > 0)
                labelX4.Text = dt.Rows[0]["HOTEN"].ToString();
            else
                labelX4.Text = "";
        }

        private void txtPhi_Leave(object sender, EventArgs e)
        {
            if ((txtPhi.Text != "") && (txtVATTL.Text != ""))
            {
                txtVATTT.Text = Convert.ToString(Convert.ToDecimal(txtPhi.Text.Replace(",", "")) * Convert.ToDecimal(txtVATTL.Text.Replace(",", "")) / 100);
                txtTongPhi.Text = Convert.ToString(Convert.ToDecimal(txtPhi.Text.Replace(",", "")) + Convert.ToDecimal(txtVATTT.Text.Replace(",", "")));

            }
            if ((txtPhi.Text != "") && (txtTLHH.Text != ""))
                txtTTHH.Text = Convert.ToString(Convert.ToDecimal(txtPhi.Text.Replace(",", "")) * Convert.ToDecimal(txtTLHH.Text.Replace(",", "")) / 100);                
        }

        private void txtVATTL_Leave(object sender, EventArgs e)
        {
            if ((txtPhi.Text != "") && (txtVATTL.Text != ""))
            {
                txtVATTT.Text = Convert.ToString(Convert.ToDecimal(txtPhi.Text.Replace(",", "")) * Convert.ToDecimal(txtVATTL.Text.Replace(",", "")) / 100);
                txtTongPhi.Text = Convert.ToString(Convert.ToDecimal(txtPhi.Text.Replace(",", "")) + Convert.ToDecimal(txtVATTT.Text.Replace(",", "")));
            }
        }

        private void txtTongPhi_Leave(object sender, EventArgs e)
        {
            if((txtTongPhi.Text!="")&&(txtTLHH.Text!=""))
                txtTTHH.Text = Convert.ToString(Convert.ToDecimal(txtPhi.Text.Replace(",", "")) * Convert.ToDecimal(txtTLHH.Text.Replace(",", "")) / 100);                
        }

        private void txtTLHH_Leave(object sender, EventArgs e)
        {
            if ((txtTongPhi.Text != "") && (txtTLHH.Text != ""))
                txtTTHH.Text = Convert.ToString(Convert.ToDecimal(txtPhi.Text.Replace(",", "")) * Convert.ToDecimal(txtTLHH.Text.Replace(",", "")) / 100);                
        }

        private void cbLHBH_TextChanged(object sender, EventArgs e)
        {
            if ((cbLHBH.Text == "Xe cơ giới")||(cbLHBH.Text == "Tàu"))
            {
                txtSoXe.Enabled = true;
                txtTNDS.Enabled = true;
                txtLPX.Enabled = true;
                txtHH.Enabled = true;
                txtVCX.Enabled = true;
            }
            else
            {
                txtSoXe.Enabled = false;
                txtTNDS.Enabled = false;
                txtLPX.Enabled = false;
                txtHH.Enabled = false;
                txtVCX.Enabled = false;
            }
        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }


    }
}