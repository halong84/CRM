using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace CRM
{
    public partial class frmCSKH_PheDuyet : Form
    {
        public frmCSKH_PheDuyet()
        {
            InitializeComponent();
            dtpFrom.CustomFormat = "MM/yyyy";
            DateTime dtCurrent = DateTime.Now;
            //dtpThang.Value = dtCurrentTime.AddMonths(-1);
            if (dtCurrent.Month == 1)
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
            }
            else
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
            }
            dtpFrom.Enabled = false;
        }

        private void frmCSKH_PheDuyet_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           laydanhsachpheduyet();
           kiemtrapheduyet();
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[6].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                dgvDanhsach.Rows[i].Cells[6].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //Kiem tra da cham diem chua

            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[6].Value.ToString() == "True")
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
                    if (dgvDanhsach.Rows[i].Cells[6].Value.ToString() == "True")
                    {
                        int pheduyet = 0;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[6].Value.ToString()) == true)
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
                            String strCmd = "Update lichsudiem set pheduyet='" + pheduyet + "',Ngaypheduyet='" + ngaypheduyet + "',Nguoipheduyet='" + frmDangnhap.UserID + "' ";
                            strCmd += " Where MAKH='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and thang='" + dtpFrom.Text + "' ";
                            
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                            //Cap nhat diem cho cac khach hang da co diem                           
                            DataTable dt = new DataTable();
                            dt.Clear();
                            strCmd = "select * from diem_cn where makh='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                            frmMain.conn.Close();
                            if(dt.Rows.Count>0)                            
                            {
                                strCmd = "Update diem_cn set diem = diem + " + dgvDanhsach.Rows[i].Cells[4].Value + ",ngaycapnhat='"+ngaypheduyet+"' where makh='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "'";
                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }
                                frmMain.conn.Open();
                                frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
                            }
                            //Insert khach hang chua co diem trong file diem_cn
                            else
                            {
                                strCmd = "insert diem_cn values('" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "','" + VietNamese2English(dgvDanhsach.Rows[i].Cells[2].Value.ToString()) + "','" + dgvDanhsach.Rows[i].Cells[7].Value.ToString() + "','" + dgvDanhsach.Rows[i].Cells[8].Value.ToString() + "','" + dgvDanhsach.Rows[i].Cells[9].Value.ToString() + "'," + dgvDanhsach.Rows[i].Cells[4].Value.ToString() + ",'" + ngaypheduyet + "','F','','"+CRM.frmMain.cn+"')";
                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }
                                frmMain.conn.Open();
                                frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
                            }
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
                MessageBox.Show("Đã phê duyệt!");
                btnConfirm.Enabled = false;
                laydanhsachpheduyet();
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }
        private void laydanhsachpheduyet()
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            //sCommand = "select khachhang.MAKH,khachhang.HOTEN,sum(lichsudiem.diem) as diem,pheduyet from LICHSUDIEM,KHACHHANG where lichsudiem.MAKH=KHACHHANG.MAKH and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and lichsudiem.thang='"+dtpFrom.Text+"' group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,pheduyet";
            if(optTatca.Checked==true)
                sCommand = "select lichsudiem.thang,khachhang.HOTEN,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.CMND,LICHSUDIEM.MAKH, diem_cn.diem as diemthangtruoc,sum(LICHSUDIEM.DIEM) as diem,LICHSUDIEM.PHEDUYET from LICHSUDIEM left join diem_cn on lichsudiem.makh=diem_cn.makh inner join khachhang on lichsudiem.makh=khachhang.makh group by LICHSUDIEM.MAKH,LICHSUDIEM.PHEDUYET,diem_cn.DIEM,LICHSUDIEM.THANG,khachhang.hoten,khachhang.loaikh,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.CMND having khachhang.loaikh=1 and LEFT(lichsudiem.makh,4)='" + frmMain.cn + "'";
            if (optPD.Checked == true)
                sCommand = "select lichsudiem.thang,khachhang.HOTEN,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.CMND,LICHSUDIEM.MAKH, diem_cn.diem as diemthangtruoc,sum(LICHSUDIEM.DIEM) as diem,LICHSUDIEM.PHEDUYET from LICHSUDIEM left join diem_cn on lichsudiem.makh=diem_cn.makh inner join khachhang on lichsudiem.makh=khachhang.makh group by LICHSUDIEM.MAKH,LICHSUDIEM.PHEDUYET,diem_cn.DIEM,LICHSUDIEM.THANG,khachhang.hoten,khachhang.loaikh,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.CMND having lichsudiem.pheduyet=1 and khachhang.loaikh=1 and LEFT(lichsudiem.makh,4)='" + frmMain.cn + "'";
            if (optCPD.Checked == true)
                sCommand = "select lichsudiem.thang,khachhang.HOTEN,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.CMND,LICHSUDIEM.MAKH, diem_cn.diem as diemthangtruoc,sum(LICHSUDIEM.DIEM) as diem,LICHSUDIEM.PHEDUYET from LICHSUDIEM left join diem_cn on lichsudiem.makh=diem_cn.makh inner join khachhang on lichsudiem.makh=khachhang.makh group by LICHSUDIEM.MAKH,LICHSUDIEM.PHEDUYET,diem_cn.DIEM,LICHSUDIEM.THANG,khachhang.hoten,khachhang.loaikh,khachhang.dienthoai1,khachhang.ngaysinh,khachhang.CMND having lichsudiem.pheduyet=0 and khachhang.loaikh=1 and LEFT(lichsudiem.makh,4)='" + frmMain.cn + "'";


            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            dskh.Clear();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Điểm tháng trước", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Số điểm", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Tổng điểm", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Phê duyệt", typeof(bool));
            dskh.Columns.Add(col);
            col = new DataColumn("SĐT", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;
            int temp = 0;
            if (iRows > 0)
            {
                for (int i = 0; i < iRows; i++)
                {

                    if (dt.Rows[i]["thang"].ToString() == dtpFrom.Text)
                    {
                        DataRow row = dskh.NewRow();
                        temp = temp + 1;
                        row[0] = temp;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        if (dt.Rows[i]["diemthangtruoc"].ToString() == "")
                            row[3] = 0;
                        else
                            row[3] = dt.Rows[i]["diemthangtruoc"].ToString();
                        row[4] = dt.Rows[i]["diem"].ToString();
                        if (dt.Rows[i]["pheduyet"].ToString() == "True")
                        {
                            if (dt.Rows[i]["thang"].ToString() == "01/2013")
                            {
                                row[3] = 0;
                                row[5] = row[4];
                            }
                            else
                            {
                                //row[5] = dt.Rows[i]["diemthangtruoc"].ToString();
                                row[5] = row[3];
                                row[3] = Convert.ToDecimal(row[5].ToString()) - Convert.ToDecimal(row[4].ToString());
                            }
                        }
                        else
                            row[5] = Convert.ToDecimal(row[3].ToString()) + Convert.ToDecimal(row[4].ToString());

                        if (dt.Rows[i]["pheduyet"].ToString() == "False")
                            row[6] = false;
                        else
                        {
                            row[6] = true;
                            
                        }
                        row[7] = dt.Rows[i]["dienthoai1"].ToString();
                        row[8] = dt.Rows[i]["ngaysinh"].ToString();
                        row[9] = dt.Rows[i]["CMND"].ToString();
                        dskh.Rows.Add(row);
                    }


                }
            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].ReadOnly = true;
            dgvDanhsach.Columns[1].ReadOnly = true;
            dgvDanhsach.Columns[2].ReadOnly = true;
            dgvDanhsach.Columns[3].ReadOnly = true;
            dgvDanhsach.Columns[4].ReadOnly = true;
            dgvDanhsach.Columns[5].ReadOnly = true;
            dgvDanhsach.Columns[6].ReadOnly = false;
            dgvDanhsach.Columns[7].Visible = false;
            dgvDanhsach.Columns[8].Visible = false;
            dgvDanhsach.Columns[9].Visible = false;
            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
        }
        
        public static string VietNamese2English(string unicodeString)
        {
            try
            {

                unicodeString = unicodeString.ToLower();
                unicodeString = Regex.Replace(unicodeString, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                unicodeString = Regex.Replace(unicodeString, "[éèẻẽẹêếềểễệ]", "e");
                unicodeString = Regex.Replace(unicodeString, "[iíìỉĩị]", "i");
                unicodeString = Regex.Replace(unicodeString, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                unicodeString = Regex.Replace(unicodeString, "[úùủũụưứừửữự]", "u");
                unicodeString = Regex.Replace(unicodeString, "[yýỳỷỹỵ]", "y");
                unicodeString = Regex.Replace(unicodeString, "[đ]", "d");

                unicodeString = unicodeString.ToUpper();



                return unicodeString;
            }
            catch
            {
                return "";
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int dem = 0;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if (dgvDanhsach.Rows[i].Cells[6].Value.ToString() == "True")
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
                    if (dgvDanhsach.Rows[i].Cells[6].Value.ToString() == "True")
                    {
                        int pheduyet = 1;
                        if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[6].Value.ToString()) == true)
                        {
                            pheduyet = 0;
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
                            String strCmd = "Update lichsudiem set pheduyet='" + pheduyet + "',Ngaypheduyet='" + ngaypheduyet + "',Nguoipheduyet='" + frmDangnhap.UserID + "' ";
                            strCmd += " Where MAKH='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "' and thang='" + dtpFrom.Text + "' ";

                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(strCmd, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                            //Cap nhat diem cho cac khach hang da co diem                           
                            strCmd = "Update diem_cn set diem = diem - " + dgvDanhsach.Rows[i].Cells[4].Value + ",ngaycapnhat='" + ngaypheduyet + "' where makh='" + dgvDanhsach.Rows[i].Cells[1].Value.ToString() + "'";
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
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Đã hủy phê duyệt!");
                btnCancel.Enabled = false;
                laydanhsachpheduyet();
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
        }
        private void kiemtrapheduyet()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            String sCommand = "";
            //sCommand = "select khachhang.MAKH,khachhang.HOTEN,sum(lichsudiem.diem) as diem,pheduyet from LICHSUDIEM,KHACHHANG where lichsudiem.MAKH=KHACHHANG.MAKH and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and lichsudiem.thang='"+dtpFrom.Text+"' group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,pheduyet";
            sCommand = "select * from lichsudiem where LEFT(lichsudiem.makh,4)='" + frmMain.cn + "' and thang ='"+dtpFrom.Text+"' and pheduyet=1 ";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            if (dt.Rows.Count > 0)
            {
                btnCancel.Visible = true;
                btnConfirm.Enabled = false;
            }
            else
            {
                btnCancel.Visible = false;
                btnConfirm.Enabled = true;
            }
        }    
    }
}