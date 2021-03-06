using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CRM
{
    public partial class frmPheduyet_Lichchamsoc : Form
    {
        public static string strMaKHCS;
        public static string loaikh;
        public frmPheduyet_Lichchamsoc()
        {
            InitializeComponent();
        }

        private void frmPheduyet_Lichchamsoc_Load(object sender, EventArgs e)
        {
            

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            layDanhsach();
        }
        private void layDanhsach()
        {
            dgvDanhsach.Refresh();
            DataTable dt = new DataTable();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            String strCmd = "";
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
            col = new DataColumn("Phê duyệt", typeof(bool));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tháng", typeof(string));
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("LoaiKH", typeof(string));
            dtDanhsach.Columns.Add(col);
            string quy = cbQuy.Text + "/" + dtpThang.Text;

            if (optCN.Checked == true)
            {
                strCmd = "Select cs.*, xl.TENLOAI, li.Tieuchi as tieuchi  from KEHOACHCHAMSOC cs, DMXEPLOAIKH xl, Lichchamsoc li ";
                strCmd += " Where cs.THANG='" + quy + "' and xl.maloai=cs.xeploaikh and cs.matc=li.MaTC and cs.MACN='" + frmMain.cn + "' and cs.loaikh=1";
            }
            if (optDN.Checked == true)
            {
                strCmd = "Select cs.*, xl.TENLOAI, li.Tieuchi as tieuchi  from KEHOACHCHAMSOC cs, DMXEPLOAIKH xl, Lichchamsoc li ";
                strCmd += " Where cs.THANG='" + quy + "' and xl.maloai=cs.xeploaikh and cs.matc=li.MaTC and cs.MACN='" + frmMain.cn + "' and cs.loaikh=2";        
            }
            if (optLDDN.Checked == true)
            {
                strCmd = "Select cs.*, xl.TENLOAI, li.Tieuchi as tieuchi  from KEHOACHCHAMSOC cs, DMXEPLOAIKH xl, Lichchamsoc li ";
                strCmd += " Where cs.THANG='" + quy + "' and xl.maloai=cs.xeploaikh and cs.matc=li.MaTC and cs.MACN='" + frmMain.cn + "' and cs.loaikh=3";
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

                    bool pheduyet = false;
                    if (Boolean.Parse(dt.Rows[i]["pheduyet"].ToString()) == true)
                    {
                        pheduyet = true;
                    }
                    else
                    {
                        pheduyet = false;
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

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[10].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[10].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

       

        private void dgvDanhsach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            strMaKHCS = dgvDanhsach.CurrentRow.Cells["Mã"].Value.ToString();
            loaikh = dgvDanhsach.CurrentRow.Cells["LoaiKH"].Value.ToString();
            frmKHCS_Chitiet frmKHCSCT = new frmKHCS_Chitiet();
            frmKHCSCT.ShowDialog();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strCmd = "";
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[10].Value.ToString() == "True")
                {
                    dem++;
                    break;
                }
            }

            if (dem > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[10].Value.ToString() == "True")
                    {
                        int pheduyet = 0;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[10].Value.ToString()) == true)
                        {
                            pheduyet = 1;
                        }

                        string ngaypheduyet = "";
                        string nam, thang, ngay, gio, phut, giay, miligiay;
                        nam = String.Format("{0:00}", Int16.Parse(DateTime.Now.Year.ToString()));
                        thang = String.Format("{0:00}", Int16.Parse(DateTime.Now.Month.ToString()));
                        ngay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Day.ToString()));
                        gio = String.Format("{0:00}", Int16.Parse(DateTime.Now.Hour.ToString()));
                        phut = String.Format("{0:00}", Int16.Parse(DateTime.Now.Minute.ToString()));
                        giay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Second.ToString()));
                        miligiay = String.Format("{0:00}", Int16.Parse(DateTime.Now.Millisecond.ToString()));
                        ngaypheduyet = thang + "/" + ngay + "/" + nam + " " + gio + ":" + phut + ":" + giay + "." + miligiay;

                        try
                        {
                            strCmd = "Update kehoachchamsoc set pheduyet='" + pheduyet + "',Ngaypheduyet='" + ngaypheduyet + "',Nguoipheduyet='" + frmDangnhap.UserID + "'";
                            strCmd += " where ma ='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "'"; 
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
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
                layDanhsach();                
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã phê duyệt!");
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }
    }
}